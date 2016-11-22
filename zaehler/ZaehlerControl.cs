using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace zaehlerNS
{
    public partial class ZaehlerControl : UserControl
    {
        private List<Zaehler> zaehlerList = new List<Zaehler>();


        public ZaehlerControl()
        {
            InitializeComponent();
            zaehlerList = new List<Zaehler>();
            chart1.ChartAreas[0].CursorX.Interval = 0.00001;
            chart1.ChartAreas[0].CursorY.Interval = 0.001;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            chart1.Legends[0].Docking = Docking.Top;
            chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;
            chart1.Legends[0].BackColor = System.Drawing.Color.Transparent;

            cBIntervall.DataSource = Enum.GetNames(typeof(ZeitIntervall));
            cBCalcMode.DataSource = Enum.GetNames(typeof(CalcMode));

        }

        public void AddZaehler(Zaehler z)
        {
            zaehlerList.Add(z);
            z.MyControl(this);
            Series serie = chart1.Series.Add(z.Name);
            serie.Points.DataBindXY(z.Values.Keys, z.Values.Values);
            serie.ChartType = SeriesChartType.Line;
            serie.XValueType = ChartValueType.DateTime;
            serie.ToolTip = z.Name + "\n" +
                "Zeit:    #VALX{d.M.y HH:mm:ss}\n" +
                "Wert:    #VAL{N2}" + "\n" +
                "Minimum:    " + "#MIN{N2}" + "\n" +
                "Maximum:    " + "#MAX{N2}" + "\n" +
                "Mittelwert: " + "#AVG{N2}" + "\n" +
                "Erster:     " + "#FIRST{N2}" + "\n" +
                "Letzter:    " + "#LAST{N2}";
            cBMarker_CheckedChanged(cBMarker, null);
        }



        public void aktualisieren()
        {
            foreach (var zaehler in zaehlerList)
            {
                Series serie = chart1.Series[zaehler.Name];
                serie.Points.DataBindXY(zaehler.Values.Keys, zaehler.Values.Values);
            }
        }



        private void cBAnzahlTage_TextChanged(object sender, EventArgs e)
        {
            ComboBox tb = (ComboBox)sender;
            int wert = 7;
            if (int.TryParse(tb.Text, out wert))
            {
                foreach (var zaehler in zaehlerList)
                {
                    zaehler.AnzTage = wert;
                    zaehler.readData();
                }
                aktualisieren();
            }
        }


        private void cBMarker_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            MarkerStyle markerStyle;
            if (cb.Checked) markerStyle = MarkerStyle.Circle;
            else markerStyle = MarkerStyle.None;

            foreach (var serie in chart1.Series)
            {
                serie.MarkerStyle = markerStyle;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            new ChartPropertiesForm(chart1).Show();
        }


        private void cBIntervall_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZeitIntervall intervall = ZeitIntervall.none;
            intervall = (ZeitIntervall)Enum.Parse(typeof(ZeitIntervall),cBIntervall.SelectedValue.ToString());
            if ((intervall == ZeitIntervall.all) | (intervall == ZeitIntervall.Sekunde) & (cBIntervallgrenzen.Checked)) return;    // bei intervallgrenzen keine schnellen Intervalle erlaubt
            //if ((intervall == ZeitIntervall.all) & (cBIntervallgrenzen.Checked)) return;    // bei intervallgrenzen keine schnellen Intervalle erlaubt

            if (intervall != ZeitIntervall.none)
            {
                foreach (var zaehler in zaehlerList)
                {
                    zaehler.Intervall = intervall;
                    zaehler.readData();
                }
                aktualisieren();
            }
        }

        private void cBIntervallgrenzen_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            foreach (var zaehler in zaehlerList)
            {
                zaehler.dataOnIntervalBoundarys = cb.Checked;
                if ((zaehler.Intervall == ZeitIntervall.all) | (zaehler.Intervall == ZeitIntervall.Sekunde) & (cBIntervallgrenzen.Checked))
                {
                    zaehler.Intervall = ZeitIntervall.Minute;
                    cBIntervall.SelectedItem = "Minute";
                }
                
                zaehler.readData();
            }
            aktualisieren();
        }

        private void cBCalcMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcMode calcMode = CalcMode.value;
            bool dataOnIntervalBoundarysAllowed = true;

            calcMode = (CalcMode)Enum.Parse(typeof(CalcMode), cBCalcMode.SelectedValue.ToString());
            if ((calcMode == CalcMode.difference ) | (calcMode == CalcMode.average))    
            {   // keine berechnung auf Intervallgrenzen
                dataOnIntervalBoundarysAllowed = false;
            }

            foreach (var zaehler in zaehlerList)
            {
                zaehler.CalcMode = calcMode;
                zaehler.Intervall = ZeitIntervall.all;
                cBIntervall.SelectedItem = "all";
                zaehler.dataOnIntervalBoundarys &= dataOnIntervalBoundarysAllowed;
                cBIntervallgrenzen.Checked = zaehler.dataOnIntervalBoundarys;

                zaehler.readData();
            }
            aktualisieren();
        }
    }
}
