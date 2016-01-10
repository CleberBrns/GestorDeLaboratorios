<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Gerenciar.aspx.cs" Inherits="Unidades_Gerenciar" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gerenciamento de Unidades</title>
    <link href="../Styles/Acoes.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="hddIdUnidade" />
        <input type="hidden" runat="server" id="hddIdCamara" />
        <input type="hidden" runat="server" id="hddIdEstante" />
        <input type="hidden" id="hddErro" runat="server" />
        <div class="pagina" runat="server" id="divPagina">
            <div runat="server" id="divLogo" style="text-align: center">
                <%--<img src="../Imagens/logo_final_alscorplab.jpg" width="352px" />--%>
            </div>
            <h2>Gerenciar Unidade<asp:Label runat="server" ID="lblUnidade" CssClass="lblCamara" /><asp:Label runat="server" ID="lblCamara" CssClass="lblCamara" /><asp:Label runat="server" ID="lblEstante" CssClass="lblCamara" />
            </h2>
            <div style="padding-bottom: 3%;">
                <div class="insercoes">
                    <div style="margin-top: 4%;" runat="server" id="divUnidades">
                        <div>
                            <div>
                                <span style="margin-right: 13%; margin-bottom: 5%;">Unidades</span>
                            </div>
                            <asp:DropDownList runat="server" ID="ddlUnidades" OnSelectedIndexChanged="ddlUnidades_SelectedIndexChanged" AutoPostBack="true" Width="180px" Height="25px">
                                <asp:ListItem Text="-- Selecione --" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div style="margin-top: 5%;" runat="server" id="divOpcoes" visible="false" class="contBt">
                        <asp:Button runat="server" ID="btNovaCamara" OnClick="btNovaCamara_Click" CssClass="bt" Text="Nova Câmara" />
                        <asp:Button runat="server" ID="btNovaEstante" OnClick="btNovaEstante_Click" CssClass="bt" Text="Nova Estante" />
                    </div>
                    <div style="margin-top: 3%;" runat="server" id="divConfiguraUnidade" visible="false">
                         <div style="margin-top: 3%;" runat="server" id="divDropCamara" visible="false">
                            Selecione a Câmara
                        <div style="margin-top: 10px">
                            <asp:DropDownList runat="server" ID="ddlCamaras" OnSelectedIndexChanged="ddlCamaras_SelectedIndexChanged" AutoPostBack="true" Width="180px" Height="25px">
                                <asp:ListItem Text="-- Selecione --" Value="0" />
                            </asp:DropDownList>
                        </div>
                        </div>
                        <div style="margin-top: 3%;" runat="server" id="divCamara" visible="false">
                            Insira a Câmara
                        <asp:Panel runat="server" DefaultButton="btCamara" Style="margin-top: 10px">
                            <asp:TextBox runat="server" ID="txtCamara" Width="180px" Height="25px" autocomplete="off" />
                            <div style="display: none">
                                <asp:Button runat="server" ID="btCamara" OnClick="btCamara_Click" />
                            </div>
                        </asp:Panel>
                        </div>
                        <div style="margin-top: 3%;" runat="server" id="divEstante" visible="false">
                            Insira a Estante
                        <asp:Panel runat="server" DefaultButton="btEstante" Style="margin-top: 10px">
                            <asp:TextBox runat="server" ID="txtEstante" Width="180px" Height="25px" autocomplete="off" />
                            <div style="display: none">
                                <asp:Button runat="server" ID="btEstante" OnClick="btEstante_Click" />
                            </div>
                        </asp:Panel>
                        </div>
                        <div style="margin-top: 3%;" runat="server" id="divPrateleiras" visible="false">
                            Insira as Prateleiras
                        <asp:Panel runat="server" DefaultButton="btPrateleiras" Style="margin-top: 10px">
                            <asp:TextBox runat="server" ID="txtPrateleiras" Width="180px" Height="25px" autocomplete="off" />
                            <div style="display: none">
                                <asp:Button runat="server" ID="btPrateleiras" OnClick="btPrateleiras_Click" />
                            </div>
                        </asp:Panel>
                        </div>
                    </div>
                    <div style="margin-top: 3%;" runat="server" id="divProcessando" visible="false">
                        Processando...
                    <div style="margin-top: 10px">
                        <asp:Image runat="server" ID="imgProcessando" ImageUrl="~/Imagens/loading.gif" Width="25%" />
                    </div>
                    </div>
                </div>
                <div style="margin-top: 3%; margin-bottom: 3%; text-align: center; font-size: 18px;" runat="server" id="divRetorno" visible="false">
                    <div style="margin-bottom: 3%;">
                        <asp:Image runat="server" ID="imgOk" ImageUrl="../Imagens/ok.png" Visible="false" Width="5%" />
                        <asp:Image runat="server" ID="imgErro" ImageUrl="../Imagens/error.png" Visible="false" Width="5%" />
                    </div>
                    <asp:Label runat="server" ID="lblRetorno" />
                </div>             
            </div>
            <div class="rodape">
            </div>
            <div class="contBt">
                <asp:Button runat="server" ID="btInicio" OnClick="btInicio_Click" CssClass="bt" Text="Início" Visible="false" /> 
                <asp:Button runat="server" ID="btMenuUnidades" OnClick="btMenuUnidades_Click" CssClass="bt" Text="Menu Unidades" />
                <asp:Button runat="server" ID="btMenuPrincipal" CssClass="bt" OnClick="btMenuPrincipal_Click" Text="Menu Principal" />
            </div>
        </div>
    </form>
</body>
</html>
