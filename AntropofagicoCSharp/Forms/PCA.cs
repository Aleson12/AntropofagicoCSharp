using System.Windows.Forms.DataVisualization.Charting;
using System;
using System.Drawing;

namespace AntropofagicoCSharp.Forms
{
    public partial class GraficoPCA : Form
    {
        public GraficoPCA(List<double>elementosDaPrimeiraColuna, List<double> elementosDaSegundaColuna)
        {
            InitializeComponent();
            this.TopMost = true;  // sobrepõe este formulário em relação ao outro
            PlotPCAresultados(elementosDaPrimeiraColuna, elementosDaSegundaColuna);
        }

        private void PlotPCAresultados(List<double> elementosDaPrimeiraColuna, List<double> elementosDaSegundaColuna)
        {

            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;
          //  chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";
         //   chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0}";
            chart.Series.Add("Scatter Series");

            this.Controls.Add(chart);

            Random rand = new Random();
            int count = 100; // Quantidade de pontos a serem plotados

            for (int i = 0; i < count; i++)
            {
                double xValue = rand.NextDouble() * 10 - 5; // Gera valores aleatórios para X
                double yValue = rand.NextDouble() * 10 - 5; // Gera valores aleatórios para Y
                chart.Series["Scatter Series"].Points.AddXY(xValue, yValue);
            }

        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {
            Arquivo.PCA();
        }
    }
}
