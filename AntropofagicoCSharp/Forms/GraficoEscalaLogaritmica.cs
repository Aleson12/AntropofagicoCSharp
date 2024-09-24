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
using Microsoft.VisualBasic.Logging;
using ScottPlot;
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
            logX = new double[arrayX.Length];
            logY = new double[arrayY.Length];

            List<double> valoresDeXFiltrados = new List<double>();
            List<double> valoresDeYFiltrados = new List<double>();

            for (int i = 0; i < arrayY.Length; i++)
            {
                // Filtrar valores de X < 100 (para que os valores abaixo de 100 do eixo X não sejam exibidos):
                if (arrayX[i] >= 100)
                    valoresDeXFiltrados.Add(arrayX[i]);

                    if (arrayY[i] > 0)
                        valoresDeYFiltrados.Add(Math.Log10((double)arrayY[i]));
                    else
                        valoresDeYFiltrados.Add(double.NaN); // Substitui valores negativos ou zero
            }

            logX = valoresDeXFiltrados.ToArray();
            logY = valoresDeYFiltrados.ToArray();

            formsPlot3.Plot.Clear();

            var myScatter = formsPlot3.Plot.Add.Scatter(logX,logY);
            myScatter.MarkerSize = 0;

            formsPlot3.Refresh();
        }
    }
}
