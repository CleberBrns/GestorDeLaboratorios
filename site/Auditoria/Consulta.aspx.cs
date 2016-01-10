using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class Auditoria_Consulta : System.Web.UI.Page
{
    SelecionaDados selecionaDados = new SelecionaDados();
    InsereDados insereDados = new InsereDados();

    protected void Page_Load(object sender, EventArgs e)
    {
        txtOpcoes.Focus();

        try
        {
            if (!IsPostBack)
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
        if (Session["SessionIdTipoAcesso"].ToString() == "1")//Adm
        {
            btMenuPrincial.Visible = true;
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

    private void CamposDefault()
    {
        hddCodPrateleira.Value = string.Empty;
        txtAmostra.Text = string.Empty;
        txtPrateleira.Text = string.Empty;
        lblRetorno.Text = string.Empty;
        lblBusca.Text = string.Empty;
        txtOpcoes.Text = string.Empty;
        txtOpcoes.Focus();
    }

    protected void btInicio_Click(object sender, EventArgs e)
    {
        CamposDefault();
        divCodAcoes.Visible = true;
        divOpcoes.Visible = true;
        divInicio.Visible = false;       
        divAmostra.Visible = false;
        divPrateleira.Visible = false;
        divConsulta.Visible = false;
        divRetorno.Visible = false;
        btImprimir.Visible = false;
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

                        MostraConsulta(txtAmostra.Text.Trim(), 1);

                        divOpcoes.Visible = false;
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


    protected void btPrateleira_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtPrateleira.Text))
        {
            MostraRetorno(string.Empty);
        }
        else
        {
            DataTable dtInfoPrateleira = selecionaDados.ConsultaPrateleira(txtPrateleira.Text.Trim());

            if (dtInfoPrateleira.Rows.Count > 0)
            {
                lblBusca.Text = " - Prateleira " + txtPrateleira.Text.Trim();
                divPrateleira.Visible = false;

                hddIdPrateleira.Value = dtInfoPrateleira.DefaultView[0]["IdPrateleira"].ToString();

                hddCodPrateleira.Value = txtPrateleira.Text.Trim();
                MostraConsulta(txtPrateleira.Text, 2);

                divOpcoes.Visible = false;
                divInicio.Visible = true;

            }
            else
            {
                MostraRetorno("A prateleira " + txtPrateleira.Text + " não foi está cadastrada!");
                txtPrateleira.Text = string.Empty;
                txtPrateleira.Focus();
                imgErro.Visible = true;
                imgOk.Visible = false;
            }
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

    private void MostraConsulta(string codConsulta, int idTipoConsulta)
    {
        DataTable dtConsulta = CarregaInfoConsulta(codConsulta, idTipoConsulta);

        if (dtConsulta.Rows.Count > 0)
        {
            divConsulta.Visible = true;            
            divInicio.Visible = true;           
            divOpcoes.Visible = false;
            divRetorno.Visible = false;
            lblRetorno.Text = string.Empty;

            if (idTipoConsulta == 2)
            {
                btImprimir.Visible = true;
                Session["SessionTipoImpressao"] = "Consulta";
                Session["SessionPrateleira"] = hddCodPrateleira.Value;
            }

            rptConsulta.DataSource = dtConsulta;
            rptConsulta.DataBind();
        }
        else
        {
            if (idTipoConsulta == 1)
            {
                MostraRetorno("A amostra " + codConsulta + " não foi cadastrada!");
                txtAmostra.Text = string.Empty;
                txtAmostra.Focus();
                divAmostra.Visible = true;
            }
            else
            {
                MostraRetorno("Não existem amostras cadastradas na prateleira " + codConsulta + " !");
                txtPrateleira.Text = string.Empty;
                txtPrateleira.Focus();
                divPrateleira.Visible = true;
            }

            imgErro.Visible = true;
            imgOk.Visible = false;
        }

    }

    /// <summary>
    /// idTipoConsulta 1 = Amostra
    /// idTipoConsulta 2 = Prateleira
    /// </summary>
    /// <param name="codConsulta"></param>
    /// <param name="idTipoConsulta"></param>
    /// <returns></returns>
    private DataTable CarregaInfoConsulta(string codConsulta, int idTipoConsulta)
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
        dtInfoEstrutura.Columns.Add("IdAcao");

        DataTable dtInfoConsulta = new DataTable();

        if (idTipoConsulta == 1)//Amostra
        {
            dtInfoConsulta = selecionaDados.ConsultaStatusAmostra(Convert.ToInt64(codConsulta));

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
        }
        else//Prateleira
        {
            dtInfoConsulta = selecionaDados.ConsultaPrateleiraAuditoria(codConsulta);

            dtInfoConsulta.DefaultView.RowFilter = "IdAcao in (1,3)";
            dtInfoConsulta = dtInfoConsulta.DefaultView.ToTable();

            if (dtInfoConsulta.Rows.Count > 0)
            {
                foreach (DataRow item in dtInfoConsulta.Rows)
                {
                    dtInfoEstrutura.Rows.Add(item["CodAmostra"].ToString(),
                        ConfiguraUsuarioRecepcao(item["DataRecepcao"].ToString(), item["UsuarioRecepcao"].ToString()), item["Estante"].ToString(),
                        item["Prateleira"].ToString(), item["Caixa"].ToString(),
                        ConfiguraUltimaAlteracao(item["NomeUsuario"].ToString(), item["DataAtualizacao"].ToString(), item["UltimaAlteracao"].ToString()),
                        item["NomeLaboratorio"].ToString(), item["Auditoria"].ToString(), item["IdAcao"].ToString());
                }
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

    protected void btOpcoes_Click(object sender, EventArgs e)
    {
        if (txtOpcoes.Text.Trim() == "01")
        {
            btDivAmostra_Click();
        }
        else if(txtOpcoes.Text.Trim() == "02")
        {
            btDivPrateleira_Click();
        }
        else
        {
            txtOpcoes.Text = string.Empty;
            txtOpcoes.Focus();
            MostraRetorno("Opção desconhecida. <br /> Por favor, consulte o administrador do sistema.");
            imgErro.Visible = true;
            imgOk.Visible = false;
        }
    }

    private void btDivAmostra_Click()
    {
        txtAmostra.Focus();
        divAmostra.Visible = true;
        divInicio.Visible = true;
        divOpcoes.Visible = false;
        divCodAcoes.Visible = false;
    }

    private void btDivPrateleira_Click()
    {
        txtPrateleira.Focus();
        divPrateleira.Visible = true;
        divInicio.Visible = true;
        divOpcoes.Visible = false;
        divCodAcoes.Visible = false;
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