using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HRMS.CommonUtility;
using System.ServiceModel.Web;

namespace HRMS.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Ihrms" in both code and config file together.
    [ServiceContract]
    public interface Ihrms
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
             BodyStyle = WebMessageBodyStyle.WrappedRequest,
          ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "test")]
        string test();

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //     BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //   ResponseFormat = WebMessageFormat.Json,
        //   UriTemplate = "signup/{email}/{empid}")]
        //String signup(string email,string empid);

        [OperationContract]
        [WebInvoke(Method = "POST",
             BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "signup")]
        String signup(Login_Details login);

        [OperationContract]
        [WebInvoke(Method = "POST",
             BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "getPayslip")]
        string getPayslip_Details(string emp_id, string month, string year);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //     BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //   UriTemplate = "login")]
        //String login(Login_Details login);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //     BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //   UriTemplate = "viewpayslip")]
        //String viewPayslip(Login_Details login);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //     BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //   UriTemplate = "addemployee")]
        //String addEmployee(Employee_Details employee_det);

    }
}
