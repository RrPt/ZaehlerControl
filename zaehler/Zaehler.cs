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
        private SortedList<DateTime,Double> values;
        private string datumSpaltenName;
        private string wertSpaltenName;
        private string tabellenName;
        private int maxRows;
        private ZeitIntervall intervall = ZeitIntervall.all;
        private int anzTage = 1;

        #region Konstruktoren


        public Zaehler(string name, string datumSpaltenName, string wertSpaltenName, string tabellenName, int maxRows = 0)
        {
            //            Zaehler z = new Zaehler("Wasser","select Top 100 Datum,Volumen from H2OZaehler order by datum desc");
            //Zaehler z = new Zaehler("Wasser", "Datum", "Volumen", "H2OZaehler");
            this.name = name;
            this.datumSpaltenName = datumSpaltenName;
            this.wertSpaltenName = wertSpaltenName;
            this.tabellenName = tabellenName;
            if (maxRows == 0) this.maxRows = int.MaxValue;
            else this.maxRows = maxRows;
            values = new SortedList<DateTime, double>();
            readData();
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
            }
        }

        public int AnzTage
        {
            get
            {
                return anzTage;
            }

            set
            {
                anzTage = value;
            }
        }

        public bool DiffMode { get; set; }
        public bool dataOnIntervalBoundarys { get; set; }




        #endregion




        public void readData()
        {
            DateTime lastTime = DateTime.MinValue.AddDays(1);
            DateTime istTime = DateTime.MinValue;
            double istWert = 0D;
            double lastWert = 0D;

            values.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DateTime anfangsDatum = DateTime.Now.AddDays(-anzTage);
                String sqlStatement = sqlStatement = String.Format("select {1},{2} from {3} where {1} > '{4}' order by datum asc", maxRows, datumSpaltenName, wertSpaltenName, tabellenName,anfangsDatum);
                SqlCommand cmd = new SqlCommand(sqlStatement);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            istTime = reader.GetDateTime(0);
                            istWert = (double)reader.GetDecimal(1);

                            if (dataOnIntervalBoundarys)
                            {
                                DateTime startTime = Interval.abrunden(lastTime, intervall) + Interval.toTimespan(intervall);
                                DateTime endTime = Interval.abrunden(istTime, intervall);
                                if (startTime < new DateTime(2010, 1, 1))
                                {
                                    startTime = endTime;
                                    lastWert = istWert;
                                }
                                for (DateTime time = startTime;time<=endTime;time+= Interval.toTimespan(intervall))
                                {
                                    values.Add(time, lastWert);
                                }
                                lastTime = endTime;
                                lastWert = istWert;
                            }
                            else
                            {
                                if (!sameIntervall(istTime, lastTime, intervall))
                                {
                                    values.Add(istTime, istWert);
                                    lastTime = istTime;
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                            //throw;
                        }
                    }
                }
            }
        }

        private bool sameIntervall(DateTime istTime, DateTime lastTime, ZeitIntervall intervall)
        {
            switch (intervall)
            {
                case ZeitIntervall.Jahrzehnt:
                    if (istTime.Year/100 == lastTime.Year/100) return true;
                    break;
                case ZeitIntervall.Jahr:
                    if (istTime.Year == lastTime.Year) return true;
                    break;
                case ZeitIntervall.Monat:
                    if ((istTime.Year == lastTime.Year)&(istTime.Month == lastTime.Month)) return true;
                    break;
                case ZeitIntervall.Woche:
                    if ((istTime.Year == lastTime.Year) & 
                        (CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(istTime, CalendarWeekRule.FirstDay,DayOfWeek.Monday) == CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(lastTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday))) return true;
                    break;
                case ZeitIntervall.Tag:
                    if (istTime.Date == lastTime.Date) return true;
                    break;
                case ZeitIntervall.Stunde:
                    if ((istTime.Date == lastTime.Date)&(istTime.Hour == lastTime.Hour)) return true;
                    break;
                case ZeitIntervall.Minute:
                    if ((istTime.Date == lastTime.Date) & (istTime.Hour == lastTime.Hour)&(istTime.Minute == lastTime.Minute)) return true;
                    break;
                case ZeitIntervall.Sekunde:
                    if ((istTime.Date == lastTime.Date) & (istTime.Hour == lastTime.Hour) & (istTime.Minute == lastTime.Minute)&(istTime.Second == lastTime.Second)) return true;
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