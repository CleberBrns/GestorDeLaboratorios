using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class Unidades_Gerenciar : System.Web.UI.Page
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
        DataTable dtUnidades = selecionaDados.ConsultaTodasUnidades();

        ddlUnidades.DataSource = dtUnidades ;
        ddlUnidades.DataTextField = "Unidade";
        ddlUnidades.DataValueField = "IdUnidade";
        ddlUnidades.DataBind();

        ddlUnidades.Items.Insert(0, new ListItem("-- Selecione -- ", "0"));
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

    protected void ddlUnidades_SelectedIndexChanged(object sender, EventArgs e)
    {
        divRetorno.Visible = false;
        btMenuUnidades.Visible = false;

        if (ddlUnidades.SelectedValue != "0")
        {
            btInicio.Visible = true;
            divOpcoes.Visible = true;         
            divUnidades.Visible = false;
            hddIdUnidade.Value = ddlUnidades.SelectedValue;

            lblUnidade.Text = " - Configuração da Unidade " + ddlUnidades.SelectedItem.Text.Trim();                    
            
        }
        else
        {
            divRetorno.Visible = false;
        }
    }

    protected void ddlCamaras_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCamaras.SelectedValue != "0")
        {
            hddIdCamara.Value = ddlCamaras.SelectedValue;
            divDropCamara.Visible = false;
            divEstante.Visible = true;

            lblCamara.Text = ", Câmara " + ddlCamaras.SelectedValue;
            txtEstante.Focus();
        }
        else
        {
            divRetorno.Visible = false;
        }

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

                btInicio.Visible = true;
                btInicio.Enabled = false;
                btInicio.ToolTip = "Por favor, finalize a configuração antes de continuar";

                btMenuPrincipal.Enabled = false;
                btMenuPrincipal.ToolTip = "Por favor, finalize a configuração antes de continuar";

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

                btMenuPrincipal.Enabled = true;
                btInicio.Enabled = true;                
                btInicio.Visible = true;

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
        divConfiguraUnidade.Visible = true;
        divOpcoes.Visible = false;
        divCamara.Visible = true;

        hddIdCamara.Value = string.Empty;
        txtCamara.Text = string.Empty;
        txtCamara.Focus();
        lblCamara.Text = string.Empty;

        hddIdEstante.Value = string.Empty;
        txtEstante.Text = string.Empty;
        lblEstante.Text = string.Empty;
    }

    protected void btNovaEstante_Click(object sender, EventArgs e)
    {
        DataTable dtCamara = selecionaDados.ConsultaCamarasUnidade(Convert.ToInt32(hddIdUnidade.Value));

        if (dtCamara.Rows.Count > 0)
        {
            ddlCamaras.DataSource = dtCamara;
            ddlCamaras.DataTextField = "NomeCamara";
            ddlCamaras.DataValueField = "IdCamara";
            ddlCamaras.DataBind();

            ddlCamaras.Items.Insert(0, new ListItem("-- Selecione -- ", "0"));

            divOpcoes.Visible = false;
            divRetorno.Visible = false;
            divPrateleiras.Visible = false;
            divEstante.Visible = false;
            divConfiguraUnidade.Visible = true;
            divDropCamara.Visible = true;

            hddIdEstante.Value = string.Empty;
            txtEstante.Text = string.Empty;
            lblEstante.Text = string.Empty;
        }
        else
        {
            MostrarRetorno("Não existem Câmaras cadastradas para essa a Unidade selecionada", 1);
        }

    }

    protected void btInicio_Click(object sender, EventArgs e)
    {
        btMenuUnidades.Visible = true;
        hddIdUnidade.Value = string.Empty;
        hddIdCamara.Value = string.Empty;
        hddIdEstante.Value = string.Empty;

        Response.Redirect("../Unidades/Gerenciar.aspx");
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