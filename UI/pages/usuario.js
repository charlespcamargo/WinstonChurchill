var Usuarios = function () {

    var table;

    return {

        init: function () { 
            Usuarios.carregarGrid();
        },
        
        carregarGrid: function () {

            var fnColunas = function ()
            {
                var colunas = new Array();
                   

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

            var fnSuccess = function (data)
            {
                $('#gridItens').bindDataTable(
                {
                    columns: fnColunas(),
                    sorter: [[0, 'asc']],
                    data: data,
                });
            }

            HelperJS.callApi({
                url: "/usuario/listar/",
                type: "POST",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },
        
        abrirModal: function () {

            $("#modalNovo").modal('show');
            $('#Dados').limpar();
            id = 0;
            $("#modalNovo").modal('layout');
        },

        fecharModal: function () {
            $("#modalNovo").modal('hide');
        },

      

    }

}();