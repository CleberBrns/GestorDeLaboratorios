$(document).ready(function () {

    $('#btNovoCadastro').hide();
    $('#lkbVoltar').hide();

    //Faz aparecer os painel de telefones///////////////////////////////
    $(".rblUsuarios input").click(function () {
        if ($('.rblUsuarios input').is(':checked')) {
            var id = $(this).attr("value");
            $(".dvUsuario").hide();
            $(".css" + id + "").show();
            $("#pnlNovoCadastro").hide();
            $('#btNovoCadastro').show();
            $('#lkbVoltar').show();
        }
    })

    $('#btCadastrar').click(function () {

        if ($('#ddlNivelAcesso').val() == "") {
            alert("Por favor, selecione o Nível de Acesso");
            return false;
        }

        if ($('#txtNovoNome').val() == "") {
            alert("Por favor, preencha o campo nome");
            return false;
        }

        if ($('#txtNovoLogin').val() == "") {
            alert("Por favor, preencha o campo login");
            return false;
        }

        if ($('#txtNovaSenha').val() == "") {
            alert("Por favor, preencha o campo senha");
            return false;
        }

    })

    $(document).on('click', '#btExcluir', function () {

        if (confirm("Deseja excluir o usuário?")) {           

        }
        else {
            ExibeMsgRetorno("Ação cancelada...");
        }

    });

    $('#btLimpar').click(function () {

        $('#txtNovoNome').val('');
        $('#txtNovoLogin').val('');
        $('#txtNovaSenha').val('');
    })
});
///////////////////////////////////////////////////////////////////////
///JavaScript//////////////////////////////////////////////////////////
function ConfirmaExclusao() {
    var confirmacao;

    if (confirm("Você está deletando definitivamente esse cadastro. Deseja continuar?")) {
        confirmacao = "Sim";
    } else {
        confirmacao = "Não";
    }
    document.getElementById("hddConfirmacao").value = confirmacao;
}
//////////////////////////////////////////////////////////////////////
function RecarregaPagina() {
    location.reload();
}
/////////////////////////////////////////////////////////////////////////////////////////////
function Redireciona() {

    var destino = "../Home/Home.aspx";

    window.location.href = destino;
}
/////////////////////////////////////////////////////////////////////////////////////////////