$(document).ready(function () {

    $("#btOK").click(function () {
        if ($('#txtSenhaCripto').val() == '') {
            alert('Por favor, preencha o campo Senha.');
            return false;
        }
    });

    $("#btLimpar").click(function () {
        $('#txtSenhaCripto').val('');
        $('#txtResultado').val('');
        return false;
    });

    $("#btLimparLogin").click(function () {
        $('#txtLogin').val('');
        $('#txtSenha').val('');
        $('#divRetorno').hide();
        return false;
    });
});
