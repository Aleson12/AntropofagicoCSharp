using Accord.Math;
using ScottPlot;

namespace AntropofagicoCSharp.Forms
{
    public partial class PCA_grafico : Form
    {
        ScottPlot.Plottables.Scatter MyScatter;
        ScottPlot.Plottables.Crosshair MyCrosshair;

        RadioButton rbNearestXY;
        GraficoDeCadaPonto graficoIndividual = new GraficoDeCadaPonto();

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

            formsPlot1.MouseMove += (s, e) => // ao sobrepor um ponto no gráfico, faça:
            {
                Pixel mousePixel = new(e.Location.X, e.Location.Y);
                Coordinates localizacaoDoMouse = formsPlot1.Plot.GetCoordinates(mousePixel);
                DataPoint nearest = MyScatter.Data.GetNearest(localizacaoDoMouse, formsPlot1.Plot.LastRender);

                // se o dataPoint for nulo:
                if (nearest.Equals(DataPoint.None))
                {
                    Cursor = Cursors.Default; // cursor do mouse ficará no estado de apresentação padrão
                    return;
                }
                else
                {
                    // remove, "limpa", cada ponto sombreado pelo ponteiro do mouse:
                    formsPlot1.Plot.Remove<ScottPlot.Plottables.Callout>();

                    // correlacionando cada ponto a cada arquivo .csv:

                    formsPlot1.Plot.Add.Callout($"{Arquivo.listaMatrizRelCSV[nearest.Index].NomeArqCSV}",

                         textLocation: nearest.Coordinates,
                         tipLocation: nearest.Coordinates

                     );

                    Cursor = Cursors.Hand; // ponteiro do mouse definido como "hand" ao sobrepor um ponto no gráfico

                    // remove, "limpa", o último ponto sombreado pelo ponteiro do mouse:
                    formsPlot1.Refresh();

                    // renderiza, na parte superior da interface do gráfico, as coordenadas (X e Y) dos pontos e o seu respectivo arquivo .csv de origem:
                    if (nearest.IsReal)

                        formsPlot1.MouseDown += (s, e) => // ao clicar num dos pontos,
                        {
                            graficoIndividual.Text = Arquivo.listaMatrizRelCSV[nearest.Index].NomeArqCSV; // apresentar o gráfico tendo como título o nome do arquivo .csv 

                            GraficoDeCadaPonto.PlotagemIndividual(Arquivo.listaMatrizRelCSV[0].ValoresInternosCSV);

                            graficoIndividual.Show(); // renderiza o gráfico na tela
                        };

                    Text = $"Coordenadas: Y={nearest.X:0.##}, X={nearest.Y:0.##}; Origem:{Arquivo.listaMatrizRelCSV[nearest.Index].NomeArqCSV}"; // texto que será exibido na borda superior do gráfico
                }
            };
        }
        #endregion PlotagemGraficoPCA
    }
}
