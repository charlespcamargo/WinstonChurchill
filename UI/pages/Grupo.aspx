<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterDefault.Master" AutoEventWireup="true" CodeBehind="Grupo.aspx.cs" Inherits="UI.pages.Grupo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
     <div class="container-fluid">
        <!-- BEGIN PAGE CONTAINER-->
        <!-- BEGIN PAGE HEADER-->
        <div class="row-fluid">
            <div class="span12">
                <h3 class="page-title">Grupo<small> listagem dos cadastrados</small>
                </h3>
                <ul class="breadcrumb">
                    <li>
                        <i class="icon-home"></i>
                        <a href="/pages/Dashboard.aspx">Home</a>
                        <span class="icon-angle-right"></span>
                    </li>
                    <li>
                        <a href="#">Grupo</a>
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
                        <i class="icon-reorder"></i>Grupo
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
                                        <%--<th style="width: 10%">Cód.</th>--%>
                                        <th style="width: 20%">Nome</th>
                                        <th style="width: 30%">Tipo</th>
                                        <th style="width: 30%">Categorias</th>
                                        <th style="width: 30%">Parceiros</th>
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
                    <div class="caption"><i class="icon-edit"></i>Novo grupo</div>
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
                                </div>
                                <div class="row-fluid">
                                    <div class="controls span12">
                                        <span>Categorias</span>
                                        <input type="hidden" class="select2-offscreen" id="hfCategoria" style="width: 100%" value="" for="ddlCategoria" data-json validate-json="Informe pelo menos uma categoria" />
                                        <input type="text" class="hidden" id="ddlCategoria" name="ddlCategoria" value="" />
                                    </div>
                                </div>
                                 <div class="row-fluid">
                                      <div class="controls span3">
                                        <span>Tipo de Grupo</span>
                                          <select id="ddlTipo" class="span12 m-wrap" data-json="TipoGrupo">
                                              <option value="1"> Compradores</option>
                                              <option value="2"> Fornecedores</option>
                                          </select>
                                      </div>
                                    <div class="controls span9">
                                        <span label-grupo></span>
                                        <input type="hidden" class="select2-offscreen" id="hfPaceiros" style="width: 100%" value="" for="ddlParceiros" data-json />
                                        <input type="text" class="hidden" id="ddlParceiros" name="ddlParceiros" value="" />
                                    </div>
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

     
         </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScript" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScripts" runat="server">
     <script src="/pages/grupo.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Grupo.init();
        });
    </script>
</asp:Content>
