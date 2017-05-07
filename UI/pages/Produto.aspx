<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterDefault.Master" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="UI.pages.Produto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="container-fluid">
        <!-- BEGIN PAGE CONTAINER-->
        <!-- BEGIN PAGE HEADER-->
        <div class="row-fluid">
            <div class="span12">
                <h3 class="page-title">Produtos<small> listagem dos cadastrados</small>
                </h3>
                <ul class="breadcrumb">
                    <li>
                        <i class="icon-home"></i>
                        <a href="/pages/Default.aspx">Home</a>
                        <span class="icon-angle-right"></span>
                    </li>
                    <li>
                        <a href="#">Produtos</a>
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
                        <i class="icon-reorder"></i>Produtos
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
                        <div class="btn-group pull-right">
                            <a id="btnOpcoes" class="btn btn-margin-5px">Opções</a>
                        </div>
                    </div>

                    <div class="row-fluid">
                        <!-- Listagem -->
                        <div class="portlet-body no-more-tables">
                            <table id="gridItens" class="table table-striped table-bordered table-hover table-full-width" cellspacing="0" width="100%" data-qtdregistros="10">
                                <thead>
                                    <tr>
                                        <%--<th style="width: 10%">Cód.</th>--%>
                                        <th style="width: 30%">Nome</th>
                                        <th style="width: 10%">Status</th>
                                        <th style="width: 13%">Cadastrado</th>
                                        <th style="width: 13%">Categorias</th>
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
        <!-- END PAGE CONTAINER-->

        <div id="modalNovo" class="modal hide fade container" tabindex="-1" data-backdrop="fixed">
            <div class="portlet box grey">
                <div class="portlet-title">
                    <div class="caption"><i class="icon-edit"></i>Novo Produto</div>
                </div>
            </div>
            <div class="modal-body">
                <div class="row-fluid">
                    <div class="span12 no-margin-left">
                        <div class="portlet box light-grey" style="margin-top:-20px;">
                            <div class="portlet-title">
                                <div class="caption"><i class="icon-info"></i>Dados Básicos</div>
                                <div class="tools">
                                </div>
                            </div>
                            <div class="portlet-body form" id="Dados">
                                <div class="clearfix noPrint">
                                    <div class="btn-group">
                                    </div>
                                    <div class="btn-group pull-right">
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="controls span12">
                                        <span>Nome</span>
                                        <input type="text" class="span12 m-wrap" id="txtNome" data-json="Nome" maxlength="50" validate-json="Informe o nome" />
                                    </div>
                                    <div class="controls span12">
                                        <span style="margin-left: -27px;">Descrição</span>
                                        <textarea class="span12 m-wrap" id="txtDescricao" data-json="Descricao" validate-json="Informe uma descrição" maxlength="255" style="height: 100px; margin-left: -27px;"></textarea>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="controls span12">
                                        <span>Categorias</span>
                                        <input type="hidden" class="select2-offscreen" id="hfCategoria" style="width: 100%" value="" for="ddlProduto" data-json validate-json="Informe pelo menos uma categoria" />
                                        <input type="text" class="hidden" id="ddlCategoria" name="ddlCategoria" value="" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span12 no-margin-left">
                        <div class="portlet box light-grey">
                            <div class="portlet-title">
                                <div class="caption"><i class="icon-table"></i>Caraterísticas</div>
                                <div class="tools">
                                </div>
                            </div>
                            <div class="portlet-body form" id="Caracteristicas">
                                <div class="row-fluid">
                                    <div class="controls span8">
                                        <span>Nome</span>
                                        <input type="text" class="span12 m-wrap" id="txtNomeCaracteristica" data-json="Nome" maxlength="50" validate-json="Informe o nome para a característica" />
                                    </div>
                                     <div class="controls span4">
                                       <a id="btnAddCaracteristica" class="btn blue btn-margin-5px" style="width: 110px; margin-top: 20px;">Adicionar| <i class="icon-plus"></i></a>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <!-- Listagem -->
                                    <div class="portlet-body no-more-tables">
                                        <table id="gridCaracterísticas" class="table table-striped table-bordered table-hover table-full-width" data-qtdregistros="5" cellspacing="0" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>Nome</th>
                                                    <th style="width: 20%;">Ações</th>
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
                </div>
            </div>
            <div class="modal-footer">
                <a id="btnSalvar" class="btn green btn-margin-5px" style="width: 110px; margin-top: 30px;">Salvar| <i class="icon-save"></i></a>
                <a class="btn red btn-margin-5px" style="width: 110px; margin-top: 30px;" data-dismiss="modal">Fechar| <i class="icon-off"></i></a>
            </div>
        </div>

        <!-- BEGIN MODAL ANEXOS-->
        <div id="modalImagens" class="modal hide fade container" tabindex="-1" data-backdrop="fixed">
            <div class="portlet box light-grey">
                <div class="portlet-title">
                    <div class="caption"><i class="icon-cloud"></i>Imagens do produtos</div>
                </div>
            </div>
            <div class="modal-body">
                <div class="row-fluid">
                    <div class="span12 no-margin-left">
                        <div class="portlet box light-grey">
                            <div class="portlet-title">
                                <div class="caption"><i class="icon-table"></i>Subir imagem</div>
                                <div class="tools">
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="row-fluid">
                                    <div class="controls span4">
                                        <h4>Selecione os arquivos</h4>
                                        <input type="file" name="file_upload" id="file_upload" />
                                    </div>
                                    <div class="controls span6">
                                        <a class="btn green btn-margin-5px" id="btnAnexar" style="margin-top: 30px;">Anexar <i class="icon-upload-alt"></i></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span12 no-margin-left">
                        <div class="portlet box light-grey">
                            <div class="portlet-title">
                                <div class="caption"><i class="icon-table"></i>Lista de Imagens</div>
                                <div class="tools">
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="row-fluid">
                                    <!-- Listagem -->
                                    <div class="portlet-body no-more-tables">
                                        <table id="gridImagens" class="table table-striped table-bordered table-hover table-full-width" data-qtdregistros="5" no-filter cellspacing="0" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>Imagem</th>
                                                    <%--<th>Data do Upload</th>--%>
                                                    <th style="width: 20%;">Ações</th>
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
                </div>
            </div>

            <div class="modal-footer">
                <a class="btn red btn-margin-5px" data-dismiss="modal">Fechar <i class="icon-off"></i></a>
            </div>
        </div>
        <%--END MODAL ANEXOS--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScript" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScripts" runat="server">
    <script src="/pages/produto.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Produtos.init();
        });
    </script>
</asp:Content>
