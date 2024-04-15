
// importação das bibliotecas necessárias:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis; // biblioteca para trabalhar com DataFrame
using Microsoft.ML;
using System.Drawing;

namespace AntropofagicoCSharp

{
    public partial class Form1 : Form
    {
        // declaração de listas:

        List<string> Lista_de_arquivos_txt_da_pasta = new List<string>();
        List<string> Lista_dos_arquivos_agrupados = new List<string>();
        List<string> Lista_nomecsv = new List<string>();

        // declaração de um vetor:

        double[,] Media_dos_valores_da_matriz = new double[,] { };

        // variáveis globais:

        int dados;
        int DivisorParaMediaEQuantidadeDeColunasDaMatriz = 0;
        string Caminho_com_o_nome_do_arquivo_csv_final = "";
        string Pasta_dos_arquivos_csv_pos_tratamento = "";
        string Numero_pos_hifen = "";
        string Compara_numero = "";
        string Compara_nome = "";
        string Nome_com_tipo = "";
        string Nome_do_csv = "";
        string Nome_novo = "";
        string Diretorio = "";

        public Form1()
        {
            InitializeComponent();
            manipulacaoDaEspessuraDaBorda();
        }

        private void manipulacaoDaEspessuraDaBorda()
        {
            int borderWidth = 1;
            var borderPen = new Pen(Color.Green, borderWidth);

            groupBox1.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1));
            };
            groupBox2.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox2.Width - 1, groupBox2.Height - 1));
            };
            groupBox3.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox3.Width - 1, groupBox3.Height - 1));
            };
            groupBox4.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox4.Width - 1, groupBox4.Height - 1));
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

        private void button1_Click(object sender, EventArgs e) // filtrar arquivos txt da pasta
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult result = fbd.ShowDialog(); // exibe, efetivamente, as pastas do diretório local para serem selecionadas   

            if (result == DialogResult.OK) // se o usuário selecionar uma pasta
            {
                Diretorio = fbd.SelectedPath; // a variável "diretorio" receberá o caminho da pasta

                for (int i = 0; i <= Diretorio.Length; i++)
                {

                }
            }


        }
    }
}
