﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI.Login" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
    <meta charset="utf-8" />
    <title>Web Rebate | Login</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/style-metro.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/style-responsive.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/themes/default.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="assets/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="assets/plugins/select2/select2_metro.css" />
    <link rel="stylesheet" type="text/css" href="<%=this.Page.ResolveClientUrl("~/assets/plugins/gritter/css/jquery.gritter.css")%>" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="assets/css/pages/login.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body class="login">
    <!-- BEGIN LOGO -->
    <div class="logo">
        <%--<img src="assets/img/logo-big.png" alt="" />--%>
    </div>
    <!-- END LOGO -->
    <!-- BEGIN LOGIN -->
    <div class="content">
        <form runat="server">
            <asp:HiddenField runat="server" ID="hfURLAPI" ClientIDMode="Static" />
            <!-- BEGIN LOGIN FORM -->
            <div id="frmLogin">
                <h3 class="form-title">LOGO</h3>
                <div class="alert alert-error hide">
                    <button class="close" data-dismiss="alert"></button>
                    <span>Enter any username and password.</span>
                </div>
                <div class="control-group">
                    <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                    <label class="control-label visible-ie8 visible-ie9">Email</label>
                    <div class="controls">
                        <div class="input-icon left">
                            <i class="icon-user"></i>
                            <input class="m-wrap placeholder-no-fix" type="text" autocomplete="off" placeholder="Email" data-json="UserName" validate-json="Informe o usuário" />
                        </div>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label visible-ie8 visible-ie9">Senha</label>
                    <div class="controls">
                        <div class="input-icon left">
                            <i class="icon-lock"></i>
                            <input class="m-wrap placeholder-no-fix" type="password" autocomplete="off" placeholder="Senha" data-json="Password" validate-json="Informe a senha" />
                        </div>
                    </div>
                </div>
                <div class="form-actions">
                    <button id="btnLogin" type="button" class="btn green pull-right">
                        Entrar <i class="m-icon-swapright m-icon-white"></i>
                    </button>
                </div>
               <%-- <div class="forget-password">
                    <h4>Esqueceu sua senha ?</h4>
                    <p>
                        Não se preocupe, clique <a href="javascript:;" id="forget-password">aqui</a>
                        para resetar a senha.
                    </p>
                </div>--%>
            </div>
            <!-- END LOGIN FORM -->
            <!-- BEGIN FORGOT PASSWORD FORM -->
         <%--   <h3>Forget Password ?</h3>
            <p>Enter your e-mail address below to reset your password.</p>
            <div class="control-group">
                <div class="controls">
                    <div class="input-icon left">
                        <i class="icon-envelope"></i>
                        <input class="m-wrap placeholder-no-fix" type="text" autocomplete="off" placeholder="Email" name="email" />
                    </div>
                </div>
            </div>
            <div class="form-actions">
                <button type="button" id="back-btn" class="btn">
                    <i class="m-icon-swapleft"></i>Back
                </button>
                <button type="submit" class="btn green pull-right" id="btnLogin">
                    Logar <i class="m-icon-swapright m-icon-white"></i>
                </button>
            </div>--%>
            <!-- END FORGOT PASSWORD FORM -->
        </form>
    </div>
    <!-- END LOGIN -->
    <!-- BEGIN COPYRIGHT -->
    <div class="copyright">
        <%=DateTime.Now.Year %> &copy; WebRebate.
    </div>
    <!-- END COPYRIGHT -->
    <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
    <!-- BEGIN CORE PLUGINS -->
    <script src="assets/plugins/jquery-1.10.1.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>
    <!-- IMPORTANT! Load jquery-ui-1.10.1.custom.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->
    <script src="assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.js" type="text/javascript"></script>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <%--<script src="assets/plugins/bootstrap-hover-dropdown/twitter-bootstrap-hover-dropdown.min.js" type="text/javascript"></script>--%>
    <!--[if lt IE 9]>
	<script src="assets/plugins/excanvas.min.js"></script>
	<script src="assets/plugins/respond.min.js"></script>  
	<![endif]-->
    <script src="assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery.cookie.min.js" type="text/javascript"></script>
    <script src="assets/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="assets/scripts/app.js" type="text/javascript"></script>
    <script src="<%=this.Page.ResolveClientUrl("~/assets/plugins/gritter/js/jquery.gritter.js")%>" type="text/javascript"></script>
    <script src="<%=this.Page.ResolveClientUrl("~/assets/scripts/funcoes/extensoes.js")%>"></script>
    <script src="/assets/scripts/HelperJS.js"></script>
    <script src="Login.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {
            Login.init();
        });
    </script>
    <!-- END JAVASCRIPTS -->
</body>
<!-- END BODY -->
</html>
