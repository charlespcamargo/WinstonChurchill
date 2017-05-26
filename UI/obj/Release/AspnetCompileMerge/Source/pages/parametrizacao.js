var Parametrizacao = function () {
    var id = 0;
    var status = true;
    return {
        init: function () {
            Parametrizacao.eventos();
            Parametrizacao.carregar();
        },

        eventos: function () {
            $('#btnSalvar').click(function () { Parametrizacao.salvar(); });
        },

        carregar: function () {
            var fnSuccess = function (data) {
                $('#Dados').popularCampos({ data: data });
                id = data.ID;
                status = data.Ativo;
            }

            HelperJS.callApi({
                url: "parametro/carregar",
                type: "GET",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });;
        },


        salvar: function () {
            
            var jsonSend = $('#Dados').obterJson();
            jsonSend.ID = id;
            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados salvos com sucesso!");
                Parametrizacao.carregar();
            }

            HelperJS.callApi({
                url: "parametro/salvar/",
                type: "POST",
                data: jsonSend,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },
    };
}();