using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Analise_Impressao : System.Web.UI.Page
{
    SelecionaDados selecionaDados = new SelecionaDados();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Session["SessionPrateleira"].ToString()))
                {
                    CarregaInfoConsulta(Session["SessionPrateleira"].ToString().Trim());
                }
                else
                {
                    RetornaPaginaErro("Perdeu a sessão. Faça o login novamente, por favor.");
                }
            }

        }
        catch (Exception ex)
        {
            RetornaPaginaErro(ex.ToString());
        }
    }

    public void RetornaPaginaErro(string erro)
    {
        Session["ExcessaoDeErro"] = erro.Trim();
        Response.Redirect("../Erro/Erro.aspx");
    }

    private void CarregaInfoConsulta(string prateleira)
    {
        DataTable dtBusca = new DataTable();

        lblTipoImpressao.Text = Session["SessionTipoImpressao"].ToString();

        lblPrateleira.Text = " - Prateleira " + prateleira;
        dtBusca = CarregaInfoPrateleira(prateleira);

        CarregaRepeater(dtBusca);
    }

    private DataTable CarregaInfoPrateleira(string codPrateleira)
    {
        DataTable dtInfoPrateleira = new DataTable();

        dtInfoPrateleira.Columns.Add("CodAmostra");
        dtInfoPrateleira.Columns.Add("DataUsuarioRecepcao");
        dtInfoPrateleira.Columns.Add("Estante");
        dtInfoPrateleira.Columns.Add("Prateleira");
        dtInfoPrateleira.Columns.Add("Caixa");
        dtInfoPrateleira.Columns.Add("UltimaAlteracao");
        dtInfoPrateleira.Columns.Add("Laboratorio");
        dtInfoPrateleira.Columns.Add("Auditado");

        DataTable dtPrateleiraAuditoria = selecionaDados.ConsultaPrateleiraAuditoria(codPrateleira);

        if (dtPrateleiraAuditoria.Rows.Count > 0)
        {
            foreach (DataRow item in dtPrateleiraAuditoria.Rows)
            {
                dtInfoPrateleira.Rows.Add(item["CodAmostra"].ToString(),
                    ConfiguraUsuarioRecepcao(item["DataRecepcao"].ToString(), item["UsuarioRecepcao"].ToString()), item["Estante"].ToString(),
                    item["Prateleira"].ToString(), item["Caixa"].ToString(),
                    ConfiguraUltimaAlteracao(item["NomeUsuario"].ToString(), item["DataAtualizacao"].ToString(), item["UltimaAlteracao"].ToString()),
                    item["NomeLaboratorio"].ToString(), item["Auditoria"].ToString());
            }
        }

        return dtInfoPrateleira;
    }

    private object ConfiguraUsuarioRecepcao(string dataRecepcao, string usuarioRecepcao)
    {
        return (usuarioRecepcao + " - " + Convert.ToDateTime(dataRecepcao).ToShortDateString() + " " + Convert.ToDateTime(dataRecepcao).ToShortTimeString());
    }

    private object ConfiguraUltimaAlteracao(string nomeUsuario, string dataAtualizacao, string acao)
    {
        if (!string.IsNullOrEmpty(dataAtualizacao))
        {
            string data = Convert.ToDateTime(dataAtualizacao).ToShortDateString() + " " +
            Convert.ToDateTime(dataAtualizacao).ToShortTimeString();

            return (nomeUsuario + " - " + data + " - " + acao);
        }
        else
        {
            return string.Empty;
        }
    }

    private void CarregaRepeater(DataTable dtBusca)
    {
        rptAuditoria.DataSource = dtBusca;
        rptAuditoria.DataBind();
    }



}