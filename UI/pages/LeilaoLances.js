var LeilaoLances = function () {

    var _id = 0;
    var _lstCompradores = [];
    var _lstFornecedores = [];
    var _item = {};
    var _tipo = 0;

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
        },

        carregar: function () {
            id = HelperJS.getQueryString("id");

            var fnSuccess = function (leilao)
            {
                $('#formDados').popularCampos({ data: leilao });

                var dataFormacao = new Date(HelperJS.RecuperarData(leilao.DataFinalFormacao));
                dataFormacao.setDate(dataFormacao.getDate() + 1);
                dataFormacao.setTime(dataFormacao.getTime() - 1);

                // SE A DATA DE FORMAÇÃO FICOU NO PASSADO
                if (dataFormacao < new Date())
                {
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
                    {
                        $("#btnLance").show();
                    }
                }

                LeilaoLances.listarCompradores(leilao.Compradores);
                LeilaoLances.listarFornecedores(leilao.Fornecedores);

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

            if (data) {
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

            if (data) {
                LeilaoLances.lstFornecedores(data);
                $("#formFornecedor").show();
            }


            $('#gridFornecedor').bindDataTable({
                columns: fnColunasFornecedor(),
                sorter: [[0, 'asc']],
                data: LeilaoLances.lstFornecedores(),
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

        editar: function (_id, _tipo) {

            LeilaoLances.id(_id);
            LeilaoLances.tipo(_tipo);
            LeilaoLances.item(LeilaoLances.findItem(_id, _tipo));

            $('#formModal').popularCampos({ data: LeilaoLances.item() });

            if (_tipo == 1)
            {
                $("#lblQtdEdicao").html("Quantidade Desejada");

                $("#txtQuantidadeDesejada").show();

                $("#txtQuantidadeMin").hide();
                $("#lblQtdMax").hide();
                $("#txtQuantidadeMax").hide();
            }
            else {
                $("#lblQtdEdicao").html("Quantidade Minima");

                $("#txtQuantidadeMin").show();
                $("#lblQtdMax").show();
                $("#txtQuantidadeMax").show();

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

            if (LeilaoLances.tipo() == 1)
            {
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


    }

}();

