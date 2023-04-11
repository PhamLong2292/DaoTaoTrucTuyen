using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using HiQPdf;

namespace OneTSQ.Printer
{
    public class BarCodePrinter
    {
        private readonly string _xsltTemplate;
        private readonly string _printerName;
        private readonly PageConfig _pageConfig;
        private readonly PaperSize _paperSize;

        public BarCodePrinter(
            string printerName,
            string xsltTemplate,
            PageConfig pageConfig,
            PaperSize paperSize = null)
        {
            _printerName = printerName;
            _pageConfig = pageConfig;
            _paperSize = paperSize;
            _xsltTemplate = xsltTemplate;
        }

        public void Print(string xml)
        {
            //if(PrinterHelper.CheckConnectedPrinter(_printerName))
            //{
            var pdfContent = GetPdf(xml);
            var printer = new HiQPdf.PdfPrinter()
            {
                SerialNumber = Constance.License,
                SilentPrinting = true,
                PageSettings =
                {
                    Landscape = false,
                    Margins = new Margins(0, 0, 0, 0),
                    PaperSize = _paperSize,
                },
                PrinterSettings =
                {
                    PrinterName = _printerName,
                }
            };
            PrinterHelper.SetDefaultPrinter(_printerName);
            printer.PrintPdf(pdfContent);
            //}
        }

        public byte[] GetPdf(string xml)
        {
            var html = XsltUtil.GenHtml(_xsltTemplate, xml);
            var pdfConverter = new HtmlToPdf
            {
                SerialNumber = Constance.License,
                Document =
                {
                    PageOrientation = _pageConfig.IsLandscape
                        ? PdfPageOrientation.Landscape
                        : PdfPageOrientation.Portrait,
                    PageSize = _pageConfig.PageSize != null
                        ? new PdfPageSize(_pageConfig.PageSize.Width, _pageConfig.PageSize.Height)
                        : PdfPageSize.A4
                }
            };
            var pdfContent = pdfConverter.ConvertHtmlToMemory(html, string.Empty);
            return pdfContent;
        }
    }
}