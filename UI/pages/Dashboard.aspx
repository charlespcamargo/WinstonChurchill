<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterDefault.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="UI.pages.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

    <style type="text/css">
        .tile {
            padding: 20px;
            margin: 20px;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">


    <!-- BEGIN PAGE CONTAINER-->
    <div class="container-fluid">
        <!-- BEGIN PAGE HEADER-->

        <div class="row-fluid">
            <div class="controls span12">
                <h4>&nbsp;</h4>
            </div>
        </div>
        <!-- END PAGE HEADER-->
        <!-- BEGIN PAGE CONTENT-->
        <div class="row-fluid">
            <div class="controls span12">
                <div class="tiles hide">

                    <div class="tile bg-grey" id="tile_perfil" tipo="1">
                        <div class="tile-body">
                            <i class="icon-male"></i>
                        </div>
                        <div class="tile-object">
                            <div class="name">
                                Meu Perfil
                            </div>
                        </div>
                    </div>

                    <div class="tile bg-grey" id="tile_usuario" tipo="3">
                        <div class="tile-body">
                            <i class="icon-user"></i>
                        </div>
                        <div class="tile-object">
                            <div class="name">
                                Usuários
                            </div>
                        </div>
                    </div>

                    <div class="tile bg-grey" id="tile_configuracoes" tipo="4">
                        <div class="tile-body">
                            <i class="icon-cogs"></i>
                        </div>
                        <div class="tile-object">
                            <div class="name">
                                Configurações
                            </div>
                        </div>
                    </div>

                    <div class="tile bg-grey" id="tile_categoria" tipo="3">
                        <div class="tile-body">
                            <i class="icon-tags"></i>
                        </div>
                        <div class="tile-object">
                            <div class="name">
                                Categorias
                            </div>
                        </div>
                    </div>

                    <div class="tile bg-grey" id="tile_produto" tipo="3">
                        <div class="tile-body">
                            <i class="icon-archive"></i>
                        </div>
                        <div class="tile-object">
                            <div class="name">
                                Produtos
                            </div>
                        </div>
                    </div>

                    <div class="tile bg-grey" id="tile_grupo" tipo="2">
                        <div class="tile-body">
                            <i class="icon-group"></i>
                        </div>
                        <div class="tile-object">
                            <div class="name">
                                Grupos
                            </div>
                        </div>
                    </div>

                    <div class="tile bg-grey" id="tile_parceiro" tipo="1">
                        <div class="tile-body">
                            <i class="icon-briefcase"></i>
                        </div>
                        <div class="tile-object">
                            <div class="name">
                                Parceiros de Negócios
                            </div>
                        </div>
                    </div>

                    <div class="tile bg-grey" id="tile_leilao" tipo="1">
                        <div class="tile-body">
                            <i class="icon-legal"></i>
                        </div>
                        <div class="tile-object">
                            <div class="name">
                                Leilões
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        <!-- END PAGE CONTENT-->
    </div>
    <!-- END PAGE CONTAINER-->
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">

    <script src="/pages/dashboard.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Dashboard.init();
        });
    </script>

</asp:Content>
