using Newtonsoft.Json;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ProcessWebService
{
    public class WsProcessCommand : WebServiceProcessCommandTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "WsProcessCommand";
            }
        }

        public override string ServiceName
        {
            get
            {
                return "Xử lý nghiệp vụ";
            }
        }
        

        public override AjaxOut ProcessCommand(WebServiceParamCls OWebServiceParam)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }

            return RetAjaxOut;
        }
    }
}
