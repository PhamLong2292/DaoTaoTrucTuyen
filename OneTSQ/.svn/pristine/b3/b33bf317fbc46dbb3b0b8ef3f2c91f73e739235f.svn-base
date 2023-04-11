using OneTSQ.Core.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OneTSQ.PrintServer
{
	/// <summary>
	/// Summary description for C1ReportController.
	/// </summary>
	public class C1ReportController
	{
        public delegate void ProcessMessageHandler(string Message);
        public event ProcessMessageHandler ProcessMessageEvent;
        
        public string TransactionId;
		public C1ReportController()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		

        void frmMain_RequestDataEvent(string PrintServiceId, int ReportIndex, string ReportTitle,  ref DataTable dt)
        {
            try
            {

                dt = new DataTable();//thay dt = Lay du lieu o day
                if (dt.Rows.Count == 0)
                {
                    if (ProcessMessageEvent != null)
                    {
                        ProcessMessageEvent(PrintServiceId+": Không có dữ liệu");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(null, ex.Message.ToString(), "Thông báo");
                if (ProcessMessageEvent != null)
                {
                    ProcessMessageEvent(PrintServiceId + ":" + ex.Message.ToString());
                }
                dt = null;
            }
        }


        public void ExecutePrint(
            string PrintServiceId,
            string _TransactionId, 
            string ReportTitle, 
            string XMLReportFile, 
            int SelectIndexReport,
            string PrinterName)
        {
            TransactionId = _TransactionId;
            frmMainWithDataSource frmMain = new frmMainWithDataSource();
            frmMain.RequestDataEvent += frmMain_RequestDataEvent;
            
            frmMain.InitForm(PrintServiceId, ReportTitle, XMLReportFile, true, SelectIndexReport, PrinterName);
            frmMain.PrintReport(PrinterName);
            frmMain.Dispose();
        }
	}
}
