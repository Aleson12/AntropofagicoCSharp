
// "PascalCase" para nomes de classes, m�todos, vari�veis globais, Listas e Vetores (globais ou locais);
// "camelCase" para nomes de vari�veis locais

// importa��o das bibliotecas necess�rias:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis; // biblioteca para trabalhar com DataFrame
using Microsoft.ML;
using System.Drawing;
using System.Collections.Immutable;
using System.Drawing.Drawing2D;
using MathNet.Numerics.LinearAlgebra; // biblioteca instalada para trabalhar com Matrizes


namespace AntropofagicoCSharp

{
    public partial class Form1 : Form
    {
        // declara��o de listas:

        List<string> Lista_de_arquivos_txt_da_pasta = new List<string>();
        List<string> Lista_dos_arquivos_agrupados = new List<string>();
        List<string> Lista_nomecsv = new List<string>();

        // declara��o de um vetor:

        double[,] Media_dos_valores_da_matriz = new double[,] { };

        // vari�veis globais:

        int Matriz;
        int dados;
        int DivisorParaMediaEQuantidadeDeColunasDaMatriz = 0;
        string Caminho_com_o_nome_do_arquivo_csv_final = "";
        string Pasta_dos_arquivos_csv_pos_tratamento = "";
        string Compara_numero = "";
        string Compara_nome = "";
        string Nome_com_tipo = "";
        string Numero_pos_hifen = "";
        string Nome_do_csv = "";
        string Nome_novo = "";
        string Diretorio = "";
       

        public Form1()
        {
            InitializeComponent();
            DefCorBorda();

        }

        private void AgrupandoOsTxtsPorClasse()
        {
            Lista_de_arquivos_txt_da_pasta.Add("null 1-1");
            Lista_de_arquivos_txt_da_pasta.Sort();

            foreach (string arquivo_txt in Lista_de_arquivos_txt_da_pasta)
            {

                string NomeArquivo_txt = Path.GetFileName(arquivo_txt);

                string[] partes = NomeArquivo_txt.Split('-');

                Nome_com_tipo = partes[0];
                Numero_pos_hifen = partes[1];

                if (Nome_com_tipo != Compara_nome)
                {
                    Compara_nome = Nome_com_tipo;
                    if (Lista_dos_arquivos_agrupados.Count != 0)
                    {
                        ProcessamentoDosTxtsAgrupados();
                        Lista_dos_arquivos_agrupados.Clear(); 
                    }
                } else if (Nome_com_tipo == Compara_nome)
                {
                    Lista_dos_arquivos_agrupados.Add(NomeArquivo_txt);
                } else if (Nome_com_tipo != "null1")
                {
                    Nome_do_csv = Nome_com_tipo;
                }
                else
                {
                    break; 
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e) // filtrarArquivosTxtsDaPasta
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog(); // objeto instanciado da classe que permite selecionar uma pasta do diret�rio local

            DialogResult result = fbd.ShowDialog(); // exibe, efetivamente, as pastas do diret�rio local para serem selecionadas, 
            // e insere na vari�vel "result" OK ou False, se o usu�rio tiver selecionado uma pasta ou n�o, respectivamente 

            if (result == DialogResult.OK) // se o usu�rio selecionar uma pasta
            {
                Diretorio = fbd.SelectedPath; 

                maskedTextBox1.Text = $"{Diretorio}\\".Replace("/", "\\"); // inserindo na caixa de entrada de texto o Path da pasta selecionada, e, tamb�m, definindo o padr�o de barras para todos os sistemas operacionais
                richTextBox1.Clear(); 
      
                var arquivos = Directory.GetFiles(Diretorio); // extraindo o caminho de cada arquivo em Diretorio e inserindo na vari�vel "arquivos"

                arquivos.ToList().ForEach(arquivo => // transformando a vari�vel em uma lista e percorrendo-a
                { 
                    if (Path.GetExtension(arquivo) == ".txt") // se a extens�o do arquivo corrente for .txt
                    {
                        Lista_de_arquivos_txt_da_pasta.Add(arquivo);
                        richTextBox1.AppendText(arquivo + "\n"); // o arquivo ser� exibido no "richTextBox1"
                    }
                });

                // criando a janela modal: 
         
                MessageBoxButtons buttons = MessageBoxButtons.YesNo; // criando bot�o de "sim" e de "n�o"

                DialogResult result1 = MessageBox.Show("Os arquivos est�o com o nome Rom e extens�o .TXT?", "Nome e extens�o do(s) arquivo(s)", buttons);

                if (result1 == DialogResult.Yes)
                {
                     AgrupandoOsTxtsPorClasse();
                }
                else
                {
                    // InterfaceRenomearArquivo.Main();
                }
            }
        }

        private void DefCorBorda() // este m�todo faz com que as bordas de alguns componentes sejam da cor verde
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

        private void ProcessamentoDosTxtsAgrupados()
        {
            int divisorParaMediaEQuantidadeDeColunasDaMatriz = Lista_dos_arquivos_agrupados.Count;

            int linhas = 2048;
            int colunas = divisorParaMediaEQuantidadeDeColunasDaMatriz;

            // cria��o de matriz com 2048 linhas e "n" colunas, preenchida apenas com zeros

            Matrix<double> matriz = Matrix<double>.Build.Dense(linhas, colunas);




            
        }

    }
}

