﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace zaehlerNS
{
    public partial class ZaehlerControl : UserControl
    {
        private List<Zaehler> zaehlerList = new List<Zaehler>();

        private double displayAnzTage = 3;

        public double DisplayAnzTage
        {
            get { return displayAnzTage; }
            set
            {
                displayAnzTage = value;

                // und die Daten neu einlesen
                foreach (var zaehler in zaehlerList)
                {
                    zaehler.AnzTage = displayAnzTage;
                    //zaehler.readDataFromSql();
                }
                aktualisieren();
            }
        }

        private ZeitIntervall displayIntervall;
        public ZeitIntervall DisplayIntervall
        {
            get { return displayIntervall; }
            set
            {
                displayIntervall = value;
                foreach (var zaehler in zaehlerList)
                {
                    zaehler.Intervall = displayIntervall;
                }
                aktualisieren();
            }
        }

        //public CalcMode DisplayMode { get; set; }
        private CalcModeEnum calcMode;
        public CalcModeEnum CalcMode
        {
            get { return calcMode; }
            set
            {
                calcMode = value;
                foreach (var zaehler in zaehlerList)
                {
                    zaehler.CalcMode = calcMode;
                }
                aktualisieren();
            }
        }

        public bool ShowMarker { get; set; }

        private bool dataOnIntervalBoundarys;
        public bool DataOnIntervalBoundarys
        {
            get { return dataOnIntervalBoundarys; }
            set
            {
                dataOnIntervalBoundarys = value;
                foreach (var zaehler in zaehlerList)
                {
                    zaehler.DataOnIntervalBoundarys = dataOnIntervalBoundarys;
                }
            }
        }

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
            cBCalcMode.DataSource = Enum.GetNames(typeof(CalcModeEnum));
            progressBar1.Visible = false;


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
            aktualisieren();
        }



        public void aktualisieren()
        {
            // Marker setzen
            MarkerStyle markerStyle;
            if (ShowMarker) markerStyle = MarkerStyle.Circle;
            else markerStyle = MarkerStyle.None;
            foreach (var serie in chart1.Series)
            {
                serie.MarkerStyle = markerStyle;
            }

            cBMarker.Checked = ShowMarker;
            cBIntervallgrenzen.Checked = DataOnIntervalBoundarys;
            cBAnzTage.Text = DisplayAnzTage.ToString();
            cBCalcMode.SelectedIndex = (int)CalcMode;
            if (DataOnIntervalBoundarys)
            {
                if (DisplayIntervall == ZeitIntervall.all) DisplayIntervall = ZeitIntervall.Minute;
                if (DisplayIntervall == ZeitIntervall.Sekunde) DisplayIntervall = ZeitIntervall.Minute;

            }
            cBIntervall.SelectedIndex = (int)DisplayIntervall;

            foreach (var zaehler in zaehlerList)
            {
                zaehler.AnzTage = DisplayAnzTage;
                zaehler.Intervall = DisplayIntervall;
                zaehler.CalcMode = CalcMode;
                zaehler.DataOnIntervalBoundarys = DataOnIntervalBoundarys;
                // berechnen
                zaehler.calculateData();
                // neu binden
                Series serie = chart1.Series[zaehler.Name];
                serie.Points.DataBindXY(zaehler.Values.Keys, zaehler.Values.Values);
            }
            // skalieren
            chart1.ChartAreas[0].RecalculateAxesScale();
        }



        private void cBAnzahlTage_TextChanged(object sender, EventArgs e)
        {
            ComboBox tb = (ComboBox)sender;
            tb.Text = tb.Text.Replace('.', ',');
            double wert = 1;
            if (double.TryParse(tb.Text, out wert))
            {
                DisplayAnzTage = wert;
            }
        }


        private void cBMarker_CheckedChanged(object sender, EventArgs e)
        {
            ShowMarker = cBMarker.Checked;
            aktualisieren();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            new ChartPropertiesForm(chart1).Show();
        }


        private void cBIntervall_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZeitIntervall intervall = ZeitIntervall.none;
            intervall = (ZeitIntervall)Enum.Parse(typeof(ZeitIntervall),cBIntervall.SelectedValue.ToString());
            //if ((intervall == ZeitIntervall.all) | (intervall == ZeitIntervall.Sekunde) & (cBIntervallgrenzen.Checked)) return;    // bei intervallgrenzen keine schnellen Intervalle erlaubt

            if (intervall != ZeitIntervall.none)
            {
                DisplayIntervall = intervall;

            }
        }

        private void cBIntervallgrenzen_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            DataOnIntervalBoundarys = cb.Checked;

            //foreach (var zaehler in zaehlerList)
            //{
            //    zaehler.DataOnIntervalBoundarys = cb.Checked;
            //    if ((zaehler.Intervall == ZeitIntervall.all) | (zaehler.Intervall == ZeitIntervall.Sekunde) & (cBIntervallgrenzen.Checked))
            //    {
            //        zaehler.Intervall = ZeitIntervall.Minute;
            //        cBIntervall.SelectedItem = "Minute";
            //    }
                
            //    //zaehler.calculateData();
            //}
            aktualisieren();
        }

        private void cBCalcMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcModeEnum calcMode = CalcModeEnum.value;
            bool dataOnIntervalBoundarysAllowed = true;

            calcMode = (CalcModeEnum)Enum.Parse(typeof(CalcModeEnum), cBCalcMode.SelectedValue.ToString());
            if ((calcMode == CalcModeEnum.difference ) | (calcMode == CalcModeEnum.average))    
            {   // keine berechnung auf Intervallgrenzen
                dataOnIntervalBoundarysAllowed = false;
            }

            foreach (var zaehler in zaehlerList)
            {
                zaehler.CalcMode = calcMode;
                zaehler.Intervall = ZeitIntervall.all;
                cBIntervall.SelectedItem = "all";
                zaehler.DataOnIntervalBoundarys &= dataOnIntervalBoundarysAllowed;
                cBIntervallgrenzen.Checked = zaehler.DataOnIntervalBoundarys;

                //zaehler.calculateData();
            }
            aktualisieren();
        }

        int anzStartPaint = 0;
        private void chart1_PrePaint(object sender, ChartPaintEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            anzStartPaint++;
            Console.WriteLine("PrePaint "+ anzStartPaint);
        }

        private void chart1_PostPaint(object sender, ChartPaintEventArgs e)
        {
            anzStartPaint--;
            Console.WriteLine("PostPaint "+ anzStartPaint);
            if (anzStartPaint==0) Cursor = Cursors.Default;
        }
    }
}
