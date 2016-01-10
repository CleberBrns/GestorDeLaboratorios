using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class Acoes_Acoes : System.Web.UI.Page
{
    SelecionaDados selecionaDados = new SelecionaDados();
    InsereDados insereDados = new InsereDados();


    protected void Page_Load(object sender, EventArgs e)
    {
        txtAcao.Focus();

        try
        {
            if (!IsPostBack)
            {
                if (Session["SessionUsuario"].ToString().ToLower() == "sistemas")
                {
                    RetornaPaginaErro("Esse usuário possui acesso somente ao cadastro de Unidades e Usuários <br/>" +
                        " Para ter acesso as ações, por favor, cadastre um usuário vinculado a alguma Unidade");
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
                        RetornaPaginaErro("Sessão perdida. Por favor, faça o login novamente.");
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

    private void CarregaPagina()
    {        
        hddIdUnidade.Value = Session["SessionIdUnidade"].ToString();

        if (Session["SessionIdTipoAcesso"].ToString() == "1")//Adm
        {
            imgAcaoAuditoria.Visible = true;
            btMenuPrincial.Visible = true;
        }
        else//Usuário
        {
            btSair.Visible = true;
        }

        try
        {
            string osVer = System.Environment.OSVersion.ToString().ToLower();
                        
            if (osVer.Contains("windows"))
            {
                divCodAcoes.Visible = true;
            }

        }
        catch (Exception ex) { }//Div continua escondida

    }

    protected void btAcao_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtAcao.Text))
        {
            MostraRetorno(string.Empty);
        }
        else
        {
            switch (txtAcao.Text)
            {
                case "01":
                    Response.Redirect("../Acoes/Recepcao.aspx");
                    break;
                case "02":
                    Response.Redirect("../Acoes/Saida.aspx");
                    break;
                case "03":
                    Response.Redirect("../Acoes/Entrada.aspx");
                    break;
                case "04":
                    Response.Redirect("../Acoes/Descarte.aspx");
                    break;
                case "05":
                    VerificaAcessoAuditoria();
                    break;
                case "06":
                    Response.Redirect("../Auditoria/Consulta.aspx");
                    break;
                case "07":
                    Response.Redirect("../Importacao/Importacao.aspx");
                    break;
                default:
                    MostraRetorno("Ação Desconhecida. Favor entrar em contato com o Administrador.");
                    break;
            }
        }
    }

    private void VerificaAcessoAuditoria()
    {
        if (Session["SessionIdTipoAcesso"].ToString() == "1")
        {
            Response.Redirect("../Auditoria/Auditoria.aspx");
        }
        else
        {
            MostraRetorno("Você não possui permissões para acessar essa ferramenta.");
        }

    }

    private void MostraRetorno(string mensagem)
    {
        divRetorno.Visible = true;
        imgErro.Visible = true;

        if (string.IsNullOrEmpty(mensagem))
        {
            lblRetorno.Text = "Por favor, preencha o campo corretamente para prosseguir";
        }
        else
        {
            lblRetorno.Text = mensagem;
        }
    }

    public void RetornaPaginaErro(string erro)
    {
        Session["ExcessaoDeErro"] = erro.Trim();
        Response.Redirect("../Erro/Erro.aspx");
    }

    protected void btMenuPrincipal_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Home/Home.aspx");
    }

    protected void btSair_Click(object sender, EventArgs e)
    {
        Session["SessionUsuario"] = null;
        Session["SessionIdUsuario"] = null;
        Session["SessionIdTipoAcesso"] = null;
        Session["SessionIdUnidade"] = null;

        Response.Redirect("../Login/Login.aspx");
    }

}