
// "PascalCase" para nomes de classes, métodos, variáveis globais, Listas e Vetores (globais ou locais);
// "camelCase" para nomes de variáveis locais

// importação das bibliotecas necessárias:

using System.IO;
using System.Globalization;
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
using CsvHelper; // biblioteca para ler (e manipular) arquivos csv
using CsvHelper.Configuration;
using System.Data;
using System.Reflection.Metadata;
using System.Linq; // LINQ

namespace AntropofagicoCSharp
{
    public partial class Form1 : Form
    {
        // declaração de listas:

        List<string> Lista_de_caminhos_dos_arquivos_txt_da_pasta = new List<string>();
        List<string> Lista_de_arquivos_txt_da_pasta = new List<string>();  // lista de .txt's não ordenada 
        List<string> Lista_ordenada_de_arquivos_txt_da_pasta = new List<string>(); // lista de .txt's ordenada numericamente 
        List<string> Lista_dos_arquivos_agrupados = new List<string>();
        List<string> Lista_nomecsv = new List<string>();

        // declaração de um vetor:

        double[,] Media_dos_valores_da_matriz = new double[,] { };

        // variáveis globais:

        int dados;
        string Caminho_com_o_nome_do_arquivo_csv_final = "";
        string Pasta_dos_arquivos_csv_pos_tratamento = "";
        string Compara_numero = "";
        string Compara_nome = "";
        string Nome_com_tipo = "";
        string Numero_pos_hifen = "";
        string Nome_do_csv = "";
        string Nome_novo = "";
        string Diretorio = "";

        private bool validaPrimeiroCaso = false;

        public Form1()
        {
            InitializeComponent();
            DefCorBorda();

        }

        private void AgrupandoOsTxtsPorClasse()
        {
            // extraindo apenas o nome do arquivo .txt (sem a extensão e o seu caminho de diretório) 
            Lista_de_caminhos_dos_arquivos_txt_da_pasta.ForEach(caminho => {
                if (Path.GetDirectoryName(caminho) != " ")

                Lista_de_arquivos_txt_da_pasta.Add(Path.GetFileNameWithoutExtension(caminho));
                Lista_ordenada_de_arquivos_txt_da_pasta = Lista_de_arquivos_txt_da_pasta.OrderBy(str => GetNumero(str)).ToList();


            });

            static int GetNumero(string str) // método para obter o número do arquivo .txt
            {
                string[] partes = str.Split('-');

                if (partes.Length >= 2 && partes[0].StartsWith("Rom"))
                {
                    int numero;

                    if (int.TryParse(partes[0].Substring(3), out numero))
                    {
                        return numero;
                    }

                }

                return int.MaxValue; 

            }

            Lista_ordenada_de_arquivos_txt_da_pasta.Add("null 1-1"); // adicionando um valor ao final da lista para que a repetição pare

            foreach (string arquivo_txt in Lista_ordenada_de_arquivos_txt_da_pasta) // para cada arquivo .txt na lista de arquivos .txt
            {

                //Console.WriteLine(arquivo_txt);

                string[] partes = arquivo_txt.Split('-'); // dividindo o nome do arquivo pelo hífen

                Nome_com_tipo = partes[0]; // nome do arquivo
                Numero_pos_hifen = partes[1]; // número após o hífen (sem a extensão do arquivo)

                if (Nome_com_tipo != Compara_nome)
                {
                    Compara_nome = Nome_com_tipo;

                    
                    if (Lista_dos_arquivos_agrupados.Count != 0)
                    {
                        ProcessamentoDosTxtsAgrupados();
                        Lista_ordenada_de_arquivos_txt_da_pasta.Clear(); // limpa a lista
                    }

                    if (!validaPrimeiroCaso)
                        Lista_dos_arquivos_agrupados.Add(arquivo_txt);
                    validaPrimeiroCaso = true;
                    
                }
                else if (Nome_com_tipo == Compara_nome)
                {
                    Lista_dos_arquivos_agrupados.Add(arquivo_txt);
                }
                else if (Nome_com_tipo != "null1")
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
            FolderBrowserDialog fbd = new FolderBrowserDialog(); // objeto instanciado da classe que permite selecionar uma pasta do diretório local

            DialogResult result = fbd.ShowDialog(); // exibe, efetivamente, as pastas do diretório local para serem selecionadas, 
            // e insere na variável "result" OK ou False, se o usuário tiver selecionado uma pasta ou não, respectivamente 

            if (result == DialogResult.OK) // se o usuário selecionar uma pasta
            {
                Diretorio = fbd.SelectedPath; // o caminho da pasta selecionada é inserido na variável global "Diretorio"

                maskedTextBox1.Text = $"{Diretorio}\\".Replace("/", "\\"); // inserindo na caixa de entrada de texto o Path da pasta selecionada, e, também, definindo o padrão de barras para todos os sistemas operacionais
                richTextBox1.Clear(); 
      
                var arquivos = Directory.GetFiles(Diretorio); // extraindo o caminho de cada arquivo em Diretorio e inserindo na variável "arquivos"

                arquivos.ToList().ForEach(arquivo => // transformando a variável em uma lista e percorrendo-a
                { 
                    if (Path.GetExtension(arquivo) == ".txt") // se a extensão do arquivo corrente for .txt
                    {
                        Lista_de_caminhos_dos_arquivos_txt_da_pasta.Add(arquivo);
                        richTextBox1.AppendText(arquivo + "\n"); // o arquivo será exibido no "richTextBox1" um abaixo do outro
                    }
                });

                // criando a janela modal: 
         
                MessageBoxButtons buttons = MessageBoxButtons.YesNo; // criando botão de "sim" ou "não"

                DialogResult result1 = MessageBox.Show("Os arquivos estão com o nome Rom e extensão .TXT?", "Nome e extensão do(s) arquivo(s)", buttons);

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

        private void ProcessamentoDosTxtsAgrupados()
        {

            Console.WriteLine(Lista_dos_arquivos_agrupados); 

            int divisorParaMediaEQuantidadeDeColunasDaMatriz = Lista_dos_arquivos_agrupados.Count;

            int linhas = 2048;
            int colunas = divisorParaMediaEQuantidadeDeColunasDaMatriz; // a quantidade de colunas da Matriz será a igual ao tamanho da Lista_dos_arquivos_agrupados

            // criação de matriz com 2048 linhas e "n" colunas, preenchida apenas com zeros

            Matrix<double> matriz = Matrix<double>.Build.Dense(linhas, colunas);

             var listaEnumerada = Lista_dos_arquivos_agrupados.Select((value, index) => new {Index = index, Value = value}); // lendo cada elemento da lista e o seu respectivo índice

            // percorrendo a lista e extraindo o seu elemento/valor e índice:

            foreach (var item in listaEnumerada) // percorrendo a lista
            {
                var colunaIndice = item.Index; // extraindo o índice
                var arquivoValor = item.Value; // extraindo o valor

                string nomeDoArquivoTxtComCaminho = (Diretorio + "\\" + arquivoValor + ".txt").ToString();

                // configuração - opcional - do arquivo csv
                var config = new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture)
                {
                    Delimiter = ";", // o separador entre os valores será o ponto-e-vírgula
                    HasHeaderRecord = false // sem cabeçalho
                };

                List<string> lista_de_arquivos_em_linha_csv = new List<string>();

                using (var leitor = new StreamReader(nomeDoArquivoTxtComCaminho))
                using (var csv = new CsvReader(leitor, config)) // "csv" recebe o nome do arquivo e a configuração personalizada para ele
                {
                    while (csv.Read()) {

                        // Constrói a linha atual do CSV
                        string linhaAtual = "";

                        // Obtém o número de campos na linha atual
                        int numCampos = csv.Parser.Count;

                        // Itera sobre os campos da linha atual
                        for (int i = 0; i < numCampos; i++)
                        {
                            // Adiciona o campo à linha atual
                            linhaAtual += csv.GetField<string>(i);

                            // Adiciona uma vírgula se não for o último campo
                            if (i < numCampos - 1)
                            {
                                linhaAtual += ";";
                            }
                        }
                        // Adiciona a linha atual à lista
                        lista_de_arquivos_em_linha_csv.Add(linhaAtual);
                    }
                }
            }
        }
    }
}
