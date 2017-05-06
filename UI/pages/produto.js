var Produtos = function () {
    return {
        init: function () {
            Produtos.eventos();
            Produtos.listar();
        },

        eventos: function () {
            $('#btnNovo').click(function () {
                $("#modalNovo").modal('layout');
                $("#modalNovo").modal('show');
            });

            $('#btnSalvar').click(function () {Produtos.salvar();});
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
                                    categorias += ' | ' + obj.Categoria.Nome;
                            });
                            categorias = categorias.substring(1);
                        }
                        return categorias;
                    }
                });

                colunas.push({
                    mData: "ID", sClass: "text-left", sType: "numeric", mRender: function (source, type, full) {
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="FCCI.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        var editar = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Editar" onclick="FCCI.editar(' + full.ID + ')"><i class="icon-edit"></i></a>';
                        var visualizarImagens = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Visualizar Imagens" onclick="FCCI.visualizarImagens(' + full.ID + ')"><i class="icon-search"></i></a>';
                        return visualizarImagens + editar + excluir;
                    }
                });

                return colunas;
            }

            var fnSucess = function (data) {
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
                functionOnSucess: fnSucess,
                functionOnError: HelperJS.showError
            });
        },


        carregar: function () {
            var fnSucesso = function () {
                HelperJS.bindJsonCrud(data, "Dados", "data-json");
                $('*[campos-valores]').each(function (i, obj) {
                    var controle = $(obj);
                    var codigo = controle.val();
                    var ref = controle.attr('campos-valores');
                    if (codigo != "")
                        Produtos.carregarCombos(controle, codigo, ref);
                });
            }

            HelperJS.callApi(APIs.API_ADMINISTRATIVO, "produto/carregar/", "GET", null, fnSucesso, HelperJS.showError);
        },

        validarCampos: function () {
            var listMsg = new Array();
            $('*[dataFieldHelperJS]').each(function (i, obj) {
                if ($(obj).val() == "") {
                    listMsg.push({ Mensagem: $('#' + $(obj).attr('for')).html() + " é obrigatório", IdControle: '#' + $(obj).attr('id') });
                }
            });
            if (listMsg.length > 0) {
                HelperJS.showListaAlert(listMsg);
                return false;
            }
            return true;
        },

        salvar: function () {
            if (Produtos.validarCampos()) {
                var obj = HelperJS.getJsonCrud("Dados", "dataFieldHelperJS");
                HelperJS.callApi(APIs.API_ADMINISTRATIVO, "parametroscontabeis/salvar/", "POST", obj, Produtos.salvar_sucesso, HelperJS.showError);
            }
        },

        salvar_sucesso: function (data) {
            HelperJS.showSuccess("Dados alterados com sucesso");
        }
    };
}();