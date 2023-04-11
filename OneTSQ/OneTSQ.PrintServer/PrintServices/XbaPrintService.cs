using OneTSQ.PrintServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.PrintServer
{
    public class XbaPrintService:PrintServiceTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "XBA";
            }
        }

        public override string ServiceName
        {
            get
            {
                return "In phiếu bán hàng";
            }
        }
    }
}
