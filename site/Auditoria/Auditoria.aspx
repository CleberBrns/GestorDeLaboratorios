<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Auditoria.aspx.cs" Inherits="Auditoria_Auditoria" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Auditoria</title>
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <link href="../Styles/Auditoria.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {;

            $("#btImprimir").click(function () {
                window.open('../Auditoria/Impressao.aspx', '_blank');
            });

        });
    </script>
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
            <h2>Auditoria <asp:Label runat="server" ID="lblCamara" CssClass="lblCamara" /><asp:Label runat="server" ID="lblPrateleira" CssClass="lblCamara" />
            </h2>
            <div style="padding-bottom: 3%;">
                <div class="insercoes">
                    <div style="margin-top: 3%;" runat="server" id="divPrateleira">
                        <span>Entre com a Prateleira</span>
                    <asp:Panel runat="server" DefaultButton="btPrateleira" style="margin-top: 10px">
                        <asp:TextBox runat="server" ID="txtPrateleira" Width="180px" Height="25px" autocomplete="off" />
                        <div style="display: none">
                            <asp:Button runat="server" ID="btPrateleira" OnClick="btPrateleira_Click" />
                        </div>
                    </asp:Panel>
                    </div>
                    <div runat="server" id="divAmostraAuditoria" visible="false" style="margin-top: 3%;">
                        <div>
                            <span>Amostra a ser auditada</span> 
                        </div>
                        <br />
                        <asp:Panel runat="server" DefaultButton="btAuditarAmostra">
                            <asp:TextBox runat="server" ID="txtAmostra" Width="180px" Height="25px" autocomplete="off" />
                            <div style="display: none">
                                <asp:Button runat="server" ID="btAuditarAmostra" OnClick="btAuditarAmostra_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="margin-top: 3%; margin-bottom: 3%; text-align: center; font-size: 18px;" runat="server" id="divRetornoAuditar" visible="false">
                        <div style="margin-bottom: 3%;">
                            <asp:Image runat="server" ID="imgOkAuditar" ImageUrl="../Imagens/ok.png" Visible="false" Width="3%" />
                            <asp:Image runat="server" ID="imgErroAuditar" ImageUrl="../Imagens/error.png" Visible="false" Width="3%" />
                            <div style="margin-top: 1%;">
                                <asp:Label runat="server" ID="lblRetornoAuditar" />
                            </div>
                        </div>
                    </div>
                    <div style="margin-top: 3%;" runat="server" id="divAuditoria" visible="false">
                        <div>
                            <asp:Repeater runat="server" ID="rptAuditoria">
                                <HeaderTemplate>
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tr class="amostrasPrateleira" style="background-color: #DDD;">
                                            <td>CodAmostra</td>
                                            <td>Usuário Data Recepção</td>                                            
                                            <td>Estante</td>
                                            <td>Prateleira</td>
                                            <td>Caixa</td>
                                            <td>Ultima Alteração</td>
                                            <td>Laboratório</td>
                                            <td>Auditado?</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="amostrasGrupo">
                                        <td><%# DataBinder.Eval(Container.DataItem, "CodAmostra") %></td>
                                        <td><%# DataBinder.Eval(Container.DataItem, "DataUsuarioRecepcao") %></td>                                        
                                        <td><%# DataBinder.Eval(Container.DataItem, "Estante") %></td>
                                        <td><%# DataBinder.Eval(Container.DataItem, "Prateleira") %></td>
                                        <td><%# DataBinder.Eval(Container.DataItem, "Caixa") %></td>
                                        <td><%# DataBinder.Eval(Container.DataItem, "UltimaAlteracao") %></td>
                                        <td><%# DataBinder.Eval(Container.DataItem, "Laboratorio") %></td>
                                        <td><%# DataBinder.Eval(Container.DataItem, "Auditado") %></td>
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
                <div runat="server" id="divInicio" visible="false" style="padding-top: 2%;">
                    <div class="rodape">
                    </div>
                    <div class="contBt">
                        <asp:Button runat="server" CssClass="bt" ID="btInicio" OnClick="btInicio_Click" Text="Nova Consulta" />                        
                        <input type="button" runat="server" id="btImprimir" class="bt" value="Imprimir" Visible="false" />
                    </div>
                </div>
            </div>
            <div class="rodape">
            </div>
            <div class="contBt">
                <asp:Button runat="server" ID="btMenuPrincial" OnClick="btMenuPrincipal_Click" CssClass="bt" Text="Menu Principal" />
                <asp:Button runat="server" ID="btAcoes" CssClass="bt" Text="Menu Ações" OnClick="btMenuAcoes_Click" />
            </div>
        </div>
    </form>
</body>
</html>
