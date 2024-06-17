using System.Windows.Forms.DataVisualization.Charting;

namespace AntropofagicoCSharp.Forms
{
    public partial class GraficoPCA : Form
    {
        public GraficoPCA(List<double>elementosDaPrimeiraColuna, List<double>elementosDaSegundaColuna)
        {
            InitializeComponent();
            this.TopMost = true;  // sobrepõe este formulário em relação ao outro
            PlotPCAresultados(elementosDaPrimeiraColuna, elementosDaSegundaColuna);
        }

        private void PlotPCAresultados(List<double> elementosDaPrimeiraColuna, List<double> elementosDaSegundaColuna)
        {


            Chart chart = this.chart1; // acessando a interface do gráfico

            Series scatterSeries = chart.Series.Add("Dados dispersos"); // adiciona uma série de dados à interface do gráfico
            // e também um identificador único a esse conjunto de dados ("Dados dispersos"), e o insere na variável 
            // "scaterSeries"

            scatterSeries.ChartType = SeriesChartType.Point; // define o tipo de gráfico (de pontos, no caso)

            elementosDaSegundaColuna.ForEach(valorSgndaColuna =>
                scatterSeries.Points.AddY(valorSgndaColuna));

            scatterSeries.MarkerStyle = MarkerStyle.Circle; // define o tipo de ponto (Círculo, no caso)
            scatterSeries.MarkerSize = 8; // definindo o tamanho de cada ponto

        }
    }
}
