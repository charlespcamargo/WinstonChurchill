//classe que contem métodos comuns que utilizaremos em várias telas, por exemplo, pra carregar o combo de clientes, basta passar o controle hidden seguido pelo nome da função
// **************
//EX de chamada de um controle na página: $('hfCliente').autoCompleteCliente();
// **************

// function combo de categorias
(function ($) {
    $.fn.categorias = function (_oSettings) {
        var multiplo = true;
        var hiddenId = $(this).prop('id');
        var selectId = $(this).prop('for');
        HelperJS.ComboAutoComplete(hiddenId, selectId, "Digite um código ou nome", "categoria/listar", multiplo,
                   formataResultados, formata, funcao, 3, null, true);

    };
    
    function formataResultados(item) {return item.ID + item.Nome;};
    function formata(item) {return item.ID + item.Nome;};
    function funcao(item) { return item.ID; };

})(jQuery);


// function combo de parceiros
(function ($) {
    $.fn.parceiros = function (_oSettings) {
        var multiplo = true;
        var hiddenId = $(this).prop('id');
        var selectId = $(this).prop('for');
        HelperJS.ComboAutoComplete(hiddenId, selectId, "Digite um código ou nome", "parceiroNegocio/listar/" + _oSettings.tipo, multiplo,
                   formataResultados, formata, funcao, 3, null, true);

    };

    function formataResultados(item) { return item.ID + item.Nome; };
    function formata(item) { return item.ID + item.Nome; };
    function funcao(item) { return item.ID; };

})(jQuery);

