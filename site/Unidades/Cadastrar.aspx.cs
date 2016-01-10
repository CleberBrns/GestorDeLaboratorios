using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class Unidades_Cadastrar : System.Web.UI.Page
{
    SelecionaDados selecionaDados = new SelecionaDados();
    InsereDados insereDados = new InsereDados();

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

    private void CarregaPagina()
    {
        DataTable dtEstados = selecionaDados.ConsultaEstados(1);

        ddlEstado.DataSource = dtEstados;
        ddlEstado.DataTextField = "Estado";
        ddlEstado.DataValueField = "IdEstado";
        ddlEstado.DataBind();

        ddlEstado.Items.Insert(0, new ListItem("-- Selecione -- ", "0"));
    }

    private void MostrarRetorno(string mensagem, int tipoRetorno)
    {
        if (tipoRetorno == 0)
        {
            imgOk.Visible = true;
            imgErro.Visible = false;
        }
        else
        {
            imgOk.Visible = false;
            imgErro.Visible = true;
        }

        divRetorno.Visible = true;
        lblRetorno.Text = mensagem;
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        divRetorno.Visible = false;

        if (ddlEstado.SelectedValue != "0")
        {
            ddlCidade.Enabled = true;

            DataTable dtCidade = selecionaDados.ConsultaCidades(Convert.ToInt32(ddlEstado.SelectedValue));

            ddlCidade.DataSource = dtCidade;
            ddlCidade.DataTextField = "Cidade";
            ddlCidade.DataValueField = "IdCidade";
            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("-- Selecione -- ", "0"));
        }
        else
        {
            ddlCidade.Enabled = false;
            ddlCidade.SelectedValue = "0";
        }

    }

    protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCidade.SelectedValue != "0")
        {
            divRetorno.Visible = false;
        }
    }

    protected void btCadastrar_Click(object sender, EventArgs e)
    {
        if (ddlEstado.SelectedValue != "0")
        {
            if (ddlCidade.SelectedValue != "0")
            {
                if (!string.IsNullOrEmpty(txtNomeUnidade.Text))
                {
                    try
                    {                        

                        divProcessando.Visible = true;

                        ddlEstado.Enabled = false;
                        ddlCidade.Enabled = false;
                        txtNomeUnidade.Enabled = false;
                        txtNomeUnidade.ReadOnly = true;
                        btMenuPrincipal.Enabled = false;
                        btMenuPrincipal.ToolTip = "Por favor, configure a unidade antes de continuar";
                        btMenuUnidades.Enabled = false;
                        btMenuUnidades.ToolTip = "Por favor, configure a unidade antes de continuar";

                        btConfigurarUnidade.Visible = true;
                        btCadastrar.Visible = false;

                        hddIdUnidade.Value = insereDados.InsereUnidade(txtNomeUnidade.Text.Trim(), Convert.ToInt32(ddlCidade.SelectedValue.Trim()),
                                             Convert.ToInt32(ddlEstado.SelectedValue.Trim())).ToString();//(string unidade, int idCidade, int idEstado)

                        divProcessando.Visible = false;
                        MostrarRetorno("Unidade cadastrada com sucesso. <br/> Por favor, clique em Configurar Unidade", 0);
                    }
                    catch (Exception ex)
                    {
                        RetornaPaginaErro(ex.ToString());
                    }

                }
                else
                {
                    MostrarRetorno("Por favor, preencha o nome da unidade", 1);
                }

            }
            else
            {
                MostrarRetorno("Por favor, selecione uma Cidade", 1);
            }
        }
        else
        {
            MostrarRetorno("Por favor, selecione um Estado", 1);
        }
    }

    protected void btConfigurarUnidade_Click(object sender, EventArgs e)
    {
        lblUnidade.Text = " - Configuração da Unidade " + txtNomeUnidade.Text.Trim();
        
        btConfigurarUnidade.Visible = false;
        divUnidade.Visible = false;
        divRetorno.Visible = false;

        divConfiguraUnidade.Visible = true;

        divCamara.Visible = true;
        txtCamara.Focus();

    }

    protected void btCamara_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtCamara.Text))
        {
            lblCamara.Text = ", Câmara " + txtCamara.Text.Trim();
            txtEstante.Focus();

            try
            {
                divProcessando.Visible = true;

                hddIdCamara.Value = insereDados.InsereCamara(txtCamara.Text, Convert.ToInt32(hddIdUnidade.Value.Trim())).ToString();

                divProcessando.Visible = false;
                MostrarRetorno("Câmara " + txtCamara.Text.Trim() + " cadastrada com sucesso", 0);
                divCamara.Visible = false;
                divEstante.Visible = true;                
            }
            catch (Exception ex)
            {
                RetornaPaginaErro(ex.ToString());
            }

        }
        else
        {
            MostrarRetorno("Para continuar preencha o campo Câmara", 1);
        }
    }

    protected void btEstante_Click(object sender, EventArgs e)
    {
        txtEstante.Focus();
        if (!string.IsNullOrEmpty(txtEstante.Text))
        {
            lblEstante.Text = ", Estante " + txtEstante.Text.Trim();
            txtPrateleiras.Focus();

            try
            {
                divProcessando.Visible = true;

                hddIdEstante.Value = insereDados.InsereEstante(txtEstante.Text.Trim(), Convert.ToInt32(hddIdCamara.Value.Trim())).ToString();

                divProcessando.Visible = false;

                MostrarRetorno("Estante " + txtEstante.Text.Trim() + " cadastrada com sucesso", 0);
                txtPrateleiras.Focus();

                divEstante.Visible = false;
                divPrateleiras.Visible = true;
            }
            catch (Exception ex)
            {
                RetornaPaginaErro(ex.ToString());
            }
        }
        else
        {
            MostrarRetorno("Para continuar preencha o campo Estante", 1);
        }
    }

    protected void btPrateleiras_Click(object sender, EventArgs e)
    {
        txtPrateleiras.Focus();
        if (!string.IsNullOrEmpty(txtPrateleiras.Text))
        {
            try
            {
                divProcessando.Visible = true;

                insereDados.InserePrateleira(Convert.ToInt32(hddIdEstante.Value.Trim()), txtPrateleiras.Text.Trim());

                divProcessando.Visible = false;

                MostrarRetorno("Prateleira " + txtPrateleiras.Text + " cadastrada com sucesso", 0);

                btMenuUnidades.Enabled = true;
                btMenuUnidades.ToolTip = string.Empty;
                btMenuPrincipal.Enabled = true;
                btMenuPrincipal.ToolTip = string.Empty;
                btInicio.Visible = true;
                btNovaEstante.Visible = true;
                btNovaCamara.Visible = true;

                txtPrateleiras.Text = string.Empty;
                txtPrateleiras.Focus();
            }
            catch (Exception ex)
            {
                RetornaPaginaErro(ex.ToString());
            }

        }
        else
        {
            MostrarRetorno("Para continuar preencha o campo Prateleira", 1);
        }
    }

    protected void btNovaCamara_Click(object sender, EventArgs e)
    {
        divRetorno.Visible = false;
        divPrateleiras.Visible = false;
        divEstante.Visible = false;
        divCamara.Visible = true;

        hddIdCamara.Value = string.Empty;
        txtCamara.Text = string.Empty;
        lblCamara.Text = string.Empty;

        hddIdEstante.Value = string.Empty;
        txtEstante.Text = string.Empty;
        lblEstante.Text = string.Empty;
    }

    protected void btNovaEstante_Click(object sender, EventArgs e)
    {
        divRetorno.Visible = false;
        divPrateleiras.Visible = false;
        divEstante.Visible = true;

        hddIdEstante.Value = string.Empty;
        txtEstante.Text = string.Empty;
        lblEstante.Text = string.Empty;

    }

    protected void btInicio_Click(object sender, EventArgs e)
    {
        btMenuUnidades.Visible = true;
        hddIdUnidade.Value = string.Empty;
        hddIdCamara.Value = string.Empty;
        hddIdEstante.Value = string.Empty;

        Response.Redirect("../Unidades/Unidades.aspx");
    }

    protected void btMenuPrincipal_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Home/Home.aspx");
    }

    public void RetornaPaginaErro(string erro)
    {
        Session["ExcessaoDeErro"] = erro.Trim();
        Response.Redirect("../Erro/Erro.aspx");
    }

    protected void btMenuUnidades_Click(object sender, EventArgs e)
    {
        hddIdUnidade.Value = string.Empty;
        hddIdCamara.Value = string.Empty;
        hddIdEstante.Value = string.Empty;

        Response.Redirect("../Unidades/Unidades.aspx");
    }

}