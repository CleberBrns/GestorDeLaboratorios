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
                
                if (Session["SessionRetornoImportacao"] != null)
                {
                    DataTable dtRetornoImportacao = (DataTable)Session["SessionRetornoImportacao"];
                    CarregaInfoConsulta(dtRetornoImportacao);
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

    private void CarregaInfoConsulta(DataTable dtConteudoImportacao)
    {
        lblNomeArquivo.Text = " - " + Session["SessionNomeArquivo"].ToString();   

        rptConsulta.DataSource = dtConteudoImportacao;
        rptConsulta.DataBind();
    }  

 public void RetornaPaginaErro(string erro)
    {
        Session["ExcessaoDeErro"] = erro.Trim();
        Response.Redirect("../Erro/Erro.aspx");
    }

}