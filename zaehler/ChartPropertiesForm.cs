using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace zaehlerNS
{
    public partial class ChartPropertiesForm : Form
    {
        private Chart chart1;


        public ChartPropertiesForm(Chart chart1)
        {
            this.chart1 = chart1;
            InitializeComponent();
            propertyGrid1.SelectedObject = chart1;
        }
    }
}
