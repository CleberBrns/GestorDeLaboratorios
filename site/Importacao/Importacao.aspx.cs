using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;
using Excel;

public partial class Importacao_Importacao : System.Web.UI.Page
{
    SelecionaDados selecionaDados = new SelecionaDados();

    protected void Page_Load(object sender, EventArgs e)
    {
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
    }

    protected void btUpload_Click(object sender, EventArgs e)
    {
        string extensao = System.IO.Path.GetExtension(fUpload.FileName);

        if (fUpload.HasFile)
        {
            if (extensao.ToLower() == ".xls" || extensao.ToLower() == ".xlsx")
            {
                DataTable dtDadosUpload = new DataTable();
                string FileName = Server.HtmlEncode(fUpload.FileName);
                string caminhoArquivo = string.Empty;
                //Caminho local
                //string caminhoDestino = Server.MapPath("/ArquivosTemp/");
                //Caminho para o servidor
                string caminhoDestino = @"C:\camarafria\ArquivosTemp\";

                if (Directory.Exists(caminhoDestino))
                {
                    string arquivoComCaminho = caminhoDestino + fUpload.FileName;

                    if (File.Exists(arquivoComCaminho))
                        File.Delete(arquivoComCaminho);

                    try
                    {
                        fUpload.SaveAs(arquivoComCaminho);

                        if (File.Exists(arquivoComCaminho))
                        {
                            using (FileStream stream = File.Open(arquivoComCaminho, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                IExcelDataReader excelReader;

                                if (extensao.ToLower() == ".xls")
                                {
                                    //Reading from a binary Excel file ('97-2003 format; *.xls)
                                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                                }
                                else
                                {
                                    //Reading from a OpenXml Excel file (2007+ format; *.xlsx)
                                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                }

                                //DataSet - Create column names from first row
                                excelReader.IsFirstRowAsColumnNames = true;
                                DataSet dsResult = excelReader.AsDataSet();
                                dtDadosUpload = dsResult.Tables[0];

                                //Free resources (IExcelDataReader is IDisposable)
                                excelReader.Close();

                                CarregaDadosConteudoImportacao(dtDadosUpload);

                            }

                            //Apaga o arquivo temporário
                            File.Delete(arquivoComCaminho);
                        }
                        else
                        {
                            MostraRetorno("Falha ao importar o arquivo.<br/> " +
                            "Por favor, contate o administrador do sistema. <br>", 2);
                        }
                    }
                    catch (Exception ex)
                    {
                        MostraRetorno("Ocorreu um erro durante o processo. <br/> " +
                            "Por favor, contate o administrador do sistema e informe a seguinte mensagem; <br> " + ex.ToString() + "", 2);
                    }
                }
                else
                {
                    MostraRetorno("Falha ao encontrar o caminho para salvar o arquivo.<br/> " +
                            "Por favor, contate o administrador do sistema. <br>" +
                             "Pasta Destino; " + Server.MapPath("/ArquivosTemp/") + "", 2);
                }
            }
            else
            {
                divProcessando.Visible = false;
                MostraRetorno("Formato de arquivo não suportado! <br/> Selecione somente arquivos .xls e .xlsx.", 2);
            }

        }
        else
        {
            MostraRetorno("Por favor, selecione um arquivo para importaçao.", 2);
        }
    }

    private void CarregaDadosConteudoImportacao(DataTable dtConteudoUpload)
    {
        try
        {
            hddNomeArquivo.Value = fUpload.FileName;
            Session["SessionNomeArquivo"] = hddNomeArquivo.Value;
            divProcessando.Visible = true;

            string amostrasConteudoUP = ExtraiConteudo(dtConteudoUpload);

            if (!string.IsNullOrEmpty(amostrasConteudoUP))
            {
                DataTable dtConsultaImportacao = VerificaAmostras(amostrasConteudoUP);
                MostraConsulta(dtConsultaImportacao);

                divProcessando.Visible = false;
            }
            else
            {
                divProcessando.Visible = false;
                MostraRetorno("Não foram encontradas informações válidas no arquivo. <br/> " +
                    " Por favor, revise o arquivo com o <br/> Administrador do projeto.", 2);
            }

        }
        catch (Exception ex)
        {
            divProcessando.Visible = false;
            MostraRetorno("Falha durante o processo. <br/> Por favor, contate o Administrador do Sistema e informe a seguinte mensagem; <br/> " +
                           ex.ToString() + "", 2);
        }

    }

    private void MostraConsulta(DataTable dtMostrarConsulta)
    {
        if (dtMostrarConsulta.Rows.Count > 0)
        {
            btImprimir.Visible = true;
            divOpcoes.Visible = true;
            divRetorno.Visible = false;
            divUpload.Visible = false;
            lblRetorno.Text = string.Empty;

            dtMostrarConsulta.DefaultView.Sort = "Prateleira desc";
            Session.Add("SessionRetornoImportacao", dtMostrarConsulta.DefaultView.ToTable());

            rptConsulta.DataSource = dtMostrarConsulta.DefaultView;
            rptConsulta.DataBind();
        }
        else
        {
            MostraRetorno("Sem dados para verificar ou <br/> campo de verificação não configurado corretamente.", 2);

            imgErro.Visible = true;
            imgOk.Visible = false;
        }
    }

    private DataTable VerificaAmostras(string amostrasConteudoUP)
    {
        DataTable dtInfoEstrutura = new DataTable();

        dtInfoEstrutura.Columns.Add("CodAmostra");        
        dtInfoEstrutura.Columns.Add("Prateleira");
        dtInfoEstrutura.Columns.Add("Caixa");
        dtInfoEstrutura.Columns.Add("UltimaAlteracao");
        dtInfoEstrutura.Columns.Add("Laboratorio");

        DataTable dtInfoConsulta = new DataTable();

        string[] aAmostras = amostrasConteudoUP.Split('|');
        foreach (var item in aAmostras)
        {
            if (ValidaCampoAmostra(item))
            {
                DataTable dtAmostra = selecionaDados.ConsultaStatusAmostra(Convert.ToInt64(item));
                if (dtAmostra.Rows.Count > 0)
                {
                    string prateleira = dtAmostra.DefaultView[0]["Prateleira"].ToString();
                    if (prateleira.Trim() == "-" )
                    {
                        prateleira = string.Empty;
                    }
                    dtInfoEstrutura.Rows.Add(item.ToString(), prateleira, dtAmostra.DefaultView[0]["caixa"].ToString(),
                        dtAmostra.DefaultView[0]["UltimaAlteracao"].ToString(), dtAmostra.DefaultView[0]["NomeLaboratorio"].ToString());
                }
                else
                {
                    dtInfoEstrutura.Rows.Add(item.ToString(), string.Empty, string.Empty, "Não Cadastrada", string.Empty);
                }
            }
            else
            {
                dtInfoEstrutura.Rows.Add("Formato não suportado", string.Empty, string.Empty, string.Empty, string.Empty);
            }
        }

        return dtInfoEstrutura;
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

    private string ExtraiConteudo(DataTable dtConteudoUpload)
    {
        string amostrasConteudo = string.Empty;
        foreach (DataRow item in dtConteudoUpload.Rows)
        {
            amostrasConteudo += item["Código da Embalagem"].ToString() + "|";
        }

        //Remove ultimo pipe
        if (!string.IsNullOrEmpty(amostrasConteudo))
        {
            amostrasConteudo = amostrasConteudo.Remove(amostrasConteudo.Length - 1);
        }

        return amostrasConteudo;
    }

    public void MostraRetorno(string mensagem, int tipoRetorno)
    {
        divRetorno.Visible = true;

        if (tipoRetorno == 1)
        {
            imgOk.Visible = true;
            imgErro.Visible = false;
        }
        else
        {
            imgErro.Visible = true;
            imgOk.Visible = false;

            btMostrarConsulta.Attributes.Add("class", "none");
            btEsconderConsulta.Attributes.Add("class", "none");
            btImprimir.Visible = false;
        }

        if (string.IsNullOrEmpty(mensagem))
        {
            lblRetorno.Text = "Por favor, preencha o campo corretamente para prosseguir";
            imgErro.Visible = true;
        }
        else
        {
            lblRetorno.Text = mensagem;
        }
    }

    private void CamposDefault()
    {
        lblRetorno.Text = string.Empty;
        lblBusca.Text = string.Empty;
        hddNomeArquivo.Value = string.Empty;

        DataTable tabelaLimpa = new DataTable();
        rptConsulta.DataSource = tabelaLimpa;
        rptConsulta.DataBind();
    }

    protected void btInicio_Click(object sender, EventArgs e)
    {
        CamposDefault();
        divUpload.Visible = true;
        divOpcoes.Visible = false;
        divConsulta.Attributes.Add("class", "none");
        divRetorno.Visible = false;
        btImprimir.Visible = false;
    }

    protected void btImprimir_Click(object sender, EventArgs e)
    {
        Session["SessionNomeArquivo"] = hddNomeArquivo.Value;
        Response.Write("<script>window.open('../Importacao/Impressao.aspx','_blank')</script");
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