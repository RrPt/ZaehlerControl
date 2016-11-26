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

            z = new Zaehler("Wasser", "Datum","Volumen","H2OZaehler");
            z.Name = "Wasserzähler";
            zaehlerControl1.AddZaehler(z);
            zaehlerControl1.aktualisieren();
            val = z.lastValue;


            Zaehler z2 = new Zaehler("Bezugszähler", "Datum", "Bezogen", "Zaehlerstaende");
            z2.Name = "Bezugszähler";
            zaehlerControl2.AddZaehler(z2);

            Zaehler z3 = new Zaehler("Erzeugtzaehler", "Datum", "Erzeugt", "Zaehlerstaende");
            z3.Name = "Erzeugtzaehler";
            zaehlerControl2.AddZaehler(z3);

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
