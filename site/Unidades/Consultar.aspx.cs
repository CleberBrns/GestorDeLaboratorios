using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class Auditoria_Auditoria : System.Web.UI.Page
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

        ddlUnidades.DataSource = dtUnidades;
        ddlUnidades.DataTextField = "Unidade";
        ddlUnidades.DataValueField = "IdUnidade";
        ddlUnidades.DataBind();

        ddlUnidades.Items.Insert(0, new ListItem("-- Selecione -- ", "0"));

    }

    protected void ddlUnidades_SelectedIndexChanged(object sender, EventArgs e)
    {
        divRetornoAuditar.Visible = false;
        //btMenuUnidades.Visible = false;

        if (ddlUnidades.SelectedValue != "0")
        {
            btInicio.Visible = true;
            btInicio.Visible = true;
            divUnidades.Visible = false;
            hddIdUnidade.Value = ddlUnidades.SelectedValue;

            lblUnidade.Text = " - Consulta da Unidade " + ddlUnidades.SelectedItem.Text.Trim();

            MostraInfosUnidade(Convert.ToInt32(ddlUnidades.SelectedValue.Trim()));

        }
        else
        {
            divRetornoAuditar.Visible = false;
        }
    }

    protected void btImprimir_Click(object sender, EventArgs e)
    {


    }

    private void MostraInfosUnidade(int idUnidade)
    {
        DataTable dtInfoUnidade = CarregaInfoUnidade(idUnidade);

        if (dtInfoUnidade.Rows.Count > 0)
        {
            //divBotaoAuditoria.Visible = true;          
            divConsulta.Visible = true;
            btImprimir.Visible = false;
            btInicio.Visible = true;

            rptConsulta.DataSource = dtInfoUnidade;
            rptConsulta.DataBind();
        }
        else
        {
            MostraRetorno("Não existem valores cadastrados para essa unidade.");
            imgErroAuditar.Visible = true;
            imgOkAuditar.Visible = false;
        }

    }

    private DataTable CarregaInfoUnidade(int idUnidade)
    {
        DataTable dtInfoUnidade = new DataTable();

        dtInfoUnidade.Columns.Add("Camara");
        dtInfoUnidade.Columns.Add("Estante");
        dtInfoUnidade.Columns.Add("Prateleira");

        DataTable dtConsultaUnidade = selecionaDados.ConsultaUnidade(idUnidade);

        string camaraAntes = string.Empty;
        string estanteAntes = string.Empty;

        if (dtConsultaUnidade.Rows.Count > 0)
        {
            foreach (DataRow item in dtConsultaUnidade.Rows)
            {
                string camara = "||";
                string estante = "||";

                if (item["Camara"].ToString() != camaraAntes)
                {
                    camara = item["Camara"].ToString();
                }

                if (item["Estante"].ToString() != estanteAntes)
                {
                    estante = item["Estante"].ToString();
                }

                dtInfoUnidade.Rows.Add(camara, estante, item["Prateleira"].ToString());

                camaraAntes = item["Camara"].ToString();
                estanteAntes = item["Estante"].ToString();
            }
        }

        return dtInfoUnidade;
    }


    public void MostraRetorno(string mensagem)
    {
        divRetornoAuditar.Visible = true;

        if (string.IsNullOrEmpty(mensagem))
        {
            lblRetornoAuditar.Text = "Por favor, preencha o campo corretamente para prosseguir";
            imgErroAuditar.Visible = true;
            imgOkAuditar.Visible = false;
        }
        else
        {
            lblRetornoAuditar.Text = mensagem;
        }

    }

    protected void btInicio_Click(object sender, EventArgs e)
    {
        ddlUnidades.SelectedValue = "0";
        divUnidades.Visible = true;
        btInicio.Visible = false;

        Response.Redirect("../Unidades/Consultar.aspx");
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

    protected void btMenuUnidades_Click(object sender, EventArgs e)
    {
        hddIdUnidade.Value = string.Empty;
        Response.Redirect("../Unidades/Unidades.aspx");
    }
}