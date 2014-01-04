<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="HRMS.Account.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <ul class="breadcrumb" style="margin-bottom:0px;">
    <li class="active" >Home</li>
   
    </ul>
       <div id="Div1" class="btn-group" style="margin-top:-25px;display:inline-block;padding-right:20px;float:right;" runat="server">
    <a  class="btn dropdown-toggle" data-toggle="dropdown" href="#">    
    <%= Request.QueryString["id"] %>
    <span class="caret"></span>
    </a> 
    <ul id="Ul1" class="dropdown-menu" runat="server">
       
    <li id="Li1" runat="server" > <asp:LinkButton ID="link_changepassword"  runat="server"  Text='Change Password'  OnClick="link_changepassword_Click" ></asp:LinkButton> </li>
    <li id="Li2" runat="server"> <asp:LinkButton ID="link_logout" OnClick="link_logout_Click" runat="server"   Text='Logout' ></asp:LinkButton></li>
    </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

     
   
<div class="content">
        
            <ul class="menulist">
           <li>
                <asp:HyperLink runat="server" ID="hlnkItem" ImageUrl="../Images/Menu/icon3.png"  NavigateUrl='~/Account/Payslip.aspx' />
                <center>
                    <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl='~/Account/Payslip.aspx' Text='Payslip' />
                </center>
            </li>
                 <%--<li>
                <asp:HyperLink runat="server" ID="HyperLink2" ImageUrl="../Images/Menu/icon3.png" NavigateUrl='~/Account/Payslip.aspx' />
                <center>
                    <asp:HyperLink runat="server" ID="HyperLink3" NavigateUrl='~/Account/Payslip.aspx' Text='First Part Come Here' />
                </center>
            </li>
                 <li>
                <asp:HyperLink runat="server" ID="HyperLink4" ImageUrl="../Images/Menu/icon3.png" NavigateUrl='~/Account/Payslip.aspx' />
                <center>
                    <asp:HyperLink runat="server" ID="HyperLink5" NavigateUrl='~/Account/Payslip.aspx' Text='First Part Come Here' />
                </center>
            </li>
                 <li>
                <asp:HyperLink runat="server" ID="HyperLink6" ImageUrl="../Images/Menu/icon3.png" NavigateUrl='~/Account/Payslip.aspx' />
                <center>
                    <asp:HyperLink runat="server" ID="HyperLink7" NavigateUrl='~/Account/Payslip.aspx' Text='First Part Come Here' />
                </center>
            </li>
                 <li>
                <asp:HyperLink runat="server" ID="HyperLink8" ImageUrl="../Images/Menu/icon3.png" NavigateUrl='~/Account/Payslip.aspx' />
                <center>
                    <asp:HyperLink runat="server" ID="HyperLink9" NavigateUrl='~/Account/Payslip.aspx' Text='First Part Come Here' />
                </center>
            </li>
        --%>
            </ul>
          

</div>
    <script>
       
    </script>
</asp:Content>
