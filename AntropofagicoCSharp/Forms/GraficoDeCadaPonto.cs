using ScottPlot;
using System;
using System.Windows.Forms;
using ScottPlot.WinForms;
using System.ComponentModel;

namespace AntropofagicoCSharp.Forms
{
    public partial class GraficoDeCadaPonto : Form
    {
        // instanciando o formulário de escala logarítmica:
        GraficoEscalaLogaritmica graficoLog = new GraficoEscalaLogaritmica();

        double[] arrayX;
        double[] arrayY;

        List<double> Lista_X = new List<double>();
        List<double> Lista_Y = new List<double>();

        public GraficoDeCadaPonto()
        {
            InitializeComponent();
        }

        public void PlotagemIndividual(double[,] valoresContidosNoArquivoCsvLido)
        {
            // limpando as listas depois de usá-las; isso faz com que  
            // os gráficos de linha contínua não se sobreponham
            Lista_X.Clear();
            Lista_Y.Clear();

            for (int i = 0; i < valoresContidosNoArquivoCsvLido.GetLength(0); i++)
            {
                Lista_X.Add(valoresContidosNoArquivoCsvLido[i, 0]); // obtendo os valores da coluna à esquerda (X) e inserindo numa Lista
                Lista_Y.Add(valoresContidosNoArquivoCsvLido[i, 1]); // obtendo os valores da colunas à direita (Y) e inserindo numa lista
            }

            arrayX = Lista_X.ToArray(); // transformando a lista de valores em array
            arrayY = Lista_Y.ToArray(); // transformando a lista de valores em array
            
            formsPlot2.Plot.Clear();

            this.BringToFront(); // fazendo sobrepor este gráfico aos demais

            var myScatter = formsPlot2.Plot.Add.Scatter(arrayX, arrayY); // armazenando o resultado da plotagem numa variável
            
            myScatter.MarkerSize = 0; // definindo zero marcadores

            formsPlot2.Refresh(); // atualizando o gráfico

            this.FormClosed += GraficoDeCadaPonto_FormClosed; // ao fechar o formulário, execute o método
        }

        private void GraficoDeCadaPonto_FormClosed(object? sender, FormClosedEventArgs e)
        {
            graficoLog.Close(); // fecha o gráfico de logarítmo
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (graficoLog == null || graficoLog.IsDisposed) //  se o objeto instanciado do gráfico de logarítmico for nulo ou se já tiver sido usado
                graficoLog = new GraficoEscalaLogaritmica(); // instancia um novo objeto do gráfico de logarítmico

            graficoLog.TopMost = true; // sobrepõe o gráfico atual em detrimento dos outros 
            graficoLog.CalculoLogaritmico(arrayX, arrayY); // faz o cálculo do gráfico de logarítmo
            graficoLog.Show(); // apresenta o gráfico
            graficoLog.Refresh();
        }
    }
}
