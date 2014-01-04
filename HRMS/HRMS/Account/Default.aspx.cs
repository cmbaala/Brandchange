using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS.Account
{
    public partial class Default : System.Web.UI.Page
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


                    this.hlnkItem.ImageUrl = ResolveUrl("~/Images/Menu/icon3.png");
                    this.hlnkItem.NavigateUrl = "~/Account/Payslip.aspx?id=" + Request.QueryString["id"] + "&sid=" + Request.QueryString["sid"];
                    this.HyperLink1.NavigateUrl = "~/Account/Payslip.aspx?id=" + Request.QueryString["id"] + "&sid=" + Request.QueryString["sid"];


                }
             }
            else
            {
                Response.Redirect("~/Default.aspx");

            }
        }

        protected void link_changepassword_Click(Object sender, EventArgs e)
        {
            string id=Request.QueryString["id"];
            Response.Redirect("~/Account/ChangePassword.aspx?id="+id + "&sid=" + Request.QueryString["sid"]);
        }
        protected void link_logout_Click(Object sender, EventArgs e)
        {
            
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}