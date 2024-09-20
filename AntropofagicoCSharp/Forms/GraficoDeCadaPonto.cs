using ScottPlot;
using System;
using System.Windows.Forms;
using ScottPlot.WinForms;

namespace AntropofagicoCSharp.Forms
{
    public partial class GraficoDeCadaPonto : Form
    {
        // instanciando o formulário de escala logarítmica:
        GraficoEscalaLogaritmica graficoLog = new GraficoEscalaLogaritmica();

        public GraficoDeCadaPonto()
        {
            InitializeComponent();


        }
        public void PlotagemIndividual(List<double> valoresContidosNoArquivoCsvLido)
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

            this.BringToFront(); // fazendo sobrepor este gráfico aos demais
            formsPlot2.Plot.Clear();
            
            var myScatter = formsPlot2.Plot.Add.Scatter(arrayX, arrayY); // armazenando o resultado da plotagem numa variável

            myScatter.MarkerSize = 0; // definindo zero marcadores

            formsPlot2.Refresh(); // atualizando o gráfico
        }

        // método para mostrar o gráfico com os dados em escala logarítmica:
        private void MostrarGraficoEmEscalaLogaritmica(object sender, EventArgs e)
        {
            if (graficoLog == null || graficoLog.IsDisposed) // se o objeto referente ao Gráfico de Logarítmo for nulo ou tiver sido descartado, 
                graficoLog = new GraficoEscalaLogaritmica(); // instancie um novo objeto

            graficoLog.Show(); // mostrar formulário
            graficoLog.TopMost = true; // força a sobreposição deste formulário em detrimento dos outros
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MostrarGraficoEmEscalaLogaritmica(sender, e);
        }
    }
}
