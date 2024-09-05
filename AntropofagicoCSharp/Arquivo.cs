using Accord.Math;
using AntropofagicoCSharp.Forms;
using CsvHelper;
using CsvHelper.Configuration;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Data;
using Accord.Statistics.Analysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Accord.Statistics.Kernels;

namespace AntropofagicoCSharp
{
    public static class Arquivo
    {
        #region propriedades
        private static List<string> arquivosTxtsDaPasta;
        private static List<string> caminhosDosArquivosTxtDaPasta;
        private static List<string> arquivosTxtsDaPastaOrdenados;
        private static List<string> arquivosAgrupados;
        private static List<double> valoresDoArquivoMatrizPCA;
        private static List<double> mediaDosValoresDaMatriz;
        private static List<double[]> todasAsColunasDeMatrizFinal = []; // uma lista de arrays
        public static List<MatrizRelCSV> listaMatrizRelCSV = new List<MatrizRelCSV>();

        private static double[,] matrizMedias; // Matriz com todas as médias dos valores
        private static readonly int _linhas = 2048;
        private static bool _validaPrimeiroCaso = false; // variável no escopo da classe vira campo/atributo
        public static string _caminhoDaPastaDosArquivosCSVPosTratamento; // membro da classe definido como "público" para ser possível acessá-lo na classe principal da Interface
        public static string _caminhosCsv;
        public static string _caminhoComONomeDoArquivoCSVFinal;
        public static int qtdLinhasEmMatrizFinal;
        private static int colunaAtual = 0;

        #endregion propriedades

        #region Metodos

        #region FiltrarArquivosTxts
        public static List<string> FiltrarArquivosTxt(string diretorio)
        {
            caminhosDosArquivosTxtDaPasta = new List<string>();

            caminhosDosArquivosTxtDaPasta = Directory.GetFiles(diretorio)
                        // Filtra apenas os arquivos com extensão ".txt"
                        .Where(arquivo => Path.GetExtension(arquivo) == ".txt").ToList();
            return caminhosDosArquivosTxtDaPasta;
        }
        #endregion FiltrarArquivosTxts

        #region AgrupandoOsTxtsPorClasse
        public static void AgrupandoOsTxtsPorClasse()
        {
            arquivosTxtsDaPasta = new List<string>();
            arquivosTxtsDaPastaOrdenados = new List<string>();
            arquivosAgrupados = new List<string>();

            string nomeComTipo = string.Empty; // nome do arquivo
            string numeroPosHifen = string.Empty; // número após o hífen (sem a extensão do arquivo)
            string comparaNome = string.Empty;
            object MatrizVAR = new object[_linhas, caminhosDosArquivosTxtDaPasta.Count];
            // extraindo apenas o nome do arquivo .txt (sem a extensão e o seu caminho de diretório) 
            caminhosDosArquivosTxtDaPasta.ToList().ForEach(caminho =>
            {
                if (Path.GetDirectoryName(caminho) != " ")
                    arquivosTxtsDaPasta.Add(Path.GetFileNameWithoutExtension(caminho)); // pega o arquivo sem a extensão
            });

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
                        ProcessamentoDosTxtsAgrupados();
                    arquivosAgrupados.Clear(); // limpa a lista

                    if (!_validaPrimeiroCaso)
                        arquivosAgrupados.Add(arquivoOrdenado);
                }
                else if (nomeComTipo == comparaNome)
                    arquivosAgrupados.Add(arquivoOrdenado);
            });
        }
        #endregion AgrupandoOsTxtsPorClasse

        #region ProcessamentoDosTxtsAgrupados
        private static void ProcessamentoDosTxtsAgrupados()
        {

            int divisorParaMediaEQuantidadeDeColunasDaMatriz = arquivosAgrupados.Count;

            int colunas = divisorParaMediaEQuantidadeDeColunasDaMatriz; // a quantidade de colunas da Matriz será a igual ao tamanho da Lista_dos_arquivos_agrupados
            string nomeDoArquivoCsv;
            int colunaDaMatriz = 0;

            // criação de matriz com 2048 linhas e "n" colunas
            double[,] matriz = new double[_linhas, colunas];

            for (int i = 0; i < colunas; i++)
            {
                string[] valores = File.ReadAllLines(FrmPrincipal.diretorio + "\\" + arquivosAgrupados[i] + ".txt".ToString());
                for (int j = 0; j < valores.Length; j++)
                    if (!double.TryParse(valores[j].Split(";")[1].Trim(), out matriz[j, i]))
                        matriz[j, i] = double.MinValue; // Caso o parse gere uma exceção
            }
            nomeDoArquivoCsv = arquivosAgrupados.First().Split("-")[0];

            GerarSomenteUmArquivoPorClasse(matriz, nomeDoArquivoCsv); // passando a matriz e o nome de cada arquivo CSV como parâmetros para este método para que ele seja capaz de manipulá-los
            colunaDaMatriz += 1;
        }
        #endregion ProcessamentoDosTxtsAgrupados

        #region ObterMedias
        private static List<double> ObterMedias(double[,] matriz)
        {
            int linhas = matriz.GetLength(0); // obtendo a quantidade de linhas
            int colunas = matriz.GetLength(1); // obtendo a quantidade de colunas

            List<double> medias = [];

            for (int i = 0; i < linhas; i++)
            {
                double soma = 0d;

                for (int j = 0; j < colunas; j++)
                    soma += matriz[i, j];
                double media;

                if (soma > 0)
                    media = (soma / colunas);
                else
                    media = 0;

                var resultadoArredondado = Math.Round(media, 5); // arredondando o resultado da divisão para cinco casas decimais
                medias.Add(resultadoArredondado);
            }
            return medias;
        }
        #endregion ObterMedias

        #region GerarSomenteUmArquivoPorClasse
        private static void GerarSomenteUmArquivoPorClasse(double[,] matriz, string nomeDoArquivoCsv)
        {
            mediaDosValoresDaMatriz = ObterMedias(matriz);
            string caminhoComNomeDoCsv;
            int indiceDoCSV = int.Parse(nomeDoArquivoCsv.Split("-")[0].Substring(3)) - 1;

            _caminhoDaPastaDosArquivosCSVPosTratamento = Path.Combine($"{FrmPrincipal.diretorio}\\Roms\\");
            Directory.CreateDirectory(_caminhoDaPastaDosArquivosCSVPosTratamento); // cria a pasta no sistema de arquivos

            caminhoComNomeDoCsv = Path.Combine($"{_caminhoDaPastaDosArquivosCSVPosTratamento}{nomeDoArquivoCsv}.csv"); // criando o caminho onde está o arquivo csv para ser escrito

            // criando o arquivo .csv, acessando-o, abrindo-o e escrevendo nele os novos valores
            using (StreamWriter writer = new StreamWriter(caminhoComNomeDoCsv))

                foreach (double vlr in mediaDosValoresDaMatriz)
                    writer.WriteLine($"{vlr}".Replace('.', ','));

            // criada uma nova variável que irá receber cada valor da variável "caminhoComNomeDoCsv" e 
            // concatenar com uma quebra de linha
            _caminhosCsv += caminhoComNomeDoCsv + '\n';

            GerandoMatrizMedias();
        }
        #endregion GerarSomenteUmArquivoPorClasse

        #region GerandoMatrizMedias
        public static void GerandoMatrizMedias()
        {
            if (Directory.Exists(_caminhoDaPastaDosArquivosCSVPosTratamento)) // se a pasta com os arquivos .csv's existe, 
            {
                string[] arquivosCsv = Directory.GetFiles(_caminhoDaPastaDosArquivosCSVPosTratamento); // extrair dessa pasta os arquivos contidos nela e inserir no array unidimensional "arquivosCsv";

                int numeroDeLinhas = mediaDosValoresDaMatriz.Count; // obtendo o número de valores total em "mediaDosValoresDaMatriz" (uma lista);
                int numeroDeColunas = arquivosCsv.Length; // obtendo a quantidade total de arquivos contidos no array "arquivosCsv";

                // Inicializa ou redimensiona a matriz se necessário
                if (matrizMedias == null) // se a matrizMedias ainda não tiver sido criada, 
                    matrizMedias = new double[numeroDeLinhas, numeroDeColunas]; // crie-a

                else
                {
                    if (matrizMedias.GetLength(0) != numeroDeLinhas || matrizMedias.GetLength(1) != numeroDeColunas) // se as dimensões estiverem erradas, 
                    {
                        matrizMedias = new double[numeroDeLinhas, numeroDeColunas]; // crie-a com as dimensões certas
                        colunaAtual = 0; // Redefine a coluna atual ao redimensionar a matriz
                    }
                }

                int indiceValor = 0;

                // Preenche a coluna atual com os valores;
                // desta forma, a cada nova lista de valores em "mediaDosValoresDaMatriz", o laço de repetição irá para a próxima coluna:
                for (int linha = 0; linha < numeroDeLinhas; linha++)
                {
                    if (indiceValor < mediaDosValoresDaMatriz.Count)
                    {
                        matrizMedias[linha, colunaAtual] = mediaDosValoresDaMatriz[indiceValor];
                        indiceValor++;
                    }
                    else
                        matrizMedias[linha, colunaAtual] = 0;
                }

                // Incrementa para a próxima coluna
                colunaAtual++;

                // Limpa a lista para o próximo uso
                mediaDosValoresDaMatriz.Clear();
            }

            // matrizMedias se refere ao arquivo .csv "MatrizPCA"

        }
        #endregion GerandoMatrizMedias

        #region GeraMatrizFinal
        public static void GeraMatrizFinal()
        {
            valoresDoArquivoMatrizPCA = new List<double>();
            List<string> arquivos = [];

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

            _caminhoComONomeDoArquivoCSVFinal = Path.Combine($"{FrmPrincipal.diretorio}\\MatrizFinal\\"); // criando novo caminho de diretório
            string nomeArquivoCsv = "MatrizPCA.csv";

            Directory.CreateDirectory(_caminhoComONomeDoArquivoCSVFinal); // crie-o

            CultureInfo culture = CultureInfo.GetCultureInfo("pt-BR"); // define configurações de formatação de números, datas, moedas, etc... em PT-BR.
            CsvConfiguration config = new(culture); // passando a configuração definida para o objeto "config", por meio do qual, a configuração do csv será aplicada
            config.HasHeaderRecord = false; // informando que o arquivo .csv não possui cabeçalho.

            for (int i = 1; i <= maiorNumeracaoNoNomeDoCsv; i++)
            {
                string arq = $"{_caminhoDaPastaDosArquivosCSVPosTratamento}Rom{i}.csv";

                if (!File.Exists(arq))
                    continue;

                using (var csv = new CsvReader(new StreamReader(arq), config))
                {
                    double[] records = csv.GetRecords<double>().ToArray(); // obtendo os valores de cada arquivo .csv, inserindo-os num array unidimensional
                    todasAsColunasDeMatrizFinal.Add(records); // e, em seguida, inserindo esse array de valores em uma lista

                    // para cada arquivo, é instanciado um Objeto da classe MatrizRelCSV, com seus valores e nome de arquivo, e, após,
                    // inserido na lista "listaMatrizRelCSV":

                    listaMatrizRelCSV.Add(new MatrizRelCSV {ValoresInternosCSV = records.ToList(), NomeArqCSV = Path.GetFileName(arq)});

                    arquivos.Add(Path.GetFileName(arq));
                               
                }
            }

            // Criação do arquivo .csv MatrizPCA:

            using (var csvMatriz = new StreamWriter(Path.Combine(_caminhoComONomeDoArquivoCSVFinal, nomeArquivoCsv)))
            {
                StringBuilder sb = new();

                foreach (var a in arquivos)
                    sb.Append(a + ";");

                csvMatriz.WriteLine(sb); // inserindo os nomes dos arquivos .csv no topo de cada coluna (cabeçalhos)

                for (int i = 0; i < _linhas; i++)
                {
                    sb.Clear(); // limpando o objeto para poder utilizá-lo de novo, mas para escrever novo valor
                    foreach (var a in todasAsColunasDeMatrizFinal)
                    {
                        try
                        {
                            sb.Append(a[i].ToString().Replace(".", ",") + ";");
                        }
                        catch { }
                    }
                    csvMatriz.WriteLine(sb);
                }
            }
        }
        #endregion GeraMatrizFinal

        #region PCA
        public static void PCA()
        {

            int numLinhas = matrizMedias.GetLength(0); // 2048
            int numColunas = matrizMedias.GetLength(1); // 113

            double[] media = new double[numColunas];
            double[] somaColunas = new double[numColunas];

            // Percorre cada coluna da matriz
            for (int coluna = 0; coluna < numColunas; coluna++)
            {
                double soma = 0;

                // Soma os valores de cada linha na coluna atual
                for (int linha = 0; linha < numLinhas; linha++)
                    
                    soma += matrizMedias[linha, coluna];

                // Armazena a soma da coluna no array unidimensional
                somaColunas[coluna] = soma;

                media[coluna] = (soma / numColunas);  // média de cada coluna
            }

            double[,] matrizCovariancia = CalcularMatrizCovariancia(matrizMedias, media);
            MatrizTransposta(numLinhas, numColunas, matrizMedias);

        }
        #endregion PCA

        #region MatrizCovariancia
        public static double[,] CalcularMatrizCovariancia(double[,] matrizMedias, double[] media)
        {
            int linhas = matrizMedias.GetLength(0);
            int colunas = matrizMedias.GetLength(1);

            double[,] matrizCovariancia = new double[linhas, colunas];

            for (int i = 0; i < colunas; i++)
                for (int j = 0; j < linhas; j++)
                    matrizCovariancia[j, i] = matrizMedias[j, i] - media[i];

            return matrizCovariancia;
        }
        #endregion MatrizCovariancia

        #region TransposiçãoDeMatriz
        public static double[,] MatrizTransposta(int numLinhas, int numColunas, double[,] matrizMedias)
        {
            double[,] matrizTranspostaArray = new double[numColunas, numLinhas]; // declaração da Matriz para Transposição

            var matriz = DenseMatrix.OfArray(matrizMedias); // cria uma nova instância de DenseMatrix usando os valores do array matrizCovariancia
            var matrizTransposta = matriz.Transpose(); // transpondo a MatrizCovariancia
            matrizTranspostaArray = matrizTransposta.ToArray(); // transformando a Matriz de Covariância Transposta em um Array (bidimensional)

            double escalar = 1.0 / (matrizMedias.GetLength(0) - 1);

            double[,] produtoMatrizCovariancia = ProdutoDeTranspostaECovarianciaMatrizes(matrizTranspostaArray, matrizMedias, escalar);

            TransformacaoDeMatrizTransposta(matrizTranspostaArray);

            return matrizTranspostaArray;

        }

        #endregion TransposiçãoDeMatriz

        #region TransformacaoDeMatrizTransposta
        public static void TransformacaoDeMatrizTransposta(double[,] matrizTranspostaArray)
        {
            double[][] matrizTranspostaJaggedArray = matrizTranspostaArray.ToJagged();

            var pca = new PrincipalComponentAnalysis()
            {
                Method = PrincipalComponentMethod.Center
            };

            pca.Learn(matrizTranspostaJaggedArray); // ajustando os valores de PCA

            double[][] componentes = pca.Transform(matrizTranspostaJaggedArray); // aplicando uma Transformação à Matriz Transposta

            MatrizTransformadaJaggedEmBidimensional(componentes);
        }
        #endregion TransformacaoDeMatrizTransposta

        #region MatrizTransformadaEmJaggedEmBidimensional
        public static void MatrizTransformadaJaggedEmBidimensional(double[][] componentes)
        {
            int numLinhas = componentes.Length;
            double[,] arrayComponentesBidimensional = new double[numLinhas, 2];

            for (int i = 0; i < numLinhas; i++)
            {
                arrayComponentesBidimensional[i, 0] = componentes[i][0];
                arrayComponentesBidimensional[i, 1] = componentes[i][1];
            }

            NormalizarDados(arrayComponentesBidimensional);
        }
        #endregion MatrizTransformadaJaggedEmBidimensional

        #region ProdutoDasMatrizesTranspostaECovariância
        // Função para multiplicar duas matrizes (Transposta e Covariância):
        public static double[,] ProdutoDeTranspostaECovarianciaMatrizes(double[,] matrizTranspostaArray, double[,] matrizCovariancia, double escalar)
        {

            int linhasA = matrizTranspostaArray.GetLength(0);
            int colunasA = matrizTranspostaArray.GetLength(1);
            int linhasB = matrizCovariancia.GetLength(0);
            int colunasB = matrizCovariancia.GetLength(1);

            // Verifica se o número de colunas de A é igual ao número de linhas de B
            if (colunasA != linhasB)
                throw new InvalidOperationException("O número de colunas da matriz A deve ser igual ao número de linhas da matriz B.");

            // Criando a matriz resultante
            double[,] matrizResultante = new double[linhasA, colunasB];

            // Calculando o produto das matrizes
            for (int i = 0; i < linhasA; i++)
            {
                for (int j = 0; j < colunasB; j++)
                {
                    for (int k = 0; k < colunasA; k++)
                        matrizResultante[i, j] += matrizTranspostaArray[i, k] * matrizCovariancia[k, j];
                }
            }

            // Multiplicando a matriz resultante pelo escalar
            for (int i = 0; i < linhasA; i++)
            {
                for (int j = 0; j < colunasB; j++)
                    matrizResultante[i, j] *= escalar;
            }

            return matrizResultante;
        }
        #endregion ProdutoDasMatrizesTranspostaECovariância

        #region NormalizaçãoDosDados
        // normalização dos dados (deixando-os em uma faixa numérica entre 0 e 1), preparando-os para a plotagem no gráfico:
        public static double[,] NormalizarDados(double[,] arrayComponentesBidimensional)
        {
            double min = double.MaxValue;
            double max = double.MinValue;

            int linhas = arrayComponentesBidimensional.GetLength(0);
            int colunas = arrayComponentesBidimensional.GetLength(1);

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    if (arrayComponentesBidimensional[i, j] < min)
                        min = arrayComponentesBidimensional[i, j];
                    if (arrayComponentesBidimensional[i, j] > max)
                        max = arrayComponentesBidimensional[i, j];
                }
            }

            double[,] arrayNormalizado = new double[linhas, colunas];

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                    arrayNormalizado[i, j] = (arrayComponentesBidimensional[i, j] - min) / (max - min);
            }

            PlotagemGraficoPCA(arrayNormalizado);

            return arrayNormalizado;

        }
        #endregion NormalizaçãoDosDados

        #region PlotagemDoGrafico
        public static void PlotagemGraficoPCA(double[,] arrayNormalizado)
        {
            int numLinhas = arrayNormalizado.GetLength(0);

            double[] x = new double[numLinhas];
            double[] y = new double[numLinhas];

            for (int i = 0; i < numLinhas; i++)
            {
                x[i] = arrayNormalizado[i, 0]; // Primeira coluna
                y[i] = arrayNormalizado[i, 1]; // Segunda coluna
            }

            PCA_grafico pcaGrafico = new PCA_grafico();

            pcaGrafico.Text = "Análise de Componentes Principais (PCA)";
            pcaGrafico.Show(); // renderiza a interface do gráfico em si.
            pcaGrafico.AtualizarGrafico(x, y); // plota os pontos no gráfico, efetivamente.

        }

        #endregion PlotagemDoGrafico

        #endregion Metodos
    }
}
