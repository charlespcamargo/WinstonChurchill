<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="WinstonChurchill.Publico.Cadastro" %>


<asp:Content ID="cphHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="cphConteudo" ContentPlaceHolderID="cphConteudo" runat="server">

    <header class="page-interna header-sobre">
        <div id="header" class="container">
            <div class="row">
                <div class="col-sm-12">
                    <div class="header-content">
                        <div id="header-content" class="header-content-interna">
                            <h1 class="text-title">| Cadastro</h1>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <section class="default-content">
        <div class="container">
            <div class="row">
                <div class="panel-heading">
                    <div class="panel-title text-center">
                        <h1 class="text-title text-uppercase">Cadastre-se no WebRebate</h1>
                    </div>
                </div>
                <div class="main-login main-center">
                    <form class="form-horizontal" method="post" action="#">

                        <div class="form-group">
                            <label for="name" class="cols-sm-2 control-label">Seu nome</label>
                            <div class="cols-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <input type="text" class="form-control" name="name" id="name" placeholder="Coloque seu nome" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="email" class="cols-sm-2 control-label">Seu E-mail</label>
                            <div class="cols-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
                                    <input type="text" class="form-control" name="email" id="email" placeholder="Coloque seu e-mail" />
                                </div>
                            </div>
                        </div>
                         

                        <div class="form-group">
                            <label for="password" class="cols-sm-2 control-label">Senha</label>
                            <div class="cols-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
                                    <input type="password" class="form-control" name="password" id="password" placeholder="Coloque sua senha" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="confirm" class="cols-sm-2 control-label">Confirme sua senha</label>
                            <div class="cols-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
                                    <input type="password" class="form-control" name="confirm" id="confirm" placeholder="Confirme sua senha" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group ">
                            <button type="button" class="btn btn-outline second-color btn-lg btn-block login-button">Cadastre-se</button>
                        </div>
                        <div class="login-register">
                            <a id="lnkLogin" href="#">Login</a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </section>
    
</asp:Content>


<asp:Content ID="cphScripts" ContentPlaceHolderID="cphScript" runat="server">

    <script src="/Default.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        Home.init();
    </script>


</asp:Content>



