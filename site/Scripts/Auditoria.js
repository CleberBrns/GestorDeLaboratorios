$(document).ready(function () {

    $('#ddlPrateleira').prop('disabled', true);
    $('#ddlCamaras').prop('disabled', true);
    CarregaUnidade($('#hddIdUnidade').val());


    $("#ddlCamaras").change(function () {
        CarregaPrateleiras($(this).find('option:selected').val());
        $("#divExibicaoInfos").hide();
        $("#divRetornoPesquisa").hide();
    });

    $("#ddlUnidade").change(function () {
        CarregaCamaras($(this).find('option:selected').val());
        $("#divExibicaoInfos").hide();
        $("#divRetornoPesquisa").hide();
    });

    $("#ddlPrateleira").change(function () {
        $("#divExibicaoInfos").hide();
        $("#divRetornoPesquisa").hide();
    });

    function CarregaUnidade(idUnidade) {

        $.ajax({
            type: "POST",
            url: "Auditoria.aspx/Unidades",
            data: JSON.stringify({ sIdUnidade: idUnidade }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var select = '<select>';
                var option = '';
                $.each(data.d, function (index, value) {
                    option += '<option value=' + value.Value + '>' + value.Text + '</option>';
                });
                select = select + option + '</select>';
                $('#ddlUnidade').html(select);

                var qtdUnidades = $("#ddlUnidade option").length;

                if (qtdUnidades == 1) {
                    $(".divUnidade").hide();
                }

                CarregaCamaras($('#ddlUnidade option:selected').val());
            }
        });
    }

    function CarregaCamaras(idUnidade) {

        $.ajax({
            type: "POST",
            url: "Auditoria.aspx/Camaras",
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

                var qtdCamaras = $("#ddlCamaras option").length;

                if (qtdCamaras == 1) {
                    $(".divCamara").hide();
                }
                else {
                    $('#ddlCamaras').prop('disabled', false);
                }

                $('#ddlPrateleira').prop('disabled', false);
                CarregaPrateleiras($('#ddlCamaras option:selected').val());
            },
        });
    }

    function CarregaPrateleiras(idCamara) {

        $.ajax({
            type: "POST",
            url: "Auditoria.aspx/Prateleiras",
            data: JSON.stringify({ idCamara: idCamara }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var select = '<select>';
                var option = '';
                $.each(data.d, function (index, value) {
                    option += '<option value=' + value.Value + '>' + value.Text + '</option>';
                });
                select = select + option + '</select>';
                $('#ddlPrateleira').html(select);

                $('#ddlPrateleira').prop('disabled', false);
            },
        });
    }

    $('#btPesquisar').click(function () {

        CarregaAmostrasPrateleira($('#ddlPrateleira option:selected').val());

    });

    function CarregaAmostrasPrateleira(idPrateleira) {

        $.ajax({
            type: "POST",
            url: "Auditoria.aspx/ConsultaPrateleira",
            data: JSON.stringify({ sIdPrateleira: idPrateleira }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                var retorno = data.d          

                if (retorno != "") {

                    var select = '<table cellspacing=0 cellpadding=0 width=100%>' +
                                    '<tr class="amostrasPrateleira" style="background-color: #DDD;">' +                                        
                                        '<td>Caixa</td>' +
                                        '<td>Cod Grupo</td>' +
                                        '<td>IdAmostra</td>' +
                                        '<td>Data Entrada</td>' +
                                        '<td>Status</td>' +
                                    '</tr>';
                    var option = '';
                    $.each(data.d, function (index, value) {
                        option += '<tr class="amostrasGrupo">' +                                 
                                    '<td>' + value.Caixa + '</td>' +
                                    '<td>' + value.CodGrupo + '</td>' +
                                    '<td>' + value.IdAmostra + '</td>' +
                                    '<td>' + value.DataEntrada + '</td>' +
                                    '<td style=' + corStatus(value.IdStatusAmostra) + '>' + value.StatusAmostra + '</td>' +
                                  '</tr>';
                    });
                    select = select + option + '</table>';
                    $('#divRetornos').html(select);

                    $("#divExibicaoInfos").show();
                    $("#divRetornoPesquisa").hide();
                }
                else {

                    $("#divExibicaoInfos").hide();
                    $("#divRetornoPesquisa").show();                  

                }
            }
        });
    }

    function corStatus(status) {
        if (status == 0) {
            return 'color:green';
        } else if (status == 1) {
            return 'color:orange';
        } else if (status == 2) {
            return 'color:blue';
        } else {
            return 'color:red';
        }
    }

});
/////////////////////////////////////////////////////////////////////////////////////////////
function Redireciona(destino) {

    var urlDestino;

    if (destino == 0) {
        urlDestino = "../Home/Home.aspx";
    }
    else {
        urlDestino = "../Analise/Analise.aspx";
    }

    window.location.href = urlDestino;
}