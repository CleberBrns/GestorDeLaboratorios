<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home_Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
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
                    <asp:Button runat="server" ID="btUnidades" CssClass="bt" Text="Gerenciar Unidades"
                        OnClick="btUnidades_Click" />
                    <asp:Button runat="server" ID="btUsuarios" CssClass="bt"
                        Text="Gerenciar Usuários" OnClick="btUsuarios_Click" />
                    <div style="margin-top: 1%;">
                        <asp:Button runat="server" ID="btAcoes" CssClass="bt"
                            Text="Ações" OnClick="btAcoes_Click" />
                        <asp:Button runat="server" ID="btLaboratorios" CssClass="bt" Text="Gerenciar Laboratórios"
                        OnClick="btLaboratorios_Click" />
                        <%--<asp:Button runat="server" ID="btOpcoesAdicionais" CssClass="bt"
                            Text="Opções Adicionais" OnClick="btOpcoesAdicionais_Click" />--%>
                    </div>
                </div>
                <div class="contBt">
                     <asp:Button runat="server" ID="btSair" CssClass="bt"
                        Text="Sair" OnClick="btSair_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
