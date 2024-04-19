
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
                Diretorio = fbd.SelectedPath; 

                maskedTextBox1.Text = $"{Diretorio}\\".Replace("/", "\\"); // inserindo na caixa de entrada de texto o Path da pasta selecionada, e, também, definindo o padrão de barras para todos os sistemas operacionais
                richTextBox1.Clear(); 
      
                var arquivos = Directory.GetFiles(Diretorio); // extraindo o caminho de cada arquivo em Diretorio e inserindo na variável "arquivos"

                arquivos.ToList().ForEach(arquivo => // transformando a variável em uma lista e percorrendo-a
                { 
                    if (Path.GetExtension(arquivo) == ".txt") // se a extensão do arquivo corrente for .txt
                    {
                        richTextBox1.AppendText(arquivo + "\n"); // o arquivo será exibido no "richTextBox1"
                    }
                });

                // criando a janela modal: 
         
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result1;
                string message = "Os arquivos estão com o nome Rom e extensão .TXT?";
                string title = "Nome e extensão do(s) arquivo(s)";

                result1 = MessageBox.Show(message, title, buttons);

                if (result1 == DialogResult.OK)
                {
                    // AgrupandoOsTxtsPorClasse();
                }
                else
                {
                    // InterfaceRenomearArquivo.Main();
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

