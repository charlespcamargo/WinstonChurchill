var Usuarios = function () {

    var id = 0;
    return {

        init: function () {
            $('#ddlGrupoAcesso').grupoAcesso();
            Usuarios.carregarGrid();
            Usuarios.eventos();
        },


        eventos: function () {
            $('#btnNovo').click(function () {
                Usuarios.abrirModal();
            });

            $('#btnSalvar').click(function () { Usuarios.salvar(); });
        },

        carregarGrid: function () {

            var fnColunas = function () {
                var colunas = new Array();


                colunas.push({ "mData": "ID", "mRender": function (source, type, full) { return source; } });

                colunas.push({ "mData": "Nome" });

                colunas.push({ "mData": "Email" });

                colunas.push({
                    "mData": "Ativo",
                    "mRender": function (source, type, full) {
                        if (source)
                            return "Ativo";
                        else
                            return "Inativo";
                    }
                });

                colunas.push({ "mData": "DataCadastro" });

                colunas.push({
                    "mData": "ID",
                    "mRender": function (source, type, full) {
                        var editar = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Editar' onclick='Usuarios.editar(" + source + ")' href='javascript:;'><i class='icon-edit'></i></a>";
                        var excluir = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Remover' onclick='Usuarios.remover(" + source + ")' href='javascript:;'><i class='icon-remove'></i></a>";

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
                url: "/usuario/listar/",
                type: "POST",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },

        abrirModal: function () {
            $("#modalNovo").modal('show');
            $('#Dados').limpar();
            id = 0;
            $("#modalNovo").modal('layout');
            $('#lblSenha').html('Senha');
            $('[alt-senha]').hide();
        },

        editar: function (_id) {
            Usuarios.abrirModal();
            id = _id;
            Usuarios.carregar(_id);
            $('#lblSenha').html('Senha Atual');
            $('[alt-senha]').show();
        },

        carregar: function (_id) {
            var fnSuccess = function (data) {
                $('#Dados').popularCampos({ data: data });
                if (data.Grupos) {
                    var grupos = new Array();
                    $.each(data.Grupos, function (i, obj) { grupos.push(obj.GrupoUsuarioID); });
                }
                $('#ddlGrupoAcesso').val(grupos);
                $('#ddlGrupoAcesso').trigger("liszt:updated");
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

        salvar: function () {
            if ($('#Dados').ehValido() == false)
                return;

            var jsonSend = $('#Dados').obterJson();
            jsonSend.ID = id;
            jsonSend.Grupos = new Array();
            jsonSend.Ativo = $('#chkAtivo').is(':checked');

            var grupos = $('#ddlGrupoAcesso').val();
            $.each(grupos, function (i, obj) {
                if (obj)
                    jsonSend.Grupos.push({ GrupoUsuarioID: obj });
            });

            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados salvos com sucesso!");
                Usuarios.fecharModal();
                Usuarios.carregarGrid();
            }

            HelperJS.callApi({
                url: "usuario/salvar",
                type: "POST",
                data: jsonSend,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },

        excluir: function (_id) {

            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados excluídos com sucesso!");
                Produtos.carregarGrid();
            }

            var fnConfirmar = function () {
                HelperJS.callApi({
                    url: "usuario/" + _id,
                    type: "DELETE",
                    data: null,
                    functionOnSucess: fnSuccess,
                    functionOnError: HelperJS.showError
                });
            }

            HelperJS.confirmar('Deseja excluir o produto?', fnConfirmar, null);
        },

        fecharModal: function () {
            $("#modalNovo").modal('hide');
        },



    }

}();