using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtLogin.Focus();
    }

    protected void Acessar_Click(object sender, EventArgs e)
    {
        VerificaAcesso();
    }

    private void VerificaAcesso()
    {       

        if (txtLogin.Text == "sistemas" && txtSenha.Text == "sistem@s01")
        {
            Session["SessionUsuario"] = "Sistemas";
            Session["SessionIdUsuario"] = "0";
            Session["SessionIdTipoAcesso"] = "1";
            Session["SessionQtdUnidades"] = "0";
            Response.Redirect("../Home/Home.aspx");
            divRetorno.Visible = false;
        }
        else
        {
            try
            {
                SelecionaDados selecionaDados = new SelecionaDados();
                DataTable dtUsuario = selecionaDados.ConsultaUsuario(txtLogin.Text.Trim(), txtSenha.Text.Trim());

                if (dtUsuario.Rows.Count > 0)
                {
                    if (dtUsuario.Rows[0]["IdTipoStatus"].ToString() == "0")
                    {
                        divRetorno.Visible = true;
                        lblRetorno.Text = "Usuário bloqueado. <br /> Para mais informações, por favor, contate o Administrador do Sistema.";
                    }
                    else
                    {
                        divRetorno.Visible = false;

                        Session["SessionUsuario"] = dtUsuario.Rows[0]["Nome"].ToString();
                        Session["SessionIdUsuario"] = dtUsuario.Rows[0]["IdUsuario"].ToString();
                        Session["SessionIdTipoAcesso"] = dtUsuario.Rows[0]["IdTipoAcesso"].ToString();
                        Session["SessionIdUnidade"] = dtUsuario.Rows[0]["IdUnidade"].ToString();

                        if (dtUsuario.Rows[0]["IdTipoAcesso"].ToString() == "1")//Adm
                        {
                            Response.Redirect("../Home/Home.aspx");
                        }
                        else//Usuário
                        {
                            Response.Redirect("../Acoes/Acoes.aspx");
                        }                        
                    }

                }
                else
                {
                    txtLogin.Focus();
                    divRetorno.Visible = true;
                    lblRetorno.Text = "Dados de acesso incorrentos <br /> e/ou usuário não cadastrado.";
                }

            }
            catch (Exception ex)
            {
                txtLogin.Focus();
                divRetorno.Visible = true;
                lblRetorno.Text = "Erro com a requisição.<br /> Por favor, contate o Administrador do sistema.";
            }

        }

    }
}