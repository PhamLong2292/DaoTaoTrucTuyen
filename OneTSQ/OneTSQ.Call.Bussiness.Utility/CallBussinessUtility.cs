using OneTSQ.Call.Bussiness.Sql;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Call.Bussiness.Ws;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Call.Bussiness.Utility
{
    public class CallBussinessUtility
    {
        public static IBussinessProcess CreateBussinessProcess()
        {
            string CallBussinessServiceId = (string)WebConfig.GetWebConfig("CallBussinessServiceId");
            if (string.IsNullOrEmpty(CallBussinessServiceId))
            {
                CallBussinessServiceId = "CoreSqlProcess";
            }

            if (CallBussinessServiceId.Equals("CoreSqlProcess"))
            {
                return new CoreSqlProcess();
            }

            if (CallBussinessServiceId.Equals("CoreWsProcess"))
            {
                return new CoreWsProcess();
            }

            throw new Exception("NOT SUPPORT [" + CallBussinessServiceId + "] SERVICE");
        }
    }
}
