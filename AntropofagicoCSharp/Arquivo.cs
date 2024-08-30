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
using Accord.Statistics;
using System;
using System;
using System.Linq;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Aspose.Words.Tables;

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

            //// Criação do arquivo .csv MatrizPCA:

            Directory.CreateDirectory(_caminhoComONomeDoArquivoCSVFinal); // crie-o

            CultureInfo culture = CultureInfo.GetCultureInfo("pt-BR");
            CsvConfiguration config = new(culture);
            config.HasHeaderRecord = false;

            for (int i = 1; i <= maiorNumeracaoNoNomeDoCsv; i++)
            {
                string arq = $"{_caminhoDaPastaDosArquivosCSVPosTratamento}Rom{i}.csv";

                if (!File.Exists(arq))
                    continue;

                using (var csv = new CsvReader(new StreamReader(arq), config))
                {
                    double[] records = csv.GetRecords<double>().ToArray();
                    todasAsColunasDeMatrizFinal.Add(records);
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
            for (int coluna = 0; coluna < matrizMedias.GetLength(1); coluna++)
            {
                double soma = 0;

                // Soma os valores de cada linha na coluna atual
                for (int linha = 0; linha < matrizMedias.GetLength(0); linha++)
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

            AutosValoresEVetores(produtoMatrizCovariancia);
            TransformacaoDeMatrizTransposta(matrizTranspostaArray);

            return matrizTranspostaArray;

        }

        #endregion TransposiçãoDeMatriz

        public static void TransformacaoDeMatrizTransposta(double[,] matrizTranspostaArray)
        {
            double[][] matrizTranspostaJaggedArray = matrizTranspostaArray.ToJagged();

            var pca = new PrincipalComponentAnalysis()
            {
                Method = PrincipalComponentMethod.Center
            };

            pca.Learn(matrizTranspostaJaggedArray); // ajustando os valores de PCA

            double[][] componentes = pca.Transform(matrizTranspostaJaggedArray); // aplicando uma Transformação à Matriz Transposta

            MatrizTransformadaEmJaggedEmBidimensional(componentes);

        }

        public static void MatrizTransformadaEmJaggedEmBidimensional(double[][] componentes)
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

            Variancia(autoValoresInvertidosEmReal);
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

        #region variância

        public static void Variancia(List<double> autoValoresInvertidosEmReal)
        {
            List<double> var_pcs = new List<double>();

            double soma = autoValoresInvertidosEmReal.Sum(); // "soma" contém o somatório de todos os valores em autoValoresInvertidosEmReal

            // porcentagem dos auto-valores em formato real:

            for (int i = 0; i < autoValoresInvertidosEmReal.Count; i++)
            {
                var result = ((autoValoresInvertidosEmReal[i] / soma) * 100);
                var_pcs.Add(result);
            }
        }

        #endregion variância

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

            if (matrizMedias.GetLength(1) == autoVetoresRealArrayBidimensional.GetLength(1))
            {
                for (int i = 0; i < matrizMedias.GetLength(0); i++)
                {
                    for (int j = 0; j < autoVetoresRealArrayBidimensional.GetLength(1); j++)
                    {
                        for (int k = 0; k < autoVetoresRealArrayBidimensional.GetLength(0); k++)
                            resultado[i, j] += matrizMedias[i, k] * autoVetoresRealArrayBidimensional[k, j];
                    }
                }
            }

            //      double[,] resultadoNormalizado = NormalizarDados(resultado);
            //  PlotagemGraficoPCA(resultado);
        }

        #endregion produtoDeMatrizes

        #region NormalizaçãoDosDados
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
            
         /*   double[] xs = new double[arrayNormalizado.GetLength(0)]; // Coordenadas x - Primeiro Componente Principal;
            double[] ys = new double[arrayNormalizado.GetLength(1)]; // Coordenadas y - Segundo Componente Principal

            for (int i = 0; i < arrayNormalizado.GetLength(0); i++)
            {
                xs[i] = arrayNormalizado[i, 0]; // Primeiro componente principal    

                for (int j = 0; j < arrayNormalizado.GetLength(1); j++)
                    ys[j] = arrayNormalizado[i, j]; // Segundo componente principal
         
            }*/

            PCA_grafico pcaGrafico = new PCA_grafico();

            pcaGrafico.Text = "Análise de Componentes Principais (PCA)";
            pcaGrafico.Show();
            pcaGrafico.AtualizarGrafico(x, y);
        }

        #endregion PlotagemDoGrafico

        #endregion Metodos
    }
}
