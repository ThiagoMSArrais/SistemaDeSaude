$(document).ready(function () {
    $("#Plano_Prestadora_PrestadoraId").change(function () {
        ObterPlanos();
    });
});

function ObterPrestadoraSelecionada() {
    return $("#Plano_Prestadora_PrestadoraId option:selected").val();
}

function ObterPlanos() {

    var obj = {
        id: ObterPrestadoraSelecionada()
    };

     $.ajax({
        url: './ObterNomesDoPlano',
        type: 'POST',
        data: obj,
        dataType: 'json',
        error: function (xhr) {
            alert('Error: ' + xhr.statusText);
        },
         success: function (result) {

             $("#Plano_PlanoId").empty();

             for (i = 0; i < result["result"].length; i++) {
                 $("#Plano_PlanoId").append($("<option></option>")
                                    .attr("value", result["result"][i].value)
                                    .text(result["result"][i].text)); 
             }

             if ($("#Plano_PlanoId option").length > 0) {
                 $("#Plano_PlanoId").css('display', 'block');
             }
        }
    });

}