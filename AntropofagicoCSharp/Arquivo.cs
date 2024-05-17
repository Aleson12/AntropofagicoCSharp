using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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

        private static bool _validaPrimeiroCaso = false; // variável no escopo da classe vira campo/atributo
        public static string caminhoDaPastaDosArquivosCSVPosTratamento; // membro da classe definido como "público" para ser possível acessá-lo na classe principal da Interface
        public static string caminhoComNomeDoCsv;
        
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
                        .Where(arquivo => Path.GetExtension(arquivo) == ".txt").ToList() ;
            return caminhosDosArquivosTxtDaPasta;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Diretorio"></param>
        public static void  AgrupandoOsTxtsPorClasse()
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

            arquivosTxtsDaPastaOrdenados.ForEach(arquivoOrdenado => {

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
                {
                    arquivosAgrupados.Add(arquivoOrdenado);

                }
                else if (nomeComTipo != "null1")
                {
                    nome_do_csv = nomeComTipo;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        private static void ProcessamentoDosTxtsAgrupados()
        {
            int divisorParaMediaEQuantidadeDeColunasDaMatriz = arquivosAgrupados.Count;

            int linhas = 2048;
            int colunas = divisorParaMediaEQuantidadeDeColunasDaMatriz; // a quantidade de colunas da Matriz será a igual ao tamanho da Lista_dos_arquivos_agrupados
            string nomeDoArquivoCsv = string.Empty;
            int colunaDaMatriz = 0;
            
            // criação de matriz com 2048 linhas e "n" colunas, preenchida apenas com zeros
            double[,] matriz = new double[linhas, colunas];

            var listaEnumerada = arquivosAgrupados.Select((valor ,indice) => new { Index = indice, Value = valor  }); // lendo cada elemento da lista e o seu respectivo índice

            List<int> valorDeCadaTxtComoLista = new List<int>();

            // percorrendo a lista e extraindo o seu elemento/valor e índice:

            foreach (var item in listaEnumerada) // percorrendo a lista
            {
                var colunaIndice = item.Index; // extraindo o índice
                var arquivoValor = item.Value; // extraindo o valor

                nomeDoArquivoCsv = arquivoValor.ToString().Split('-')[0];

                string nomeDoArquivoTxtComCaminho = (IPrincipal.diretorio + "\\" + arquivoValor + ".txt").ToString();

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
                for (int i = 0; i < valorDeCadaTxtComoLista.Count;i++)
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

            double valor = 0;

            int linhas = matriz.GetLength(0); // obtendo a quantidade de linhas
            int colunas = matriz.GetLength(1); // obtendo a quantidade de colunas

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                    valor += matriz[i,j];

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

            caminhoDaPastaDosArquivosCSVPosTratamento = Path.Combine($"{IPrincipal.diretorio}\\Roms\\");
            Directory.CreateDirectory(caminhoDaPastaDosArquivosCSVPosTratamento); // cria a pasta no sistema de arquivos

            // transformando cada valor número da lista em string, substituindo o ponto por vírgula, transformando tudo em uma lista e inserindo na nova variável
          //  List<string> mediaDosValoresDaMatrizComoString = mediaDosValoresDaMatriz.Select(valor => valor.ToString().Replace(".",",")).ToList();
            
            caminhoComNomeDoCsv = $"{caminhoDaPastaDosArquivosCSVPosTratamento}{nomeDoArquivoCsv}.csv"; // criando o caminho onde está o arquivo csv para ser escrito

            // criando o arquivo .csv, acessando-o, abrindo-o e escrevendo nele os novos valores
            using (StreamWriter writer = new StreamWriter(caminhoComNomeDoCsv))
            {
                foreach (double vlr in mediaDosValoresDaMatriz)
                {
                    writer.WriteLine($"{vlr}".Replace('.', ','));
                }
            }
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
                int numero; // variável criada para receber o valor de cada arquivo 

                if (int.TryParse(partesDoNomeDoArquivo[0].Substring(3), out numero)) // o valor na terceira posição é transformado em inteiro e inserido na variável "numero"
                    return numero; // retorna o número de cada arquivo
                
            }

            return int.MaxValue; // indica que não foi possível obter um número válido do texto fornecido

        }
        #endregion Metodos
    }
}
