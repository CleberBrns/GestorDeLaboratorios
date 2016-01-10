<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Opcoes.aspx.cs" Inherits="Home_Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Opções</title>
    <link href="../Styles/Home.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="divConteudo">
            <div class="pagina">
                <div runat="server" id="divLogo" style="text-align: center">
                    <%--<img src="../Imagens/logo_final_alscorplab.jpg" width="352px" />--%>
                </div>            
                <div class="contBt">
                    <asp:Button runat="server" ID="btLaboratorios" CssClass="bt" Text="Gerenciar Laboratórios"
                        OnClick="btLaboratorios_Click" />
                </div>
                <div class="contBt">
                    <asp:Button runat="server" ID="btMenuPrincipal" CssClass="bt" OnClick="btMenuPrincipal_Click" Text="Menu Principal" />
                     <asp:Button runat="server" ID="btSair" CssClass="bt"
                        Text="Sair" OnClick="btSair_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
