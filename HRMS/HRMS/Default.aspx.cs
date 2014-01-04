using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS.CommonUtility;
using System.Web.Security;

namespace HRMS
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           /* if (Session["empid"] != null)
            {
               Response.Redirect("~/Account/");
            }
            */
        }

        protected void bt_loginclick(object sender, EventArgs e)
        {

            String empid= txt_emp_id.Text.ToString().ToLower();
            String password = txt_password.Text.ToString();

            UserActivity useractivity = new UserActivity();
            int usertype = Convert.ToInt32(Select_user_type.Text);
            if (usertype == 2)
            {
                int status = useractivity.doLogin(empid, password);
                if (status == 1)
                {

                    // Session["empid"] = empid;
                    string sid = CommonUtility.CommonUtility.GeneratePassword(18);
                    Session["sid"] = sid;
                    Response.Redirect("~/Account/Default.aspx?id=" + empid + "&sid=" + sid);
                }
                else if (status == -1)
                {
                    //Response.Redirect("~/Default.aspx");
                    errormsglabel.Text = "Invalid User Id";
                }
                else
                {
                    //Response.Redirect("~/Default.aspx");
                    errormsglabel.Text = "Invalid Password";
                }
            }else if(usertype==1){
                if (FormsAuthentication.Authenticate(empid, password))
                {

                    string sid = CommonUtility.CommonUtility.GeneratePassword(18);
                    Session["sid"] = sid;
                    Response.Redirect("~/Admin/Default.aspx?id=" + empid + "&sid=" + sid);

                }
                else
                {
                    //Response.Redirect("~/Default.aspx");
                    errormsglabel.Text = "Invalid User/Password";
                }
               
            
            }

        }

       
    }
}