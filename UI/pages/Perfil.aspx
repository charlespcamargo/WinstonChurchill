<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterDefault.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="WinstonChurchill.UI.pages.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="container-fluid">
        <!-- BEGIN PAGE HEADER-->
        <div class="row-fluid">
            <div class="span12">
                <h3 class="page-title"></h3>
                <ul class="breadcrumb">
                    <li>
                        <i class="icon-home"></i>
                        <a href="/pages/Dashboard.aspx">Home</a>
                        <span class="icon-angle-right"></span>
                    </li>
                    <li>
                        <a href="#">Perfil</a>
                        <span class="icon-angle-right"></span>
                    </li>
                </ul>
            </div>
        </div>
        <!-- END PAGE HEADER-->
        <!-- BEGIN PAGE CONTENT-->
        <div class="row-fluid">
            <div class="portlet box grey">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-info"></i>Perfil
                    </div>
                    <div class="tools hidden-phone">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body form" id="Dados">
                    <div class="row-fluid">
                        <div class="controls span6">
                            <h4>Nome</h4>
                            <input type="text" class="span12 m-wrap" id="txtNome" data-json="Nome" maxlength="50" validate-json="Informe o nome" />
                        </div>
                        <div class="controls span6">
                            <h4>E-mail</h4>
                            <input type="text" class="span12 m-wrap" id="txtEmail" data-json="Email" maxlength="50" validate-json="Informe o E-mail" validate-email="Email inválido" />
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="controls span12">
                            <h4>Senha Atual</h4>
                            <input type="password" class="span12 m-wrap" id="txtSenhaAtual" data-json="Senha" maxlength="50" />
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="controls span12">
                            <h4>Nova Senha</h4>
                            <input type="password" class="span12 m-wrap" id="txtSenhaNova" data-json="SenhaNova" />
                        </div>
                    </div>

                    <div class="row-fluid">
                        <div class="controls span12">
                            <h4>Confirmar Nova Senha</h4>
                            <input type="password" class="span12 m-wrap" id="txtSenhaNovaConfirmar" data-json="SenhaNovaConfirmar" />
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="controls span2">
                            <a id="btnSalvar" class="btn green btn-margin-5px" style="width: 110px; margin-top: 30px;">Salvar| <i class="icon-save"></i></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScript" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScripts" runat="server">
    <script src="/pages/perfil.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Perfil.init();
        });
    </script>
</asp:Content>
