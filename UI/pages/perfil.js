var Perfil = function () {
    return {

        init: function () {
            Perfil.carregar();
            Perfil.eventos();
        },


        eventos: function () {
            $('#btnSalvar').click(function () { Perfil.salvar(); });
        },

        carregar: function () {
            var fnSuccess = function (data) {
                $('#Dados').popularCampos({ data: data });
                status = data.Ativo;
            }

            HelperJS.callApi({
                url: "perfil/carregar",
                type: "GET",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });

        },

        salvar: function () {
            if ($('#Dados').ehValido() == false)
                return;

            var jsonSend = $('#Dados').obterJson();
            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados salvos com sucesso!");
            }

            HelperJS.callApi({
                url: "perfil/salvar",
                type: "POST",
                data: jsonSend,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },
    }

}();