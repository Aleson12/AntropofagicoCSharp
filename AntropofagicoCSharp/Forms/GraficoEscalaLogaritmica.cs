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

        public void CalculoLogaritmico(double[] arrayX, double[] arrayY)
        {
            List<double> valoresDeXFiltrados = new List<double>();
            List<double> valoresDeXFiltradosAcimaDeZero = new List<double>();

            List<double> valoresDeYFiltrados = new List<double>();
            List<double> valoresDeYFiltradosAcimaDeZero = new List<double>();

            List<double> valoresDeXEmEnergia = new List<double>();

            /*for (int i = 0; i < arrayY.Length; i++)
            {
                // Filtrar valores de X < 100 (para que os valores abaixo de 100 do eixo X não sejam exibidos):
                if (arrayY[i] >= 100)
                    valoresDeYFiltrados.Add(arrayY[i]); // valores filtrados (maiores que 100) adicionados à lista

                valoresDeYFiltrados.ForEach(valorEixoY => {

                    var x = ((valorEixoY * 0.02) - 0.0053);

                    valoresDeYEmEnergia.Add(x);


                });

                if (arrayX[i] > 0) // valores negativos não são considerados no eixo Y
                    valoresDeXFiltrados.Add(Math.Log10((double)arrayX[i]));
                else
                    valoresDeXFiltrados.Add(double.NaN); // Substitui valores negativos ou zero                
            }*/

            for (int i = 0; i < arrayX.Length; i++)
            {
                // Filtrar valores de X < 100 (para que os valores abaixo de 100 do eixo X não sejam exibidos):
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

            valoresDeXEmEnergia.Sort();

            // Converter as listas filtradas para arrays
            eixoXEmEnergia = valoresDeXEmEnergia.ToArray();

            valoresDeYFiltrados.ForEach(valorEixoY => { if (valorEixoY > 0) { valoresDeYFiltradosAcimaDeZero.Add(valorEixoY);}}); // retirando os valores negativos

            logY = valoresDeYFiltradosAcimaDeZero.ToArray();

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
