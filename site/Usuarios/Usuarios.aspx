<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs" Inherits="Usuarios_Usuarios"  EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Usuários</title>
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/Usuarios.js" type="text/javascript"></script>
    <link href="../Styles/Usuarios.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin-top: 0px; margin-left: 0px;">
    <form id="formGerenciadorUsuarios" method="post" runat="server">
    <input type="hidden" id="hddErro" runat="server" />
    <input type="hidden" id="hddConfirmacao" name="hddConfirmacao" runat="server" />
    <div id="divCadastro" runat="server" class="pagina">
        <div runat="server" id="divLogo" style="text-align: center">
            <%--<img src="../Imagens/logo_final_alscorplab.jpg" width="352px" />--%>
        </div>
        <div class="header">
            Gerenciar Usuários
        </div>
        <asp:Panel ID="pnl_form" Visible="true" runat="server">
            <div style="float: left;">
                <div>
                    <div class="divCadastrados">
                        <asp:Panel runat="server" ID="pnlUsuarios">
                            <asp:RadioButtonList runat="server" ID="rblUsuarios" CssClass="rblUsuarios">
                            </asp:RadioButtonList>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div style="float: left; width: 288px;">
                <asp:Panel runat="server" ID="pnlCadastros">
                    <asp:Repeater ID="rptCadastros" runat="server" OnItemDataBound="rptCadastros_ItemDataBound">
                        <ItemTemplate>
                            <asp:Panel CssClass=" bold none" ID="dvUsuario" runat="server">
                                <asp:Label runat="server" ID="lblSenha" Visible="false" Text='<%#Eval("SENHA")%>' />
                                <div class="titCand" id="dvNomeCanal" runat="server">
                                    <div>
                                        <span>
                                            <asp:Label ID="lblNome" runat="server" Text='<%#Eval("NOME")%>' Style="display: none;"></asp:Label>
                                            <asp:Label ID="lblIdCadastro" runat="server" Style="display: none;" Text='<%#Eval("IDUSUARIO")%>'></asp:Label>
                                        </span>
                                    </div>
                                    <div style="margin-top: 3px;">
                                        <span class="descricao">Unidade</span>
                                        <div>
                                            <asp:Label ID="lblUnidade" runat="server" Text='<%#Eval("IDUNIDADE")%>'></asp:Label></div>
                                        <br />
                                    </div>
                                    <div style="margin-top: 3px;">
                                        <span class="descricao">Tipo de Acesso</span>
                                        <div>
                                            <asp:Label ID="lblTipoAcesso" runat="server" Text='<%#Eval("TIPOACESSO")%>'></asp:Label></div>
                                    </div>
                                    <div style="margin-top: 15px;">
                                        <asp:Panel runat="server" ID="pnlCadastro">
                                            <div id="divLogin" runat="server" class="contInformacoe">
                                                <span>Login</span>
                                                <asp:TextBox ID="txtLogin" Width="120px" Font-Size="11px" Text='<%#Eval("LOGIN")%>'
                                                    runat="server" Style="text-align: center;" ToolTip="Máximo de 15 dígitos!" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div id="divSenha" runat="server" class="contInformacoe">
                                                <span>Senha</span>
                                                <asp:TextBox ID="txtSenha" TextMode="Password" Width="120px" Font-Size="11px"
                                                    runat="server" Style="text-align: center;" ToolTip="Máximo de 15 dígitos!" MaxLength="15"></asp:TextBox>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="contBotoes">
                                        <asp:Button ID="btAlterar" runat="server" CssClass="btAcoes" Text="Alterar" CommandArgument='<%#Eval("IDUSUARIO")%>'
                                            OnClick="btAlterar_Click" ToolTip="Alterar o Cadastro" />
                                        <asp:Button ID="btExcluir" runat="server" CssClass="btAcoes" Text="Excluir" CommandArgument='<%#Eval("IDUSUARIO")%>'
                                            OnClick="btExcluir_Click" ToolTip="Excluí o Cadastro" />                                        
                                    </div>
                                </div>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="pnlNovoCadastro" runat="server">
                    <div class="header">
                        <span>
                            <asp:Label runat="server" ID="lblModoExibicao" Text="Novo cadastro"></asp:Label></span>
                    </div>
                    <div style="height: 10px;">
                    </div>
                    <div id="divOpcoes" style="padding-left: 10px;">
                        <div id="div4" runat="server" class="contInformacoe">
                            <span class="descricao">Unidade</span>
                            <asp:DropDownList runat="server" ID="ddlUnidade" Width="165px" Font-Size="15px" Style="text-align: center;">
                            </asp:DropDownList>
                        </div>
                        <div id="div5" runat="server" class="contInformacoe">
                            <span class="descricao">Nível de Acesso</span>
                            <asp:DropDownList runat="server" ID="ddlNivelAcesso" Width="165px" Font-Size="15px"
                                Style="text-align: center;">
                            </asp:DropDownList>
                        </div>
                        <div id="div3" runat="server" class="contInformacoe">
                            <span class="descricao">Nome Usuário</span>
                            <asp:TextBox ID="txtNovoNome" Width="160px" autocomplete="off" Font-Size="15px" runat="server"
                                Style="text-align: center;"></asp:TextBox>
                        </div>
                        <div id="div1" runat="server" class="contInformacoe">
                            <span class="descricao">Login</span>
                            <asp:TextBox ID="txtNovoLogin" Width="160px" Font-Size="15px" runat="server" Style="text-align: center;"
                                ToolTip="Mínimo de 4 e Máximo de 15 caracteres!" autocomplete="off" MaxLength="15"></asp:TextBox>
                        </div>
                        <div id="div2" runat="server" class="contInformacoe">
                            <span class="descricao">Senha</span>
                            <asp:TextBox ID="txtNovaSenha" TextMode="Password" autocomplete="off" Width="160px"
                                Font-Size="15px" runat="server" Style="text-align: center;" ToolTip="Mínimo de 4 e Máximo de 15 caracteres!"
                                MaxLength="15"></asp:TextBox>
                        </div>
                    </div>
                    <div class="contBotoes" style="width: 176%;">                      
                        <asp:Button runat="server" ID="btCadastrar" Text="Cadastrar" CssClass="btAcoes"
                            ToolTip="Cadastrar Novo Usuário" OnClick="btCadastrar_Click" />
                        <asp:Button ID="btLimpar" runat="server" Text="Limpar Campos" CssClass="btAcoes"
                            ToolTip="Limpa as informações" />
                    </div>
                </asp:Panel>
            </div>
            <div style="clear: both;">
            </div>
        </asp:Panel>
        <div class="contBotoes" style="padding-right: 13px; margin-top: 10px;">
            <asp:Button ID="lkbVoltar" runat="server" CssClass="bt" Text="Voltar" OnClick="RecarregarPagina_Click"
                ToolTip="Voltar para a listagem" />
            <asp:Button ID="btNovoCadastro" runat="server" CssClass="bt" Text="Novo Cadastro"
                OnClick="RecarregarPagina_Click" ToolTip="Janela para novo cadastro"  />
            <asp:Button ID="btMenuInicial" CssClass="bt" runat="server" Text="Menu Inicial" ToolTip="Voltar ao menu inicial"
                OnClick="btMenuInicial_Click" />            
        </div>
        <div style="height: 20px">
        </div>
    </div>
    <asp:Panel runat="server" id="divBotoesAux" class="none" DefaultButton="btErro">
        <asp:Button runat="server" ID="btErro" Text="Erro" OnClick="btErro_Click" />
    </asp:Panel>
    </form>
</body>
</html>
