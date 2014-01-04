using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace HRMS.Account
{
    public partial class Payslip : System.Web.UI.Page
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
                     
                     this.linkpay.NavigateUrl = "~/Account/Default.aspx?id=" + Request.QueryString["id"] + "&sid=" + Request.QueryString["sid"];

                     poppulateMonthValues();
                 }


             }
             else
             {
                 Response.Redirect("~/Default.aspx");
             
             }
        }
        public void poppulateMonthValues()
        {

               string emp_id = Request.QueryString["id"];
                DataTable dt = CommonUtility.CommonUtility.getMonthDetails(emp_id);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                selectmonth.DataSource = null;
                selectmonth.DataSource = ds.Tables[0].DefaultView;
                selectmonth.DataValueField = "Value";
                selectmonth.DataTextField = "Text";
                selectmonth.DataBind();
            
        }

        protected void bt_downlopdf_Click(object sender, EventArgs e)
        {
          

            string text = selectmonth.Text;
            if (!text.Equals("-1"))
            {
                string[] values = text.Split('-');
                int month = Convert.ToInt32(values[0]);
                int year = Convert.ToInt32(values[1]);
                string monthstring = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                string emp_id = Request.QueryString["id"];

                CommonUtility.PdfGeneration.generatePdf("Payslip-"+monthstring + "-" + year + ".pdf", CommonUtility.CommonUtility.getPaySlip(Request.QueryString["id"], month, year), monthstring, year);
            }

        }

        public void createPdf()
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
           // string contents = "<html ><head></head><body><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr ><td colspan=\"2\" ><table id=\"uppertable\"><tr><td><img src=\"http://localhost/hrms/images/logo.png\" class=\"logo\"/><div><div style=\"color:#0062A1;font-weight:bold;\">Kaspon Techworks Private limited</div><div style=\"color:#0062A1;font-size:14px;\">#123, Developed Plot Estate,</div><div style=\"color:#0062A1;font-size:14px;\">Off OMR, Perungudi, Chennai -600096,</div><div style=\"color:#0062A1;font-size:14px;\">Tel : +91-4442125741,42125914.</div><div style=\"color:#0062A1;font-size:14px;\">http://www.kaspontech.com</div></div></td></tr></table></td></tr><tr  height=\"60px\"><td colspan=\"2\"><div class=\"payslipheading\" style=\"background-color:#0062A1;color:black;font-weight:bold;text-align:center;margin:20px auto;\"> Payslip for the month of January 2013</div></td></tr><tr  width=\"100%\" ><td colspan=\"2\"><table class=\"employeeDetailsTable\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr width=\"100%\"><td width=\"20%\">Employee Name</td><td width=\"30%\">: A.Ansari</td><td width=\"20%\">Location</td><td width=\"20%\">: Chennai</td></tr><tr width=\"100%\"><td width=\"20%\">Employee Id</td><td width=\"30%\">: KT0009</td><td width=\"20%\">Pan Number</td><td width=\"20%\">: BBZPA3928A</td></tr><tr width=\"100%\"><td width=\"20%\">Designation</td><td width=\"30%\">: Project Engineer Trainee</td><td width=\"20%\">DOJ</td><td width=\"20%\">: 01/02/2012</td></tr></table></td></tr><tr colspan=\"2\" width=\"100%\" class=\"top row \"><td colspan=\"2\"><div style=\"background-color:#0062A1;color:white;font-weight:bold;\"> Attendance Details</div></td></tr><tr class=\"top row \"><td width=\"20%\" >Total Days</td><td >: 30</td></tr><tr class=\"top row \"><td width=\"20%\" >No. of Days Worked</td><td > : 30</td></tr><tr width=\"100%\" id=\"earnings_Deductions_header\" ><td width=\"50%\" style=\"background-color:#0062A1;font-weight:bold;\"><table id=\"earnings\" style=\"color:#FFF;font-weight:bold;\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"bot\"><tr class=\"top \" width=\"100%\"><td width=\"75%\">Earnings</td><td width=\"25%\">Amount</td></tr></table></td><td width=\"50%\" style=\"background-color:#0062A1;color:white;font-weight:bold;\"><table id=\"deductions\" style=\"color:#FFF;font-weight:bold;\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"right\"><tr class=\"top  \" width=\"100%\"><td  width=\"75%\">Deductions</td><td  width=\"25%\">Amount</td></tr></table></td></tr><tr width=\"100%\" ><td width=\"50%\" ><table  style=\"font-weight:bold;\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"bot\"><tr class=\"top  \"><td width=\"75%\">Basic Salary</td><td><span  >3750.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">HRA</td><td><span >1875.0</span></td></tr><tr class=\"top \"><td width=\"75%\">Telephone Allowance</td><td ><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Dearness Allowance</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Conveyance</td><td><span  >800.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Loyalty Incentives</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Performance incentives</td><td><span  >6400.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Others</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td></td><td></td></tr></table></td><td width=\"50%\" ><table style=\"font-weight:bold;\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"right\"><tr class=\"top  \"><td width=\"75%\">Provident Fund</td><td><span >450.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">ESI Deductions</td><td><span >225.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">TDS</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Gratuity</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Professional tax</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\"></td><td></td></tr><tr class=\"top  \"><td width=\"75%\"></td><td></td></tr><tr class=\"top  \"><td width=\"75%\"></td><td></td></tr><tr class=\"top \"><td></td><td></td></tr></table></td></tr><tr><td colspan=\"2\"><table width=\"100%\" cellspacing=\"0\" class=\"bot right\"><tr style=\"background-color:#999;\" class=\"top  \"><td width=\"37.5%\" >Total Earnings</td><td ><span  >12825.00</span></td><td width=\"37.5%\">Total Deductions</td><td ><span  >675.00</span></td></tr><tr style=\"background-color:#999;\" class=\"top  bottom row\"><td width=\"37.5%\" ></td><td ><span  ></span></td><td width=\"37.5%\" >Net Take Home</td><td ><span  >12150.00</span></td></tr></table></td></tr></table></body></html>";//CommonUtility.CommonUtility.getPayslipDetails("kt0009", 1, 2013);
           // string contents = "<html ><head></head><body><table border=\"1\">  <tr><th>Month</th><th>Savings</th></tr><tr><td>January</td><td>$100</td></tr><tr><td>February</td><td>$100</td></tr><tr><td colspan=\"2\">Sum: $180</td></tr></table></body></html> ";
            //string contents = CommonUtility.CommonUtility.getPayslipDetails("kt0009", 1, 2013);
            //string contents = "<html ><head><title>Home</title></head><body style='line-height:20px;'><div style='width:700px;'><table id='uppertable'><tr><td><img src='http://localhost/hrms/images/logo.png' class='logo'/><div><div style='color:#0062A1;font-weight:bold;'>Kaspon Techworks Private limited</div><div style='color:#0062A1;font-size:14px;'>#123, Developed Plot Estate,</div><div style='color:#0062A1;font-size:14px;'>Off OMR, Perungudi, Chennai -600096,</div><div style='color:#0062A1;font-size:14px;'>Tel : +91-4442125741,42125914.</div><div style='color:#0062A1;font-size:14px;'>http://www.kaspontech.com</div></div></td>"
            //    +"</tr></table><div class='payslipheading' style='background-color:#0062A1;color:white;font-weight:bold;text-align:center;	margin:20px auto;'> PaySlip For The Month of Dec 2012</div>"
            //    +"<table class='employeeDetailsTable' width='100%' cellpadding='0' cellspacing='0'><tr width='100%'><td width='20%'>Employee Name</td><td width='30%'>: Balasubramaniyan</td><td width='20%'>Location</td>"
            //    +"<td width='20%'>: Chennai</td></tr><tr width='100%'><td width='20%'>Employee Id</td><td width='30%'>: KT0008</td><td width='20%'>Pan Number</td><td width='20%'>"
            //    +":</td></tr><tr width='100%'><td width='20%'>Designation</td><td width='30%'>: Project Engineer</td><td width='20%'>DOJ</td><td width='20%'>:01/02/2012"
            //    +"</td></tr></table><div style='background-color:#0062A1;color:white;font-weight:bold;'> Attendance Details</div><table width='100%' cellpadding='0' cellspacing='0'>"
            //    +"<tr class='top row '><td width='20%' style=' border-top: 1px solid #0062A1;border-left:  1px solid #0062A1;' >Total Days</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '>"
            //    +": 31</td></tr><tr class='top row '><td width='20%' style=' border-top: 1px solid #0062A1;border-left:  1px solid #0062A1;'>No. of Days Worked</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '> : 31"
            //    +"</td></tr></table><table width='100%' cellpadding='0' cellspacing='0'><tr width='100%' id='earnings_Deductions_header' ><td width='50%' style='background-color:#0062A1;font-weight:bold;'>"
            //    +"<table id='earnings' style='color:#FFF;font-weight:bold;border-left:  1px solid #0062A1;' cellpadding='0' cellspacing='0' width='100%' class='bot'>"
            //    +"<tr class='top ' width='100%'><td width='75%'>Earnings</td><td width='25%'>Amount</td></tr></table></td><td width='50%' style='background-color:#0062A1;color:white;font-weight:bold;'>"
            //    +"<table id='deductions' style='color:#FFF;font-weight:bold;' cellpadding='0' cellspacing='0' width='100%' class='right'><tr class='top  ' width='100%'>"
            //    +"<td  width='75%' style=' border-top: 1px solid #0062A1;  '>Deductions</td><td  width='25%' style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '>"
            //    +"Amount</td></tr></table></td></tr><tr width='100%' ><td width='50%' style='border-left:  1px solid #0062A1;'><table  style='font-weight:bold;' width='100%' cellpadding='0' cellspacing='0' class='bot'>"
            //    +"<tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Basic Salary</td><td style=' border-top: 1px solid #0062A1;'><span  >3750.00</span></td></tr><tr class='top  '>"
            //    +"<td width='75%' style=' border-top: 1px solid #0062A1;'>HRA</td><td style=' border-top: 1px solid #0062A1;'><span  >1825.00</span></td></tr><tr class='top '><td width='75%' style=' border-top: 1px solid #0062A1;'>"
            //    +"Telephone Allowance</td><td style=' border-top: 1px solid #0062A1;'> <span  >37500.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Dearness Allowance</td><td style=' border-top: 1px solid #0062A1;'><span  >3750.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Conveyance</td><td style=' border-top: 1px solid #0062A1;'><span  >0.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Loyalty Incentives</td><td style=' border-top: 1px solid #0062A1;'><span  >0.00</span></td>"
            //    +"</tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Performance incentives</td><td style=' border-top: 1px solid #0062A1;'><span  >0.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Others"
            //    +"</td><td style=' border-top: 1px solid #0062A1;'><span  >3750.00</span></td></tr><tr class='top  '><td style=' border-top: 1px solid #0062A1;'>&nbsp;</td><td style=' border-top: 1px solid #0062A1;' >&nbsp;</td></tr></table></td><td width='50%' ><table style='font-weight:bold;' width='100%' cellpadding='0' cellspacing='0' class='right'><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Provident Fund</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span >3750.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>ESI Deductions</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span >3750.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>TDS</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span  >3750.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Gratuity</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span  >183.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Professional tax</td>"
            //    +"<td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span  >375000000.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>&nbsp;</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '>&nbsp;</td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>&nbsp;</td>"
            //    +"<td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; ' >&nbsp;</td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>&nbsp;</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; ' >&nbsp;</td>"
            //    +"</tr><tr class='top '><td style=' border-top: 1px solid #0062A1;'>&nbsp;</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; ' >&nbsp;</td></tr>"
            //    +"</table></td></tr></table><table width='100%' cellspacing='0' class='bot right'><tr style='background-color:#999;' class='top  '><td width='37.5%' style=' border-top: 1px solid #0062A1;border-left:  1px solid #0062A1;' >Total Earnings</td><td  style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span  >1095</span></td><td width='37.5%' style=' border-top: 1px solid #0062A1;'>Total Deductions</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span  >1095</span></td></tr><tr style='background-color:#999;' class='top  bottom row'><td width='37.5%' style=' border-top: 1px solid #0062A1; border-bottom:  1px solid #0062A1;border-left:  1px solid #0062A1;' >&nbsp;</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; border-bottom:  1px solid #0062A1 ' ><span  >&nbsp;</span></td><td width='37.5%' style=' border-top: 1px solid #0062A1; border-bottom:  1px solid #0062A1' >Net Take Home</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; border-bottom:  1px solid #0062A1 ' ><span  >1095</span></td></tr></table></div></body></html>";

            string contents = "<body style='line-height:20px;'><div style='width:700px;'><table id='uppertable'><tr><td><img src='http://localhost/hrms/images/logo.png' class='logo'/><div><div style='color:#0062A1;font-weight:bold;'>Kaspon Techworks Private limited</div><div style='color:#0062A1;font-size:14px;'>#123, Developed Plot Estate,</div><div style='color:#0062A1;font-size:14px;'>Off OMR, Perungudi, Chennai -600096,</div><div style='color:#0062A1;font-size:14px;'>Tel : +91-4442125741,42125914.</div><div style='color:#0062A1;font-size:14px;'>http://www.kaspontech.com</div></div></td></tr></table><div class='payslipheading' style='background-color:#0062A1;color:white;font-weight:bold;text-align:center;	margin:20px auto;'> PaySlip For The Month of Dec 2012</div><table class='employeeDetailsTable' width='100%' cellpadding='0' cellspacing='0'><tr width='100%'><td width='20%'>Employee Name</td><td width='30%'>: Balasubramaniyan</td><td width='20%'>Location</td><td width='20%'>: Chennai</td></tr><tr width='100%'><td width='20%'>Employee Id</td><td width='30%'>: KT0008</td><td width='20%'>Pan Number</td><td width='20%'>:</td></tr><tr width='100%'><td width='20%'>Designation</td><td width='30%'>: Project Engineer</td><td width='20%'>DOJ</td><td width='20%'>:01/02/2012</td></tr></table><div style='background-color:#0062A1;color:white;font-weight:bold;'> Attendance Details</div><table width='100%' cellpadding='0' cellspacing='0'><tr class='top row '><td width='20%' style=' border-top: 1px solid #0062A1;border-left:  1px solid #0062A1;' >Total Days</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '>: 31</td></tr><tr class='top row '><td width='20%' style=' border-top: 1px solid #0062A1;border-left:  1px solid #0062A1;'>No. of Days Worked</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '> : 31</td></tr></table><table width='100%' cellpadding='0' cellspacing='0'><tr width='100%' id='earnings_Deductions_header' ><td width='50%' style='background-color:#0062A1;font-weight:bold;'><table id='earnings' style='color:#FFF;font-weight:bold;border-left:  1px solid #0062A1;' cellpadding='0' cellspacing='0' width='100%' class='bot'><tr class='top ' width='100%'><td width='75%'>Earnings</td><td width='25%'>Amount</td></tr></table></td><td width='50%' style='background-color:#0062A1;color:white;font-weight:bold;'><table id='deductions' style='color:#FFF;font-weight:bold;' cellpadding='0' cellspacing='0' width='100%' class='right'><tr class='top  ' width='100%'><td  width='75%' style=' border-top: 1px solid #0062A1;  '>Deductions</td><td  width='25%' style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '>Amount</td></tr></table></td></tr><tr width='100%' ><td width='50%' style='border-left:  1px solid #0062A1;'><table  style='font-weight:bold;' width='100%' cellpadding='0' cellspacing='0' class='bot'><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Basic Salary</td><td style=' border-top: 1px solid #0062A1;'><span  >3750.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>HRA</td><td style=' border-top: 1px solid #0062A1;'><span  >1825.00</span></td></tr><tr class='top '><td width='75%' style=' border-top: 1px solid #0062A1;'>Telephone Allowance</td><td style=' border-top: 1px solid #0062A1;'> <span  >37500.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Dearness Allowance</td><td style=' border-top: 1px solid #0062A1;'><span  >3750.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Conveyance</td><td style=' border-top: 1px solid #0062A1;'><span  >0.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Loyalty Incentives</td><td style=' border-top: 1px solid #0062A1;'><span  >0.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Performance incentives</td><td style=' border-top: 1px solid #0062A1;'><span  >0.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Others</td><td style=' border-top: 1px solid #0062A1;'><span  >3750.00</span></td></tr><tr class='top  '><td style=' border-top: 1px solid #0062A1;'>&nbsp;</td><td style=' border-top: 1px solid #0062A1;' >&nbsp;</td></tr></table></td><td width='50%' ><table style='font-weight:bold;' width='100%' cellpadding='0' cellspacing='0' class='right'><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Provident Fund</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span >3750.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>ESI Deductions</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span >3750.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>TDS</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span  >3750.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Gratuity</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span  >183.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>Professional tax</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span  >375000000.00</span></td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>&nbsp;</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '>&nbsp;</td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>&nbsp;</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; ' >&nbsp;</td></tr><tr class='top  '><td width='75%' style=' border-top: 1px solid #0062A1;'>&nbsp;</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; ' >&nbsp;</td></tr><tr class='top '><td style=' border-top: 1px solid #0062A1;'>&nbsp;</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; ' >&nbsp;</td></tr></table></td></tr></table><table width='100%' cellspacing='0' class='bot right'><tr style='background-color:#999;' class='top  '><td width='37.5%' style=' border-top: 1px solid #0062A1;border-left:  1px solid #0062A1;' >Total Earnings</td><td  style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span  >1095</span></td><td width='37.5%' style=' border-top: 1px solid #0062A1;'>Total Deductions</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; '><span  >1095</span></td></tr><tr style='background-color:#999;' class='top  bottom row'><td width='37.5%' style=' border-top: 1px solid #0062A1; border-bottom:  1px solid #0062A1;border-left:  1px solid #0062A1;' >&nbsp;</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; border-bottom:  1px solid #0062A1 ' ><span  >&nbsp;</span></td><td width='37.5%' style=' border-top: 1px solid #0062A1; border-bottom:  1px solid #0062A1' >Net Take Home</td><td style=' border-top: 1px solid #0062A1; border-right:  1px solid #0062A1; border-bottom:  1px solid #0062A1 ' ><span  >1095</span></td></tr></table></div></body>";

            try
            {
                PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);

                //Open PDF Document to write data
                pdfDoc.Open();

                StyleSheet css = new StyleSheet();

                contents = Regex.Replace(contents, "&nbsp;", string.Empty);
                contents = contents.Replace("'", "\"");
              /* 
                css.LoadTagStyle("body", "line-height", "20px");
                css.LoadTagStyle("table", "border", "1");
                css.LoadTagStyle("table", "border-color", "#0062A1");
                css.LoadTagStyle("tr.row td:first-child", "border-left", "1px solid #0062A1");
                css.LoadTagStyle("tr.row td:last-child", "border-right", "1px solid #0062A1");
                
                */
                css.LoadStyle("payslipheading", "style", "color:black;font-weight:bold;text-align:center;");
                css.LoadTagStyle("td", "text-align", "justify");

                //Assign Html content in a string to write in PDF
                //string contents = "<h5>EXPORT HTML CONTENT TO PDF</h5><br/><br/><b><u>This content is convert from html string to PDF</u></b><br/><br/><br/><font color='red'>Samples from Ravi!!!</font>";

                //Read string contents using stream reader and convert html to parsed conent 
                var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), css);

                //Get each array values from parsed elements and add to the PDF document
                foreach (var htmlElement in parsedHtmlElements)
                    pdfDoc.Add(htmlElement as IElement);

                //Close your PDF
                pdfDoc.Close();

                Response.ContentType = "application/pdf";

                //Set default file Name as current datetime
                Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");
                System.Web.HttpContext.Current.Response.Write(pdfDoc);

                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        
        }



        //protected void selectmonth_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string text = selectmonth.Text;
        //    string emp_id = Session["empid"].ToString();
        //    string payslip = CommonUtility.CommonUtility.getPayslipDetails(emp_id,1,2013);
        //   // pay_panel.Controls.Clear();
            
        //    //pay_panel.Controls.Add();

        //}

        public void genpdf()
        {
         Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);

        try
        {
            PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);

            //Open PDF Document to write data
            pdfDoc.Open();

            //Assign Html content in a string to write in PDF
            string contents = "";

            StreamReader sr;
            try
            {
                //Read file from server path
                sr = File.OpenText(Server.MapPath("test.html"));
                //store content in the variable
                contents = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
              
            }

            //Read string contents using stream reader and convert html to parsed conent
            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), null);

            //Get each array values from parsed elements and add to the PDF document
            foreach (var htmlElement in parsedHtmlElements)
            pdfDoc.Add(htmlElement as IElement);

            //Close your PDF
            pdfDoc.Close();

            Response.ContentType = "application/pdf";

            //Set default file Name as current datetime
            Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");
            System.Web.HttpContext.Current.Response.Write(pdfDoc);

            Response.Flush();
            Response.End();

        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

        protected void selectmonth_SelectedIndexChanged(object sender, EventArgs e)
        {

            string text = selectmonth.Text;
            if (!text.Equals("-1"))
            {
                string[] values = text.Split('-');
                int month = Convert.ToInt32(values[0]);
                int year = Convert.ToInt32(values[1]);
                string emp_id = Request.QueryString["id"];

                string payslip = CommonUtility.CommonUtility.getPayslipDetails(emp_id, month, year);


                pay_panel.InnerHtml = payslip;
            }
            else
            {
                pay_panel.InnerHtml = "";
            }


        }
        


    }
}