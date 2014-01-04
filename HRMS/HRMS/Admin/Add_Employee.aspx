<%@ Page Title="Add Employee" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add_Employee.aspx.cs" Inherits="HRMS.Admin.Add_Employee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
       <ul class="breadcrumb">
     <li> <asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl='~/Admin/Default.aspx' Text='Home' /> <span class="divider">/</span></li>
    <li class="active">Add Employee</li>
   
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
    <div class="form-horizontal formcontent" id="signup" >
        
		<div class="control-group">
           <asp:Label ID="Label1" runat="server"  class="control-label"><span class="star">*</span>Employee Id</asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				<span class="add-on"><i class="icon-user"></i></span>
                    <asp:TextBox ID="txt_emp_id" runat="server" class="input-xlarge" placeholder="Employee Id"></asp:TextBox>
					</div>
			</div>
		</div>
        <div class="control-group">
            <asp:Label ID="Label2" runat="server" class="control-label"><span class="star">*</span>First Name</asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				<span class="add-on"><i class="icon-user"></i></span>
                    <asp:TextBox ID="txt_first_name" runat="server" class="input-xlarge" placeholder="First Name"></asp:TextBox>
					</div>
			</div>
		</div>
        <div class="control-group">
            <asp:Label ID="Label3" runat="server" class="control-label"><span class="star">*</span>Last Name</asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				<span class="add-on"><i class="icon-user"></i></span>
                    <asp:TextBox ID="txt_last_name" runat="server" class="input-xlarge" placeholder="Last Name"></asp:TextBox>
					</div>
			</div>
		</div>
         <div class="control-group">
            <asp:Label ID="Label12" runat="server" class="control-label"><span class="star">*</span>Email</asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				<span class="add-on"><i class="icon-user"></i></span>
                    <asp:TextBox ID="txt_email" runat="server" class="input-xlarge" placeholder="Email"></asp:TextBox>
					</div>
			</div>
		</div>
        <div class="control-group">
            <asp:Label ID="Label4" runat="server"  class="control-label"><span class="star">*</span>Location</asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				<span class="add-on"><i class="icon-info-sign"></i></span>
                    <asp:TextBox ID="txt_location" runat="server" class="input-xlarge" placeholder="Location"></asp:TextBox>
					</div>
			</div>
		</div>
       
        <div class="control-group">
            <asp:Label ID="Label10" runat="server"  class="control-label"><span class="star">*</span>DOJ</asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				<span class="add-on"><i class="icon-calendar"></i></span>
                    <asp:TextBox ID="txt_doj" runat="server" class="input-xlarge datepicker" ></asp:TextBox>
					</div>
			</div>
		</div>
        <div class="control-group">
            <asp:Label ID="Label5" runat="server" Text="Pan Number" class="control-label"></asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				<span class="add-on"><i class="icon-info-sign"></i></span>
                    <asp:TextBox ID="txt_pan_number" runat="server" class="input-xlarge" placeholder="Pan Number"></asp:TextBox>
					</div>
			</div>
		</div>
        <div class="control-group">
            <asp:Label ID="Label6" runat="server"  class="control-label"><span class="star">*</span>Designation</asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				<span class="add-on"><i class="icon-info-sign"></i></span>
                    <asp:TextBox ID="txt_designation" runat="server" class="input-xlarge" placeholder="Designation"></asp:TextBox>
					</div>
			</div>
		</div>
        <div class="control-group">
            <asp:Label ID="Label7" runat="server"  class="control-label"><span class="star">*</span>Gender</asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				
                    <label class="radio inline">
                    <asp:RadioButton ID="RadioButton1" GroupName="gender" runat="server" Text="Male" Checked="true"/> 
                        </label>
                    <label class="radio inline">
                    <asp:RadioButton ID="RadioButton2" runat="server" name="gender"  Text="Female" GroupName="gender" /> 
                        </label>
					</div>
                
			</div>
		</div>

	    <div class="control-group">
            <asp:Label ID="Label8" runat="server" Text="Bank Name" class="control-label"></asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				<span class="add-on"><i class="icon-info-sign"></i></span>
                    <asp:TextBox ID="txt_bank" runat="server" class="input-xlarge" placeholder="Bank Name"></asp:TextBox>
					</div>
			</div>
		</div>
        <div class="control-group">
            <asp:Label ID="Label9" runat="server" Text="Account Number" class="control-label"></asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				<span class="add-on"><i class="icon-info-sign"></i></span>
                    <asp:TextBox ID="txt_account_number" runat="server" class="input-xlarge" placeholder="Account Number"></asp:TextBox>
					</div>
			</div>
		</div>

        <div class="control-group">
            <asp:Label ID="Label11" runat="server" Text="Active" class="control-label"><span class="star">*</span>Active</asp:Label>
	    
			<div class="controls">
			    <div class="input-prepend">
				
                   <label class="checkbox inline">
                       <asp:CheckBox ID="chk_active" runat="server" Text="Active" Checked="true" />

                   </label>
					</div>
                
			</div>
		</div>

		<div class="control-group">
		<label class="control-label"></label>
	      <div class="controls">
              <asp:Button ID="bt_add" runat="server" Text="Add" class="btn btn-success" OnClick="bt_add_Click" />
	     

	      </div>

	</div>

	  
        
        </div>
        </div>
    <script>
        $(function () {
            $("#<%=txt_doj.ClientID%>").datepicker({
                
            });
        });
	</script>
</asp:Content>
