<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Erro.aspx.cs" Inherits="Erro_Erro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Erro</title>
    <link href="../Styles/Erro.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/Erro.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" runat="server" id="hddInclusoes" />
    <div class="pagina" runat="server" id="divErro">
        <div runat="server" id="divLogo" style="text-align: center">
            <%--<img src="../Imagens/logo_final_alscorplab.jpg" width="352px" />--%>
        </div>
        <div style="text-align: center;">
            <h4>
                Ocorreu um erro inesperado.</h4>
        </div>
        <div style="text-align: center;">
            <h5>
                Por favor, entre em contato com o gestor da página 
                <asp:Label runat="server" ID="lblComExcessao" Visible="false"> e informe a segunte mensagem;</asp:Label> </h5>
        </div>
        <div class="mensagemErro">
            <asp:Label runat="server" ID="lblExcessao" />
        </div>
        <div class="rodape" style="margin-top:38%;">
        </div>
        <div class="contBt">
            <asp:Button runat="server" ID="btMenuPrincipal" OnClick="btMenuPrincipal_Click" Text="Menu Principal" CssClass="bt" />
            <asp:Button runat="server" ID="btSair" OnClick="btSair_Click" Text="Sair" CssClass="bt" />
        </div>
    </div>
    </form>
</body>
</html>
