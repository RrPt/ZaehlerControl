using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

// todo
// Kurven ein-/ausblenden
// Anzeige der Differenzen
// Beschriftungen, Einheiten
// 
// ...


namespace zaehlerNS
{
    [Serializable()]
    public class Zaehler
    {
        private String connectionString = "Data Source=KnxPC;Initial Catalog=EIB;Integrated Security=SSPI";
        private string name;
        private SortedList<DateTime, Double> values;
        private string datumSpaltenName;
        private string wertSpaltenName;
        private string tabellenName;
        private ZeitIntervall intervall = ZeitIntervall.all;
        private double anzTage = 2;
        ZaehlerControl myControl = null;
        SortedList<DateTime, double> rawData = new SortedList<DateTime, double>();
       

        #region Konstruktoren


        public Zaehler(string name, string datumSpaltenName, string wertSpaltenName, string tabellenName)
        {
            //            Zaehler z = new Zaehler("Wasser","select Top 100 Datum,Volumen from H2OZaehler order by datum desc");
            //Zaehler z = new Zaehler("Wasser", "Datum", "Volumen", "H2OZaehler");
            this.name = name;
            this.datumSpaltenName = datumSpaltenName;
            this.wertSpaltenName = wertSpaltenName;
            this.tabellenName = tabellenName;
            values = new SortedList<DateTime, double>();
            RawDataValid = false;
            DataCalculated = false;
            //readDataFromSql();
            //calculateData();
        }

        internal void MyControl(ZaehlerControl zaehlerControl)
        {
            myControl = zaehlerControl;
        }
        #endregion

        #region Zugriffe
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public SortedList<DateTime, double> Values
        {
            get
            {
                return values;
            }

            set
            {
                values = value;
            }
        }

        internal ZeitIntervall Intervall
        {
            get
            {
                return intervall;
            }

            set
            {
                intervall = value;
                DataCalculated = false;
            }
        }

        public double AnzTage
        {
            get
            {
                return anzTage;
            }

            set
            {
                anzTage = value;
                RawDataValid = false;
                DataCalculated = false;
            }
        }

        private CalcModeEnum calcMode;

        public CalcModeEnum CalcMode
        {
            get { return calcMode; }
            set
            {
                calcMode = value;
                DataCalculated = false;
            }
        }


        private bool dataOnIntervalBoundarys;

        public bool DataOnIntervalBoundarys
        {
            get { return dataOnIntervalBoundarys; }
            set
            {
                dataOnIntervalBoundarys = value;
                DataCalculated = false;
            }
        }


        public bool RawDataValid { get; private set; }

        public bool DataCalculated { get; private set; }

        #endregion


        private void readDataFromSql()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DateTime anfangsDatum = DateTime.Now.AddDays(-anzTage);
                String sqlStatement = sqlStatement = String.Format("select {0},{1} from {2} where {0} > '{3}' order by datum asc",  datumSpaltenName, wertSpaltenName, tabellenName, anfangsDatum);
                SqlCommand cmd = new SqlCommand(sqlStatement);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    rawData.Clear();
                    while (reader.Read())
                    {
                        try
                        {
                            DateTime istTime = reader.GetDateTime(0);
                            double istWert = (double)reader.GetDecimal(1);
                            rawData.Add(istTime, istWert);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            RawDataValid = true;
            //calculateData();
        }


        public void calculateData()
        {
            DateTime istTime = DateTime.MinValue;
            DateTime lastTime = DateTime.MinValue.AddDays(1);
            double istWert = 0D;
            double lastWert = 0D;
            int anzValues = 0;
            int anz = 0;

            // evtl müssen die Daten neu eingelesen werden
            if (!RawDataValid)
            {
                readDataFromSql();
            }

            if (rawData == null) return;
            if ((anzValues = rawData.Count) < 1) return;

            if (!DataCalculated)
            {

                values.Clear();
                if (myControl != null) myControl.progressBar1.Visible = true;

                foreach (var item in rawData)
                {
                    try
                    {
                        istTime = item.Key;
                        istWert = item.Value;

                        if (DataOnIntervalBoundarys)
                        {   // Werte nur an Intervallgrenzen
                            DateTime startTime = Interval.abrunden(lastTime, intervall) + Interval.toTimespan(intervall);
                            DateTime endTime = Interval.abrunden(istTime, intervall);
                            if (startTime < new DateTime(2010, 1, 1))
                            {
                                startTime = endTime;
                                lastWert = istWert;
                            }
                            for (DateTime time = startTime; time <= endTime; time += Interval.toTimespan(intervall))
                            {
                                // Werte ausdünnen
                                if (!sameIntervall(time, lastTime, intervall))
                                {   // erster Wert in neuem Intervall
                                    if (CalcMode == CalcModeEnum.value)
                                    {   // Wert anzeigen
                                        values.Add(time, istWert);
                                    }
                                    else if (CalcMode == CalcModeEnum.difference)
                                    {   // Differenz zu letztem angezeigten Wert anzeigen
                                        if (lastTime > new DateTime(2010, 1, 1)) values.Add(time, istWert - lastWert);
                                    }
                                    else if (CalcMode == CalcModeEnum.average)
                                    {   // Mittelwert über letztes angezeigte Intervall anzeigen
                                        if (lastTime > new DateTime(2010, 1, 1))
                                        {   // nivht der erste Wert (da sonst kein Zeitintervall bekannt)
                                            double dauer = (time - lastTime).TotalSeconds;
                                            values.Add(lastTime, (istWert - lastWert) / dauer);
                                        }
                                    }
                                    lastWert = istWert;
                                    lastTime = time;
                                }
                            }
                            //lastTime = endTime;
                            //lastWert = istWert;
                        }
                        else
                        {   // Werte ausdünnen
                            if (!sameIntervall(istTime, lastTime, intervall))
                            {   // erster Wert in neuem Intervall
                                if (CalcMode == CalcModeEnum.value)
                                {   // Wert anzeigen
                                    values.Add(istTime, istWert);
                                }
                                else if (CalcMode == CalcModeEnum.difference)
                                {   // Differenz zu letztem angezeigten Wert anzeigen
                                    if (lastTime > new DateTime(2010, 1, 1)) values.Add(istTime, istWert - lastWert);
                                }
                                else if (CalcMode == CalcModeEnum.average)
                                {   // Mittelwert über letztes angezeigte Intervall anzeigen
                                    if (lastTime > new DateTime(2010, 1, 1))
                                    {   // nivht der erste Wert (da sonst kein Zeitintervall bekannt)
                                        double dauer = (istTime - lastTime).TotalSeconds;
                                        values.Add(lastTime, (istWert - lastWert) / dauer);
                                    }
                                }
                                lastWert = istWert;
                                lastTime = istTime;
                            }
                        }
                        // Fortschritt anzeigen
                        anz++;
                        int fortschritt = anz * 100 / anzValues;
                        if (myControl != null)
                        {
                            myControl.progressBar1.Value = fortschritt;
                        }
                        //Console.WriteLine(fortschritt);
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }

                }

                if (myControl != null) myControl.progressBar1.Visible = false;
            }
            DataCalculated = true;
        }

        private bool sameIntervall(DateTime istTime, DateTime lastTime, ZeitIntervall intervall)
        {
            switch (intervall)
            {
                case ZeitIntervall.Jahrzehnt:
                    if (istTime.Year / 100 == lastTime.Year / 100) return true;
                    break;
                case ZeitIntervall.Jahr:
                    if (istTime.Year == lastTime.Year) return true;
                    break;
                case ZeitIntervall.Monat:
                    if ((istTime.Year == lastTime.Year) & (istTime.Month == lastTime.Month)) return true;
                    break;
                case ZeitIntervall.Woche:
                    if ((istTime.Year == lastTime.Year) &
                        (CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(istTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday) == CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(lastTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday))) return true;
                    break;
                case ZeitIntervall.Tag:
                    if (istTime.Date == lastTime.Date) return true;
                    break;
                case ZeitIntervall.Stunde:
                    if ((istTime.Date == lastTime.Date) & (istTime.Hour == lastTime.Hour)) return true;
                    break;
                case ZeitIntervall.Minute:
                    if ((istTime.Date == lastTime.Date) & (istTime.Hour == lastTime.Hour) & (istTime.Minute == lastTime.Minute)) return true;
                    break;
                case ZeitIntervall.Sekunde:
                    if ((istTime.Date == lastTime.Date) & (istTime.Hour == lastTime.Hour) & (istTime.Minute == lastTime.Minute) & (istTime.Second == lastTime.Second)) return true;
                    break;
                case ZeitIntervall.all:
                    return false;

                case ZeitIntervall.none:
                    return true;

                default:
                    return false;

            }
            return false;
        }
    }


}