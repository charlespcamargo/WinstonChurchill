var Leilao = function () {

    var id = 0;

    return {
        init: function () {
            Leilao.carregar();
            Leilao.eventos();
        },


        carregar: function () {
                        
            this.id = Ihara.getQueryString("cotacao");

            var fnSuccess = function (data)
            {
                
            }
             
            HelperJS.callApi({
                url: "leilao/carregar/" + ,
                type: "POST",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },

    }
        
}();