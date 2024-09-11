using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Accord.Math;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WinForms;

namespace AntropofagicoCSharp.Forms
{
    public partial class GraficoDeCadaPonto : Form
    {

        public GraficoDeCadaPonto()
        {
            InitializeComponent();
        }

        public void PlotagemIndividual(List<double> valoresContidosNoArquivoCsvLido)
        {
            ScottPlot.Plottables.Scatter myScatter;

            List<double> Lista_X = new List<double>();
            List<double> Lista_Y = new List<double>();

            int cont = 0; // equivale à variável "canais" no código em python

            valoresContidosNoArquivoCsvLido.ForEach(valor =>
            {
                Lista_X.Add(cont);
                Lista_Y.Add(valor);

                cont++;
            });

            double[] arrayX = Lista_X.ToArray();
            double[] arrayY = Lista_Y.ToArray();

            formsPlot2.Plot.Clear();
            myScatter = formsPlot2.Plot.Add.Scatter(arrayX, arrayY);
            myScatter.LineWidth = 0;
            myScatter.LineColor = ScottPlot.Colors.Red;
                        
        }
    }
}
