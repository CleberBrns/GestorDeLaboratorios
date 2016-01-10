using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class Unidades_Unidades : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["SessionIdTipoAcesso"].ToString() != "1")
            {
                RetornaPaginaErro("Você não possui permissões para acessar essa ferramenta.");
            }
            else
            {
                if (!IsPostBack)
                {
                    CarregaDados();
                }
            }
        }
        catch (Exception ex)
        {
            if (Session["SessionIdTipoAcesso"] == null)
            {
                RetornaPaginaErro("Sessão perdida. Por favor, faça o login novamente.");
            }
            else
            {
                RetornaPaginaErro(ex.ToString());
            }

        }

    }

    private void CarregaDados()
    {
        int qtdUnidades = 0;

        Int32.TryParse(Session["SessionQtdUnidades"].ToString(), out qtdUnidades);

        if (qtdUnidades > 0)
        {
            btGerenciar.Visible = true;
            btConsultar.Visible = true;
        }
    }

    protected void btNovaUnidade_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Unidades/Cadastrar.aspx");
    }

    protected void btGerenciar_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Unidades/Gerenciar.aspx");
    }

    protected void btMenuPrincipal_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Home/Home.aspx");
    }

    protected void btConsultar_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Unidades/Consultar.aspx");
    }

    public void RetornaPaginaErro(string erro)
    {
        Session["ExcessaoDeErro"] = erro.Trim();
        Response.Redirect("../Erro/Erro.aspx");
    }

}