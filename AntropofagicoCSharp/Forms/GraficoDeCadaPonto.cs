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

            this.BringToFront();
            formsPlot2.Plot.Clear();
            myScatter = formsPlot2.Plot.Add.Scatter(arrayX, arrayY); // armazenando os pontos na variável "myScatter"
            myScatter.LineWidth = 0; // 0 linhas que ligam os pontos
        }
    }
}
