<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Acoes.aspx.cs" Inherits="Acoes_Acoes" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entrada</title>
    <link href="../Styles/Acoes.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="hddTestPost" />
        <input type="hidden" runat="server" id="hddIdUnidade" value="0" />
        <input type="hidden" runat="server" id="hddInclusoes" />
        <input type="hidden" id="hddErro" runat="server" />
        <div class="pagina" runat="server" id="divPagina">
            <div runat="server" id="divLogo" style="text-align: center">
                <%--<img src="../Imagens/logo_final_alscorplab.jpg" width="352px" />--%>
            </div>
            <h2>Ações</h2>
            <div style="padding-bottom: 3%;">
                <div class="insercoes">
                    <div style="margin-top: 3%;">
                        Aguardando a&ccedil;&atilde;o...   
                    <asp:Panel runat="server" DefaultButton="btAcao" style="margin-top: 10px">
                        <asp:TextBox runat="server" ID="txtAcao" Width="180px" Height="25px" autocomplete="off" />
                        <div style="display: none">
                            <asp:Button runat="server" ID="btAcao" OnClick="btAcao_Click" />
                        </div>
                    </asp:Panel>
                    </div>
                </div>
                <div style="margin-top: 3%; margin-bottom: 3%; text-align: center; font-size: 18px; color: red;" runat="server" id="divRetorno" visible="false">
                    <div style="margin-bottom: 3%;">
                        <asp:Image runat="server" ID="imgErro" ImageUrl="../Imagens/error.png" Visible="false" Width="8%" />
                    </div>
                    <asp:Label runat="server" ID="lblRetorno" />
                </div>
            </div>
            <div class="rodape">
            </div>
            <div runat="server" id="divCodAcoes" visible="false" style="text-align: center">
                <div>
                    <img src="../Imagens/Acao-Recepcao.png" />
                    <img src="../Imagens/Acao-Entrada.png" />
                </div>
                <div>
                    <img src="../Imagens/Acao-Saida.png" />
                    <img src="../Imagens/Acao-Descarte.png" />
                </div>
                <div>
                    <img runat="server" id="imgAcaoAuditoria" visible="false" src="../Imagens/Acao-Auditoria.png" />
                    <img src="../Imagens/Acao-Consultar.png" />
                </div>
                <div>
                    <img src="../Imagens/Acao-ImportarArquivo.png" />
                </div>
            </div>
            <div class="rodape" style="margin-bottom: 3%;">
            </div>
            <div class="contBt">
                <asp:Button runat="server" ID="btMenuPrincial" OnClick="btMenuPrincipal_Click" CssClass="bt" Text="Menu Principal" Visible="false" />
                <asp:Button runat="server" ID="btSair" CssClass="bt" Text="Sair" OnClick="btSair_Click" Visible="false" />
            </div>
        </div>
    </form>
</body>
</html>
