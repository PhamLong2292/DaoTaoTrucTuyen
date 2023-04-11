using System;
using System.Management;
using System.Runtime.InteropServices;

namespace OneTSQ.Printer
{
    public static class PrinterHelper
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);

        public static bool CheckConnectedPrinter(string _printerName)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from win32_pnpentity where PNPClass = 'Printer'");
            foreach (var item in searcher.Get())
            {
                if (item["Name"].ToString().ToLower().Equals(_printerName.ToLower()))
                    return true;
            }
            return false;
            //Lấy cổng port
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort WHERE Name LIKE '%COM%'");
        }
    }
}