<%@ Page Title="ChangePassword" Language="C#" MasterPageFile="../Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="HRMS.Account.ChangePassword" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
        <ul class="breadcrumb">
     <li> <asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl='T' Text='Home' /> <span class="divider">/</span></li>
    <li class="active">ChangePassword</li>
   
    </ul>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div style="width:500px;margin:90px auto" id="div_changepasswordform">

    <asp:Panel ID="ChangePassword_Panel"  runat="server">

    <asp:Panel ID="panel_currentpassword" runat="server" class="control-group" >

    <asp:Label ID="Label1" class="control-label " for="txt_current_password" runat="server" Text="Old Password"></asp:Label>
        
     <asp:Panel ID="Panel1" runat="server" class="controls">
        <asp:TextBox ID="txt_current_password" runat="server" TextMode="Password" placeholder="Please type your current password"></asp:TextBox>
          </asp:Panel>
        </asp:Panel>

      <asp:Panel ID="Panel4" runat="server" class="control-group">

       <asp:Label ID="Label2" class="control-label " for="txt_type_password" runat="server" Text="New Password"></asp:Label>
        
       <asp:Panel ID="Panel3" runat="server" class="controls">

        <asp:TextBox ID="txt_type_password" runat="server" TextMode="Password" placeholder="Please type the new password "></asp:TextBox>
         </asp:Panel>
       </asp:Panel>
         <asp:Panel ID="Panel2" runat="server" class="control-group">

       <asp:Label ID="Label3" class="control-label " for="txt_retype_password" runat="server" Text="Confirm Password"></asp:Label>
        
       <asp:Panel ID="Panel5" runat="server" class="controls">

        <asp:TextBox ID="txt_retype_password" runat="server" TextMode="Password" placeholder="Please retype the new password"></asp:TextBox>
         </asp:Panel>
       </asp:Panel>
        <asp:Panel ID="panel_button" runat="server" class="control-group">
       <asp:Panel  runat="server" class="controls">
        <asp:Button ID="bt_changepassword" runat="server" Text="Change Password"  class="btn btn-primary"  data-loading-text="Loading..."
            OnClientClick="return validate();" OnClick="bt_changepassword_Click"  />


        </asp:Panel>
            </asp:Panel>

        <asp:Panel ID="ErrorPanel" runat="server" style="text-align:center;color:red;font-size:14px;">
            <asp:Label ID="errormsg" class="control-label "  runat="server" Text=""></asp:Label>
        </asp:Panel>

    </asp:Panel>
       
    </div>
    <script type="text/javascript">
        function validate() {
            if (($("#<%=txt_current_password.ClientID %>").val().trim().length < 1)||$("#<%=txt_type_password.ClientID %>").val().length < 1||$("#<%=txt_retype_password.ClientID %>").val().length < 1) {
                $("#<%=errormsg.ClientID %>").text("Please enter appropriate values");
                return false;

            } else if ($("#<%=txt_type_password.ClientID %>").val()!= $("#<%=txt_retype_password.ClientID %>").val()) {

                $("#<%=errormsg.ClientID %>").text("Password doesn't match");
                return false;
            } else {
                return true;
            }

        }

    </script>

</asp:Content>