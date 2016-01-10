using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;
using System.Xml.Linq;
using System.Configuration;

public partial class Acoes_Saida : System.Web.UI.Page
{
    SelecionaDados selecionaDados = new SelecionaDados();
    InsereDados insereDados = new InsereDados();

    #region Inicio/Load

    protected void Page_Load(object sender, EventArgs e)
    {
        txtAmostra.Focus();

        try
        {
            if (!IsPostBack)
            {
                if (Session["SessionUsuario"].ToString() != string.Empty)
                {
                    if (Session["SessionIdTipoAcesso"].ToString() == "1")//Adm
                    {
                        btMenuPrincial.Visible = true;
                    }

                    if (!IsPostBack)
                        CarregaPagina();
                }
                else
                {
                    RetornaPaginaErro("Sessão perdida. Por favor, faça o login novamente.");
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
        hddIdUnidade.Value = Session["SessionIdUnidade"].ToString();
        hddIdUsuario.Value = Session["SessionIdUsuario"].ToString();

        txtLaboratorio.Focus();
    }

    #endregion

    #region Laboratórios

    protected void btLaboratorio_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLaboratorio.Text))
        {
            MostraRetorno(string.Empty);
        }
        else
        {
            try
            {
                DataTable dtLaboratorio = selecionaDados.ConsultaLaboratorio();

                if (dtLaboratorio.Rows.Count > 0)
                {
                    dtLaboratorio.DefaultView.RowFilter = "IdTipoStatus = 1 and IdUnidade = " + hddIdUnidade.Value + " and CodLaboratorio = '" + txtLaboratorio.Text.Trim() + "'";
                    dtLaboratorio = dtLaboratorio.DefaultView.ToTable();

                    if (dtLaboratorio.Rows.Count > 0)
                    {
                        string complementoLabDD = string.Empty;
                        hddIdLaboratorio.Value = dtLaboratorio.DefaultView[0]["IdLaboratorio"].ToString();

                        if (!string.IsNullOrEmpty(hddIdLaboratorio.Value.Trim()))
                        {
                            if (validaLabDescarteDireto(Convert.ToInt32(hddIdLaboratorio.Value.Trim())))
                            {
                                hddBoolLabDescarte.Value = "1";
                                complementoLabDD = " - Descarte Direto";
                            }
                            else
                            {
                                hddBoolLabDescarte.Value = "0";
                                complementoLabDD = string.Empty;
                            }
                        }

                        lblLaboratorio.Text = " - Laboratório " + dtLaboratorio.DefaultView[0]["Nome"].ToString() + complementoLabDD;

                        divRetorno.Visible = false;
                        lblRetorno.Text = string.Empty;
                        divLaboratorio.Visible = false;
                        divInsercoes.Visible = true;
                        btNovoLaboratorio.Visible = true;
                        divInicio.Visible = true;
                        txtAmostra.Focus();
                    }
                    else
                    {
                        divRetorno.Visible = true;
                        imgOk.Visible = false;
                        imgErro.Visible = true;
                        lblRetorno.Text = "Laboratório não cadastrado. <br/> Favor consultar o Administrador do Sistema";
                        txtLaboratorio.Text = string.Empty;
                        txtLaboratorio.Focus();
                    }

                }
                else
                {
                    divRetorno.Visible = true;
                    imgOk.Visible = false;
                    imgErro.Visible = true;
                    lblRetorno.Text = "Laboratório não cadastrado. <br/> Favor consultar o Administrador do Sistema";
                    txtLaboratorio.Text = string.Empty;
                    txtLaboratorio.Focus();
                }

            }
            catch (Exception ex)
            {
                RetornaPaginaErro(ex.ToString());
            }
        }
    }

    #region  XML LabsDescarteDireto

    public string CaminhoPastaXML()
    {
        //Caminho local
        string caminhoPastaXml = Server.MapPath("/ArquivosPermanentes/LabsDescarteDireto.xml");

        if (ConfigurationManager.AppSettings.Get("CaminhoPastaXMLLabsDD") == "1")
        {
            //Caminho para o servidor
            caminhoPastaXml = @"C:\camarafria\ArquivosPermanentes\LabsDescarteDireto.xml";
        }

        return caminhoPastaXml;
    }

    public List<Laboratorio> ListarLabsDescarteDireto()
    {
        List<Laboratorio> labsDescarteDireto = new List<Laboratorio>();

        if (File.Exists(CaminhoPastaXML()))
        {
            try
            {
                XElement xml = XElement.Load(CaminhoPastaXML());
                foreach (XElement x in xml.Elements())
                {
                    if (!string.IsNullOrEmpty(x.Value.Trim()))
                    {
                        Laboratorio lab = new Laboratorio()
                        {
                            PkIdLaboratorio = int.Parse(x.Value.Trim()),
                        };
                        labsDescarteDireto.Add(lab);
                    }
                }
            }
            catch (Exception ex) { }//Caso o arquivo seja apagado não será exibido um erro  
        }

        return labsDescarteDireto;
    }

    public bool validaLabDescarteDireto(int pkIdLaboratorio)
    {
        bool labDescarte = false;

        try
        {
            List<Laboratorio> labsDD = ListarLabsDescarteDireto();

            if (labsDD.Count > 0)
            {
                foreach (Laboratorio lab in labsDD)
                {
                    if (lab.PkIdLaboratorio == pkIdLaboratorio)
                    {
                        labDescarte = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Erro ao verificar o Descarte Direto. As Amostras saíram no padrão normal.');", true);
        }

        return labDescarte;
    }

    #endregion

    protected void btNovoLaboratorio_Click(object sender, EventArgs e)
    {
        hddIdLaboratorio.Value = string.Empty;
        txtLaboratorio.Text = string.Empty;
        lblLaboratorio.Text = string.Empty;
        txtLaboratorio.Focus();

        divRetorno.Visible = false;
        divInsercoes.Visible = false;
        divInicio.Visible = false;
        divLaboratorio.Visible = true;
    }

    #endregion

    #region Amostra

    protected void btAmostra_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtAmostra.Text))
        {
            MostraRetorno(string.Empty);
        }
        else
        {
            try
            {
                bool formatoCorreto = ValidaCampoAmostra(txtAmostra.Text.Trim());

                if (formatoCorreto)
                {
                    InsereAmostraSaida(txtAmostra.Text.Trim());
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
                MostraRetorno("Ocorreu um erro ao tentar executar a Saída da amostra; " + txtAmostra.Text.Trim());
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

    private void InsereAmostraSaida(string sCodAmostra)
    {
        try
        {
            divProcessando.Visible = true;
            divInsercoes.Visible = false;

            long codAmostra = Convert.ToInt64(sCodAmostra);

            DataTable dtStatusAmos = selecionaDados.ConsultaStatusAmostra(codAmostra);
            if (dtStatusAmos.Rows.Count > 0)
            {
                string statusAmostra = string.Empty;

                statusAmostra = dtStatusAmos.DefaultView[0]["UltimaAlteracao"].ToString();

                if (statusAmostra != string.Empty && statusAmostra.ToLower() == "descarte")
                {
                    MostraRetornoErro("A amostra " + sCodAmostra + " foi descartada <br/> e já não pode passar por qualquer nova Ação.");
                    divProcessando.Visible = false;
                    txtAmostra.Text = string.Empty;
                    txtAmostra.Focus();
                }
                else if (statusAmostra != string.Empty && statusAmostra.ToLower() == "saída")
                {
                    MostraRetornoErro("A amostra " + sCodAmostra + " já passou por essa Ação <br/> e ainda não foi retornada para alguma prateleira.");
                    divProcessando.Visible = false;
                    txtAmostra.Text = string.Empty;
                    txtAmostra.Focus();
                }
                else
                {
                    int idPrateleira = Convert.ToInt32(dtStatusAmos.DefaultView[0]["IdPrateleira"].ToString());

                    insereDados.InsereAmostraSaida(idPrateleira, Convert.ToInt32(hddIdUsuario.Value.Trim()), codAmostra, string.Empty,
                                                   Convert.ToInt32(hddIdLaboratorio.Value.Trim()));

                    if (hddBoolLabDescarte.Value == "1")
                    {
                        if (DescartaAmostra(codAmostra))
                        {
                            MostraRetorno("Saída e Descarte da amostra executados com sucesso.");
                        }
                        else
                        {
                            MostraRetorno("Saída da amostra executada com sucesso,</br> mas devido a um erro não foi possivel executar o Descarte.");
                        }
                    }
                    else
                    {
                        MostraRetorno("Saída da amostra executada com sucesso.");
                    }


                    imgOk.Visible = true;
                    imgErro.Visible = false;

                    txtAmostra.Text = string.Empty;
                    divProcessando.Visible = false;
                    divInsercoes.Visible = true;
                }
            }
            else
            {
                MostraRetornoErro("A amostra " + sCodAmostra + " ainda não foi cadastrada, <br /> A mesma deve passar pela a ação de Recepção." +
                    "<br /> Qualquer dúvida, por favor, consulte o administrador do sistema");
                txtAmostra.Text = string.Empty;
                txtAmostra.Focus();
            }
        }
        catch (Exception ex)
        {
            MostraRetornoErro("Ocorreu um erro ao tentar executar a Saída da amostra. <br /> Por favor, consulte o administrador do sistema");
        }

    }

    private bool DescartaAmostra(long codAmostra)
    {
        bool descarteOk = false;

        try
        {
            DataTable dtStatusAmos = selecionaDados.ConsultaStatusAmostra(codAmostra);

            if (dtStatusAmos.Rows.Count > 0)
            {
                string statusAmostra = string.Empty;
                statusAmostra = dtStatusAmos.DefaultView[0]["UltimaAlteracao"].ToString();

                if (statusAmostra != string.Empty && statusAmostra.ToLower() != "descarte")
                {
                    int idPrateleira = Convert.ToInt32(dtStatusAmos.DefaultView[0]["IdPrateleira"].ToString());

                    insereDados.InsereAmostraDescarte(idPrateleira, Convert.ToInt32(hddIdUsuario.Value.Trim()),
                                                      codAmostra, string.Empty);
                    descarteOk = true;
                }

            }
        }
        catch (Exception ex) { }

        return descarteOk;
    }

    #endregion

    #region Métodos Gerais

    private void MostraRetornoErro(string mensagem)
    {
        divProcessando.Visible = false;
        divInsercoes.Visible = true;

        MostraRetorno(mensagem);

        imgOk.Visible = false;
        imgErro.Visible = true;
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

    #endregion
}