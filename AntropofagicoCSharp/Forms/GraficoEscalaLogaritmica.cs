using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AntropofagicoCSharp;
using ScottPlot.WinForms;

namespace AntropofagicoCSharp.Forms
{
    public partial class GraficoEscalaLogaritmica : Form
    {
        static double[]? logX;
        static double[]? logY;

        public GraficoEscalaLogaritmica()
        {
            InitializeComponent();
        }

        public void CalculoLogaritmico(double[] arrayX, double[] arrayY)
        {
            logX = new double[arrayX.Length];
            logY = new double[arrayY.Length];

            for (int i = 0; i < arrayX.Length; i++)
            {
                // se o valor for maior do que zero, faça o cálculo de logarítmo:
                if (arrayX[i] > 0 && arrayY[i] > 0) // Garantir que os valores sejam positivos
                {
                    logX[i] = Math.Log10(arrayX[i]);
                    logY[i] = Math.Log10(arrayY[i]);
                }
                else // se o valor for negativo, não mostre no gráfico:
                {
                    logX[i] = double.NaN; // ou você pode escolher remover esse ponto do gráfico
                    logY[i] = double.NaN;
                }
            }
        }

        public void formsPlot3_Load(object sender, EventArgs e)
        {
            if (logX != null || logY != null)  // se não for nulo, faça:
                formsPlot3.Plot.Clear(); // limpa o gráfico

            var myScatter = formsPlot3.Plot.Add.Scatter(logX, logY); // plota no gráfico os dados
            myScatter.MarkerSize = 0; // sem marcadores
            formsPlot3.Refresh();

        }
    }
} 