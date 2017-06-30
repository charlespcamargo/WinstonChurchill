var Categoria = function () {
    var id = 0;
    var status = true;
    return {
        init: function () {
            Categoria.eventos();
            Categoria.listar();
        },

        eventos: function () {
            $('#btnNovo').click(function () {
                Categoria.abrirModal();
            });

            $('#btnSalvar').click(function () { Categoria.salvar(); });
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
                    mData: "ID", sClass: "text-left", sType: "numeric", mRender: function (source, type, full) {
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="Categoria.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        var editar = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Editar" onclick="Categoria.editar(' + full.ID + ')"><i class="icon-edit"></i></a>';
                        var visualizarImagens = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Visualizar Imagens" onclick="Categoria.visualizarImagens(' + full.ID + ')"><i class="icon-search"></i></a>';
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
                url: "categoria/listar/",
                type: "GET",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },


        carregar: function (_id) {
            var fnSuccess = function (data) {
                $('#Dados').popularCampos({ data: data });
                id = data.ID;
                status = data.Ativo;
            }

            HelperJS.callApi({
                url: "categoria/" + _id,
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

            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados salvos com sucesso!");
                Categoria.fecharModal();
                Categoria.listar();
            }

            HelperJS.callApi({
                url: "categoria/salvar/",
                type: "POST",
                data: jsonSend,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },

        editar: function (_id) {
            Categoria.abrirModal();
            Categoria.carregar(_id);
        },

        excluir: function (_id) {
            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados excluídos com sucesso!");
                Categoria.listar();
            }

            var fnConfirmar = function () {
                HelperJS.callApi({
                    url: "categoria/excluir/" + _id,
                    type: "POST",
                    data: null,
                    functionOnSucess: fnSuccess,
                    functionOnError: HelperJS.showError
                });
            }

            HelperJS.confirmar('Deseja excluir a categoria?', fnConfirmar, null);
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
                url: "categoriaImagem/anexar/" + Categoria.GetID(),
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
                url: "categoriaImagem/" + Categoria.GetID(),
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
                url: "categoriaImagem/excluir/" + _id,
                type: "POST",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        }
    }

}();