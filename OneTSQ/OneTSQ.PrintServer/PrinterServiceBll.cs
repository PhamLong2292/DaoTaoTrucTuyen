using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.PrintServer
{
    public class PrinterServiceBll
    {
        public static bool SetDefaultPrinter(string defaultPrinter)
        {
            using (ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
            {
                using (ManagementObjectCollection objectCollection = objectSearcher.Get())
                {
                    foreach (ManagementObject mo in objectCollection)
                    {
                        if (string.Compare(mo["Name"].ToString(), defaultPrinter, true) == 0)
                        {
                            mo.InvokeMethod("SetDefaultPrinter", null, null);
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        public static ManagementObject GetPrinter(string defaultPrinter)
        {
            using (ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
            {
                using (ManagementObjectCollection objectCollection = objectSearcher.Get())
                {
                    foreach (ManagementObject mo in objectCollection)
                    {
                        if (string.Compare(mo["Name"].ToString(), defaultPrinter, true) == 0)
                        {
                            return mo;
                        }
                    }
                }
            }
            return null;
        }


        public static List<string> GetPrinterList()
        {
            List<string> 
                Printers = new List<string> { };
            
            using (ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
            {
                using (ManagementObjectCollection objectCollection = objectSearcher.Get())
                {
                    foreach (ManagementObject mo in objectCollection)
                    {
                        Printers.Add(mo["Name"].ToString());
                    }
                }
            }

            return Printers;
        }



        public static void PrintTest(string PrinterName)
        {
            ManagementObject mNewPrinter= GetPrinter(PrinterName);
            if (mNewPrinter == null) throw new Exception("Không tìm thấy máy in");
            ManagementBaseObject outParams =
                mNewPrinter.InvokeMethod("PrintTestPage", null, null);
        }
    }
}
