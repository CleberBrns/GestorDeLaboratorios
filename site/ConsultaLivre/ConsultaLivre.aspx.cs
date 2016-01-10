using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class ConsultaLivre : System.Web.UI.Page
{
    SelecionaDados selecionaDados = new SelecionaDados();
    InsereDados insereDados = new InsereDados();

    protected void Page_Load(object sender, EventArgs e)
    {
        txtAmostra.Focus();
    }

    private void CamposDefault()
    {
        lblRetorno.Text = string.Empty;
        lblBusca.Text = string.Empty;
        txtAmostra.Text = string.Empty;
        txtAmostra.Focus();
    }

    protected void btInicio_Click(object sender, EventArgs e)
    {
        CamposDefault();
        divInicio.Visible = false;
        divConsulta.Visible = false;
        divRetorno.Visible = false;
        divAmostra.Visible = true;
    }

    protected void btAmostra_Click(object sender, EventArgs e)
    {
        try
        {
            divRetorno.Visible = true;

            if (!string.IsNullOrEmpty(txtAmostra.Text))
            {
                try
                {
                    bool formatoCorreto = ValidaCampoAmostra(txtAmostra.Text.Trim());

                    if (formatoCorreto)
                    {
                        divProcessando.Visible = true;

                        long codAmostra = Convert.ToInt64(txtAmostra.Text.Trim());

                        MostraConsulta(txtAmostra.Text.Trim());

                        divInicio.Visible = true;
                        divProcessando.Visible = false;
                        txtAmostra.Text = string.Empty;
                    }
                    else
                    {
                        MostraRetorno("O campo Amostra só aceita caracteres numéricos. <br /> Por favor, consulte o administrador do sistema.");
                        imgErro.Visible = true;
                        imgOk.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    divProcessando.Visible = false;
                    imgErro.Visible = true;
                    imgOk.Visible = false;
                    lblRetorno.Text = ex.ToString();
                }

            }
            else
            {
                imgErro.Visible = true;
                imgOk.Visible = false;
                lblRetorno.Text = "Por favor, preencha o campo corretamente para prosseguir";
            }

        }
        catch (Exception ex)
        {
            RetornaPaginaErro(ex.ToString());
        }
    }

    private bool ValidaCampoAmostra(string codAmostra)
    {
        bool valido = false;

        try
        {
            long dCodAmostra = Convert.ToInt64(codAmostra.Trim());
            valido = true;
        }
        catch (Exception) { }//Continua false

        return valido;
    }

    private void MostraConsulta(string codConsulta)
    {
        DataTable dtConsulta = CarregaInfoConsulta(codConsulta);

        if (dtConsulta.Rows.Count > 0)
        {
            divConsulta.Visible = true;
            divInicio.Visible = true;
            divRetorno.Visible = false;
            lblRetorno.Text = string.Empty;

            rptConsulta.DataSource = dtConsulta;
            rptConsulta.DataBind();
        }
        else
        {

            MostraRetorno("A amostra " + codConsulta + " não foi cadastrada!");
            txtAmostra.Text = string.Empty;
            txtAmostra.Focus();
            divAmostra.Visible = true;
            imgErro.Visible = true;
            imgOk.Visible = false;
        }

    }

    private DataTable CarregaInfoConsulta(string codConsulta)
    {
        DataTable dtInfoEstrutura = new DataTable();

        dtInfoEstrutura.Columns.Add("CodAmostra");
        dtInfoEstrutura.Columns.Add("DataUsuarioRecepcao");
        dtInfoEstrutura.Columns.Add("Estante");
        dtInfoEstrutura.Columns.Add("Prateleira");
        dtInfoEstrutura.Columns.Add("Caixa");
        dtInfoEstrutura.Columns.Add("UltimaAlteracao");
        dtInfoEstrutura.Columns.Add("Laboratorio");
        dtInfoEstrutura.Columns.Add("Auditado");

        DataTable dtInfoConsulta = selecionaDados.ConsultaStatusAmostra(Convert.ToInt64(codConsulta));

        if (dtInfoConsulta.Rows.Count > 0)
        {
            foreach (DataRow item in dtInfoConsulta.Rows)
            {
                dtInfoEstrutura.Rows.Add(item["CodAmostra"].ToString(),
                    ConfiguraUsuarioRecepcao(item["DataRecepcao"].ToString(), item["UsuarioRecepcao"].ToString()), item["Estante"].ToString(),
                    item["Prateleira"].ToString(), item["Caixa"].ToString(),
                    ConfiguraUltimaAlteracao(item["NomeUsuario"].ToString(), item["DataAtualizacao"].ToString(), item["UltimaAlteracao"].ToString()),
                    item["NomeLaboratorio"].ToString(), item["Auditoria"].ToString());
            }
        }


        return dtInfoEstrutura;
    }

    private object ConfiguraUsuarioRecepcao(string dataRecepcao, string usuarioRecepcao)
    {
        return (usuarioRecepcao + " - " + Convert.ToDateTime(dataRecepcao).ToShortDateString() + " " + Convert.ToDateTime(dataRecepcao).ToShortTimeString());
    }

    private object ConfiguraUltimaAlteracao(string nomeUsuario, string dataAtualizacao, string acao)
    {
        if (!string.IsNullOrEmpty(dataAtualizacao))
        {
            string data = Convert.ToDateTime(dataAtualizacao).ToShortDateString() + " " +
            Convert.ToDateTime(dataAtualizacao).ToShortTimeString();

            return (nomeUsuario + " - " + data + " - " + acao);
        }
        else
        {
            return string.Empty;
        }
    }

    public void MostraRetorno(string mensagem)
    {
        divRetorno.Visible = true;

        if (string.IsNullOrEmpty(mensagem))
        {
            lblRetorno.Text = "Por favor, preencha o campo corretamente para prosseguir";
            imgErro.Visible = true;
            imgOk.Visible = false;
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

    protected void btMenuAcoes_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Acoes/Acoes.aspx");
    }

}