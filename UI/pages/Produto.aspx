<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterDefault.Master" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="UI.pages.Produto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    
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
                                <a id="btnNovo" class="btn btn-margin-5px input-medium">Novo <i class="icon-plus"></i></a>
                            </div>
                            <div class="btn-group pull-right">
                                <a id="btnOpcoes" class="btn btn-margin-5px">Opções</a>
                            </div>
                        </div>

                    <div class="row-fluid">
                        <!-- Listagem -->
                        <div class="portlet-body no-more-tables">
                            <table id="gridItens" class="table table-striped table-bordered table-hover table-full-width" cellspacing="0" width="100%" data-qtdRegistros="10">
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
        <div class="portlet box light-grey">
            <div class="portlet-title">
                <div class="caption"><i class="icon-exclamation-sign"></i>Falha na Transmissão dos dados</div>
            </div>
        </div>
        <div class="modal-body" id="divTexto">
        </div>
        <div class="modal-footer">
            <a type="button" class="btn btn-margin-5px" id="btnCancelarContrato" data-dismiss="modal">Fechar <i class="icon-off"></i></a>
        </div>
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
