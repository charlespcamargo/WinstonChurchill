<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Contato.aspx.cs" Inherits="WinstonChurchill.Publico.Contato" %>

<asp:Content ID="cphHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="cphConteudo" ContentPlaceHolderID="cphConteudo" runat="server">

    <header class="page-interna header-sobre">
        <div id="header" class="container">
            <div class="row">
                <div class="col-sm-12">
                    <div class="header-content">
                        <div id="header-content" class="header-content-interna">
                            <h1 class="text-title">| Contato</h1>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <section id="contact" class="default-content content-section text-center">
        <div class="contact-section">
            <div class="container">
                <h1 class="text-title text-uppercase">Envie-nos sua mensagem</h2>
                    </h2>
                    <div class="row">
                        <div class="col-md-8 col-md-offset-2">
                            <form class="form-horizontal">
                                <div class="form-group">
                                    <label for="exampleInputName2">Nome</label>
                                    <input type="text" class="form-control" id="exampleInputName2" placeholder="Seu Nome">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail2">E-mail</label>
                                    <input type="email" class="form-control" id="exampleInputEmail2" placeholder="seu.nome@exemplo.com">
                                </div>
                                <div class="form-group ">
                                    <label for="exampleInputText">Sua Mensagem</label>
                                    <textarea class="form-control" placeholder="Sua mensagem"></textarea>
                                </div>
                                <button type="submit" class="btn btn-outline second-color">Enviar Mensagem</button>
                            </form>
                        </div>
                    </div>
            </div>
        </div>
    </section>

</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="cphScript" runat="server">

    <script src="/Default.js?v=<%=new Random().Next(0,10000)%>"></script>

    <script type="text/javascript">
        Home.init();
    </script>


</asp:Content>
