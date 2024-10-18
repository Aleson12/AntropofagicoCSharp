using Accord;
using Accord.Math;
using Accord.Statistics.Analysis;
using Accord.Statistics.Kernels;
using AntropofagicoCSharp.Forms;
using CsvHelper;
using CsvHelper.Configuration;
using MathNet.Numerics.LinearAlgebra.Double;
using Plotly.NET.ConfigObjects;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using AntropofagicoCSharp;
using System.Linq;

namespace AntropofagicoCSharp
{
    public class Arquivo
    {
        #region Propriedades

        private static List<string> arquivosTxtsDaPasta;
        private static List<string> caminhosDosArquivosTxtDaPasta;
        private static List<string> arquivosTxtsDaPastaOrdenados;
        private static List<string> arquivosAgrupados;
        private static List<double> valoresDoArquivoMatrizPCA;
        private static double[,] mediaDosValoresDaMatriz;
        //private static List<double> mediaDosValoresDaMatriz;
        private static List<double[]> todasAsColunasDeMatrizFinal = []; // uma lista de arrays
        public static List<MatrizRelCSV> listaMatrizRelCSV = new List<MatrizRelCSV>();
        public static List<string> valoresEixoX = new List<string>();
        public static List<string> valoresEixoY = new List<string>();

        private static double[,] matrizMedias; // Matriz com todas as médias dos valores
        private static readonly int _linhas = 2048;
        private static bool _validaPrimeiroCaso = false; // variável no escopo da classe vira campo/atributo
        public static string _caminhoDaPastaDosArquivosCSVPosTratamento; // membro da classe definido como "público" para ser possível acessá-lo na classe principal da Interface
        public static string _caminhoDaPastaComOsArquivosTXTsAgrupados;
        public static string _caminhosCsv;
        public static string _caminhoComONomeDoArquivoCSVFinal;
        public static int qtdLinhasEmMatrizFinal;
        private static double[] varianciaExplicada;
        private static int colunaAtual = 0;
        public static string[] valores;

        public static string C1;
        public static string C2;

        #endregion Propriedades

        #region Métodos

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
            string[] valores;

            int divisorParaMediaEQuantidadeDeColunasDaMatriz = arquivosAgrupados.Count;

            int colunas = divisorParaMediaEQuantidadeDeColunasDaMatriz * 2; // a quantidade de colunas da Matriz será a igual ao tamanho da Lista_dos_arquivos_agrupados
            string nomeDoArquivoCsv;
            int colunaDaMatriz = 0;

            // criação de matriz com 2048 linhas e "n" colunas
            double[,] matriz = new double[_linhas, colunas];

            for (int i = 0; i < arquivosAgrupados.Count; i++)
            {
                nomeDoArquivoCsv = arquivosAgrupados.First().Split("-")[0];
                valores = File.ReadAllLines(FrmPrincipal.diretorio + "\\" + arquivosAgrupados[i] + ".txt".ToString()); // o conteúdo de cada arquivo .txt lido (primeira e segunda colunas) é inserido nesse array de strings chamado "valores"

                valores.ToList();

                // obtendo os valores dos três arquivos .txt que são manipulados:
                for (int j = 0; j < valores.Length; j++)
                {
                    if (!double.TryParse(valores[j].Split(";")[1].Trim(), out matriz[j, i])) // obtendo os valores da coluna à direita de cada arquivo .txt (que vai ser o eixo Y do gráfico logarítmico)
                        matriz[j, i] = double.MinValue; // Caso o parse gere uma exceção
                    if (!double.TryParse(valores[j].Split(";")[0].Trim(), out matriz[j, i + arquivosAgrupados.Count])) // obtendo os valores da coluna à esquerda de cada arquivo .txt (que vai ser o eixo X do gráfico logarítmico)
                        matriz[j, i] = double.MinValue; // Caso o parse gere uma exceção
                }
            }

            nomeDoArquivoCsv = arquivosAgrupados.First().Split("-")[0];

            GerarSomenteUmArquivoPorClasse(matriz, nomeDoArquivoCsv); // passando a matriz e o nome de cada arquivo CSV como parâmetros para este método para que ele seja capaz de manipulá-los

            colunaDaMatriz += 1;
        }
        #endregion ProcessamentoDosTxtsAgrupados

        #region ObterMedias
        private static double[,] ObterMedias(double[,] matriz)
        {
            int linhas = matriz.GetLength(0); // obtendo a quantidade de linhas
            int colunas = arquivosAgrupados.Count; // obtendo a quantidade de colunas (3)

            double[,] medias = new double[linhas, 2];

            for (int i = 0; i < linhas; i++)
            {
                double soma = 0d;

                // Percorrer apenas as colunas de índice 3, 4 e 5
                for (int j = 3; j <= 5; j++)
                    soma += matriz[i, j];

                double mediaColunasEsquerdaArquivosTXT = soma > 0 ? (soma / 3) : 0; // média das três colunas

                for (int k = 0; k <= 2; k++)
                    soma += matriz[i, k];

                double mediaColunasDireitaArquivosTXT = soma > 0 ? (soma / 3) : 0;

                //var resultadoArredondado = Math.Round(media, 5); // arredondando o resultado da divisão para cinco casas decimais
                medias[i, 0] = mediaColunasEsquerdaArquivosTXT;
                medias[i, 1] = mediaColunasDireitaArquivosTXT;
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

            for (int i = 0; i < mediaDosValoresDaMatriz.GetLength(0); i++) // percorrendo a quantidade de linhas em "mediaDosValoresDaMatriz" (2048)
                writer.WriteLine($"{mediaDosValoresDaMatriz[i, 0]};{mediaDosValoresDaMatriz[i,1]}"); // obtendo os valores na coluna 0 e escrevendo na coluna 0 do arquivo .csv; obtendo os valores na coluna 1 e escrevendo na coluna 1 do arquivo .csv

            // criada uma nova variável que irá receber cada valor da variável "caminhoComNomeDoCsv" e 
            // concatenar com uma quebra de linha
            _caminhosCsv += caminhoComNomeDoCsv + '\n';

            GerandoMatrizMedias(mediaDosValoresDaMatriz);
        }
        #endregion GerarSomenteUmArquivoPorClasse

        #region GerandoMatrizMedias
        public static void GerandoMatrizMedias(double[,] mediaDosValoresDaMatriz)
        {
            if (Directory.Exists(_caminhoDaPastaDosArquivosCSVPosTratamento)) // se a pasta com os arquivos .csv's existe, 
            {
                List<double> mediaDosValoresDoEixoYDaMatrizEmLista = new List<double>();
                List<double> mediaDosValoresDaMatrizEmLista = mediaDosValoresDaMatriz.Cast<double>().ToList();

                for (int i = 1; i < mediaDosValoresDaMatrizEmLista.Count; i += 2)
                    mediaDosValoresDoEixoYDaMatrizEmLista.Add(mediaDosValoresDaMatrizEmLista[i]);              

                string[] arquivosCsv = Directory.GetFiles(_caminhoDaPastaDosArquivosCSVPosTratamento); // extrair dessa pasta os arquivos contidos nela e inserir no array unidimensional "arquivosCsv";

                int numeroDeLinhas = mediaDosValoresDaMatriz.GetLength(0); // obtendo o número de valores total em "mediaDosValoresDaMatriz" (uma lista);
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
                    if (indiceValor < mediaDosValoresDoEixoYDaMatrizEmLista.Count)
                    {
                        matrizMedias[linha, colunaAtual] = mediaDosValoresDoEixoYDaMatrizEmLista[indiceValor];
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

                using (var csv = new CsvReader(new StreamReader(arq), config)) // inicializando o leitor e "escritor" de arquivo .csv
                {
                    // Mapeia os registros CSV para a classe CsvRecord
                    var registros = csv.GetRecords<CsvRecord>().ToList(); 

                    // Extrai apenas os valores da Coluna1
                    double[] valoresColuna1 = registros.Select(registro => registro.Coluna1).ToArray();

                    // Adiciona os valores da coluna 1 à lista
                    todasAsColunasDeMatrizFinal.Add(valoresColuna1);

                    // Cria e adiciona um objeto da classe MatrizRelCSV com os valores da coluna 1 e o nome do arquivo
                    listaMatrizRelCSV.Add(new MatrizRelCSV { ValoresInternosCSV = valoresColuna1.ToList(), NomeArqCSV = Path.GetFileName(arq) });

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

            var pca = new Accord.Statistics.Analysis.PrincipalComponentAnalysis()
            {
                Method = PrincipalComponentMethod.Center
            };

            pca.Learn(matrizTranspostaJaggedArray);

            var aprendizadoDePCA = pca.Learn(matrizTranspostaJaggedArray); // ajustando os valores de PCA

            double[][] componentes = pca.Transform(matrizTranspostaJaggedArray); // aplicando uma Transformação à Matriz Transposta

            MatrizTransformadaJaggedEmBidimensional(componentes, pca);
            PercentualDeCadaComponente(pca);
        }
        #endregion TransformacaoDeMatrizTransposta

        #region MatrizTransformadaEmJaggedEmBidimensional
        public static void MatrizTransformadaJaggedEmBidimensional(double[][] componentes, Accord.Statistics.Analysis.PrincipalComponentAnalysis pca)
        {
            int numLinhas = componentes.Length;
            double[,] arrayComponentesBidimensional = new double[numLinhas, 2];

            for (int i = 0; i < numLinhas; i++)
            {
                arrayComponentesBidimensional[i, 0] = componentes[i][0];
                arrayComponentesBidimensional[i, 1] = componentes[i][1];
            }

            NormalizarDados(arrayComponentesBidimensional, pca);
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

            AutosValoresEVetores(matrizResultante);
            return matrizResultante;
        }
        #endregion ProdutoDasMatrizesTranspostaECovariância

        #region AutoValores e AutoVetores

        public static void AutosValoresEVetores(double[,] produtoMatrizCovariancia)
        {
            // Convertendo a matriz resultante para um formato compatível com Math.NET:
            var matriz = DenseMatrix.OfArray(produtoMatrizCovariancia);

            // Calculando autovalores e autovetores:
            var evd = matriz.Evd();
            var autoValores = evd.EigenValues;
            var autoVetores = evd.EigenVectors;

            // Autovetores:

            List<System.Numerics.Complex> ListaDeAutoVetores = new List<System.Numerics.Complex>();

            for (int i = 0; i < autoVetores.ColumnCount; i++)
            {
                var autoVetor = autoVetores.Column(i);
                foreach (var item in autoVetor)
                    ListaDeAutoVetores.Add(item);
            }

            AutoVetoresEmReal(ListaDeAutoVetores);

            List<System.Numerics.Complex> ListaDosAutoValoresInvertidos = new List<System.Numerics.Complex>();

            var listaDeAutoValores = autoValores.ToList(); // transformando autoValores em uma lista
            listaDeAutoValores.Reverse(); // invertendo a lista de autoValores

            foreach (System.Numerics.Complex i in listaDeAutoValores)
                ListaDosAutoValoresInvertidos.Add(i);

            AutoValoresEmReal(ListaDosAutoValoresInvertidos);

        }
        #endregion AutoValores e AutoVetores

        #region AutoValoresEmNumeraçãoReal

        public static void AutoValoresEmReal(List<System.Numerics.Complex> ListaDosAutoValoresInvertidos)
        {
            List<double> autoValoresInvertidosEmReal = new List<double>();

            foreach (System.Numerics.Complex valor in ListaDosAutoValoresInvertidos) // listaInvertidaDeAutoValores
                autoValoresInvertidosEmReal.Add(valor.Real);

            double totalVariance = autoValoresInvertidosEmReal.Sum();
            double[] razãoDeVarianciaExplicada = autoValoresInvertidosEmReal.Select(e => e / totalVariance).ToArray();

        }

        #endregion AutoValoresEmNumeraçãoReal

        #region AutoVetoresEmNumeraçãoReal

        public static void AutoVetoresEmReal(List<System.Numerics.Complex> ListaDeAutoVetores)
        {
            List<double> autoVetoresEmReal = new List<double>();

            foreach (System.Numerics.Complex valor in ListaDeAutoVetores)
                autoVetoresEmReal.Add(valor.Real);

            TransformandoArrayUniEmBiDim(autoVetoresEmReal);

        }

        #endregion AutoVetoresEmNumeraçãoReal

        #region PercentualDeCadaComponente

        public static void PercentualDeCadaComponente(Accord.Statistics.Analysis.PrincipalComponentAnalysis pca)
        {

            double[] autovalores = pca.Eigenvalues; // obtendo os autovalores
            double varianciaTotal = autovalores.Sum(); // somando todos os autovalores
            double[] varianciaExplicada = autovalores.Select(e => e / varianciaTotal).ToArray();

            C1 = (Math.Round(varianciaExplicada[0] * 100).ToString() + "%"); // porcentagem do primeiro componente
            C2 = (Math.Round(varianciaExplicada[1] * 100).ToString() + "%"); // porcentagem do segundo componente

        }
        #endregion PercentualDeCadaComponente

        #region transformandoArrayUnidimensionalEmBi

        public static void TransformandoArrayUniEmBiDim(List<double> autoVetoresEmReal)
        {
            // convertendo de double para int:
            int linhas = Convert.ToInt32(Math.Sqrt(autoVetoresEmReal.Count));
            int colunas = Convert.ToInt32(Math.Sqrt(autoVetoresEmReal.Count));

            double[,] autoVetoresRealArrayBidimensional = new double[linhas, colunas];

            int indice = 0;

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                    autoVetoresRealArrayBidimensional[i, j] = autoVetoresEmReal[indice++]; // preenchendo a matriz (array bidimensional)
            }
            ProdutoDeMatrizes(matrizMedias, autoVetoresRealArrayBidimensional);
        }

        #endregion transformandoArrayUnidimensionalEmBi

        #region produtoDeMatrizes

        public static void ProdutoDeMatrizes(double[,] matrizMedias, double[,] autoVetoresRealArrayBidimensional)
        {
            double[,] resultado = new double[matrizMedias.GetLength(0), autoVetoresRealArrayBidimensional.GetLength(1)];

            if (matrizMedias.GetLength(1) == autoVetoresRealArrayBidimensional.GetLength(0)) // condição necessária para multiplicar matrizes
            {
                // Realizando a multiplicação das matrizes:
                for (int i = 0; i < matrizMedias.GetLength(0); i++)                
                    for (int j = 0; j < autoVetoresRealArrayBidimensional.GetLength(1); j++)                    
                        for (int k = 0; k < autoVetoresRealArrayBidimensional.GetLength(0); k++)
                            resultado[i, j] += matrizMedias[i, k] * autoVetoresRealArrayBidimensional[k, j]; 
            }
        }

        #endregion produtoDeMatrizes*/

        #region NormalizaçãoDosDados
        // normalização dos dados (deixando-os em uma faixa numérica entre 0 e 1), preparando-os para a plotagem no gráfico:
        public static double[,] NormalizarDados(double[,] arrayComponentesBidimensional, Accord.Statistics.Analysis.PrincipalComponentAnalysis pca)
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

            PlotagemGraficoPCA(arrayNormalizado, pca);

            return arrayNormalizado;

        }
        #endregion NormalizaçãoDosDados

        #region PlotagemDoGrafico
        public static void PlotagemGraficoPCA(double[,] arrayNormalizado, Accord.Statistics.Analysis.PrincipalComponentAnalysis pca)
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

            pcaGrafico.BringToFront(); // traz o formulário atual para frente, sobrepondo outros que estiverem na frente
            pcaGrafico.AtualizarGrafico(x, y); // plota os pontos no gráfico, efetivamente.
            PercentualDeCadaComponente(pca);
            pcaGrafico.AtualizaLabel(C1, C2);
        }

        #endregion PlotagemDoGrafico

        #endregion Métodos
    }
}
