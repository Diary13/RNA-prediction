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
using MathNet.Numerics.LinearAlgebra;

namespace RNA_prediction.childForms
{
    public partial class PredictPlusieursPas : UserControl
    {
        Prediction p = new Prediction(500);
        public PredictPlusieursPas()
        {
            InitializeComponent();
            var result = p.manyStepPredictionProcess(p.optimalArchitect, 1, p.stepManyStep, p.HenonSerial);
            DrawChart(result);
        }

        public void DrawChart(Matrix<double> result)
        {
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            this.chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            for (int i = 0; i < p.stepManyStep; i++)
            {
                this.chart1.Series[0].Points.AddY(result[i, 0]);
                this.chart1.Series[1].Points.AddY(result[i, 1]);
                double tmp1 = (result[i, 0] > result[i, 1]) ? result[i, 0] - result[i, 1] : result[i, 1] - result[i, 0];
                this.chart2.Series[0].Points.AddY(Math.Abs(tmp1));
            }
        }
        private void Button1_Click_1(object sender, EventArgs e)
        {
            this.chart1.Series[0].Points.Clear();
            this.chart1.Series[1].Points.Clear();
            this.chart2.Series[0].Points.Clear();
            p.stepManyStep = comboBox1.SelectedIndex+1;
            var result = p.manyStepPredictionProcess(p.optimalArchitect, 1, p.stepManyStep, p.HenonSerial);
            DrawChart(result);
        }
    }
}
