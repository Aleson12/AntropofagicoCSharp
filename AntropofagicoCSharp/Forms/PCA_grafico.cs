using Microsoft.DotNet.Interactive.Formatting;
using ScottPlot;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AntropofagicoCSharp.Forms
{
    public partial class PCA_grafico : Form
    {
        public PCA_grafico()
        {
            InitializeComponent();
            this.TopMost = true; // sobrepõe este formulário em detrimento de outros
        }

        public void formsPlot1_Load(List<double> elemPrimCol, List<double> elemSegCol)
        {
            var myPlot = formsPlot1.Plot;
            var MyScatter = myPlot.Add.ScatterPoints(elemPrimCol, elemSegCol);

            //  var imagemDoGraficoEmPNG = myPlot.SavePng("scatter_list_demo.png", 400, 300); // inserir isto, depois, em uma pasta dentro da pasta onde está o projeto

            formsPlot1.MouseMove += (s, e) =>
            {
                Pixel mousePixel = new(e.Location.X, e.Location.Y);
                Coordinates mouseLocation = formsPlot1.Plot.GetCoordinates(mousePixel);

                DataPoint nearestPoint = MyScatter.Data.GetNearest(mouseLocation, formsPlot1.Plot.LastRender);

                if (nearestPoint.IsReal)
                {
                    label2.Text = $"Coordenadas: X={nearestPoint.X:0.00}, Y={nearestPoint.Y:0.00}";
                    this.Controls.Add(label2);
                    formsPlot1.Refresh();
                }
                else
                {
                    label2.Text = string.Empty;
                    this.Controls.Add(label2);
                    formsPlot1.Refresh();
                }
            };

        }
    }
}
