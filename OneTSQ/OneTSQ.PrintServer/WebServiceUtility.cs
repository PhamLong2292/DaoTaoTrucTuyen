using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneTSQ.PrintServer
{
    public class WebServiceUtility
    {
        static AjaxOut Convert(OnlineService.AjaxOut OnlineAjaxOut)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            RetAjaxOut.InfoMessage = OnlineAjaxOut.InfoMessage;
            RetAjaxOut.XmlData = OnlineAjaxOut.XmlData;
            RetAjaxOut.XmlDataSchema = OnlineAjaxOut.XmlDataSchema;

            RetAjaxOut.XmlDataResult = OnlineAjaxOut.XmlDataResult;
            RetAjaxOut.XmlDataResultSchema = OnlineAjaxOut.XmlDataResultSchema;

            RetAjaxOut.RetBoolean = OnlineAjaxOut.RetBoolean;
            
            RetAjaxOut.RetNumber = OnlineAjaxOut.RetNumber;
            RetAjaxOut.RetNumber1 = OnlineAjaxOut.RetNumber1;
            RetAjaxOut.RetNumber2 = OnlineAjaxOut.RetNumber2;
            RetAjaxOut.RetNumber3 = OnlineAjaxOut.RetNumber3;
            RetAjaxOut.RetNumber4 = OnlineAjaxOut.RetNumber4;
            

            RetAjaxOut.RetDecimal = OnlineAjaxOut.RetDecimal;
            
            RetAjaxOut.RefKeyId = OnlineAjaxOut.RefKeyId;

            RetAjaxOut.RetExtraParam1 = OnlineAjaxOut.RetExtraParam1;
            RetAjaxOut.RetExtraParam2 = OnlineAjaxOut.RetExtraParam2;
            RetAjaxOut.RetExtraParam3 = OnlineAjaxOut.RetExtraParam3;
            RetAjaxOut.RetExtraParam4 = OnlineAjaxOut.RetExtraParam4;
            RetAjaxOut.RetExtraParam5 = OnlineAjaxOut.RetExtraParam5;
            RetAjaxOut.RetExtraParam6 = OnlineAjaxOut.RetExtraParam6;


            RetAjaxOut.SaveFile = OnlineAjaxOut.SaveFile;

            RetAjaxOut.Error = OnlineAjaxOut.Error;
            
            return RetAjaxOut;
        }


        static OnlineService.Online CreateWebService()
        {
            OnlineService.Online ol = new OnlineService.Online();
            ol.Url = "https://s247.vn/app/services/online/online.asmx";
        //    ol.Url = "http://localhost:4321/services/online/online.asmx";
            return ol;
        }

       

    }
}
