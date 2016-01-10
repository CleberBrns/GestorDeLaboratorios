<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Impressao.aspx.cs" Inherits="Analise_Impressao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Impressão</title>
    <link href="../Styles/Auditoria.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        window.print();

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="hddIdGrupo" runat="server" />
        <input type="hidden" id="hddErro" runat="server" />
        <div runat="server" id="divConteudo">
            <div class="pagina">
            <div runat="server" id="divLogo" style="text-align: center">
                <%--<img src="../Imagens/logo_final_alscorplab.jpg" width="352px" />--%>
            </div>
                <div style="text-align: center">
                    <h2>
                        <asp:Label runat="server" ID="lblTipoImpressao" />
                        <asp:Label runat="server" ID="lblPrateleira" CssClass="lblCamara" />
                    </h2>
                </div>
                <div class="insercoes">
                    <div style="margin-top: 3%;" runat="server" id="divAuditoria">
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
                </div>
                <div class="clear"></div>
                <div class="rodape">
                </div>
                <%--<div class="clear" style="padding-top: 40px;"></div>
                <div class="divAssinaturas">
                    <div>
                        <span style="border-top-style: solid; border-top-color: black; border-top-width: 3px;">Assinatura Responsável Estoque</span>
                        <span style="padding-left: 160px;"></span>
                        <span style="border-top-style: solid; border-top-color: black; border-top-width: 3px;">Assinatura Responsável Retirada</span>
                    </div>
                </div>--%>
            </div>
        </div>
    </form>
</body>
</html>
