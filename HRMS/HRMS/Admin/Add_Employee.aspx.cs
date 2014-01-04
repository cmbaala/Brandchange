using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS.Admin
{
    public partial class Add_Employee : System.Web.UI.Page
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

                    if(!(string.IsNullOrEmpty(Request.QueryString["e_id"] as string))){

                        string emp_id = Request.QueryString["e_id"];
                         CommonUtility.Employee emp=CommonUtility.CommonUtility.getEmployeeDetails(emp_id);
                         txt_designation.Text = emp.getDesignation();
                         txt_emp_id.Text = emp_id;
                         txt_first_name.Text = emp.getEmployeename();
                         txt_location.Text = emp.getLocation();
                         txt_pan_number.Text = emp.getPan_number();
                         txt_last_name.Text = emp.getLastname();
                         txt_email.Text = emp.getEmail();
                         txt_doj.Text = emp.getDoj().ToString("dd/MM/yyyy");
                       
                    }
                   
                }


            }
            else
            {
                Response.Redirect("~/Default.aspx");
               
            }

        }

        protected void bt_add_Click(object sender, EventArgs e)
        {
            CommonUtility.Employee employee = new CommonUtility.Employee();
            employee.setAccount_Number(txt_account_number.Text);
            employee.setEmail(txt_email.Text);
            employee.setDoj(Convert.ToDateTime(txt_doj.Text));
            employee.setDesignation(txt_designation.Text);
            employee.setActive(chk_active.Checked);
            employee.setEmployeename(txt_first_name.Text);
            employee.setLocation(txt_location.Text);
            employee.setLastname(txt_last_name.Text);
            employee.setPan_number(txt_pan_number.Text);
            employee.setGender(true);



            if ((string.IsNullOrEmpty(Request.QueryString["e_id"])))
            {
                employee.setEmployee_id(txt_emp_id.Text);
               int state= CommonUtility.CommonUtility.insertEmployeeDetails(employee);

            }
            else
            {

                employee.setEmployee_id(Request.QueryString["e_id"]);
                int state=CommonUtility.CommonUtility.updateEmployeeDetails(employee);


            }

        }
    }
}