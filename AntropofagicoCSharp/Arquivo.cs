﻿using Accord.Math;
using Accord.Statistics.Analysis;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System;
using AntropofagicoCSharp.Forms;
using ScottPlot;
using ScottPlot.WinForms;

namespace AntropofagicoCSharp
{
    public static class Arquivo
    {
        #region propriedades
        private static List<string> arquivosTxtsDaPasta;
        private static List<string> caminhosDosArquivosTxtDaPasta;
        private static List<string> arquivosTxtsDaPastaOrdenados;
        private static List<string> arquivosAgrupados;
        private static List<double> mediaDosValoresDaMatriz;
        private static List<double> valoresDoArquivoMatrizPCA;

        private static double[,] MatrizMedias; // Matriz com todas as médias dos valores
        private static readonly int _linhas = 2048;
        private static bool _validaPrimeiroCaso = false; // variável no escopo da classe vira campo/atributo
        public static string _caminhoDaPastaDosArquivosCSVPosTratamento; // membro da classe definido como "público" para ser possível acessá-lo na classe principal da Interface
        public static string _caminhosCsv;
        public static string _caminhoComONomeDoArquivoCSVFinal;

        #endregion propriedades
        #region Metodos
        /// <summary>
        /// obtém o caminho de cada arquivo do diretório (com a extensão .txt) transformando em uma Lista
        /// </summary>
        /// <param name="diretorio"></param>
        /// <returns></returns>
        public static List<string> FiltrarArquivosTxt(string diretorio)
        {
            caminhosDosArquivosTxtDaPasta = new List<string>();

            caminhosDosArquivosTxtDaPasta = Directory.GetFiles(diretorio)
                        // Filtra apenas os arquivos com extensão ".txt"
                        .Where(arquivo => Path.GetExtension(arquivo) == ".txt").ToList();
            return caminhosDosArquivosTxtDaPasta;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Diretorio"></param>
        public static void AgrupandoOsTxtsPorClasse()
        {
            arquivosTxtsDaPasta = new List<string>();
            arquivosTxtsDaPastaOrdenados = new List<string>();
            arquivosAgrupados = new List<string>();

            //string nome_do_csv = string.Empty;
            string[] partes; // vai receber as duas partes de um nome de arquivo separados pelo hífen
            string nomeComTipo = string.Empty; // nome do arquivo
            string numeroPosHifen = string.Empty; // número após o hífen (sem a extensão do arquivo)
            string comparaNome = string.Empty;
            MatrizMedias = new double[_linhas, caminhosDosArquivosTxtDaPasta.Count];
            object MatrizVAR = new object[_linhas, caminhosDosArquivosTxtDaPasta.Count]; 
            // extraindo apenas o nome do arquivo .txt (sem a extensão e o seu caminho de diretório) 
            caminhosDosArquivosTxtDaPasta.ToList().ForEach(caminho =>
            {
                if (Path.GetDirectoryName(caminho) != " ")
                    arquivosTxtsDaPasta.Add(Path.GetFileNameWithoutExtension(caminho)); // pega o arquivo sem a extensão
            });
            //arquivosTxtsDaPastaOrdenados = arquivosTxtsDaPasta.OrderBy(arquivo => RecuperarNumeracaoDeNomeDeArquivo(arquivo)).ToList(); // ordenando a lista crescentemente de acordo com o número de cada arquivo
            arquivosTxtsDaPastaOrdenados = arquivosTxtsDaPasta.OrderBy((arquivo) =>
            {
                int num;
                bool success = int.TryParse(arquivo.Split("-")[0].Substring(3), out num);
                return success ? num : int.MaxValue;
            }).ToList();

            arquivosTxtsDaPastaOrdenados.Add("null 1-1"); // adicionando um valor ao final da lista para que a repetição pare

            arquivosTxtsDaPastaOrdenados.ForEach(arquivoOrdenado =>
            {
                string[] partes = arquivoOrdenado.Split('-'); // dividindo o nome do arquivo pelo hífen

                nomeComTipo = partes[0]; // nome do arquivo

                if (nomeComTipo != comparaNome)
                {
                    comparaNome = nomeComTipo;

                    if (arquivosAgrupados.Count != 0)
                    {
                        ProcessamentoDosTxtsAgrupados();
                        arquivosAgrupados.Clear(); // limpa a lista
                    }

                    if (!_validaPrimeiroCaso)
                        arquivosAgrupados.Add(arquivoOrdenado);
                }
                else if (nomeComTipo == comparaNome)
                    arquivosAgrupados.Add(arquivoOrdenado);

                //else if (nomeComTipo != "null1")
                //    nome_do_csv = nomeComTipo;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomeDoArquivo"></param>
        /// <returns></returns>
        private static int RecuperarNumeracaoDeNomeDeArquivo(string nomeDoArquivo) // método para obter o número do arquivo .txt
        {
            string[] partesDoNomeDoArquivo = nomeDoArquivo.Split('-'); // divide a string em duas partes, tendo como delimitador, o hífen

            if (partesDoNomeDoArquivo.Length >= 2 && partesDoNomeDoArquivo[0].StartsWith("Rom")) // se o tamanho do array for igual a dois ou maior E a string iniciar com a palavra "Rom"
            {
                if (int.TryParse(partesDoNomeDoArquivo[0].Substring(3), out int numero)) // o valor na quarta posição é transformado em inteiro e inserido na variável "numero"
                    return numero; // retorna o número de cada arquivo

            }

            return int.MaxValue; // indica que não foi possível obter um número válido do texto fornecido

        }

        /// <summary>
        /// 
        /// </summary>
        private static void ProcessamentoDosTxtsAgrupados()
        { 

            int divisorParaMediaEQuantidadeDeColunasDaMatriz = arquivosAgrupados.Count;

            int colunas = divisorParaMediaEQuantidadeDeColunasDaMatriz; // a quantidade de colunas da Matriz será a igual ao tamanho da Lista_dos_arquivos_agrupados
            string nomeDoArquivoCsv;
            int colunaDaMatriz = 0;

            // criação de matriz com 2048 linhas e "n" colunas, preenchida apenas com zeros
            double[,] matriz = new double[_linhas, colunas];

            for (int i = 0; i < colunas; i++)
            {
                string[] valores = File.ReadAllLines(FrmPrincipal.diretorio + "\\" + arquivosAgrupados[i] + ".txt".ToString());
                for (int j = 0; j < valores.Length; j++)
                {
                    if (!double.TryParse(valores[j].Split(";")[1].Trim(), out matriz[j,i]))
                    {
                        matriz[j,i] = double.MinValue; // Caso o parse gere uma exceção
                    }
                }
            }
            nomeDoArquivoCsv = arquivosAgrupados.First().Split("-")[0];
            
            GerarSomenteUmArquivoPorClasse(matriz, nomeDoArquivoCsv); // passando a matriz e o nome de cada arquivo CSV como parâmetro para este método para que ele seja capaz de manipulá-los
            colunaDaMatriz += 1;
        }
        private static List<double> ObterMedias(double[,] matriz)
        {
            int linhas = matriz.GetLength(0); // obtendo a quantidade de linhas
            int colunas = matriz.GetLength(1); // obtendo a quantidade de colunas

            List<double> medias = [];

            for (int i = 0; i < linhas; i++)
            {
                double soma = 0d;
                for (int j = 0; j < colunas; j++)
                {
                    soma += matriz[i, j];
                }

                double media;
                if (soma > 0)
                {
                    media = (soma / colunas);
                }
                else
                {
                    media = 0;
                }

                var resultadoArredondado = Math.Round(media, 5); // arredondando o resultado da divisão para cinco casas decimais
                medias.Add(resultadoArredondado);
            }
            return medias;
        }
        private static void GerarSomenteUmArquivoPorClasse(double[,] matriz, string nomeDoArquivoCsv)
        {
            mediaDosValoresDaMatriz = ObterMedias(matriz);
            string caminhoComNomeDoCsv;
            int indiceDoCSV = int.Parse(nomeDoArquivoCsv.Split("-")[0].Substring(3)) - 1;
             
            for (int i = 0; i < mediaDosValoresDaMatriz.Count; i++)
            {
                MatrizMedias[i, indiceDoCSV] = mediaDosValoresDaMatriz[i];
            }
            _caminhoDaPastaDosArquivosCSVPosTratamento = Path.Combine($"{FrmPrincipal.diretorio}\\Roms\\");
            Directory.CreateDirectory(_caminhoDaPastaDosArquivosCSVPosTratamento); // cria a pasta no sistema de arquivos

            caminhoComNomeDoCsv = Path.Combine($"{_caminhoDaPastaDosArquivosCSVPosTratamento}{nomeDoArquivoCsv}.csv"); // criando o caminho onde está o arquivo csv para ser escrito

            // criando o arquivo .csv, acessando-o, abrindo-o e escrevendo nele os novos valores
            using (StreamWriter writer = new StreamWriter(caminhoComNomeDoCsv))
            {
                foreach (double vlr in mediaDosValoresDaMatriz)
                    writer.WriteLine($"{vlr}".Replace('.', ','));
            }

            // criada uma nova variável que irá receber cada valor da variável "caminhoComNomeDoCsv" e 
            // concatenar com uma quebra de linha
            _caminhosCsv += caminhoComNomeDoCsv + '\n';

            mediaDosValoresDaMatriz.Clear();
        }

        public static void GeraMatrizFinal()
        {

            bool ignorarCondicao = true; // variável do tipo booleano criada apenas para ignorar uma condição
            List<string> arquivosDaPastaCsv = new List<string>();

            // no diretório especificado, extrair os arquivos .csv que estão lá e inserí-los numa lista
            List<string> caminhosDosArquivosCsvCriados = Directory
                .GetFiles(_caminhoDaPastaDosArquivosCSVPosTratamento)
                .Where(arquivo => Path.GetExtension(arquivo) == ".csv")
                .ToList();

            // obtendo o maior número presente nos arquivos
            int maiorNumeracaoNoNomeDoCsv = caminhosDosArquivosCsvCriados
                .Select(arquivo => Regex.Match(Path.GetFileNameWithoutExtension(arquivo), @"Rom(\d+)", RegexOptions.IgnoreCase))
                .Where(match => match.Success)
                .Select(match => int.Parse(match.Groups[1].Value))
                .DefaultIfEmpty(0)
                .Max();

            string[,] MatrizComTodosCsv = new string[_linhas, maiorNumeracaoNoNomeDoCsv];

            int qtdDeLinhas = MatrizComTodosCsv.GetLength(0); // obtendo a quantidade de linhas da Matriz

            _caminhoComONomeDoArquivoCSVFinal = Path.Combine($"{FrmPrincipal.diretorio}\\MatrizFinal\\"); // criando novo caminho de diretório
            string nomeArquivoCsv = "MatrizPCA.csv";

            //// Criação do arquivo .csv MatrizPCA:
            
            Directory.CreateDirectory(_caminhoComONomeDoArquivoCSVFinal); // crie-o

            CultureInfo culture = CultureInfo.GetCultureInfo("pt-BR");
            CsvConfiguration config = new(culture);
            config.HasHeaderRecord = false;

            valoresDoArquivoMatrizPCA = new List<double>();
            List<string> arquivos = [];
            List<double[]> Valores = []; // uma lista de arrays

            for (int i = 1; i <= maiorNumeracaoNoNomeDoCsv; i++)
            {
               string arq = $"{_caminhoDaPastaDosArquivosCSVPosTratamento}Rom{i}.csv";

                if (!File.Exists(arq))
                        continue;

               using (var csv = new CsvReader(new StreamReader(arq), config))
               {
                  double[] records = csv.GetRecords<double>().ToArray();
                  Valores.Add(records);
                  arquivos.Add(Path.GetFileName(arq));
               }
            }

            using (var csvMatriz = new StreamWriter(Path.Combine(_caminhoComONomeDoArquivoCSVFinal, nomeArquivoCsv)))
            {
               StringBuilder sb = new();

               foreach (var a in arquivos)
                  sb.Append(a + ";");

               csvMatriz.WriteLine(sb); // inserindo os nomes dos arquivos .csv no topo de cada coluna (cabeçalhos)

               for (int i = 0; i < _linhas; i++)
               {
                 sb.Clear(); // limpando o objeto para poder utilizá-lo de novo, mas para escrever novo valor
                 foreach (var a in Valores)
                 {
                      try
                      {
                        sb.Append(a[i].ToString().Replace(".", ",") + ";");
                      }
                      catch { }
                 }
                 csvMatriz.WriteLine(sb);
               }

               foreach (double[] array in Valores) // lendo cada array presente em uma lista
               {
                  for (int i = 0; i < array.Length; i++) // percorrendo as posições dentro desse array
                  {
                     double valor = array[i]; // obtendo cada valor em cada posição do array
                       valoresDoArquivoMatrizPCA.Add(valor); // adicionando esses valores em uma lista
                  }
               }
            }
        }

        public static string[,] ObterTransposta(string[,] matriz)
        {
            int linhas = matriz.GetLength(0);
            int colunas = matriz.GetLength(1);

            string[,] transposta = new string[colunas, linhas];

            if (linhas == colunas)
            {
                transposta = (string[,])matriz.Clone();
                for (int i = 1; i < linhas; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        string temp = transposta[i, j];
                        transposta[i, j] = transposta[j, i];
                        transposta[j, i] = temp;
                    }
                }
            }
            else
            {
                for (int j = 0; j < colunas; j++)
                {
                    for (int i = 0; i < linhas; i++)
                    {
                        transposta[j,i] = matriz[i, j];
                    }
                }
            }
            return transposta;
        }

        public static void PCA()
        {
            List<double> elementosDaPrimeiraColuna = new List<double>();
            List<double> elementosDaSegundaColuna = new List<double>();

            int linhas = MatrizMedias.GetLength(0);
            int colunas = MatrizMedias.GetLength(1);

            string[,] matrizMediasString = new string[linhas, colunas];

            for (int i = 0;i < linhas; i++)
            {
                for(int j = 0;j < colunas; j++)
                {
                    matrizMediasString[i, j] = MatrizMedias[i,j].ToString("E5"); // transformando os números em Notação Científica
                }
            }

            string[,] matrizTransposta = ObterTransposta(matrizMediasString); // trazendo a matriz transposta para este método (PCA)
            string[][] matrizTrspstaJagged = ConverterParaArrayJagged(matrizTransposta); // convertendo a matriz bidimensional para um array de array (array jagged)

            // configurando a classe (e a sua instância) para fazer o cálculo de PCA
            PrincipalComponentAnalysis pca = new PrincipalComponentAnalysis()
            {
                Method = PrincipalComponentMethod.Center,
                Whiten = false
            };

           pca.Learn(matrizTrspstaJagged.ToDouble()); // ajuste dos dados a serem manipulados

           double[][] componentes = pca.Transform(matrizTrspstaJagged.ToDouble());
           
           double[][] dadosNormalizados = NormalizeData(componentes);
             
           // obtendo os números presentes na coluna 0
           for (int i = 0; i < dadosNormalizados.Length; i++)
                elementosDaPrimeiraColuna.Add(dadosNormalizados[i][0]);

           // obtendo os números presentes na coluna 1
           for (int j = 0; j < dadosNormalizados.Length; j++)
                elementosDaSegundaColuna.Add((dadosNormalizados[j][1]) * (-1)); // multiplicando todos esses números por -1 

            PCA_grafico pcaGrafico = new PCA_grafico();
            pcaGrafico.formsPlot1_Load(elementosDaPrimeiraColuna, elementosDaSegundaColuna);

            pcaGrafico.Show();

        }

        // transforma os dados para que todos os valores estejam em uma escala entre 0 e 1:
        public static double[][] NormalizeData(double[][] componentes)
        {
            // Encontrar o valor mínimo e máximo no array de arrays
            double min = componentes.SelectMany(innerArray => innerArray).Min();
            double max = componentes.SelectMany(innerArray => innerArray).Max();

            // Aplicar a normalização a cada valor no array de arrays
            return componentes.Select(innerArray =>
                innerArray.Select(value => (value - min) / (max - min)).ToArray()
            ).ToArray();
        } 

        // método para conversão de array bidimensional para um array de arrays:
        public static string[][] ConverterParaArrayJagged(string[,] matrizTransposta)
        {
            int linhas = matrizTransposta.GetLength(0); // obtendo a quantidade de linhas da matrizTransposta
            int colunas = matrizTransposta.GetLength(1);

            string[][] matrizTrspstaJagged = new string[linhas][]; // criando uma matriz jagged de mesma quantidade de linhas da matrizTransposta

            for (int i = 0; i < linhas; i++) // percorrendo a quantidade de linhas 
            {
                string[] valores = new string[colunas];

                matrizTrspstaJagged[i] = new string[colunas];

                for (int j = 0; j < colunas; j++)
                {
                    valores[j] = matrizTransposta[i, j]; // inserindo os valores na matriz jagged
                    matrizTrspstaJagged[i] = valores; // em cada linha da matrizTrspstaJagged, está sendo inserida as linhas da matrizTransposta
                }
            }
            return matrizTrspstaJagged;
        }
        #endregion Metodos  
    }
}
