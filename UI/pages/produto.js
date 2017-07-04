var Produtos = function () {
    var id = 0;
    var status = true;
    return {
        init: function () {
            $('#hfCategoria').categorias({ multiplo: true });
            Produtos.eventos();
            Produtos.listar();
            Caracteristicas.init();
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
            Caracteristicas.limparJson();
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
                    $('#hfCategoria').select2("data", categorias);
                }
                id = data.ID;
                status = data.Ativo;
                Caracteristicas.listar(true);
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

            jsonSend.Caracteristicas = Caracteristicas.get();

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
                    url: "produto/excluir/" + _id,
                    type: "POST",
                    data: null,
                    functionOnSucess: fnSuccess,
                    functionOnError: HelperJS.showError
                });
            }

            HelperJS.confirmar('Deseja excluir o produto?', fnConfirmar, null);
        },

        visualizarImagens: function (_id) {
            id = _id;
            Anexos.init();
            $('#modalImagens').modal('show');
        },

        GetID: function () {
            return id;
        }
    };
}();

var Caracteristicas = function () {
    var jsonCaracteristicas = new Array();
    return {
        init: function () {
            Caracteristicas.eventos();
        },

        get: function () {
            return jsonCaracteristicas;
        },

        set: function (obj) {
            if (!obj.ID)
                obj.index = (jsonCaracteristicas.length + 1) * -1;
            obj.ProdutoID = Produtos.GetID();
            jsonCaracteristicas.push(obj);
        },

        limparJson: function () {
            jsonCaracteristicas = new Array();
            Caracteristicas.listar(false);
        },

        eventos: function () {
            $('#btnAddCaracteristica').click(function () { Caracteristicas.inserir(); });
        },

        listar: function (requestOnServer) {
            var fnColunas = function () {
                var colunas = new Array();
                colunas.push({ mData: "Nome", sClass: "text-left", sType: "string" });
                colunas.push({
                    mData: "Nome", sClass: "text-left", sType: "numeric", mRender: function (source, type, full) {
                        var id;
                        if (full.ID)
                            id = full.ID;
                        else
                            id = full.index;
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="Caracteristicas.excluir(' + id + ')"><i class="icon-remove"></i></a>';
                        return excluir;
                    }
                });

                return colunas;
            }

            var fnSuccess = function (data) {
                jsonCaracteristicas = data;
                $('#gridCaracterísticas').bindDataTable({
                    columns: fnColunas(),
                    sorter: [[0, 'asc']],
                    data: data,
                });
            }

            if (requestOnServer)
                HelperJS.callApi({
                    url: "produto/listarCaracteristicas/" + Produtos.GetID(),
                    type: "GET",
                    data: null,
                    functionOnSucess: fnSuccess,
                    functionOnError: HelperJS.showError
                });
            else
                fnSuccess(jsonCaracteristicas);
        },

        excluir: function (_id) {
            jsonCaracteristicas = $.grep(jsonCaracteristicas, function (e) { return e.ID != _id && e.index != _id });
            Caracteristicas.listar(false);
        },

        inserir: function () {
            if ($('#Caracteristicas').ehValido() == false)
                return;
            var obj = $('#Caracteristicas').obterJson();

            Caracteristicas.set(obj);
            $('#Caracteristicas').limpar();
            Caracteristicas.listar(false);
        },
    };
}();

var Anexos = function () {
    return {
        init: function () {
            Anexos.configurarUpload();
            Anexos.eventos();
            Anexos.carregarGrid();
        },

        configurarUpload: function () {
            var fnUploadComplete = function (file) {
                Anexos.carregarGrid();
            }

            $("#file_upload").configurarUpload({
                ehMultiplo: true,
                url: "produtoImagens/anexar/" + Produtos.GetID(),
                onUploadComplete: fnUploadComplete,
                formato: "*.*",
                limiteFila: 20
            });
        },

        eventos: function () {
            $('#btnAnexar').click(function () {
                $('#file_upload').fazerUpload();
            });
        },

        carregarGrid: function () {
            var fnColunas = function () {
                var colunas = new Array();
                //colunas.push({ mData: "Nome", sClass: "text-left", sType: "string" });
                colunas.push({
                    mData: "ID", sClass: "text-left", sType: "string", mRender: function (source, type, full) {
                        return '<img src="' + full.Imagem.Url + '" style="width: 180px; heigth: 180px;"></img>'
                    }
                });

                colunas.push({
                    mData: "ID", sClass: "text-left", sType: "numeric", mRender: function (source, type, full) {
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="Anexos.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        return excluir;
                    }
                });

                return colunas;
            }

            var fnSuccess = function (data) {
                $('#gridImagens').bindDataTable({
                    columns: fnColunas(),
                    sorter: [[0, 'asc']],
                    data: data,
                });
            }

            HelperJS.callApi({
                url: "produtoImagens/" + Produtos.GetID(),
                type: "GET",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },

        excluir: function (_id) {
            var fnSuccess = function (data) {
                HelperJS.showSuccess("Imagem excluída com sucesso!");
                Anexos.carregarGrid();
            }

            HelperJS.callApi({
                url: "produtoImagens/excluir/" + _id,
                type: "POST",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        }
    }

}();