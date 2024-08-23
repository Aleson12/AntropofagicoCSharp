using Accord.Math;
using AntropofagicoCSharp.Forms;
using CsvHelper;
using CsvHelper.Configuration;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

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
        private static List<string> lista_nomeCSV;

        private static double[,] matrizMedias; // Matriz com todas as médias dos valores
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
                        ProcessamentoDosTxtsAgrupados();
                    arquivosAgrupados.Clear(); // limpa a lista

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
                if (int.TryParse(partesDoNomeDoArquivo[0].Substring(3), out int numero)) // o valor na quarta posição é transformado em inteiro e inserido na variável "numero"
                    return numero; // retorna o número de cada arquivo            
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

        private static void GerarSomenteUmArquivoPorClasse(double[,] matriz, string nomeDoArquivoCsv)
        {
            List<double> mediaDosValoresDaMatriz = new List<double>();

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

            if (Directory.Exists(_caminhoDaPastaDosArquivosCSVPosTratamento))
            {
                string[] arquivosCsv = Directory.GetFiles(_caminhoDaPastaDosArquivosCSVPosTratamento);

                int numeroDeLinhas = mediaDosValoresDaMatriz.Count;
                int numeroDeColunas = arquivosCsv.Length;   

                matrizMedias = new double[numeroDeLinhas, numeroDeColunas];

                int indiceValor = 0;

                for (int linha = 0; linha < numeroDeLinhas; linha++)
                {
                    for (int coluna = 0; coluna < numeroDeColunas; coluna++)
                    {
                        if (indiceValor < mediaDosValoresDaMatriz.Count)
                        {
                            matrizMedias[linha, coluna] = mediaDosValoresDaMatriz[indiceValor];
                            indiceValor++;
                        }
                        else
                            matrizMedias[linha, coluna] = 0;
                    }
                }
                mediaDosValoresDaMatriz.Clear();
            }
        }
         
        public static void GeraMatrizFinal()
        {
            List<string> arquivosDaPastaCsv = new List<string>();
            lista_nomeCSV = new List<string>();

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

            // percorrendo a lista de arquivos .csv e ordenando-os numericamente em ordem crescente:

            for (int num = 1; num <= maiorNumeracaoNoNomeDoCsv; num++)
            {
                if (File.Exists($"{_caminhoDaPastaDosArquivosCSVPosTratamento}\\Rom{num}.csv"))
                    lista_nomeCSV.Add($"Rom{num}.csv"); // esta lista será utilizada como base para referenciar os pontos que serão plotados no gráfico de dispersão!
            }

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
                    for (int i = 0; i < array.Length; i++) // percorrendo as posições dentro desse array
                    {
                        double valor = array[i]; // obtendo cada valor em cada posição do array
                        valoresDoArquivoMatrizPCA.Add(valor); // adicionando esses valores em uma lista
                    }

            }
        }
        #region PCA
        public static void PCA()
        {
            int numColunas = matrizMedias.GetLength(0); // 339
            int numLinhas = matrizMedias.GetLength(1); // 2048

            double[] media = new double[numColunas];
            double[] somaColunas = new double[numColunas];

            // percorrer cada coluna de matrizMedias, somando os valores de cada coluna e inserindo o resultado de cada soma 
            // no array unidimensional "somaColunas":

            for (int coluna = 0; coluna < numColunas; coluna++)
            {
                for (int linha = 0; linha < numLinhas; linha++)
                    somaColunas[coluna] = somaColunas[coluna] + matrizMedias[coluna, linha];

                media[coluna] = somaColunas[coluna] / numColunas;
            }

            double[,] matrizCovariancia = CalcularMatrizCovariancia(matrizMedias, media);
            MatrizTransposta(numLinhas, numColunas, matrizCovariancia);
        }
        #endregion PCA

        #region MatrizCovariancia
        public static double[,] CalcularMatrizCovariancia(double[,] matrizMedias, double[] media)
        {
            int linhas = matrizMedias.GetLength(1);
            int colunas = matrizMedias.GetLength(0);

            double[,] matrizCovariancia = new double[linhas, colunas];

            for (int i = 0; i < colunas; i++)
                for (int j = 0; j < linhas; j++)
                    matrizCovariancia[j, i] = matrizMedias[i, j] - media[i];

            return matrizCovariancia;
        }
        #endregion MatrizCovariancia

        #region TransposiçãoDeMatriz
        public static double[,] MatrizTransposta(int numLinhas, int numColunas, double[,] matrizCovariancia)
        {
            double[,] matrizTranspostaArray = new double[numColunas, numLinhas]; // declaração da Matriz para Transposição

            var matriz = DenseMatrix.OfArray(matrizCovariancia); // cria uma nova instância de DenseMatrix usando os valores do array matrizCovariancia
            var matrizCovarianciaTransposta = matriz.Transpose(); // transpondo a MatrizCovariancia
            matrizTranspostaArray = matrizCovarianciaTransposta.ToArray(); // transformando a Matriz de Covariância Transposta em um Array (bidimensional)

            double escalar = 1.0 / (matrizCovariancia.GetLength(0) - 1);

            double[,] produtoMatrizCovariancia = ProdutoDeTranspostaECovarianciaMatrizes(matrizTranspostaArray, matrizCovariancia, escalar);
            AutosValoresEVetores(produtoMatrizCovariancia);

            return matrizTranspostaArray;
        }
        #endregion TransposiçãoDeMatriz

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

            if (matrizMedias.GetLength(0) == autoVetoresRealArrayBidimensional.GetLength(1))
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

            double[,] resultadoNormalizado = NormalizarDados(resultado);
            PlotagemGraficoPCA(resultadoNormalizado);

        }

        #endregion produtoDeMatrizes

        #region NormalizaçãoDosDados
        public static double[,] NormalizarDados(double[,] resultado)
        {
            int linhas = resultado.GetLength(0);
            int colunas = resultado.GetLength(1);

            double[,] dadosNormalizados = new double[linhas, colunas];

            for (int j = 0; j < colunas; j++)
            {
                double valorMin = resultado[0, j];
                double valorMax = resultado[0, j];

                // Encontre os valores mínimo e máximo em cada coluna
                for (int i = 1; i < linhas; i++)
                {
                    if (resultado[i, j] < valorMin) valorMin = resultado[i, j];
                    if (resultado[i, j] > valorMax) valorMax = resultado[i, j];
                }

                // Normalize os dados na coluna
                for (int i = 0; i < linhas; i++)
                {
                    dadosNormalizados[i, j] = (resultado[i, j] - valorMin) / (valorMax - valorMin);
                    dadosNormalizados[i, j] = dadosNormalizados[i, j] * -1 + 1;
                }
            }

            return dadosNormalizados; // 339 linhas e colunas
        }
        #endregion NormalizaçãoDosDados
        
        #region PlotagemDoGrafico

        public static void PlotagemGraficoPCA(double[,] resultadoNormalizado)
        {
            double[] xs = new double[resultadoNormalizado.GetLength(0)]; // Coordenadas x - Primeiro Componente Principal;
            double[] ys = new double[resultadoNormalizado.GetLength(1)]; // Coordenadas y - Segundo Componente Principal

            for (int i = 0; i < resultadoNormalizado.GetLength(0); i++)
            {
                xs[i] = resultadoNormalizado[i, 0]; // Primeiro componente principal
                ys[i] = resultadoNormalizado[i, 1]; // Segundo componente principal
            }

            PCA_grafico pcaGrafico = new PCA_grafico();

            pcaGrafico.Text = "Análise de Componentes Principais (PCA)";
            pcaGrafico.Show();
            pcaGrafico.AtualizarGrafico(xs,ys);
        }

        #endregion PlotagemDoGrafico
        #endregion Metodos
    }
}
