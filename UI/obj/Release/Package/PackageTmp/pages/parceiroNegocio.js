var PN = function () {
    var id = 0;
    var status = true;
    return {
        init: function () {
            $('#ddlUF').UF();
            $('#txtCEP').endereco({ elemento: '#Endereco', objeto: 'Endereco' });
            $('#hfProdutoComprador').produtos();
            $('#hfProdutoFornecedor').produtos();
            $('#hfGrupoParceiro').grupoParceiros();

            PN.eventos();
            PN.listar();
            CompradorProduto.init();
            FornecedorProduto.init();
            Contatos.init();
        },

        eventos: function () {
            $('#btnNovo').click(function () {
                PN.abrirModal();
            });

            $('#btnSalvar').click(function () { PN.salvar(); });

            $('#ddlTipoPaceiro').change(function () { PN.configurarVisualizacao($(this).val()); });
        },

        abrirModal: function () {
            $("#modalNovo").modal('layout');
            $("#modalNovo").modal('show');
            $('#formDados').limpar();
            $('#formContatos').limpar();
            $('#formFornecedor').limpar();
            $('#formComprador').limpar();
            id = 0;
            status = true;
            FornecedorProduto.limparJson();
            CompradorProduto.limparJson();
            Contatos.limparJson();
            PN.configurarVisualizacao(1);
        },

        fecharModal: function () {
            $("#modalNovo").modal('hide');
        },


        listar: function () {
            var fnColunas = function () {
                var colunas = new Array();
                colunas.push({ mData: "CNPJ", sClass: "text-left", sType: "string" });
                colunas.push({ mData: "RazaoSocial", sClass: "text-left", sType: "string" });
                colunas.push({ mData: "NomeFantasia", sClass: "text-left", sType: "string" });
                colunas.push({ mData: "Telefone", sClass: "text-left", sType: "string", mRender: function (source, type, full) { return HelperJS.formatarTelefone(source); } });
                colunas.push({ mData: "Celular", sClass: "text-left", sType: "string", mRender: function (source, type, full) { return HelperJS.formatarTelefone(source); } });
                colunas.push({ mData: "Email", sClass: "text-left", sType: "string" });
                colunas.push({
                    mData: "TipoParceiro", sClass: "text-left", sType: "string", mRender: function (source, type, full) {
                        switch (source) {
                            case 1: return 'Comprador';
                            case 2: return 'Fornecedor';
                            case 3: return 'Comprador/Fornecedor';
                        }
                    }
                });
                colunas.push({
                    mData: "ID", sClass: "text-left", sType: "numeric", mRender: function (source, type, full) {
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="PN.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        var editar = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Editar" onclick="PN.editar(' + full.ID + ')"><i class="icon-edit"></i></a>';
                        return editar + excluir;
                    }
                });

                return colunas;
            }

            var fnSuccess = function (data) {
                $('#gridParceiros').bindDataTable({
                    columns: fnColunas(),
                    sorter: [[0, 'asc']],
                    data: data,
                });
            }

            HelperJS.callApi({
                url: "parceiroNegocio/listar/",
                type: "POST",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },


        carregar: function (_id) {
            var fnSuccess = function (data) {
                $('#formDados').popularCampos({ data: data });

                Contatos.listar(data.Contatos);
                CompradorProduto.listar(data.CompradorProduto);
                FornecedorProduto.listar(data.FornecedorProduto);

                if (data.Grupos) {
                    var grupos = new Array();
                    $.each(data.Grupos, function (i, obj) { grupos.push(obj.Grupo); });
                    $('#hfGrupoParceiro').select2("data", grupos);
                }

                id = data.ID;
                status = data.Ativo;
                PN.configurarVisualizacao($('#ddlTipoPaceiro').val());
            }

            HelperJS.callApi({
                url: "parceiroNegocio/" + _id,
                type: "GET",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });;
        },


        salvar: function () {
            if ($('#formDados').ehValido() == false) return;

            //   if (HelperJS.validarDocumento('#' + $('[data-json="CNPJ"]').attr('id'), 'CNPJ inválido') == false) return;

            var jsonSend = $('#formDados').obterJson();
            jsonSend.ID = id;
            jsonSend.Ativo = status;

            jsonSend.FornecedorProduto = FornecedorProduto.get();
            jsonSend.CompradorProduto = CompradorProduto.get();
            jsonSend.Contatos = Contatos.get();

            jsonSend.Grupos = new Array();
            var grupos = $('#hfGrupoParceiro').getSelect2Data();

            $.each(grupos, function (i, grupo) {
                jsonSend.Grupos.push({ GrupoID: grupo.ID });
            });

            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados salvos com sucesso!");
                PN.fecharModal();
                PN.listar();
            }

            HelperJS.callApi({
                url: "parceiroNegocio/salvar/",
                type: "POST",
                data: jsonSend,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },

        editar: function (_id) {
            PN.abrirModal();
            PN.carregar(_id);
        },

        excluir: function (_id) {
            var fnSuccess = function (data) {
                HelperJS.showSuccess("Dados excluídos com sucesso!");
                PN.listar();
            }

            var fnConfirmar = function () {
                HelperJS.callApi({
                    url: "parceiroNegocio/" + _id,
                    type: "DELETE",
                    data: null,
                    functionOnSucess: fnSuccess,
                    functionOnError: HelperJS.showError
                });
            }

            HelperJS.confirmar('Deseja excluir o PN?', fnConfirmar, null);
        },

        visualizarImagens: function (_id) {
            id = _id;
            Anexos.init();
            $('#modalImagens').modal('show');
        },

        GetID: function () {
            return id;
        },

        configurarVisualizacao: function (tipo) {
            switch (parseInt(tipo)) {
                case 1: this.alterarVisualizacao('tabComprador'); break;
                case 2: this.alterarVisualizacao('tabFornecedor'); break;
                case 3: this.alterarVisualizacao(null); break;
            }
        },

        alterarVisualizacao: function (controle) {
            if (controle == null) {
                $('[data-toggle="tab"]').each(function () {
                    $(this).show();
                });

                $('#tabComprador').addClass('active');
                $('#tabFornecedor').removeClass('active');
            }
            else {
                $('[data-toggle="tab"]').each(function () {
                    $(this).hide();
                });

                $('.tab-pane').each(function () {
                    if ($(this).attr('id') == controle) {
                        $(this).addClass('active');
                    } else {
                        $(this).removeClass('active');
                    }
                });
            }
        }
    };
}();

var FornecedorProduto = function () {
    var json = new Array();
    var id = 0;
    return {
        init: function () {
            FornecedorProduto.eventos();
        },

        get: function () {
            return json;
        },

        set: function (obj) {
            if (!obj.ID) {
                let max = json.length > 0 ? HelperJS.getMax(json, 'ID', true) : 0;
                obj.ID = max - 1;
            }
            obj.ParceiroID = PN.GetID();
            json = $.grep(json, function (e) { return e.ID != obj.ID });
            json.push(obj);
        },

        limparJson: function () {
            json = new Array();
            FornecedorProduto.listar();
        },

        eventos: function () {
            $('#btnAddProdutoFornecedor').click(function () { FornecedorProduto.inserir(); });
        },

        listar: function (data) {
            var fnColunas = function () {
                var colunas = new Array();
                colunas.push({ mData: "Produto.Nome", sClass: "text-left", sType: "string" });
                colunas.push({ mData: "Valor", sClass: "text-left", sType: "decimal", mRender: function (source, type, full) { return 'R$' + HelperJS.formatMoney(source, 2, '.', ','); } });
                colunas.push({ mData: "Volume", sClass: "text-left", sType: "decimal" });
                colunas.push({ mData: "CapacidadeMaxima", sClass: "text-left", sType: "decimal" });
                colunas.push({
                    mData: "Produto.Nome", sClass: "text-left", sType: "numeric", mRender: function (source, type, full) {
                        var editar = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Editar" onclick="FornecedorProduto.editar(' + full.ID + ')"><i class="icon-edit"></i></a>';
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="FornecedorProduto.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        return editar + excluir;
                    }
                });

                return colunas;
            }

            if (data)
                json = data;
            $('#gridProdutoFornecedor').bindDataTable({
                columns: fnColunas(),
                sorter: [[0, 'asc']],
                data: json,
            });
        },

        excluir: function (_id) {
            json = $.grep(json, function (e) { return e.ID != _id });
            FornecedorProduto.listar();
        },

        editar: function (_id) {
            var data = $.grep(json, function (e) { return e.ID == _id })[0];
            id = data.ID;
            $('#formFornecedor').popularCampos({ data: data });
            $('#txtValor').val(HelperJS.formatMoney(data.Valor, 2, '.', ','));
            $('#hfProdutoFornecedor').select2("data", data.Produto);
        },

        inserir: function () {
            if ($('#formFornecedor').ehValido() == false)
                return;

            var obj = $('#formFornecedor').obterJson();
            obj.Produto = $('#hfProdutoFornecedor').getSelect2Data();
            obj.ID = id

            function fnAny(result) {
                if (!result || result.ID !== 0 && result.ID === obj.ID) return false;

                let listMsg = new Array();
                listMsg.push({ Mensagem: 'O produto: ' + obj.Produto.Nome + ' já foi adicionado', IdControle: '' });
                HelperJS.showListaAlert(listMsg);
                return true;
            }

            if (HelperJS.any('Produto.ID', json, obj.Produto.ID, fnAny)) {
                return;
            }
;
            FornecedorProduto.set(obj);
            $('#formFornecedor').limpar();
            FornecedorProduto.listar();
            id = 0;
        },
    };
}();

var CompradorProduto = function () {
    var json = new Array();
    var id = 0;
    return {
        init: function () {
            CompradorProduto.eventos();
        },

        get: function () {
            return json;
        },

        set: function (obj) {
            if (!obj.ID) {
                let max = json.length > 0 ? HelperJS.getMax(json, 'ID', true) : 0;
                obj.ID = max - 1;
            }
            obj.ParceiroID = PN.GetID();
            json = $.grep(json, function (e) { return e.ID != obj.ID });
            json.push(obj);
        },

        limparJson: function () {
            json = new Array();
            CompradorProduto.listar();
        },

        eventos: function () {
            $('#btnAddProdutoComprador').click(function () { CompradorProduto.inserir(); });
        },

        listar: function (data) {
            var fnColunas = function () {
                let colunas = new Array();
                colunas.push({ mData: "Produto.Nome", sClass: "text-left", sType: "string" });
                colunas.push({ mData: "ValorMedioCompra", sClass: "text-left", sType: "decimal", mRender: function (source, type, full) { return 'R$' + HelperJS.formatMoney(source, 2, '.', ','); } });
                colunas.push({ mData: "Quantidade", sClass: "text-left", sType: "decimal" });
                colunas.push({ mData: "Frequencia", sClass: "text-left", sType: "decimal" });
                colunas.push({
                    mData: "Produto.Nome", sClass: "text-left", sType: "numeric", mRender: function (source, type, full) {
                        var editar = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Editar" onclick="CompradorProduto.editar(' + full.ID + ')"><i class="icon-edit"></i></a>';
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="CompradorProduto.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        return editar + excluir;
                    }
                });

                return colunas;
            }

            if (data)
                json = data;
            $('#gridProdutoComprador').bindDataTable({
                columns: fnColunas(),
                sorter: [[0, 'asc']],
                data: json,
            });

        },

        excluir: function (_id) {
            json = $.grep(json, function (e) { return e.ID != _id });
            CompradorProduto.listar();
        },

        editar: function (_id) {
            let data = $.grep(json, function (e) { return e.ID == _id })[0];
            id = data.ID;

            $('#formComprador').popularCampos({ data: data });
            $('#txtValorMedioCompra').val(HelperJS.formatMoney(data.ValorMedioCompra, 2, '.', ','));
            $('#hfProdutoComprador').select2("data", data.Produto);
        },

        inserir: function () {
            if ($('#formComprador').ehValido() === false)
                return;

            let obj = $('#formComprador').obterJson();
            obj.Produto = $('#hfProdutoComprador').getSelect2Data();
            obj.ID = id;

            function fnAny(result) {
                if (!result || result.ID !== 0 && result.ID === obj.ID) return false;

                let listMsg = new Array();
                listMsg.push({ Mensagem: 'O produto: ' + obj.Produto.Nome + ' já foi adicionado', IdControle: '' });
                HelperJS.showListaAlert(listMsg);
                return true;
            }

            if (HelperJS.any('Produto.ID', json, obj.Produto.ID, fnAny)) {
                return;
            }

            CompradorProduto.set(obj);
            $('#formComprador').limpar();
            CompradorProduto.listar();
            id = 0;
        },
    };
}();

var Contatos = function () {
    var json = new Array();
    var id = 0;
    return {
        init: function () {
            Contatos.eventos();
        },

        get: function () {
            return json;
        },

        set: function (obj) {
            if (!obj.ID) {
                let max = json.length > 0 ? HelperJS.getMax(json, 'ID', true) : 0;
                obj.ID = max - 1;
            }
            obj.ParceiroID = PN.GetID();
            json = $.grep(json, function (e) { return e.ID != obj.ID });
            json.push(obj);
        },

        limparJson: function () {
            json = new Array();
            Contatos.listar();
        },

        eventos: function () {
            $('#btnAddContato').click(function () { Contatos.inserir(); });
        },

        listar: function (data) {
            var fnColunas = function () {
                var colunas = new Array();
                colunas.push({ mData: "Nome", sClass: "text-left", sType: "string" });
                colunas.push({ mData: "Email", sClass: "text-left", sType: "string" });
                colunas.push({ mData: "Telefone", sClass: "text-left", sType: "string", mRender: function (source, type, full) { return HelperJS.formatarTelefone(source); } });
                colunas.push({
                    mData: "Nome", sClass: "text-left", sType: "numeric", mRender: function (source, type, full) {
                        var editar = '&nbsp<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Editar" onclick="Contatos.editar(' + full.ID + ')"><i class="icon-edit"></i></a>';
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="Contatos.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        return editar + excluir;
                    }
                });

                return colunas;
            }

            if (data)
                json = data;
            $('#gridContatos').bindDataTable({
                columns: fnColunas(),
                sorter: [[0, 'asc']],
                data: json,
            });

        },

        excluir: function (_id) {
            json = $.grep(json, function (e) { return e.ID != _id });
            Contatos.listar();
        },

        editar: function (_id) {
            var data = $.grep(json, function (e) { return e.ID == _id })[0];
            id = data.ID;
            $('#formContatos').popularCampos({ data: data });
        },

        inserir: function () {
            if ($('#formContatos').ehValido() == false)
                return;
            var obj = $('#formContatos').obterJson();
            obj.ID = id;
            Contatos.set(obj);
            $('#formContatos').limpar();
            Contatos.listar();
            id = 0;
        },
    };
}();