namespace AntropofagicoCSharp
{
    public partial class IPrincipal : Form
    {
        public static string diretorio;

        public IPrincipal() // Interface Principal
        {
            InitializeComponent();           

        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selecionarPasta = new FolderBrowserDialog(); // objeto instanciado da classe que permite selecionar uma pasta do diret�rio local

            if (selecionarPasta.ShowDialog() == DialogResult.OK) // se o usu�rio selecionar uma pasta
            {
                diretorio = selecionarPasta.SelectedPath; // o caminho da pasta selecionada � inserido na vari�vel global "Diretorio"
                mtx_MaskedTextBox1.Text = $"{diretorio}\\".Replace("/", "\\"); // inserindo na caixa de entrada de texto o Path da pasta selecionada, e, tamb�m, definindo o padr�o de barras para todos os sistemas operacionais
                rtx_RichTextBox1.Clear();

                Arquivo.FiltrarArquivosTxt(mtx_MaskedTextBox1.Text).ForEach(arquivo => { rtx_RichTextBox1.AppendText(arquivo + "\n"); });


                if (MessageBox.Show("Os arquivos est�o com o nome Rom e extens�o .TXT?", "Nome e extens�o do(s) arquivo(s)", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Arquivo.AgrupandoOsTxtsPorClasse();


            }
        }

        private void FormularioPrincipal_Load(object sender, EventArgs e)
        {

            // definindo a cor das bordas:

            int borderWidth = 1; // definindo a largura da borda (sua espessura)
            var borderPen = new Pen(Color.Green, borderWidth); // definindo sua cor 

            groupBox1.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1));
            };
            groupBox2.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox2.Width - 1, groupBox2.Height - 1));
            };
            rtx_RichTextBox1.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, rtx_RichTextBox1.Width - 1, rtx_RichTextBox1.Height - 1));
            };
            groupBox5.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox5.Width - 1, groupBox5.Height - 1));
            };
            groupBox6.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox6.Width - 1, groupBox6.Height - 1));
            };
        }
    }
}
