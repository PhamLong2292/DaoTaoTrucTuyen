using OneTSQ.TempService;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for TempMediaServiceProcess
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class TempServiceProcess : System.Web.Services.WebService {

    public TempServiceProcess () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string CheckWs()
    {
        return "ok";
    }


    [WebMethod]
    public OneTSQ.Core.Model.AjaxOut UploadMedia(string SecurityCode, string Xml, string XmlSchema,  string RenderInfoXml,string RenderInfoXmlSchema, byte[] Bytes)
    {
        return TempServiceUtility.UploadMedia(SecurityCode, Xml, XmlSchema, RenderInfoXml, RenderInfoXmlSchema, Bytes);
    }

    [WebMethod]
    public OneTSQ.Core.Model.AjaxOut DownloadMedia(string SecurityCode, string Xml, string XmlSchema, string RenderInfoXml, string RenderInfoXmlSchema)
    {
        return TempServiceUtility.DownloadMedia(SecurityCode, Xml, XmlSchema, RenderInfoXml, RenderInfoXmlSchema);
    }

    [WebMethod]
    public OneTSQ.Core.Model.AjaxOut GetMediaInfo(string SecurityCode, string Xml, string XmlSchema, string RenderInfoXml, string RenderInfoXmlSchema)
    {
        return TempServiceUtility.GetMediaInfo(SecurityCode, Xml, XmlSchema, RenderInfoXml, RenderInfoXmlSchema);
    }
    
}
