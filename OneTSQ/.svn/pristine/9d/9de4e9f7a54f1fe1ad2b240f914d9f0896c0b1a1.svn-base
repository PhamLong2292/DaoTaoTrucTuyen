using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using FlexCel.Report;
using System.Data;
using OneTSQ.Utility;
using OneTSQ.Core.Model;
using System.IO;
using FlexCel.XlsAdapter;
using Aspose.Words;
using OneTSQ.ReportUtility.Utility;
using OneTSQ.ReportUtility;
using Aspose.Words.Reporting;
using OneTSQ.Utilities;
using OneTSQ.CallTempService;
using System.Reflection;
using FlexCel.Core;
using FlexCel.Render;
using System.Xml.Linq;
using Aspose.Words.Drawing;

namespace OneTSQ.ReportUtility
{
    [Serializable, DataContract(IsReference = true)]
    public abstract class Report : IReport
    {
        protected Report()
        {
            var type = this.GetType();
            this.Attribute = (ReportAttribute)type.GetCustomAttributes(typeof(ReportAttribute), true).Single();
            var reportType = type;
        }

        public enum eType
        {
            Flexcel = 1,
            ComponentOne,
            Word,
        }

        //public enum eLoaiPhieu
        //{
        //    //Thuong = 0,
        //    PhieuChiDinhDichVu,//1
        //    PhieuKetQua,//2
        //    GiayToKemTheo,//3
        //    DonThuoc,//4
        //    BangKe,//5
        //    Khac,//6
        //}

        //public static Dictionary<eLoaiPhieu, string> dicLoaiPhieu = new Dictionary<eLoaiPhieu, string>()
        //{
        //    { eLoaiPhieu.PhieuChiDinhDichVu, "Phiếu chỉ định dịch vụ" },
        //    { eLoaiPhieu.PhieuKetQua, "Phiếu kết quả" },
        //    { eLoaiPhieu.GiayToKemTheo, "Giấy tờ kèm theo" },
        //    { eLoaiPhieu.DonThuoc, "Đơn thuốc" },
        //    { eLoaiPhieu.BangKe, "Phiếu thanh toán / Bảng kê" },
        //    { eLoaiPhieu.Khac, "Các phiếu in chung khác" },
        //};

        [DataMember]

        public readonly ReportAttribute Attribute;
        ReportAttribute IReport.Attibute
        {
            get { return this.Attribute; }
        }
        public abstract AjaxOut OnLoad(RenderInfoCls ORenderInfo, object filter);
        public abstract AjaxOut OnLoad(RenderInfoCls ORenderInfo, object filter, object[] parameters);
        public abstract Stream OnLoadStream(RenderInfoCls ORenderInfo, object filter);
        public abstract Stream OnLoadStream(RenderInfoCls ORenderInfo, object filter, object[] parameters);
        public abstract AjaxOut OnExport(RenderInfoCls ORenderInfo, object filter);
        public abstract AjaxOut OnPrint(RenderInfoCls ORenderInfo, object filter);
        public abstract AjaxOut OnPrint(RenderInfoCls ORenderInfo, object filter, object[] parameters);
        public virtual bool CanExecute(RenderInfoCls ORenderInfo, string id)
        {
            return true;
        }
        public virtual bool CanExecute(RenderInfoCls ORenderInfo, object filter, object[] parameters)
        {
            return true;
        }
        [DataMember]
        public abstract Type ReportType { get; }
        Type IReport.ReportType
        {
            get { return this.ReportType; }
        }
        //public abstract bool CanExecute(RenderInfoCls ORenderInfo, string id);
        public virtual bool AllowCustomReportPath { get { return false; } }
        public abstract XlsFile OnFinalize(RenderInfoCls ORenderInfo, XlsFile reportDocument);
        public virtual bool AllowBase64String { get { return false; } }
        public virtual bool AllowExport { get { return false; } }
        public virtual bool IsMultiTemplate { get { return false; } }

        //public virtual eLoaiPhieu LoaiPhieu
        //{
        //    get
        //    {
        //        return Report.eLoaiPhieu.Khac;
        //    }
        //}
    }

    [Serializable, DataContract]
    public abstract class ReportT<T> : Report
    {
        sealed class FieldMergingCallback : IFieldMergingCallback
        {
            private readonly Report wordReport;
            private readonly object filter;
            private readonly RenderInfoCls _renderInfo;
            public FieldMergingCallback(RenderInfoCls ORenderInfo, Report report, object filter)
            {
                this._renderInfo = ORenderInfo;
                this.filter = filter;
                this.wordReport = report;
            }
            //private DocumentBuilder mBuilder;
            private const char speechmarkChar = '"';
            void IFieldMergingCallback.FieldMerging(FieldMergingArgs e)
            {
                //if (e.FieldValue != null && (e.FieldValue as string) != null && (e.FieldValue as string).Contains(speechmarkChar))
                //{
                //    if (mBuilder == null)
                //        mBuilder = new DocumentBuilder(e.Document);
                //    // Manually insert the field result while replacing any speechmarks with quote fields.
                //    mBuilder.MoveTo(e.Field.Start);
                //    foreach (char c in (e.FieldValue as string))
                //    {
                //        if (c == speechmarkChar)
                //            mBuilder.InsertField("QUOTE 34");
                //        else
                //            mBuilder.Write(c.ToString());
                //    }
                //    // We have already inserted the text so we can just remove the merge field.
                //    e.Text = string.Empty;
                //}

                if (e.FieldValue != null && e.DocumentFieldName.Length > e.FieldName.Length)
                {
                    switch (e.DocumentFieldName.Substring(0, e.DocumentFieldName.Length - e.FieldName.Length).ToUpperInvariant())
                    {
                        case "RICH:":
                            if (e.FieldValue is long || e.FieldValue is byte[])
                                ((ReportT<T>)this.wordReport).OnCreateWord(e.Document, this._renderInfo, this.filter, e.DocumentFieldName, e.FieldName, e.FieldValue);
                            else if (e.FieldValue is string && !String.IsNullOrEmpty((string)e.FieldValue))
                            {
                                if (((string)e.FieldValue).StartsWith(@"{\rtf"))
                                    ((ReportT<T>)this.wordReport).OnCreateRtf(e.Document, this._renderInfo, this.filter, e.DocumentFieldName, e.FieldName, e.FieldValue);
                                else
                                    ((ReportT<T>)this.wordReport).OnCreateHtml(e.Document, this._renderInfo, this.filter, e.DocumentFieldName, e.FieldName, e.FieldValue);
                            }
                            break;
                        case "HTML:":
                            ((ReportT<T>)this.wordReport).OnCreateHtml(e.Document, this._renderInfo, this.filter, e.DocumentFieldName, e.FieldName, e.FieldValue);
                            break;
                        case "RTF:":
                            ((ReportT<T>)this.wordReport).OnCreateRtf(e.Document, this._renderInfo, this.filter, e.DocumentFieldName, e.FieldName, e.FieldValue);
                            break;
                        case "WORD:":
                            ((ReportT<T>)this.wordReport).OnCreateWord(e.Document, this._renderInfo, this.filter, e.DocumentFieldName, e.FieldName, e.FieldValue);
                            break;
                    }
                }
            }

            void IFieldMergingCallback.ImageFieldMerging(ImageFieldMergingArgs e)
            {
                if (e.FieldValue != null)
                    ((ReportT<T>)this.wordReport).OnCreateImage(e.Document, this._renderInfo, this.filter, e.FieldName, e.FieldName, false, e.FieldValue);
            }
        }
        public virtual string GetCustomReportPath(RenderInfoCls ORenderInfo, T filter)
        {
            return this.Attribute.ReportPath;
        }
        public sealed override AjaxOut OnLoad(RenderInfoCls ORenderInfo, object filter)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string templatePath = string.Empty;

                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                if (this.AllowCustomReportPath)
                    templatePath = OSiteParam.PathRoot + GetCustomReportPath(ORenderInfo, (T)filter);
                else
                    templatePath = OSiteParam.PathRoot + this.Attribute.ReportPath;

                string reportName = this.Attribute.Name;
                string Id = reportName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                string LoginName = ORenderInfo.LoginName;//string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string SaveFile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot + "\\" + Id);
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);
                if (this.Attribute.Type == eType.Flexcel)
                {
                    if (!this.IsMultiTemplate)
                    {
                        XlsFile Result = new XlsFile(true);
                        Result.Open(templatePath);
                        // Cho phép build lại file ExcelTemplate trong 1 số trường hợp báo cáo động như: Phiếu Công khai thuốc, Sổ tổng hợp thuốc....
                        this.OnBuildExcel(ORenderInfo, ref Result, (T)filter);

                        FlexCelReport OflexCelReport = this.OnFlexcelBuild(ORenderInfo, (T)filter);
                        OflexCelReport.GetImageData += new GetImageDataEventHandler((o, e) =>
                        {
                            if (e.ImageName != null && !String.IsNullOrWhiteSpace(e.ImageName))
                            {
                                double width = e.Width, height = e.Height;
                                var barcodeInfo = !e.ImageName.EndsWith("|") ? null : ReportBarcodeManager.GetBarcodeInfo(this.Attribute.ID, Result, e.ImageName); // Maybe BARCODE
                                var data = barcodeInfo != null ?
                                    this.OnLoadBarcode(filter, barcodeInfo.SymbologyIndex, e.ImageName.Substring(0, e.ImageName.Length - barcodeInfo.Tail.Length), ref width, ref height) :
                                    this.OnLoadImage(filter, e.ImageName, ref width, ref height);
                                e.ImageData = data;
                                if (data != null)
                                {
                                    if (e.Width != width)
                                        e.Width = width;
                                    if (e.Height != height)
                                        e.Height = height;
                                }
                                else if (barcodeInfo != null)
                                    ReportBarcodeManager.DeleteImage(this.Attribute.ID, e.ImageName);
                            }
                        });
                        RetAjaxOut = new OneTSQ.ReportUtility.FlexcelReportUtility().Execute(ORenderInfo, LoginName, templatePath, reportName, OflexCelReport, SaveFile, Result, this, this.AllowBase64String);
                    }
                    else
                    {
                        return this.ExecuteMultiTemplate(ORenderInfo, (T)filter);

                    }
                }
                else if (this.Attribute.Type == eType.ComponentOne)
                {
                    RetAjaxOut = new OneTSQ.ReportUtility.C1ReportUtility().Execute(ORenderInfo, LoginName, templatePath, reportName, this.OnC1Build(ORenderInfo, (T)filter), SaveFile);
                }
                else if (this.Attribute.Type == eType.Word)
                {
                    var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
                    Document reportDocument = new Document(stream);
                    foreach (var fieldname in reportDocument.MailMerge.GetFieldNames())
                    {
                        var valIndex = fieldname.IndexOf(':');
                        if (valIndex > 0)
                            reportDocument.MailMerge.MappedDataFields.Add(fieldname, fieldname.Substring(valIndex + 1));
                    }
                    reportDocument.MailMerge.FieldMergingCallback = new FieldMergingCallback(ORenderInfo, this, filter);
                    RetAjaxOut = new OneTSQ.ReportUtility.WordReportUtility().Execute(ORenderInfo, LoginName, templatePath, reportName, this.OnWordBuild(ORenderInfo, ref reportDocument, (T)filter), SaveFile);
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = string.IsNullOrEmpty(ex.Message) ? ex.ToString() : ex.Message;
                RetAjaxOut.HtmlContent = ex.ToString();
            }

            return RetAjaxOut;
        }
        public sealed override Stream OnLoadStream(RenderInfoCls ORenderInfo, object filter)
        {
            try
            {
                string templatePath = string.Empty;

                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                if (this.AllowCustomReportPath)
                    templatePath = OSiteParam.PathRoot + GetCustomReportPath(ORenderInfo, (T)filter);
                else
                    templatePath = OSiteParam.PathRoot + this.Attribute.ReportPath;

                string reportName = this.Attribute.Name;
                string Id = reportName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                string LoginName = ORenderInfo.LoginName;//string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string SaveFile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot + "\\" + Id);
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);

                if (this.Attribute.Type == eType.Flexcel)
                {
                    if (!this.IsMultiTemplate)
                    {
                        XlsFile Result = new XlsFile(true);
                        Result.Open(templatePath);
                        //Cho phép build lại file ExcelTemplate trong 1 số trường hợp báo cáo động như: Phiếu Công khai thuốc, Sổ tổng hợp thuốc....
                        this.OnBuildExcel(ORenderInfo, ref Result, (T)filter);

                        FlexCelReport OflexCelReport = this.OnFlexcelBuild(ORenderInfo, (T)filter);
                        OflexCelReport.GetImageData += new GetImageDataEventHandler((o, e) =>
                        {
                            if (e.ImageName != null && !String.IsNullOrWhiteSpace(e.ImageName))
                            {
                                double width = e.Width, height = e.Height;
                                var barcodeInfo = !e.ImageName.EndsWith("|") ? null : ReportBarcodeManager.GetBarcodeInfo(this.Attribute.ID, Result, e.ImageName); // Maybe BARCODE
                                var data = barcodeInfo != null ?
                                        this.OnLoadBarcode(filter, barcodeInfo.SymbologyIndex, e.ImageName.Substring(0, e.ImageName.Length - barcodeInfo.Tail.Length), ref width, ref height) :
                                        this.OnLoadImage(filter, e.ImageName, ref width, ref height);
                                e.ImageData = data;
                                if (data != null)
                                {
                                    if (e.Width != width)
                                        e.Width = width;
                                    if (e.Height != height)
                                        e.Height = height;
                                }
                                else if (barcodeInfo != null)
                                    ReportBarcodeManager.DeleteImage(this.Attribute.ID, e.ImageName);
                            }
                        });
                        return new OneTSQ.ReportUtility.FlexcelReportUtility().ExecuteStream(ORenderInfo, LoginName, templatePath, reportName, OflexCelReport, SaveFile, Result, this, this.AllowBase64String);
                    }
                    else
                    {
                        return this.OnLoadStreamMultiTemplate(ORenderInfo, (T)filter);
                    }
                }
                else if (this.Attribute.Type == eType.Word)
                {
                    if (!this.IsMultiTemplate)
                    {
                        var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
                        Document reportDocument = new Document(stream);
                        foreach (var fieldname in reportDocument.MailMerge.GetFieldNames())
                        {
                            var valIndex = fieldname.IndexOf(':');
                            if (valIndex > 0)
                                reportDocument.MailMerge.MappedDataFields.Add(fieldname, fieldname.Substring(valIndex + 1));
                        }
                        reportDocument.MailMerge.FieldMergingCallback = new FieldMergingCallback(ORenderInfo, this, filter);
                        return new OneTSQ.ReportUtility.WordReportUtility().ExecuteStream(ORenderInfo, LoginName, templatePath, reportName, this.OnWordBuild(ORenderInfo, ref reportDocument, (T)filter), SaveFile);
                    }
                    else
                    {
                        return this.OnLoadStreamMultiTemplate(ORenderInfo, (T)filter);
                    }
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public sealed override AjaxOut OnExport(RenderInfoCls ORenderInfo, object filter)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string templatePath = string.Empty;
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                if (this.AllowCustomReportPath)
                    templatePath = GetCustomReportPath(ORenderInfo, (T)filter);
                else
                    templatePath = OSiteParam.PathRoot + this.Attribute.ReportPath;
                string reportName = this.Attribute.Name;
                string Id = reportName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                string LoginName = ORenderInfo.LoginName;//string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string SaveFile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot + "\\" + Id);
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);
                if (this.Attribute.Type == eType.Flexcel)
                {
                    XlsFile Result = new XlsFile(true);
                    Result.Open(templatePath);
                    // Cho phép build lại file ExcelTemplate trong 1 số trường hợp báo cáo động như: Phiếu Công khai thuốc, Sổ tổng hợp thuốc....
                    this.OnBuildExcel(ORenderInfo, ref Result, (T)filter);

                    FlexCelReport OflexCelReport = this.OnFlexcelBuild(ORenderInfo, (T)filter);
                    OflexCelReport.GetImageData += new GetImageDataEventHandler((o, e) =>
                    {
                        if (e.ImageName != null && !String.IsNullOrWhiteSpace(e.ImageName))
                        {
                            double width = e.Width, height = e.Height;
                            var barcodeInfo = !e.ImageName.EndsWith("|") ? null : ReportBarcodeManager.GetBarcodeInfo(this.Attribute.ID, Result, e.ImageName); // Maybe BARCODE
                            var data = barcodeInfo != null ?
                                this.OnLoadBarcode(filter, barcodeInfo.SymbologyIndex, e.ImageName.Substring(0, e.ImageName.Length - barcodeInfo.Tail.Length), ref width, ref height) :
                                this.OnLoadImage(filter, e.ImageName, ref width, ref height);
                            e.ImageData = data;
                            if (data != null)
                            {
                                if (e.Width != width)
                                    e.Width = width;
                                if (e.Height != height)
                                    e.Height = height;
                            }
                            else if (barcodeInfo != null)
                                ReportBarcodeManager.DeleteImage(this.Attribute.ID, e.ImageName);
                        }
                    });
                    RetAjaxOut = DoExport(ORenderInfo, LoginName, templatePath, reportName, OflexCelReport, SaveFile, Result, this);
                }
                else if (this.Attribute.Type == eType.ComponentOne)
                {
                    RetAjaxOut = new OneTSQ.ReportUtility.C1ReportUtility().Execute(ORenderInfo, LoginName, templatePath, reportName, this.OnC1Build(ORenderInfo, (T)filter), SaveFile);
                }
                else if (this.Attribute.Type == eType.Word)
                {
                    var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
                    Document reportDocument = new Document(stream);
                    foreach (var fieldname in reportDocument.MailMerge.GetFieldNames())
                    {
                        var valIndex = fieldname.IndexOf(':');
                        if (valIndex > 0)
                            reportDocument.MailMerge.MappedDataFields.Add(fieldname, fieldname.Substring(valIndex + 1));
                    }
                    reportDocument.MailMerge.FieldMergingCallback = new FieldMergingCallback(ORenderInfo, this, filter);
                    RetAjaxOut = new OneTSQ.ReportUtility.WordReportUtility().Export(ORenderInfo, LoginName, templatePath, reportName, this.OnWordBuild(ORenderInfo, ref reportDocument, (T)filter), SaveFile);
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = string.IsNullOrEmpty(ex.Message) ? ex.ToString() : ex.Message;
                RetAjaxOut.HtmlContent = ex.ToString();
            }

            return RetAjaxOut;
        }
        public sealed override AjaxOut OnPrint(RenderInfoCls ORenderInfo, object filter)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                string templatePath = OSiteParam.PathRoot + this.Attribute.ReportPath;
                string reportName = this.Attribute.Name;
                string Id = reportName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string LoginName = ORenderInfo.LoginName;//string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string SaveFile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot + "\\" + Id);
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);
                if (this.Attribute.Type == eType.Flexcel)
                {
                    SaveFile += ".pdf";
                    //SaveFile += ".xlsx";
                    XlsFile Result = new XlsFile(true);
                    Result.Open(templatePath);
                    // Cho phép build lại file ExcelTemplate trong 1 số trường hợp báo cáo động như: Phiếu Công khai thuốc, Sổ tổng hợp thuốc....
                    this.OnBuildExcel(ORenderInfo, ref Result, (T)filter);

                    FlexCelReport OflexCelReport = this.OnFlexcelBuild(ORenderInfo, (T)filter);
                    OflexCelReport.GetImageData += new GetImageDataEventHandler((o, e) =>
                    {
                        if (e.ImageName != null && !String.IsNullOrWhiteSpace(e.ImageName))
                        {
                            double width = e.Width, height = e.Height;
                            var barcodeInfo = !e.ImageName.EndsWith("|") ? null : ReportBarcodeManager.GetBarcodeInfo(this.Attribute.ID, Result, e.ImageName); // Maybe BARCODE
                            var data = barcodeInfo != null ?
                                this.OnLoadBarcode(filter, barcodeInfo.SymbologyIndex, e.ImageName.Substring(0, e.ImageName.Length - barcodeInfo.Tail.Length), ref width, ref height) :
                                this.OnLoadImage(filter, e.ImageName, ref width, ref height);
                            e.ImageData = data;
                            if (data != null)
                            {
                                if (e.Width != width)
                                    e.Width = width;
                                if (e.Height != height)
                                    e.Height = height;
                            }
                            else if (barcodeInfo != null)
                                ReportBarcodeManager.DeleteImage(this.Attribute.ID, e.ImageName);
                        }
                    });
                    RetAjaxOut = Print(ORenderInfo, LoginName, templatePath, reportName, OflexCelReport, SaveFile, Result, this);
                }
                else if (this.Attribute.Type == eType.ComponentOne)
                {
                    //Chua lam
                    //RetAjaxOut = new OneTSQ.ReportUtility.C1ReportUtility().Execute(ORenderInfo, LoginName, templatePath, reportName, this.OnC1Build(ORenderInfo, (T)filter), SaveFile);
                }
                else if (this.Attribute.Type == eType.Word)
                {
                    //SaveFile += ".docx";
                    SaveFile += ".pdf";
                    var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
                    Document reportDocument = new Document(stream);
                    foreach (var fieldname in reportDocument.MailMerge.GetFieldNames())
                    {
                        var valIndex = fieldname.IndexOf(':');
                        if (valIndex > 0)
                            reportDocument.MailMerge.MappedDataFields.Add(fieldname, fieldname.Substring(valIndex + 1));
                    }
                    reportDocument.MailMerge.FieldMergingCallback = new FieldMergingCallback(ORenderInfo, this, filter);

                    RetAjaxOut = new OneTSQ.ReportUtility.WordReportUtility().Print(ORenderInfo, LoginName, templatePath, reportName, this.OnWordBuild(ORenderInfo, ref reportDocument, (T)filter), SaveFile);
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = string.IsNullOrEmpty(ex.Message) ? ex.ToString() : ex.Message;
                RetAjaxOut.HtmlContent = ex.ToString();
            }

            return RetAjaxOut;
        }

        #region DirectCall
        public sealed override AjaxOut OnLoad(RenderInfoCls ORenderInfo, object filter, object[] parameters)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string templatePath = string.Empty;

                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                if (this.AllowCustomReportPath)
                    templatePath = OSiteParam.PathRoot + GetCustomReportPath(ORenderInfo, (T)filter);
                else
                    templatePath = OSiteParam.PathRoot + this.Attribute.ReportPath;

                string reportName = this.Attribute.Name;
                string Id = reportName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                string LoginName = ORenderInfo.LoginName;//string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string SaveFile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot + "\\" + Id);
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);
                if (this.Attribute.Type == eType.Flexcel)
                {
                    if (!this.IsMultiTemplate)
                    {
                        XlsFile Result = new XlsFile(true);
                        Result.Open(templatePath);
                        // Cho phép build lại file ExcelTemplate trong 1 số trường hợp báo cáo động như: Phiếu Công khai thuốc, Sổ tổng hợp thuốc....
                        this.OnBuildExcel(ORenderInfo, ref Result, (T)filter);

                        FlexCelReport OflexCelReport = this.OnFlexcelBuild(ORenderInfo, (T)filter, parameters);
                        OflexCelReport.GetImageData += new GetImageDataEventHandler((o, e) =>
                        {
                            if (e.ImageName != null && !String.IsNullOrWhiteSpace(e.ImageName))
                            {
                                double width = e.Width, height = e.Height;
                                var barcodeInfo = !e.ImageName.EndsWith("|") ? null : ReportBarcodeManager.GetBarcodeInfo(this.Attribute.ID, Result, e.ImageName); // Maybe BARCODE
                                var data = barcodeInfo != null ?
                                    this.OnLoadBarcode(filter, barcodeInfo.SymbologyIndex, e.ImageName.Substring(0, e.ImageName.Length - barcodeInfo.Tail.Length), ref width, ref height) :
                                    this.OnLoadImage(filter, e.ImageName, ref width, ref height);
                                e.ImageData = data;
                                if (data != null)
                                {
                                    if (e.Width != width)
                                        e.Width = width;
                                    if (e.Height != height)
                                        e.Height = height;
                                }
                                else if (barcodeInfo != null)
                                    ReportBarcodeManager.DeleteImage(this.Attribute.ID, e.ImageName);
                            }
                        });
                        RetAjaxOut = new OneTSQ.ReportUtility.FlexcelReportUtility().Execute(ORenderInfo, LoginName, templatePath, reportName, OflexCelReport, SaveFile, Result, this, this.AllowBase64String);
                    }
                    else
                    {
                        return this.ExecuteMultiTemplate(ORenderInfo, (T)filter, parameters);

                    }
                }
                else if (this.Attribute.Type == eType.ComponentOne)
                {
                    RetAjaxOut = new OneTSQ.ReportUtility.C1ReportUtility().Execute(ORenderInfo, LoginName, templatePath, reportName, this.OnC1Build(ORenderInfo, (T)filter), SaveFile);
                }
                else if (this.Attribute.Type == eType.Word)
                {
                    var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
                    Document reportDocument = new Document(stream);
                    foreach (var fieldname in reportDocument.MailMerge.GetFieldNames())
                    {
                        var valIndex = fieldname.IndexOf(':');
                        if (valIndex > 0)
                            reportDocument.MailMerge.MappedDataFields.Add(fieldname, fieldname.Substring(valIndex + 1));
                    }
                    reportDocument.MailMerge.FieldMergingCallback = new FieldMergingCallback(ORenderInfo, this, filter);
                    RetAjaxOut = new OneTSQ.ReportUtility.WordReportUtility().Execute(ORenderInfo, LoginName, templatePath, reportName, this.OnWordBuild(ORenderInfo, ref reportDocument, (T)filter), SaveFile);
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = string.IsNullOrEmpty(ex.Message) ? ex.ToString() : ex.Message;
                RetAjaxOut.HtmlContent = ex.ToString();
            }

            return RetAjaxOut;
        }
        public sealed override Stream OnLoadStream(RenderInfoCls ORenderInfo, object filter, object[] parameters)
        {
            try
            {
                string templatePath = string.Empty;

                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                if (this.AllowCustomReportPath)
                    templatePath = OSiteParam.PathRoot + GetCustomReportPath(ORenderInfo, (T)filter);
                else
                    templatePath = OSiteParam.PathRoot + this.Attribute.ReportPath;

                string reportName = this.Attribute.Name;
                string Id = reportName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                string LoginName = ORenderInfo.LoginName;//string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string SaveFile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot + "\\" + Id);
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);

                if (this.Attribute.Type == eType.Flexcel)
                {
                    if (!this.IsMultiTemplate)
                    {
                        XlsFile Result = new XlsFile(true);
                        Result.Open(templatePath);
                        //Cho phép build lại file ExcelTemplate trong 1 số trường hợp báo cáo động như: Phiếu Công khai thuốc, Sổ tổng hợp thuốc....
                        this.OnBuildExcel(ORenderInfo, ref Result, (T)filter);

                        FlexCelReport OflexCelReport = this.OnFlexcelBuild(ORenderInfo, (T)filter, parameters);

                        OflexCelReport.GetImageData += new GetImageDataEventHandler((o, e) =>
                        {
                            if (e.ImageName != null && !String.IsNullOrWhiteSpace(e.ImageName))
                            {
                                double width = e.Width, height = e.Height;
                                var barcodeInfo = !e.ImageName.EndsWith("|") ? null : ReportBarcodeManager.GetBarcodeInfo(this.Attribute.ID, Result, e.ImageName); // Maybe BARCODE
                                var data = barcodeInfo != null ?
                                        this.OnLoadBarcode(filter, barcodeInfo.SymbologyIndex, e.ImageName.Substring(0, e.ImageName.Length - barcodeInfo.Tail.Length), ref width, ref height) :
                                        this.OnLoadImage(filter, e.ImageName, ref width, ref height);
                                e.ImageData = data;
                                if (data != null)
                                {
                                    if (e.Width != width)
                                        e.Width = width;
                                    if (e.Height != height)
                                        e.Height = height;
                                }
                                else if (barcodeInfo != null)
                                    ReportBarcodeManager.DeleteImage(this.Attribute.ID, e.ImageName);
                            }
                        });
                        return new OneTSQ.ReportUtility.FlexcelReportUtility().ExecuteStream(ORenderInfo, LoginName, templatePath, reportName, OflexCelReport, SaveFile, Result, this, this.AllowBase64String);
                    }
                    else
                    {
                        return this.OnLoadStreamMultiTemplate(ORenderInfo, (T)filter);
                    }
                }
                else if (this.Attribute.Type == eType.Word)
                {
                    if (!this.IsMultiTemplate)
                    {
                        var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
                        Document reportDocument = new Document(stream);
                        foreach (var fieldname in reportDocument.MailMerge.GetFieldNames())
                        {
                            var valIndex = fieldname.IndexOf(':');
                            if (valIndex > 0)
                                reportDocument.MailMerge.MappedDataFields.Add(fieldname, fieldname.Substring(valIndex + 1));
                        }
                        reportDocument.MailMerge.FieldMergingCallback = new FieldMergingCallback(ORenderInfo, this, filter);
                        return new OneTSQ.ReportUtility.WordReportUtility().ExecuteStream(ORenderInfo, LoginName, templatePath, reportName, this.OnWordBuild(ORenderInfo, ref reportDocument, (T)filter), SaveFile);
                    }
                    else
                    {
                        return this.OnLoadStreamMultiTemplate(ORenderInfo, (T)filter);
                    }
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Logging.Instance.Logger.Error(ex.ToString());
                return null;
            }
        }
        public sealed override AjaxOut OnPrint(RenderInfoCls ORenderInfo, object filter, object[] parameters)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                string templatePath = OSiteParam.PathRoot + this.Attribute.ReportPath;
                string reportName = this.Attribute.Name;
                string Id = reportName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string LoginName = ORenderInfo.LoginName;//string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string SaveFile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot + "\\" + Id);
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);
                if (this.Attribute.Type == eType.Flexcel)
                {
                    SaveFile += ".pdf";
                    //SaveFile += ".xlsx";
                    XlsFile Result = new XlsFile(true);
                    Result.Open(templatePath);
                    // Cho phép build lại file ExcelTemplate trong 1 số trường hợp báo cáo động như: Phiếu Công khai thuốc, Sổ tổng hợp thuốc....
                    this.OnBuildExcel(ORenderInfo, ref Result, (T)filter);

                    FlexCelReport OflexCelReport = this.OnFlexcelBuild(ORenderInfo, (T)filter, parameters);
                    OflexCelReport.GetImageData += new GetImageDataEventHandler((o, e) =>
                    {
                        if (e.ImageName != null && !String.IsNullOrWhiteSpace(e.ImageName))
                        {
                            double width = e.Width, height = e.Height;
                            var barcodeInfo = !e.ImageName.EndsWith("|") ? null : ReportBarcodeManager.GetBarcodeInfo(this.Attribute.ID, Result, e.ImageName); // Maybe BARCODE
                            var data = barcodeInfo != null ?
                                this.OnLoadBarcode(filter, barcodeInfo.SymbologyIndex, e.ImageName.Substring(0, e.ImageName.Length - barcodeInfo.Tail.Length), ref width, ref height) :
                                this.OnLoadImage(filter, e.ImageName, ref width, ref height);
                            e.ImageData = data;
                            if (data != null)
                            {
                                if (e.Width != width)
                                    e.Width = width;
                                if (e.Height != height)
                                    e.Height = height;
                            }
                            else if (barcodeInfo != null)
                                ReportBarcodeManager.DeleteImage(this.Attribute.ID, e.ImageName);
                        }
                    });
                    RetAjaxOut = Print(ORenderInfo, LoginName, templatePath, reportName, OflexCelReport, SaveFile, Result, this);
                }
                else if (this.Attribute.Type == eType.ComponentOne)
                {
                    //Chua lam
                    //RetAjaxOut = new OneTSQ.ReportUtility.C1ReportUtility().Execute(ORenderInfo, LoginName, templatePath, reportName, this.OnC1Build(ORenderInfo, (T)filter), SaveFile);
                }
                else if (this.Attribute.Type == eType.Word)
                {
                    //SaveFile += ".docx";
                    SaveFile += ".pdf";
                    var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
                    Document reportDocument = new Document(stream);
                    foreach (var fieldname in reportDocument.MailMerge.GetFieldNames())
                    {
                        var valIndex = fieldname.IndexOf(':');
                        if (valIndex > 0)
                            reportDocument.MailMerge.MappedDataFields.Add(fieldname, fieldname.Substring(valIndex + 1));
                    }
                    reportDocument.MailMerge.FieldMergingCallback = new FieldMergingCallback(ORenderInfo, this, filter);

                    RetAjaxOut = new OneTSQ.ReportUtility.WordReportUtility().Print(ORenderInfo, LoginName, templatePath, reportName, this.OnWordBuild(ORenderInfo, ref reportDocument, (T)filter), SaveFile);
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = string.IsNullOrEmpty(ex.Message) ? ex.ToString() : ex.Message;
                RetAjaxOut.HtmlContent = ex.ToString();
            }

            return RetAjaxOut;
        }
        #endregion
        public AjaxOut DoExport(RenderInfoCls ORenderInfo, string LoginName, string templatePath, string _ReportName, FlexCelReport flexCelReport, string FileName, XlsFile Result = null, Report report = null)
        {
            Byte[] Bytes;
            Assembly a = Assembly.GetExecutingAssembly();
            FileInfo f = new FileInfo(templatePath);

            flexCelReport.Run(Result);

            if (report != null)
            {
                Result = report.OnFinalize(ORenderInfo, Result);
            }

            using (MemoryStream OutStream = new MemoryStream())
            {
                Result.Save(OutStream, TFileFormats.Xlsx);
                OutStream.Position = 0;
                Bytes = OutStream.ToArray();
            }

            MediaInfoCls
                UploadTempInfo = new MediaInfoCls();
            UploadTempInfo.MediaInfoId = System.Guid.NewGuid().ToString();
            UploadTempInfo.Month = System.DateTime.Now.Month;
            UploadTempInfo.Year = System.DateTime.Now.Year;
            UploadTempInfo.UploadFileName = new System.IO.FileInfo(FileName).Name;
            UploadTempInfo.LoginName = LoginName;
            UploadTempInfo.Section = "xlsx";

            AjaxOut UploadAjaxOut = CallTempServiceUtility.UploadTemp(ORenderInfo, UploadTempInfo, Bytes);
            try
            {
                System.IO.File.Delete(FileName);
            }
            catch { }
            return UploadAjaxOut;
        }
        public AjaxOut Print(RenderInfoCls ORenderInfo, string LoginName, string templatePath, string _ReportName, FlexCelReport flexCelReport, string FileName, XlsFile Result = null, Report report = null)
        {
            AjaxOut retAjaxOut = new AjaxOut();
            Assembly a = Assembly.GetExecutingAssembly();
            FileInfo f = new FileInfo(templatePath);
            byte[] Bytes;
            flexCelReport.Run(Result);

            if (report != null)
            {
                Result = report.OnFinalize(ORenderInfo, Result);
            }
            //using (MemoryStream OutStream = new MemoryStream())
            //{
            //    Result.Save(OutStream, TFileFormats.Xlsx);
            //    OutStream.Position = 0;
            //    Bytes = OutStream.ToArray();
            //}

            //if (Result != null)
            //{
            //    Result.Save(FileName);
            //    f = new FileInfo(FileName);
            //}

            //using (FileStream InStream = f.OpenRead())
            //{
            //    using (MemoryStream OutStream = new MemoryStream())
            //    {
            //        flexCelReport.Run(InStream, OutStream);
            //        Bytes = OutStream.ToArray();
            //    }
            //}

            using (MemoryStream OutStream = new MemoryStream())
            {
                using (FlexCelPdfExport pdf = new FlexCelPdfExport())
                {
                    pdf.Workbook = Result;
                    pdf.BeginExport(OutStream);
                    pdf.ExportAllVisibleSheets(false, "FlexCel");
                    pdf.EndExport();
                    OutStream.Position = 0;
                    Bytes = OutStream.ToArray();
                }
            }

            MediaInfoCls
                UploadTempInfo = new MediaInfoCls();
            UploadTempInfo.MediaInfoId = System.Guid.NewGuid().ToString();
            UploadTempInfo.Month = System.DateTime.Now.Month;
            UploadTempInfo.Year = System.DateTime.Now.Year;
            UploadTempInfo.UploadFileName = new System.IO.FileInfo(FileName).Name;
            UploadTempInfo.LoginName = LoginName;
            UploadTempInfo.Section = "PDF";
            //UploadTempInfo.Section = "xlsx";
            AjaxOut UploadAjaxOut = CallTempServiceUtility.UploadTemp(ORenderInfo, UploadTempInfo, Bytes);
            try
            {
                System.IO.File.Delete(FileName);
            }
            catch { }
            UploadAjaxOut.RetUrl = "http://" + WebConfig.BaseSiteUrl + UploadAjaxOut.RetUrl;
            return UploadAjaxOut;
        }
        public override XlsFile OnFinalize(RenderInfoCls ORenderInfo, XlsFile reportDocument)
        {
            if (this.AllowAsposeFinalize)
                using (var input = this.ToStream(reportDocument))
                {
                    var workbook = new Aspose.Cells.Workbook(input);
                    this.OnAsposeFinalize(ORenderInfo, workbook);

                    using (var output = new MemoryStream(2048))
                    {
                        workbook.Save(output, FlexcelReportUtility.ASPOSE_SAVEFORMAT);
                        output.Seek(0, SeekOrigin.Begin);

                        var xlsResult2 = new XlsFile();
                        xlsResult2.Open(output);
                        return xlsResult2;
                    }
                }
            return reportDocument;
        }
        protected virtual bool AllowAsposeFinalize
        {
            get { return false; }
        }
        protected virtual void OnAsposeFinalize(RenderInfoCls ORenderInfo, Aspose.Cells.Workbook workbook)
        {
            throw new NotImplementedException();
        }
        public Stream ToStream(object reportDocument)
        {
            var stream = new MemoryStream();
            if (reportDocument != null && reportDocument.GetType() == typeof(XElement))
            {
                ((XElement)reportDocument).Save(stream, SaveOptions.None);
            }
            else
            {
                ((XlsFile)reportDocument).Save(stream, TFileFormats.Xlsx);
            }
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        public virtual byte[] OnLoadImage(object filter, string name, ref double width, ref double height)
        {
            return null;
        }
        public byte[] OnLoadBarcode(object filter, int symbology, string code, ref double width, ref double height)
        {
            if (String.IsNullOrWhiteSpace(code))
                return null;
            using (var builder = AsposeBarcodeDefinitions.CreateBuider(code, symbology, (float)width, (float)height))
            {
                try
                {
                    using (var image = this.OnBuildBarcode(builder, filter))
                    {
                        if (width != image.Width)
                            width = image.Width;
                        //if (height != image.Height)
                        //    height = image.Height;
                        return image.ToData();
                    }

                }
                catch (Aspose.BarCode.BarCodeException)
                {
                    return null;
                }
            }
        }
        public virtual System.Drawing.Bitmap OnBuildBarcode(Aspose.BarCode.BarCodeBuilder builder, object filter)
        {
            return builder.GenerateBarCodeImage();
            //return builder.GetCustomSizeBarCodeImage(new System.Drawing.Size((int)builder.ImageWidth, (int)builder.ImageHeight), false);
        }
        public virtual void OnBuildExcel(RenderInfoCls ORenderInfo, ref XlsFile XlsFile, T filter)
        {
            return;
        }

        #region MergeField - Image

        /// <summary>
        /// Hàm load và tạo Image hoặc Picture rồi đưa vào báo cáo tại vị trí của mergefield
        /// </summary>
        public virtual Shape OnCreateImage(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, bool nullable, object value)
        {
            byte[] data;
            if (value == null)
                data = null;
            //else if (value is long)
            //    data = this.OnLoadImage(document, ORenderInfo, filter, mergefield, field, (long)value);
            //else if (value is string && !String.IsNullOrEmpty((string)value))
            //    data = this.OnLoadImage(document, ORenderInfo, filter, mergefield, field, (string)value);
            else if (value is byte[])
                data = (byte[])value;
            else if (value is string)
            {
                if (field.EndsWith("|"))
                {
                    var barcodeInfo = ReportBarcodeManager.GetBarcodeInfo(this.Attribute.ID, null, (string)value); // Maybe BARCODE
                    double width = 165;
                    double height = 75;
                    data = barcodeInfo != null ?
                        this.OnLoadBarcode(filter, barcodeInfo.SymbologyIndex, (string)value, ref width, ref height)
                        : this.OnLoadBarcode(filter, 6, (string)value, ref width, ref height);
                }
                else data = null;
            }
            else
                data = null;
            var builder = document.ReplaceMergeField(mergefield);
            if (data == null)
                if (nullable)
                    return null;
                else
                    return this.OnInsertImage(builder, ORenderInfo, filter, mergefield, field, value, WordReportUtility.BlankImage);
            try
            {
                return this.OnInsertImage(builder, ORenderInfo, filter, mergefield, field, value, data);
            }
            catch (ArgumentException)
            {
                // INPUT Image Data is wrong format
                if (nullable)
                    return null;
                return this.OnInsertImage(builder, ORenderInfo, filter, mergefield, field, value, WordReportUtility.BlankImage);
            }
        }

        /// <summary>
        /// Hàm nhúng hình ảnh đã load ra dạng data stream vào documentBuilder - thay thế vào MergeField trong báo cáo
        /// Hệ thống mặc định gọi hàm InsertImage: documentBuilder.InsertImage(stream, horzPos: RelativeHorizontalPosition.Column, left: 0.0, vertPos: RelativeVerticalPosition.Paragraph, top: 0.0, width: -1.0, height: -1.0, wrapType: WrapType.Inline),
        /// báo cáo ghi đè hàm này để cung cấp thêm tham số như kích thước và layout
        /// </summary>
        public virtual Shape OnInsertImage(DocumentBuilder documentBuilder, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, object value, byte[] data)
        {
            return documentBuilder.InsertImage(data,
                horzPos: RelativeHorizontalPosition.Column, left: 0.0,
                vertPos: RelativeVerticalPosition.Paragraph, top: 0.0,
                width: -1.0, height: -1.0,
                wrapType: WrapType.Inline);
        }

        /// <summary>
        /// Hàm load ra nội dung hình ảnh nhúng vào báo cáo dựa trên tên hình ảnh (có thể là dạng ID dưới tên - do Aspose.Words chỉ hỗ trợ dạng string)
        /// </summary>
        //public virtual byte[] OnLoadImage(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, string imageName)
        //{
        //    long id;
        //    if (!long.TryParse(imageName, out id) || id <= 0)
        //        return null;
        //    return this.OnLoadImage(document, ORenderInfo, filter, mergefield, field, id);
        //}

        /// <summary>
        /// Hàm lấy ra nội dung hình ảnh nhúng vào báo cáo dựa trên tên hình ảnh
        /// Độ rộng và Chiều cao ảnh có thể chỉnh sửa bằng cách sửa các đầu vào
        /// </summary>
        //public virtual byte[] OnLoadImage(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, long imageID)
        //{
        //    return SystemFiles.LoadData(ORenderInfo, imageID);
        //}

        #endregion

        #region MergeField - Html

        /// <summary>
        /// Hàm load và tạo Html rồi đưa vào báo cáo tại vị trí của mergefield
        /// </summary>
        public virtual void OnCreateHtml(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, object value)
        {
            string html;
            //if (value is long)
            //    html = this.OnLoadHtml(document, ORenderInfo, filter, mergefield, field, (long)value);
            //else 
            if (value is string && !String.IsNullOrEmpty((string)value))
                html = this.OnLoadHtml(document, ORenderInfo, filter, mergefield, field, (string)value);
            else if (value is byte[])
                html = Encoding.UTF8.GetString((byte[])value);
            else
                html = null;
            if (html != null)
                document.ReplaceMergeField(mergefield).InsertHtml(html);
        }

        /// <summary>
        /// Hàm lấy ra nội dung Html nhúng vào báo cáo dựa trên tên của Html (hoặc chính là nội dung Html - mặc định hệ thống coi đây là nội dung của Html)
        /// </summary>
        public virtual string OnLoadHtml(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, string html)
        {
            return html;
        }

        /// <summary>
        /// Hàm lấy ra nội dung Html nhúng vào báo cáo dựa trên ID của file đính kèm Html
        /// </summary>
        //public virtual string OnLoadHtml(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, long htmlID)
        //{
        //    using (var stream = SystemFiles.LoadStream(ORenderInfo, htmlID))
        //    using (var reader = new StreamReader(stream))
        //        return reader.ReadToEnd();
        //}

        #endregion

        #region MergeField - Rtf

        /// <summary>
        /// Hàm load và tạo Rtf rồi đưa vào báo cáo tại vị trí của mergefield
        /// </summary>
        public virtual Document OnCreateRtf(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, object value)
        {
            Stream stream;
            //if (value is long)
            //    stream = this.OnLoadRtf(document, ORenderInfo, filter, mergefield, field, (long)value);
            //else 
            if (value is string && !String.IsNullOrEmpty((string)value))
                stream = this.OnLoadRtf(document, ORenderInfo, filter, mergefield, field, (string)value);
            else if (value is byte[])
                stream = new MemoryStream((byte[])value);
            else
                stream = null;

            if (stream != null)
                using (stream)
                {
                    var rtfDocument = new Document(stream, new Aspose.Words.LoadOptions() { Encoding = Encoding.ASCII, LoadFormat = LoadFormat.Rtf });
                    document.ReplaceMergeField(mergefield).CurrentParagraph.InsertDocument(rtfDocument);
                    return rtfDocument;
                }

            return null;
        }

        /// <summary>
        /// Hàm lấy ra nội dung Rtf nhúng vào báo cáo dựa trên chuỗi đầu vào có thể là: tên (ghi đè và cung cấp bởi lớp dưới), id file đính kèm dạng chuỗi, hoặc chính nội dung của Rtf
        /// </summary>
        public virtual Stream OnLoadRtf(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, string rtf)
        {
            //long rtfID;
            //if (Int64.TryParse(rtf, out rtfID))
            //    if (rtfID > 0)
            //        return this.OnLoadRtf(document, ORenderInfo, filter, mergefield, field, rtfID);
            //    else
            //        return null;
            return new MemoryStream(Encoding.ASCII.GetBytes(rtf));
        }

        /// <summary>
        /// Hàm lấy ra nội dung Rtf nhúng vào báo cáo dựa trên ID của file đính kèm Rtf
        /// </summary>
        //public virtual Stream OnLoadRtf(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, long rtfID)
        //{
        //    return SystemFiles.LoadStream(ORenderInfo, rtfID);
        //}

        #endregion

        #region MergeField - Word

        /// <summary>
        /// Hàm load và tạo Word rồi đưa vào báo cáo tại vị trí của mergefield
        /// </summary>
        public virtual Document OnCreateWord(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, object value)
        {
            Stream stream;
            if (value is long)
                stream = this.OnLoadWord(document, ORenderInfo, filter, mergefield, field, (long)value);
            else if (value is string && !String.IsNullOrEmpty((string)value))
                stream = this.OnLoadWord(document, ORenderInfo, filter, mergefield, field, (string)value);
            else if (value is byte[])
                stream = new MemoryStream((byte[])value);
            else
                stream = null;

            if (stream != null)
                using (stream)
                {
                    var wordDocument = new Document(stream, new Aspose.Words.LoadOptions() { LoadFormat = WordReportUtility.ASPOSE_LOADFORMAT });
                    document.ReplaceMergeField(mergefield).CurrentParagraph.InsertDocument(wordDocument);
                    return wordDocument;
                }
            return null;
        }

        /// <summary>
        /// Hàm lấy ra nội dung Word nhúng vào báo cáo dựa trên chuỗi đầu vào có thể là: tên (ghi đè và cung cấp bởi lớp dưới), id file đính kèm dạng chuỗi, hoặc chính nội dung của Word
        /// </summary>
        public virtual Stream OnLoadWord(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, string word)
        {
            long wordID;
            if (Int64.TryParse(word, out wordID))
                if (wordID > 0)
                    return this.OnLoadWord(document, ORenderInfo, filter, mergefield, field, wordID);
                else
                    return null;
            return new MemoryStream(Encoding.UTF8.GetBytes(word));
        }

        /// <summary>
        /// Hàm lấy ra nội dung Word nhúng vào báo cáo dựa trên ID của file đính kèm Word
        /// </summary>
        public virtual Stream OnLoadWord(Document document, RenderInfoCls ORenderInfo, object filter, string mergefield, string field, long wordID)
        {
            return null;
            //return SystemFiles.LoadStream(ORenderInfo, wordID);
        }

        #endregion

        protected virtual FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, T filter)
        {
            return new FlexCelReport();
        }
        protected virtual FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, T filter, object[] parameters)
        {
            return new FlexCelReport();
        }
        protected virtual AjaxOut ExecuteMultiTemplate(RenderInfoCls ORenderInfo, T filter)
        {
            return new AjaxOut();
        }
        protected virtual AjaxOut ExecuteMultiTemplate(RenderInfoCls ORenderInfo, T filter, object[] parameters)
        {
            return new AjaxOut();
        }
        protected virtual Stream OnLoadStreamMultiTemplate(RenderInfoCls ORenderInfo, T filter)
        {
            return null;
        }
        protected virtual Stream OnLoadStreamMultiTemplate(RenderInfoCls ORenderInfo, T filter, object[] parameters)
        {
            return null;
        }
        protected virtual DataTable OnC1Build(RenderInfoCls ORenderInfo, T filter)
        {
            return null;
        }
        public override bool CanExecute(RenderInfoCls ORenderInfo, string id)
        {
            return true;
        }
        protected virtual Document OnWordBuild(RenderInfoCls ORenderInfo, ref Document reportDocument, T filter)
        {
            return null;
        }
        protected virtual Document OnWordBuild(RenderInfoCls ORenderInfo, ref Document reportDocument, T filter, object[] parameters)
        {
            return null;
        }
    }

    [Serializable, DataContract]
    public abstract class BusinessReport<TFilter> : ReportT<TFilter>
        where TFilter : FilterCls
    {
        protected BusinessReport()
        {
        }
        public override Type ReportType { get { return typeof(TFilter); } }

        protected override DataTable OnC1Build(RenderInfoCls ORenderInfo, TFilter filter)
        {
            return null;
        }

        protected override FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, TFilter filter)
        {
            return base.OnFlexcelBuild(ORenderInfo, filter);
        }
        protected override FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, TFilter filter, object[] parameters)
        {
            return base.OnFlexcelBuild(ORenderInfo, filter);
        }

        protected override Document OnWordBuild(RenderInfoCls ORenderInfo, ref Document reportDocument, TFilter filter)
        {
            return null;
        }
        protected override Document OnWordBuild(RenderInfoCls ORenderInfo, ref Document reportDocument, TFilter filter, object[] parameters)
        {
            return null;
        }
        public override bool CanExecute(RenderInfoCls ORenderInfo, string id)
        {
            return true;
        }

        public override bool AllowExport
        {
            get { return true; }
        }
    }

    [Serializable, DataContract]
    public abstract class ObjectReport<TFilter> : ReportT<string>
        where TFilter : WebPartTemplate
    {
        protected ObjectReport()
        {
        }
        public override Type ReportType { get { return typeof(TFilter); } }

        protected override DataTable OnC1Build(RenderInfoCls ORenderInfo, string filter)
        {
            return null;
        }

        protected override FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, string filter)
        {
            return base.OnFlexcelBuild(ORenderInfo, filter);
        }
        protected override FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, string filter, object[] parameters)
        {
            return base.OnFlexcelBuild(ORenderInfo, filter);
        }
        protected override AjaxOut ExecuteMultiTemplate(RenderInfoCls ORenderInfo, string filter)
        {
            return base.ExecuteMultiTemplate(ORenderInfo, filter);
        }

        protected override Document OnWordBuild(RenderInfoCls ORenderInfo, ref Document reportDocument, string filter)
        {
            return null;
        }
        protected override Document OnWordBuild(RenderInfoCls ORenderInfo, ref Document reportDocument, string filter, object[] parameters)
        {
            return null;
        }

        public override bool CanExecute(RenderInfoCls ORenderInfo, string id)
        {
            return true;
        }

        public override bool AllowExport { get { return false; } }
    }
}
 