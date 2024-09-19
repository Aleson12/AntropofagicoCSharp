using Accord.Math;
using ScottPlot;

namespace AntropofagicoCSharp.Forms
{
    public partial class PCA_grafico : Form
    {
        public PCA_grafico()
        {
            InitializeComponent();
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
                formsPlot1.Plot.Axes.AutoScale(); // ajustando automaticamente a escala dos pontos de dispersão no gráfico
            LocalizaPonto(x, y);
        }

        public void LocalizaPonto(double[] x, double[] y)
        {

            ScottPlot.Plottables.Crosshair MyCrosshair;
            ScottPlot.Plottables.Scatter MyScatter;

            GraficoDeCadaPonto graficoIndividual = new GraficoDeCadaPonto();

            MyScatter = formsPlot1.Plot.Add.Scatter(x, y);
            MyScatter.LineWidth = 0;

            // criando um Crosshair para ser possível capturar as coordendas de cada ponto
            // sombreado pelo ponteiro e exibir o resultado (X e Y) na borda superior da interface:

            MyCrosshair = formsPlot1.Plot.Add.Crosshair(0, 0);
            MyCrosshair.IsVisible = false;
            MyCrosshair.MarkerShape = MarkerShape.OpenCircle;
            List<double> valoresContidosNoArquivoCsvLido = new List<double>();

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
                    // remove, "limpa", cada ponto sombreado pelo ponteiro do mouse (para que a caixa de texto que indica o arquivo .csv correspondente ao ponto suma quando o ponto deixar de estar sobreposto com o cursor do mouse):
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
                        valoresContidosNoArquivoCsvLido = Arquivo.listaMatrizRelCSV[nearest.Index].ValoresInternosCSV;
                        Text = $"Coordenadas: Y={nearest.X:0.##}, X={nearest.Y:0.##}; Origem:{Arquivo.listaMatrizRelCSV[nearest.Index].NomeArqCSV}"; // texto que será exibido na borda superior do gráfico
                }
            };

            formsPlot1.MouseDown += (s, e) => // ao clicar em um ponto no gráfico, faça:
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
                    Cursor = Cursors.Hand; // ponteiro do mouse definido como "hand" ao sobrepor um ponto no gráfico

                    // se o objeto que representa o gráfico for nulo ou descartado, será instanciado um novo (para que a aplicação não quebre quando o usuário fechar o gráfico menor e tentar abri-lo de novo, clicando em outro ponto).
                   if (graficoIndividual == null || graficoIndividual.IsDisposed)
                        graficoIndividual = new GraficoDeCadaPonto();
                    
                    try
                    {
                        // renderiza, na parte superior da interface do gráfico, as coordenadas (X e Y) dos pontos e o seu respectivo arquivo .csv de origem:
                        if (nearest.IsReal)

                            graficoIndividual.Text = Arquivo.listaMatrizRelCSV[nearest.Index].NomeArqCSV; // apresentar o gráfico tendo como título o nome do arquivo .csv 
                            graficoIndividual.Show();
                            graficoIndividual.PlotagemIndividual(valoresContidosNoArquivoCsvLido);
                            graficoIndividual.Refresh();
                    }
                    catch (Exception ex)
                    {
                        // "Task.Run()" usado para que, ao fechar o message.box, o cursor do mouse não fique agarrado no ponto clicado
                        Task.Run(() => MessageBox.Show("Não foi possível renderizar o gráfico individual do ponto", "Configuração", MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
                    }
                }
            };
        }

        public void AtualizaLabel(string c1, string c2)
        {
            // mostrando o percentual de variância de cada componente principal:
            formsPlot1.Plot.XLabel("PC 2 " + "(" + c1 + ")"); // eixo x;
            formsPlot1.Plot.YLabel("PC 1 " + "(" + c2 + ")"); // eixo y
        }

        #endregion PlotagemGraficoPCA
    }
}
