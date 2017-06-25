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

    function formataResultados(item) { return item.ID + item.Nome; };
    function formata(item) { return item.ID + item.Nome; };
    function funcao(item) { return item.ID; };

})(jQuery);


// function combo de parceiros
(function ($) {
    $.fn.parceiros = function (_oSettings) {
        var multiplo = true;
        var hiddenId = $(this).prop('id');
        var selectId = $(this).prop('for');
        var url = '';
        if (_oSettings.tipo === 1)
            url = 'parceiroNegocio/listarComprador';
        else
            url = 'parceiroNegocio/listarFornecedor';

        HelperJS.ComboAutoComplete(hiddenId, selectId, "Digite um código ou nome", url, multiplo,
            formataResultados, formata, funcao, 3, null, true);

    };

    function formataResultados(item) { return item.ID + item.RazaoSocial; };
    function formata(item) { return item.ID + item.RazaoSocial; };
    function funcao(item) { return item.ID; };

})(jQuery);


// function combo de produtos
(function ($) {
    $.fn.produtos = function (_oSettings) {
        var multiplo = false;
        var hiddenId = $(this).prop('id');
        var selectId = $(this).prop('for');
        var url = 'produto/listar';

        HelperJS.ComboAutoComplete(hiddenId, selectId, "Digite um código ou nome", url, multiplo,
            formataResultados, formata, funcao, 3, null, true);

    };

    function formataResultados(item) { return item.ID + item.Nome; };
    function formata(item) { return item.ID + item.Nome; };
    function funcao(item) { return item.ID; };

})(jQuery);



// function combo de grupo de parceiros
(function ($) {
    $.fn.grupoParceiros = function (_oSettings) {
        var multiplo = true;
        var hiddenId = $(this).prop('id');
        var selectId = $(this).prop('for');
        var url = 'grupo/listar/0';

        HelperJS.ComboAutoComplete(hiddenId, selectId, "Digite um código ou nome", url, multiplo,
            formataResultados, formata, funcao, 3, null, true);

    };

    function formataResultados(item) { return item.ID + item.Nome; };
    function formata(item) { return item.ID + item.Nome; };
    function funcao(item) { return item.ID; };

})(jQuery);


//function Grupo de acesso
(function ($) {
    $.fn.grupoAcesso = function () {
        var controle = $(this);
        HelperJS.dataBindComboChosen("/grupoacesso/listar/", controle, 'Descricao', 'ID', null, true);
        //function fnSuccess(data) {
        //    if (data != null && data.length > 0) {
        //        $.each(data, function (i, obj) {
        //            controle.append(new Option(obj.Descricao, obj.ID));
        //        });
        //    }
        //}

        //HelperJS.callApi({
        //    url: "/grupoacesso/listar/",
        //    type: "GET",
        //    data: null,
        //    functionOnSucess: fnSuccess,
        //    functionOnError: HelperJS.showError
        //});
    }
})(jQuery);
