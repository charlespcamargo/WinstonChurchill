<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterDefault.Master" AutoEventWireup="true" CodeBehind="Leilao.aspx.cs" Inherits="UI.pages.Leilao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">


    <!-- BEGIN PAGE CONTAINER-->
    <div class="container-fluid">
        <!-- BEGIN PAGE HEADER-->
        <div class="row-fluid">
            <div class="span12">
                <h3 class="page-title">Leilão<small> cadastro passo a passo</small>
                </h3>
                <ul class="breadcrumb">
                    <li>
                        <i class="icon-home"></i>
                        <a href="/pages/Dashboard.aspx">Home</a>
                        <span class="icon-angle-right"></span>
                    </li>
                    <li>
                        <a href="/pages/Leiloes.aspx">Leilões</a>
                        <span class="icon-angle-right"></span>
                    </li>
                    <li><a href="#">cadastro passo a passo</a></li>
                </ul>
            </div>
        </div>
        <!-- END PAGE HEADER-->
        <!-- BEGIN PAGE CONTENT-->
        <div class="row-fluid">
            <div class="portlet box grey" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-reorder"></i>Leilão - <span class="step-title">Passo 1 de 3</span>
                    </div>
                    <div class="tools hidden-phone">
                        <a href="javascript:;" class=""></a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal" id="submit_form">
                        <div class="form-wizard">
                            <div class="navbar steps">
                                <div class="navbar-inner">
                                    <ul class="row-fluid">
                                        <li class="span4 ">
                                            <a href="#formDados" data-toggle="tab" class="step active">
                                                <span class="number">1</span>
                                                <span>Dados Básicos</span>
                                            </a>
                                        </li>
                                        <li class="span4">
                                            <a href="#formComprador" data-toggle="tab" class="step">
                                                <span class="number">2</span>
                                                <span>Compradores</span>
                                            </a>
                                        </li>
                                        <li class="span4">
                                            <a href="#formFornecedor" data-toggle="tab" class="step">
                                                <span class="number">3</span>
                                                <span>Fornecedores</span>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div id="bar" class="progress progress-success progress-striped">
                                <div class="bar" style="width: 33%;"></div>
                            </div>
                            <div class="tab-content">
                                <div class="tab-pane active" id="formDados">
                                    <div class="row-fluid">
                                        <div class="portlet box grey" id="">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    Dados Básicos do Leilão
                                                </div>
                                                <div class="tools hidden-phone">
                                                    <a href="javascript:;" class=""></a>
                                                </div>
                                            </div>
                                            <div class="portlet-body form">
                                                <div class="row-fluid">
                                                    <div class="span4 controls">
                                                        <h4>Nome do Leilão</h4>
                                                        <input type="text" class="span12 m-wrap" name="nome" value="" data-json="Nome" validate-json="Informe o Nome do Leilão" />
                                                    </div>
                                                    <div class="span2 controls">
                                                        <h4>Data de Formação</h4>
                                                        <div class="input-append date date-picker" data-date-format="dd-mm-yyyy">
                                                            <input class="span10 m-wrap date-picker" type="text" value="" id="txtDataFormacao" data-json="DataFinalFormacao" validate-json="Informe a Data de Formação" />
                                                            <span class="add-on"><i class="icon-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                    <div class="span2 controls">
                                                        <h4>Data de Abertura</h4>
                                                        <div class="input-append date date-picker" data-date-format="dd-mm-yyyy">
                                                            <input class="span10 m-wrap date-picker" type="text" value=""  id="txtDataAbertura" data-json="DataAbertura" validate-json="Informe a Data de Abertura" />
                                                            <span class="add-on"><i class="icon-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                    <div class="span2 controls">
                                                        <h4>Duração da rodada</h4>
                                                        <input class="span12 m-wrap" type="text" value="0" id="txtDuracaoCadaRodada" data-json="DiasCadaRodada"  readonly="readonly" />
                                                    </div>
                                                    <div class="span2 controls">
                                                        <h4>Qtd de Rodadas</h4>
                                                        <input class="span12 m-wrap" type="text" value="0" id="txtQtdRodadas" data-json="RodadasLeilao" readonly="readonly" />
                                                    </div>
                                                </div>
                                                <div class="row-fluid">
                                                    <div class="span4 controls">
                                                        <h4>Produto</h4>
                                                        <input type="hidden" class="select2-offscreen" id="hfProduto" style="width: 100%" value="" for="ddlProduto" data-json="ProdutoID" validate-json="Informe o Produto" />
                                                        <input type="text" class="hidden" id="ddlProduto" name="ddlProduto" value="" />
                                                    </div>
                                                    <div class="span4 controls">
                                                        <h4>Representante Comercial</h4>
                                                        <input type="hidden" class="select2-offscreen" id="hfRepresentanteComercial" style="width: 100%" value="" for="ddlRepresentanteComercial" data-json="RepresentanteID" validate-json="Informe o Representante Comercial" />
                                                        <input type="text" class="hidden" id="ddlRepresentanteComercial" name="ddlRepresentanteComercial" value="" />
                                                    </div>
                                                    <div class="span2 controls">
                                                        <h4>Quantidade Total</h4>
                                                        <input type="text" class="span12 m-wrap" name="quantidade" value="0" readonly="readonly" data-json="QtdDesejada" />
                                                    </div>
                                                    <div class="span2 controls">
                                                        <h4>Ativo</h4>
                                                        <input class="span12 m-wrap" type="checkbox" id="chkAtivo" data-json="Ativo" checked="checked" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="formComprador">
                                    <div class="portlet box grey" id="">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                Adicione compradores para o Leilão
                                            </div>
                                            <div class="tools hidden-phone">
                                                <a href="javascript:;" class=""></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body form">
                                            <div class="row-fluid">
                                                <div class="controls span8">
                                                    <span>Comprador</span>
                                                    <input type="hidden" class="select2-offscreen" id="hfComprador" style="width: 100%" value="" for="ddlComprador" data-json validate-json="Informe o Comprador" />
                                                    <input type="text" class="hidden" id="ddlComprador" name="ddlComprador" value="" />
                                                </div>
                                                <div class="controls span2">
                                                    <span>Qtd Desejada</span>
                                                    <input type="text" class="span12 m-wrap" id="txtCapacidade" data-json="QtdDesejada" mascara="9999999" validate-json="Informe a Quantidade Desejada" />
                                                </div>
                                                <div class="controls span2">
                                                    <a id="btnAddComprador" class="btn blue btn-margin-5px" style="width: 110px; margin-top: 20px;">Adicionar| <i class="icon-plus"></i></a>
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                                <!-- Listagem -->
                                                <div class="portlet-body no-more-tables">
                                                    <table id="gridComprador" class="table table-striped table-bordered table-hover table-full-width" data-qtdregistros="5" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 20%;">Comprador</th>
                                                                <th style="width: 20%;">Participando</th>
                                                                <th style="width: 20%;">Qtd Desejada</th>
                                                                <th style="width: 20%;">*</th>
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
                                <div class="tab-pane" id="formFornecedor">
                                    <div class="portlet box grey" id="">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                Adicione fornecedores para o Leilão
                                            </div>
                                            <div class="tools hidden-phone">
                                                <a href="javascript:;" class=""></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body form">
                                            <div class="row-fluid">
                                                <div class="controls span6">
                                                    <span>Fornecedor</span>
                                                    <input type="hidden" class="select2-offscreen" id="hfFornecedor" style="width: 100%" value="" for="ddlFornecedor" data-json="" validate-json="Informe o Fornecedor" />
                                                    <input type="text" class="hidden" id="ddlFornecedor" name="ddlFornecedor" value="" />
                                                </div>
                                                <div class="controls span2">
                                                    <span>Qtd Minima</span>
                                                    <input type="text" class="span12 m-wrap" id="txtQtdMinima" data-json="QtdMinima" mascara="9999999" validate-json="Informe a Quantidade Minima" />
                                                </div>
                                                <div class="controls span2">
                                                    <span>Qtd Máxima</span>
                                                    <input type="text" class="span12 m-wrap" id="txtQtdMaxima" data-json="QtdMaxima" mascara="9999999" validate-json="Informe a Quantidade Maxima" />
                                                </div>
                                                <div class="controls span2">
                                                    <a id="btnAddFornecedor" class="btn blue btn-margin-5px" style="width: 110px; margin-top: 20px;">Adicionar| <i class="icon-plus"></i></a>
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                                <!-- Listagem -->
                                                <div class="portlet-body no-more-tables">
                                                    <table id="gridFornecedor" class="table table-striped table-bordered table-hover table-full-width" data-qtdregistros="5" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 20%;">Fornecedor</th>
                                                                <th style="width: 20%;">Participando</th>
                                                                <th style="width: 20%;">Qtd Minima</th>
                                                                <th style="width: 20%;">Qtd Máxima</th>
                                                                <th style="width: 20%;">*</th>
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
                        <div class="form-actions clearfix">
                            <div class="row-fluid">
                                <div class="span2 controls">
                                    <a href="javascript:;" class="btn button-previous" style="display: none; width: 100px !important" id="btnAnterior">
                                        <i class="m-icon-swapleft"></i>Voltar 
                                    </a>
                                </div>
                                <div class="span2 controls">
                                    <a href="javascript:;" class="btn blue button-next" style="width: 100px" id="btnContinuar">Continuar <i class="m-icon-swapright m-icon-white"></i>
                                    </a>
                                </div>
                                <div class="span5 controls">
                                </div>
                                <div class="span3 controls">
                                    <a href="javascript:;" class="btn green button-submit" style="width: 100px" id="btnSalvar">Salvar <i class="m-icon-swapright m-icon-white"></i>
                                    </a>
                                </div>
                            </div>
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

    <script src="/pages/leilao.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Leilao.init();
        });
    </script>


</asp:Content>
