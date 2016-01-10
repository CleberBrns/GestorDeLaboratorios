<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Consultar.aspx.cs" Inherits="Auditoria_Auditoria" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consultar</title>
    <link href="../Styles/ConsultarUnidade.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="hddTestPost" />
        <input type="hidden" runat="server" id="hddIdUnidade" value="0" />
        <input type="hidden" runat="server" id="hddIdPrateleira" />
        <input type="hidden" runat="server" id="hddCodPrateleira" />
        <input type="hidden" id="hddErro" runat="server" />
        <div class="pagina" runat="server" id="divPagina">
            <div runat="server" id="divLogo" style="text-align: center">
                <%--<img src="../Imagens/logo_final_alscorplab.jpg" width="352px" />--%>
            </div>
            <h2>Consultar <asp:Label runat="server" ID="lblUnidade" CssClass="lblCamara" />
            </h2>
            <div style="padding-bottom: 3%;">
                <div class="insercoes">
                    <div style="margin-top: 4%;" runat="server" id="divUnidades">
                        <div>
                            <div>
                                <span style="margin-right: 9%; margin-bottom: 5%;">Unidades</span>
                            </div>
                            <asp:DropDownList runat="server" ID="ddlUnidades" OnSelectedIndexChanged="ddlUnidades_SelectedIndexChanged" AutoPostBack="true" Width="180px" Height="25px">
                                <asp:ListItem Text="-- Selecione --" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>                   
                    <div style="margin-top: 3%; margin-bottom: 3%; text-align: center; font-size: 18px;" runat="server" id="divRetornoAuditar" visible="false">
                        <div style="margin-bottom: 3%;">
                            <asp:Image runat="server" ID="imgOkAuditar" ImageUrl="../Imagens/ok.png" Visible="false" Width="3%" />
                            <asp:Image runat="server" ID="imgErroAuditar" ImageUrl="../Imagens/error.png" Visible="false" Width="3%" />
                            <asp:Label runat="server" ID="lblRetornoAuditar" />
                        </div>
                    </div>
                    <div style="margin-top: 3%;" runat="server" id="divConsulta" visible="false">
                        <div>
                            <asp:Repeater runat="server" ID="rptConsulta">
                                <HeaderTemplate>
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tr class="amostrasPrateleira" style="background-color: #DDD;">
                                            <td>Câmara</td>
                                            <td>Estante</td>                                            
                                            <td>Prateleira</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="amostrasGrupo">
                                        <td><%# DataBinder.Eval(Container.DataItem, "Camara") %></td>                                                                            
                                        <td><%# DataBinder.Eval(Container.DataItem, "Estante") %></td>
                                        <td><%# DataBinder.Eval(Container.DataItem, "Prateleira") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div style="margin-top: 10%;" runat="server" id="divProcessando" visible="false">
                        Processando...
                    <div style="margin-top: 10px">
                        <asp:Image runat="server" ID="imgProcessando" ImageUrl="~/Imagens/loading.gif" Width="25%" />
                    </div>
                    </div>
                </div>
            </div>
            <div class="rodape">
            </div>
            <div class="contBt">
                <asp:Button runat="server" CssClass="bt" ID="btInicio" OnClick="btInicio_Click" Text="Nova Consulta" Visible="false" />
                <asp:Button runat="server" ID="btImprimir" OnClick="btImprimir_Click" CssClass="bt" Text="Imprimir" Visible="false" />
                <asp:Button runat="server" ID="btMenuUnidades" OnClick="btMenuUnidades_Click" CssClass="bt" Text="Menu Unidades" />
                <asp:Button runat="server" ID="btMenuPrincial" OnClick="btMenuPrincipal_Click" CssClass="bt" Text="Menu Principal" />                
            </div>
        </div>
    </form>
</body>
</html>
