using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_Home : System.Web.UI.Page
{
    SelecionaDados selecionaDados = new SelecionaDados();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["SessionIdTipoAcesso"].ToString() != "1")
                {
                    RetornaPaginaErro("Você não possui permissões para acessar essa ferramenta.");
                }
                else
                {
                    if (Session["SessionUsuario"].ToString() != string.Empty)
                    {
                        if (!IsPostBack)
                            CarregaPagina();
                    }
                    else
                    {
                        RedirecionaLogin();
                    }
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

    private void RedirecionaLogin()
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Perdeu a sessão!');", true);
        Response.Redirect("../Login/Login.aspx");
    }

    public void RetornaPaginaErro(string erro)
    {
        Session["ExcessaoDeErro"] = erro.Trim();
        Response.Redirect("../Erro/Erro.aspx");
    }

    private void CarregaPagina()
    {

    }

    protected void btLaboratorios_Click(object sender, EventArgs e)
    {
        Response.Redirect("../OpcoesAdicionais/Laboratorios.aspx");
    }

    protected void btSair_Click(object sender, EventArgs e)
    {
        Session["SessionUsuario"] = null;
        Session["SessionIdUsuario"] = null;
        Session["SessionIdTipoAcesso"] = null;
        Session["SessionIdUnidade"] = null;

        Response.Redirect("../Login/Login.aspx");
    }

    protected void btMenuPrincipal_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Home/Home.aspx");
    }

}