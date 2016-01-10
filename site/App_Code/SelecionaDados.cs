using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;


/// <summary>
///
/// </summary>
public class SelecionaDados
{
    string sConexao = ConfigurationManager.AppSettings.Get("sConexaoSQL");

    public DataTable ConsultaLaboratorio()
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;                
                sqlCommand.CommandText = "usp_laboratorio_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        dtConsulta.DefaultView.RowFilter = "IdTipoStatus <> 2";
        return dtConsulta.DefaultView.ToTable();
    }

    public DataTable ConsultaStatusAmostra(long codAmostra)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@codAmostra", codAmostra);
                sqlCommand.CommandText = "usp_amostra_status_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaUnidade(int idUnidade)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idUnidade", idUnidade);
                sqlCommand.CommandText = "usp_UnidadeCamara_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaLoginUsuario(string login)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@login", login);
                sqlCommand.CommandText = "usp_loginUsuario_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaUsuario(string login, string senha)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@login", login);
                sqlCommand.Parameters.AddWithValue("@senha", senha);
                sqlCommand.CommandText = "usp_usuario_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaTodosUsuarios()
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_todosUsuarios_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaTipoAcesso()
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_tipoAcesso_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaTodasUnidades()
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_todasUnidades_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaPaises()
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_Pais_select";//Não existe a proc ainda
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }
    
    public DataTable ConsultaEstados(int idPais)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idPais", idPais);
                sqlCommand.CommandText = "usp_Estado_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaCidades(int idEstado)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idEstado", idEstado);
                sqlCommand.CommandText = "usp_Cidade_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaCamarasUnidade(int idUnidade)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idUnidade", idUnidade);
                sqlCommand.CommandText = "usp_camaraUnidade_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaPrateleira(string prateleira)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@prateleira", prateleira);
                sqlCommand.CommandText = "usp_prateleira_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaAmostraRecepcao(int idPrateleira, long codAmostra)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idPrateleira", idPrateleira);
                sqlCommand.Parameters.AddWithValue("@codAmostra", codAmostra);
                sqlCommand.CommandText = "usp_amostraRecepcao_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaAmostraSaida(int idPrateleira, int codAmostra)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idPrateleira", idPrateleira);
                sqlCommand.Parameters.AddWithValue("@codAmostra", codAmostra);
                sqlCommand.CommandText = "usp_amostraSaida_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaAmostraEntrada(int idPrateleira, int codAmostra)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idPrateleira", idPrateleira);
                sqlCommand.Parameters.AddWithValue("@codAmostra", codAmostra);
                sqlCommand.CommandText = "usp_amostraEntrada_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaAmostraDescarte(int idPrateleira, long codAmostra)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idPrateleira", idPrateleira);
                sqlCommand.Parameters.AddWithValue("@codAmostra", codAmostra);
                sqlCommand.CommandText = "usp_amostraDescarte_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaAmostraAuditoria(int idPrateleira, int codAmostra)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@idPrateleira", idPrateleira);
                sqlCommand.Parameters.AddWithValue("@codAmostra", codAmostra);
                sqlCommand.CommandText = "usp_amostraAuditoria_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaAmostra(long codAmostra)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;           
                sqlCommand.Parameters.AddWithValue("@codAmostra", codAmostra);
                sqlCommand.CommandText = "usp_amostra_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaPrateleiraAuditoria(string codPrateleira)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Prateleira", codPrateleira);
                sqlCommand.CommandText = "usp_auditoria_prateleira_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

    public DataTable ConsultaAmostraAuditoriaPrateleira(string codPrateleira)
    {
        DataTable dtConsulta = new DataTable();
        SqlConnection sqlConnection = new SqlConnection(sConexao);
        try
        {
            using (sqlConnection)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Prateleira", codPrateleira);
                sqlCommand.CommandText = "usp_amostra_auditoria_prateleira_select";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dtConsulta.Load(sqlDataReader);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlDataReader = null;
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

        return dtConsulta;
    }

}
