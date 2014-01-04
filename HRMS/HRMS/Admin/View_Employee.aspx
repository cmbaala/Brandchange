<%@ Page Title="View Employee" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View_Employee.aspx.cs" Inherits="HRMS.Admin.View_Employee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <ul class="breadcrumb">
     <li> <asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl='~/Admin/Default.aspx' Text='Home' /> <span class="divider">/</span></li>
    <li class="active">View Employee</li>
   
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div class="content">
    
    <asp:GridView ID="GridView_emp" runat="server" AutoGenerateColumns="false"   AllowSorting="true" Width="100%"
                        AllowPaging="true"  PageSize="20" OnRowCommand="GetDetails_RowCommand" >
        <Columns>
            <asp:BoundField HeaderText="Employee Id" 
            DataField="Emp_Id" ReadOnly="true" />
            <asp:BoundField HeaderText="First Name" 
            DataField="First Name" ReadOnly="true" />
            <asp:BoundField HeaderText="Designation" 
            DataField="Designation" ReadOnly="true" />
            <asp:BoundField HeaderText="Email Id" 
            DataField="Email" ReadOnly="true" />
             <asp:buttonfield buttontype="Link" 
            commandname="Select"
            headertext="Action" 
            text="View"/>

            </Columns>

    </asp:GridView>
         </div>
         
</asp:Content>
