<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaLivre.aspx.cs" Inherits="ConsultaLivre" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consulta Livre</title>
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
            <h2>Consulta Livre<asp:Label runat="server" ID="lblBusca" CssClass="lblCamara" />
            </h2>
            <div style="padding-bottom: 3%;">
                <div class="insercoes">           
                    <div runat="server" id="divAmostra" style="margin-top: 3%;">
                        <div>
                            Entre com a Amostra 
                        </div>
                        <br />
                        <asp:Panel runat="server" DefaultButton="btAmostra">
                            <asp:TextBox runat="server" ID="txtAmostra" Width="180px" Height="25px" autocomplete="off" />
                            <div style="display: none">
                                <asp:Button runat="server" ID="btAmostra" OnClick="btAmostra_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="margin-top: 3%; margin-bottom: 3%; text-align: center; font-size: 18px;" runat="server" id="divRetorno" visible="false">
                        <div style="margin-bottom: 3%;">
                            <asp:Image runat="server" ID="imgOk" ImageUrl="../Imagens/ok.png" Visible="false" Width="3%" />
                            <asp:Image runat="server" ID="imgErro" ImageUrl="../Imagens/error.png" Visible="false" Width="3%" />
                            <div style="margin-top: 2%;">
                                <asp:Label runat="server" ID="lblRetorno" />
                            </div>
                        </div>
                    </div>
                    <div style="margin-top: 3%;" runat="server" id="divConsulta" visible="false">
                        <div>
                            <asp:Repeater runat="server" ID="rptConsulta">
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
                <div runat="server" id="divInicio" style="padding-top: 2%;" visible="false">
                    <div class="rodape">
                    </div>
                    <div class="contBt">
                        <asp:Button runat="server" CssClass="bt" ID="btInicio" OnClick="btInicio_Click" Text="Nova Consulta" />                   
                    </div>
                </div>
            </div>
            <div class="rodape">
            </div>
        </div>
    </form>
</body>
</html>
