var Leiloes = function () {

    var id = 0;

    return {
        init: function () {
            Leiloes.carregarGrid();
            Leiloes.eventos();
        },

        eventos: function () {

            $('#btnNovo').click(function ()
            {
                window.location.href = '/pages/Leilao.aspx';
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

            window.location.href = '/pages/Leilao.aspx?id=' + _id;
            
        },

       

    }


}();


