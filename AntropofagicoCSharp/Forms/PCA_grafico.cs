using ScottPlot;

namespace AntropofagicoCSharp.Forms
{
    public partial class PCA_grafico : Form
    {
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

        public void AtualizarGrafico(double[]xs, double[]ys)
        {
            formsPlot1.Plot.Add.ScatterPoints(xs, ys);
        }

        #endregion PlotagemGraficoPCA

    }
}
