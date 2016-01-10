$(document).ready(function () {

    $('#btNovoCadastro').hide();
    $('#lkbVoltar').hide();

    //Faz aparecer os painel de telefones///////////////////////////////
    $(".rblLaboratorios input").click(function () {
        if ($('.rblLaboratorios input').is(':checked')) {
            var id = $(this).attr("value");
            $(".dvLaboratorio").hide();
            $(".css" + id + "").show();
            $("#pnlNovoCadastro").hide();
            $('#btNovoCadastro').show();
            $('#lkbVoltar').show();
        }
    })

    $('#btCadastrar').click(function () {

        if ($('#ddlUnidade').val() == "0") {
            alert("Por favor, selecione o Nível de Acesso");
            return false;
        }

        if ($('#txtNovoNome').val() == "") {
            alert("Por favor, preencha o campo Nome");
            return false;
        }

        if ($('#txtNovoCodigo').val() == "") {
            alert("Por favor, preencha o campo Código");
            return false;
        }

    })

    $(document).on('click', '#btExcluir', function () {

        if (confirm("Deseja realmente excluir o Laboratório?")) {           

        }
        else {
            ExibeMsgRetorno("Ação cancelada...");
        }

    });

    $('#btLimpar').click(function () {

        $('#txtNovoNome').val('');        
        $('#txtNovoCodigo').val('');
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