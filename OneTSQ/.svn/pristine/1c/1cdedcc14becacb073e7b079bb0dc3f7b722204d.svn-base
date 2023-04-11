using OneTSQ.PrintServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.PrintServer
{
    public class PrintServiceUtility
    {
        public static PrintServiceTemplate[] GetPrintServiceTemplate()
        {
            PrintServiceTemplate[]
                PrintServiceTemplates = new PrintServiceTemplate[]
                {
                    new XbaPrintService(),
                    new XbaOrderPrintService(),
                    new XbaOrderPrintNewService()
                };

            return PrintServiceTemplates;
        }
    }
}
