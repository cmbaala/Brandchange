using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS.Admin
{
    public partial class View_Employee : System.Web.UI.Page
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

                    this.HyperLink1.NavigateUrl = "~/Admin/Default.aspx?id=" + Request.QueryString["id"] + "&sid=" + Request.QueryString["sid"];
                    DataTable dt = CommonUtility.CommonUtility.getAllEmployeeDetails();
                    GridView_emp.DataSource = dt;
                    GridView_emp.DataBind();
                    

                }


            }
            else
            {
                Response.Redirect("~/Default.aspx");

            }

        }
       protected void GetDetails_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.
                GridViewRow selectedRow = GridView_emp.Rows[index];
                TableCell contactName = selectedRow.Cells[0];
                string emp_id = contactName.Text;
                Response.Redirect("~/Admin/Add_Employee.aspx?id=" + Request.QueryString["id"] + "&sid=" + Request.QueryString["sid"]+"&e_id="+emp_id);
                // Display the selected author.
               // Message.Text = "You selected " + contact + ".";

            }
        
        }
    }
}