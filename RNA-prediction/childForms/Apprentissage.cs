using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RNA_prediction.Assets;

namespace RNA_prediction.childForms
{
    public partial class Apprentissage : UserControl
    {
        public Apprentissage()
        {
            InitializeComponent();
            this.apprentissageChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            var p = new Prediction(500);
            //p.GenerateHenonSerial();
            //var result = p.OptimalNetArchitect();
            //double[] result = { 40, 5, 63, 1, 2, 89, 2, 3, 87, 2, 3, 11, 3 };
            //for (int i = 0; i < 6; i++)
            //{
            //    this.apprentissageChart.Series[0].Points.AddXY(i, result[i]);
            //}
        }
    }
}
