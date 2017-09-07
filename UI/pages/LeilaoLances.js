var LeilaoLances = function () {

    var _id = 0;
    var _lstCompradores = [];
    var _lstFornecedores = [];
    var _lstLances = [];
    var _item = {};
    var _tipo = 0;
    var _abertoLance = false;

    return {

        init: function () {
            LeilaoLances.configurarControles();
            LeilaoLances.inicializarGrid();
            LeilaoLances.eventos();
            LeilaoLances.carregar();
        },

        eventos: function () {
            $('#btnSalvar').click(function () { LeilaoLances.salvar(); });
            $('#btnLance').click(function () { LeilaoLances.lance(); });
        },

        configurarControles: function () {
            LeilaoLances.lstCompradores([]);
            LeilaoLances.lstFornecedores([]);
            $('#hfFornecedor').parceiros({ multiplo: false, tipo: 2 });

            $("#txtValorLance1").attr("data-original-title", "....... 1111");
            $("#txtValorLance2").attr("data-original-title", "2222");
        },

        carregar: function () {
            id = HelperJS.getQueryString("id");

            var fnSuccess = function (leilao) {
                $('#formDados').popularCampos({ data: leilao });

                var dataFormacao = new Date(HelperJS.RecuperarData(leilao.DataFinalFormacao));
                dataFormacao.setDate(dataFormacao.getDate() + 1);
                dataFormacao.setTime(dataFormacao.getTime() - 1);

                // SE A DATA DE FORMAÇÃO FICOU NO PASSADO
                if (dataFormacao < new Date()) {
                    $("#chkParticipando").prop("disabled", "disabled");
                    $("#txtQuantidadeDesejada").prop("disabled", "disabled");
                    $("#txtQuantidadeMin").prop("disabled", "disabled");
                    $("#txtQuantidadeMax").prop("disabled", "disabled");
                    $("#btnSalvar").hide();

                    var dataAbertura = new Date(HelperJS.RecuperarData(leilao.DataAbertura));
                    dataAbertura.setDate(dataAbertura.getDate() + 1);
                    dataAbertura.setTime(dataAbertura.getTime() - 1);

                    // SE JÁ CHEGOU A DATA DE ABERTURA 
                    if (dataAbertura < new Date())
                        LeilaoLances.abertoLance(true);
                }

                LeilaoLances.listarCompradores(leilao.Compradores);
                LeilaoLances.listarFornecedores(leilao.Fornecedores);
                LeilaoLances.listarLances(leilao.Rodadas[0].lstFornecedoresRodada);

                LeilaoLances.id(leilao.ID);
            }

            HelperJS.callApi(
                {
                    url: "leilao/lances/" + id,
                    type: "GET",
                    data: null,
                    functionOnSucess: fnSuccess,
                    functionOnError: HelperJS.showError
                });
        },

        inicializarGrid: function () {
            LeilaoLances.listarCompradores();
            LeilaoLances.listarFornecedores();
        },

        listarCompradores: function (data) {

            var fnColunasComprador = function () {
                var colunas = new Array();
                colunas.push({ mData: "ID", mRender: function (source, type, full) { return full.ParceiroNegocio.ID + ' - ' + full.ParceiroNegocio.NomeFantasia } });
                colunas.push({ mData: "Participando", mRender: function (source, type, full) { return source ? "Sim" : "Não" } });
                colunas.push({ mData: "QtdDesejada", sClass: "text-left", sType: "decimal" });
                colunas.push({
                    mData: "ID", mRender: function (source, type, full) {
                        var editar = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Editar" onclick="LeilaoLances.editar(' + full.ParceiroNegocioID + ', 1)"><i class="icon-edit"></i></a>';
                        return editar;
                    }
                });

                return colunas;
            }

            if (data && data.length > 0) {
                LeilaoLances.lstCompradores(data);
                $("#formComprador").show();
            }

            $('#gridComprador').bindDataTable({
                columns: fnColunasComprador(),
                sorter: [[0, 'asc']],
                data: LeilaoLances.lstCompradores(),
            });
        },

        listarFornecedores: function (data) {

            var fnColunasFornecedor = function () {
                var colunas = new Array();
                colunas.push({ mData: "ID", mRender: function (source, type, full) { return full.ParceiroNegocio.ID + ' - ' + full.ParceiroNegocio.NomeFantasia } });
                colunas.push({ mData: "Participando", mRender: function (source, type, full) { return source ? "Sim" : "Não" } });
                colunas.push({ mData: "QtdMinima", sClass: "text-left", sType: "decimal" });
                colunas.push({ mData: "QtdMaxima", sClass: "text-left", sType: "decimal" });
                colunas.push({
                    mData: "ID", mRender: function (source, type, full) {
                        var editar = '<a class="icons-dataTable tooltips" data-toggle="tooltip" data-original-title="Editar" onclick="LeilaoLances.editar(' + full.ParceiroNegocioID + ', 2)"><i class="icon-edit"></i></a>';
                        return editar;
                    }
                });

                return colunas;
            }

            if (data && data.length > 0) {
                LeilaoLances.lstFornecedores(data);
                $("#formFornecedor").show();
            }
            
            $('#gridFornecedor').bindDataTable({
                columns: fnColunasFornecedor(),
                sorter: [[0, 'asc']],
                data: LeilaoLances.lstFornecedores(),
            });
        },

        listarLances: function (data) {

            var fnColunasLances = function () {

                var colunas = new Array();
                colunas.push({ mData: "ID", mRender: function (source, type, full) { return full.LeilaoFornecedor.ParceiroNegocio.ID + ' - ' + full.LeilaoFornecedor.ParceiroNegocio.NomeFantasia } });
                colunas.push({ mData: "ID", mRender: function (source, type, full) { return full.LeilaoRodada.Numero; } });
                colunas.push({ mData: "DataLance", mRender: function (source, type, full) { return source; } });
                colunas.push({ mData: "ValorPrimeiraMargem", sClass: "text-left", sType: "decimal" });
                colunas.push({ mData: "ValorSegundaMargem", sClass: "text-left", sType: "decimal" });
                
                return colunas;
            }

            if (data && data.length > 0) {
                LeilaoLances.lstLances(data);
                $("#formLances").show();
            }


            $('#gridLances').bindDataTable({
                columns: fnColunasLances(),
                sorter: [[0, 'asc']],
                data: LeilaoLances.lstLances(),
            });
        },

        id: function (_id) {
            if (_id !== undefined)
                LeilaoLances._id = _id;
            else
                return LeilaoLances._id;
        },

        tipo: function (_tipo) {
            if (_tipo !== undefined)
                LeilaoLances._tipo = _tipo;
            else
                return LeilaoLances._tipo;
        },

        item: function (_item) {
            if (_item !== undefined)
                LeilaoLances._item = _item;
            else
                return LeilaoLances._item;
        },

        lstCompradores: function (_lstCompradores) {
            if (_lstCompradores !== undefined)
                LeilaoLances._lstCompradores = _lstCompradores;
            else
                return LeilaoLances._lstCompradores;
        },

        lstFornecedores: function (_lstFornecedores) {
            if (_lstFornecedores !== undefined)
                LeilaoLances._lstFornecedores = _lstFornecedores;
            else
                return LeilaoLances._lstFornecedores;
        },

        lstLances: function (_lstLances) {
            if (_lstLances !== undefined)
                LeilaoLances._lstLances = _lstLances;
            else
                return LeilaoLances._lstLances;
        },

        abertoLance: function (_abertoLance) {
            if (_abertoLance !== undefined)
                LeilaoLances._abertoLance = _abertoLance;
            else
                return LeilaoLances._abertoLance;
        },

        editar: function (_id, _tipo) {

            LeilaoLances.id(_id);
            LeilaoLances.tipo(_tipo);
            LeilaoLances.item(LeilaoLances.findItem(_id, _tipo));

            $('#formModal').popularCampos({ data: LeilaoLances.item() });

            if (_tipo == 1) {
                $("#lblQtdEdicao").html("Qtd. Desejada");

                $("#txtQuantidadeDesejada").show();

                $("#btnLance").hide();
                $("#txtQuantidadeMin").hide();
                $("#lblQtdMax").hide();
                $("#txtQuantidadeMax").hide();
                $("#lance_valor_container").hide();
            }
            else {
                $("#lblQtdEdicao").html("Qtd. Minima");

                $("#txtQuantidadeMin").show();
                $("#lblQtdMax").show();
                $("#txtQuantidadeMax").show();
                $("#btnLance").show();
                $("#lance_valor_container").show();

                $("#txtQuantidadeDesejada").hide();
            }

            $("#modal").modal();
        },

        findItem: function (_id, _tipo) {

            if (_tipo == 1)
                return $.grep(LeilaoLances.lstCompradores(), function (e) { return e.ParceiroNegocioID == _id })[0];
            else
                return $.grep(LeilaoLances.lstFornecedores(), function (e) { return e.ParceiroNegocioID == _id })[0];
        },

        salvar: function () {
            var urlRecurso = "";

            var obj = LeilaoLances.item();
            obj.ParceiroNegocio = null;
            obj.Participando = $("#chkParticipando").prop("checked");

            if (LeilaoLances.tipo() == 1) {
                urlRecurso = "leilao/salvarComprador/";
                obj.QtdDesejada = parseFloat($("#txtQuantidadeDesejada").val());
            }
            else {
                urlRecurso = "leilao/salvarFornecedor/";
                obj.QtdMinima = parseFloat($("#txtQuantidadeMin").val());
                obj.QtdMaxima = parseFloat($("#txtQuantidadeMax").val());
            }

            HelperJS.callApi(
                {
                    url: urlRecurso,
                    type: "POST",
                    data: obj,
                    functionOnSucess: LeilaoLances.salvo,
                    functionOnError: HelperJS.showError
                });
        },

        salvo: function () {
            HelperJS.showSuccess("Salvo com sucesso!");
            LeilaoLances.carregar();
            $("#modal").modal('hide');
        },

        lance: function () {

            var obj = LeilaoLances.item();
            obj.ParceiroNegocio = null;
            obj.QtdMinima = parseFloat($("#txtQuantidadeMin").val());
            obj.QtdMaxima = parseFloat($("#txtQuantidadeMax").val());

            console.log("lance", obj);

            HelperJS.callApi(
                {
                    url: "leilao/efetuarlance/",
                    type: "POST",
                    data: obj,
                    functionOnSucess: LeilaoLances.lance_efetuado,
                    functionOnError: HelperJS.showError
                });
        },

        lance_efetuado: function ()
        {
            HelperJS.showSuccess("Salvo com sucesso!");
            LeilaoLances.carregar();
            $("#modal").modal('hide');
        },


    }

}();

