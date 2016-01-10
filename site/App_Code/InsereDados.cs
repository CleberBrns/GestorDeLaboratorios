using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// 
/// </summary>
public class InsereDados
{
    SelecionaDados selecionaDados = new SelecionaDados();
    string sConexao = ConfigurationManager.AppSettings.Get("sConexaoSQL");


    public void InsereLaboratorio(string codLaboratorio, string Nome, int idTipoStatus, int idUnidade)
    {
       
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_laboratorio_insert";
                sqlCommand.Parameters.AddWithValue("@CodLaboratorio", codLaboratorio);
                sqlCommand.Parameters.AddWithValue("@Nome", Nome);
                sqlCommand.Parameters.AddWithValue("@IdTipoStatus", idTipoStatus);
                sqlCommand.Parameters.AddWithValue("@IdUnidade", idUnidade);
             
                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);               
                sqlCommand.Dispose();
                sqlCommand = null;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }
        
    }

    public int InsereUnidade(string unidade, int idCidade, int idEstado)
    {
        int idUnidade = 0;

        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_unidade_insert";
                sqlCommand.Parameters.AddWithValue("@unidade", unidade);
                sqlCommand.Parameters.AddWithValue("@idCidade", idCidade);
                sqlCommand.Parameters.AddWithValue("@idEstado", idEstado);
                sqlCommand.Parameters.AddWithValue("@idUnidade", 0).Direction = ParameterDirection.Output;

                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                idUnidade = int.Parse(sqlCommand.Parameters["@idUnidade"].Value.ToString());
                sqlCommand.Dispose();
                sqlCommand = null;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }

        return idUnidade;
    }

    public int InsereCamara(string codCamara, int idUnidade)
    {
        int idCamara = 0;

        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_camara_insert";
                sqlCommand.Parameters.AddWithValue("@NomeCamara", codCamara);
                sqlCommand.Parameters.AddWithValue("@idUnidade", idUnidade);                              
                sqlCommand.Parameters.AddWithValue("@idCamara", 0).Direction = ParameterDirection.Output;

                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                idCamara = int.Parse(sqlCommand.Parameters["@idCamara"].Value.ToString());
                sqlCommand.Dispose();
                sqlCommand = null;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }

        return idCamara;
    }

    public int InsereEstante(string codEstante, int idCamara)
    {
        int idEstante = 0;

        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_estante_insert";
                sqlCommand.Parameters.AddWithValue("@Estante", codEstante);
                sqlCommand.Parameters.AddWithValue("@idCamara", idCamara);
                sqlCommand.Parameters.AddWithValue("@idEstante", 0).Direction = ParameterDirection.Output;

                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                idEstante = int.Parse(sqlCommand.Parameters["@idEstante"].Value.ToString());
                sqlCommand.Dispose();
                sqlCommand = null;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }

        return idEstante;
    }

    public void InserePrateleira(int idEstante, string codPrateleira)
    {
        SqlConnection sqlConnection = new SqlConnection(sConexao);

        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_prateleira_insert";
                sqlCommand.Parameters.AddWithValue("@idEstante", idEstante);
                sqlCommand.Parameters.AddWithValue("@Prateleira", codPrateleira);
                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                sqlCommand.Dispose();
                sqlCommand = null;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }
    }

    public void InsereUsuario(string nome, string login, string senha, int idUnidade, int idTipoAcesso, int idStatus)
    {
        SqlConnection sqlConnection = new SqlConnection(sConexao);

        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_usuario_insert";
                sqlCommand.Parameters.AddWithValue("@Nome", nome);
                sqlCommand.Parameters.AddWithValue("@Login", login);
                sqlCommand.Parameters.AddWithValue("@senha", senha);
                sqlCommand.Parameters.AddWithValue("@idUnidade", idUnidade);
                sqlCommand.Parameters.AddWithValue("@idTipoAcesso", idTipoAcesso);
                sqlCommand.Parameters.AddWithValue("@idTipoStatus", idStatus);
                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                sqlCommand.Dispose();
                sqlCommand = null;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }
    }

    public void InsereAmostraRecepcao(int idPrateleira, int idUsuario, long codAmostra, string caixa)
    {
        SqlConnection sqlConnection = new SqlConnection(sConexao);

        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_amostra_recepcao_insert";

                sqlCommand.Parameters.AddWithValue("@IdPrateleira", idPrateleira);
                sqlCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);
                sqlCommand.Parameters.AddWithValue("@IdAcao", 1);
                sqlCommand.Parameters.AddWithValue("@CodAmostra", codAmostra);          
                sqlCommand.Parameters.AddWithValue("@Caixa", caixa);

                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                sqlCommand.Dispose();
                sqlCommand = null;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }
    }

    public void InsereAmostraSaida(int idPrateleira, int idUsuario, long codAmostra, string caixa, int idLaboratorio)
    {
        SqlConnection sqlConnection = new SqlConnection(sConexao);

        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_amostra_saida_insert";

                sqlCommand.Parameters.AddWithValue("@IdPrateleira", idPrateleira);
                sqlCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);
                sqlCommand.Parameters.AddWithValue("@IdAcao", 2);
                sqlCommand.Parameters.AddWithValue("@CodAmostra", codAmostra);
                sqlCommand.Parameters.AddWithValue("@Caixa", caixa);
                sqlCommand.Parameters.AddWithValue("@IdLaboratorio", idLaboratorio);

                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                sqlCommand.Dispose();
                sqlCommand = null;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }
    }

    public void InsereAmostraEntrada(int idPrateleira, int idUsuario, long codAmostra, string caixa)
    {
        SqlConnection sqlConnection = new SqlConnection(sConexao);

        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_amostra_entrada_insert";

                sqlCommand.Parameters.AddWithValue("@IdPrateleira", idPrateleira);
                sqlCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);
                sqlCommand.Parameters.AddWithValue("@IdAcao", 3);
                sqlCommand.Parameters.AddWithValue("@CodAmostra", codAmostra);
                sqlCommand.Parameters.AddWithValue("@Caixa", caixa);

                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                sqlCommand.Dispose();
                sqlCommand = null;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }
    }

    public void InsereAmostraDescarte(int idPrateleira, int idUsuario, long codAmostra, string caixa)
    {
        SqlConnection sqlConnection = new SqlConnection(sConexao);

        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_amostra_descarte_insert";

                sqlCommand.Parameters.AddWithValue("@IdPrateleira", idPrateleira);
                sqlCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);
                sqlCommand.Parameters.AddWithValue("@IdAcao", 4);
                sqlCommand.Parameters.AddWithValue("@CodAmostra", codAmostra);
                sqlCommand.Parameters.AddWithValue("@Caixa", caixa);

                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                sqlCommand.Dispose();
                sqlCommand = null;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }
    }

    public void InsereAmostraAuditoria(int idPrateleira, int idUsuario, long codAmostra)
    {
        SqlConnection sqlConnection = new SqlConnection(sConexao);

        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_amostra_auditoria_insert";

                sqlCommand.Parameters.AddWithValue("@IdPrateleira", idPrateleira);
                sqlCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);
                sqlCommand.Parameters.AddWithValue("@IdAcao", 5);
                sqlCommand.Parameters.AddWithValue("@CodAmostra", codAmostra);
                sqlCommand.Parameters.AddWithValue("@Caixa", string.Empty);

                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                sqlCommand.Dispose();
                sqlCommand = null;
            }
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection = null;
        }
    }

}
