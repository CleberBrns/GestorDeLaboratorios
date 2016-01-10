$(document).ready(function () {

    $("#dialog-sucesso").dialog({
        resizable: false,
        autoOpen: false,
        height: 200,
        width: 320,
        modal: true,
        buttons: {
            "Ok": function () {
                $(this).dialog("close");
                $('#btMenuPrincipal').trigger('click');
            }
        },
        close: function () {
            window.close();
        }
    });

    $("#dialog-MsgRetorno").dialog({
        resizable: false,
        autoOpen: false,
        height: 200,
        width: 320,
        modal: true,
        buttons: {
            "Ok": function () {
                $(this).dialog("close");

            }
        },
        close: function () {
            window.close();
        }
    });

    CarregaPais();
    CarregaEstados();
    CarregaAlfabeto();

    $('#ddlCidade').prop('disabled', true);

    $('#divNovaUnidade').show();

    $('#btAdicionarPrateleira').click(function () {

        if ($("#ddlAlfabeto option").length != 0) {

            //Duas variaveis com o mesmo valor, uma é para salvar as infos e a outra é para exibir
            var valores = $('#ddlCamaras :selected').val() + '_' + $('#ddlAlfabeto :selected').text() + '_' + $('#ddlNumeracao').val() + '|';
            var valoreExibicao = $('#ddlCamaras :selected').text() + '_' + $('#ddlAlfabeto :selected').text() + '_' + $('#ddlNumeracao').val() + '|';

            if ($("#hddPrateleiraIncluida").val() != "") {
                valores += $("#hddPrateleiraIncluida").val();
            }
            $("#hddPrateleiraIncluida").val(valores);

            if ($("#hddExibicaoPrateleiras").val() != "") {
                valoreExibicao += $("#hddExibicaoPrateleiras").val();
            }
            $("#hddExibicaoPrateleiras").val(valoreExibicao);

            RemoveLetras(0);

            var incluidos = $("#hddExibicaoPrateleiras").val().split('|');

            var prateleirasIncluidas = "";

            for (var prateleira = 0; prateleira < incluidos.length; prateleira++) {

                var infosPrateleira = incluidos[prateleira].split('_');
                if (infosPrateleira[1] != undefined) {
                    prateleirasIncluidas += infosPrateleira[0] + "; Coluna " + infosPrateleira[1] + ": " + infosPrateleira[2] + " prateleira(s), ";
                }
            }

            $('#lblPrateleirasIncluidas').text(prateleirasIncluidas);

        }
        else {

            var msgRetorno = "Não é possivel incluir mais pratelerias pois todas as letras foram usadas. Finalize a unidade ou configure outra camara (se houver).";
            ExibeMsgRetorno(msgRetorno);
        }

        var qtdCamaras = $("#ddlQtdCamaras").find('option:selected').val();

        if (qtdCamaras == 1) {

            if ($("#hddPrateleiraIncluida").val() != "") {
                $('#divConcluirUnidade').show();
            }
        }

        var ultimaCamara = $("#ddlCamaras").find('option:last').val();
        var camaraAtual = $("#ddlCamaras").find('option:selected').val();

        if (ultimaCamara == camaraAtual) {
            $('#divConcluirUnidade').show();
        }

    });


    $("#ddlEstado").change(function () {

        $(this).find("option[value='-1']").remove();

        $.ajax({
            type: "POST",
            url: "Unidades.aspx/Cidades",
            data: JSON.stringify({ idEstado: $(this).val().trim() }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var select = '<select>';
                var option = '';
                $.each(data.d, function (index, value) {
                    option += '<option value=' + value.Value + '>' + value.Text + '</option>';
                });
                select = select + option + '</select>';
                $('#ddlCidade').html(select);
                $('#ddlCidade').prop('disabled', false);
            }
        });
    });

    $(document).on('click', '#btInserirPrateleiras', function () {

        if ($('#ddlEstado').val() == "0") {
            alert("Por favor, selecione o Estado");
            return false;
        }

        if ($('#ddlCidade').val() == "0") {
            alert("Por favor, selecione a Cidade");
            return false;
        }

        if ($('#txtNomeUnidade').val() == "") {
            alert("Por favor, preencha o Nome da Unidade");
            return false;
        }

        $.ajax({
            type: "POST",
            url: "Unidades.aspx/InsereUnidade",
            data: JSON.stringify({ nomeUnidade: $('#txtNomeUnidade').val(), sIdCidade: $('#ddlCidade').val(),
                sIdEstado: $('#ddlEstado').val(), sIdPais: $('#ddlPais').val(), sQtdCamaras: $('#ddlQtdCamaras').val()
            }),

            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                var idUnidade = 0;
                $.each(data.d, function (index, value) {
                    idUnidade = value.Value;
                });

                $("#hddIdUnidade").val(idUnidade);

                CarregaCamaras(idUnidade);
            },
            error: function (x, e) {
                $("#hddErro").val(e.responseText);
                $('#btErro').trigger('click');
            }
        });

        $('#btMenuPrincipal').hide();
        $('#divNovaUnidade').hide();
        $('#divPrateleiras').show();

        var qtdCamaras = $("#ddlQtdCamaras").find('option:selected').val();
        if (qtdCamaras > 1) {
            $('.divCamaraCadastrada').show();
        }

    });

    $("#ddlCamaras").change(function () {

        $('#hddLetraIncluida').val("");

        var selecaoAnterior = ($(this).find('option:selected').val() - 1);
        var nomeCamara = $(this).find("option[value='" + selecaoAnterior + "']").text();

        if ($('#lblPrateleirasIncluidas').text() != "") {

            var msgRetorno = "As prateleiras da " + nomeCamara + " estão salvas! Insira agora as prateleiras da nova Câmara Selecionada";

            ExibeMsgRetorno(msgRetorno);

            $("#ddlCamaras option[value='" + selecaoAnterior + "']").remove();

            $('#hddExibicaoPrateleiras').val('');
            $('#lblPrateleirasIncluidas').text('');

            CarregaAlfabeto();

        }
        else {            

            $('#ddlCamaras').find("option[value='" + selecaoAnterior + "']").prop('selected', true);

            $('#lblMsgRetorno').text("Antes de configurar uma nova Câmara, por favor, finalize a configuração da Câmara atual.");
            $('#dialog-MsgRetorno').dialog('open');            
                      
        }

    });

    $(document).on('click', '#btConcluirUnidade', function () {

        if (confirm("Deseja Concluir a Unidade!")) {

            $.ajax({
                type: "POST",
                url: "Unidades.aspx/InserePrateleiras",
                data: JSON.stringify({ prateleiras: $('#hddPrateleiraIncluida').val()
                }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#dialog-sucesso').dialog('open');
                },
                error: function (x, e) {
                    $("#hddErro").val(e.responseText);
                    $('#btErro').trigger('click');
                }
            });

        }
        else {
            ExibeMsgRetorno("Ação cancelada...");
        }

    });

    function CarregaPais() {

        $.ajax({
            type: "POST",
            url: "Unidades.aspx/Pais",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var select = '<select>';
                var option = '';
                $.each(data.d, function (index, value) {
                    option += '<option value=' + value.Value + '>' + value.Text + '</option>';
                });
                select = select + option + '</select>';
                $('#ddlPais').html(select);
            }
        });

    }

    function CarregaEstados() {

        $.ajax({
            type: "POST",
            url: "Unidades.aspx/Estados",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var select = '<select>';
                var option = '';
                $.each(data.d, function (index, value) {
                    option += '<option value=' + value.Value + '>' + value.Text + '</option>';
                });
                select = select + option + '</select>';
                $('#ddlEstado').html(select);
                $('#ddlEstado').prepend("<option value='-1' selected='selected'>-- Selecione --</option>");
            }
        });

    }

    function CarregaCamaras(idUnidade) {

        $.ajax({
            type: "POST",
            url: "Unidades.aspx/Camaras",
            data: JSON.stringify({ idUnidade: idUnidade }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var select = '<select>';
                var option = '';
                $.each(data.d, function (index, value) {
                    option += '<option value=' + value.Value + '>' + value.Text + '</option>';
                });
                select = select + option + '</select>';
                $('#ddlCamaras').html(select);
            },
            error: function (x, e) {
                $("#hddErro").val(e.responseText);
                $('#btErro').trigger('click');
            }
        });

    }

    function CarregaAlfabeto() {

        $.ajax({
            type: "POST",
            url: "Unidades.aspx/Alfabeto",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var select = '<select>';
                var option = '';
                $.each(data.d, function (index, value) {
                    option += '<option value=' + value.Value + '>' + value.Text + '</option>';
                });
                select = select + option + '</select>';
                $('#ddlAlfabeto').html(select);
            }
        });

    }

    function RemoveLetras(acao) {

        var letrasIncluidas = "";
        if ($("#hddLetraIncluida").val() != "") {
            letrasIncluidas += $("#hddLetraIncluida").val();
        }

        letrasIncluidas += $('#ddlAlfabeto :selected').val() + ",";
        $("#hddLetraIncluida").val(letrasIncluidas);

        if ($('#hddLetraIncluida').val() != '') {

            var removerDrop = $('#hddLetraIncluida').val();
            var arrayRemocao = removerDrop.split(',');

            for (var remover = 0; remover < arrayRemocao.length; remover++) {

                var teste = arrayRemocao[remover];
                $("#ddlAlfabeto option[value='" + arrayRemocao[remover] + "']").remove();

            }
        }
    }

    function ExibeMsgRetorno(msgRetorno) {

        $('#lblMsgRetorno').text(msgRetorno);
        $('#dialog-MsgRetorno').dialog('open');

    }

});
/////////////////////////////////////////////////////////////////////////////////////////////
function Redireciona(tipo) {

    var destino = "../Home/Home.aspx";

    if (tipo == 1) {
        destino = "../Login/Login.aspx";
    }

    window.location.href = destino;
}
