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
        double[] logX;
        double[] logY;

        public GraficoEscalaLogaritmica()
        {
            InitializeComponent();
        }

        public static void CalculoLogaritmico(double[] arrayX, double[] arrayY)
        {

            GraficoEscalaLogaritmica graficoLog = new GraficoEscalaLogaritmica();

            graficoLog.logX = new double[arrayX.Length];
            graficoLog.logY = new double[arrayY.Length];

            for (int i = 0; i < graficoLog.logX.Length; i++)
            {
                graficoLog.logX[i] = Math.Log10((double)arrayX[i]);
                graficoLog.logY[i] = Math.Log10((double)arrayY[i]);

            }
        }

        public void formsPlot3_Load(object sender, EventArgs e)
        {
            var myScatter = formsPlot3.Plot.Add.Scatter(logX, logY);
            myScatter.MarkerSize = 0;   
            formsPlot3.Refresh();
        }
    }
}
