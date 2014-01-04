using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Hosting;
using HRMS.CommonUtility;
using System.Globalization;
using System.IO;
namespace HRMS.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "hrms" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select hrms.svc or hrms.svc.cs at the Solution Explorer and start debugging.
    
    public class hrms : Ihrms
    {
       
        public string test()
        {
            //string mailstatus=HRMS.CommonUtility.CommonUtility.sendEmailMessage();
       ;
       //return "Hello :" + OperationContext.Current.RequestContext.RequestMessage.Headers.To.GetLeftPart(UriPartial.Authority)+ HostingEnvironment.ApplicationVirtualPath;
     return  CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(1);
        
        }

        public string signup(Login_Details logindetails)
        {
            UserActivity useractivity = new UserActivity();
            int status = useractivity.doSignup(logindetails);

            if (status == 1)
            {
                if(logindetails.email.Equals("signup"))
                return "Profile Created Successfully please check your email";
                else
                return "New password sent to your mail please check your email";
            }
            else if (status == -1)
            {
                return "User Already Exists Please use forget password";
            }
            else if (status == 2)
            {
                return "Employee id not found in server please contact your admin";
            }
            else
            {
                return "Some unexpected error occured. Please try again after sometimes";
            }


        }

        public string getPayslip_Details(string emp_id,string month,string year)
        {
            

            return CommonUtility.CommonUtility.getPayslipDetails(emp_id, Convert.ToInt32(month), Convert.ToInt32(year));


        }

       

        //public string signup(string email,string empid)
        //{
        //    Login_Details logindetails = new Login_Details();

        //    logindetails.email = email;
        //    logindetails.emp_id = empid;

        //    UserActivity useractivity = new UserActivity();
        //    int status = useractivity.doSignup(logindetails);
        //    if (status == 1)
        //    {
        //        return "Profile Created Successfully please check your email";
        //    }
        //    else if (status == -1)
        //    {
        //        return "User Already Exists Please use forget password";
        //    }
        //    else
        //    {
        //        return "Some unexpected error occured. Please try again after sometimes";
        //    }


        //}

    }
}
