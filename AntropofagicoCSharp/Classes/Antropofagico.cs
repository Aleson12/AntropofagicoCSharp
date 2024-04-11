
// importação das bibliotecas necessárias:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis; // biblioteca para trabalhar com DataFrame
using Microsoft.ML;

namespace AntropofagicoCSharp.Classes
{
    internal class Antropofagico
    {
        
        // declaração de listas:

        List<string> Lista_de_arquivos_txt_da_pasta = new List<string>();
        List<string> Lista_dos_arquivos_agrupados = new List<string>();
        List<int> Media_dos_valores_da_matriz = new List<int>();
        List<string> List_nomecsv = new List<string>();

        // variáveis globais:

        private int dados;
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

        private int ImportarDados()
        {
            DataFrame df = new DataFrame();
            dados = df.ToDataFrame(Media_dos_valores_da_matriz[]) ;
            return dados;

        }




    }
}
