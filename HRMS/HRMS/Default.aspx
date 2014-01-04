<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HRMS._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
    <div style="width:500px;margin:90px auto ;" class="form-horizontal" id="loginform">
     <fieldset>
     <legend>Employee's Login</legend>
    <div class="control-group">
    <asp:Label ID="lbl_emp_id" class="control-label" for="txt_emp_id" runat="server" Text="Employee Id"></asp:Label>
        
      <div class="controls">
          <div class="input-prepend">
          <span class="add-on"><i class="icon-user"></i></span>
        <asp:TextBox ID="txt_emp_id" runat="server" required></asp:TextBox>
          </div>
          </div>
        </div>
        <div class="control-group">
        <asp:Label ID="lbl_password" class="control-label" for="txt_password"  runat="server" Text="Password"></asp:Label>
            <div class="controls">
                <div class="input-prepend">
       <span class="add-on"><i class="icon-lock"></i></span>
      <asp:TextBox ID="txt_password" runat="server" TextMode="Password" required></asp:TextBox>
                </div>
                </div>
      </div>
         <div class="control-group">
        <asp:Label ID="Label2" class="control-label" for="txt_usertype"  runat="server" Text="User Type"></asp:Label>
            <div class="controls">
                <div class="input-prepend">
       <span class="add-on"><i class="icon-user"></i></span>
      
                    <asp:DropDownList ID="Select_user_type" runat="server">
                        <asp:ListItem Value="1" Text="Admin"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Employee"></asp:ListItem>

                    </asp:DropDownList>
                </div>
                </div>
      </div>
        <div class="control-group">
            <div class="controls">
                <label style="padding-bottom:5px;">
                    <asp:LinkButton  runat="server"   Text='Forget Password' OnClientClick="return forgetpassword();" ></asp:LinkButton>
        <%--<asp:HyperLink ID="HyperLink1" runat="server">Forget Password</asp:HyperLink>--%>
                    </label>
        <asp:Button ID="bt_login" runat="server" Text="Login"  class="btn btn-primary" OnClick="bt_loginclick" data-loading-text="Loading..." />

        <asp:Button ID="bt_signup" runat="server" Text="Sign Up" class="btn btn-primary" />
                </div>
             </div>
       
      </fieldset>
        <asp:Panel ID="error_panel_default" runat="server" CssClass="notifymsg" style="text-align:center;color:red;font-size:14px;">
        <asp:Label ID="errormsglabel" runat="server" Text=""></asp:Label>
       </asp:Panel>

    </div>

       

         <div style="width:500px;margin:90px auto;"  class="form-horizontal hide" id="signupform" >
     <fieldset>
         
     <legend id="lbl_legend">Sign Up Here</legend>
    <div class="control-group">
    <asp:Label ID="Label1" class="control-label " for="TextBox1" runat="server" Text="Employee ID"></asp:Label>
        
      <div class="controls" >
        <asp:TextBox ID="txt_signup_empid" runat="server" placeholder="Please enter your employee ID "></asp:TextBox>
          </div>
        </div>
<%--         <div class="control-group">
       <asp:Label ID="Label2" class="control-label " for="TextBox1" runat="server" Text="Company Email"></asp:Label>
        
      <div class="controls">
        <asp:TextBox ID="txt_signup_email" runat="server" placeholder="Please enter your company mail "></asp:TextBox>
          </div>
        </div>       --%>
        <div class="control-group" id="div_signupbtn">
            <div class="controls">
               
             <asp:Button ID="bt_signupform" runat="server" Text="Sign Up"  OnClientClick="bt_signupform_Click()" class="btn btn-success"  />

                 <asp:Button runat="server"  ID="bt_signup_back" OnClientClick="back();" Text="Back" class="btn btn-primary" />  
                </div>
             </div>
       
         <div id="div_forgetpassword" class="control-group">

              <div class="controls">
              <asp:Button ID="bt_forgetpassword" Text="Forget Password" runat="server" OnClientClick="bt_forgetpassword_clientClick()" class="btn btn-primary"  />
               <asp:Button runat="server"  ID="bt_forget_back" Text="Back" OnClientClick="back();" class="btn btn-primary" />      
              </div>
         </div>
      </fieldset>
    </div>
    </div>
<script>
    url = '<%=ResolveUrl("~/Services/hrms.svc")%>';
   /* $.ajax({
        type: "GET",
        url: url,
       success: function (data)
        {
            alert('sucess :'+data);
        },
        error: function () {
            alert('error');
        },
        complete: function (XMLHttpRequest, textStatus, finished) {
          
            alert(finished+" -"+textStatus);
            alert('complete');
        },
        beforeSend: function (XMLHttpRequest) {
           
            alert('before send');
        }
    });
    */

    //var atpostion = "murugesan.balasubramaniyan@kaspontech.com".indexOf('@');
    //var dotpostiion = "murugesan.balasubramaniyan@kaspontech.com".lastIndexOf('.');
    //alert("result :" + "murugesan.balasubramaniyan@kaspontech.com".substring(atpostion + 1, dotpostiion));

    $('#<%=bt_signup.ClientID%>').bind('click', function () {
     
        $('#loginform').hide();
        $("#signupform").show();
        $("#lbl_legend").text("Sign Up Here");
        $("#div_forgetpassword").hide();
        $("#div_signupbtn").show();

        return false;

    });

    function back() {
        $('#loginform').show();
        $("#signupform").hide();
        $("#lbl_legend").text("Sign Up Here");
        $("#div_forgetpassword").hide();
        $("#div_signupbtn").hide();
        return false;
    }

    function bt_forgetpassword_clientClick() {

        validate("forget");
        return false;

    }
    function bt_signupform_Click() {
        validate("signup");
        return false;
    }
    
    function forgetpassword() {
        $('#loginform').hide();
        $("#signupform").show();
        $("#lbl_legend").text("Forget Password");
        $("#div_forgetpassword").show();
        $("#div_signupbtn").hide();
        return false;
    }
   
    
   


    function validate(clicktype) {
        
        var empid = $('#<%=txt_signup_empid.ClientID%>').val().toLowerCase();
        var email = clicktype;
        if (empid.length < 6) {
            alert("Please enter correct employee id");
        }          
        else {

            $.blockUI({
                message: '<p>Please Wait ...</p>'
            });
            //data.login = login;

            $.ajax({
                type: "POST",
                url: url + "/signup",
                data: JSON.stringify({ login: { "emp_id": empid, "email": email } }),
                contentType: 'application/json',
                success: function (data) {
                    alert(data);
                    window.location = "";
                    $.unblockUI();
                },
                error: function () {
                    //alert('error');
                    $.unblockUI();

                },
                complete: function (XMLHttpRequest, textStatus, finished) {

                    //alert(finished + " -" + textStatus);
                    //alert('complete');

                },
                beforeSend: function (XMLHttpRequest) {



                }
            });


        }
        
    }

  //  $(document).ajaxStart($.blockUI({ message: "Please Wait .." })).ajaxStop($.unblockUI);

    function validateMail(mailString) {

        var atpostion = mailString.indexOf('@');
        var dotpostiion = mailString.lastIndexOf('.');
        if (mailString.substring(atpostion + 1, dotpostiion) == "kaspontech") {
            return true;
        } else {
            return false;
        }
    }

    $(document).ready(function () {
      
        
    });
</script>
</asp:Content>
