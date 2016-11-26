using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace zaehlerNS
{
    public partial class Form1 : Form
    {
        Zaehler z;
        double val = 6000;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();

            zaehlerControl1.Einheiten("l", "l/s");
            z = new Zaehler("Wasserzähler", "Datum","Volumen","H2OZaehler",1d,1d);
            zaehlerControl1.AddZaehler(z);
            val = z.lastValue;

            zaehlerControl2.Einheiten("kWh", "kW" );
            Zaehler z2 = new Zaehler("Wärmezähler", "Datum", "EnergieCalc", "WaermeZaehler",1000d, 1000d / 3600d);
            zaehlerControl2.AddZaehler(z2);

            //Zaehler z2 = new Zaehler("Bezugszähler", "Datum", "Bezogen", "Zaehlerstaende");
            //zaehlerControl2.AddZaehler(z2);

            //Zaehler z3 = new Zaehler("Erzeugtzaehler", "Datum", "Erzeugt", "Zaehlerstaende");
            //zaehlerControl2.AddZaehler(z3);

            timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            z.AddPoint(DateTime.Now, val);
            val += rnd.Next(1,20);
            zaehlerControl1.aktualisieren();
        }
    }
}
