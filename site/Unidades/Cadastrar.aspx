<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cadastrar.aspx.cs" Inherits="Unidades_Cadastrar" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastro de Unidade</title>
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
            <h2>Nova Unidade<asp:Label runat="server" ID="lblUnidade" CssClass="lblCamara" /><asp:Label runat="server" ID="lblCamara" CssClass="lblCamara" /><asp:Label runat="server" ID="lblEstante" CssClass="lblCamara" />
            </h2>
            <div style="padding-bottom: 3%;">
                <div class="insercoes">
                    <div style="margin-top: 4%;" runat="server" id="divUnidade">
                        <div style="margin-top: 5px; display: none;">
                            <div>
                                <span style="margin-right: 13%; margin-bottom: 5%;">País</span>
                            </div>
                            <asp:DropDownList runat="server" ID="ddlPais" Width="180px" Height="25px">
                                <asp:ListItem Text="-- Selecione --" Value="0" />
                            </asp:DropDownList>
                        </div>
                        <div style="margin-top: 5px;">
                            <div>
                                <span style="margin-right: 10%; margin-bottom: 5%;">Estado</span>
                            </div>
                            <asp:DropDownList runat="server" ID="ddlEstado" Width="180px" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                                <asp:ListItem Text="-- Selecione --" Value="0" />
                            </asp:DropDownList>
                        </div>
                        <div style="margin-top: 5px;">
                            <div>
                                <span style="margin-right: 10%; margin-bottom: 5%;">Cidade</span>
                            </div>
                            <asp:DropDownList runat="server" ID="ddlCidade" Width="180px" Height="25px" Enabled="false" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged">
                                <asp:ListItem Text="-- Selecione --" Value="0" />
                            </asp:DropDownList>
                        </div>
                        <div style="margin-top: 10px;">
                            <div>
                                <span style="margin-right: 3%; margin-bottom: 5%;">Nome da Unidade</span>
                            </div>
                            <asp:TextBox runat="server" placeholder="Campo obrigatório" autocomplete="off" Width="180px"
                                Height="25px" ID="txtNomeUnidade"></asp:TextBox>
                        </div>
                    </div>
                    <div style="margin-top: 4%;" runat="server" id="divConfiguraUnidade" visible="false">
                        <div style="margin-top: 4%;" runat="server" id="divCamara" visible="false">
                            <div>
                                <span style="margin-right: 3%; margin-bottom: 5%;">Insira a Câmara</span>
                            </div>
                            <asp:Panel runat="server" DefaultButton="btCamara" Style="margin-top: 10px">
                                <asp:TextBox runat="server" ID="txtCamara" Width="180px" Height="25px" autocomplete="off" />
                                <div style="display: none">
                                    <asp:Button runat="server" ID="btCamara" OnClick="btCamara_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                        <div style="margin-top: 4%;" runat="server" id="divEstante" visible="false">
                            <div>
                                <span style="margin-right: 3%; margin-bottom: 5%;">Insira a Estante</span>
                            </div>
                            <asp:Panel runat="server" DefaultButton="btEstante" Style="margin-top: 10px">
                                <asp:TextBox runat="server" ID="txtEstante" Width="180px" Height="25px" autocomplete="off" />
                                <div style="display: none">
                                    <asp:Button runat="server" ID="btEstante" OnClick="btEstante_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                        <div style="margin-top: 4%;" runat="server" id="divPrateleiras" visible="false">
                            <div>
                                <span style="margin-right: 3%; margin-bottom: 5%;">Insira as Prateleiras</span>
                            </div>
                            <asp:Panel runat="server" DefaultButton="btPrateleiras" Style="margin-top: 10px">
                                <asp:TextBox runat="server" ID="txtPrateleiras" Width="180px" Height="25px" autocomplete="off" />
                                <div style="display: none">
                                    <asp:Button runat="server" ID="btPrateleiras" OnClick="btPrateleiras_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div style="margin-top: 4%;" runat="server" id="divProcessando" visible="false">
                        <div>
                            <span style="margin-right: 5%;">Processando...</span>
                        </div>
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
                <div runat="server" id="divAcoes">
                    <div class="rodape">
                    </div>
                    <div class="contBt">
                        <asp:Button runat="server" ID="btCadastrar" OnClick="btCadastrar_Click" CssClass="bt" Text="Cadastrar" />
                        <asp:Button runat="server" ID="btConfigurarUnidade" OnClick="btConfigurarUnidade_Click" CssClass="bt" Text="Configurar Unidade" Visible="false" />
                        <asp:Button runat="server" ID="btInicio" OnClick="btInicio_Click" CssClass="bt" Text="Inicio" Visible="false" />
                        <asp:Button runat="server" ID="btNovaCamara" OnClick="btNovaCamara_Click" CssClass="bt" Text="Nova Câmara" Visible="false" />
                        <asp:Button runat="server" ID="btNovaEstante" OnClick="btNovaEstante_Click" CssClass="bt" Text="Nova Estante" Visible="false" />
                    </div>
                </div>
            </div>
            <div class="rodape">
            </div>
            <div class="contBt">
                <asp:Button runat="server" ID="btMenuUnidades" OnClick="btMenuUnidades_Click" CssClass="bt" Text="Menu Unidades" />
                <asp:Button runat="server" ID="btMenuPrincipal" CssClass="bt" OnClick="btMenuPrincipal_Click" Text="Menu Principal" />
            </div>
        </div>
    </form>
</body>
</html>
