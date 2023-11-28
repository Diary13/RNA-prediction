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
    public partial class PredictUnPas : UserControl
    {
        Prediction p = new Prediction(500);
        public PredictUnPas()
        {
            InitializeComponent();
            var result = p.oneStepProcess(p.optimalArchitect, 1, p.stepOneStep, p.HenonSerial);
            DrawChart(result);
        }
        public void DrawChart(Matrix<double> result)
        {
            this.chartPredictOneStep.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            this.chartPredictOneStep.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            for (int i = 0; i < p.stepOneStep; i++)
            {
                this.chartPredictOneStep.Series[0].Points.AddY(result[i, 0]);
                this.chartPredictOneStep.Series[1].Points.AddY(result[i, 1]);
                double tmp1 = (result[i, 0] > result[i, 1]) ? result[i, 0] - result[i, 1] : result[i, 1] - result[i, 0];
                this.chart1.Series[0].Points.AddY(Math.Abs(tmp1));
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            this.chartPredictOneStep.Series[0].Points.Clear();
            this.chartPredictOneStep.Series[1].Points.Clear();
            this.chart1.Series[0].Points.Clear();
            p.stepOneStep = comboBox1.SelectedIndex + 1;
            var result = p.oneStepProcess(p.optimalArchitect, 1, p.stepOneStep, p.HenonSerial);
            DrawChart(result);
        }
    }
}
