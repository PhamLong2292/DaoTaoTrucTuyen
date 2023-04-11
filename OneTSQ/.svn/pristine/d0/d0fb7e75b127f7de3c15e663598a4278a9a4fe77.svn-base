using System.Drawing.Printing;
using HiQPdf;

namespace OneTSQ.Printer
{
    public class SequencePrinter
    {
        private readonly string _printerName;
        private readonly PageConfig _pageConfig;
        private readonly string _template;

        public SequencePrinter(string printerName, PageConfig pageConfig, string template)
        {
            _printerName = printerName;
            _pageConfig = pageConfig;
            _template = template;
        }

        public void Print(string xml)
        {
            //if(PrinterHelper.CheckConnectedPrinter(_printerName))
            //{
            var pdf = GetPdf(xml);
            var printer = new PdfPrinter()
            {
                SerialNumber = Constance.License,
                SilentPrinting = true,
                PageSettings =
                {
                    Landscape = false,
                    Margins = new Margins(0, 0, 0, 0)
                },
                PrinterSettings =
                {
                    PrinterName = _printerName,
                }
            };
            PrinterHelper.SetDefaultPrinter(_printerName);
            printer.PrintPdf(pdf);
            //}
        }

        public byte[] GetPdf(string xml)
        {
            var htmlToPdf = new HtmlToPdf
            {
                SerialNumber = Constance.License,
                Document =
                {
                    FitPageWidth = true,
                    FitPageHeight = true,
                    PageSize = HiQPdf.PdfPageSize.A4
                }
            };
            var html = XsltUtil.GenHtml(_template, xml);
            var pdf = htmlToPdf.ConvertHtmlToMemory(html, "");
            return pdf;
        }
    }
}