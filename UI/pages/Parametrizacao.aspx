<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterDefault.Master" AutoEventWireup="true" CodeBehind="Parametrizacao.aspx.cs" Inherits="UI.pages.Parametrizacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="container-fluid">
        <!-- BEGIN PAGE CONTAINER-->
        <!-- BEGIN PAGE HEADER-->
        <div class="row-fluid">
            <div class="span12">
                <h3 class="page-title">Paramêtros
                </h3>
                <ul class="breadcrumb">
                    <li>
                        <i class="icon-home"></i>
                        <a href="/pages/Dashboard.aspx">Home</a>
                        <span class="icon-angle-right"></span>
                    </li>
                    <li>
                        <a href="#">Parâmetros</a>
                    </li>
                </ul>
            </div>
        </div>
        <!-- END PAGE HEADER-->
        <!-- BEGIN PAGE CONTENT-->
        <div class="row-fluid">
            <div class="span12 no-margin-left">
                <div class="portlet box light-grey" style="margin-top: -20px;">
                    <div class="portlet-title">
                        <div class="caption"><i class="icon-info"></i>Informações dos parâmetros</div>
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
                            <div class="controls span6">
                                <div class="row-fluid">
                                    <div class="controls span8">
                                        <span>Limite de Cancelamento de Compra</span>
                                    </div>
                                    <div class="controls span4">
                                        <input type="text" class="span12 m-wrap" data-json="LimiteCancelCompra" mascara="999999999" />
                                    </div>
                                </div>
                            </div>
                            <div class="controls span6">
                                <div class="row-fluid">
                                    <div class="controls span8">
                                        <span>Percentual de Lucratividade da Empresa.</span>
                                    </div>
                                    <div class="controls span4">
                                        <input type="text" class="span12 m-wrap maskdecimal" data-json="PercLucroEmpresa" maxlength="6" />
                                    </div>
                                </div>
                            </div>
                         
                        </div>
                        <div class="row-fluid">
                               <div class="controls span6">
                                <div class="row-fluid">
                                    <div class="controls span8">
                                        <span>Percentual de Lucratividade do Representante Comercial.</span>
                                    </div>
                                    <div class="controls span4">
                                        <input type="text" class="span12 m-wrap maskdecimal" data-json="PercLucroRepComercial" maxlength="6" />
                                    </div>
                                </div>
                            </div>
                            <div class="controls span6">
                                <div class="row-fluid">
                                    <div class="controls span8">
                                        <span>Rodas do Leilão.</span>
                                    </div>
                                    <div class="controls span4">
                                        <input type="text" class="span12 m-wrap" data-json="RodasLeilao" maxlength="50" mascara="999999999" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="controls span6">
                                <div class="row-fluid">
                                    <div class="controls span8">
                                        <span>Dias entre cada rodada.</span>
                                    </div>
                                    <div class="controls span4">
                                        <input type="text" class="span12 m-wrap" data-json="DiasCadaRodada" maxlength="50" mascara="999" />
                                    </div>
                                </div>
                            </div>
                            <div class="controls span6">
                                <div class="row-fluid">
                                    <div class="controls span8">
                                        <span>Margem de Garantia de Preço.</span>
                                    </div>
                                    <div class="controls span4">
                                        <input type="text" class="span12 m-wrap maskdecimal" data-json="MargemGarantiaPreco" maxlength="50" maxlength="6"/>
                                    </div>
                                </div>
                            </div>
                            </div>
                         <div class="row-fluid">
                            <div class="controls span6">
                                <div class="row-fluid">
                                    <div class="controls span8">
                                        <span>Segunda Margem de Garantia de Preço.</span>
                                    </div>
                                    <div class="controls span4">
                                        <input type="text" class="span12 m-wrap maskdecimal" data-json="SegundaMargemGarantiaPreco" maxlength="50" maxlength="6"/>
                                    </div>
                                </div>
                            </div>
                            <div class="controls">
                                <a id="btnSalvar" class="btn green btn-margin-5px" style="width: 110px; margin-top: 30px;">Salvar| <i class="icon-save"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- END PAGE CONTENT-->
        <!-- END PAGE CONTAINER-->
    </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">
  <script src="/pages/parametrizacao.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Parametrizacao.init();
        });
    </script>
</asp:Content>
