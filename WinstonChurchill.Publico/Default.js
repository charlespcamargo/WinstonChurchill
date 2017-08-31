var Home = function () {

    var ehHome = false;

    return {

        init: function (ehHome) {

            if (ehHome)
            {
                Home.ehHome = true;
            }
            else
            {
                $("#lnkHomeFooter, #lnkHome, #lnkQuemConfiaFooter, #lnkQuemConfia").hide();
            }

            Home.links();
        },

        links: function () {
            $("#lnkLogin, #lnkLoginFooter").on("click", function () { Home.redirecionar(1) });
        },

        redirecionar: function (id) {
            window.location.href = '/Redirecionar.aspx?id=' + id;
        }
    }

}();