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
    public partial class Henon : UserControl
    {
        public Henon()
        {
             InitializeComponent();
            this.HenonChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            
            var p = new Prediction(500);
            var result = p.HenonSerialXY;

            for (int i = 0; i < 500; i++)
                this.HenonChart.Series[0].Points.AddXY(result[i, 0], result[i, 1]);
        }
    }
}