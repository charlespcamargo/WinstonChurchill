<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucTopo.ascx.cs" Inherits="UI.controls.ucTopo" %>
<script>
    $(document).ready(function () {
        $('#btnLogoff').click(function () {
            function sucesso(data) {
                $.removeCookie('Authorization', { path: '/' });
                window.location.href = '/login.aspx';
            }

            HelperJS.callApi({
                url: "logoff",
                type: "GET",
                data: null,
                functionOnSucess: sucesso,
                functionOnError: null
            });
        });

        HelperJS.callApi({
            url: "perfil/carregar",
            type: "GET",
            data: null,
            functionOnSucess: function (data) { $('.username').html(data.Nome); },
            functionOnError: HelperJS.showError
        });
    });
</script>

<div class="header navbar navbar-inverse navbar-fixed-top">
    <!-- BEGIN TOP NAVIGATION BAR -->
    <div class="navbar-inner">
        <div class="container-fluid">
            <!-- BEGIN LOGO -->
            <a class="brand" href="/pages/dashboard.aspx">
                <%--<img src="/assets/img/logo.png" alt="logo" />--%>
                        &nbsp;&nbsp;LOGO
            </a>
            <!-- END LOGO -->
            <!-- BEGIN RESPONSIVE MENU TOGGLER -->
            <a href="javascript:;" class="btn-navbar collapsed" data-toggle="collapse" data-target=".nav-collapse">
                <img src="/assets/img/menu-toggler.png" alt="" />
            </a>
            <!-- END RESPONSIVE MENU TOGGLER -->
            <!-- BEGIN TOP NAVIGATION MENU -->

            <ul class="nav pull-right">
                <!-- BEGIN NOTIFICATION DROPDOWN -->
                <%-- <li class="dropdown" id="header_notification_bar">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                        <i class="icon-warning-sign"></i>
                        <span class="badge">6</span>
                    </a>
                    <ul class="dropdown-menu extended notification">
                        <li>
                            <p>Você tem 14 novas notificações</p>
                        </li>
                        <li>
                            <ul class="dropdown-menu-list scroller" style="height: 250px">
                                <li>
                                    <a href="#">
                                        <span class="label label-success"><i class="icon-plus"></i></span>
                                        Novo usuário registrado. 
										<span class="time">Agora</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="label label-important"><i class="icon-bolt"></i></span>
                                        Servidor #12 sobrecarregado. 
										<span class="time">15 mins</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="label label-warning"><i class="icon-bell"></i></span>
                                        Servidor #2 não respondendo.
										<span class="time">22 mins</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="label label-info"><i class="icon-bullhorn"></i></span>
                                        Error na aplicação.
										<span class="time">40 mins</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="label label-important"><i class="icon-bolt"></i></span>
                                        Banco de Dados sobrecarregado 68%. 
										<span class="time">2 hrs</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="label label-important"><i class="icon-bolt"></i></span>
                                        2 IP de usuário bloqueados.
										<span class="time">5 hrs</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="label label-warning"><i class="icon-bell"></i></span>
                                        Servidor de Armazenamento #4 não respondendo.
										<span class="time">45 mins</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="label label-info"><i class="icon-bullhorn"></i></span>
                                        Erro no sistema.
										<span class="time">55 mins</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="label label-important"><i class="icon-bolt"></i></span>
                                        Banco de Dados sobrecarregado 68%. 
										<span class="time">2 hrs</span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <li class="external">
                            <a href="#">Ver todas as notificações <i class="m-icon-swapright"></i></a>
                        </li>
                    </ul>
                </li>--%>
                <!-- END NOTIFICATION DROPDOWN -->
                <!-- BEGIN INBOX DROPDOWN -->
                <%--<li class="dropdown" id="header_inbox_bar">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                        <i class="icon-envelope"></i>
                        <span class="badge">5</span>
                    </a>
                    <ul class="dropdown-menu extended inbox">
                        <li>
                            <p>Você tem 12 novas mensagens</p>
                        </li>
                        <li>
                            <ul class="dropdown-menu-list scroller" style="height: 250px">
                                <li>
                                    <a href="inbox.html?a=view">
                                        <span class="photo">
                                            <img src="/assets/img/avatar2.jpg" alt="" /></span>
                                        <span class="subject">
                                            <span class="from">Lucy Liu</span>
                                            <span class="time">Agora</span>
                                        </span>
                                        <span class="message">Vivamus sed auctor nibh congue nibh. auctor nibh auctor nibh...
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="inbox.html?a=view">
                                        <span class="photo">
                                            <img src="/assets/img/avatar3.jpg" alt="" /></span>
                                        <span class="subject">
                                            <span class="from">Brad Pitt</span>
                                            <span class="time">16 mins</span>
                                        </span>
                                        <span class="message">Vivamus sed congue nibh auctor nibh congue nibh. auctor nibhauctor nibh...
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="inbox.html?a=view">
                                        <span class="photo">
                                            <img src="/assets/img/charles_camargo_avatar.jpg" alt="" /></span>
                                        <span class="subject">
                                            <span class="from">Charles Camargo</span>
                                            <span class="time">2 hrs</span>
                                        </span>
                                        <span class="message">Vivamus sed nibh auctor nibh congue nibh. auctor nibh
										auctor nibh...
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="inbox.html?a=view">
                                        <span class="photo">
                                            <img src="/assets/img/avatar2.jpg" alt="" /></span>
                                        <span class="subject">
                                            <span class="from">Lucy Liu</span>
                                            <span class="time">40 mins</span>
                                        </span>
                                        <span class="message">Vivamus sed auctor 40% nibh congue nibh...
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="inbox.html?a=view">
                                        <span class="photo">
                                            <img src="/assets/img/avatar3.jpg" alt="" /></span>
                                        <span class="subject">
                                            <span class="from">Brad Pitt</span>
                                            <span class="time">46 mins</span>
                                        </span>
                                        <span class="message">Vivamus sed congue nibh auctor nibh congue nibh. auctor nibh
										auctor nibh...
                                        </span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <li class="external">
                            <a href="inbox.html">See all messages <i class="m-icon-swapright"></i></a>
                        </li>
                    </ul>
                </li>--%>
                <!-- END INBOX DROPDOWN -->
                <!-- BEGIN TODO DROPDOWN -->
                <%--  <li class="dropdown" id="header_task_bar">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                        <i class="icon-tasks"></i>
                        <span class="badge">5</span>
                    </a>
                    <ul class="dropdown-menu extended tasks">
                        <li>
                            <p>Você tem 12 tarefas pendentes</p>
                        </li>
                        <li>
                            <ul class="dropdown-menu-list scroller" style="height: 250px">
                                <li>
                                    <a href="#">
                                        <span class="task">
                                            <span class="desc">New release v1.2</span>
                                            <span class="percent">30%</span>
                                        </span>
                                        <span class="progress progress-success ">
                                            <span style="width: 30%;" class="bar"></span>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="task">
                                            <span class="desc">Application deployment</span>
                                            <span class="percent">65%</span>
                                        </span>
                                        <span class="progress progress-danger progress-striped active">
                                            <span style="width: 65%;" class="bar"></span>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="task">
                                            <span class="desc">Mobile app release</span>
                                            <span class="percent">98%</span>
                                        </span>
                                        <span class="progress progress-success">
                                            <span style="width: 98%;" class="bar"></span>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="task">
                                            <span class="desc">Database migration</span>
                                            <span class="percent">10%</span>
                                        </span>
                                        <span class="progress progress-warning progress-striped">
                                            <span style="width: 10%;" class="bar"></span>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="task">
                                            <span class="desc">Web server upgrade</span>
                                            <span class="percent">58%</span>
                                        </span>
                                        <span class="progress progress-info">
                                            <span style="width: 58%;" class="bar"></span>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="task">
                                            <span class="desc">Mobile development</span>
                                            <span class="percent">85%</span>
                                        </span>
                                        <span class="progress progress-success">
                                            <span style="width: 85%;" class="bar"></span>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="task">
                                            <span class="desc">New UI release</span>
                                            <span class="percent">18%</span>
                                        </span>
                                        <span class="progress progress-important">
                                            <span style="width: 18%;" class="bar"></span>
                                        </span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <li class="external">
                            <a href="#">Ver todas as tarefas <i class="m-icon-swapright"></i></a>
                        </li>
                    </ul>
                </li>--%>
                <!-- END TODO DROPDOWN -->
                <!-- BEGIN LANGUAGE DROPDOWN -->
                <%-- <li class="dropdown language">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                        <img alt="" src="/assets/img/flags/br.png" />
                        <span class="username">BR</span>
                        <i class="icon-angle-down"></i>
                    </a>
                    <ul class="dropdown-menu">
                        <li><a href="#">
                            <img alt="" src="/assets/img/flags/es.png" />
                            Spanish</a></li>
                        <li><a href="#">
                            <img alt="" src="/assets/img/flags/de.png" />
                            German</a></li>
                        <li><a href="#">
                            <img alt="" src="/assets/img/flags/us.png" />
                            United States</a></li>
                        <li><a href="#">
                            <img alt="" src="/assets/img/flags/fr.png" />
                            French</a></li>
                    </ul>
                </li>--%>
                <!-- END LANGUAGE DROPDOWN -->
                <!-- BEGIN USER LOGIN DROPDOWN -->
                <li class="dropdown user">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                        <%--<img alt="" src="/assets/img/charles_camargo_avatar.jpg" />--%>
                        <span class="username"></span>
                        <i class="icon-angle-down"></i>
                    </a>
                    <ul class="dropdown-menu">
                        <li><a href="/pages/Perfil.aspx"><i class="icon-user"></i>Meu Perfil</a></li>
                        <%-- <li><a href="page_calendar.html"><i class="icon-calendar"></i>Meu Calendário</a></li>
                        <li><a href="inbox.html"><i class="icon-envelope"></i>Minha Inbox <span class="badge badge-important">3</span></a></li>
                        <li><a href="#"><i class="icon-tasks"></i>Minhas Tarefas<span class="badge badge-success">8</span></a></li>
                        <li class="divider"></li>--%>
                        <li><a href="javascript:;" id="trigger_fullscreen"><i class="icon-move"></i>Tela Cheia</a></li>
                        <li><a href="#" id="btnLogoff"><i class="icon-key"></i>Sair</a></li>
                    </ul>
                </li>
                <!-- END USER LOGIN DROPDOWN -->
                <!-- END USER LOGIN DROPDOWN -->
            </ul>


            <!-- END TOP NAVIGATION MENU -->
        </div>
    </div>
    <!-- END TOP NAVIGATION BAR -->
</div>
