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
                        <a href="index.html">Home</a>
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
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal" id="submit_form">
                        <div class="form-wizard">
                            <div class="navbar steps">
                                <div class="navbar-inner">
                                    <ul class="row-fluid">
                                        <li class="span4 ">
                                            <a href="#tab1" data-toggle="tab" class="step active">
                                                <span class="number">1</span>
                                                <span class="desc"><i class="icon-ok"></i>Dados Básicos</span>
                                            </a>
                                        </li>
                                        <li class="span4">
                                            <a href="#tab2" data-toggle="tab" class="step">
                                                <span class="number">2</span>
                                                <span class="desc"><i class="icon-ok"></i>Compradores</span>
                                            </a>
                                        </li>
                                        <li class="span4">
                                            <a href="#tab3" data-toggle="tab" class="step">
                                                <span class="number">3</span>
                                                <span class="desc"><i class="icon-ok"></i>Fornecedores</span>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div id="bar" class="progress progress-success progress-striped">
                                <div class="bar" style="width: 25%;"></div>
                            </div>
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <h3 class="block">Dados Básicos do Leilão</h3>
                                    <div class="row-fluid">
                                        <div class="span6 controls">
                                            <h4>Nome do Leilão</h4>
                                            <input type="text" class="span12 m-wrap" name="nome" value="Leilão de Farinha Sorocaba - 139" />
                                        </div>
                                        <div class="span6 controls">
                                            <h4>Produto</h4>
                                            <select class="span12 chosen" data-placeholder="Selecione o Produto" tabindex="1">
                                                <option value="1">Farinha Branca Fina</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row-fluid">                                        
                                        <div class="span3 controls">
                                            <h4>Quantidade Total</h4>
                                            <input type="text" class="span12 m-wrap" name="quantidade" value="0" readonly />
                                        </div>
                                        <div class="span3 controls">
                                            <h4>Data de Formação</h4>
                                            <div class="input-append date date-picker" data-date-format="dd-mm-yyyy">
                                                <input class="span12 m-wrap date-picker" type="text" value="24/03/2017 - 28/03/2017" />
                                                <span class="add-on"><i class="icon-calendar"></i></span>
                                            </div>
                                        </div>
                                        <div class="span3 controls">
                                            <h4>Data de Abertura</h4>
                                            <div class="input-append date date-picker" data-date-format="dd-mm-yyyy">
                                                <input class="span12 m-wrap date-picker" type="text" value="29/03/2017" />
                                                <span class="add-on"><i class="icon-calendar"></i></span>
                                            </div>
                                        </div>
                                        <div class="span3 controls">
                                            <h4>Duração da rodada(dias)</h4>
                                            <input class="span12 m-wrap" type="text" value="02" /> 
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="span6 controls">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab2">
                                    <h3 class="block">Provide your profile details</h3>
                                    <div class="control-group">
                                        <label class="control-label">Fullname<span class="required">*</span></label>
                                        <div class="controls">
                                            <input type="text" class="span6 m-wrap" name="fullname" />
                                            <span class="help-inline">Provide your fullname</span>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">Phone Number<span class="required">*</span></label>
                                        <div class="controls">
                                            <input type="text" class="span6 m-wrap" name="phone" />
                                            <span class="help-inline">Provide your phone number</span>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">Gender<span class="required">*</span></label>
                                        <div class="controls">
                                            <label class="radio">
                                                <input type="radio" name="gender" value="M" data-title="Male" />
                                                Male
                                            </label>
                                            <div class="clearfix"></div>
                                            <label class="radio">
                                                <input type="radio" name="gender" value="F" data-title="Female" />
                                                Female
                                            </label>
                                            <div id="form_gender_error"></div>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">Address<span class="required">*</span></label>
                                        <div class="controls">
                                            <input type="text" class="span6 m-wrap" name="address" />
                                            <span class="help-inline">Provide your street address</span>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">City/Town<span class="required">*</span></label>
                                        <div class="controls">
                                            <input type="text" class="span6 m-wrap" name="city" />
                                            <span class="help-inline">Provide your city or town</span>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">Country</label>
                                        <div class="controls">
                                            <select name="country" id="country_list" class="span6">
                                                <option value="">Select</option>
                                                <option value="AF">Afghanistan</option>
                                                <option value="VI">Virgin Islands (U.S.)</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">Remarks</label>
                                        <div class="controls">
                                            <textarea class="span6 m-wrap" rows="3" name="remarks"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab3">
                                    <h3 class="block">Provide your billing and credit card details</h3>
                                    <div class="control-group">
                                        <label class="control-label">Card Holder Name<span class="required">*</span></label>
                                        <div class="controls">
                                            <input type="text" class="span6 m-wrap" name="card_name" />
                                            <span class="help-inline"></span>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">Card Number<span class="required">*</span></label>
                                        <div class="controls">
                                            <input type="text" class="span6 m-wrap" name="card_number" />
                                            <span class="help-inline"></span>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">CVC<span class="required">*</span></label>
                                        <div class="controls">
                                            <input type="text" placeholder="" class="m-wrap" name="card_cvc" />
                                            <span class="help-inline"></span>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">Expiration(MM/YYYY)<span class="required">*</span></label>
                                        <div class="controls">
                                            <input type="text" placeholder="MM" maxlength="2" class="m-wrap small" name="card_expiry_mm" />
                                            <input type="text" placeholder="YYYY" maxlength="4" class="m-wrap small" name="card_expiry_yyyy" />
                                            <span class="help-inline"></span>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">Payment Options<span class="required">*</span></label>
                                        <div class="controls">
                                            <label class="checkbox line">
                                                <input type="checkbox" name="payment[]" value="1" data-title="Auto-Pay with this Credit Card." />
                                                Auto-Pay with this Credit Card
                                            </label>
                                            <label class="checkbox line">
                                                <input type="checkbox" name="payment[]" value="2" data-title="Email me monthly billing." />
                                                Email me monthly billing
                                            </label>
                                            <div id="form_payment_error"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions clearfix">
                                <a href="javascript:;" class="btn button-previous ">
                                    <i class="m-icon-swapleft"></i>Voltar 
                                </a>
                                <a href="javascript:;" class="btn blue button-next">Continuar <i class="m-icon-swapright m-icon-white"></i>
                                </a>
                                <a href="javascript:;" class="btn green button-submit">Salvar <i class="m-icon-swapright m-icon-white"></i>
                                </a>
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
     
</asp:Content>