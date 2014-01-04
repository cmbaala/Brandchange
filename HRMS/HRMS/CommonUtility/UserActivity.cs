using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.CommonUtility
{
    public class UserActivity
    {

        public int doSignup(Login_Details logindetails)
        {
            bool status;
            if (logindetails.email.Equals("signup"))
                status = CommonUtility.checkingEmpidStatus(logindetails.emp_id);
            else
                status = false;

            if (status == true)
            {
                // refers already empid exist
                return -1;
            }
            else
            {
                string email = CommonUtility.getEmployeeDetails(logindetails.emp_id).getEmail();
                if (!string.IsNullOrEmpty(email))
                {
                    string temppassword = CommonUtility.GeneratePassword(6);

                    string encodedPassword = CommonUtility.encrypt(temppassword, logindetails.emp_id);
                    bool insertstatus;
                    if (logindetails.email.Equals("signup"))
                        insertstatus = CommonUtility.insertLoginDetails(logindetails.emp_id, encodedPassword);
                    else
                        insertstatus = CommonUtility.updateLoginDetails(logindetails.emp_id, encodedPassword);

                    if (insertstatus == true)
                    {
                        CommonUtility.emailPassword(email, temppassword, logindetails.emp_id,logindetails.email);

                        return 1;

                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 2;
                }



            }
            
        }

        public  int doLogin(string empid,string password)
        {
            int status = CommonUtility.checkingUserCredentials(empid,password);

            return status;
        }

        public  int doChangePassword(string emp_id,string oldpassword,string newpassword)
        {
           
            int status=doLogin(emp_id,oldpassword);
             // if()
            if(status==1){
              int updated= CommonUtility.changePassword(emp_id,newpassword);
                if(updated==1){
                    return 1;
                }

            }else{
                return -1;
            }


                return 0;

            }

        public int doGetPayDetails(string emp_id,int month, int year)
        {



            return 0;

        }


      


    }
}