using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for Auxiliar
/// </summary>
public class Auxiliar
{
    public DataTable RetornaAlfabeto()
    {
        DataTable dtAlfabeto = new DataTable();
        dtAlfabeto.Columns.Add("IdLetra");
        dtAlfabeto.Columns.Add("Letra");

        string[] aAlfabeto = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "l", "m", "n", "o", "p", 
                             "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        for (int countLetra = 0; countLetra < aAlfabeto.Length; countLetra++)
        {
            DataRow dRow = dtAlfabeto.NewRow();

            dRow["IdLetra"] = countLetra;
            dRow["Letra"] = aAlfabeto[countLetra].ToUpper();

            dtAlfabeto.Rows.Add(dRow);
        }

        return dtAlfabeto;
    }

    public class AmostraXGrupo
    {
        public int IdAmostra { get; set; }   
        public string TipoAmostra { get; set; }     
        public int IdStatusAmostra { get; set; }  
        public string StatusAmostra { get; set; }
        public string DataEntrada { get; set; }        
    }

    public class AmostrasAuditoria
    {
        public string Camara { get; set; }
        public string Caixa { get; set; }
        public string CodGrupo { get; set; }
        public string IdAmostra { get; set; }
        public string DataEntrada { get; set; }
        public string IdStatusAmostra { get; set; }
        public string StatusAmostra { get; set; }
    }
}
