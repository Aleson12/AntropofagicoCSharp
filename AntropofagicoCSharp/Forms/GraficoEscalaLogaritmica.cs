using Accord.Math;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.TickGenerators;
using System.Threading;
using System.Threading.Tasks;

namespace AntropofagicoCSharp.Forms
{
    public partial class GraficoEscalaLogaritmica : Form
    {
        static double[]? logY;
        static double[]? eixoXEmEnergia;

        public GraficoEscalaLogaritmica()
        {
            InitializeComponent();
        }

        public static void TratamentoDeDadosTxts(string[] medias)
        {
            List<string> valoresEixoX = new List<string>();
            List<string> valoresEixoY = new List<string>();

            List<double> valoresEixoX_Doubles = new List<double>();
            List<int> valoresEixoY_Inteiros = new List<int>();

            foreach (string valor in medias)
            {
                // Divide a string usando o ponto-e-vírgula como delimitador
                string[] partes = valor.Split(';');

                // Adiciona o valor à esquerda na lista "valoresEixoX" e o valor à direita na lista "valoresEixoY"
                valoresEixoX.Add(partes[0].Trim());
                valoresEixoY.Add(partes[1].Trim());

                valoresEixoX_Doubles = valoresEixoX.ConvertAll(double.Parse);
                valoresEixoY_Inteiros = valoresEixoY.ConvertAll(int.Parse);

            }
            // Console.WriteLine(valoresEixoX_Doubles);
            // Console.WriteLine(valoresEixoY_Inteiros);
        }

        public void CalculoLogaritmico(double[] arrayX, double[] arrayY)
        {
            List<double> valoresDeXFiltrados = new List<double>();
            List<double> valoresDeYFiltrados = new List<double>();
            List<double> valoresDeXEmEnergia = new List<double>();

            for (int i = 0; i < arrayX.Length; i++)
            {
                // Filtrar valores de X < 100 (para que os valores abaixo de 100 do eixo X não sejam exibidos):
                if (arrayX[i] >= 100)
                {
                    valoresDeXFiltrados.Add(arrayX[i]); // valores filtrados (maiores que 100) adicionados à lista

                    valoresDeXFiltrados.ForEach(valorEixoX => {

                        var x = ((valorEixoX * 0.02) - 0.0053);

                        valoresDeXEmEnergia.Add(x);


                    });
                    if (arrayY[i] > 0) // valores negativos não são considerados no eixo Y
                        valoresDeYFiltrados.Add(Math.Log10((double)arrayY[i]));
                    else
                        valoresDeYFiltrados.Add(double.NaN); // Substitui valores negativos ou zero
                }
            }

            valoresDeXEmEnergia.Sort();

            // Converter as listas filtradas para arrays
            eixoXEmEnergia = valoresDeXEmEnergia.ToArray();
            logY = valoresDeYFiltrados.ToArray();

            // Limpar o gráfico e plotar os novos valores
            formsPlot3.Plot.Clear();

            var myScatter = formsPlot3.Plot.Add.Scatter(eixoXEmEnergia, logY); // plotagem dos valores no gráfico
            myScatter.MarkerSize = 0; // sem marcadores na linha

            // Atualizar o gráfico
            formsPlot3.Refresh();

            PlotagemGraficoLogaritmico(eixoXEmEnergia, logY);
        }

        public void PlotagemGraficoLogaritmico(double[] eixoXEmEnergia, double[] logY)
        {
            ScottPlot.Plottables.Scatter MyScatter;
            MyScatter = formsPlot3.Plot.Add.Scatter(eixoXEmEnergia, logY);

            MyScatter.MarkerSize = 0;

            formsPlot3.MouseMove += (s, e) =>
            {
                Pixel mousePixel = new(e.Location.X, e.Location.Y);
                Coordinates localizacaoDoMouse = formsPlot3.Plot.GetCoordinates(mousePixel);
                DataPoint pontoMaisProximo = MyScatter.Data.GetNearest(localizacaoDoMouse, formsPlot3.Plot.LastRender);

                if (pontoMaisProximo.Equals(DataPoint.None))
                {
                    Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    Cursor = Cursors.Hand;

                    formsPlot3.Plot.Remove<ScottPlot.Plottables.Callout>();

                    formsPlot3.Plot.Add.Callout($" X:{pontoMaisProximo.X}\n Y:{pontoMaisProximo.Y}",

                        textLocation: pontoMaisProximo.Coordinates,
                        tipLocation: pontoMaisProximo.Coordinates

                    );

                    formsPlot3.Refresh(); // esta instrução faz com que as caixas de texto (que informam as coordenadas da região da linha sombreada)
                                          // sejam renderizadas dinamicamente, exatamente no momento
                                          // em que o cursor do mouse sombreia
                                          // a linha do gráfico.
                }
            };
        }
    }
}
