using C1.C1Report;
using FlexCel.Core;
using FlexCel.Render;
using FlexCel.Report;
using FlexCel.XlsAdapter;
using OneTSQ.CallTempService;
using OneTSQ.Core.Model;
using OneTSQ.ReportUtility.Utility;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ReportUtility
{
    public class FlexcelReportUtility
    {
        private string XMLReportFile;
        private FlexCelReport ExcelReport = null;
        private string ReportName;
        public const Aspose.Cells.LoadFormat ASPOSE_LOADFORMAT = Aspose.Cells.LoadFormat.Xlsx;
        public const Aspose.Cells.SaveFormat ASPOSE_SAVEFORMAT = Aspose.Cells.SaveFormat.Xlsx;
        public FlexcelReportUtility()
        {
            //
            // TODO: Add constructor logic here
            //
            ExcelReport = new FlexCelReport();
        }

        public AjaxOut ExportEx(RenderInfoCls ORenderInfo, string LoginName, string templatePath, string _ReportName, FlexCelReport flexCelReport, string FileName, XlsFile Result = null, Report report = null, bool AllowBase64String = false)
        {
            ExcelReport = flexCelReport;
            ReportName = _ReportName;
            XMLReportFile = templatePath;
            Byte[] Bytes;
            Assembly a = Assembly.GetExecutingAssembly();
            FileInfo f = new FileInfo(templatePath);
            if (Result != null)
            {
                Result.Save(FileName);
                f = new FileInfo(FileName);
            }

            using (FileStream InStream = f.OpenRead())
            {
                using (MemoryStream OutStream = new MemoryStream())
                {
                    flexCelReport.Run(InStream, OutStream);
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
            UploadTempInfo.Section = "xlsx";
            //Byte[] Bytes = FunctionUtility.GetBytesFromFile(FileName);
            AjaxOut UploadAjaxOut = CallTempServiceUtility.UploadTemp(ORenderInfo, UploadTempInfo, Bytes);
            try
            {
                System.IO.File.Delete(FileName);
            }
            catch { }
            return UploadAjaxOut;
        }
        public AjaxOut Execute(RenderInfoCls ORenderInfo, string LoginName, string templatePath, string _ReportName, FlexCelReport flexCelReport, string FileName, XlsFile Result = null, Report report = null, bool AllowBase64String = false)
        {
            FlexCel.Render.FlexCelPdfExport pdfReport = new FlexCel.Render.FlexCelPdfExport();
            flexCelReport.GetImageData += new GetImageDataEventHandler((o, e) =>
            {
                if (e.ImageName != null && !String.IsNullOrWhiteSpace(e.ImageName))
                {
                    double width = e.Width, height = e.Height;
                    var barcodeInfo = !e.ImageName.EndsWith("|") ? null : ReportBarcodeManager.GetBarcodeInfo("B92CBA11-4C87-478A-9767-7F82FE297E49", Result, e.ImageName); // Maybe BARCODE
                    var data = barcodeInfo != null ?
                        this.OnLoadBarcode("", barcodeInfo.SymbologyIndex, e.ImageName.Substring(0, e.ImageName.Length - barcodeInfo.Tail.Length), ref width, ref height) :
                        this.OnLoadImage("", e.ImageName, ref width, ref height);
                    e.ImageData = data;
                    if (data != null)
                    {
                        if (e.Width != width)
                            e.Width = width;
                        if (e.Height != height)
                            e.Height = height;
                    }
                    else if (barcodeInfo != null)
                        ReportBarcodeManager.DeleteImage("B92CBA11-4C87-478A-9767-7F82FE297E49", e.ImageName);
                }
            });
            ReportName = _ReportName;
            XMLReportFile = templatePath;
            string retBase64String = string.Empty;
            Byte[] Bytes;
            FileInfo f = new FileInfo(templatePath);
            if (Result == null)
            {
                Result = new XlsFile(true);
                Result.Open(templatePath);
            }

            flexCelReport.Run(Result);

            if (report != null)
            {
                Result = report.OnFinalize(ORenderInfo, Result);
            }

            RepeatRownAndShowBlock(ORenderInfo, Result);


            if (Result.SheetName == "SH_VDUH")
            {
                //Tinh toan chieu cao gan cho chu ky -SH.VDUH
                //NhuHH
                int page1Height = 18018;
                int page2Height = 15622;
                int page3Height = 18676;
                int RowLastHeight = 0;
                int RowHeightPlus = 0;
                for (int i = 1; i < Result.RowCount; i++)
                {
                    if (!Result.GetRowHidden(i))
                        RowLastHeight += Result.GetRowHeight(i);
                }
                if ((page2Height + page1Height) <= RowLastHeight)
                {
                    //do dai row cuoi cung duoc tinh tu bat dau page3
                    RowLastHeight = RowLastHeight - page1Height - page2Height;
                    //do dai khoang trong duoc tinh = do dai page3 - do dai row cuoi cung
                    RowHeightPlus = page3Height - RowLastHeight;
                }
                else if (page1Height <= RowLastHeight)//Neu do dai row cuoi cung lon hon do dai page1
                {
                    //do dai row cuoi cung duoc tinh tu bat dau page2
                    RowLastHeight = RowLastHeight - page1Height;
                    //do dai khoang trong duoc tinh = do dai page2 - do dai row cuoi cung
                    RowHeightPlus = page2Height - RowLastHeight;
                }
                else if(page1Height > RowLastHeight)
                {
                    //do dai khoang trong duoc tinh = do dai page1 - do dai row cuoi cung
                    RowHeightPlus = page1Height - RowLastHeight;
                }
                //do dai can tang len duoc tinh = do dai khoang trong + do dai cua row thu 7 tu duoi len(row tren dong thoi gian).                
                RowHeightPlus = RowHeightPlus + Result.GetRowHeight((Result.RowCount - 7));
                //set height cua khoang trong de cach deu chu ky
                Result.SetRowHeight((Result.RowCount - 7), RowHeightPlus);
                //Result.SetCellValue((Result.RowCount - 7), 4, "RowCount-7");
                //Result.SetCellValue(Result.RowCount, 4, "Result.RowCount");
            }
            //int indexH = 0;
            //for (int i = 1; i < Result.RowCount; i++)
            //{
            //    if (!Result.GetRowHidden(i))
            //        indexH += Result.GetRowHeight(i);
            //    Result.SetCellValue(i, 4, indexH);
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
            //Byte[] Bytes = FunctionUtility.GetBytesFromFile(FileName);
            AjaxOut UploadAjaxOut = CallTempServiceUtility.UploadTemp(ORenderInfo, UploadTempInfo, Bytes);
            if (AllowBase64String)
            {
                using (MemoryStream OutStream = new MemoryStream())
                {
                    using (FlexCelImgExport imgExport = new FlexCelImgExport())
                    {
                        imgExport.AllVisibleSheets = false;
                        imgExport.PageSize = null;
                        imgExport.ResetPageNumberOnEachSheet = false;
                        imgExport.Resolution = 96F;
                        imgExport.Workbook = Result;
                        var exportInfo = imgExport.GetFirstPageExportInfo();
                        Bitmap bmp = imgExport.ExportImageNext(ref exportInfo);
                        bmp.Save(OutStream, ImageFormat.Jpeg);
                        retBase64String = Convert.ToBase64String(OutStream.ToArray());
                    }
                }
                UploadAjaxOut.RetExtraParam1 = retBase64String;
                UploadAjaxOut.RetObject = Bytes;
            }


            //UploadAjaxOut.RetExtraParam1 = Convert.ToBase64String(Bytes);t
            try
            {
                System.IO.File.Delete(FileName);
            }
            catch { }
            return UploadAjaxOut;
        }
        public Stream ExecuteStream(RenderInfoCls ORenderInfo, string LoginName, string templatePath, string _ReportName, FlexCelReport flexCelReport, string FileName, XlsFile Result = null, Report report = null, bool AllowBase64String = false)
        {
            FlexCel.Render.FlexCelPdfExport pdfReport = new FlexCel.Render.FlexCelPdfExport();
            flexCelReport.GetImageData += new GetImageDataEventHandler((o, e) =>
            {
                if (e.ImageName != null && !String.IsNullOrWhiteSpace(e.ImageName))
                {
                    double width = e.Width, height = e.Height;
                    var barcodeInfo = !e.ImageName.EndsWith("|") ? null : ReportBarcodeManager.GetBarcodeInfo("B92CBA11-4C87-478A-9767-7F82FE297E49", Result, e.ImageName); // Maybe BARCODE
                    var data = barcodeInfo != null ?
                        this.OnLoadBarcode("", barcodeInfo.SymbologyIndex, e.ImageName.Substring(0, e.ImageName.Length - barcodeInfo.Tail.Length), ref width, ref height) :
                        this.OnLoadImage("", e.ImageName, ref width, ref height);
                    e.ImageData = data;
                    if (data != null)
                    {
                        if (e.Width != width)
                            e.Width = width;
                        if (e.Height != height)
                            e.Height = height;
                    }
                    else if (barcodeInfo != null)
                        ReportBarcodeManager.DeleteImage("B92CBA11-4C87-478A-9767-7F82FE297E49", e.ImageName);
                }
            });
            ReportName = _ReportName;
            XMLReportFile = templatePath;
            string retBase64String = string.Empty;
            Byte[] Bytes;
            FileInfo f = new FileInfo(templatePath);
            if (Result == null)
            {
                Result = new XlsFile(true);
                Result.Open(templatePath);
            }

            flexCelReport.Run(Result);

            if (report != null)
            {
                Result = report.OnFinalize(ORenderInfo, Result);
            }
            RepeatRownAndShowBlock(ORenderInfo, Result);

            if (Result.SheetName == "SH_VDUH")
            {
                //Tinh toan chieu cao gan cho chu ky -SH.VDUH
                //NhuHH
                int page1Height = 18018;
                int page2Height = 15622;
                int page3Height = 18676;
                int RowLastHeight = 0;
                int RowHeightPlus = 0;
                for (int i = 1; i < Result.RowCount; i++)
                {
                    if (!Result.GetRowHidden(i))
                        RowLastHeight += Result.GetRowHeight(i);
                }
                if ((page2Height + page1Height) <= RowLastHeight)
                {
                    //do dai row cuoi cung duoc tinh tu bat dau page3
                    RowLastHeight = RowLastHeight - page1Height - page2Height;
                    //do dai khoang trong duoc tinh = do dai page3 - do dai row cuoi cung
                    RowHeightPlus = page3Height - RowLastHeight;
                }
                else if (page1Height <= RowLastHeight)//Neu do dai row cuoi cung lon hon do dai page1
                {
                    //do dai row cuoi cung duoc tinh tu bat dau page2
                    RowLastHeight = RowLastHeight - page1Height;
                    //do dai khoang trong duoc tinh = do dai page2 - do dai row cuoi cung
                    RowHeightPlus = page2Height - RowLastHeight;
                }
                else if (page1Height > RowLastHeight)
                {
                    //do dai khoang trong duoc tinh = do dai page1 - do dai row cuoi cung
                    RowHeightPlus = page1Height - RowLastHeight;
                }
                //do dai can tang len duoc tinh = do dai khoang trong + do dai cua row thu 7 tu duoi len(row tren dong thoi gian).                
                RowHeightPlus = RowHeightPlus + Result.GetRowHeight((Result.RowCount - 7));
                //set height cua khoang trong de cach deu chu ky
                Result.SetRowHeight((Result.RowCount - 7), RowHeightPlus);
                //Result.SetCellValue((Result.RowCount - 7), 4, "RowCount-7");
                //Result.SetCellValue(Result.RowCount, 4, "Result.RowCount");
            }
            //int indexH = 0;
            //for (int i = 1; i < Result.RowCount; i++)
            //{
            //    if (!Result.GetRowHidden(i))
            //        indexH += Result.GetRowHeight(i);
            //    Result.SetCellValue(i, 4, indexH);
            //}

            MemoryStream OutStream = new MemoryStream();
            using (FlexCelPdfExport pdf = new FlexCelPdfExport())
            {
                pdf.Workbook = Result;
                pdf.BeginExport(OutStream);
                pdf.ExportAllVisibleSheets(false, "FlexCel");
                pdf.EndExport();
                OutStream.Position = 0;
                Bytes = OutStream.ToArray();
            }

            return OutStream;
        }
        internal virtual byte[] OnLoadImage(object filter, string name, ref double width, ref double height)
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
        public void RepeatRownAndShowBlock(RenderInfoCls ORenderInfo, XlsFile Result)
        {
            #region Chèn rows title lên đầu các page với mục đích là lặp lại tiêu đề danh sách trên đầu các trang.
            //A4: 1440 PPI: 11906 x 16838 px; 8.3 x 11.7 in (Lấy A4 làm chuẩn)
            //A3: 1440 PPI: 16838 x 23812 px; 11.7 x 16.5 in
            //A5: 1440 PPI: 8419 x 11906 px; 5.8 x 8.3 in
            //A6: 1440 PPI: 5953 x 8419 px; 4.1 x 5.8 in
            int rowQuantity = Result.RowCount;
            int colQuantity = Result.ColCount;
            int pageHeight = 16838;//mặc định là A4 dọc.// 15000: A3, trang ngang 
            FlexCel.Core.TXlsMargins margins = Result.GetPrintMargins();
            FlexCel.Core.TPaperSize pageSize = Result.PrintPaperSize;
            int topBottomMargin = (int)Math.Round((margins.Top + margins.Bottom) * 23812 / 16.5);//đổi giá trị margin từ đơn vị inch sang pixcel
            bool printLandscape = Result.PrintLandscape;
            if (pageSize == FlexCel.Core.TPaperSize.A6)
            {
                if (printLandscape)//in trang ngang
                    pageHeight = 5953 - topBottomMargin;
                else//in trang dọc
                    pageHeight = 8419 - topBottomMargin;
            }
            else if (pageSize == FlexCel.Core.TPaperSize.A5)
            {
                if (printLandscape)//in trang ngang
                    pageHeight = 8419 - topBottomMargin;
                else//in trang dọc
                    pageHeight = 11906 - topBottomMargin;
            }
            else if (pageSize == FlexCel.Core.TPaperSize.A4)
            {
                if (printLandscape)//in trang ngang
                    pageHeight = 11906 - topBottomMargin;
                else//in trang dọc
                    pageHeight = 16838 - topBottomMargin;
            }
            else if (pageSize == FlexCel.Core.TPaperSize.A3)
            {
                if (printLandscape)//in trang ngang
                    pageHeight = 16838 - topBottomMargin;
                else//in trang dọc
                    pageHeight = 23812 - topBottomMargin;
            }

            int rowIndex = 1;
            //Duyệt lần lượt từ row đầu tiên đến row cuối cùng
            while (rowIndex <= rowQuantity)
            {
                string cellValue = (Result.GetCellValue(rowIndex, 1) as string);
                if (!string.IsNullOrEmpty(cellValue) && cellValue.StartsWith("#StartRepeating:"))//Nếu là row đánh dấu sự bắt đầu lặp rows
                {
                    Result.SetCellValue(rowIndex, 1, null);
                    Result.SetRowHidden(rowIndex, true);
                    string[] startRepeatingArr = cellValue.Split(':');
                    if (startRepeatingArr.Length != 2 || string.IsNullOrWhiteSpace(startRepeatingArr[1]))
                        throw new Exception(WebLanguage.GetLanguage(ORenderInfo, "Chưa thiết lập số dòng được lặp lại trên mỗi trang trong file Excel mẫu."));
                    int repeatedRowQuantity = 0;
                    int.TryParse(startRepeatingArr[1].Trim(), out repeatedRowQuantity);
                    if (repeatedRowQuantity == 0)
                        throw new Exception(WebLanguage.GetLanguage(ORenderInfo, "Chưa thiết lập đúng định dạng số dòng được lặp lại trên mỗi trang trong file Excel mẫu."));
                    //Thực hiện lặp rows cho đến row đánh dấu sự kết thúc lặp
                    rowIndex = RepeatRows(Result, rowIndex + 1, 1, rowIndex + repeatedRowQuantity, colQuantity, pageHeight);
                    //Cập nhật lại tổng số row có trong sheet
                    rowQuantity = Result.RowCount;
                }
                else if (cellValue == "#StartBlock")//Nếu là row đánh dấu sự bắt đầu block
                {
                    Result.SetCellValue(rowIndex, 1, null);
                    Result.SetRowHidden(rowIndex, true);
                    //Thực hiện đảm bảo hiển thị các rows thuộc block trong cùng 1 page
                    rowIndex = ShowBlock(Result, rowIndex + 1, pageHeight);
                }
                rowIndex++;
            }
            #endregion
        }
        /// <summary>
        /// Hàm thực hiện việc lặp các rows tiêu đề trên các trang trong 1 đoạn Excel được xác định bởi cặp #StartRepeating - #EndRepeating
        /// Nếu không tìm thấy điểm kết thúc #EndRepeating thì trả về chỉ số row cuối cùng của sheet.
        /// Nếu tìm thấy điểm kết thúc #EndRepeating thì trả về chỉ số row chứa điểm kết thúc.
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="firstRow"></param>
        /// <param name="firstCol"></param>
        /// <param name="lastRow"></param>
        /// <param name="lastCol"></param>
        /// <param name="pageHeight"></param>
        /// <returns></returns>
        private int RepeatRows(XlsFile Result, int firstRow, int firstCol, int lastRow, int lastCol, int pageHeight)
        {
            FlexCel.Core.TXlsCellRange cellRang = new FlexCel.Core.TXlsCellRange(firstRow, firstCol, lastRow, lastCol);

            int rowQuantity = Result.RowCount;
            int currentPageHeight = 0;
            int cellRangHeight = 0;
            //Tính chiều cao của các row tiêu đề
            for (int i = firstRow; i <= lastRow; i++)
                cellRangHeight += Result.GetRowHeight(i);
            //Tìm row tiếp theo tiêu đề mà không bị ẩn
            int j = lastRow + 1;
            while (Result.GetRowHidden(j))
                j++;

            //đi ngược về đầu trang để tìm điểm HPageBreak. Hành động này có thể lướt qua nhiều trang nếu giữa các trang này không có HPageBreak.
            int startedRowIndex = 1;//Mặc định bắt đầu tính từ row đầu tiên của trang đầu tiên
            for (int i = firstRow - 1; i >= 1; i--)
            {
                if (Result.HasHPageBreak(i))//i là chỉ số của row mà sau đó có HPageBreak
                {
                    startedRowIndex = i + 1;
                    break;
                }
            }
            //Tìm chỉ số row bắt đầu tính để lặp tiêu đề và chiều cao số dòng từ đầu trang đến tiêu đề. 
            //Chỉ số này phải nằm đầu trang có chứa row có chỉ số là firstRow
            for (int i = startedRowIndex; i < firstRow; i++)
            {
                if (!Result.GetRowHidden(i))//chỉ tính chiều cao của các row được hiển thị.
                {
                    if (currentPageHeight + Result.GetRowHeight(i) <= pageHeight)
                        currentPageHeight += Result.GetRowHeight(i);
                    //Hết 1 trang và sang trang mới
                    else
                    {
                        startedRowIndex = i;
                        currentPageHeight = Result.GetRowHeight(i);
                    }
                }
            }

            //break trang khi trang chứa điểm bắt đầu lặp tiêu đề không đủ để chứa tiêu đề vào một row tiếp theo tiêu đề
            //và gán lại chỉ số row bắt đầu tính để lặp tiêu đề là chỉ số row tiêu đề. 
            if (currentPageHeight + cellRangHeight + Result.GetRowHeight(j) > pageHeight)
            {
                Result.InsertHPageBreak(firstRow - 1);//HPageBreak được insert ngay sau row thứ firstRow-1
                startedRowIndex = firstRow;
            }
            currentPageHeight = 0;
            //Thực hiện lặp tiêu đề
            for (int i = startedRowIndex; i <= rowQuantity; i++)
            {
                string cellValue = (Result.GetCellValue(i, 1) as string);
                if (cellValue == "#EndRepeating")//Nếu là row đánh dấu sự kết thúc lặp rows
                {
                    Result.SetCellValue(i, 1, null);
                    Result.SetRowHidden(i, true);
                    return i;//Trả về chỉ số row kết thúc của việc lặp 
                }
                if (!Result.GetRowHidden(i))//chỉ tính chiều cao của các row được hiển thị.
                {
                    if (currentPageHeight + Result.GetRowHeight(i) <= pageHeight)
                        currentPageHeight += Result.GetRowHeight(i);
                    else
                    {
                        Result.InsertAndCopyRange(cellRang, i, 1, 1, FlexCel.Core.TFlxInsertMode.ShiftRowDown);//Insert tiêu đề lên trước row i
                        Result.InsertHPageBreak(i - 1);//HPageBreak được insert ngay sau row thứ i-1
                        currentPageHeight = Result.GetRowHeight(i);
                        rowQuantity += cellRang.RowCount;
                    }
                }
                if (Result.HasHPageBreak(i))//Nếu có HPageBreak thì xóa
                {
                    Result.DeleteHPageBreak(i);
                }
            }
            return rowQuantity;//Nếu không tìm thấy row kết thúc lặp thì trả về chỉ số row cuối cùng của file
        }
        /// <summary>
        /// Hàm thực hiện việc hiển thị 1 khối các rows liền nhau trong cùng 1 page được xác định bởi cặp #StartBlock - #EndBlock
        /// Nếu không tìm thấy điểm kết thúc #EndBlock thì trả về chỉ số row cuối cùng của sheet.
        /// Nếu tìm thấy điểm kết thúc #EndBlock thì trả về chỉ số row chứa điểm kết thúc.
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="firstRow"></param>
        /// <param name="pageHeight"></param>
        /// <returns></returns>
        private int ShowBlock(XlsFile Result, int firstRow, int pageHeight)
        {
            int rowQuantity = Result.RowCount;
            int currentPageHeight = 0;
            bool breaked = false;
            //đi ngược về đầu trang để tìm điểm HPageBreak.
            int startedRowIndex = 1;//Mặc định bắt đầu tính từ row đầu tiên của trang đầu tiên
            for (int i = firstRow - 1; i >= 1; i--)
                if (Result.HasHPageBreak(i))//i là chỉ số của row mà sau đó có HPageBreak
                {
                    startedRowIndex = i + 1;
                    break;
                }

            for (int i = startedRowIndex; i <= rowQuantity; i++)
            {
                string cellValue = (Result.GetCellValue(i, 1) as string);
                if (cellValue == "#EndBlock")//Nếu là row đánh dấu sự kết thúc block
                {
                    Result.SetCellValue(i, 1, null);
                    Result.SetRowHidden(i, true);
                    return i;
                }
                //Nếu có HPageBreak trong block thì xóa HpageBreak và nếu chưa break thì insert HPageBreak ngay đầu block để đẩy toàn bộ block sang page mới.
                else if (Result.HasHPageBreak(i))
                {
                    Result.DeleteHPageBreak(i);
                    //nếu chưa break thì insert HPageBreak ngay đầu block để đẩy toàn bộ block sang page mới
                    if (!breaked)
                    {
                        Result.InsertHPageBreak(firstRow - 1);//HPageBreak được insert ngay sau row thứ firstRow-1
                        breaked = true;
                    }
                }
                //Nếu chưa break page
                if (!breaked && !Result.GetRowHidden(i))//chỉ tính chiều cao của các row được hiển thị.
                {
                    if (currentPageHeight + Result.GetRowHeight(i) <= pageHeight)
                        currentPageHeight += Result.GetRowHeight(i);
                    //Nếu có kết thúc trang chứa điểm bắt đầu block mà chưa tìm thấy điểm kết thúc bock insert HPageBreak ngay đầu block để đẩy toàn bộ block sang page mới.
                    else
                    {
                        Result.InsertHPageBreak(firstRow - 1);//HPageBreak được insert ngay sau row thứ firstRow-1
                        breaked = true;
                    }
                }
            }
            return rowQuantity;//Nếu không tìm thấy row kết thúc block thì trả về chỉ số row cuối cùng của file
        }
    }
}
