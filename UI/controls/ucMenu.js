﻿var Menu = function () {

    var indiceColunaTemFilhos = 0;
    var indiceColunaTexto = 1;
    var indiceColunaLink = 2;
    var indiceColunaIcone = 3;
    var indiceColunaFilhos = 4;
    var indiceAcesso = 5;
    var digitado = '';
    var menuFiltrado = [];

    var acesso = new Array();

    return {

        init: function () {
            Menu.acesso();
        },

        acesso: function () {
            HelperJS.callApi({ url: "usuario/listarGrupos", type: "GET", data: null, functionOnSucess: Menu.gruposObtidos, functionOnError: HelperJS.showError });
        },

        gruposObtidos: function (lst) {


            if ($.grep(lst, function (e) { return e == "S" }).length >= 1)
                acesso = [1, 2, 3, 4];

            else if ($.grep(lst, function (e) { return e == "A" }).length >= 1)
                acesso = [1, 2, 3];

            else if ($.grep(lst, function (e) { return e == "R" }).length >= 1)
                acesso = [1, 2];

            else if ($.grep(lst, function (e) { return e == "F" || e == "C" }).length >= 1)
                acesso = [1];


            Menu.ConfigurarVisualizacao();
            Menu.RecarregarMenu();
        },

        ConfigurarVisualizacao: function () {
            Menu.SelecionarMenu(1);
        },

        ObterDominio: function (url) {
            return (window.location.href.toUpperCase().indexOf(url.toUpperCase()) > -1);
        },

        RecarregarMenu: function (tipo) {

            if (tipo == undefined || tipo == null || tipo == '')
                tipo = Menu.MenuSelecionado();


            Menu.SelecionarMenu(tipo);

            Menu.LerXML();
        },

        LerXML: function () {

            var menu = [];
            var item = [];
            var arquivo = "/controls/menu.xml?v=" + Math.random();


            $.ajax(
                {
                    type: "GET",
                    url: arquivo,
                    dataType: "xml",
                    success: function (xml) {
                        $(xml).find('Menu').children().each(function () {
                            item = Menu.LerSubItens($(this));

                            if (item != null)
                                menu.push(item);
                        });

                        if (digitado != null && digitado != '') {
                            menu = Menu.ProcurarElemento(null, menu);
                        }


                        Menu.CarregarMenuSideBar(menu);
                    }
                });
        },

        LerSubItens: function (item) {
            var opcao = null;
            var opcoes = [];


            if ($(item).children().length > 0) {
                var opcaoEncontrada;

                for (var i = 0; i < $(item).children().length; i++) {
                    opcaoEncontrada = Menu.LerSubItens($(item).children()[i]);
                    opcoes.push(opcaoEncontrada);
                }
            }

            opcao =
                [
                    true,
                    $(item).attr("text"),
                    $(item).attr("url"),
                    $(item).attr("icone"),
                    opcoes,
                    $(item).attr("tipoAcesso")
                ]



            return opcao;

        },



        CarregarMenuSideBar: function (menuBase) {

            var itens = "";

            // remove todos, menos os dois fixos
            //$("ul.page-sidebar-menu li[class!=fixed]").remove();

            /*
            	<li class="tooltips" data-placement="right" data-original-title="Frontend&nbsp;Theme&nbsp;For&nbsp;Brown&nbsp;Admin">
					<a href="http://Brown.com/preview/index.php?theme=Brown_frontend" target="_blank">
					<i class="icon-gift"></i> 
					<span class="title">Frontend Theme</span>
					</a>
				</li>
            */

            for (var i = 0; i < menuBase.length; i++)
            {
                var acessoItem = parseInt(menuBase[i][indiceAcesso]);

                if (acesso.indexOf(acessoItem) == -1)
                    continue;

                itens += "<li>";

                if (menuBase[i][indiceColunaLink] == null || menuBase[i][indiceColunaLink] == undefined || menuBase[i][indiceColunaLink] == '')
                    itens += "  <a href='javascript:;'>";
                else
                    itens += "  <a href='" + menuBase[i][indiceColunaLink] + "' target='_self' >";

                if (menuBase[i][indiceColunaIcone] != null && menuBase[i][indiceColunaIcone] != undefined)
                    itens += "      <i class='" + menuBase[i][indiceColunaIcone] + "'></i>";
                else
                    itens += "      <i class='icon-arrow-right'></i>";


                itens += "      <span class='title'>" + menuBase[i][indiceColunaTexto] + "</span>";


                if (menuBase[i][indiceColunaTemFilhos] == true && menuBase[i][indiceColunaFilhos].length > 0)
                    itens += "      <span class='arrow'></span>";


                itens += "  </a>";

                if (menuBase[i][indiceColunaTemFilhos] == true && menuBase[i][indiceColunaFilhos] != null && menuBase[i][indiceColunaFilhos].length > 0)
                {
                    itens += "<ul  class='sub-menu'>";

                    for (var j = 0; j < menuBase[i][indiceColunaFilhos].length; j++)
                    {
                        var segundoNivel = menuBase[i][indiceColunaFilhos][j];

                        var acessoItem = parseInt(segundoNivel[indiceAcesso]);

                        if (acesso.indexOf(acessoItem) == -1)
                            continue;

                        itens += "  <li>"

                        itens += "      <a href='" + segundoNivel[indiceColunaLink] + "'>";

                        //Mostrar icone no segundo nivel
                        if (segundoNivel[indiceColunaIcone] != null && segundoNivel[indiceColunaIcone] != undefined) {
                            itens += "      <i class='" + segundoNivel[indiceColunaIcone] + "'></i>";
                        }

                        itens += " " + segundoNivel[indiceColunaTexto];

                        if (segundoNivel[indiceColunaTemFilhos] && segundoNivel[indiceColunaFilhos].length > 0) {
                            itens += "          <span class='arrow'></span>";
                            itens += "  </a>";
                            itens += Menu.CarregarSubItens(segundoNivel[indiceColunaFilhos], true);
                        } else {
                            itens += "  </a>";
                        }

                        itens += "  </li>";
                    }

                    itens += "</ul>";
                }

                itens += "</li>";
            }

            $("ul.page-sidebar-menu").append(itens);


            Menu.AbrirMenuPaginaAtual();
        },

        CarregarSubItens: function (item) {
            var subitens = "";

            if ($.isArray(item)) {
                subitens += "<ul class='sub-menu'>";

                for (var i = 0; i < item.length; i++) {
                    subitens += "   <li>";

                    if (item[i][indiceColunaLink] != '') {
                        subitens += "       <a href='" + item[i][indiceColunaLink] + "'>";
                    }
                    else {
                        subitens += "       <a href='javascript:;'>";
                    }

                    //Mostrar icone no segundo nivel
                    if (item[i][indiceColunaIcone] != null && item[i][indiceColunaIcone] != undefined) {
                        subitens += "      <i class='" + item[i][indiceColunaIcone] + "'></i>";
                    }

                    subitens += " " + item[i][indiceColunaTexto];

                    if (item[i][indiceColunaTemFilhos] == true && item[i][indiceColunaFilhos].length > 0) {
                        subitens += "          <span class='arrow'></span>";
                        subitens += "      </a>";
                        subitens += Menu.CarregarSubItens(item[i][indiceColunaFilhos]);
                    }
                    else {
                        subitens += "      </a>";
                    }


                    subitens += "   </li>";
                }

                subitens += "</ul>";
            }


            return subitens;
        },

        SelecionarMenu: function (id) {
            $.cookie('menuSelecionado', id, { path: "/" });

            // LIMPA
            $("ul.nav:first li").removeClass("active");
            $("ul.nav:first li span.selected").remove();

            // PREENCHE
            $("ul.nav:first li#" + id).addClass("active");
            $("ul.nav:first li#" + id).append("<span class='selected'></span>");
            $("#ddlMenuPrincipal").val(id);
        },

        MenuSelecionado: function () {
            if ($.cookie('menuSelecionado') == undefined) {
                Menu.SelecionarMenu(1);
            }

            return $.cookie('menuSelecionado');
        },

        ProcurarElemento: function (elemento, elementos, encontrados) {


            if (encontrados == null || encontrados == undefined)
                encontrados = [];


            // SOMENTE ENTRO AQUI NA PRIMEIRA CHAMADA
            if (elemento == null && elementos != null) {
                for (var i = 0; i < elementos.length; i++)
                    Menu.ProcurarElemento(elementos[i], null, encontrados);

                return encontrados;
            }
            // PARA TODAS AS OUTRAS CHAMADAS
            else {
                // PROCURO NO ITEM
                if (elemento[indiceColunaTexto].toUpperCase().indexOf(digitado.toUpperCase()) >= 0) {
                    encontrados.push([
                        elemento[indiceColunaTemFilhos],
                        elemento[indiceColunaTexto],
                        elemento[indiceColunaLink],
                        elemento[indiceColunaIcone],
                        elemento[indiceColunaFilhos]
                    ]);
                }

                // SE É PAI
                if (elemento[indiceColunaTemFilhos] == true) {
                    // PROCURO NOS FILHOS
                    for (var j = 0; j < elemento[indiceColunaFilhos].length; j++) {
                        Menu.ProcurarElemento(elemento[indiceColunaFilhos][j], null, encontrados);
                    }
                }
            }

            return encontrados;
        },

        Buscar: function (texto) {
            digitado = texto;
            Menu.RecarregarMenu();
        },

        CarregarImagemAvatar: function () {
            //$("#imgAvatar").error(function () {
            //$(this).attr("src", "/assets/img/avatar.png");
            //});
            $("#imgAvatar").attr("src", "/assets/img/avatar.png");
        },

        AbrirMenuPaginaAtual: function () {
            var paginaAtual = window.location.pathname.substring(window.location.pathname.lastIndexOf("/") + 1);

            if (paginaAtual != null && paginaAtual.length > 1) {
                var arrItens = $("ul.page-sidebar-menu li a[href$='/" + paginaAtual + "'");

                if (arrItens != null && arrItens.length > 0) {

                    $.each(arrItens, function (i) {
                        Menu.EncontrarPai($(arrItens[i]));
                    });
                }
            }
        },

        EncontrarPai: function (item) {
            var pai = item.parent();

            if (item.is("li")) {
                item.addClass("open");
            }
            else if (item.is("ul")) {
                item.css("display", "block");
            }
            else if (item.is("a")) {
                item.addClass("active");
            }

            if (pai.is("li") || pai.is("ul")) {
                Menu.EncontrarPai(pai);
            }
        },



    };




}();
