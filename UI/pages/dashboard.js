var Dashboard = function () {


    return {

        init: function () {
            Dashboard.links();
            Dashboard.obterGrupo();
        },

        obterGrupo: function () {
            HelperJS.callApi({ url: "usuario/listarGrupos", type: "GET", data: null, functionOnSucess: Dashboard.gruposObtidos, functionOnError: HelperJS.showError });
        },

        gruposObtidos: function (lst) {

            var acesso = 0;

            $(".tile").hide();

            if ($.grep(lst, function (e) { return e == "S" }).length >= 1)
                $(".tile[tipo=1], .tile[tipo=2], .tile[tipo=3], .tile[tipo=4]").show();

            if ($.grep(lst, function (e) { return e == "A" }).length >= 1)
                $(".tile[tipo=1], .tile[tipo=2], .tile[tipo=3]").show();

            else if ($.grep(lst, function (e) { return e == "R" }).length >= 1)
                $(".tile[tipo=1], .tile[tipo=2]").show();

            else if ($.grep(lst, function (e) { return e == "F" || e == "C" }).length >= 1)
                $(".tile[tipo=1]").show();

            $(".tiles").show();
        },


        links: function () {
            $("#tile_perfil").on("click", function () { Dashboard.redirecionar('/Pages/Perfil.aspx') });
            $("#tile_usuario").on("click", function () { Dashboard.redirecionar('/Pages/Usuario.aspx') });
            $("#tile_configuracoes").on("click", function () { Dashboard.redirecionar('/Pages/Parametrizacao.aspx') });
            $("#tile_categoria").on("click", function () { Dashboard.redirecionar('/Pages/Categoria.aspx') });
            $("#tile_produto").on("click", function () { Dashboard.redirecionar('/Pages/Produto.aspx') });
            $("#tile_grupo").on("click", function () { Dashboard.redirecionar('/Pages/Grupo.aspx') });
            $("#tile_parceiro").on("click", function () { Dashboard.redirecionar('/Pages/ParceiroNegocio.aspx') });
            $("#tile_leilao").on("click", function () { Dashboard.redirecionar('/Pages/Leiloes.aspx') });
        },

        redirecionar: function (link) {
            window.location.href = link;
        }

    }

}();