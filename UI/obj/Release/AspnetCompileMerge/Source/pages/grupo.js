var Grupo = function () {
    var id = 0;
    return {
        init: function () {
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

            $('#hfPaceiros').parceiros({ multiplo: true, tipo: 1 });
            this.alterarTipo(1);
        },

        fecharModal: function () {
            $("#modalNovo").modal('hide');
        },


        listar: function () {
            var fnColunas = function () {
                var colunas = new Array();
                colunas.push({ mData: "Nome", sClass: "text-left", sType: "string" });
                colunas.push({
                    mData: "TipoGrupo", sClass: "text-left", sType: "string", mRender: function (source, type, full) {
                        switch (source) {
                            case 1: return 'Comprador';
                            case 2: return 'Fornecedor';
                        }
                    }
                });
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
                        var parceiros = '';
                        if (full.ParceiroNegocioGrupo && full.ParceiroNegocioGrupo.length > 0) {
                            $.each(full.ParceiroNegocioGrupo, function (i, obj) {
                                if (obj.Parceiro)
                                    parceiros += '|' + obj.Parceiro.RazaoSocial;
                            });
                            parceiros = parceiros.substring(1);
                        }
                        return parceiros;
                    }
                });

                colunas.push({
                    mData: "ID", sClass: "text-left", sType: "numeric", mRender: function (source, type, full) {
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="Grupo.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        var editar = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Editar" onclick="Grupo.editar(' + full.ID + ')"><i class="icon-edit"></i></a>';
                        return editar + excluir;
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
                url: "grupo/listar/0",
                type: "GET",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },


        carregar: function (_id) {
            var fnSuccess = function (data) {
                $('#Dados').popularCampos({ data: data });
                if (data.GrupoCategoria) {
                    var categorias = new Array();
                    $.each(data.GrupoCategoria, function (i, obj) { categorias.push(obj.Categoria); });
                    $('#hfCategoria').select2("data", categorias);
                }

                if (data.ParceiroNegocioGrupo) {
                    var parceiros = new Array();
                    $.each(data.ParceiroNegocioGrupo, function (i, obj) { parceiros.push(obj.Parceiro); });
                    $('#hfPaceiros').select2("data", parceiros);
                }

                id = data.ID;
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
            jsonSend.GrupoCategoria = new Array();
            var categorias = $('#hfCategoria').getSelect2Data();

            $.each(categorias, function (i, categoria) {
                jsonSend.GrupoCategoria.push({ CategoriaID: categoria.ID });
            });


            jsonSend.ParceiroNegocioGrupo = new Array();
            var parceiros = $('#hfPaceiros').getSelect2Data();

            $.each(parceiros, function (i, parceiro) {
                jsonSend.ParceiroNegocioGrupo.push({ ParceiroID: parceiro.ID });
            });

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
                    url: "grupo/excluir/" + _id,
                    type: "POST",
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
            switch (parseInt(tipo)) {
                case 1: // Comprador
                    this.alterarVisualizacao({ label: 'Compradores', tipo: 1 });
                    break;
                case 2: // Fornecedor
                    this.alterarVisualizacao({ label: 'Fornecedores', tipo: 2 });
                    break;
            }
        },

        alterarVisualizacao: function (_opcoes) {
            $('[label-grupo]').html(_opcoes.label);
            $('#hfPaceiros').parceiros({ multiplo: true, tipo: _opcoes.tipo });

        },
    };
}();

