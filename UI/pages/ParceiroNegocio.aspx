<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterDefault.Master" AutoEventWireup="true" CodeBehind="ParceiroNegocio.aspx.cs" Inherits="UI.pages.ParceiroNegocio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="container-fluid">
        <!-- BEGIN PAGE CONTAINER-->
        <!-- BEGIN PAGE HEADER-->
        <div class="row-fluid">
            <div class="span12">
                <h3 class="page-title">Parceiro de Negócio<small> listagem dos cadastrados</small>
                </h3>
                <ul class="breadcrumb">
                    <li>
                        <i class="icon-home"></i>
                        <a href="/pages/Dashboard.aspx">Home</a>
                        <span class="icon-angle-right"></span>
                    </li>
                    <li>
                        <a href="#">Parceiro de Negócio</a>
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
                        <i class="icon-reorder"></i>Parceiro de Negócio
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
                        <%--<div class="btn-group pull-right">
                            <a id="btnOpcoes" class="btn btn-margin-5px">Opções</a>
                        </div>--%>
                    </div>

                    <div class="row-fluid">
                        <!-- Listagem -->
                        <div class="portlet-body no-more-tables">
                            <table id="gridParceiros" class="table table-striped table-bordered table-hover table-full-width" cellspacing="0" width="100%" data-qtdregistros="10">
                                <thead>
                                    <tr>
                                        <%--<th style="width: 10%">Cód.</th>--%>
                                        <th style="width: 15%">CNPJ</th>
                                        <th>Razão Social</th>
                                        <th>Nome Fantasia</th>
                                       <%-- <th>Telefone</th>
                                        <th>Celular</th>--%>
                                        <th>Email</th>
                                        <th style="width: 13%">Tipo de Parceiro</th>
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
                    <div class="caption"><i class="icon-edit"></i>Novo Parceiro de Negócio</div>
                </div>
            </div>
            <div class="modal-body">
                <div id="formDados">
                    <div class="row-fluid">
                        <div class="span12 no-margin-left">
                            <div class="portlet box light-grey" style="margin-top: -20px;">
                                <div class="portlet-title">
                                    <div class="caption"><i class="icon-info"></i>Dados do Parceiro</div>
                                    <div class="tools">
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="clearfix noPrint">
                                        <div class="btn-group">
                                        </div>
                                        <div class="btn-group pull-right">
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="controls span6">
                                            <span>Razão Social</span>
                                            <input type="text" class="span12 m-wrap" id="txtRazaoSocial" data-json="RazaoSocial" maxlength="100" validate-json="Informe a Razão Social" />
                                        </div>
                                        <div class="controls span6">
                                            <span>Nome Fantasia</span>
                                            <input type="text" class="span12 m-wrap" id="txtNomeFantasia" data-json="NomeFantasia" maxlength="100" validate-json="Informe o Nome Fantasia" />
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="controls span6">
                                            <span>CNPJ</span>
                                            <input type="text" class="span12 m-wrap" id="txtCNPJ" data-json="CNPJ" maxlength="18" mascara="99.999.999/9999-99" validate-json="Informe o CNPJ" />
                                        </div>
                                        <div class="controls span6">
                                            <span>Tipo de Parceiro</span>
                                            <select id="ddlTipoPaceiro" class="span12 m-wrap" data-json="TipoParceiro">
                                                <option value="1">Comprador</option>
                                                <option value="2">Fornecedor</option>
                                                <option value="3">Comprador/Fornecedor</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="controls span12">
                                            <span>Grupos Associados</span>
                                            <input type="hidden" class="select2-offscreen" id="hfGrupoParceiro" style="width: 100%" value="" for="ddlGrupoParceiro" data-json />
                                            <input type="text" class="hidden" id="ddlGrupoParceiro" name="ddlGrupoParceiro" value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span12 no-margin-left">
                            <div class="portlet box light-grey" style="margin-top: -20px;">
                                <div class="portlet-title">
                                    <div class="caption"><i class="icon-envelope"></i>Contato</div>
                                    <div class="tools">
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="clearfix noPrint">
                                        <div class="btn-group">
                                        </div>
                                        <div class="btn-group pull-right">
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="controls span6">
                                            <span>Email</span>
                                            <input type="text" class="span12 m-wrap" id="txtEmail" data-json="Email" maxlength="150" validate-json="Informe o Email" validate-email="Email inválido" />
                                        </div>
                                        <div class="controls span3">
                                            <span>Telefone</span>
                                            <input type="text" class="span12 m-wrap" id="txtTelefone" data-json="Telefone" mascara="(99)9999-9999" validate-json="Informe o Telefone" />
                                        </div>
                                        <div class="controls span3">
                                            <span>Celular</span>
                                            <input type="text" class="span12 m-wrap" id="txtCelular" data-json="Celular" mascara="(99)99999-9999" validate-json="Informe o Celular" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span12 no-margin-left">
                            <div class="portlet box light-grey" style="margin-top: -20px;">
                                <div class="portlet-title">
                                    <div class="caption"><i class="icon-truck"></i>Endereço</div>
                                    <div class="tools">
                                    </div>
                                </div>
                                <div class="portlet-body form" id="Endereco">
                                    <div class="clearfix noPrint">
                                        <div class="btn-group">
                                        </div>
                                        <div class="btn-group pull-right">
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="controls span2">
                                            <span>CEP</span>
                                            <input type="text" class="span12 m-wrap" id="txtCEP" data-json="Endereco.CEP" mascara="99.999-999" validate-json="Informe o  CEP" />
                                        </div>
                                        <div class="controls span10">
                                            <span>Logradouro e Número</span>
                                            <input type="text" class="span12 m-wrap" id="txtLogradouro" data-json="Endereco.Logradouro" maxlength="150" validate-json="Informe o Logradouro e Número" />
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="controls span5">
                                            <span>Bairro</span>
                                            <input type="text" class="span12 m-wrap" id="txtBairro" data-json="Endereco.Bairro" maxlength="50" validate-json="Informe o Bairro" />
                                        </div>
                                        <div class="controls span5">
                                            <span>Cidade</span>
                                            <input type="text" class="span12 m-wrap" id="txtCidade" data-json="Endereco.Cidade" maxlength="50" validate-json="Informe a Cidade" />
                                        </div>
                                        <div class="controls span2">
                                            <span>Estado</span>
                                            <select id="ddlUF" class="span12 m-wrap" data-json="Endereco.Estado">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <ul id="iconePerfilTab" class="nav nav-tabs">
                        <li class="active">
                            <a href="#tabComprador" data-toggle="tab">Comprador</a>
                        </li>
                        <li>
                            <a href="#tabFornecedor" data-toggle="tab">Fornecedor</a>
                        </li>
                    </ul>

                    <div id="TabContent" class="tab-content">
                        <div class="tab-pane active" id="tabComprador">
                            <div class="clearfix noPrint">
                                <div class="btn-group">
                                </div>
                                <div class="btn-group pull-right">
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span12 no-margin-left">
                                    <div class="portlet box light-grey" style="margin-top: -20px;">
                                        <div class="portlet-title">
                                            <div class="caption"><i class="icon-usd"></i>Dados do Comprador</div>
                                            <div class="tools">
                                            </div>
                                        </div>
                                        <div class="portlet-body form" id="formComprador">
                                            <div class="clearfix noPrint">
                                                <div class="btn-group">
                                                </div>
                                                <div class="btn-group pull-right">
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                                <div class="controls span2">
                                                    <span>Produto</span>
                                                    <input type="hidden" class="select2-offscreen" id="hfProdutoComprador" style="width: 100%" value="" for="ddlProdutoComprador" data-json validate-json="Informe o produto" />
                                                    <input type="text" class="hidden" id="ddlProdutoComprador" name="ddlProdutoComprador" value="" />
                                                </div>
                                                <div class="controls span2">
                                                    <span>Valor Médio Compra</span>
                                                    <input type="text" class="span12 m-wrap maskdecimal" id="txtValorMedioCompra" data-json="ValorMedioCompra" maxlength="13" validate-json="Informe o Valor Médio Compra" />
                                                </div>
                                                <div class="controls span2">
                                                    <span>Quantidade</span>
                                                    <input type="text" class="span12 m-wrap" id="txtQuantidade" data-json="Quantidade" mascara="9999999" validate-json="Informe a Quantidade" />
                                                </div>
                                                <div class="controls span2">
                                                    <span>Frequencia</span>
                                                    <input type="text" class="span12 m-wrap" id="txtFrequencia" data-json="Frequencia" mascara="9999999" validate-json="Informe a Frequência" />
                                                </div>
                                                <div class="controls span4">
                                                    <a id="btnAddProdutoComprador" class="btn blue btn-margin-5px" style="width: 110px; margin-top: 20px;">Adicionar| <i class="icon-plus"></i></a>
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                                <!-- Listagem -->
                                                <div class="portlet-body no-more-tables">
                                                    <table id="gridProdutoComprador" class="table table-striped table-bordered table-hover table-full-width" data-qtdregistros="5" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>Produto</th>
                                                                <th>Valor Médio Compra</th>
                                                                <th style="width: 15%;">Quantidade</th>
                                                                <th style="width: 15%;">Frequencia</th>
                                                                <th style="width: 10%;">Ações</th>
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

                        <div class="tab-pane" id="tabFornecedor" tipo-aba-painel="1">
                            <div class="clearfix noPrint">
                                <div class="btn-group">
                                </div>
                                <div class="btn-group pull-right">
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span12 no-margin-left">
                                    <div class="portlet box light-grey" style="margin-top: -20px;">
                                        <div class="portlet-title">
                                            <div class="caption"><i class="icon-usd"></i>Dados do Fornecedor</div>
                                            <div class="tools">
                                            </div>
                                        </div>
                                        <div class="portlet-body form" id="formFornecedor">
                                            <div class="clearfix noPrint">
                                                <div class="btn-group">
                                                </div>
                                                <div class="btn-group pull-right">
                                                </div>
                                            </div>

                                            <div class="row-fluid">
                                                <div class="controls span2">
                                                    <span>Produto</span>
                                                    <input type="hidden" class="select2-offscreen" id="hfProdutoFornecedor" style="width: 100%" value="" for="ddlProdutoFornecedor" data-json validate-json="Informe o produto" />
                                                    <input type="text" class="hidden" id="ddlProdutoFornecedor" name="ddlProdutoFornecedor" value="" />
                                                </div>
                                                <div class="controls span2">
                                                    <span>Valor</span>
                                                    <input type="text" class="span12 m-wrap maskdecimal" id="txtValor" data-json="Valor" maxlength="13" validate-json="Informe o Valor" />
                                                </div>
                                                <div class="controls span2">
                                                    <span>Volume</span>
                                                    <input type="text" class="span12 m-wrap" id="txtVolume" data-json="Volume" mascara="9999999" validate-json="Informe o Volume" />
                                                </div>
                                                <div class="controls span2">
                                                    <span>Capacidade  Máxima</span>
                                                    <input type="text" class="span12 m-wrap" id="txtCapacidade" data-json="CapacidadeMaxima" mascara="9999999" validate-json="Informe a Capacidade Máxima" />
                                                </div>
                                                <div class="controls span4">
                                                    <a id="btnAddProdutoFornecedor" class="btn blue btn-margin-5px" style="width: 110px; margin-top: 20px;">Adicionar| <i class="icon-plus"></i></a>
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                                <!-- Listagem -->
                                                <div class="portlet-body no-more-tables">
                                                    <table id="gridProdutoFornecedor" class="table table-striped table-bordered table-hover table-full-width" data-qtdregistros="5" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>Produto</th>
                                                                <th style="width: 15%;">Valor</th>
                                                                <th style="width: 15%;">Volume</th>
                                                                <th style="width: 15%;">Capacidade  Máxima</th>
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
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span12 no-margin-left">
                        <div class="portlet box light-grey">
                            <div class="portlet-title">
                                <div class="caption"><i class="icon-user"></i>Contatos</div>
                                <div class="tools">
                                </div>
                            </div>
                            <div class="portlet-body form" id="formContatos">
                                <div class="row-fluid">
                                    <div class="controls span6">
                                        <span>Nome</span>
                                        <input type="text" class="span12 m-wrap" id="txtNomeContato" data-json="Nome" maxlength="50" validate-json="Informe o nome para o contato" />
                                    </div>
                                    <div class="controls span6">
                                        <span>Email</span>
                                        <input type="text" class="span12 m-wrap" id="txtEmailContato" data-json="Email" maxlength="50" validate-json="Informe o email para o contato" validate-email="Email inválido" />
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="controls span6">
                                        <span>Telefone</span>
                                        <input type="text" class="span12 m-wrap" id="txtTelefoneContato" data-json="Telefone" mascara="(99)9999-9999" validate-json="Informe o telefone para o contato" />
                                    </div>
                                    <div class="controls span4">
                                        <a id="btnAddContato" class="btn blue btn-margin-5px" style="width: 110px; margin-top: 20px;">Adicionar| <i class="icon-plus"></i></a>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <!-- Listagem -->
                                    <div class="portlet-body no-more-tables">
                                        <table id="gridContatos" class="table table-striped table-bordered table-hover table-full-width" data-qtdregistros="5" cellspacing="0" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>Nome</th>
                                                    <th>Email</th>
                                                    <th>Telefone</th>
                                                    <th style="width: 10%;">Ações</th>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScript" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScripts" runat="server">
    <script src="/pages/parceiroNegocio.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            PN.init();
        });
    </script>
</asp:Content>
