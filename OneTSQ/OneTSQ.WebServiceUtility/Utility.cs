using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebServiceUtility
{
    public class Utility
    {
        public static CallWs.FuncWebServiceProcess CreateWebService()
        {
            string WsUrl = (string)WebConfig.GetWebConfig("WsUrl");
            CallWs.FuncWebServiceProcess
                OCallWs = new CallWs.FuncWebServiceProcess();
            OCallWs.Url = WsUrl;

            return OCallWs;
        }

        public static string GetSecurityCode()
        {
            string WsSecurityCode = (string)WebConfig.GetWebConfig("WsSecurityCode");
            return WsSecurityCode;
        }

        public static XmlCls CreateCommand(
            string Command,
            RenderInfoCls ORenderInfo,
            string KeyId,
            XmlCls OXml)
        {
            WebServiceParamCls
             OWebServiceParam = new WebServiceParamCls();
            OWebServiceParam.Command = Command;
            OWebServiceParam.OwnerId = ORenderInfo.OwnerId;
            OWebServiceParam.SiteId = ORenderInfo.SiteId;
            OWebServiceParam.SiteLang = ORenderInfo.SiteLanguage;
            OWebServiceParam.XmlData = OXml.XmlData;
            OWebServiceParam.XmlDataSchema = OXml.XmlDataSchema;
            OWebServiceParam.KeyId = KeyId;
            OWebServiceParam.UseJson = 0;

            XmlCls ORetXml = WebServiceParamParser.GetXml(OWebServiceParam);
            return ORetXml;
        }

        public static XmlCls CreateCommand(
            string Command,
            RenderInfoCls ORenderInfo,
            string KeyId,
            string JsonData)
        {
            WebServiceParamCls
             OWebServiceParam = new WebServiceParamCls();
            OWebServiceParam.Command = Command;
            OWebServiceParam.OwnerId = ORenderInfo.OwnerId;
            OWebServiceParam.SiteId = ORenderInfo.SiteId;
            OWebServiceParam.SiteLang = ORenderInfo.SiteLanguage;
            OWebServiceParam.UseJson = 1;
            OWebServiceParam.JsonData = JsonData;
            OWebServiceParam.KeyId = KeyId;

            XmlCls ORetXml = WebServiceParamParser.GetXml(OWebServiceParam);
            return ORetXml;
        }
    }
}
