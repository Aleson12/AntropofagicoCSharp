using Accord.Math;
using ScottPlot;

namespace AntropofagicoCSharp.Forms
{
    public partial class PCA_grafico : Form
    {
        ScottPlot.Plottables.Scatter MyScatter;
        ScottPlot.Plottables.Crosshair MyCrosshair;

        RadioButton rbNearestXY;

        static string caminhoDaPastaComOsCSVs = Arquivo._caminhoDaPastaDosArquivosCSVPosTratamento;
      
        string[] arquivosCSVs = Directory.GetFiles(caminhoDaPastaComOsCSVs);
       

        public PCA_grafico()
        {
            InitializeComponent();
            this.TopMost = true; // sobrepõe este formulário em detrimento de outros
        }

        #region invocaçãoDoMétodoPCA

        private void formsPlot1_Load(object sender, EventArgs e)
        {
            Arquivo.PCA();

        }
        #endregion invocaçãoDoMétodoPCA

        #region PlotagemGraficoPCA

        public void AtualizarGrafico(double[] xs, double[] ys)
        {
            double[,] pontos = new double[xs.Length, ys.Length];

            if (xs.Count() == ys.Count())
            {

                formsPlot1.Plot.Add.ScatterPoints(xs, ys);

                for (int i = 0; i < xs.Count(); i++) {

                    pontos[0, i] = xs[i]; // Linha 0 para coordenadas X
                    pontos[1, i] = ys[i]; // Linha 1 para coordenadas Y

                }
            }

            LocalizaPonto(xs, ys);

        }

        public void LocalizaPonto(double[] xs, double[] ys)
        {
            MyScatter = formsPlot1.Plot.Add.Scatter(xs, ys);
            MyScatter.LineWidth = 0;

            MyCrosshair = formsPlot1.Plot.Add.Crosshair(42, 0.48);
            MyCrosshair.IsVisible = false;
            MyCrosshair.MarkerShape = MarkerShape.OpenCircle;

            // inicializa e adiciona um botão
            rbNearestXY = new RadioButton();
            rbNearestXY.Checked = true; 
            rbNearestXY.Location = new Point(10, 10); 
            Controls.Add(rbNearestXY);

            formsPlot1.MouseMove += (s, e) =>
            {
                
                Pixel mousePixel = new(e.Location.X, e.Location.Y);
                Coordinates localizacaoDoMouse = formsPlot1.Plot.GetCoordinates(mousePixel);

                DataPoint nearest = MyScatter.Data.GetNearest(localizacaoDoMouse, formsPlot1.Plot.LastRender);

                // se o dataPoint for nulo:
                if (nearest.Equals(DataPoint.None))
                    return;
                
                // remove, "limpa", cada ponto sombreado pelo ponteiro do mouse:
                formsPlot1.Plot.Remove<ScottPlot.Plottables.Callout>();

                List<string> nomeDosArquivosCSV = new List<string>();

                foreach (string arquivoCSV in arquivosCSVs)
                    nomeDosArquivosCSV.Add(Path.GetFileNameWithoutExtension(arquivoCSV));
                
                var objectCallot = formsPlot1.Plot.Add.Callout($"X: {nearest.X}\nY: {nearest.Y}\nOrigem: {nomeDosArquivosCSV[nearest.Index]}" ,
                    
                    textLocation: nearest.Coordinates,
                    tipLocation: nearest.Coordinates

                 );

                // remove, "limpa", o último ponto sombreado pelo ponteiro do mouse:
                formsPlot1.Refresh();

                 if (nearest.IsReal)
                 {
                     MyCrosshair.Position = nearest.Coordinates;
                     formsPlot1.Refresh();

                     Text = $"Coordenadas: X={nearest.X:0.##}, Y={nearest.Y:0.##}; Origem:  ";

                 }

                 // hide the crosshair when no point is selected
                 if (!nearest.IsReal && MyCrosshair.IsVisible)
                 {
                     MyCrosshair.IsVisible = false;
                     formsPlot1.Refresh();
                     Text = $"No point selected";
                 }
            };
        }

        /*
        public void FormsPlot1_MouseHover(double[] xs, double[]ys)
        {

            formsPlot1.Plot.Add.Callout("Hello",
                textLocation: new(xs[3], ys[5]),
                tipLocation: new(xs[6], ys[6])
            );

            formsPlot1.Plot.Add.Callout("World",
                textLocation: new(xs[5], ys[2]),
                tipLocation: new(xs[13], ys[13])
            );

        }*/

        #endregion PlotagemGraficoPCA
    }
}
