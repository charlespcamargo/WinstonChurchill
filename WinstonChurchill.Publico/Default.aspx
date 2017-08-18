<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WinstonChurchill.Publico.Default" %>


<!DOCTYPE html>

<html lang="en">

<head runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>WebRebate</title>
    <link href="lib/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Lato" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Catamaran:100,200,300,400,500,600,700,800,900" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Muli" rel="stylesheet">
    <link href="css/webrebate.css" rel="stylesheet">
    <link href="css/main.css" rel="stylesheet">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>

<body id="page-top">

    <form id="form1" runat="server">

        <nav id="mainNav" class="navbar navbar-default navbar-custom navbar-fixed-top">
            <div class="container">
                <div class="navbar-header page-scroll">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                        <span class="sr-only">Toggle navigation</span> Menu <i class="fa fa-bars"></i>
                    </button>
                    <a class="navbar-brand page-scroll" href="#page-top">WebRebate</a>
                </div>

                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav navbar-right">
                        <li class="hidden">
                            <a href="#page-top"></a>
                        </li>
                        <li>
                            <a class="page-scroll" href="#home">Home</a>
                        </li>
                        <li>
                            <a class="page-scroll" href="#">O que fazemos?</a>
                        </li>
                        <li>
                            <a class="page-scroll" id="lnkParticipar" href="#">Cadastre-se</a>
                        </li>
                        <li>
                            <a class="page-scroll" href="#quemconfia">Quem confia</a>
                        </li>
                        <li>
                            <a class="page-scroll" id="lnkInstitucional" href="#institucional">Institucional</a>
                        </li>
                        <li>
                            <a class="page-scroll" id="lnkContato" href="#contato">Contato</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <header>
            <div id="header" class="container">
                <div class="row">
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
                                        <a href="#participar" id="lnkParticipar" class="btn btn-outline btn-xl page-scroll second-color">Participar</a>
                                    </div>
                                    <div class="col-sm-6 col-md-6 col-lg-6 btn-padding">
                                        <a href="#download" class="btn btn-outline btn-xl page-scroll">Como funciona?</a>
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
                            <span>Video</span>
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

    </form>

    <!--footer-->
    <footer class="main-green">
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-6 footerleft ">
                    <div class="logofooter">
                        <img src="img/logo.png" />
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 paddingtop-bottom">
                    <h6 class="heading7">ENDEREÇO</h6>
                    <div class="post">
                        <p><i class="fa fa-map-pin"></i>Rua Lorem Ipsum, 99, São Paulo</p>
                        <p><i class="fa fa-phone"></i>Telefone : +11 9999 878 398</p>
                        <p><i class="fa fa-envelope"></i>E-mail : info@webrebate.com</p>
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 paddingtop-bottom">
                    <h6 class="heading7">LINKS</h6>
                    <ul class="footer-ul">
                        <li><a href="#" id="lnkHome">Home</a></li>
                        <li><a href="#" id="lnkFazemos">O que fazemos?</a></li>
                        <li><a href="#" id="lnkCadastre">Cadastre-se</a></li>
                        <li><a href="#" id="lnkConfia">Quem confia</a></li>
                        <li><a href="#" id="lnkInstitucional">Institucional</a></li>
                        <li><a href="#" id="lnkContato">Contato</a></li>
                    </ul>
                </div>

                <div class="col-md-3 col-sm-6 paddingtop-bottom">
                    <div class="fb-page" data-href="https://www.facebook.com/facebook" data-tabs="timeline" data-height="300" data-small-header="false"
                        style="margin-bottom: 15px;" data-adapt-container-width="true" data-hide-cover="false" data-show-facepile="true">
                        <div class="fb-xfbml-parse-ignore">
                            <blockquote cite="https://www.facebook.com/facebook"><a href="https://www.facebook.com/facebook">Facebook</a></blockquote>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </footer>
    <!--footer-->

    <div class="copyright main-green2">
        <div class="container">
            <div class="col-lg-12">
                <p>© 2017 - WebRebate. Todos os direitos reservados.</p>
            </div>
        </div>
    </div>

    <!-- jQuery -->
    <script src="lib/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="lib/bootstrap/js/bootstrap.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.3/jquery.easing.min.js"></script>

    <!-- Theme JavaScript -->
    <script src="js/webrebate.js"></script>

    <script src="Default.js"></script>

    <script type="text/javascript">
        Home.init();
    </script>

</body>

</html>
