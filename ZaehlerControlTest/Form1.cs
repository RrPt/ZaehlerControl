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

            Zaehler z = new Zaehler("Wasser", "Datum","Volumen","H2OZaehler");
            z.Name = "Wasserzähler";
            zaehlerControl1.AddZaehler(z);
            zaehlerControl1.aktualisieren();
            
            

            Zaehler z2 = new Zaehler("Bezugszähler", "Datum","Bezogen","Zaehlerstaende");
            z2.Name = "Bezugszähler";
            zaehlerControl2.AddZaehler(z2);

            Zaehler z3 = new Zaehler("Erzeugtzaehler", "Datum", "Erzeugt", "Zaehlerstaende");
            z3.Name = "Erzeugtzaehler";
            zaehlerControl2.AddZaehler(z3);

        }
    }
}
