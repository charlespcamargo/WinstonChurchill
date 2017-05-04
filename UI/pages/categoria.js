var Categoria = function () {
    return {
        init: function () { CategoriaListar.init(); },
    };

}();



var CategoriaListar = function () {
    return {
        init: function () { },

        eventos: function () {
            $('#btnFiltrar').click(function () { CategoriaListar.carregar(); });
        },

        carregar: function () {

        },

        carregar_sucesso: function () {
            CategoriaComon.popularGrid('grdCategorias', colunas, data);
        },

        montarColunas: function () {
            var colunas = new Array();
            colunas.push({ mData: 'ID', mRender: function (source, type, full) { return source; } });
            colunas.push({ mData: "Nome" });

            colunas.push({ mData: "Email" });

            colunas.push({
                mData: "ID",
                mRender: function (source, type, full) {
                    var editar = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Editar' onclick='CategoriaListar.editar(" + source + ")' href='javascript:;'><i class='icon-edit'></i></a>";
                    var excluir = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Remover' onclick='CategoriaListar.remover(" + source + ")' href='javascript:;'><i class='icon-remove'></i></a>";

                    return "<center> " + editar + excluir + "</center>";
                }
            });

            return colunas;
        },

        montarFiltros: function () {
            var filtros = new Object();

            return filtros;
        },


    };

}();


var CategoriaComon = function () {
    return {
        popularGrid: function (idGrid, colunas, data) {
            HelperJS.dataTableResult(idGrid, colunas, [[0, 'asc'], [1, 'asc']], data);
        },
    };

}();