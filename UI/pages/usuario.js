var Usuarios = function () {

    var table;

    return {

        init: function () {
            //Usuarios.inicializaGrid();
            Usuarios.carregarGrid();
        },


        carregarGrid: function () {

            var param = [];
            var fnDrawCallback = function (oSettings) {
                alert('DataTables has redrawn the table');
            }

            table = HelperJS.dataTableServer("gridItens", Usuarios.montarColunasGrid(), APIs.API, "/usuario/listar/", param, Usuarios.carregarSucesso);

            //table.fnReloadAjax();
        },

        carregarSucesso: function (data) {
            console.log("data", data);
        },

        inicializaGrid: function () {
            HelperJS.dataTableResult("gridItens", Usuarios.montarColunasGrid(), [[0, 'asc'], [1, 'asc']], []);
        },


        montarColunasGrid: function () {

            var colunas = [];

            colunas.push({ "mData": "ID", "mRender": function (source, type, full) { return source; } });

            colunas.push({ "mData": "Nome" });

            colunas.push({ "mData": "Email" });

            colunas.push({
                "mData": "Ativo",
                "mRender": function (source, type, full) {
                    if (source)
                        return "Ativo";
                    else
                        return "Inativo";
                }
            });

            colunas.push({ "mData": "DataCadastro" });

            colunas.push({
                "mData": "ID",
                "mRender": function (source, type, full) {
                    var editar = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Editar' onclick='Usuarios.editar(" + source + ")' href='javascript:;'><i class='icon-edit'></i></a>";
                    var excluir = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Remover' onclick='Usuarios.remover(" + source + ")' href='javascript:;'><i class='icon-remove'></i></a>";

                    return "<center> " + editar + excluir + "</center>";
                }
            });

            return colunas;
        }

    }

}();