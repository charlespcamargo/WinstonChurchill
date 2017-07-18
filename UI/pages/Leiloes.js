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

                colunas.push({
                    "mData": "ProdutoID",
                    "mRender": function (source, type, full) {
                        return full.Produto.ID + ' - ' + full.Produto.Nome;
                    }
                });

                colunas.push({ "mData": "DataFinalFormacao" });

                colunas.push({ "mData": "QtdDesejada" });

                colunas.push({ "mData": "DataAbertura" });

                colunas.push({
                    "mData": "RepresentanteID",
                    "mRender": function (source, type, full) {
                        return full.Representante.ID + ' - ' + full.Representante.Nome;
                    }
                });                               

                colunas.push({
                    "mData": "ID",
                    "mRender": function (source, type, full) {

                        var editar = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Editar' onclick='Leiloes.editar(" + source + ")' href='javascript:;'><i class='icon-edit'></i></a>";
                        
                        return "<center> " + editar + "</center>";
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


