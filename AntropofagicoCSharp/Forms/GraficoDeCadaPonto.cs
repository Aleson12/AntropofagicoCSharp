﻿using ScottPlot;
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

            arrayX = Lista_X.ToArray();
            arrayY = Lista_Y.ToArray();

            this.BringToFront(); // fazendo sobrepor este gráfico aos demais
            formsPlot2.Plot.Clear();

            var myScatter = formsPlot2.Plot.Add.Scatter(arrayX, arrayY); // armazenando o resultado da plotagem numa variável

            myScatter.MarkerSize = 0; // definindo zero marcadores

            formsPlot2.Refresh(); // atualizando o gráfico
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (graficoLog == null || graficoLog.IsDisposed) //  se o objeto instanciado do gráfico de logarítmico for nulo ou se já tiver sido usado
                graficoLog = new GraficoEscalaLogaritmica(); // instancia um novo objeto do gráfico de logarítmico
            
            graficoLog.TopMost = true; // sobrepõe o gráfico atual em detrimento dos outros 
            graficoLog.CalculoLogaritmico(arrayX, arrayY); // faz o cálculo do gráfico de logarítmo
            graficoLog.formsPlot3_Load(sender, e); // invocação do método que plota os dados no gráfico
            graficoLog.Show(); // apresenta o gráfico
            graficoLog.Refresh(); 
        }
    }
}
