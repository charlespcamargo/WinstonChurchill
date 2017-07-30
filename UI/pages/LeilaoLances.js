var LeilaoLances = function () {

    var _id = 0;
    var passo = 1;
    var item = {};

    return {

        init: function () {
            LeilaoLances.carregar();
            LeilaoLances.eventos();
        },

        eventos: function () {
       
        },
        
        carregar: function ()
        {
            id = HelperJS.getQueryString("id");

            
            var fnSuccess = function (data)
            {
                $('#formDados').popularCampos({ data: data });
                id = data.ID;
            }

            HelperJS.callApi(
                {
                    url: "leilao/" + id,
                    type: "GET",
                    data: null,
                    functionOnSucess: fnSuccess,
                    functionOnError: HelperJS.showError
                });
        },

        GetID: function () {
            return id;
        },

        salvar: function () {

            if ($('#formDados').ehValido())
            {
                item = $('#formDados').obterJson();
                item.ID = Leilao.GetID();
                item.Ativo = $("#chkAtivo").prop("checked");
                item.Compradores = Comprador.get();
                item.Fornecedores = Fornecedor.get();

                
                //HelperJS.callApi({ url: "leilao/salvar", type: "POST", data: item, functionOnSucess: Leilao.salvo, functionOnError: HelperJS.showError });
            }
        },

        salvo: function ()
        {
            HelperJS.showSuccess("Dados salvos com sucesso!");
            setTimeout(function () { window.location.href = 'Leiloes.aspx'; }, 2000);
        },

    }

}();

 