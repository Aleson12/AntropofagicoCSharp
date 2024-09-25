using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Math;
using AntropofagicoCSharp;
using LiveCharts.Wpf;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.VisualBasic.Logging;
using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.WinForms;

namespace AntropofagicoCSharp.Forms
{
    public partial class GraficoEscalaLogaritmica : Form
    {
        static double[]? logY;
        static double[]? logX;

        public GraficoEscalaLogaritmica()
        {
            InitializeComponent();
        }

        public void CalculoLogaritmico(double[] arrayX, double[] arrayY)
        {
            List<double> valoresDeXFiltrados = new List<double>();
            List<double> valoresDeYFiltrados = new List<double>();

            for (int i = 0; i < arrayY.Length; i++)
            {
                // Filtrar valores de X < 100 (para que os valores abaixo de 100 do eixo X não sejam exibidos):
                if (arrayX[i] >= 100)
                {
                    valoresDeXFiltrados.Add(arrayX[i]); // valores filtrados (maiores que 100) adicionados à lista

                    if (arrayY[i] > 0) // valores negativos não são considerados no eixo Y
                        valoresDeYFiltrados.Add(Math.Log10((double)arrayY[i]));
                    else
                        valoresDeYFiltrados.Add(double.NaN); // Substitui valores negativos ou zero
                }
            }

            // Converter as listas filtradas para arrays
            logX = valoresDeXFiltrados.ToArray();
            logY = valoresDeYFiltrados.ToArray();

            // Limpar o gráfico e plotar os novos valores
            formsPlot3.Plot.Clear();

            var myScatter = formsPlot3.Plot.Add.Scatter(logX, logY); // plotagem dos valores no gráfico
            myScatter.MarkerSize = 0; // sem marcadores na linha

            // Atualizar o gráfico
            formsPlot3.Refresh();
            
            Teste(logX, logY);
        }

        public void Teste(double[] logX, double[] logY)
        {
            ScottPlot.Plottables.Scatter MyScatter;

            ScottPlot.Color color = new ScottPlot.Color();
            
            MyScatter = formsPlot3.Plot.Add.Scatter(logX, logY);


            MyScatter.MarkerSize = 0;
            //MyScatter.Color = color.;

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
