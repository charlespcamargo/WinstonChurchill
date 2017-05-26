﻿var APIs = {
    API: { IDHiddenField: "#hfURLAPI", nome: "API", },
};

var HelperJS = function () {
    var API;
    var Ano;

    return {

        init: function () {
            if (jQuery().tooltip) {
                //Ativa os tooltips
                $('body').tooltip({
                    selector: '[data-toggle=tooltip]'
                });
            }

            HelperJS.ConfiguraEventosGlobais();
        },



        getBaseURL: function (api) {
            var apiUrl = "";

            if (api != null && api != undefined) {
                apiUrl = $(api.IDHiddenField).val();
            }
            else {
                apiUrl = $(APIs.API.IDHiddenField).val();
            }

            return apiUrl != undefined ? apiUrl : "";
        },

        ConfiguraEventosGlobais: function () {

            $(document).ajaxSend(function (event, jqxhr, settings) {
                App.blockUI($(".page-content"));
            });

            $(document).ajaxStart(function () {
                App.blockUI($(".page-content"));
            });

            $(document).ajaxComplete(function (event, request, settings) {
                App.unblockUI($(".page-content"));
            });

            $(document).ajaxError(function (event, request, settings) {
                App.unblockUI($(".page-content"));
            });
        },

        callApi: function (_oSettings) {
            if (App.isIE8() || App.isIE9()) {
                HelperJS.callApiIE(_oSettings.url, _oSettings.type, _oSettings.data, _oSettings.functionOnSucess, _oSettings.functionOnError);
            }
            else {
                HelperJS.callApiGoodBrowser(_oSettings.url, _oSettings.type, _oSettings.data, _oSettings.functionOnSucess, _oSettings.functionOnError);
            }
        },
        simpleRequest: function (url, method, data, functionOnSucess, functionOnError) {

            $.ajax({
                type: method,
                url: url,
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: functionOnSucess,
                failure: functionOnError
            });
        },

        callApiIE: function (api, url, type, dataSend, functionOnSucess, functionOnError) {

            var urlProxy = "/proxy.ashx?tipo=" + type + "&api=" + HelperJS.getBaseURL(api) + "&url=" + url + "&";

            $.ajax({
                global: true,
                type: type,
                url: urlProxy,
                cache: false,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(dataSend),
                success: function (data) {
                    functionOnSucess(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    functionOnError(jqXHR, textStatus, errorThrown);
                }
            });

        },

        callApiGoodBrowser: function (url, type, dataSend, functionOnSucess, functionOnError) {

            // fixbug
            if (dataSend != undefined && dataSend != null && ($.isArray(dataSend) || typeof dataSend != 'object')) {
                dataSend = { '': dataSend };
            }

            $.ajax({
                global: true,
                type: type,
                url: HelperJS.getURLApi(url),
                cache: false,
                data: dataSend,
                success: function (data) {
                    functionOnSucess(data);
                    //App.unblockUI(".content");
                },
                error: function (jqXHR, textStatus, errorThrown) {

                    functionOnError(jqXHR, textStatus, errorThrown);
                    //App.unblockUI(".content");
                }
            });

        },

        simpleRequest: function (url, method, dataSend, functionOnSucess, functionOnError, dType, cType) {

            dType = typeof dType !== 'undefined' ? a : "json";
            cType = typeof cType !== 'undefined' ? cType : "application/json; charset=utf-8";

            $.ajax({
                global: false,
                dataType: dType,
                contentType: cType,
                type: method,
                url: url,
                cache: false,
                data: dataSend,
                success: functionOnSucess,
                error: functionOnError
            });
        },

        getURLApi: function (url) {

            var baseURL = HelperJS.getBaseURL(API);

            if (App.isIE8() || App.isIE9()) {
                var urlProxy = "/proxy.ashx?tipo=GET&api=" + baseURL + "&url=" + url;
                return urlProxy;
            }
            else {
                var url = baseURL + url;
                return url;
            }
        },


        temValor: function (prop) {
            if ($.isArray(prop))
                return (prop != null && prop.length > 0);
            else
                return (prop != null && prop != undefined && prop != '' && prop != 0);
        },

        //Usado para informar em tela algum erro retornado da api
        showError: function (jqXHR) {

            //$.gritter.removeAll();


            if (jqXHR != null) {
                if (jqXHR.status == 405) {//Erro de validação
                    $('*[data-json]').each(function () { $(this).removeClass("required"); });
                    var titulo = "Atenção";
                    var texto = "";
                    for (var i = 0; i < jqXHR.responseJSON.length; i++) {
                        texto += jqXHR.responseJSON[i].MensagemValidacao + "</br>";

                        HelperJS.pintaValidacoes(jqXHR.responseJSON[i].IdControle);
                    }
                    $.gritter.add({ title: titulo, text: texto, sticky: false, class_name: "msg_attention", time: 10000, before_open: HelperJS.gritter_before_open });
                }
                else if (jqXHR.status == 409) {//erro não tratado
                    var mensagem = jqXHR.responseJSON != undefined && jqXHR.responseJSON.Message != undefined ? jqXHR.responseJSON.Message : "Erro desconhecido, verifique sua conexão!";
                    var titulo = "Erro";
                    $.gritter.add({ title: titulo, text: mensagem, sticky: true, class_name: "msg_Error", before_open: HelperJS.gritter_before_open });
                }
                else if (jqXHR.status == 400) {//erro de método não encontrado

                    var titulo = "Atenção";
                    $.gritter.add({ title: titulo, text: jqXHR.responseJSON.Message, sticky: true, class_name: "msg_attention", before_open: HelperJS.gritter_before_open });
                }
                else if (jqXHR.status == 401) {//erro de acesso não permitido

                    var titulo = "Acesso Negado";
                    $.gritter.add(
                        {
                            title: titulo,
                            text: "Você não possui acesso ao conteúdo. <br/> Verifique se a opção selecionada está correta ou avalie a necessidade junto a seu gestor e solicite o acesso a esta funcionalidade clicando em [<a href='/pages/perfilAcesso/SolicitarAcessoPerfil.aspx'>Solicitar Acesso</a>].",
                            sticky: true,
                            class_name: "msg_attention",
                            before_open: HelperJS.gritter_before_open
                        });
                }
                else if (jqXHR.status == 404) {
                    var mensagem = "ERRO DESCONHECIDO [404], VERIFIQUE SUA CONEXÃO!";
                    var titulo = "ERRO";
                    $.gritter.add({ title: titulo, text: mensagem, sticky: true, class_name: "msg_Error", before_open: HelperJS.gritter_before_open });
                }
                else if (jqXHR.status == 302) {
                    var mensagem = "O Sistema perdeu sua autenticação, é necessário realizar o login novamente!";
                    var titulo = "LOGOUT";
                    $.gritter.add({ title: titulo, text: mensagem, sticky: true, class_name: "msg_Error", before_open: HelperJS.gritter_before_open });
                }
                else {
                    if (App.isIE8() || App.isIE9()) {
                        if ((jqXHR.responseJSON == null || jqXHR.responseJSON == undefined) && jqXHR.responseText != '') {
                            if (HelperJS.jsonEhValido(jqXHR.responseText)) {
                                jqXHR.responseJSON = $.parseJSON(jqXHR.responseText);
                            }
                        }

                        var mensagem = jqXHR.responseText != undefined ? (jqXHR.responseText != "" ? jqXHR.responseText : "ERRO DESCONHECIDO [IE8-9?], VERIFIQUE SUA CONEXÃO!") : "ERRO DESCONHECIDO, VERIFIQUE SUA CONEXÃO!";

                        var titulo = "ERRO";
                        $.gritter.add({ title: titulo, text: mensagem, sticky: true, class_name: "msg_Error", before_open: HelperJS.gritter_before_open });
                    }
                    else {
                        var mensagem = "ERRO DESCONHECIDO, VERIFIQUE SUA CONEXÃO!";
                        var titulo = "ERRO";
                        $.gritter.add({ title: titulo, text: mensagem, sticky: true, class_name: "msg_Error", before_open: HelperJS.gritter_before_open });
                    }
                }
            }

            App.unblockUI($('body'));
            App.unblockUI($('.container-fluid'));

        },

        gritter_before_open: function () {
            if ($('.gritter-item-wrapper').length >= 5) {
                $.gritter.removeAll();
                return false;
            }
        },

        showSuccess: function (mensagem) {
            var titulo = "Sucesso";
            $.gritter.add({ title: titulo, text: mensagem, sticky: false, class_name: "msg_success", time: 5000 });
        },

        showAlert: function (mensagem, IdControle) {
            var titulo = "Atenção";
            $.gritter.add({ title: titulo, text: mensagem, sticky: false, class_name: "msg_attention", time: 15000 });

            if (IdControle != undefined && IdControle != null && IdControle != '') {
                HelperJS.pintaValidacoes(IdControle);
            }
        },

        showListaAlert: function (listaMensagem) {
            var titulo = "Atenção";
            var texto = "";
            for (var i = 0; i < listaMensagem.length; i++) {
                texto += listaMensagem[i].Mensagem + "</br>";

                var idControle = listaMensagem[i].IdControle
                var prefix = '';
                if (idControle.indexOf('#') < 0) {
                    prefix = "#";
                }

                var tipo = $(prefix + idControle).getType();
                if (tipo == 'chosen') {
                    $(prefix + idControle).addClass("required").trigger("liszt:updated");
                }
                else {
                    $(prefix + idControle).addClass("required");
                }
            }

            $.gritter.add({ title: titulo, text: texto, sticky: false, class_name: "msg_attention", time: 5000 });
        },

        pintaValidacoes: function (idControle) {
            $('*[data-json]').each(function () {
                if (idControle == $(this).attr("data-json"))
                    $(this).addClass("required");
            });
        },

        iniciarUploadify: function (controleId, ehMultiplo, recurso, onUploadComplete, formato, limiteFila) {


            $(controleId).uploadify({
                'successTimeout': 1200000,
                'queueSizeLimit': limiteFila,
                'buttonText': "Selecione o arquivo",
                'preventCaching': false,
                'sizeLimit': '10000000',
                'auto': false,
                'multi': ehMultiplo,
                'swf': $("#swfUrl").attr("urlAbsoluta") + 'uploadify.swf',
                'uploader': HelperJS.getURLApi(recurso),
                'fileTypeExts': formato,
                'onQueueComplete': onUploadComplete,
                'onUploadComplete': onUploadComplete,
            });
        },

        getURLParameter: function (paramName) {
            var searchString = window.location.search.substring(1),
                i, val, params = searchString.split("&");

            for (i = 0; i < params.length; i++) {
                val = params[i].split("=");
                if (val[0].toLowerCase() == paramName.toLowerCase()) {
                    if (unescape(val[1]) == "")
                        return null;

                    return unescape(val[1]);
                }
            }
            return null;
        },

        ComboAutoComplete: function (hiddenID, valueID, tituloCampo, URLApi, isMultiple, funcaoRender, funcaoSelection, funcaoID, qtdCaracteres, changeEvent, allowClear, ajaxOption) {


            var sid = '#' + hiddenID;

            if (allowClear == null || allowClear == undefined) {
                allowClear = false;
            }


            $(sid).select2({
                placeholder: tituloCampo,
                minimumInputLength: qtdCaracteres,
                allowClear: allowClear,
                multiple: isMultiple,
                ajax: {
                    url: HelperJS.getURLApi(URLApi),
                    dataType: 'json',
                    data: function (term, page) {
                        return {
                            id: term // search term
                        };
                    },
                    params: {

                    },
                    results: function (data) {
                        return { results: data };
                    }
                },
                initSelection: function (element, callback) {

                    // the input tag has a value attribute preloaded that points to a preselected make's id
                    // this function resolves that id attribute to an object that select2 can render
                    // using its formatResult renderer - that way the make text is shown preselected
                    var id = $('#' + valueID).val();

                    if (id != undefined && id !== null && id.length > 0) {
                        $.ajax(HelperJS.getURLApi(URLApi) + "/" + id,
                            {
                                dataType: "json",
                                params: {

                                }

                            }).done(function (data) { callback(data); });
                    }
                },
                formatResult: funcaoRender,
                formatSelection: funcaoSelection,
                id: funcaoID
            }).on("change", function (e) {
                if (changeEvent != null && changeEvent != undefined)
                    changeEvent(e);
            });

            $(document.body).on("change", sid, function (ev) {
                var choice;
                var values = ev.val;
                // This is assuming the value will be an array of strings.
                // Convert to a comma-delimited string to set the value.
                if (values !== null && values.length > 0) {
                    for (var i = 0; i < values.length; i++) {
                        if (typeof choice !== 'undefined') {
                            choice += ",";
                            choice += values[i];
                        }
                        else {
                            choice = values[i];
                        }
                    }
                }

                // Set the value so that MVC will load the form values in the postback.
                $('#' + valueID).val(choice);
            });
        },

        popularSelect2: function (idHidden, arrData) {
            if (idHidden.indexOf("#") == -1)
                $("#" + idHidden).select2("data", arrData);
            else
                $(idHidden).select2("data", arrData);
        },

        limparSelect2: function (idHidden) {
            HelperJS.popularSelect2(idHidden, null);
        },


        //inicio - Método sumarizados usado para exibir os totais no footer do datatable
        bindJsonSum: function (jsonDados, containerHml, dataField) {
            var idPesquisa = HelperJS.getId(containerHml, dataField);

            if (dataField == null || dataField == undefined) {
                dataField = "data-json";
            }

            $(idPesquisa).each(function () {
                var bindDataField = $(this).attr(dataField);
                var tipo = $(this).attr("dataTypeUFSCar");
                var value = 0;

                for (var i = 0; i < jsonDados.length; i++) {
                    try {
                        value = parseFloat(value) + parseFloat(HelperJS.formataDecimal(jsonDados[i][bindDataField]));
                    } catch (e) {
                        value = parseFloat(value) + parseFloat(jsonDados[i][bindDataField]);
                    }
                }

                switch (tipo) {
                    case "dolar":
                        $(this).html("US$ " + HelperJS.formatMoney(value, 2, ".", ","));
                        break;
                    case "real":
                        $(this).html("R$ " + HelperJS.formatMoney(value, 2, ".", ","));
                        break;
                    case "percentage":
                        $(this).html(parseFloat(value).toFixed(2) + " %");
                        break;
                    case "decimal":
                        $(this).html(HelperJS.formatMoney(value, 2, ".", ","));
                        break;
                    default:
                        $(this).html(value);
                        break;
                }
            });
        },

        //fim - Método sumarizados usado para exibir os totais no footer do datatable

        mudarAtributoReadOnly: function (containerHml, dataField, ehReadOnly) {
            var idPesquisa = HelperJS.getId(containerHml, dataField);

            $(idPesquisa).each(function () {
                $(this).attr('readonly', ehReadOnly);
            });
        },

        getId: function (containerHml, dataField) {
            var idPesquisa = "";
            var idDataField = "data-json"

            idPesquisa = "*[" + idDataField + "]";

            if (dataField != null && dataField != undefined) {
                idDataField = dataField;
            }

            if (containerHml != null && containerHml != undefined) {

                idPesquisa = "#" + containerHml + " *[" + idDataField + "]";
            }

            return idPesquisa;
        },

        formataDecimal: function (valor) {

            while (valor.toString().indexOf(".") > 0) {
                valor = valor.toString().replace(".", "");
            }

            while (valor.toString().indexOf(",") > 0) {
                valor = valor.toString().replace(",", ".");
            }
            return valor;
        },

        preparaDolar: function (valor) {

            while (valor.indexOf(",") > 0) {
                valor = valor.replace(",", "");
            }
            return valor;
        },

        formatMoney: function (valor, decPlaces, thouSeparator, decSeparator) {
            var n = valor,
                decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 2 : decPlaces,
                decSeparator = decSeparator == undefined ? "." : decSeparator,
                thouSeparator = thouSeparator == undefined ? "," : thouSeparator,
                sign = n < 0 ? "-" : "",
                i = parseInt(n = Math.abs(+n || 0).toFixed(decPlaces)) + "",
                j = (j = i.length) > 3 ? j % 3 : 0;
            return sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - i).toFixed(decPlaces).slice(2) : "");
        },

        formatarTextoGCD: function (data) {
            if (data != null && data != '' && data != undefined) {
                var textoFormatado = data.toString().substring(0, 1);
                textoFormatado += "." + data.toString().substring(1, 2);
                textoFormatado += "." + data.toString().substring(4, 2);
                textoFormatado += "." + data.toString().substring(4);
                return textoFormatado;
            }
            else {
                return "";
            }
        },

        //Formata a data dd/mm/yyyy em formato de dada Json
        RecuperarData: function (date) {
            var DataJson;
            if (date == null) {
                DataJson = new Date();
            } else {
                DataJson = new Date(Date.parse(date));
            }
            return HelperJS.RecuperarDataJson(DataJson);
        },

        RecuperarDataJson: function (date) {
            var dateString = JSON.stringify(date);
            var dateJson = JSON.parse(dateString);
            return dateJson;
        },

        padLeft: function (str, max) {
            str = str.toString();
            return str.length < max ? HelperJS.padLeft("0" + str, max) : str;
        },

        gerarCookie: function (strCookie, strValor, lngDias) {
            $.cookie(strCookie, strValor, {
                expires: lngDias
            });
        },

        apagarCookie: function (strCookie) {
            $.cookie(strCookie, null);
        },


        obterDatasCalendarioPorAnoInformado: function (ano) {

            var intervalo = "1-1-" + ano + ",31-12-" + ano;
            var datasDisponiveis = $.makeArray(intervalo.split(","));
            var date = new Date();
            var dmy = date.getDate() + "-" + (date.getMonth() + 1) + "-" + ano;
            if ($.inArray(dmy, datasDisponiveis) != -1) {
                return [true, "", "Available"];
            } else {
                return [false, "", "unAvailable"];
            }
        },

        jsonEhValido: function (json) {
            try {
                JSON.parse(json);
            } catch (e) {
                return false;
            }
            return true;
        },

        inserirNaPosicao: function (texto, posicao, inserir) {
            return [texto.slice(0, posicao), inserir, texto.slice(posicao)].join('');
        },

        formatarTelefone: function (telefone) {
            if (telefone == null) {
                return "";
            }

            telefone = telefone.replace("-", "").replace("(", "").replace(")", "").replace(/\s/g, '');

            // [11] => 15999999999 => (15)99999-9999
            if (telefone.length == 11) {
                telefone = HelperJS.inserirNaPosicao(telefone, 7, "-");
            }
            // [10] => 1533333333 => (15)3333-3333
            else if (telefone.length == 10) {
                telefone = HelperJS.inserirNaPosicao(telefone, 6, "-");
            }
            // [9] => 999999999 => 99999-9999
            else if (telefone.length == 9) {
                telefone = HelperJS.inserirNaPosicao(telefone, 5, "-");
            }
            // [8] => 33333333 => 3333-3333
            else if (telefone.length == 8) {
                telefone = HelperJS.inserirNaPosicao(telefone, 4, "-");
            }

            if (telefone.length > 10) {
                telefone = HelperJS.inserirNaPosicao(telefone, 2, ")");
                telefone = HelperJS.inserirNaPosicao(telefone, 0, "(");
            }
            else {
                telefone = HelperJS.inserirNaPosicao(telefone, 0, "(00)");
            }

            return telefone;
        },

        formatarCpfOuCnpj: function (documento) {
            documento = documento.replace("-", "").replace("/", "").replace(".", "").replace(/\s/g, '');

            // [11] => 99999999999 => 999.999.999-99
            if (documento.length == 11) {
                documento = HelperJS.inserirNaPosicao(documento, 3, ".");
                documento = HelperJS.inserirNaPosicao(documento, 7, ".");
                documento = HelperJS.inserirNaPosicao(documento, 11, "-");
            }
            else if (documento.length == 14) {
                documento = HelperJS.inserirNaPosicao(documento, 2, ".");
                documento = HelperJS.inserirNaPosicao(documento, 6, ".");
                documento = HelperJS.inserirNaPosicao(documento, 10, "/");
                documento = HelperJS.inserirNaPosicao(documento, 15, "-");
            }
            return documento;
        },

        getQueryString: function (key) {
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                if (pair[0] == key) {
                    return pair[1];
                }
            }
        },

        dataBindComboChosen: function (api, url, idControle, texto, valor, onChange, allowClear, chznInputWidth) {

            $(idControle).empty().trigger("liszt:updated");

            $(idControle).off('change');  //preciso destruir o evento atual para evitar chamar duas vezes a api no onchange.

            $(idControle).on("change", function (e) {
                if (onChange != null && onChange != undefined)
                    onChange(e);
            });

            HelperJS.callApi(api, url, "GET", null, function (dataSource) {
                if (dataSource != null && dataSource.length > 0) {
                    $(idControle).append(new Option());
                    $.each(dataSource, function (i, obj) {
                        $(idControle).append(new Option($(obj).prop(texto), $(obj).prop(valor)));
                    });
                }

                $(idControle).trigger("liszt:updated");
                if (allowClear != null && allowClear == true) {
                    $(idControle).chosen({ allow_single_deselect: true });
                }
                else {
                    $(idControle).chosen({ allow_single_deselect: false });
                }

                if (chznInputWidth != undefined) {
                    $(idControle + '_chzn input').css({ "width": chznInputWidth });
                }
            },
                HelperJS.showError);
        },

        ///pode passar uma lista de controles, quando for utilizar valores identificos, ex: (Sim/Não)
        dataBindComboChosenDefault: function (controles, data, texto, valor, onChange, allowClear, chznInputWidth) {
            if (controles != undefined && controles != null && controles.length > 0) {
                $.each(controles, function (i, controle) {
                    $(controle).empty();
                    $(controle).on("change", function (e) {
                        if (onChange != null && onChange != undefined)
                            onChange(e);
                    });

                    if (data != null && data.length > 0) {
                        $(controle).append(new Option());
                        $.each(data, function (j, obj) {
                            $(controle).append(new Option($(obj).prop(texto), $(obj).prop(valor)));
                        });

                        if (allowClear != null && allowClear == true) {
                            $(controle).chosen({ allow_single_deselect: true });
                        }

                        $(controle).val('').trigger("liszt:updated");

                        if (chznInputWidth != undefined) {
                            $(controle + '_chzn input').css({ "width": chznInputWidth });
                        }
                    }
                });
            }
        },

        validarDocumento: function (campo, msgInvalido) {


            var valor = jQuery.trim($(campo).val());// retira espaços em branco
            var valido = null;

            // DEIXA APENAS OS NÚMEROS
            valor = valor.replace('/', '');
            valor = valor.replace('.', '');
            valor = valor.replace('.', '');
            valor = valor.replace('-', '');


            var ehCPF = valor.length <= 11;

            if (ehCPF) {
                var cpf = valor;

                while (cpf.length < 11)
                    cpf = "0" + cpf;

                var expReg = /^0+$|^1+$|^2+$|^3+$|^4+$|^5+$|^6+$|^7+$|^8+$|^9+$/;
                var a = [];
                var b = new Number;
                var c = 11;

                for (i = 0; i < 11; i++) {
                    a[i] = cpf.charAt(i);
                    if (i < 9) b += (a[i] * --c);
                }

                if ((x = b % 11) < 2) {
                    a[9] = 0
                }
                else {
                    a[9] = 11 - x
                }

                b = 0;
                c = 11;

                for (y = 0; y < 10; y++)
                    b += (a[y] * c--);

                if ((x = b % 11) < 2) {
                    a[10] = 0;
                }
                else {
                    a[10] = 11 - x;
                }

                if ((cpf.charAt(9) != a[9]) || (cpf.charAt(10) != a[10]) || cpf.match(expReg))
                    valido = false;
                else
                    valido = true;
            }
            else {

                var cnpj = valor;

                var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
                digitos_iguais = 1;

                if (cnpj.length < 14 && cnpj.length < 15) {
                    valido = false;
                }

                for (i = 0; i < cnpj.length - 1; i++) {
                    if (cnpj.charAt(i) != cnpj.charAt(i + 1)) {
                        digitos_iguais = 0;
                        break;
                    }
                }

                if (!digitos_iguais) {
                    tamanho = cnpj.length - 2
                    numeros = cnpj.substring(0, tamanho);
                    digitos = cnpj.substring(tamanho);
                    soma = 0;
                    pos = tamanho - 7;

                    for (i = tamanho; i >= 1; i--) {
                        soma += numeros.charAt(tamanho - i) * pos--;

                        if (pos < 2) {
                            pos = 9;
                        }
                    }
                    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

                    if (resultado != digitos.charAt(0)) {
                        valido = false;
                    }

                    tamanho = tamanho + 1;
                    numeros = cnpj.substring(0, tamanho);
                    soma = 0;
                    pos = tamanho - 7;

                    for (i = tamanho; i >= 1; i--) {
                        soma += numeros.charAt(tamanho - i) * pos--;
                        if (pos < 2) {
                            pos = 9;
                        }
                    }

                    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

                    if (resultado != digitos.charAt(1))
                        valido = false;
                    else if (valido == null)
                        valido = true;
                }
                else
                    valido = false;

            }

            if (!valido && msgInvalido != undefined && msgInvalido != null && msgInvalido != '') {
                var lstMsg = [];
                var msg = { Mensagem: msgInvalido, IdControle: campo };
                lstMsg.push(msg);
                HelperJS.showListaAlert(lstMsg);
            }

            return valido;
        },

        compararRangeData: function (data1, data2) {
            data1 = HelperJS.inverterMesDia(data1);
            data2 = HelperJS.inverterMesDia(data2);
            if (new Date(data1).getTime() > new Date(data2).getTime()) {
                HelperJS.showAlert("A data inicial tem que ser menor ou igual a data final.");
                return false;
            }
            else {
                return true;
            }
        },

        //Esse método server para inverter a posição entre mês de dia: Ex: dd/mm para mm/dd
        //Deve-se usar para conversões de datas, por exemplo new Date(data) o formato à ser passado tem que ser: mm/dd para evitar um retorno do tipo NAN
        inverterMesDia: function (data) {
            if (data != "" && data != undefined && data != null) {
                data = data.substring(3, 5) + "/" + data.substring(0, 2) + "/" + data.substring(6);
            }
            return data;
        },

        ObterTipoIndefinido: function (ctrl) {
            if ($(ctrl).is('select')) {
                if ($(ctrl).is('.chzn-done')) {
                    return 'chosen';
                }
                else {
                    return 'select';
                }
            }

            if ($(ctrl).is('checkbox')) {
                return 'checkbox';
            }

            if ($(ctrl).is('h4') || $(ctrl).is('span') || $(ctrl).is('div')) {
                return 'html'
            }

            if ($(ctrl).is('textarea')) {
                return 'textarea';
            }
        },
        toNumber: function (number) {


            return parseFloat(HelperJS.formataDecimal(number));

        },
        fixChoosen: function () {
            var els = jQuery(".chosen-select");
            els.on("chosen:showing_dropdown", function () {
                $(this).parents("div").css("overflow", "visible");
            });
            els.on("chosen:hiding_dropdown", function () {
                var $parent = $(this).parents("div");

                // See if we need to reset the overflow or not.
                var noOtherExpanded = $('.chosen-with-drop', $parent).length == 0;
                if (noOtherExpanded)
                    $parent.css("overflow", "");
            });
        },
        confirmar: function (htmlMsg, onConfirmar, onCancelar) {
            bootbox.confirm(htmlMsg, function (escolha) {
                if (escolha != null) {
                    if (escolha == true) {
                        if (onConfirmar != undefined && onConfirmar != null)
                            onConfirmar();
                    }
                    else {
                        if (onCancelar != undefined && onCancelar != null) {
                            onCancelar();
                        }

                        bootbox.hideAll();
                    }
                }

                return escolha;
            });

        },

        //verificar se o sistema eh offline ou UFSCarweb
        ehLocal: function () {
            var valor = $.cookie('conexaolocal');
            if (valor != undefined && valor != null && valor.toString().toLowerCase() == "true") {
                return true;
            }
            else {
                return false;
            }
        },


        getCol: function (matrix, col) {
            var column = [];
            for (var i = 1; i < matrix.length; i++) {
                column.push(Number(matrix[i].lstColuna[col].Valor.replace(',', '.')));
            }
            return column;
        },

        formatarDecimalBR: function (number) {
            var postComma, preComma, stringReverse, _ref;
            stringReverse = function (str) {
                return str.split('').reverse().join('');
            };
            _ref = number.toFixed(2).split('.'), preComma = _ref[0], postComma = _ref[1];
            preComma = stringReverse(stringReverse(preComma).match(/.{1,3}/g).join('.'));
            return "" + preComma + "," + postComma;
        },

        formatarPercentual: function (valor, exibicao) {

            if (valor == undefined || valor == null || valor == '')
                valor = 0;

            if (exibicao) {
                if (typeof (valor) == "string") {
                    // removo todos os pontos e substituo a virgula pelo ponto, ficando somente um ponto
                    // exemplo: Valor em pt-BR (ponto pra milhar e virgula decimal) "1.234.567,89" fica assim "1234567.89"
                    valor = valor.replace(/\./g, "").replace(",", ".");

                    return HelperJS.formatMoney(valor.toString().replace(".", "").replace(",", "."), 2, ".", ","); // string
                }
                else
                    return HelperJS.formatMoney(valor, 2, ".", ",");
            }
            else
                return Number(HelperJS.formataDecimal(valor)); // number
        },

        //Verificar se o browser tem suporte ou plugin de flash habilitado
        temSuporteFlash: function () {
            var hasFlash = false;
            try {
                hasFlash = Boolean(new ActiveXObject('ShockwaveFlash.ShockwaveFlash'));
            } catch (exception) {
                hasFlash = ('undefined' != typeof navigator.mimeTypes['application/x-shockwave-flash']);
            }
            return hasFlash;
        },

        validParts: /dd?|DD?|mm?|MM?|yy(?:yy)?/g,
        parseFormat: function (format) {
            // IE treats \0 as a string end in inputs (truncating the value),
            // so it's a bad format delimiter, anyway
            var separators = format.replace(this.validParts, '\0').split('\0'),
                parts = format.match(this.validParts);
            if (!separators || !separators.length || !parts || parts.length === 0) {
                throw new Error("Invalid date format.");
            }
            return { separators: separators, parts: parts };
        },
        formatarData: function (valor, exibicao) {
            var format = "dd/mm/yyyy";
            if (exibicao && exibicao != null && exibicao != undefined && typeof exibicao === "string")
                format = exibicao.toLowerCase();

            format = HelperJS.parseFormat(format);

            var date = new Date(valor);
            var val = {
                d: date.getUTCDate(),
                m: date.getUTCMonth() + 1,
                yy: date.getUTCFullYear().toString().substring(2),
                yyyy: date.getUTCFullYear()
            };
            val.dd = (val.d < 10 ? '0' : '') + val.d;
            val.mm = (val.m < 10 ? '0' : '') + val.m;
            var date = [],
                seps = $.extend([], format.separators);
            for (var i = 0, cnt = format.parts.length; i <= cnt; i++) {
                if (seps.length)
                    date.push(seps.shift());
                date.push(val[format.parts[i]]);
            }
            return date.join('');
        },

        //função de upload usando HTML 5
        iniciarUploadifive: function (controleId, ehMultiplo, recurso, onUploadComplete, formato, limiteFila) {

            $(controleId).uploadifive({
                'successTimeout': 1200000,
                'queueSizeLimit': limiteFila,
                'buttonText': "Selecione o arquivo",
                'preventCaching': false,
                'sizeLimit': '10000000',
                'auto': false,
                'multi': ehMultiplo,
                'uploadScript': HelperJS.getURLApi(recurso),
                'fileTypeExts': formato,
                'fileType': true,
                'width': 180,
                'onUploadComplete': onUploadComplete
            });
        },


        // Replace caracteres (Windows1252) replace para caracteres ASCII ou ISO-8859-1
        // Sybase não suporta alguns caracteres da (tabela Windows-1252)
        // CONSULTAR EM: en.wikipedia.org/wiki/Windows-1252#Codepage_layout
        replaceWordChars: function (text) {
            if (text != null && text != undefined) {
                var s = text;
                // smart single quotes and apostrophe
                s = s.replace(/[\u2018\u2019\u201A]/g, "\'");
                // smart double quotes
                s = s.replace(/[\u201C\u201D\u201E]/g, "\"");
                // ellipsis
                s = s.replace(/\u2026/g, "...");
                // dashes
                s = s.replace(/[\u2013\u2014]/g, "-");
                // circumflex
                s = s.replace(/\u02C6/g, "^");
                // open angle bracket
                s = s.replace(/\u2039/g, "<");
                // close angle bracket
                s = s.replace(/\u203A/g, ">");
                // spaces
                s = s.replace(/[\u02DC\u00A0]/g, " ");
                //Bullets
                s = s.replace(/[\u002E\u2022]/g, "-");
                //Replace others
                s = s.replace(/[\u2020\u2021\u02C6\u2030\u20AC]/g, " ");

                return s;
            }
            else
                return "";
        },


        ajustarPosicaoModal: function (controle, milisegundos) {
            var tempo = 0;

            if (milisegundos != undefined || milisegundos != null) {
                tempo = milisegundos;
            }

            setTimeout(function () {
                $(controle).modal('layout');
            },
                tempo);
        },

        newGuid: function () {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                    .toString(16)
                    .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                s4() + '-' + s4() + s4() + s4();
        },

        getMax: function (arr, prop, inverte) {
            var max;
            for (var i = 0; i < arr.length; i++) {
                if (inverte) {
                    if (!max || parseInt(arr[i][prop]) < parseInt(max[prop]))
                        max = arr[i];
                } else {
                    if (!max || parseInt(arr[i][prop]) > parseInt(max[prop]))
                        max = arr[i];
                }
            }
            return max[prop];
        },

        any: function (propriedade, lista, valor, fnCallBack) {
            let resultado = $.grep(lista, function (e) {
                let split = propriedade.split('.');
                if (split.length > 0) {
                    let prop;
                    for (var i = 0; i < split.length; i++) {
                        prop = split[i];
                        e = e[prop];
                    }
                    return e === valor;
                }
                else
                    return e[propriedade] === valor;
            });
            return fnCallBack(resultado[0]);
        }
    };

}();


//Extensions
$.fn.getType = function () {
    var tipo = "";
    if ($(this[0]).attr("class") == "select2-offscreen")
        return "select2";

    if (this[0] != undefined) {
        if (this[0].tagName == "INPUT")
            return this[0].type.toLowerCase()
        else
            tipo = HelperJS.ObterTipoIndefinido(this[0]);
    }

    return tipo;
};
