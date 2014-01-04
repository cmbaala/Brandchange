<%@ Page Title="ViewPayslip" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Payslip.aspx.cs" Inherits="HRMS.Account.Payslip" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
      <ul class="breadcrumb">
     <li> <asp:HyperLink runat="server" ID="linkpay" NavigateUrl='/' Text='Home' /> <span class="divider">/</span></li>
    <li class="active">Payslip</li>
   
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
<div class="forminpayslip">
 <div class="control-group">
    <asp:Label ID="lbl_emp_idmonth" class="control-label" for="selectmonth" runat="server" Text="Select Month" ></asp:Label>
        
      <div class="controls">
          <asp:DropDownList ID="selectmonth" class="span2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="selectmonth_SelectedIndexChanged" >
           
          </asp:DropDownList>
            <asp:Button ID="bt_downlopdf" runat="server" Text="DownloadPdf" CssClass="downloadbt btn btn-primary" OnClick="bt_downlopdf_Click" />
          </div>
        
        </div>
    </div>
        <div class="paycontent" runat="server" ID="pay_panel" style="width:700px;">
          Please select month to view your payslip
        </div>
  <%--<asp:Panel ID="pay_panel" runat="server" class="paycontent">
 


  </asp:Panel>--%>
  
</div>
    <script type="text/javascript">
        url = '<%=ResolveUrl("~/Services/hrms.svc")%>';

        function selectmonth_Change() {

            var selectedValue = $("#<%=selectmonth.ClientID%>").val();
            var emp_id = getParameterByName("id");
           
            if (selectedValue != "-1") {
                var arr = selectedValue.split('-');
                $.blockUI({
                    message: '<p>Please Wait ...</p>'
                });
                $.ajax({
                    type: "POST",
                    url: url + "/getPayslip",
                    data: JSON.stringify({ "emp_id": emp_id, "month": arr[0], "year": arr[1] }),
                    contentType: 'application/json',
                    success: function (data) {
                       $("#pay_panel").empty();
                        $("#pay_panel").append(data);

                    },
                    error: function () {
                        console.log("with in error method");


                    },
                    complete: function (XMLHttpRequest, textStatus, finished) {


                        //alert('complete');
                        $.unblockUI();
                        console.log("with in complete method");

                    },
                    beforeSend: function (XMLHttpRequest) {



                    }
                });
            } else {
                $("#pay_panel").empty();
            }
        }
      
    </script>
</asp:Content>
