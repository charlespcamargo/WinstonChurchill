<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Sobre.aspx.cs" Inherits="WinstonChurchill.Publico.Sobre" %>
 
<asp:Content ID="cphHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="cphConteudo" ContentPlaceHolderID="cphConteudo" runat="server">

        <header class="page-interna header-sobre">
            <div id="header" class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="header-content">
                            <div id="header-content" class="header-content-interna">
                                <h1 class="text-title">| O que fazemos?</h1>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <section class="default-content">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <h1 class="text-title">Sobre o Web Rebate</h1>
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et
                        dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip
                        ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore
                        eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia
                        deserunt mollit anim id est laborum.
                        </p>
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et
                        dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip
                        ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore
                        eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia
                        deserunt mollit anim id est laborum.
                        </p>
                    </div>
                </div>
            </div>
        </section>

        <section class="default-content">
            <div class="container">
                <div class="row">
                    <div class="col-md-7 no-mobile text-center">
                        <img src="img/img-dashboard.png" class="shadow no-mobile" />
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
     
</asp:Content>


<asp:Content ID="cphScripts" ContentPlaceHolderID="cphScript" runat="server">

    <script src="/Default.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        Home.init();
    </script>


</asp:Content>