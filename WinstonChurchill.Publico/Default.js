var Home = function () {


    return {

        init: function () {
            Home.links();
        },

        links: function () {
            $("#lnkParticipar").on("click", function () { Home.redirecionar(1) });
            $("#lnkCadastre").on("click", function () { Home.redirecionar(1) });
            $("#lnkInstitucional").on("click", function () { Home.redirecionar(2) });
            $("#lnkContato").on("click", function () { Home.redirecionar(3) });
        },

        redirecionar: function (id) {
            window.location.href = '/Redirecionar.aspx?id=' + id;
        }
    }

}();