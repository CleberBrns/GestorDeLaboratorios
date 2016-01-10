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
            if (Session["SessionIdTipoAcesso"].ToString() != string.Empty)
            {
                if (Session["SessionIdTipoAcesso"].ToString() == "1")//Adm
                {
                    CarregaPagina();
                }
                else//Usuário
                {
                    Response.Redirect("../Acoes/Acoes.aspx");
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

    public void RetornaPaginaErro(string erro)
    {
        Session["ExcessaoDeErro"] = erro.Trim();
        Response.Redirect("../Erro/Erro.aspx");
    }

    private void CarregaPagina()
    {
        //Usuário Produção
        if (Session["SessionIdTipoAcesso"].ToString() != "1")
        {
            btUnidades.Visible = false;
            btUsuarios.Visible = false;
        }
        else
        {
            try
            {
                if (Session["SessionUsuario"].ToString().ToLower() == "sistemas")
                {
                    btAcoes.Visible = false;
                }

                DataTable dtUnidades = selecionaDados.ConsultaTodasUnidades();

                Session["SessionQtdUnidades"] = "0";
                if (!(dtUnidades.Rows.Count > 0))
                {
                    btLaboratorios.Visible = false;
                    btUsuarios.Visible = false;
                }
                else
                {
                    Session["SessionQtdUnidades"] = dtUnidades.Rows.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                btUsuarios.Visible = false;
            }

        }
    }

    protected void btUsuarios_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Usuarios/Usuarios.aspx");
    }

    protected void btSair_Click(object sender, EventArgs e)
    {
        Session["SessionUsuario"] = null;
        Session["SessionIdUsuario"] = null;
        Session["SessionIdTipoAcesso"] = null;
        Session["SessionIdUnidade"] = null;

        Response.Redirect("../Login/Login.aspx");
    }

    protected void btUnidades_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Unidades/Unidades.aspx");
    }

    protected void btAcoes_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Acoes/Acoes.aspx");
    }    

    protected void btOpcoesAdicionais_Click(object sender, EventArgs e)
    {
        Response.Redirect("../OpcoesAdicionais/Opcoes.aspx");
    }

    protected void btLaboratorios_Click(object sender, EventArgs e)
    {
        Response.Redirect("../OpcoesAdicionais/Laboratorios.aspx");
    }
}