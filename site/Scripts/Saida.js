$(document).ready(function () {

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

    CarregaAmostrasGrupo($("#hddIdGrupo").val());
      
    $('#txtAmostra').keydown(function (event) {        

        var keyCode = (event.keyCode ? event.keyCode : event.which);
        if (keyCode == 13) {

            if ($("#txtAmostra").val() != "") {
                $('#btRetiraAmostra').trigger('click');              
                return false;
            }
            else {
                return false;
            }
        }
    });

    $('#txtAmostra').keyup(function (event) {
        $("#divImpressao").hide();
        $("#divRetornoSaida").hide();
        $("#divInsercoes").hide();
    });

    function CarregaAmostrasGrupo(idGrupo) {

        $.ajax({
            type: "POST",
            url: "Saida.aspx/ConsultaAmostrasGrupo",
            data: JSON.stringify({ sIdGrupo: idGrupo }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var select = '<div>' + '<ul class="amostrasGrupo" style="background-color: #DDD;"><li style="float: left">CodAmostra</li><li style="float: left">Tipo Amostra</li><li style="float: left">Data Entrada</li><li>Status</li></ul>';
                var option = '';
                $.each(data.d, function (index, value) {
                    option += '<ul class="amostrasGrupo"><li style="float: left">' + value.IdAmostra + '</li><li style="float: left">' + value.TipoAmostra + '</li><li style="float: left">' + value.DataEntrada + '</li><li style=' + corStatus(value.IdStatusAmostra) + '>' + value.StatusAmostra + '</li></ul>';
                });
                select = select + option + '</div>';
                $('#divRetornos').html(select);
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

function Impressao()
{
    var urlDestino = "../Analise/ImpressaoSaida.aspx";
    window.open(urlDestino);
}
//////////////////////////////////////////////////////////////////////////////////////////////