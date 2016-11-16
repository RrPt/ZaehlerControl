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
        public Form1()
        {
            InitializeComponent();

//            Zaehler z = new Zaehler("Wasser","select Top 100 Datum,Volumen from H2OZaehler order by datum desc");
            Zaehler z = new Zaehler("Wasser", "Datum","Volumen","H2OZaehler",1000);
            z.Name = "Wasserzähler";
            zaehlerControl1.AddZaehler(z);

//            Zaehler z2 = new Zaehler("Strom", "select Top 100 Datum,Bezogen from Zaehlerstaende order by datum desc");
            Zaehler z2 = new Zaehler("Bezugszähler", "Datum","Bezogen","Zaehlerstaende",100);
            z2.Name = "Bezugszähler";
            zaehlerControl2.AddZaehler(z2);

            Zaehler z3 = new Zaehler("Erzeugtzaehler", "Datum", "Erzeugt", "Zaehlerstaende", 100);
            //new Zaehler("Strom", "select Top 100 Datum,Erzeugt from Zaehlerstaende order by datum desc",100);
            z3.Name = "Erzeugtzaehler";
            zaehlerControl2.AddZaehler(z3);

            //zaehlerControl1.redraw();
            //zaehlerControl2.redraw();
        }
    }
}
