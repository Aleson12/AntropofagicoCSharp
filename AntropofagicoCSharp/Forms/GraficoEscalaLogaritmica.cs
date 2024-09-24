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

            for (int i = 0; i < arrayY.Length; i++)
            {
                if (arrayY[i] > 0)
                    logY[i] = Math.Log10((double)arrayY[i]);
                else
                    logX[i] = double.NaN;
            }
            
            formsPlot3.Plot.Clear();

            var myScatter = formsPlot3.Plot.Add.Scatter(arrayX,logY);
            myScatter.MarkerSize = 0;

          //  formsPlot3.Plot.Axes.AutoScale(true);
            formsPlot3.Refresh();

        }
    }
}
