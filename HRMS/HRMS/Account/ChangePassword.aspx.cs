using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS.CommonUtility;

namespace HRMS.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(Session["sid"] as string)) || (string.IsNullOrEmpty(Request.QueryString["sid"] as string)) || (string.IsNullOrEmpty(Request.QueryString["sid"] as string)))
            {
                Response.Redirect("~/Default.aspx");
            }
            else if ((Request.QueryString["sid"].Equals(Session["sid"])))
            {
                if (!IsPostBack)
                {
                    this.HyperLink2.NavigateUrl = "~/Account/Default.aspx?id=" + Request.QueryString["id"] + "&sid=" + Request.QueryString["sid"]; 
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");

            }
        }

        protected void bt_changepassword_Click(object sender, EventArgs e)
        {
            string currentPassword = txt_current_password.Text;
            string newpassword = txt_type_password.Text;
            string emp_id = Request.QueryString["id"].ToLower(); //Session["empid"].ToString().ToLower();
            UserActivity useractivity=new UserActivity();
            int status=useractivity.doChangePassword(emp_id,currentPassword,newpassword);
            if (status == 1)
            {
                errormsg.Text = "Password has been changed";
            }
            else if (status == -1)
            {
                errormsg.Text = "Old password was incorrect";
            }
            else
            {
                errormsg.Text = "Coudn't change Password try again";
            }
        }
    }
}