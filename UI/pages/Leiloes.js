var Leiloes = function () {

    var id = 0;

    return {
        init: function () {
            Leiloes.carregarGrid();
            Leiloes.eventos();
        },

        eventos: function () {
            $('#btnNovo').click(function () {
                Leiloes.abrirModal();
            });
        },

        carregarGrid: function () {

            var fnColunas = function () {
                var colunas = new Array();


                colunas.push({ "mData": "ID", "mRender": function (source, type, full) { return source; } });

                colunas.push({ "mData": "Nome" });

                colunas.push({ "mData": "ProdutoID" });

                colunas.push({
                    "mData": "Ativo",
                    "mRender": function (source, type, full) {
                        if (source)
                            return "Ativo";
                        else
                            return "Inativo";
                    }
                });

                colunas.push({
                    "mData": "ID"
                });

                colunas.push({
                    "mData": "ID",
                    "mRender": function (source, type, full) {
                        var editar = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Editar' onclick='Leiloes.editar(" + source + ")' href='javascript:;'><i class='icon-edit'></i></a>";
                        var excluir = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Remover' onclick='Leiloes.excluir(" + source + ")' href='javascript:;'><i class='icon-remove'></i></a>";

                        return "<center> " + editar + excluir + "</center>";
                    }
                });

                return colunas;
            }

            var fnSuccess = function (data) {
                $('#gridItens').bindDataTable(
                    {
                        columns: fnColunas(),
                        sorter: [[0, 'asc']],
                        data: data,
                    });
            }

            HelperJS.callApi({
                url: "leilao/listar/",
                type: "POST",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },

        editar: function (_id) {
            Leiloes.abrirModal();
            id = _id;
            Leiloes.carregar(_id);
            $('#lblSenha').html('Senha Atual');
            $('[alt-senha]').show();
        },

        carregar: function (_id) {
            var fnSuccess = function (data) {
                $('#Dados').popularCampos({ data: data });
                
                id = data.ID;
            }

            HelperJS.callApi({
                url: "usuario/" + _id,
                type: "GET",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });

        },

    }


}();


