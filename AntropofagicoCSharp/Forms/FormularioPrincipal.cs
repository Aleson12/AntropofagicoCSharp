using Microsoft.VisualBasic.ApplicationServices;
using ScottPlot.WinForms;
using System.Windows.Forms;

namespace AntropofagicoCSharp
{
    public partial class FrmPrincipal : Form
    {

        public static string diretorio;

        public FrmPrincipal() // Interface Principal
        {
            InitializeComponent();
            UseWaitCursor = false; // forçando o cursor do mouse a não ficar em estado de espera quando ele sobrepor os componentes da interface
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selecionarPasta = new FolderBrowserDialog(); // objeto instanciado da classe que permite selecionar uma pasta do diretório local

            if (selecionarPasta.ShowDialog() == DialogResult.OK) // se o usuário selecionar uma pasta
            {
                diretorio = selecionarPasta.SelectedPath; // o caminho da pasta selecionada é inserido na variável global "Diretorio"
                mtx_DiretorioArquivosTxt.Text = $"{diretorio}\\".Replace("/", "\\"); // inserindo na caixa de entrada de texto o Path da pasta selecionada, e, também, definindo o padrão de barras para todos os sistemas operacionais
                rtx_ArquivosTxt.Clear();

                Arquivo.FiltrarArquivosTxt(mtx_DiretorioArquivosTxt.Text).ForEach(arquivo => { rtx_ArquivosTxt.AppendText(arquivo + "\n"); });


                if (MessageBox.Show("Os arquivos estão com o nome Rom e extensão .TXT?", "Nome e extensão do(s) arquivo(s)", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Arquivo.AgrupandoOsTxtsPorClasse();
                mtx_DiretorioArquivosCsv.Clear(); // limpa o campo de texto
                mtx_DiretorioArquivosCsv.Text = Arquivo._caminhoDaPastaDosArquivosCSVPosTratamento; // insere no campo o caminho de diretório onde estão os arquivos tratados
                rtx_ArquivosCsv.Text = Arquivo._caminhosCsv; // exibindo cada caminho de arquivo csv no richTextBox
            }
        }

        private void FormularioPrincipal_Load(object sender, EventArgs e)
        {
            // definindo a cor das bordas:

            int borderWidth = 1; // definindo a largura da borda (sua espessura)
            var borderPen = new Pen(Color.DarkBlue, borderWidth); // definindo sua cor 

            grp_LeituraEExibicaoDosArquivosTxt.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 8, grp_LeituraEExibicaoDosArquivosTxt.Width - 1, grp_LeituraEExibicaoDosArquivosTxt.Height - 10));
            };
            grp_GerarEExibirArquivosCsv.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 8, grp_GerarEExibirArquivosCsv.Width - 1, grp_GerarEExibirArquivosCsv.Height - 10));
            };
            rtx_ArquivosTxt.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, rtx_ArquivosTxt.Width - 1, rtx_ArquivosTxt.Height - 1));
            };
            grp_GerarArquivoCsvComTodosDados.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 8, grp_GerarArquivoCsvComTodosDados.Width - 1, grp_GerarArquivoCsvComTodosDados.Height - 10));
            };
            grpAplicarEPlotarPCA.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 8, grpAplicarEPlotarPCA.Width - 1, grpAplicarEPlotarPCA.Height - 10));
            };
        }

        private void btn_GerarCSV_Click(object sender, EventArgs e)
        {
            Arquivo.GeraMatrizFinal();
            mtx_DiretorioDoArquivoCsvFinal.Clear();
            mtx_DiretorioDoArquivoCsvFinal.Text = Arquivo._caminhoComONomeDoArquivoCSVFinal + "MatrizPCA.csv".ToString();
        }

        private void btn_AplicarPCA_Click(object sender, EventArgs e)
        {
            Arquivo.PCA();
        }
    }
}