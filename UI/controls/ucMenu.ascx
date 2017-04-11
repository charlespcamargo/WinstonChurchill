<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMenu.ascx.cs" Inherits="UI.controls.ucMenu" %>


<div class="page-sidebar nav-collapse collapse">
    <!-- BEGIN SIDEBAR MENU -->
    <ul class="page-sidebar-menu">
        <li>
            <!-- BEGIN SIDEBAR TOGGLER BUTTON -->
            <div class="sidebar-toggler hidden-phone"></div>
            <!-- BEGIN SIDEBAR TOGGLER BUTTON -->
        </li>
        <li>
            <!-- BEGIN RESPONSIVE QUICK SEARCH FORM -->
            <div class="sidebar-search">
                <div class="input-box">
                    <a href="javascript:;" class="remove"></a>
                    <input type="text" placeholder="Buscar..." />
                    <input type="button" class="submit" value=" " />
                </div>
            </div>
            <!-- END RESPONSIVE QUICK SEARCH FORM -->
        </li>
        <li class="start ">
            <a href="index.html">
                <i class="icon-home"></i>
                <span class="title">Dashboard</span>
            </a>
        </li>        
        <li>
            <a href="javascript:;">
                <i class="icon-user"></i>
                <span class="title">Usuários</span> 
            </a> 
        </li>
        <li>
            <a href="javascript:;">
                <i class="icon-cogs"></i>
                <span class="title">Configurações</span>
                <span class="arrow"></span>
            </a>
            <ul class="sub-menu">
                <li class="active">
                    <a href="layout_language_bar.html"> <i class="icon-cog"></i> Parâmetros</a>
                </li>
            </ul>
        </li>  
        <li>
            <a href="javascript:;">
                <i class="icon-archive"></i>
                <span class="title">Produtos</span> 
                <span class="arrow"></span>
            </a> 
        </li>
         <li>
            <a href="javascript:;">
                <i class="icon-group"></i>
                <span class="title">Grupos</span>  
            </a> 
        </li>
       
        <li>
            <a href="javascript:;">
                <i class="icon-bar-chart"></i>
                <span class="title">Relatórios</span>
                <span class="arrow "></span>
            </a>
            <ul class="sub-menu">
                <li>
                    <a href="extra_profile.html">User Profile</a>
                </li>
                <li>
                    <a href="extra_lock.html">Lock Screen</a>
                </li>  
            </ul>
        </li>
         <li>
            <a href="javascript:;">
                <i class="icon-dollar"></i>
                <span class="title">Comprador</span> 
            </a> 
        </li>
        
         <li>
            <a href="javascript:;">
                <i class="icon-truck"></i>
                <span class="title">Fornecedor</span> 
            </a> 
        </li>
         <li class="active">
            <a href="javascript:;" >
                <i class="icon-legal"></i>
                <span class="title">Leilões</span> 
                <span class="selected"></span>
            </a> 
        </li>
    </ul>
    <!-- END SIDEBAR MENU -->
</div>
