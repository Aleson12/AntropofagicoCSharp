using CsvHelper;
using CsvHelper.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


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

            string nome_do_csv = string.Empty;
            string[] partes; // vai receber as duas partes de um nome de arquivo separados pelo hífen
            string nomeComTipo = string.Empty; // nome do arquivo
            string numeroPosHifen = string.Empty; // número após o hífen (sem a extensão do arquivo)
            string comparaNome = string.Empty;

            // extraindo apenas o nome do arquivo .txt (sem a extensão e o seu caminho de diretório) 
            caminhosDosArquivosTxtDaPasta.ToList().ForEach(caminho =>
            {

                if (Path.GetDirectoryName(caminho) != " ")

                    arquivosTxtsDaPasta.Add(Path.GetFileNameWithoutExtension(caminho)); // pega o arquivo sem a extensão

                arquivosTxtsDaPastaOrdenados = arquivosTxtsDaPasta.OrderBy(arquivo => RecuperarNumeracaoDeNomeDeArquivo(arquivo)).ToList(); // ordenando a lista crescentemente de acordo com o número de cada arquivo

            });

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
                    //  _validaPrimeiroCaso = true;

                }
                else if (nomeComTipo == comparaNome)
                    arquivosAgrupados.Add(arquivoOrdenado);

                else if (nomeComTipo != "null1")
                    nome_do_csv = nomeComTipo;

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
                if (int.TryParse(partesDoNomeDoArquivo[0].Substring(3), out int numero)) // o valor na terceira posição é transformado em inteiro e inserido na variável "numero"
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
            string nomeDoArquivoCsv = string.Empty;
            int colunaDaMatriz = 0;

            // criação de matriz com 2048 linhas e "n" colunas, preenchida apenas com zeros
            double[,] matriz = new double[_linhas, colunas];

            var listaEnumerada = arquivosAgrupados.Select((valor, indice) => new { Index = indice, Value = valor }); // lendo cada elemento da lista e o seu respectivo índice

            List<int> valorDeCadaTxtComoLista = new List<int>();

            // percorrendo a lista e extraindo o seu elemento/valor e índice:

            foreach (var item in listaEnumerada) // percorrendo a lista
            {
                var colunaIndice = item.Index; // extraindo o índice
                var arquivoValor = item.Value; // extraindo o valor

                nomeDoArquivoCsv = arquivoValor.ToString().Split('-')[0];

                string nomeDoArquivoTxtComCaminho = (FrmPrincipal.diretorio + "\\" + arquivoValor + ".txt").ToString();

                // configuração - opcional - do arquivo csv
                CsvConfiguration configuracao = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    Delimiter = ";", // o separador entre os valores será o ponto-e-vírgula
                    HasHeaderRecord = false // sem cabeçalho
                };

                List<string> ArquivosEmLinhaCsv = new List<string>();

                using (StreamReader leitor = new StreamReader(nomeDoArquivoTxtComCaminho)) // ler as linhas do arquivo
                using (CsvReader? _arquivoCsv = new CsvReader(leitor, configuracao)) // "csv" recebe o conteúdo do arquivo e a configuração personalizada para ele
                {
                    while (_arquivoCsv.Read())
                    { // enquanto o arquivo csv for lido, faça:

                        // constrói a linha atual do CSV
                        string linhaAtual = "";

                        // obtém o número de campos na linha atual
                        int numeroDeCampos = _arquivoCsv.Parser.Count;

                        // itera sobre os campos da linha atual
                        for (int i = 0; i < numeroDeCampos; i++)
                        {
                            // adiciona o campo à linha atual
                            linhaAtual += _arquivoCsv.GetField<string>(i);

                            // Adiciona um ponto-e-vírgula se não for o último campo
                            if (i < numeroDeCampos - 1)

                                linhaAtual += ";";

                        }
                        // adiciona a linha atual à lista
                        ArquivosEmLinhaCsv.Add(linhaAtual);

                    }
                }

                valorDeCadaTxtComoLista = ArquivosEmLinhaCsv.ConvertAll(arquivoValor =>
                int.Parse(arquivoValor.Split(';')[1])); // obtém o valor da coluna de índice 1 após o ponto-e-vírgula

                // obtendo o valor e seu respectivo índice de cada elemento da lista
                for (int i = 0; i < valorDeCadaTxtComoLista.Count; i++)
                {

                    int valorDeCadaLinha = valorDeCadaTxtComoLista[i]; // obtendo cada valor isoladamente

                    matriz[i, colunaDaMatriz] = valorDeCadaLinha; // inserindo na matriz os novos valores

                }

            }

            GerarSomenteUmArquivoPorClasse(matriz, nomeDoArquivoCsv); // passando a matriz e o nome de cada arquivo CSV como parâmetro para este método para que ele seja capaz de manipulá-los
            colunaDaMatriz += 1;
        }

        private static void GerarSomenteUmArquivoPorClasse(double[,] matriz, string nomeDoArquivoCsv)
        {
            mediaDosValoresDaMatriz = new List<double>(); // instanciando o objeto dessa Lista para que 
            // ela possa ser manipulada e não ocorrer o erro
            // "System.NullReferenceException: 'Object reference not set to an instance of an object.'"

            string caminhoComNomeDoCsv;

            double valor = 0;

            int linhas = matriz.GetLength(0); // obtendo a quantidade de linhas
            int colunas = matriz.GetLength(1); // obtendo a quantidade de colunas

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                    valor += matriz[i, j];

                if (valor > 0.00)
                {
                    var a = (valor / colunas);
                    var resultadoArredondado = Math.Round(a, 5); // arredondando o resultado da divisão para cinco casas decimais
                    mediaDosValoresDaMatriz.Add(resultadoArredondado);

                }
                else
                    mediaDosValoresDaMatriz.Add(0.0000);
                valor = 0;

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
            string[] dados;
            //string caminhoComONomeDoArquivoCSVFinal = string.Empty;
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

            if (!Directory.Exists(_caminhoComONomeDoArquivoCSVFinal))
            { // se o diretório não existir, 

                Directory.CreateDirectory(_caminhoComONomeDoArquivoCSVFinal); // crie-o

                CultureInfo culture = CultureInfo.GetCultureInfo("pt-BR");
                CsvConfiguration config = new(culture);
                config.HasHeaderRecord = false;

                List<double> valoresDoArquivoMatrizPCA = new List<double>();
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
            else // se o diretório já existir, ignore esta segunda condição
                ignorarCondicao = false;
        }

       public static void PCA()
       {
         //   string matrizString = string.Empty;

       }


        #endregion Metodos
    }
}
