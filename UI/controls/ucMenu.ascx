<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMenu.ascx.cs" Inherits="UI.controls.ucMenu" %>

<script src="/controls/ucMenu.js"></script>

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
    </ul>
    <!-- END SIDEBAR MENU -->
</div>

 