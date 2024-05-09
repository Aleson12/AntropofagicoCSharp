using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntropofagicoCSharp
{

    public static class Arquivo
    {
        #region propriedades
        private static List<string> ArquivosTxtDaPasta;
        private static List<string> CaminhosDosArquivosTxtDaPasta; 
        private static List<string> ArquivosTxtDaPastaOrdenados;
        private static List<string> ArquivosAgrupados;


        private static bool _validaPrimeiroCaso = false; // variável no escopo da classe vira campo/atributo

        #endregion propriedades
        #region Metodos
        /// <summary>
        /// obtém o caminho de cada arquivo do diretório (com a extensão .txt) transformando em uma Lista
        /// </summary>
        /// <param name="diretorio"></param>
        /// <returns></returns>
        public static List<string> FiltrarArquivosTxt(string diretorio )
        {
            CaminhosDosArquivosTxtDaPasta = new List<string>();

               CaminhosDosArquivosTxtDaPasta = Directory.GetFiles(diretorio)
                        // Filtra apenas os arquivos com extensão ".txt"
                        .Where(arquivo => Path.GetExtension(arquivo) == ".txt").ToList() ;
            return CaminhosDosArquivosTxtDaPasta;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Diretorio"></param>
        public static void  AgrupandoOsTxtsPorClasse()
        {
            ArquivosTxtDaPasta = new List<string>();
            ArquivosTxtDaPastaOrdenados = new List<string>();
            ArquivosAgrupados = new List<string>();

            string nome_do_csv = string.Empty;
            string[] partes; // vai receber as duas partes de um nome de arquivo separados pelo hífen
            string nomeComTipo = string.Empty; // nome do arquivo
            string numeroPosHifen = string.Empty; // número após o hífen (sem a extensão do arquivo)
            string comparaNome = string.Empty; 

            // extraindo apenas o nome do arquivo .txt (sem a extensão e o seu caminho de diretório) 
            CaminhosDosArquivosTxtDaPasta.ToList().ForEach(caminho =>
            {

                if (Path.GetDirectoryName(caminho) != " ")

                ArquivosTxtDaPasta.Add(Path.GetFileNameWithoutExtension(caminho)); // pega o arquivo sem a extensão
                
                ArquivosTxtDaPastaOrdenados = ArquivosTxtDaPasta.OrderBy(arquivo => RecuperarNumeracaoDeNomeDeArquivo(arquivo)).ToList(); // ordenando a lista crescentemente de acordo com o número de cada arquivo

            });

            ArquivosTxtDaPastaOrdenados.Add("null 1-1"); // adicionando um valor ao final da lista para que a repetição pare

            ArquivosTxtDaPastaOrdenados.ForEach(arquivoOrdenado => {

                if (nomeComTipo != comparaNome)
                {
                    comparaNome = nomeComTipo;


                    if (ArquivosAgrupados.Count != 0)
                    {
                        ProcessamentoDosTxtsAgrupados();
                        ArquivosAgrupados.Clear(); // limpa a lista
                    }

                     if (!_validaPrimeiroCaso)
                        ArquivosAgrupados.Add(arquivoOrdenado);
                    _validaPrimeiroCaso = true;

                }
                else if (nomeComTipo == comparaNome)
                {
                    ArquivosAgrupados.Add(arquivoOrdenado);
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
            int divisorParaMediaEQuantidadeDeColunasDaMatriz = ArquivosAgrupados.Count;

            int linhas = 2048;
            int colunas = divisorParaMediaEQuantidadeDeColunasDaMatriz; // a quantidade de colunas da Matriz será a igual ao tamanho da Lista_dos_arquivos_agrupados

            // criação de matriz com 2048 linhas e "n" colunas, preenchida apenas com zeros
            double[,] matriz = new double[linhas, colunas];

            var listaEnumerada = ArquivosAgrupados.Select((valor ,indice) => new { Index = indice, Value = valor  }); // lendo cada elemento da lista e o seu respectivo índice

            List<int> valoresDeCadaTxtComoLista = new List<int>();

            // percorrendo a lista e extraindo o seu elemento/valor e índice:

            foreach (var item in listaEnumerada) // percorrendo a lista
            {
                var colunaIndice = item.Index; // extraindo o índice
                var arquivoValor = item.Value; // extraindo o valor

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

                valoresDeCadaTxtComoLista = ArquivosEmLinhaCsv.ConvertAll(arquivoValor => int.Parse(arquivoValor));

              //  valoresDeCadaTxtComoLista = ArquivosEmLinhaCsv.Select(arquivoValor => arquivoValor[1]).ToList();


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
