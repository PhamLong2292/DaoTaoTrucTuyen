using OneTSQ.PrintServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.PrintServer
{
    public class XbaOrderPrintService : PrintServiceTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "XBA.ORDER";
            }
        }

        public override string ServiceName
        {
            get
            {
                return "In phiếu tạm";
            }
        }
    }
}
