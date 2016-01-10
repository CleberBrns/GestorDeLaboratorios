<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="../Styles/Login.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/Login.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="divLogin">
            <div class="pagina paginaLogin">
                <div runat="server" id="divLogo" style="text-align: center">
                    <%--<img src="../Imagens/logo_final_alscorplab.jpg" width="270px" />--%>
                </div>
                <div>
                    <h4>Login:</h4>
                    <asp:TextBox ID="txtLogin" Style="margin-top: 5px; margin-bottom: 5px;" runat="server" Width="100%"></asp:TextBox>
                    <h4>Senha:</h4>
                    <asp:TextBox ID="txtSenha" Style="margin-top: 5px;" TextMode="Password" runat="server" Width="100%"></asp:TextBox>
                    <div runat="server" id="divRetorno" visible="false" style="margin-top: 5%; text-align:center;">
                        <div>
                            <asp:Image runat="server" ID="imgErro" ImageUrl="../Imagens/error.png" Width="10%" />
                        </div>
                        <br />
                        <asp:Label runat="server" ID="lblRetorno" Style="color: Red; font-weight: bold;" />
                    </div>
                </div>
                <div class="contBt">
                    <asp:Button runat="server" ID="btLogar" CssClass="bt" Text="OK" OnClick="Acessar_Click" />
                    <asp:Button runat="server" ID="btLimparLogin" CssClass="bt" Text="Limpar" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
