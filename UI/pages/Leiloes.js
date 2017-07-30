var Leiloes = function () {

    var id = 0;
    var edit = false;

    return {

        init: function () {
            Leiloes.eventos();
            Leiloes.obterGrupo();
        },

        obterGrupo: function () {
            HelperJS.callApi({ url: "usuario/listarGrupos", type: "GET", data: null, functionOnSucess: Leiloes.gruposObtidos, functionOnError: HelperJS.showError });
        },

        gruposObtidos: function (lst) {

            if ($.grep(lst, function (e) { return e == "S" || e == "A" || e == "R" }).length >= 1)
                edit = true;

            Leiloes.carregarGrid();
        },

        eventos: function () {

            $('#btnNovo').click(function () {
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
                    "mData": "Ativo",
                    "mRender": function (source, type, full) {
                        return source ? "Sim" : "Não";
                    }
                });

                colunas.push({
                    "mData": "ID",
                    "mRender": function (source, type, full) {

                        var editar = "";

                        if (edit)
                            editar = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Editar' onclick='Leiloes.editar(" + source + ")' href='javascript:;'><i class='icon-edit'></i></a>";

                        var rodadas = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Lances' onclick='Leiloes.lances(" + source + ")' href='javascript:;'><i class='icon-legal'></i></a>";

                        return "<center> " + editar + rodadas + "</center>";
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

        lances: function (_id) {
            window.location.href = 'LeilaoLances.aspx?id=' + _id;
        }

    }


}();


