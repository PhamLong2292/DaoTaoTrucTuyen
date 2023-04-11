using Aspose.Words;
using Aspose.Words.Reporting;
using OneTSQ.CallTempService;
using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ReportUtility
{
    public class WordReportUtility
    {
        //private string XMLReportFile;
        private Document ReportDocument = null;
        //private string ReportName;

        public const Aspose.Words.LoadFormat ASPOSE_LOADFORMAT = Aspose.Words.LoadFormat.Docx;
        public const Aspose.Words.SaveFormat ASPOSE_SAVEFORMAT = Aspose.Words.SaveFormat.Docx;

        public static readonly byte[] EmptyInclude = StreamHelper.Read(typeof(WordReportUtility).Assembly.GetManifestResourceStream("OneTSQ.ReportUtility.Aspose.EmptyInclude.docx"));
        public static readonly byte[] BlankImage = StreamHelper.Read(typeof(WordReportUtility).Assembly.GetManifestResourceStream("OneTSQ.ReportUtility.Aspose.BlankImage.png"));

        public WordReportUtility()
        {
            //
            // TODO: Add constructor logic here
            //
            ReportDocument = new Document();
        }

        Stream OnLoadTemplate(string templatePath)
        {
            return new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        }

        public AjaxOut Execute(RenderInfoCls ORenderInfo, string LoginName, string _XMLReportFile, string _ReportName, Document reportDocument, string FileName)
        {
            var OutStream = new MemoryStream();
            ((Document)reportDocument).Save(OutStream, Aspose.Words.SaveFormat.Pdf);
            OutStream.Seek(0, SeekOrigin.Begin);


            MediaInfoCls
                UploadTempInfo = new MediaInfoCls();
            UploadTempInfo.MediaInfoId = System.Guid.NewGuid().ToString();
            UploadTempInfo.Month = System.DateTime.Now.Month;
            UploadTempInfo.Year = System.DateTime.Now.Year;
            UploadTempInfo.UploadFileName = new System.IO.FileInfo(FileName).Name;
            UploadTempInfo.LoginName = LoginName;
            UploadTempInfo.Section = "PDF";
            //Byte[] Bytes = FunctionUtility.GetBytesFromFile(FileName);
            AjaxOut UploadAjaxOut = CallTempServiceUtility.UploadTemp(ORenderInfo, UploadTempInfo, OutStream.ToArray());
            try
            {
                System.IO.File.Delete(FileName);
            }
            catch { }
            return UploadAjaxOut;
        }

        public AjaxOut Print(RenderInfoCls ORenderInfo, string LoginName, string _XMLReportFile, string _ReportName, Document reportDocument, string FileName)
        {
            byte[] Bytes;
            using (var OutStream = new MemoryStream())
            {
                ((Document)reportDocument).Save(OutStream, Aspose.Words.SaveFormat.Pdf);
                OutStream.Seek(0, SeekOrigin.Begin);
                Bytes = OutStream.ToArray();
            }

            //using (var OutStream = new MemoryStream())
            //{
            //    ((Document)reportDocument).Save(OutStream, Aspose.Words.SaveFormat.Pdf);
            //    OutStream.Seek(0, SeekOrigin.Begin);
            //    Bytes = OutStream.ToArray();
            //}

            MediaInfoCls
               UploadTempInfo = new MediaInfoCls();
            UploadTempInfo.MediaInfoId = System.Guid.NewGuid().ToString();
            UploadTempInfo.Month = System.DateTime.Now.Month;
            UploadTempInfo.Year = System.DateTime.Now.Year;
            UploadTempInfo.UploadFileName = new System.IO.FileInfo(FileName).Name;
            UploadTempInfo.LoginName = LoginName;
            //UploadTempInfo.Section = "DOCX";
            UploadTempInfo.Section = "PDF";

            var retAjaxOut = CallTempServiceUtility.UploadTemp(ORenderInfo, UploadTempInfo, Bytes);
            retAjaxOut.RetUrl = "http://" + WebConfig.BaseSiteUrl + retAjaxOut.RetUrl;
            return retAjaxOut;
        }

        public AjaxOut Export(RenderInfoCls ORenderInfo, string LoginName, string _XMLReportFile, string _ReportName, Document reportDocument, string FileName)
        {
            byte[] Bytes;
            using (var OutStream = new MemoryStream())
            {
                ((Document)reportDocument).Save(OutStream, Aspose.Words.SaveFormat.Docx);
                OutStream.Seek(0, SeekOrigin.Begin);
                Bytes = OutStream.ToArray();
            }

            //using (var OutStream = new MemoryStream())
            //{
            //    ((Document)reportDocument).Save(OutStream, Aspose.Words.SaveFormat.Pdf);
            //    OutStream.Seek(0, SeekOrigin.Begin);
            //    Bytes = OutStream.ToArray();
            //}

            MediaInfoCls
               UploadTempInfo = new MediaInfoCls();
            UploadTempInfo.MediaInfoId = System.Guid.NewGuid().ToString();
            UploadTempInfo.Month = System.DateTime.Now.Month;
            UploadTempInfo.Year = System.DateTime.Now.Year;
            UploadTempInfo.UploadFileName = new System.IO.FileInfo(FileName).Name;
            UploadTempInfo.LoginName = LoginName;
            UploadTempInfo.Section = "DOCX";
            //UploadTempInfo.Section = "PDF";

            var retAjaxOut = CallTempServiceUtility.UploadTemp(ORenderInfo, UploadTempInfo, Bytes);
            retAjaxOut.RetUrl = "http://" + WebConfig.BaseSiteUrl + retAjaxOut.RetUrl;
            return retAjaxOut;
        }

        public Stream ExecuteStream(RenderInfoCls ORenderInfo, string LoginName, string _XMLReportFile, string _ReportName, Document reportDocument, string FileName)
        {
            var OutStream = new MemoryStream();
            ((Document)reportDocument).Save(OutStream, Aspose.Words.SaveFormat.Pdf);
            OutStream.Seek(0, SeekOrigin.Begin);
            return OutStream;
        }
    }
}
