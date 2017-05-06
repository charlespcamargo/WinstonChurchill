var Produtos = function () {
    var id = 0;
    var status = true;
    return {
        init: function () {
            $('#hfCategoria').categorias({ multiplo: true });
            Produtos.eventos();
            Produtos.listar();
        },

        eventos: function () {
            $('#btnNovo').click(function () {
                Produtos.abrirModal();
            });

            $('#btnSalvar').click(function () { Produtos.salvar(); });
        },

        abrirModal: function () {
            $("#modalNovo").modal('layout');
            $("#modalNovo").modal('show');
            $('#Dados').limpar();
            id = 0;
            status = true;
        },

        fecharModal: function () {
            $("#modalNovo").modal('hide');
        },


        listar: function () {
            var fnColunas = function () {
                var colunas = new Array();
                colunas.push({ mData: "Nome", sClass: "text-left", sType: "string" });

                colunas.push({ mData: "Ativo", sClass: "text-left", sType: "string", mRender: function (source, type, full) { return source ? 'Ativo' : 'Inativo' } });

                colunas.push({ mData: "DataCadastro", sClass: "text-left", sType: "string" });

                colunas.push({
                    mData: "ID", sClass: "text-left", sType: "string", mRender: function (source, type, full) {
                        var categorias = '';
                        if (full.CategoriasProdutos && full.CategoriasProdutos.length > 0) {
                            $.each(full.CategoriasProdutos, function (i, obj) {
                                if (obj.Categoria)
                                    categorias += '|' + obj.Categoria.Nome;
                            });
                            categorias = categorias.substring(1);
                        }
                        return categorias;
                    }
                });

                colunas.push({
                    mData: "ID", sClass: "text-left", sType: "numeric", mRender: function (source, type, full) {
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="Produtos.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        var editar = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Editar" onclick="Produtos.editar(' + full.ID + ')"><i class="icon-edit"></i></a>';
                        var visualizarImagens = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Visualizar Imagens" onclick="Produtos.visualizarImagens(' + full.ID + ')"><i class="icon-search"></i></a>';
                        return visualizarImagens + editar + excluir;
                    }
                });

                return colunas;
            }

            var fnSuccess = function (data) {
                $('#gridItens').bindDataTable({
                    columns: fnColunas(),
                    sorter: [[0, 'asc']],
                    data: data,
                });
            }

            HelperJS.callApi({
                url: "produto/listar/",
                type: "POST",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },


        carregar: function (_id) {
            var fnSuccess = function (data) {
                $('#Dados').popularCampos({ data: data });
                if (data.CategoriasProdutos) {
                    var categorias = new Array();
                    $.each(data.CategoriasProdutos, function (i, obj) { categorias.push(obj.Categoria); });
                }
                ;
                $('#hfCategoria').select2("data", categorias);
                id = data.ID;
                status = data.Ativo;
            }

            HelperJS.callApi({
                url: "produto/" + _id,
                type: "GET",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });;
        },


        salvar: function () {
            if ($('#Dados').ehValido() == false)
                return;

            var jsonSend = $('#Dados').obterJson();
            jsonSend.ID = id;
            jsonSend.Ativo = status;
            jsonSend.CategoriasProdutos = new Array();
            var categorias = $('#hfCategoria').getSelect2Data();

            $.each(categorias, function (i, categoria) {
                jsonSend.CategoriasProdutos.push({ CategoriaID: categoria.ID });
            });

            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados salvos com sucesso!");
                Produtos.fecharModal();
                Produtos.listar();
            }

            HelperJS.callApi({
                url: "produto/salvar/",
                type: "POST",
                data: jsonSend,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },

        editar: function (_id) {
            Produtos.abrirModal();
            Produtos.carregar(_id);
        },

        excluir: function (_id) {
            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados excluídos com sucesso!");
                Produtos.listar();
            }

            var fnConfirmar = function () {
                HelperJS.callApi({
                    url: "produto/" + _id,
                    type: "DELETE",
                    data: null,
                    functionOnSucess: fnSuccess,
                    functionOnError: HelperJS.showError
                });
            }

            HelperJS.confirmar('Deseja excluir o produto?', fnConfirmar, null);
        }
    };
}();