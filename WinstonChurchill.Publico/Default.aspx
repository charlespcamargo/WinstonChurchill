﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WinstonChurchill.Publico.Default" %>


<asp:Content ID="cphHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="cphConteudo" ContentPlaceHolderID="cphConteudo" runat="server">


    <header>
        <div id="header" class="container">
            <div id="home" class="row">
                <div class="col-sm-5">
                    <div class="header-content">
                        <div id="header-content" class="header-content-inner">
                            <h1 class="text-title">Texto descritivo sobre o produto bem como outra frase importante</h1>
                            <hr>
                            <h3 class="text-description">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut
                                labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
                                laboris nisi ut aliquip ex ea commodo consequat.
                            </h3>
                            <div id="header-dl-button" class="row">
                                <div class="col-sm-6 col-md-6 col-lg-6 btn-padding">
                                    <a href="/Cadastro.aspx" id="lnkParticipar" class="btn btn-outline btn-xl page-scroll second-color">Participar</a>
                                </div>
                                <div class="col-sm-6 col-md-6 col-lg-6 btn-padding">
                                    <a href="/Sobre.aspx" class="btn btn-outline btn-xl page-scroll">Como funciona?</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-7 hidden-sm no-mobile">
                    <div class="header-content">
                        <div class="header-content-inner">
                            <img src="img/press-1.png" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <section id="features" class="features">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <div class="section-heading">
                        <h2>Use o <strong>WebRebate</strong> em 4 passos</h2>
                        <hr>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-7 no-mobile text-center">
                    <img src="img/img-dashboard.png" class="shadow no-mobile" />
                    <div class="row">
                        <div class="col-sm-12 paddingtop-bottom btn-padding">
                            <a href="#participar" class="btn btn-outline btn-xl page-scroll second-color">Participar</a>
                        </div>
                    </div>
                </div>
                <div class="col-md-5 col-sm-12">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="feature-item">
                                    <div class="round-badge">
                                        <span>1</span>
                                    </div>
                                    <p class="text-muted">Ready to use HTML/CSS device mockups, no Photoshop required!</p>
                                </div>
                            </div>
                            <div class="col-md-6 text-center">
                                <div class="feature-item">
                                    <div class="round-badge">
                                        <span class="badge-number">2</span>
                                    </div>
                                    <p class="text-muted">Ready to use HTML/CSS device mockups, no Photoshop required!</p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 text-center">
                                <div class="feature-item">
                                    <div class="round-badge">
                                        <span class="badge-number">3</span>
                                    </div>
                                    <p class="text-muted">Ready to use HTML/CSS device mockups, no Photoshop required!</p>
                                </div>
                            </div>
                            <div class="col-md-6 text-center">
                                <div class="feature-item">
                                    <div class="round-badge">
                                        <span class="badge-number">4</span>
                                    </div>
                                    <p class="text-muted">Ready to use HTML/CSS device mockups, no Photoshop required!</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section id="video" class="video main-green">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <div class="section-heading">
                        <h2>Veja como funciona</h2>
                        <hr>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 col-xs-6 col-lg-12 text-center">
                    <div class="plc-video no-mobile">
                        <iframe src="https://player.vimeo.com/video/228355212?title=0&byline=0&portrait=0" width="840" height="400"
                            frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section id="quemconfia" class="quemconfia">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <div class="section-heading">
                        <h2>Quem confia no <strong>WebRebate</strong></h2>
                        <hr>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12 text-center marginbottom-top">
                    <div class="col-sm-3 std-margin">
                        <img src="img/logo1.png" />
                    </div>
                    <div class="col-sm-3 std-margin">
                        <img src="img/logo2.png" />
                    </div>
                    <div class="col-sm-3 std-margin">
                        <img src="img/logo1.png" />
                    </div>
                    <div class="col-sm-3 std-margin">
                        <img src="img/logo2.png" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12 text-center marginbottom-top">
                    <div class="col-sm-3 std-margin">
                        <img src="img/logo2.png" />
                    </div>
                    <div class="col-sm-3 std-margin">
                        <img src="img/logo1.png" />
                    </div>
                    <div class="col-sm-3 std-margin">
                        <img src="img/logo2.png" />
                    </div>
                    <div class="col-sm-3 std-margin">
                        <img src="img/logo1.png" />
                    </div>
                </div>
            </div>

        </div>
    </section>


</asp:Content>


<asp:Content ID="cphScripts" ContentPlaceHolderID="cphScript" runat="server">

    <script src="/Default.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        Home.init(true);
    </script>


</asp:Content>




