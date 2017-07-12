<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterDefault.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="UI.pages.Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">


    <!-- BEGIN PAGE CONTAINER-->
    <div class="container-fluid">
        <!-- BEGIN PAGE HEADER-->
        <div class="row-fluid">
            <div class="span12">
                <h3 class="page-title">Usuários<small> listagem dos cadastrados</small>
                </h3>
                <ul class="breadcrumb">
                    <li>
                        <i class="icon-home"></i>
                        <a href="/pages/Dashboard.aspx">Home</a>
                        <span class="icon-angle-right"></span>
                    </li>
                    <li>
                        <a href="#">Usuários</a>
                        <span class="icon-angle-right"></span>
                    </li>
                    <li><a href="/pages/Usuario.aspx">Listagem</a></li>
                </ul>
            </div>
        </div>
        <!-- END PAGE HEADER-->
        <!-- BEGIN PAGE CONTENT-->
        <div class="row-fluid">
            <div class="portlet box grey">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-reorder"></i>Usuários
                    </div>
                    <div class="tools hidden-phone">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="clearfix noPrint">
                        <div class="btn-group pull-left" style="width: 200px">
                            <a id="btnNovo" class="btn blue btn-margin-5px input-medium">Novo <i class="icon-plus"></i></a>
                        </div>
                    </div>

                    <div class="row-fluid">
                        <!-- Listagem -->
                        <div class="portlet-body no-more-tables">
                            <table id="gridItens" class="table table-striped table-bordered table-hover table-full-width" cellspacing="0" width="100%" data-qtdregistros="10">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">Cód.</th>
                                        <th style="width: 30%">Nome</th>
                                        <th style="width: 30%">Email</th>
                                        <th style="width: 10%">Status</th>
                                        <th style="width: 13%">Perfil</th>
                                        <th style="width: 7%">Ações</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                        <!-- FIM - Listagem -->
                    </div>

                </div>
            </div>
        </div>
        <!-- END PAGE CONTENT-->
    </div>
    <!-- END PAGE CONTAINER-->

    <div id="modalNovo" class="modal hide fade container fixedwidth" tabindex="-1" data-backdrop="fixed">
        <div class="portlet box grey">
            <div class="portlet-title">
                <div class="caption"><i class="icon-edit"></i>Novo Usuário</div>
            </div>
        </div>
        <div class="modal-body">
            <div class="row-fluid">
                <div class="span12 no-margin-left">
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
                                <h4>Ativo</h4>
                                <input type="checkbox" class="span12 m-wrap" id="chkAtivo" data-json="Ativo" />
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="controls span12">
                                <h4>Grupo de Acesso</h4>
                                <select id="ddlGrupoAcesso" class="span12 m-wrap chosen" data-json=""  data-placeholder="Selecione" multiple="multiple" style="width:500px;" validate-json="Informe pelo menos 1 grupo de acesso">
                                </select>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="controls span12">
                                <h4 id="lblSenha">Senha Atual</h4>
                                <input type="password" class="span12 m-wrap" id="txtSenhaAtual" data-json="Senha" maxlength="50" />
                            </div>
                        </div>
                        <div class="row-fluid" alt-senha>
                            <div class="controls span12">
                                <h4>Nova Senha</h4>
                                <input type="password" class="span12 m-wrap" id="txtSenhaNova" data-json="SenhaNova" />
                            </div>
                        </div>

                        <div class="row-fluid" alt-senha>
                            <div class="controls span12">
                                <h4>Confirmar Nova Senha</h4>
                                <input type="password" class="span12 m-wrap" id="txtSenhaNovaConfirmar" data-json="SenhaNovaConfirmar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </div>
        <div class="modal-footer">
            <a id="btnSalvar" class="btn green btn-margin-5px" style="width: 110px; margin-top: 30px;">Salvar| <i class="icon-save"></i></a>
            <a class="btn red btn-margin-5px" style="width: 110px; margin-top: 30px;" data-dismiss="modal">Fechar| <i class="icon-off"></i></a>
        </div>
    </div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">

    <script src="/pages/usuario.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Usuarios.init();
        });
    </script>

</asp:Content>
