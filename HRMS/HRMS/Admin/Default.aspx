<%@ Page Title="Admin Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HRMS.Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <ul class="breadcrumb">
    <li class="active">Admin Home</li>
   
    </ul>
    <div id="Div1" class="btn-group" style="margin-top:-25px;display:inline-block;padding-right:20px;float:right;" runat="server">
    <a  class="btn dropdown-toggle" data-toggle="dropdown" href="#">    
    <%= Request.QueryString["id"] %>
    <span class="caret"></span>
    </a> 
    <ul id="Ul1" class="dropdown-menu" runat="server">
       
    <%--<li id="Li1" runat="server" > <asp:LinkButton ID="link_changepassword"  runat="server"  Text='Change Password'  OnClick="link_changepassword_Click" ></asp:LinkButton> </li>--%>
    <li id="Li2" runat="server"> <asp:LinkButton ID="link_logout" OnClick="link_logout_Click" runat="server"   Text='Logout' ></asp:LinkButton></li>
    </ul>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
    
            <ul class="menulist">
           <li>
                <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="../Images/Menu/icon3.png" NavigateUrl='~/Admin/Add_Employee.aspx' />
                <center>
                    <asp:HyperLink ID="HyperLink2" runat="server"  NavigateUrl='~/Admin/Add_Employee.aspx' Text='Add Employee' />
                </center>
            </li>
                 <li>
                <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="../Images/Menu/icon3.png" NavigateUrl='~/Admin/View_Employee.aspx' />
                <center>
                    <asp:HyperLink ID="HyperLink4" runat="server"  NavigateUrl='~/Admin/View_Employee.aspx' Text='View/Update Employee' />
                </center>
            </li>
                <%-- <li>
                <asp:HyperLink runat="server"  ImageUrl="../Images/Menu/icon3.png" NavigateUrl='~/Admin/Add_Employee.aspx' />
                <center>
                    <asp:HyperLink runat="server"  NavigateUrl='~/Admin/Add_Employee.aspx' Text='First Part Come Here' />
                </center>
            </li>
                 <li>
                <asp:HyperLink runat="server"  ImageUrl="../Images/Menu/icon3.png" NavigateUrl='~/Admin/Add_Employee.aspx' />
                <center>
                    <asp:HyperLink runat="server"  NavigateUrl='~/Admin/Add_Employee.aspx' Text='First Part Come Here' />
                </center>
            </li>
                 <li>
                <asp:HyperLink runat="server" ImageUrl="../Images/Menu/icon3.png" NavigateUrl='~/Admin/Add_Employee.aspx' />
                <center>
                    <asp:HyperLink runat="server" NavigateUrl='~/Admin/Add_Employee.aspx' Text='First Part Come Here' />
                </center>
            </li>--%>
        
            </ul>
          

</div>
</asp:Content>
