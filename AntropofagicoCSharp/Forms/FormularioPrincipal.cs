
// "PascalCase" para nomes de classes, métodos, variáveis globais, Listas e Vetores (globais ou locais);
// "camelCase" para nomes de variáveis locais

// importação das bibliotecas necessárias:


namespace AntropofagicoCSharp
{
    public partial class FormularioPrincipal : Form
    {

        public static string Diretorio;


        public FormularioPrincipal()
        {
            InitializeComponent();           

        }

      
        private void btn3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _selecionarPasta = new FolderBrowserDialog(); // objeto instanciado da classe que permite selecionar uma pasta do diretório local

            if (_selecionarPasta.ShowDialog() == DialogResult.OK) // se o usuário selecionar uma pasta
            {
                Diretorio = _selecionarPasta.SelectedPath; // o caminho da pasta selecionada é inserido na variável global "Diretorio"
                maskedTextBox1.Text = $"{Diretorio}\\".Replace("/", "\\"); // inserindo na caixa de entrada de texto o Path da pasta selecionada, e, também, definindo o padrão de barras para todos os sistemas operacionais
                richTextBox1.Clear();

                Arquivo.FiltrarArquivosTxt(maskedTextBox1.Text).ForEach(arquivo => { richTextBox1.AppendText(arquivo + "\n"); });


                if (MessageBox.Show("Os arquivos estão com o nome Rom e extensão .TXT?", "Nome e extensão do(s) arquivo(s)", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            richTextBox1.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, richTextBox1.Width - 1, richTextBox1.Height - 1));
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
