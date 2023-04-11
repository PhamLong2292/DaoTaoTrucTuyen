using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for FuncWebServiceProcess
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FuncWebServiceProcess : System.Web.Services.WebService {

    public FuncWebServiceProcess () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string CheckWs() {
        return "ok";
    }


    [WebMethod]
    public OneTSQ.Core.Model.AjaxOut ProcessComand(
        string SecurityCode,
        string XmlData,
        string XmlDataSchema)
    {
        return OneTSQ.ProcessWebService.Utility.ProcessWebServiceUtility.ProcessComand(SecurityCode, XmlData, XmlDataSchema);
    }


}
