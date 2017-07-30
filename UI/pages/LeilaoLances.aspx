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
                        Dados Básicos do Leilão
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
                        <div class="span5 controls">
                            <h4>Produto</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="Produto.Nome" />
                        </div>
                        <div class="span3 controls">
                            <h4>Data de Formação</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="DataFinalFormacao" />
                        </div> 
                    </div>
                    <div class="row-fluid">                                                 
                        <div class="span4 controls">
                            <h4>Representante Comercial</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="Representante.Nome" />
                        </div>
                        <div class="span2 controls">
                            <h4>Quantidade Total</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="QtdDesejada" />
                        </div>
                        <div class="span3 controls">
                            <h4>Duração da rodada(dias)</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="DuracaoRodadasDias" />
                        </div>
                        <div class="span3 controls">
                            <h4>Data de Abertura</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="DataAbertura" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row-fluid">
            <div class="portlet box grey" id="formFornecedor">
                <div class="portlet-title">
                    <div class="caption">
                        Informações do Fornecedor
                    </div>
                    <div class="tools ">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="row-fluid">
                        <div class="span3 controls">
                            <h4>Fornecedor</h4>
                            <input type="text" class="span12 m-wrap" readonly="readonly" data-json="" />
                        </div>
                        <div class="span3 controls">
                            <h4>Quantidade Minima</h4>
                            <input type="text" class="span12 m-wrap" id="txtQtdMinima" data-json="QtdMinima" mascara="9999999" validate-json="Informe a Quantidade Maxima" />
                        </div> 
                        <div class="span3 controls">
                            <h4>Quantidade Maxima</h4>
                            <input type="text" class="span12 m-wrap" id="txtQtdMaxima" data-json="QtdMinima" mascara="9999999" validate-json="Informe a Quantidade Maxima" />
                        </div> 
                        <div class="span3 controls">
                            <h4>Participando</h4>
                            <input class="span12 m-wrap" type="checkbox" id="chkParticipando" data-json="Participando" />
                        </div> 
                    </div>
                    <div class="row-fluid">
                         
                    </div>
                </div>
            </div>
        </div>
        <!-- END PAGE CONTENT-->
    </div>
    <!-- END PAGE CONTAINER-->
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">

    <script src="/pages/LeilaoLances.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            LeilaoLances.init();
        });
    </script>


</asp:Content>
