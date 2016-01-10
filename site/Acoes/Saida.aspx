<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Saida.aspx.cs" Inherits="Acoes_Saida" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Saída</title>
    <link href="../Styles/Saida.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">     
        <input type="hidden" runat="server" id="hddBoolLabDescarte" value="0" />   
        <input type="hidden" runat="server" id="hddIdUsuario" />
        <input type="hidden" runat="server" id="hddIdUnidade" />
        <input type="hidden" runat="server" id="hddIdCamara" />
        <input type="hidden" runat="server" id="hddIdEstante" />
        <input type="hidden" runat="server" id="hddIdLaboratorio" />
        <input type="hidden" id="hddErro" runat="server" />
        <div class="pagina" runat="server" id="divPagina">
            <div runat="server" id="divLogo" style="text-align: center">
                <%--<img src="../Imagens/logo_final_alscorplab.jpg" width="352px" />--%>
            </div>
            <h2>Sáida<asp:Label runat="server" ID="lblLaboratorio" CssClass="lblCamara" />
            </h2>
            <div style="padding-bottom: 3%;">
                <div class="insercoes">
                    <div style="margin-top: 3%;" runat="server" id="divInsercaoAtual">
                        <asp:Label runat="server" ID="lblInsercaoAtual" />
                    </div>
                    <div style="margin-top: 3%;" runat="server" id="divLaboratorio">
                        <span>Entre com o Código do Laboratório</span>
                        <asp:Panel runat="server" DefaultButton="btLaboratorio" Style="margin-top: 10px">
                            <asp:TextBox runat="server" ID="txtLaboratorio" Width="180px" Height="25px" autocomplete="off" />
                            <div style="display: none">
                                <asp:Button runat="server" ID="btLaboratorio" OnClick="btLaboratorio_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="margin-top: 3%;" runat="server" id="divInsercoes" visible="false">
                        <span>Amostra a sair</span>
                        <asp:Panel runat="server" DefaultButton="btAmostra" Style="margin-top: 10px">
                            <asp:TextBox runat="server" ID="txtAmostra" Width="180px" Height="25px" autocomplete="off" />
                            <div style="display: none">
                                <asp:Button runat="server" ID="btAmostra" OnClick="btAmostra_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="margin-top: 3%;" runat="server" id="divProcessando" visible="false">
                        <span>Processando...</span> 
                    <div style="margin-top: 3%">
                        <asp:Image runat="server" ID="imgProcessando" ImageUrl="~/Imagens/loading.gif" Width="25%" />
                    </div>
                    </div>
                </div>
                <div style="margin-top: 3%; margin-bottom: 3%; text-align: center; font-size: 18px;" runat="server" id="divRetorno" visible="false">
                    <div style="margin-bottom: 3%;">
                        <asp:Image runat="server" ID="imgOk" ImageUrl="../Imagens/ok.png" Visible="false" Width="8%" />
                        <asp:Image runat="server" ID="imgErro" ImageUrl="../Imagens/error.png" Visible="false" Width="8%" />
                    </div>
                    <asp:Label runat="server" ID="lblRetorno" />
                </div>
                <div runat="server" id="divInicio" visible="false">
                    <div class="rodape">
                    </div>
                    <div class="contBt">
                        <asp:Button runat="server" ID="btNovoLaboratorio" OnClick="btNovoLaboratorio_Click" CssClass="bt" Text="Novo Laboratório" />
                    </div>
                </div>
            </div>
            <div class="rodape">
            </div>
            <div runat="server" id="divCodLabs" style="text-align: center">
                <div>
                    <div>
                        <img src="../Imagens/Labs/Imeditas.png" />
                        <img src="../Imagens/Labs/DBO-DQO.png" />
                        <img src="../Imagens/Labs/Fenol.png" />
                        <img src="../Imagens/Labs/Surfactantes.png" />
                    </div>
                    <div>

                        <img src="../Imagens/Labs/Fosfato.png" />
                        <img src="../Imagens/Labs/Varredura.png" />
                        <img src="../Imagens/Labs/Cianeto.png" />
                        <img src="../Imagens/Labs/Sulfeto.png" />
                    </div>
                    <div>

                        <img src="../Imagens/Labs/TOC.png" />
                        <img src="../Imagens/Labs/SerieNitrogenada.png" />
                        <img src="../Imagens/Labs/SeriedeSolidos.png" />
                        <img src="../Imagens/Labs/Metais.png" />
                    </div>
                    <div>
                        <img src="../Imagens/Labs/VOC.png" />
                        <img src="../Imagens/Labs/Microbiologia.png" />
                        <img src="../Imagens/Labs/NBR.png" />
                        <img src="../Imagens/Labs/Extracao.png" />
                    </div>
                </div>
            </div>
            <div>
                <div class="rodape">
                </div>
                <div class="contBt">
                    <asp:Button runat="server" ID="btMenuPrincial" OnClick="btMenuPrincipal_Click" CssClass="bt" Text="Menu Principal" Visible="false" />
                    <asp:Button runat="server" ID="btMenuAcoes" OnClick="btMenuAcoes_Click" CssClass="bt" Text="Menu Ações" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
