
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
}


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
}