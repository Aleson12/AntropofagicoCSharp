using Accord.Math;
using ScottPlot;

namespace AntropofagicoCSharp.Forms
{
    public partial class PCA_grafico : Form
    {
        ScottPlot.Plottables.Scatter MyScatter;
        ScottPlot.Plottables.Crosshair MyCrosshair;

        RadioButton rbNearestXY;

        static string caminhoDaPastaComOsCSVs = Arquivo._caminhoDaPastaDosArquivosCSVPosTratamento;
        static string[] arquivosCSVs = Directory.GetFiles(caminhoDaPastaComOsCSVs);

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
       
        public void AtualizarGrafico(double[] x, double[] y)
        {
            if (x.Count() == y.Count())
              formsPlot1.Plot.Add.ScatterPoints(x, y);            
            LocalizaPonto(x, y);
        }

        public void LocalizaPonto(double[] x, double[] y)
        {
            MyScatter = formsPlot1.Plot.Add.Scatter(x, y);
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

                MatrizRelCSV resultado = Arquivo.listaMatrizRelCSV.Where(matrizRel =>
                {

                    matrizRel.ValorX = nearest.X; 
                    matrizRel.ValorY = nearest.Y;
                    return matrizRel.ValorY == nearest.Y && matrizRel.ValorX == nearest.X ;

                }).FirstOrDefault();
                
                // se o dataPoint for nulo:
                if (nearest.Equals(DataPoint.None))
                    return;
                
                // remove, "limpa", cada ponto sombreado pelo ponteiro do mouse:
                formsPlot1.Plot.Remove<ScottPlot.Plottables.Callout>();

                List<string> nomesDosArquivosCSV = new List<string>();

                foreach (string arquivoCSV in arquivosCSVs)
                    nomesDosArquivosCSV.Add(Path.GetFileName(arquivoCSV));

               if (nearest.Index >= 0 && nearest.Index < nomesDosArquivosCSV.Count)
               {

                    formsPlot1.Plot.Add.Callout($"teste",

                        textLocation: nearest.Coordinates,
                        tipLocation: nearest.Coordinates

                    );

                    Cursor = Cursors.Hand; // ponteiro do mouse definido como "hand" ao sobrepor um ponto no gráfico

                    // remove, "limpa", o último ponto sombreado pelo ponteiro do mouse:
                    formsPlot1.Refresh();

                    // renderiza, na parte superior da interface do gráfico, as coordenadas (X e Y) dos pontos e o seu respectivo arquivo .csv de origem:
                    if (nearest.IsReal)
                        Text = $"Coordenadas: Y={nearest.X:0.##}, X={nearest.Y:0.##}; Origem:";

               }
            };
        }
        #endregion PlotagemGraficoPCA
    }
}
