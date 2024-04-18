
// "PascalCase" para nomes de classes, métodos, variáveis globais, Listas e Vetores (globais ou locais);
// "camelCase" para nomes de variáveis locais

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
            DefCorBorda();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog(); // objeto instanciado da classe que permite selecionar uma pasta do diretório local

            DialogResult result = fbd.ShowDialog(); // exibe, efetivamente, as pastas do diretório local para serem selecionadas, 
            // e insere na variável "result" OK ou False, se o usuário tiver selecionado uma pasta ou não, respectivamente 

            if (result == DialogResult.OK) // se o usuário selecionar uma pasta
            {
                Diretorio = fbd.SelectedPath; // a variável "diretorio" receberá o caminho da pasta

                maskedTextBox1.Text = $"{Diretorio}\\".Replace("/", "\\"); // inserindo na caixa de entrada de texto o Path da pasta selecionada, e, também, definindo o padrão de barras para todos os sistemas operacionais

                string[] CaminhoDeCadaArquivo = Directory.GetFiles(Diretorio); // extraindo o caminho de cada arquivo da variável "Diretorio" (que contém o caminho da pasta selecionada) e inserindo no vetor "CaminhoDeCadaArquivo"
                List<string> ArquivoComExtensao = new List<string>();

                foreach (string caminho in CaminhoDeCadaArquivo) // para cada caminho de arquivo,
                {
                    ArquivoComExtensao.Add(Path.GetFileName(caminho)); // extraindo apenas o nome dele (do arquivo) *junto com a sua extensão*
                }

                foreach (string arquivo in ArquivoComExtensao)
                {
                    string apenasNomeDoArquivo = Path.GetFileNameWithoutExtension(arquivo); // obtendo apenas o nome do arquivo
                    string apenasExtensao = Path.GetExtension(arquivo); // obtendo apenas a extensão do arquivo

                    if (apenasExtensao == ".txt") // se a extensão for ".txt", 
                    {
                        Lista_de_arquivos_txt_da_pasta.Add(apenasNomeDoArquivo); // adicione cada um dos arquivos - sem extensão - na lista

                        string caminhoDosArquivosComBarraInvertida = $"{Diretorio}/{arquivo}".Replace("/", "\\");

                    }

                    List<string> ArquivosComBarraInvertida = new List<string>();

                    ArquivosComBarraInvertida.Add(arquivo);

                    foreach (string a in ArquivosComBarraInvertida)
                    {
                        groupBox3.Text = a;
                    }

                }
            }
        }

        private void DefCorBorda() // este método faz com que as bordas de alguns componentes sejam da cor verde
        { 

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

            groupBox5.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox5.Width - 1, groupBox5.Height - 1));
            };
            groupBox6.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox6.Width - 1, groupBox6.Height - 1));
            };
            groupBox3.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox3.Width - 1, groupBox3.Height - 1));

            };
            groupBox4.Paint += (sender, e) =>
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, groupBox4.Width - 1, groupBox4.Height - 1));

            };
        }
    }
}

