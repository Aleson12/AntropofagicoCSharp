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
            if (xs.Count() == ys.Count())
                formsPlot1.Plot.Add.ScatterPoints(xs, ys);
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

                // lista para conter apenas o nome de cada arquivo.csv sem o seu caminho de diretório:
                List<string> nomeDosArquivosCSV = new List<string>();

                // percorrendo cada arquivo.csv, obtendo apenas o seu nome e extensão:
                foreach (string arquivoCSV in arquivosCSVs)
                    nomeDosArquivosCSV.Add(Path.GetFileName(arquivoCSV));
                
                // obtendo o retorno do método Callout, que é um Objeto, para manipulá-lo:
                formsPlot1.Plot.Add.Callout($"X: {nearest.X}\nY: {nearest.Y}\nOrigem: {nomeDosArquivosCSV[nearest.Index]}" ,
                    
                    textLocation: nearest.Coordinates,
                    tipLocation: nearest.Coordinates

                 );

                // remove, "limpa", o último ponto sombreado pelo ponteiro do mouse:
                formsPlot1.Refresh();

                 if (nearest.IsReal)
                 {
                     MyCrosshair.Position = nearest.Coordinates;
                     formsPlot1.Refresh();

                     Text = $"Coordenadas: X={nearest.X:0.##}, Y={nearest.Y:0.##}; Origem: {nomeDosArquivosCSV[nearest.Index]}";

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
        #endregion PlotagemGraficoPCA
    }
}
