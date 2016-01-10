using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Web.Services;

public partial class Usuarios_Usuarios : System.Web.UI.Page
{
    InsereDados insereDados = new InsereDados();
    SelecionaDados selecionaDados = new SelecionaDados();
    AtualizaDados atualizaDados = new AtualizaDados();

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
                else if (Session["SessionQtdUnidades"].ToString() == "0")
                {
                    RetornaPaginaErro("Não existem Unidades cadastradas para vincular usuários. <br/>" +
                                      "Por favor, consulte o administrador do sistemas.");
                }
                else
                {
                    if (Session["SessionUsuario"].ToString() != string.Empty)
                    {
                        if (!IsPostBack)
                            CarregaDados();
                        else
                            DesmarcaSelecionados();
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

    /// <summary>
    /// Inicia a pagina
    /// </summary>
    public void CarregaDados()
    {
        CarregaUsuarios();
        CarregaUnidades();
        CarregaTipoAcesso();
        DadosDefault();
    }

    private void CarregaUnidades()
    {
        DataTable dtUnidades = selecionaDados.ConsultaTodasUnidades();

        if (dtUnidades.Rows.Count > 0)
        {
            ddlUnidade.DataSource = dtUnidades;
            ddlUnidade.DataTextField = "Unidade";
            ddlUnidade.DataValueField = "IdUnidade";
            ddlUnidade.DataBind();
        }
    }

    private void CarregaTipoAcesso()
    {
        DataTable dtTipoAcesso = selecionaDados.ConsultaTipoAcesso();

        if (dtTipoAcesso.Rows.Count > 0)
        {
            ddlNivelAcesso.DataSource = dtTipoAcesso;
            ddlNivelAcesso.DataTextField = "TipoAcesso";
            ddlNivelAcesso.DataValueField = "IdTipoAcesso";
            ddlNivelAcesso.DataBind();
        }
    }

    private void InsereUsuario(string sIdUnidade, string sIdTipoAcesso, string nomeUsuario, string login, string senha)
    {
        int idUnidade = Convert.ToInt32(sIdUnidade.Trim());
        int idTipoAcesso = Convert.ToInt32(sIdTipoAcesso.Trim());

        InsereDados insereDados = new InsereDados();

        insereDados.InsereUsuario(nomeUsuario, login, senha, idUnidade, idTipoAcesso, 1);
    }

    private void DeletaUsuario(string sIdUsuario)
    {
        int idUsuario = Convert.ToInt32(sIdUsuario.Trim());

        DeletaDados deletaDados = new DeletaDados();

        deletaDados.DeletaUsuario(idUsuario);
    }

    private void CarregaUsuarios()
    {
        DataTable dtUsuarios = selecionaDados.ConsultaTodosUsuarios();

        if (dtUsuarios.Rows.Count > 0)
        {
            rblUsuarios.DataSource = dtUsuarios;
            rblUsuarios.DataTextField = "Nome";
            rblUsuarios.DataValueField = "IdUsuario";
            rblUsuarios.DataBind();

            rptCadastros.DataSource = dtUsuarios;
            rptCadastros.DataBind();
        }
    }


    private void DadosDefault()
    {
        txtNovoNome.Text = string.Empty;
        txtNovoLogin.Text = string.Empty;
        txtNovaSenha.Text = string.Empty;
    }

    private void DesmarcaSelecionados()
    {
        foreach (ListItem item in rblUsuarios.Items)
        {
            if (item.Selected)
                item.Selected = false;
        }
    }

    protected void rptCadastros_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblTipoAcesso = (Label)e.Item.FindControl("lblTipoAcesso");
        Label lblUnidade = (Label)e.Item.FindControl("lblUnidade");
        Label lblIdCadastro = (Label)e.Item.FindControl("lblIdCadastro");
        Panel dvUsuario = (Panel)e.Item.FindControl("dvUsuario");

        TextBox txtSenha = (TextBox)e.Item.FindControl("txtSenha");
        Label lblSenha = (Label)e.Item.FindControl("lblSenha");
        txtSenha.Attributes.Add("value", lblSenha.Text.Trim());

        lblUnidade.Text = RetornaDescricaoUnidade(Convert.ToInt32(lblUnidade.Text.Trim()));

        dvUsuario.CssClass = "none dvUsuario css" + lblIdCadastro.Text.Trim();
    }

    private string RetornaDescricaoUnidade(int idUnidade)
    {
        string nomeUnidade = "Não definida";

        try
        {
            DataTable dtUnidades = selecionaDados.ConsultaTodasUnidades();
            dtUnidades.DefaultView.RowFilter = "IdUnidade = " + idUnidade + "";

            nomeUnidade = dtUnidades.DefaultView[0]["Unidade"].ToString().Trim();
        }
        catch (Exception) { }//Continua defaut

        return nomeUnidade;
    }

    #region Ações dos Botões

    protected void btCadastrar_Click(object sender, EventArgs e)
    {
        try
        {
            bool loginCadastrado = false;
            loginCadastrado = VerificaLogin(txtNovoLogin.Text.Trim());

            if (!loginCadastrado)
            {
                InsereUsuario(ddlUnidade.SelectedValue, ddlNivelAcesso.SelectedValue, txtNovoNome.Text.Trim(), txtNovoLogin.Text.Trim(), txtNovaSenha.Text.Trim());

                DadosDefault();
                DesmarcaSelecionados();
                CarregaDados();

                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Usuário cadastrado com sucesso!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Por favor, defina um novo Login pois o digitado já está sendo usado.');", true);
            }
           
        }
        catch (Exception ex)
        {
            Session["ExcessaoDeErro"] = ex.ToString();
            Response.Redirect("../Erro/Erro.aspx");
        }
    }

    private bool VerificaLogin(string novoLogin)
    {
        DataTable dtLoginUsuario = selecionaDados.ConsultaLoginUsuario(novoLogin);
        dtLoginUsuario.DefaultView.RowFilter = "IdTipoStatus = 1";
        //O mesmo login só é valido para usuários que já foram cadastrados e bloqueados posteriormente

        if (dtLoginUsuario.DefaultView.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }      
    }

    /// <summary>
    /// Grava e altera os números
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btAlterar_Click(object sender, EventArgs e)
    {
        string idCadastro = ((Button)sender).CommandArgument.ToString().Trim();

        try
        {
            foreach (RepeaterItem item in rptCadastros.Items)
            {
                Label lblIdCadastro = (Label)item.FindControl("lblIdCadastro");
                Label lblNome = (Label)item.FindControl("lblNome");

                TextBox txtLogin = (TextBox)item.FindControl("txtLogin");
                TextBox txtSenha = (TextBox)item.FindControl("txtSenha");

                if (idCadastro == lblIdCadastro.Text)
                {
                    atualizaDados.AtualizaUsuario(Convert.ToInt32(idCadastro), txtLogin.Text.Trim(), txtSenha.Text.Trim());
                    DadosDefault();
                    DesmarcaSelecionados();
                    CarregaDados();
                    break;
                }
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Usuário alterado com sucesso!');", true);
        }
        catch (Exception ex)
        {
            Session["ExcessaoDeErro"] = ex.ToString();
            Response.Redirect("../Erro/Erro.aspx");
        }
    }

    protected void btExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            string idCadastro = ((Button)sender).CommandArgument.ToString().Trim();

            foreach (RepeaterItem item in rptCadastros.Items)
            {
                Label lblIdCadastro = (Label)item.FindControl("lblIdCadastro");

                if (idCadastro == lblIdCadastro.Text)
                {
                    DeletaUsuario(idCadastro);
                    DadosDefault();
                    DesmarcaSelecionados();
                    CarregaDados();
                    break;
                }
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Usuário excluído com sucesso!');", true);
        }
        catch (Exception ex)
        {
            Session["ExcessaoDeErro"] = ex.ToString();
            Response.Redirect("../Erro/Erro.aspx");
        }

    }

    #endregion


    protected void btMenuInicial_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Home/Home.aspx");
    }

    protected void btErro_Click(object sender, EventArgs e)
    {
        Session["ExcessaoDeErro"] = hddErro.Value;
        Response.Redirect("../Erro/Erro.aspx");
    }

    public void RetornaPaginaErro(string erro)
    {
        Session["ExcessaoDeErro"] = erro.Trim();
        Response.Redirect("../Erro/Erro.aspx");
    }

    protected void RecarregarPagina_Click(object sender, EventArgs e)
    {
        DesmarcaSelecionados();
        Response.Redirect("../Usuarios/Usuarios.aspx");
    }
}