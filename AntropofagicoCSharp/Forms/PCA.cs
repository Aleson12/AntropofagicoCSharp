using System.Windows.Forms.DataVisualization.Charting;

namespace AntropofagicoCSharp.Forms
{
    public partial class GraficoPCA : Form
    {
        public GraficoPCA(double[][] arrayDeArrays)
        {
            InitializeComponent();
            this.TopMost = true;  // sobrepõe este formulário em relação ao outro
            PlotPCAresultados(arrayDeArrays);
        }

        private void PlotPCAresultados(double[][] resultados)
        {
            Chart chart = new Chart();
            chart.Series.Clear();

            // Certificar que ChartArea existe antes de usá-lo
            if (chart.ChartAreas.Count == 0)
                chart.ChartAreas.Add(new ChartArea());
            
            Series series = new Series
            {
                Name = "PCA",
                Color = System.Drawing.Color.Blue,
                ChartType = SeriesChartType.Point
            };

            chart.Series.Add(series);

            for (int i = 0; i < resultados.Length; i++)
                series.Points.AddXY(resultados[i][0], resultados[i][1]);

            chart.ChartAreas[0].AxisX.Title = "Componente Principal 1";
            chart.ChartAreas[0].AxisY.Title = "Componente Principal 2";

        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {
            Arquivo.PCA();
        }
    }
}
