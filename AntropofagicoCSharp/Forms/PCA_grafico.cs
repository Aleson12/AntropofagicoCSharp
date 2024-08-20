using Accord.Math;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using ScottPlot;

namespace AntropofagicoCSharp.Forms
{
    public partial class PCA_grafico : Form
    {
        ScottPlot.Plottables.Scatter MyScatter;
        ScottPlot.Plottables.Crosshair MyCrosshair;

        RadioButton rbNearestXY;

        static string caminhoDaPastaComOsCSVs = Arquivo._caminhoDaPastaDosArquivosCSVPosTratamento;
      
        string[] arquivosCSVs = Directory.GetFiles(caminhoDaPastaComOsCSVs);
       
        public PCA_grafico()
        {
            InitializeComponent();
            this.TopMost = true; // sobrepõe este formulário em detrimento de outros
        }

        #region invocaçãoDoMétodoPCA

        private void formsPlot1_Load(object sender, EventArgs e)
        {
            Arquivo.PCA();

        }
        #endregion invocaçãoDoMétodoPCA

        #region PlotagemGraficoPCA

        public void AtualizarGrafico(double[] xs, double[] ys)
        {
            if (xs.Count() == ys.Count())
                formsPlot1.Plot.Add.ScatterPoints(xs, ys);
            LocalizaPonto(xs, ys);
        }

        public void LocalizaPonto(double[] xs, double[] ys)
        {
            MyScatter = formsPlot1.Plot.Add.Scatter(xs, ys);
            MyScatter.LineWidth = 0;

            // criando um Crosshair para ser possível capturar as coordendas de cada ponto
            // sombreado pelo ponteiro e exibir o resultado (X e Y) na borda superior da interface:

            MyCrosshair = formsPlot1.Plot.Add.Crosshair(0, 0);
            MyCrosshair.IsVisible = false;
            MyCrosshair.MarkerShape = MarkerShape.OpenCircle;

            formsPlot1.MouseMove += (s, e) =>
            {
                
                Pixel mousePixel = new(e.Location.X, e.Location.Y);
                Coordinates localizacaoDoMouse = formsPlot1.Plot.GetCoordinates(mousePixel);

                DataPoint nearest = MyScatter.Data.GetNearest(localizacaoDoMouse, formsPlot1.Plot.LastRender);

                // se o dataPoint for nulo:
                if (nearest.Equals(DataPoint.None))
                    return;
                
                // remove, "limpa", cada ponto sombreado pelo ponteiro do mouse:
                formsPlot1.Plot.Remove<ScottPlot.Plottables.Callout>();

                // lista para conter apenas o nome de cada arquivo.csv sem o seu caminho de diretório:
                List<string> nomeDosArquivosCSV = new List<string>();

                // percorrendo cada arquivo.csv, obtendo apenas o seu nome e extensão:
                foreach (string arquivoCSV in arquivosCSVs)
                    nomeDosArquivosCSV.Add(Path.GetFileName(arquivoCSV));

                if (nearest.Index >= 0 && nearest.Index < nomeDosArquivosCSV.Count)
                {
                    formsPlot1.Plot.Add.Callout($"X: {nearest.X}\nY: {nearest.Y}\nOrigem: {nomeDosArquivosCSV[nearest.Index]}" ,
                    
                        textLocation: nearest.Coordinates,
                        tipLocation: nearest.Coordinates

                     );

                    // remove, "limpa", o último ponto sombreado pelo ponteiro do mouse:
                    formsPlot1.Refresh();

                    // renderiza, na parte superior da interface do gráfico, as coordenadas (X e Y) dos pontos e o seu respectivo arquivo .csv de origem:
                    if (nearest.IsReal)
                         Text = $"Coordenadas: X={nearest.X:0.##}, Y={nearest.Y:0.##}; Origem: {nomeDosArquivosCSV[nearest.Index]}";
                }
            };
        }
        #endregion PlotagemGraficoPCA
    }
}
