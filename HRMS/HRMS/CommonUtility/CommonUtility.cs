using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Configuration;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HRMS.CommonUtility
{
    public class CommonUtility
    {
        public static string mailstatus { get; set; }
        public static int earningslength { get; set; }

        static int Main(string[] args)
        {
             
             string encdata = CommonUtility.encrypt("bala", "kt0008");
            Console.WriteLine(encdata);
            Console.WriteLine(CommonUtility.decrypt(encdata, "kt0008"));
            return 0;
        }

        static string alphaCaps = "QWERTYUIOPASDFGHJKLZXCVBNM";
        static string alphaLow = "qwertyuiopasdfghjklzxcvbnm";
        static string numerics = "1234567890";
        static string special = "@#$";
        //create another string which is a concatenation of all above
        private static string allChars = alphaCaps + alphaLow + numerics + special;

        //generate simple password
       public static string GeneratePassword(int length)
        {
            Random r = new Random();
            String generatedPassword = "";
            for (int i = 0; i < length; i++)
            {
                double rand = r.NextDouble();
                if (i == 0)
                {
                    //First character is an upper case alphabet
                    generatedPassword += alphaCaps.ToCharArray()[(int)Math.Floor(rand * alphaCaps.Length)];
                }
                else
                {
                    generatedPassword += allChars.ToCharArray()[(int)Math.Floor(rand * allChars.Length)];
                }
            }
            return generatedPassword;
        }


        public static string encrypt(string message, string passphrase)
        {
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            //to create the object for UTF8Encoding  class
            //TO create the object for MD5CryptoServiceProvider
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
            //to convert to binary passkey
            //TO create the object for  TripleDESCryptoServiceProvider
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;//to  pass encode key
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] encrypt_data = utf8.GetBytes(message);
            //to convert the string to utf encoding binary

            try
            {

                //To transform the utf binary code to md5 encrypt
                ICryptoTransform encryptor = desalg.CreateEncryptor();
                results = encryptor.TransformFinalBlock(encrypt_data, 0, encrypt_data.Length);

            }
            finally
            {
                //to clear the allocated memory
                desalg.Clear();
                md5.Clear();
            }

            //to convert to 64 bit string from converted md5 algorithm binary code
            return Convert.ToBase64String(results);

        }

        public static bool checkingEmpidStatus(string empid)
        {
            using (SqlConnection conn = getConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_checklogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@empid", empid));
                SqlDataReader rd = cmd.ExecuteReader();


              
                if (rd.HasRows)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
              }
        }


        public static void insertEmloyeeDetails()
        {
           
        }


        public static int checkingUserCredentials(string empid,string password)
        {
            using (SqlConnection conn = getConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_checklogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@empid", empid));
                SqlDataReader rd = cmd.ExecuteReader();


                
                if (rd.HasRows)

                {
                    while (rd.Read())
                    {
                        if (password.Equals(CommonUtility.decrypt(rd.GetString(0), empid)))
                        {
                            // valid user
                            conn.Close();
                            return 1;
                        }
                     }
                    //password error
                    return 0;
                }
                else
                {
                    //emp id not exists
                    return -1;
                }
            }

        }


        public static string decrypt(string message, string passphrase)
        {
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] decrypt_data = Convert.FromBase64String(message);
            try
            {
                //To transform the utf binary code to md5 decrypt
                ICryptoTransform decryptor = desalg.CreateDecryptor();
                results = decryptor.TransformFinalBlock(decrypt_data, 0, decrypt_data.Length);
            }
            finally
            {
                desalg.Clear();
                md5.Clear();

            }
            //TO convert decrypted binery code to string
            return utf8.GetString(results);
        }


        public static void emailPassword(string mailid,string password,string empid,string type)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = false;
            smtp.SendCompleted += new SendCompletedEventHandler(onCompleted);
           // string toEmail = "cmbaala@gmail.com";
            string fromEmail = ConfigurationManager.AppSettings["Email"].ToString();
            MailMessage message = new MailMessage(fromEmail, mailid);
            message.Subject = "Keep Password Protected";
            if (type.Equals("signup"))
             message.Body = "<h3>Welcome to this application </h3><br> Your Employee Id: "+empid+"<br>Your password is :"+password+"<br><br>Plese change password once you login";
            else
                message.Body = "Employee Id: " + empid + "<br>Your new password is :" + password + "<br><br>Plese change password once you login";
            message.IsBodyHtml = true;
           // message.Attachments.Add(new Attachment("D:\\profile.jpg"));


            smtp.Send(message);

        }

        public static string sendEmailMessage()
        {
            //both are working
            //var myMailMessage = new System.Net.Mail.MailMessage();
            //myMailMessage.From = new System.Net.Mail.MailAddress("murugesan.balasubramaniyan@kaspontech.com");
            //myMailMessage.To.Add("murugesan.balasubramaniyan@kaspontech.com");// Mail would be sent to this address
            //myMailMessage.Subject = "Feedback Form";
            //myMailMessage.Body = "Hello";
            //myMailMessage.Attachments.Add(new Attachment("D:\\profile.jpg"));
            //var smtpServer = new System.Net.Mail.SmtpClient("smtp.kaspontech.com");
            //smtpServer.Port = 587;
            //smtpServer.Credentials = new System.Net.NetworkCredential("murugesan.balasubramaniyan@kaspontech.com", "kasponhome000*");
            //smtpServer.EnableSsl = true;
            //smtpServer.Send(myMailMessage);

            SmtpClient smtp = new SmtpClient();
            smtp.SendCompleted += new SendCompletedEventHandler(onCompleted);
            string toEmail = "cmbaala@gmail.com";
            string fromEmail = ConfigurationManager.AppSettings["Email"].ToString();
            MailMessage message = new MailMessage(fromEmail, toEmail);
            message.Subject = "This is for test";
            message.Body = "This is for from kaspon";
            message.Attachments.Add(new Attachment("D:\\profile.jpg"));

         
           // smtp.SendAsync(message,"Test Mail");
            smtp.Send(message);
            return mailstatus;
        }

        private static void onCompleted(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
               // Console.WriteLine("[{0}] Send canceled.", token);
                mailstatus = "Cancelled";
            }
            if (e.Error != null)
            {
                //Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
                mailstatus = "Error";
            }
            else
            {
                //Console.WriteLine("Message sent.");
                mailstatus = "Mail Sent";
            }
           

        }
        public static SqlConnection getConnection() {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbCon"].ConnectionString);
        
        
        }

        public static bool insertLoginDetails(string empid,string password)
       {
           using (SqlConnection conn = getConnection())
           {
               conn.Open();

               SqlCommand cmd = new SqlCommand("sp_insert_logindetails", conn);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add(new SqlParameter("@empid", empid));
               cmd.Parameters.Add(new SqlParameter("@password", password));
               SqlDataReader rd = cmd.ExecuteReader();
              
               if (rd.HasRows)
               {
                   while (rd.Read())
                   {
                       if (rd.GetInt32(0) > 0)
                       {
                           return true;
                       }
                   }
               }
               conn.Close();
              
           }

           return false;
        }

        public static bool updateLoginDetails(string empid, string password)
        {
            using (SqlConnection conn = getConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_changepassword", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@empid", empid));
                cmd.Parameters.Add(new SqlParameter("@password", password));
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd.GetInt32(0) > 0)
                        {
                            return true;
                        }
                    }
                }
                conn.Close();

            }

            return false;
        }


        public static int changePassword(string emp_id,string newpassword)
        {

            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                string encodedPassword = CommonUtility.encrypt(newpassword, emp_id);
                SqlCommand cmd = new SqlCommand("sp_changepassword", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@empid", emp_id));
                cmd.Parameters.Add(new SqlParameter("@password", encodedPassword));
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd.GetInt32(0) > 0)
                        {
                            return 1;
                        }
                    }
                }
                conn.Close();

            }


            return 0;
        }

        public static Pay getPaySlip(string emp_id, int month, int year)
        {
            Pay pay = new Pay();
            Employee employee = getEmployeeDetails(emp_id);
            pay.setEmployee(employee);
            using (SqlConnection conn = getConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_getpayslipDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@empid", emp_id));
                cmd.Parameters.Add(new SqlParameter("@month", month));
                cmd.Parameters.Add(new SqlParameter("@year", year));
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        pay.setTotalDays(Convert.ToInt32(rd["Total_Days"]));
                        pay.setWorkingDays(Convert.ToInt32(rd["Working_Days"]));
                        pay.setNetincome(Convert.ToDouble(rd["Net_Income"]));
                        pay.setNettakehome(Convert.ToDouble(rd["Net_Takehome"]));

                        pay.setDeduction(rd["Deductions"].ToString());
                        pay.setEarnings(rd["Earnings"].ToString());
                        pay.setTotalDeduction(Convert.ToDouble(rd["Total_deduction"]));
                        pay.setTotalEarnings(Convert.ToDouble(rd["Total_Earnings"]));
                      

                        // payslipstring = rd["pay"].ToString();

                    }
                }
                conn.Close();

            }

            return pay;
        
        }


        public static string getPayslipDetails(string emp_id, int month, int year)
        {
            string payslipstring = "";
            Pay pay = getPaySlip(emp_id, month,  year);
            payslipstring = getPayslipstring(pay, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),year);
          


            return payslipstring;
        }

        public static string getPayslipstring(Pay pay,string month,int year)
        {
            string paylipstring = "";
            Employee emp = pay.getEmployee();

            string earningsstring = formEarningsString(pay.getEarnings());
            string deductionstring= formDeductionString(pay.getDeduction());

            paylipstring = "<html ><head></head><body><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr ><td colspan=\"2\" ><table id=\"uppertable\"><tr><td><img src=\"../images/logo.png\" class=\"logo\"/><div><div style=\"color:#0062A1;font-weight:bold;\">Kaspon Techworks Private limited</div><div style=\"color:#0062A1;font-size:14px;\">#123, Developed Plot Estate,</div><div style=\"color:#0062A1;font-size:14px;\">Off OMR, Perungudi, Chennai -600096,</div><div style=\"color:#0062A1;font-size:14px;\">Tel : +91-44 42125741,42125914.</div><div style=\"color:#0062A1;font-size:14px;\">http://www.kaspontech.com</div></div></td></tr></table></td></tr><tr  height=\"60px\"><td colspan=\"2\" ><div class=\"payslipheading\" style=\"background-color:#0062A1;color:white;font-weight:bold;text-align:center;\">Payslip for the month of " + month + "  " + year + "</div></td></tr><tr  width=\"100%\" ><td colspan=\"2\"><table class=\"employeeDetailsTable\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tr width=\"100%\"><td width=\"20%\">Employee Name</td><td width=\"30%\">: " + emp.getEmployeename() + "</td><td width=\"20%\">Location</td><td width=\"20%\">: " + emp.getLocation() + "</td></tr><tr width=\"100%\"><td width=\"20%\">Employee Id</td><td width=\"30%\">: " + emp.getEmployee_id().ToUpper() + "</td><td width=\"20%\">Pan Number</td><td width=\"20%\">: " + emp.getPan_number() + "</td></tr><tr width=\"100%\"><td width=\"20%\">Designation</td><td width=\"30%\">:  " + emp.getDesignation() + "</td><td width=\"20%\">DOJ</td><td width=\"20%\">:  " + emp.getDoj().ToString("dd/MM/yyyy") + "</td></tr></table></td></tr><tr colspan=\"2\" width=\"100%\" class=\"top row \"><td colspan=\"2\"><div style=\"background-color:#0062A1;color:white;font-weight:bold;\"> Attendance Details</div></td></tr><tr class=\"top row \"><td width=\"20%\" >Total Days</td><td >:  " + pay.getTotalDays() + "</td></tr><tr class=\"top row \"><td width=\"20%\" >No. of Days Worked</td><td > : " + pay.getWorkingDays() + "</td></tr><tr width=\"100%\" id=\"earnings_Deductions_header\" ><td width=\"50%\" style=\"background-color:#0062A1;font-weight:bold;\"><table id=\"earnings\" style=\"color:#FFF;font-weight:bold;\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"bot\"><tr class=\"top \" width=\"100%\"><td width=\"75%\">Earnings</td><td width=\"25%\">Amount</td></tr></table></td><td width=\"50%\" style=\"background-color:#0062A1;color:white;font-weight:bold;\"><table id=\"deductions\" style=\"color:#FFF;font-weight:bold;\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"right\"><tr class=\"top  \" width=\"100%\"><td  width=\"75%\">Deductions</td><td  width=\"25%\">Amount</td></tr></table></td></tr><tr width=\"100%\" ><td width=\"50%\" >" + earningsstring + "</td><td width=\"50%\" >" + deductionstring + "</td></tr><tr><td colspan=\"2\"><table width=\"100%\" cellspacing=\"0\" class=\"bot right\"><tr style=\"background-color:#999;\" class=\"top  \"><td width=\"37.5%\" >Total Earnings</td><td ><span  >" + pay.getTotalEarnings() + "</span></td><td width=\"37.5%\">Total Deductions</td><td ><span  >" + pay.getTotalDeduction() + "</span></td></tr><tr style=\"background-color:#999;\" class=\"top  bottom row\"><td width=\"37.5%\" ></td><td ><span  ></span></td><td width=\"37.5%\" >Net Take Home</td><td ><span  >" + pay.getNettakehome() + "</span></td></tr></table></td></tr></table></body></html>";


            return paylipstring;
        
        }
        public static string formEarningsString(string earnings)
        {
            string earningsstring = "";
           // <table  style=\"font-weight:bold;\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"bot\"><tr class=\"top  \"><td width=\"75%\">Basic Salary</td><td><span  >3750.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">HRA</td><td><span >1875.0</span></td></tr><tr class=\"top \"><td width=\"75%\">Telephone Allowance</td><td ><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Dearness Allowance</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Conveyance</td><td><span  >800.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Loyalty Incentives</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Performance incentives</td><td><span  >6400.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Others</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td></td><td></td></tr></table>
            earningsstring += "<table  style=\"font-weight:bold;\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"bot\">";

            string[] earningsarray = Regex.Split(earnings, "---");
            //  
            foreach (string value in earningsarray)
            {

             string[] temp = Regex.Split(value, "--");

             earningsstring += "<tr class=\"top  \"><td width=\"75%\">"+temp[0]+"</td><td><span  >"+temp[1]+"</span></td></tr>";

            
            }
            earningsstring += "<tr class=\"top  \"><td style=\"\" >&nbsp;</td><td>&nbsp;</td></tr></table>";

            CommonUtility.earningslength = earningsarray.Length + 1;


            return earningsstring;
        
        }

        public static string formDeductionString(string deduction)
        {
            string deductionstring = "";
            //<table style=\"font-weight:bold;\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"right\"><tr class=\"top  \"><td width=\"75%\">Provident Fund</td><td><span >450.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">ESI Deductions</td><td><span >225.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">TDS</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Gratuity</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\">Professional tax</td><td><span  >0.0</span></td></tr><tr class=\"top  \"><td width=\"75%\"></td><td></td></tr><tr class=\"top  \"><td width=\"75%\"></td><td></td></tr><tr class=\"top  \"><td width=\"75%\"></td><td></td></tr><tr class=\"top \"><td></td><td></td></tr></table>
            deductionstring += "<table style=\"font-weight:bold;\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"right\">";
            string[] deductionsarray = Regex.Split(deduction, "---");
          //  
            foreach (string value in deductionsarray)
            {
                string[] temp = Regex.Split(value,"--");
                deductionstring += "<tr class=\"top  \"><td width=\"75%\">"+temp[0]+"</td><td><span >"+temp[1]+"</span></td></tr>";
            
            }
            for (int i = deductionsarray.Length; i < CommonUtility.earningslength; i++)
            {
                deductionstring += "<tr class=\"top \"> <td>&nbsp;</td><td>&nbsp;</td></tr>";  

            }
            deductionstring += "</table>";

            return deductionstring;
        
        }
        
        public static Employee getEmployeeDetails(string emp_id)
        {
            Employee employee = new Employee();
            using (SqlConnection conn = getConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_get_employeedetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@empid", emp_id));
                
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        employee.setEmployee_id(emp_id);
                        employee.setEmail(rd["Email"].ToString());
                        employee.setDesignation(rd["Designation"].ToString());
                        employee.setEmployeename(getStringFromReader(rd,"First Name"));
                        employee.setLocation(getStringFromReader(rd,"Location"));
                        employee.setPan_number(getStringFromReader(rd,"Pan_Number"));
                        employee.setDoj(Convert.ToDateTime(rd["Doj"]));
                       // employee.setAccount_Number(getStringFromReader(rd,"Pan_Number"));
                    }
                }
                conn.Close();

            }


            return employee;

        }

        public static int insertEmployeeDetails(Employee employee)
        {
            //Employee employee = new Employee();
            int returnvalue=0;
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                //( @Emp_Id , @Firstname , @Lastname, @Email, @Location , @Pan_Number , @Designation , @Doj, 
                //@Gender bit, @Bank_Name varchar(50), @Account_Number varchar(50), @Active bit , @isApplicableForExtraDeduction bit)
                SqlCommand cmd = new SqlCommand("sp_insert_empdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@emp_id", employee.getEmployee_id()));
                cmd.Parameters.Add(new SqlParameter("@Firstname", employee.getEmployeename()));
                cmd.Parameters.Add(new SqlParameter("@Lastname", employee.getLastname()));
                cmd.Parameters.Add(new SqlParameter("@Email", employee.getEmail()));
                cmd.Parameters.Add(new SqlParameter("@Location", employee.getLocation()));
                cmd.Parameters.Add(new SqlParameter("@Doj", employee.getDoj()));
                cmd.Parameters.Add(new SqlParameter("@Pan_Number", employee.getPan_number()));
                cmd.Parameters.Add(new SqlParameter("@Designation", employee.getDesignation()));
                cmd.Parameters.Add(new SqlParameter("@Gender", employee.isActive()));
                cmd.Parameters.Add(new SqlParameter("@Bank_Name", employee.getBank_Name()));
                cmd.Parameters.Add(new SqlParameter("@Account_Number", employee.getAccount_Number()));
                cmd.Parameters.Add(new SqlParameter("@Active", employee.isActive()));
                cmd.Parameters.Add(new SqlParameter("@isApplicableForExtraDeduction", 0));

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if ((rd[0].ToString().Equals(employee.getEmployee_id())))
                            returnvalue = 1;
                        else
                            returnvalue = 2;
                        //employee.setEmployee_id(emp_id);
                        //employee.setEmail(rd["Email"].ToString());
                        //employee.setDesignation(rd["Designation"].ToString());
                        //employee.setEmployeename(getStringFromReader(rd, "First Name"));
                        //employee.setLocation(getStringFromReader(rd, "Location"));
                        //employee.setPan_number(getStringFromReader(rd, "Pan_Number"));
                        //employee.setDoj(Convert.ToDateTime(rd["Doj"]));
                        // employee.setAccount_Number(getStringFromReader(rd,"Pan_Number"));
                    }
                }
                conn.Close();

            }


            return returnvalue;

        }
        public static int updateEmployeeDetails(Employee employee)
        {
            //Employee employee = new Employee();
            int returnvalue = 0;
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                //( @Emp_Id , @Firstname , @Lastname, @Email, @Location , @Pan_Number , @Designation , @Doj, 
                //@Gender bit, @Bank_Name varchar(50), @Account_Number varchar(50), @Active bit , @isApplicableForExtraDeduction bit)
                SqlCommand cmd = new SqlCommand("sp_insert_empdetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@emp_id", employee.getEmployee_id()));
                cmd.Parameters.Add(new SqlParameter("@Firstname", employee.getEmployeename()));
                cmd.Parameters.Add(new SqlParameter("@Lastname", employee.getLastname()));
                cmd.Parameters.Add(new SqlParameter("@Email", employee.getEmail()));
                cmd.Parameters.Add(new SqlParameter("@Location", employee.getLocation()));
                cmd.Parameters.Add(new SqlParameter("@Doj", employee.getDoj()));
                cmd.Parameters.Add(new SqlParameter("@Pan_Number", employee.getPan_number()));
                cmd.Parameters.Add(new SqlParameter("@Designation", employee.getDesignation()));
                cmd.Parameters.Add(new SqlParameter("@Gender", employee.isActive()));
                cmd.Parameters.Add(new SqlParameter("@Bank_Name", employee.getBank_Name()));
                cmd.Parameters.Add(new SqlParameter("@Account_Number", employee.getAccount_Number()));
                cmd.Parameters.Add(new SqlParameter("@Active", employee.isActive()));
                cmd.Parameters.Add(new SqlParameter("@isApplicableForExtraDeduction", 0));

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if ((rd[0].ToString().Equals(employee.getEmployee_id())))
                            returnvalue = 1;
                        else
                            returnvalue = 2;
                        //employee.setEmployee_id(emp_id);
                        //employee.setEmail(rd["Email"].ToString());
                        //employee.setDesignation(rd["Designation"].ToString());
                        //employee.setEmployeename(getStringFromReader(rd, "First Name"));
                        //employee.setLocation(getStringFromReader(rd, "Location"));
                        //employee.setPan_number(getStringFromReader(rd, "Pan_Number"));
                        //employee.setDoj(Convert.ToDateTime(rd["Doj"]));
                        // employee.setAccount_Number(getStringFromReader(rd,"Pan_Number"));
                    }
                }
                conn.Close();

            }


            return returnvalue;

        }


        public static DataTable getAllEmployeeDetails()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = getConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_getAllactiveemployees", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
               
            }


            return dt;

        }


        public static string getStringFromReader(SqlDataReader reader, string colName)
        {
            if (reader[colName] != DBNull.Value)
            {
                return reader[colName].ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        public static DataTable getMonthDetails(string emp_id)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            dt.Rows.Add("(Select One)", "-1");
            using (SqlConnection conn = getConnection())
            {
                
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_getmonthDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@empid", emp_id));
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        int year = Convert.ToInt32(rd["year"]);
                        int month = Convert.ToInt32(rd["month"]);
                        string text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)+"-"+year;
                        string value=month+"-"+year;
                        dt.Rows.Add(text,value);
                        
                    }
                }
                conn.Close();

            }


            return dt;
        }






    }
}