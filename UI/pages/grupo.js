var Grupo = function () {
    var id = 0;
    var status = true;
    return {
        init: function () {
            $('#hfPaceiros').parceiros({ multiplo: true, tipo: 1 });
            $('#hfCategoria').categorias({ multiplo: true });
            Grupo.eventos();
            Grupo.listar();
        },

        eventos: function () {
            $('#btnNovo').click(function () {
                Grupo.abrirModal();
            });

            $('#btnSalvar').click(function () { Grupo.salvar(); });

            $('#ddlTipo').change(function () { Grupo.alterarTipo($(this).val()); });
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

                colunas.push({
                    mData: "ID", sClass: "text-left", sType: "string", mRender: function (source, type, full) {
                        var categorias = '';
                        if (full.GrupoCategoria && full.GrupoCategoria.length > 0) {
                            $.each(full.GrupoCategoria, function (i, obj) {
                                if (obj.Categoria)
                                    categorias += '|' + obj.Categoria.Nome;
                            });
                            categorias = categorias.substring(1);
                        }
                        return categorias;
                    }
                });

                colunas.push({
                    mData: "ID", sClass: "text-left", sType: "string", mRender: function (source, type, full) {
                        var grupos = '';
                        if (full.ParceiroNegocioGrupo && full.ParceiroNegocioGrupo.length > 0) {
                            $.each(full.ParceiroNegocioGrupo, function (i, obj) {
                                if (obj.Grupo)
                                    grupos += '|' + obj.Grupo.Nome;
                            });
                            grupos = grupos.substring(1);
                        }
                        return grupos;
                    }
                });

                colunas.push({ mData: "DataCadastro", sClass: "text-left", sType: "string" });

                colunas.push({
                    mData: "ID", sClass: "text-left", sType: "string", mRender: function (source, type, full) {
                        var categorias = '';
                        if (full.CategoriasGrupo && full.CategoriasGrupo.length > 0) {
                            $.each(full.CategoriasGrupo, function (i, obj) {
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
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="Grupo.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        var editar = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Editar" onclick="Grupo.editar(' + full.ID + ')"><i class="icon-edit"></i></a>';
                        var visualizarImagens = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Visualizar Imagens" onclick="Grupo.visualizarImagens(' + full.ID + ')"><i class="icon-search"></i></a>';
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
                url: "grupo/listar/",
                type: "POST",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },


        carregar: function (_id) {
            var fnSuccess = function (data) {
                $('#Dados').popularCampos({ data: data });
                if (data.CategoriasGrupo) {
                    var categorias = new Array();
                    $.each(data.CategoriasGrupo, function (i, obj) { categorias.push(obj.Categoria); });
                }
                ;
                $('#hfCategoria').select2("data", categorias);
                id = data.ID;
                status = data.Ativo;
                Caracteristicas.listar(true);
            }

            HelperJS.callApi({
                url: "grupo/" + _id,
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
            jsonSend.CategoriasGrupo = new Array();
            var categorias = $('#hfCategoria').getSelect2Data();

            $.each(categorias, function (i, categoria) {
                jsonSend.CategoriasGrupo.push({ CategoriaID: categoria.ID });
            });

            jsonSend.Caracteristicas = Caracteristicas.get();

            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados salvos com sucesso!");
                Grupo.fecharModal();
                Grupo.listar();
            }

            HelperJS.callApi({
                url: "grupo/salvar/",
                type: "POST",
                data: jsonSend,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },

        editar: function (_id) {
            Grupo.abrirModal();
            Grupo.carregar(_id);
        },

        excluir: function (_id) {
            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados excluídos com sucesso!");
                Grupo.listar();
            }

            var fnConfirmar = function () {
                HelperJS.callApi({
                    url: "grupo/" + _id,
                    type: "DELETE",
                    data: null,
                    functionOnSucess: fnSuccess,
                    functionOnError: HelperJS.showError
                });
            }

            HelperJS.confirmar('Deseja excluir o grupo?', fnConfirmar, null);
        },

        visualizarImagens: function (_id) {
            id = _id;
            Anexos.init();
            $('#modalImagens').modal('show');
        },


        alterarTipo: function (tipo) {
            switch (tipo) {
                case 1: // Comprador
                    this.alterarVisualizacao({ label: 'Compradores', tipo: 1 });
                    return;
                case 2: // Fornecedor
                    this.alterarVisualizacao({ label: 'Fornecedores', tipo: 2 });
                    return;
            }
        },

        alterarVisualizacao: function (_opcoes) {
            $('[label-grupo]').html(_opcoes.label);
            $('#hfPaceiros').parceiros({ multiplo: true, tipo: _opcoes.tipo });

        },
    };
}();

