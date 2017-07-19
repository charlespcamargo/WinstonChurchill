var Leilao = function () {

    var id = 0;

    return {
        init: function () {
            Leilao.carregar();
            Leilao.eventos();
        },

        eventos: function () {

        },

        carregar: function () {
            $('#hfProduto').produtos();
            Comprador.init();
            Fornecedor.init();

            this.id = HelperJS.getQueryString("id");

            if (this.id)
                Leilao.editar();
        },

        editar: function () {
            var fnSuccess = function (data) {
                $('#formDados').popularCampos({ data: data });
                $('#hfProduto').select2("data", data.Produto);
                id = data.ID;
            }

            HelperJS.callApi(
            {
                url: "leilao/" + this.id,
                type: "GET",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },

        GetID: function () {
            return id;
        },

    }

}();


var Comprador = function () {
    var id = 0;
    var json = new Array();

    return {
        init: function () {
            Comprador.carregar();
            Comprador.inicializarGrid();
            Comprador.eventos();
        },
        
        get: function () {
            return json;
        },

        set: function (obj) {
            if (!obj.ID) {
                let max = json.length > 0 ? HelperJS.getMax(json, 'ID', true) : 0;
                obj.ID = max - 1;
            }
            obj.LeilaoID = Leilao.GetID();
            json = $.grep(json, function (e) { return e.ID != obj.ID });
            json.push(obj);
        },

        eventos:function()
        {
            $('#btnAddComprador').click(function () { Comprador.inserir(); });
        },            

        inserir: function () {

            if ($('#formComprador').ehValido() === false)
                return;

            let obj = $('#formComprador').obterJson();
            obj.ParceiroNegocioID = $('#hfComprador').getSelect2Data();
            obj.Participando = false;
            obj.ID = id;

            function fnAny(result) {
                if (!result || result.ID !== 0 && result.ID === obj.ID) return false;

                let listMsg = new Array();
                listMsg.push({ Mensagem: 'O comprador: ' + obj.Comprador.Nome + ' já foi adicionado', IdControle: '' });
                HelperJS.showListaAlert(listMsg);
                return true;
            }

            Comprador.set(obj);
            $('#formComprador').limpar();
            Comprador.listar();
            id = 0;
        },

        carregar: function () {
            $('#hfComprador').parceiros({ multiplo: false, tipo: 1 });
        },

        inicializarGrid: function () {
            Comprador.listar();
        },

        listar: function (data) {

            var fnColunas = function () {
                var colunas = new Array();
                colunas.push({ mData: "ID", mRender: function (source, type, full) { return full.ID + ' - ' + full.Nome } });
                colunas.push({ mData: "Participando", mRender: function (source, type, full) { return source ? "Sim" : "Não" } });
                colunas.push({ mData: "QtdDesejada", sClass: "text-left", sType: "decimal" });
                colunas.push({
                    mData: "ID", mRender: function (source, type, full) {
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="Comprador.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        return excluir;
                    }
                });

                return colunas;
            }

            if (data)
                json = data;

            $('#gridComprador').bindDataTable({
                columns: fnColunas(),
                sorter: [[0, 'asc']],
                data: json,
            });
        }

    }

}();


var Fornecedor = function () {
    var id = 0;
    var json = new Array();

    return {

        init: function () {
            Fornecedor.carregar();
            Fornecedor.inicializarGrid();
            Fornecedor.eventos();
        },

        get: function () {
            return json;
        },

        set: function (obj) {
            if (!obj.ID) {
                let max = json.length > 0 ? HelperJS.getMax(json, 'ID', true) : 0;
                obj.ID = max - 1;
            }
            obj.LeilaoID = Leilao.GetID();
            json = $.grep(json, function (e) { return e.ID != obj.ID });
            json.push(obj);
        },
        
        eventos: function () {
            $('#btnAddFornecedor').click(function () { Fornecedor.inserir(); });
        },

        inserir: function () {

            if ($('#formFornecedor').ehValido() === false)
                return;

            let obj = $('#formFornecedor').obterJson();
            obj.ParceiroNegocioID = $('#hfFornecedor').getSelect2Data();
            obj.Participando = false;
            obj.ID = id;

            function fnAny(result) {
                if (!result || result.ID !== 0 && result.ID === obj.ID) return false;

                let listMsg = new Array();
                listMsg.push({ Mensagem: 'O fornecedor: ' + obj.Fornecedor.Nome + ' já foi adicionado', IdControle: '' });
                HelperJS.showListaAlert(listMsg);
                return true;
            }
            
            Fornecedor.set(obj);
            $('#formFornecedor').limpar();
            Fornecedor.listar();
            id = 0;
        },

        carregar: function () {
            $('#hfFornecedor').parceiros({ multiplo: false, tipo: 2 });
        },

        inicializarGrid: function () {
            Fornecedor.listar();
        },

        listar: function (data) {
            
            var fnColunas = function () {
                var colunas = new Array();
                colunas.push({ mData: "ID", mRender: function (source, type, full) { return full.ID + ' - ' + full.Nome } });
                colunas.push({ mData: "Participando", mRender: function (source, type, full) { return source ? "Sim" : "Não" } });
                colunas.push({ mData: "QtdMinima", sClass: "text-left", sType: "decimal" });
                colunas.push({ mData: "QtdMaxima", sClass: "text-left", sType: "decimal" });
                colunas.push({
                    mData: "ID", mRender: function (source, type, full) {
                        var excluir = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Excluir" onclick="Fornecedor.excluir(' + full.ID + ')"><i class="icon-remove"></i></a>';
                        return excluir;
                    }
                });

                return colunas;
            }

            if (data)
                json = data;

            $('#gridFornecedor').bindDataTable({
                columns: fnColunas(),
                sorter: [[0, 'asc']],
                data: json,
            });
        }

    }

}();