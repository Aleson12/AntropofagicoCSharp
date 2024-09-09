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

        public static Scatter PlotagemIndividual(List<double> valoresContidosNoArquivoCsvLido)
        {
            ScottPlot.Plottables.Scatter MyScatter;

            List<double> Lista_Y = new List<double>();
            List<double> Lista_X = new List<double>();

            int cont = 0; // equivale à variável "canais" no código em python

            valoresContidosNoArquivoCsvLido.ForEach(x =>
            {

                double valor = x;
                Lista_X.Add(cont);
                Lista_Y.Add(valor);

                Console.WriteLine(Lista_Y);
                Console.WriteLine(Lista_X);

                cont++;

            });

            // PCA_grafico pcaGrafico = new PCA_grafico();

            //  pcaGrafico.AtualizarGrafico(Lista_X.ToArray(), Lista_Y.ToArray());

            ScottPlot.WinForms.FormsPlot formsPlot2 = new ScottPlot.WinForms.FormsPlot();
            formsPlot2.Plot.Add.ScatterPoints(Lista_X, Lista_Y);

            MyScatter = formsPlot2.Plot.Add.Scatter(Lista_X, Lista_Y);

            return MyScatter;
        }
    }
}
