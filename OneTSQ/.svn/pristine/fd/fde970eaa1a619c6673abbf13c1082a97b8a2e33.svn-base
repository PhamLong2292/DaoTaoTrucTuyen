using C1.C1Report;
using OneTSQ.CallTempService;
using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ReportUtility
{
    public class C1ReportUtility
    {
        private string XMLReportFile;
        private C1Report c1Report1 = null;
        private string m_ReportFile;
        private DataTable dataSource;
        private string ReportName;

        public C1ReportUtility()
        {
            //
            // TODO: Add constructor logic here
            //
            c1Report1 = new C1Report();
        }

        public AjaxOut Execute(RenderInfoCls ORenderInfo, string LoginName, string _XMLReportFile, string _ReportName, DataTable _dtSource, string FileName)
        {
            dataSource = _dtSource;
            ReportName = _ReportName;
            XMLReportFile = _XMLReportFile;
            SetReportDefinitionFile(XMLReportFile);
            LoadReport(ReportName);

            c1Report1.RenderToFile(FileName, FileFormatEnum.PDF);
            MediaInfoCls
                UploadTempInfo = new MediaInfoCls();
            UploadTempInfo.MediaInfoId = System.Guid.NewGuid().ToString();
            UploadTempInfo.Month = System.DateTime.Now.Month;
            UploadTempInfo.Year = System.DateTime.Now.Year;
            UploadTempInfo.UploadFileName = new System.IO.FileInfo(FileName).Name;
            UploadTempInfo.LoginName = LoginName;
            UploadTempInfo.Section = "PDF";
            Byte[] Bytes = FunctionUtility.GetBytesFromFile(FileName);
            AjaxOut UploadAjaxOut = CallTempServiceUtility.UploadTemp(ORenderInfo, UploadTempInfo, Bytes);
            try
            {
                System.IO.File.Delete(FileName);
            }
            catch { }
            return UploadAjaxOut;
        }

        public string[] GetReports(string xmlFile)
        {
            string[] reports = c1Report1.GetReportInfo(xmlFile);
            return reports;
        }

        bool SetReportDefinitionFile(string xmlFile)
        {
            string[] reports = c1Report1.GetReportInfo(xmlFile);
            m_ReportFile = xmlFile;

            return true;
        }

        // load the report that is currently selected in the listbox
        bool LoadReport(string ReportName)
        {
            if (string.IsNullOrEmpty(ReportName))
                return false;
            //lstReports.Text
            bool retval = true;
            c1Report1.Load(m_ReportFile, ReportName);
            c1Report1.DataSource.Recordset = dataSource;

            return retval;
        }
    }
}
