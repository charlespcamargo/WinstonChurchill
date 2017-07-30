var LeilaoLances = function () {

    var _id = 0;
    var passo = 1;
    var item = {};

    return {

        init: function () {
            Leilao.carregar();
            Leilao.eventos();
            Comprador.init();
            Fornecedor.init();
            Leilao.mudarPasso(0);
        },

        eventos: function () {
            $('#btnAnterior').click(function () { passo--; Leilao.mudarPasso(); });
            $('#btnContinuar').click(function () { passo++; Leilao.mudarPasso(); });
            $('#btnSalvar').click(function () { Leilao.salvar(); });

            $(".steps .navbar-inner ul li a").click(function () {
                passo = parseInt($(this).find(".number").text());
                Leilao.mudarPasso();
            });

        },

        mudarPasso: function () {

            $(".form-horizontal .form-wizard .navbar-inner ul li").removeClass("active");
            $(".tab-content .tab-pane").removeClass("active");

            $($(".form-horizontal .form-wizard .navbar-inner ul li")[passo - 1]).addClass("active");// zero-based
            $($(".tab-content .tab-pane").removeClass("active")[passo - 1]).addClass("active"); // zero-based

            $("#bar .bar").attr("style", "width: " + Math.round(passo / 3 * 100) + "%")
            $(".step-title").text("Passo " + passo + " de 3")

            if (passo == 1) {
                $('#btnAnterior').hide();
                $('#btnContinuar').show();
            }
            else if (passo == 2) {
                $('#btnAnterior').show();
                $('#btnContinuar').show();
            }
            else {
                $('#btnAnterior').show();
                $('#btnContinuar').hide();
            }

        },

        carregar: function () {
            $('#hfProduto').produtos();
            $('#hfRepresentanteComercial').representanteComercial();

            id = HelperJS.getQueryString("id");

            if (id)
                Leilao.editar();
        },

        editar: function () {

            var fnSuccess = function (data) {
                $('#formDados').popularCampos({ data: data });
                $('#hfProduto').select2("data", data.Produto);
                $('#hfRepresentanteComercial').select2("data", data.Representante);

                id = data.ID;

                
                Comprador.listar(data.Compradores);
                Fornecedor.listar(data.Fornecedores);
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

                
                HelperJS.callApi({ url: "leilao/salvar", type: "POST", data: item, functionOnSucess: Leilao.salvo, functionOnError: HelperJS.showError });
            }
        },

        salvo: function ()
        {
            HelperJS.showSuccess("Dados salvos com sucesso!");
            setTimeout(function () { window.location.href = 'Leiloes.aspx'; }, 2000);
        },

    }

}();

 