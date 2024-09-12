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

        public Plot PlotagemIndividual(List<double> valoresContidosNoArquivoCsvLido)
        {

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

            var plt = new ScottPlot.Plot();
            plt.Add.Scatter(arrayX, arrayY);
            plt.XLabel("Canal");
            plt.YLabel("Contagem");
            plt.Title("Canal x Contagem");

            return plt;

           // ScottPlot.WinForms.FormsPlot formsPlot2 = new ScottPlot.WinForms.FormsPlot();
  
           // formsPlot2.Plot.Add.Scatter(arrayX, arrayY);
            //formsPlot2.Refresh();
            
           // return formsPlot2;
        }
    }
}
