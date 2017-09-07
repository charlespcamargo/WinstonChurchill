<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterDefault.Master" AutoEventWireup="true" CodeBehind="LeilaoLances.aspx.cs" Inherits="UI.pages.LeilaoLances" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <!-- BEGIN PAGE CONTAINER-->
    <div class="container-fluid">
        <!-- BEGIN PAGE HEADER-->
        <div class="row-fluid">
            <div class="span12">
                <h3 class="page-title"><small>Leilão</small> Lances
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
                    <li><a href="#">Lances</a></li>
                </ul>
            </div>
        </div>
        <!-- END PAGE HEADER-->
        <!-- BEGIN PAGE CONTENT-->
        <div class="row-fluid">
            <div class="portlet box grey" id="formDados">
                <div class="portlet-title">
                    <div class="caption">
                        Dados do Leilão
                    </div>
                    <div class="tools ">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="row-fluid">
                        <div class="span4 controls">
                            <h4>Nome do Leilão</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="Nome" />
                        </div>
                        <div class="span4 controls">
                            <h4>Produto</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="Produto.Nome" />
                        </div>
                        <div class="span4 controls">
                            <h4>Representante Comercial</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="Representante.Nome" />
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span2 controls">
                            <h4>Quantidade Total</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="QtdDesejada" />
                        </div>
                        <div class="span2 controls">
                            <h4>Duração da rodada</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="DiasCadaRodada" />
                        </div>
                        <div class="span2 controls">
                            <h4>Qtd de Rodadas</h4>
                            <input class="span12 m-wrap" type="text" value="0" id="txtQtdRodadas" data-json="RodadasLeilao" readonly="readonly" />
                        </div>
                        <div class="span2 controls">
                            <h4>Data de Formação</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="DataFinalFormacao" />
                        </div>
                        <div class="span2 controls">
                            <h4>Data de Abertura</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="DataAbertura" />
                        </div>
                        <div class="span2 controls">
                            <h4>Ativo</h4>
                            <input class="span12 m-wrap" type="checkbox" id="chkAtivo" data-json="Ativo" checked="checked" disabled="disabled" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row-fluid">
            <div class="portlet box grey hide" id="formLances">
                <div class="portlet-title">
                    <div class="caption">
                        Lances
                    </div>
                    <div class="tools ">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="row-fluid">
                        <!-- Listagem -->
                        <div class="portlet-body no-more-tables">
                            <table id="gridLances" class="table table-striped table-bordered table-hover table-full-width" data-qtdregistros="5" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th style="width: 40%;">Fornecedor</th>
                                        <th style="width: 15%;">Nº da Rodada</th>
                                        <th style="width: 15%;">Data</th>
                                        <th style="width: 15%;">Valor</th>
                                        <th style="width: 15%;">Valor 2ª Margem</th> 
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

        <div class="row-fluid">
            <div class="portlet box grey hide" id="formComprador">
                <div class="portlet-title">
                    <div class="caption">
                        Compradores
                    </div>
                    <div class="tools ">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body form">
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

        <div class="row-fluid">
            <div class="portlet box grey hide" id="formFornecedor">
                <div class="portlet-title">
                    <div class="caption">
                        Fornecedores
                    </div>
                    <div class="tools ">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="row-fluid">
                        <!-- Listagem -->
                        <div class="portlet-body no-more-tables">
                            <table id="gridFornecedor" class="table table-striped table-bordered table-hover table-full-width" data-qtdregistros="5" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th style="width: 20%;">Forncedor</th>
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

        
        <!-- END PAGE CONTENT-->
    </div>
    <!-- END PAGE CONTAINER-->

    <div id="modal" class="modal hide fade" tabindex="-1" data-backdrop="fixed">
        <div class="portlet box grey">
            <div class="portlet-title">
                <div class="caption"><i class="icon-edit"></i>Parceiro de Negócio</div>
            </div>
        </div>
        <div class="modal-body" id="formModal">
            <div class="row-fluid">
                <div class="controls span9">
                    <h4>Nome Fantasia</h4>
                    <input type="text" class="span12 m-wrap" id="txtNomeFantasia" data-json="ParceiroNegocio.NomeFantasia" readonly="readonly" />
                </div>
                <div class="controls span3">
                    <h4>Participando</h4>
                    <input class="span12 m-wrap" type="checkbox" id="chkParticipando" data-json="Participando" checked="checked" />
                </div>
            </div>
            <div class="row-fluid">
                <div class="controls span6">
                    <h4 id="lblQtdEdicao">Qtd. Desejada/Qtd. Minima</h4>
                    <input type="text" class="span12 m-wrap hide" id="txtQuantidadeDesejada" data-json="QtdDesejada" />
                    <input type="text" class="span12 m-wrap hide" id="txtQuantidadeMin" data-json="QtdMinima" />
                </div>
                <div class="controls span6">
                    <h4 id="lblQtdMax">Qtd. Máxima</h4>
                    <input type="text" class="span12 m-wrap" id="txtQuantidadeMax" data-json="QtdMaxima" />
                </div>
            </div>
            <div class="row-fluid" id="lance_valor_container">
                <div class="controls span6">
                    <h4 id="lblMargem1">Valor</h4>
                    <input type="text" class="span12 m-wrap tooltips" id="txtValorLance1" data-trigger="hover" data-original-title="..." />
                </div>
                <div class="controls span6">
                    <h4 id="lblMargem2">Valor - 2ª Margem</h4>
                    <input type="text" class="span12 m-wrap tooltips" id="txtValorLance2" data-trigger="hover" data-original-title="..." />
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <a type="button" class="btn btn-margin-5px red" data-dismiss="modal">Cancelar | <i class="icon-off"></i></a>
            <a type="button" class="btn btn-margin-5px green" id="btnSalvar" onclick="javascript:;">Salvar | <i class="icon-save"></i></a>
            <a type="button" class="btn btn-margin-5px green hide" id="btnLance" onclick="javascript:;">Efetuar Lance | <i class="icon-legal"></i></a>
        </div>
    </div>


</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">

    <script src="/pages/LeilaoLances.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            LeilaoLances.init();
        });
    </script>


</asp:Content>
