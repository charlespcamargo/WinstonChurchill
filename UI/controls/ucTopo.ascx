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
