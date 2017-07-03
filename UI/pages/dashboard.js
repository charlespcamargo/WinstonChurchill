var Dashboard = function () {


    return {

        init: function () {
            Dashboard.links();
        },

        links: function () {
            $("#tile_usuario").on("click", function () { Dashboard.redirecionar('/Pages/Usuario.aspx') });
            $("#tile_categoria").on("click", function () { Dashboard.redirecionar('/Pages/Categoria.aspx') });
            $("#tile_produto").on("click", function () { Dashboard.redirecionar('/Pages/Produto.aspx') });
            $("#tile_grupo").on("click", function () { Dashboard.redirecionar('/Pages/Grupo.aspx') });
            $("#tile_parceiro").on("click", function () { Dashboard.redirecionar('/Pages/ParceiroNegocio.aspx') });
            $("#tile_leilao").on("click", function () { Dashboard.redirecionar('/Pages/Leilao.aspx') });

        },

        redirecionar: function (link) {
            window.location.href = link;
        }

    }

}();