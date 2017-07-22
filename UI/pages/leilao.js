var Leilao = function () {

    var id = 0;
    var passo = 1;

    return {

        init: function () {
            Leilao.carregar();
            Leilao.eventos();
            Comprador.init();
            Fornecedor.init();
            Leilao.mudarPasso(0);
        },

        eventos: function () {
            $('#btnAnterior').click(function () { passo--; Leilao.mudarPasso(); });
            $('#btnContinuar').click(function () { passo++; Leilao.mudarPasso(); });
            $('#btnSalvar').click(function () { Leilao.salvar(); });

            $(".steps .navbar-inner ul li a").click(function ()
            {
                passo = parseInt($(this).find(".number").text());
                Leilao.mudarPasso();
            });

        },

        mudarPasso: function () {           

            $(".form-horizontal .form-wizard .navbar-inner ul li").removeClass("active");
            $(".tab-content .tab-pane").removeClass("active");

            $($(".form-horizontal .form-wizard .navbar-inner ul li")[passo - 1]).addClass("active");// zero-based
            $($(".tab-content .tab-pane").removeClass("active")[passo - 1]).addClass("active"); // zero-based

            $("#bar .bar").attr("style", "width: " + Math.round(passo / 3 * 100) + "%")
            $(".step-title").text("Passo " + passo + " de 3")

            if (passo == 1)
            {
                $('#btnAnterior').hide();
                $('#btnContinuar').show();
            }
            else if (passo == 2) {
                $('#btnAnterior').show();
                $('#btnContinuar').show();
            }
            else {
                $('#btnAnterior').show();
                $('#btnContinuar').hide();
            }

        },

        carregar: function () {
            $('#hfProduto').produtos();

            id = HelperJS.getQueryString("id");

            if (id)
                Leilao.editar();
        },

        editar: function () {

            var fnSuccess = function (data) {
                $('#formDados').popularCampos({ data: data });
                $('#hfProduto').select2("data", data.Produto);
                id = data.ID;

                Comprador.limparJson();
                Fornecedor.limparJson();
            }

            HelperJS.callApi(
            {
                url: "leilao/" + id,
                type: "GET",
                data: null,
                functionOnSucess: fnSuccess,
                functionOnError: HelperJS.showError
            });
        },

        GetID: function () {
            return id;
        },

        salvar: function ()
        {
            HelperJS.showAlert("Em breve...")
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
                obj.ID = max + 1;
            }

            obj.LeilaoID = Leilao.GetID();
            json = $.grep(json, function (e) { return e.ID != obj.ID });
            json.push(obj);
        },

        eventos: function () {
            $('#btnAddComprador').click(function () { Comprador.inserir(); });
        },

        inserir: function () {

            if ($('#formComprador').ehValido() === false)
                return;

            let obj = $('#formComprador').obterJson();
            obj.ParceiroNegocio = $('#hfComprador').getSelect2Data()[0];
            obj.ParceiroNegocioID = obj.ParceiroNegocio.ID;
            obj.Participando = false;
            obj.ID = id;

            function fnAny(result) {
                if (!result || result.ID !== 0 && result.ID === obj.ID) return false;

                let listMsg = new Array();
                listMsg.push({ Mensagem: 'O comprador[' + obj.ParceiroNegocio.NomeFantasia + '] já foi adicionado!', IdControle: '' });
                HelperJS.showListaAlert(listMsg);
                return true;
            }

            if (HelperJS.any('ParceiroNegocio.ID', json, obj.ParceiroNegocio.ID, fnAny)) {
                return;
            }


            Comprador.set(obj);
            $('#formComprador').limpar();
            Comprador.listar();
            id = 0;
        },

        carregar: function () {
            $('#hfComprador').parceiros({ multiplo: false, tipo: 1 });
        },

        limparJson: function () {
            json = new Array();
            Comprador.listar();
        },

        inicializarGrid: function () {
            Comprador.listar();
        },

        listar: function (data) {

            var fnColunas = function () {
                var colunas = new Array();
                colunas.push({ mData: "ID", mRender: function (source, type, full) { return full.ParceiroNegocio.ID + ' - ' + full.ParceiroNegocio.NomeFantasia } });
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
        },

        excluir: function (_id) {
            json = $.grep(json, function (e) { return e.ID != _id });
            Comprador.listar();
        },

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
                obj.ID = max + 1;
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
            obj.ParceiroNegocio = $('#hfFornecedor').getSelect2Data()[0];
            obj.ParceiroNegocioID = obj.ParceiroNegocio.ID;
            obj.Participando = false;
            obj.ID = id;

            function fnAny(result) {
                if (!result || result.ID !== 0 && result.ID === obj.ID) return false;

                let listMsg = new Array();
                listMsg.push({ Mensagem: 'O fornecedor[' + obj.ParceiroNegocio.NomeFantasia + '] já foi adicionado!', IdControle: '' });
                HelperJS.showListaAlert(listMsg);
                return true;
            }

            if (HelperJS.any('ParceiroNegocio.ID', json, obj.ParceiroNegocio.ID, fnAny)) {
                return;
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
                colunas.push({ mData: "ID", mRender: function (source, type, full) { return full.ParceiroNegocio.ID + ' - ' + full.ParceiroNegocio.NomeFantasia } });
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
        },

        limparJson: function () {
            json = new Array();
            Fornecedor.listar();
        },

        excluir: function (_id) {
            json = $.grep(json, function (e) { return e.ID != _id });
            Fornecedor.listar();
        },

    }

}();