<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Descarte.aspx.cs" Inherits="Acoes_Descarte" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Descarte</title>
    <link href="../Styles/Acoes.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">        
        <input type="hidden" runat="server" id="hddIdUsuario" />
        <input type="hidden" runat="server" id="hddIdUnidade" />
        <input type="hidden" runat="server" id="hddIdCamara" />
        <input type="hidden" runat="server" id="hddIdEstante" />
        <input type="hidden" runat="server" id="hddIdPrateleria" />
        <input type="hidden" id="hddErro" runat="server" />
        <div class="pagina" runat="server" id="divPagina">
            <div runat="server" id="divLogo" style="text-align: center">
                <%--<img src="../Imagens/logo_final_alscorplab.jpg" width="352px" />--%>
            </div>
            <h2>Descarte<%--<asp:Label runat="server" ID="lblPrateleira" CssClass="lblCamara" />--%>
            </h2>
            <div style="padding-bottom: 3%;">
                <div class="insercoes">
                    <div style="margin-top: 3%;" runat="server" id="divInsercaoAtual">
                        <asp:Label runat="server" ID="lblInsercaoAtual" />
                    </div>                  
                    <div style="margin-top: 3%;" runat="server" id="divInsercoes" visible="true">
                        Amostra a descartar
                    <asp:Panel runat="server" DefaultButton="btAmostra" style="margin-top: 10px">
                        <asp:TextBox runat="server" ID="txtAmostra" Width="180px" Height="25px" autocomplete="off" />
                        <div style="display: none">
                            <asp:Button runat="server" ID="btAmostra" OnClick="btAmostra_Click" />
                        </div>                      
                    </asp:Panel>
                    </div>
                    <div style="margin-top: 3%;" runat="server" id="divProcessando" visible="false">
                        Processando...
                    <div style="margin-top: 3px">
                        <asp:Image runat="server" ID="imgProcessando" ImageUrl="~/Imagens/loading.gif" Width="25%" />
                    </div>
                    </div>
                </div>
                <div style="margin-top: 3%; margin-bottom: 3%; text-align: center; font-size: 18px;" runat="server" id="divRetorno" visible="false">
                    <div style="margin-bottom: 3%;">
                        <asp:Image runat="server" ID="imgOk" ImageUrl="../Imagens/ok.png" Visible="false" Width="8%" />
                        <asp:Image runat="server" ID="imgErro" ImageUrl="../Imagens/error.png" Visible="false" Width="8%" />
                    </div>
                    <asp:Label runat="server" ID="lblRetorno" />
                </div>
            </div>
            <div>
                <div class="rodape">
                </div>
                <div class="contBt">
                    <asp:Button runat="server" ID="btMenuPrincial" OnClick="btMenuPrincipal_Click" CssClass="bt" Text="Menu Principal" Visible="false" />
                    <asp:Button runat="server" ID="btMenuAcoes" OnClick="btMenuAcoes_Click" CssClass="bt" Text="Menu Ações" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
