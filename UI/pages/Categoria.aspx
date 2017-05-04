<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterDefault.Master" AutoEventWireup="true" CodeBehind="Categoria.aspx.cs" Inherits="WinstonChurchill.UI.pages.Categoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
        <!-- BEGIN PAGE CONTAINER-->
    <div class="container-fluid">
        <!-- BEGIN PAGE HEADER-->
        <div class="row-fluid">
            <div class="span12">
                <h3 class="page-title">Categoria
                </h3>
                <ul class="breadcrumb">
                    <li>
                        <i class="icon-home"></i>
                        <a href="/pages/Default.aspx">Home</a>
                        <span class="icon-angle-right"></span>
                    </li>
                    <li>
                        <a href="#">Categoria</a>
                    </li>
                </ul>
            </div>
        </div>
        <!-- END PAGE HEADER-->
        <!-- BEGIN PAGE CONTENT-->

        <div class="row-fluid">
            <div class="span12 no-margin-left">

                <div class="portlet box grey">
                    <div class="portlet-title">
                        <div class="caption"><i class="icon-filter"></i>Filtros</div>
                        <div class="tools">
                            <a class="collapse" href="javascript:;"></a>
                        </div>
                    </div>


                    <div id="filtros" class="portlet-body form">
                        <div class="clearfix noPrint">
                            <div class="btn-group">
                            </div>
                            <div class="btn-group pull-right">
                            </div>
                        </div>

                        <div class="row-fluid">

                            <div class="controls span2">
                                <span>Código do Reembolso</span>
                                <input type="text" class="span12 m-wrap" id="txtCodigoFiltro" data-filter="Numero" maxlength="6" />
                            </div>
                            <div class="controls span1">
                                <span>Ano</span>
                                <input type="text" class="span12 m-wrap" id="txtAnoFiltro" data-filter="Ano" maxlength="4" />
                            </div>
                            <div class="controls span3">
                                <span>Período de Inclusão</span>
                                <input type="text" class="span12 m-wrap periododata" id="txtPeriodoInclusao" data-filter="DtInclIni,DtInclFim" />
                            </div>
                            <div class="controls span3">
                                <span>Tipo</span>
                                <select class="span12 m-wrap chosen" id="ddlTipoFiltro" data-with-deselect="1" data-placeholder="Selecione o Tipo" data-filter="Tipo">
                                    <option value=""></option>
                                    <%--<option value="R">Reembolso</option>--%>
                                    <option value="C">Cartão</option>
                                    <option value="R">Reembolso (Colaborador)</option>
                                    <option value="T">Terceiro</option>
                                    <option value="F">Fornecedor</option>
                                    <option value="O">Outros</option>
                                </select>
                            </div>
                            <div class="controls span3">
                                <span>Centro de Custo</span>
                                <input type="text" class="span12 m-wrap" id="txtCentroFiltro" data-filter="CentroCusto" maxlength="8" />
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div id="divFuncionarioFiltro" class="controls span4" style="display: none;">
                                <span>Funcionário</span>
                                <input type="text" class="span12 m-wrap" id="txtFuncionarioFiltro" data-filter="Chapa" />
                            </div>

                            <div id="divTerceirosFiltro" class="controls span4" style="display: none;">
                                <span>Fornecedor</span>
                                <input type="text" class="span12 m-wrap" id="txtFornecedorFiltro" data-filter="Terceiro" />
                            </div>

                            <div id="divOutrosFiltro" class="controls span4" style="display: none;">
                                <span>Outros</span>
                                <input type="text" class="span12 m-wrap" id="txtOutrosFiltros" data-filter="Outros" />
                            </div>
                            <div id="divRegiaoComercialFiltro" class="controls span6" style="display: none;">
                                <span>Região Comercial</span>
                                <%--<input type="text" class="span12 m-wrap" id="txtRegiaoComercialFiltro" datafieldihara="Descricao" />--%>
                                <select class="span12 m-wrap" id="ddlRegiaoComercialFiltro" data-placeholder="Selecione" data-filter="RegiaoComercial">
                                    <option value=""></option>
                                </select>
                            </div>
                        </div>

                        <div class="portlet-footer form">
                            <br />
                            <div class="clearfix noPrint">
                                <div class="btn-group">
                                </div>
                                <div class="btn-group pull-right">
                                    <a id="btnFiltrar" onclick="Reembolso.Buscar();" class="btn btn-margin-5px">Buscar
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


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


                    <div class="row-fluid">
                        <!-- Listagem -->
                        <div class="portlet-body no-more-tables">
                            <table id="gridItens" class="table table-striped table-bordered table-hover table-full-width" cellspacing="0" width="100%" data-qtdRegistros="10">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">Cód.</th>
                                        <th style="width: 30%">Nome</th>
                                        <th style="width: 30%">Email</th>
                                        <th style="width: 10%">Status</th>
                                        <th style="width: 13%">Cadastrado</th>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScript" runat="server">

 

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScripts" runat="server">
     <script src="categoria.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Categoria.init();
        });
    </script>
</asp:Content>
