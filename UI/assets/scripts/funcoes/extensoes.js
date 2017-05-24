
//Obtem o texto de um controle
$.fn.getText = function () {
    ///chama o getType do HelperJS.js para pegar o tipo do controle
    var tipo = $(this).getType();
    var controleId = $(this).attr('Id');
    var valores = null;
    switch (tipo) {
        case "chosen":  // se o controle for do tipo chosen então eu retorno uma array separado com os valores, pode ser do tipo multiplo e único.
            valores = new Array();
            valores = $("#" + controleId + " option:selected").map(function () {
                return $(this).text();
            }).get(); //Precisa usar o map pra poder pegar os delimitadores entre os texto, pq o texto do controle que usa multiple não tem virgula entre os texto, como é nos valores
            break;
        case "select":
            valores = $("#" + controleId + " option:selected").text();
            break;
    }
    return valores;
};



$.fn.getSelect2Data = function (propriedade) {
    var controle = $(this);
    var obj = controle.select2('data');
    if (obj == null)
        return obj;

    if (propriedade != null && propriedade != '')
        return $(obj).prop(propriedade);
    else
        return obj;
};


//id, columns, sorter, data, paginate, sort, fnDrawCallback, fnRowCallback
$.fn.bindDataTable = function (_oSettings) {
    var controle = $(this);
    controle.dataTable().fnDestroy();

    if (_oSettings.paginate == undefined) {
        _oSettings.paginate = true;
    }

    if (_oSettings.sort == undefined) {
        _oSettings.sort = true;
    }

    var tabela = controle.dataTable({
        "aLengthMenu": [
            [5, 10, 15, 20, 50, 100], //, -1
            [5, 10, 15, 20, 50, 100] //, "Todos"  // change per page values here
        ],
        // set the initial value
        "iDisplayLength": controle.attr("data-qtdRegistros") != undefined ? parseInt(controle.attr("data-qtdRegistros")) : 10,
        "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span12'i><'span12 text-align-center no-margin-left margin-top-5'p>>",
        "sPaginationType": "bootstrap",
        "bSort": _oSettings.sort,
        "bDestroy": true,
        "bRetrieve": true,
        "bStateSave": true,
        "bPaginate": _oSettings.paginate,
        "aaSorting": _oSettings.sorter,
        "oLanguage": {
            "sLengthMenu": "Registros por p&aacute;gina: <br/> _MENU_",
            "sInfo": "Mostrando de _START_ a _END_ de um total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 de 0 total de 0",
            "sInfoFiltered": "(filtrado de _MAX_ registros)",
            "sSearch": "Filtrar:<br/>",
            "sZeroRecords": "Nenhum registro encontrado",
            "oPaginate": {
                "sPrevious": "Anterior",
                "sNext": "Pr&oacute;ximo"
            }
        },
        "aaData": _oSettings.data,
        "aoColumns": _oSettings.columns,
        "cache": false,
        "fnDrawCallback": function (oSettings) {
            if (_oSettings.fnDrawCallback != undefined && _oSettings.fnDrawCallback != null) {
                _oSettings.fnDrawCallback(oSettings);
            }
        },
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            if (_oSettings.fnRowCallback != undefined & _oSettings.fnRowCallback != null) {
                return _oSettings.fnRowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull);
            }
        }
    });


    var CreateColumns = function (cols) {
        var colunas = [];
        for (i in cols) {
            var ao = { "mDataProp": "string" };
            colunas.push(ao);
        }
        return colunas;
    }

    return tabela;
};

$.fn.ehValido = function () {
    var listMsg = new Array();
    var id = $(this).attr('id');
    $('#' + id + ' *[validate-json]').each(function (i, obj) {
        if ($(obj).val() == "") {
            var label = $(obj).attr('validate-json');

            if (label)
                listMsg.push({ Mensagem: label, IdControle: '#' + $(obj).attr('id') });
        }
    });

    if (listMsg.length > 0) {
        HelperJS.showListaAlert(listMsg);
        return false;
    }
    return true;
};


$.fn.obterJson = function (_oSettings) {
    var objRequest = {};
    var id = $(this).attr('id');
    var idPesquisa;
    if (_oSettings) {
        if (!_oSettings.dataField) {
            _oSettings.dataField = "data-json";
        }
        idPesquisa = HelperJS.getId(id, _oSettings.dataField);
    }
    else {
        _oSettings = new Object();
        _oSettings.dataField = "data-json";
        idPesquisa = HelperJS.getId(id, null);

    }

    $(idPesquisa).each(function () {


        if ($(this).hasClass("maskdecimal") || $(this).hasClass("masknegativo")) {
            objRequest[$(this).attr(_oSettings.dataField)] = $(this).toDecimal();
        }

        else if ($(this).hasClass("maskdolar"))
            objRequest[$(this).attr(_oSettings.dataField)] = $(this).preparaDolar();

        else if ($(this).hasClass("date-picker"))
            objRequest[$(this).attr(_oSettings.dataField)] = HelperJS.RecuperarData($(this).val());


        //inicio - esse pedaço é usado para montar o json quando usamos o componente de data com intervalo, ele já monta o objeto com a data inicial e final
        // informar no atributo do html o datafield ex: data-json="NomeAtributoJson1,NomeAtributoJson1", senão ele pega por padrão (DataInicio,DataFim)
        //Pode incluir qtos atributos achar necessário no atributo
        else if ($(this).hasClass("periododata")) {
            var atributosSplit = null;
            if ($(this).attr(_oSettings.dataField) != undefined) {
                atributosSplit = $(this).attr(_oSettings.dataField).split(',');
            }
            else {
                var itens = 'DataInicio,DataFim';
                atributosSplit = itens.split(',');
            }
            var dataSplit = $(this).val().split('-');
            $.each(dataSplit, function (i, obj) {
                objRequest[atributosSplit[i]] = (obj != undefined && obj != '' ? HelperJS.RecuperarData(obj.toString().trim()) : null);
            });
        }
        //fim - esse pedaço é usado para montar o json quando usamos o componente de data com intervalo, ele já monta o objeto com a data inicial e final


        //inicio - esse pedaço é usado para montar o json quando usamos intervalo de valor. Exemplo: 0-100
        // informar no atributo do html o datafield ex: data-json="Valor1,Valor1", senão ele pega por padrão (Range1,Range2). 
        // Pode incluir qtos atributos achar necessário no atributo
        else if ($(this).hasClass("rangepadrao")) {
            var atributosSplit = null;
            if ($(this).attr(_oSettings.dataField) != undefined) {
                atributosSplit = $(this).attr(_oSettings.dataField).split(',');
            }
            else {
                var itens = 'Range1,Range2';
                atributosSplit = itens.split(',');
            }
            var dataSplit = $(this).val().split('-');
            $.each(dataSplit, function (i, obj) {
                objRequest[atributosSplit[i]] = (obj != undefined && obj != '' ? obj.toString().trim() : null);
            });
        }
        //fim - esse pedaço é usado para montar o json quando usamos intervalo de valor. Exemplo: 0-100

        else
            objRequest[$(this).attr(_oSettings.dataField)] = $(this).val();
    });

    return objRequest;
};


$.fn.popularCampos = function (_oSettings) {
    var id = $(this).attr('id');
    if (!_oSettings) {
        return;
    }
    var idPesquisa;
    if (_oSettings)
        idPesquisa = HelperJS.getId(id, _oSettings.dataField);
    else
        idPesquisa = HelperJS.getId(id, null);

    if (_oSettings.dataField == null || _oSettings.dataField == undefined) {
        _oSettings.dataField = "data-json";
    }

    $(idPesquisa).each(function () {
        var tipo = $(this).getType();
        var value;
        var bindDataField = $(this).attr(_oSettings.dataField);

        if (bindDataField.split('.').length > 1) { // nesse caso eu posso recuperar e preencher um controle que contenha várias propriedades. Ex: data-json="Evento.Participante.Nome"
            var objSplit = bindDataField.split('.');
            var objAux = _oSettings.data[objSplit[0]];
            if (objAux != null) {
                for (var i = 1; i < objSplit.length; i++) {
                    objAux = objAux[objSplit[i]];
                }
                value = objAux;
            }
        }
        else {
            value = _oSettings.data[bindDataField];
        }

        if (value != null && value != undefined) {
            switch (tipo) {
                case 'text':
                    $(this).val(value);
                    break;
                case "hidden":
                    $(this).val(value);
                    break;
                case 'select':
                    $(this).val(value);
                    break;
                case "checkbox":
                case "radio":
                    //Quando utilizar fazwer testes
                    $(this).attr("checked", value);
                    $.uniform.update($(this));
                    break;
                case "select2":
                    HelperJS.popularSelect2($(this).prop('id'), value);
                    break;
                case "chosen":
                    $(this).val(value).trigger("liszt:updated");
                    break;
                default:
                case "html":
                    $(this).html(value);
                    break;
                case "textarea":
                    $(this).val(value);
                    break;
            }
        }
        else { // caso seja nulo eu obrigo o select (chosen) ficar desmarcado
            switch (tipo) {
                case "chosen":
                    $(this).val('').trigger("liszt:updated");
                    break;
            }
        }
    });

};



$.fn.limpar = function (_oSettings) {
    var id = $(this).attr('id');
    var idPesquisa;
    if (_oSettings)
        idPesquisa = HelperJS.getId(id, _oSettings.dataField);
    else
        idPesquisa = HelperJS.getId(id, null);
    $(idPesquisa).each(function () {
        var tipo = $(this).getType();

        switch (tipo) {
            case "text":
                $(this).val("");
                break;
            case "hidden":
                $(this).val("");
                break;
            case "checkbox":
            case "radio":
                var elemento = $(this).attr("checked", false);
                $.uniform.update(elemento);
                break;
            case "select":
                $(this).prop('selectedIndex', 0);
                break;
            case "select2":
                HelperJS.popularSelect2($(this).prop('id'), null);
                break;
            case "chosen":
                $(this).val('').trigger("liszt:updated");
                break;
            case "html":
                $(this).empty();
                break;
            case "textarea":
                $(this).val('');
                break;
        }
        $(this).removeClass("required");
    });
};


//INICIO - Configura o upload
$.fn.configurarUpload = function (_oSettings) {
    if (!_oSettings) return;

    if (HelperJS.temSuporteFlash()) { // se tiver suporte ao flash usa o uploadfy
        HelperJS.iniciarUploadify("#" + $(this[0]).attr('id'), _oSettings.ehMultiplo, _oSettings.url, _oSettings.onUploadComplete, _oSettings.formato, _oSettings.limiteFila);
    }
    else { // Senão tiver suporte ao flash, usa o uploadifive - HTML 5
        HelperJS.iniciarUploadifive("#" + $(this[0]).attr('id'), _oSettings.ehMultiplo, _oSettings.url, _oSettings.onUploadComplete, formato, _oSettings.limiteFila);
    }
};


$.fn.fazerUpload = function () {
    if (HelperJS.temSuporteFlash()) { // se tiver suporte ao flash usa o uploadfy
        $(this[0]).uploadify('upload', '*');
    }
    else { // Senão tiver suporte ao flash, usa o uploadifive - HTML 5
        $(this[0]).uploadifive('upload');
    }
};

$.fn.cancelarUpload = function () {
    if (HelperJS.temSuporteFlash()) { // se tiver suporte ao flash usa o uploadfy
        $(this[0]).uploadify('cancel');
    }
    else {
        $(this[0]).uploadifive('cancel');
    }
};

//Fim - Configura o upload


$.fn.toDecimal = function () {
    var valor = $(this[0]).val();
    while (valor.indexOf(".") > 0) {
        valor = valor.replace(".", "");
    }

    while (valor.indexOf(",") > 0) {
        valor = valor.replace(",", ".");
    }
    return valor;
};


// carrega endereço
(function ($) {
    $.fn.endereco = function (_oSettings) {
        $(this).change(function () {
            var url = 'cep/';

            function fnSuccess(data) {
                if (_oSettings.objeto) {
                    data[_oSettings.objeto] = data;
                }
                $(_oSettings.elemento).popularCampos({ data: data });
            }

            HelperJS.callApi({
                url: url + $(this).val(),
                type: "GET",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });;
        });
    };

})(jQuery);

// carrega endereço
(function ($) {
    $.fn.UF = function () {
        let controle = $(this);
        let uf = [
            { id: 'AC' }, { id: 'AL' }, { id: 'AP' }, { id: 'AM' }, { id: 'BA' }, { id: 'CE' }, { id: 'DF' }, { id: 'ES' }, { id: 'GO' },
            { id: 'MA' }, { id: 'MT' }, { id: 'MS' }, { id: 'MG' }, { id: 'PA' }, { id: 'PB' }, { id: 'PR' }, { id: 'PE' }, { id: 'PI' },
            { id: 'RJ' }, { id: 'RN' }, { id: 'RS' }, { id: 'RO' }, { id: 'RR' }, { id: 'SC' }, { id: 'SP' }, { id: 'SE' }, { id: 'TO' }
        ];

        uf.forEach(function (e) { controle.append(new Option(e.id, e.id)) });
    };

})(jQuery);