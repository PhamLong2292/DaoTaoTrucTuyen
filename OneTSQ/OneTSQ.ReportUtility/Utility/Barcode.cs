﻿using Aspose.BarCode;
using FlexCel.XlsAdapter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ReportUtility.Utility
{
    #region Execution - Barcode

    public sealed class BarcodeImageInfo
    {
        public BarcodeImageInfo(int index, string imageName, string tail, int symbologyIndex)
        {
            this.Index = index;
            this.ImageName = imageName;
            this.Tail = tail;
            this.SymbologyIndex = symbologyIndex;
        }

        public readonly int Index;
        public readonly string ImageName;
        public readonly string Tail;
        public readonly int SymbologyIndex;
    }

    public sealed class ReportBarcodeManager
    {
        private static Dictionary<string, ReportBarcodeManager> Managers = new Dictionary<string, ReportBarcodeManager>();

        private readonly Dictionary<string, BarcodeImageInfo> images;

        private ReportBarcodeManager(XlsFile xlsTemplate)
        {
            this.images = new Dictionary<string, BarcodeImageInfo>();
            for (int i = 1, l = xlsTemplate.ImageCount; i <= l; i++)
            {
                var imageName = xlsTemplate.GetImageName(i);
                if (imageName.StartsWith("<#") && imageName.EndsWith("|"))
                {
                    var startIndex = imageName.IndexOf(">|");
                    if (startIndex >= 0 && startIndex < (imageName.Length - 2))
                    {
                        var tail = imageName.Substring(startIndex + 1);
                        if (!this.images.ContainsKey(tail))
                        {
                            var value = tail.Substring(1, tail.Length - 2);
                            if (value.Length == 1)
                            {
                                if (value == "|")
                                {
                                    this.images.Add(tail, new BarcodeImageInfo(i, imageName, tail, AsposeBarcodeDefinitions.DefaultSymbologyIndex));
                                }
                            }
                            else if (value.Length > 1)
                            {
                                var sepIndex = value.IndexOf('|');
                                if (sepIndex >= 0)
                                    this.images.Add(tail, new BarcodeImageInfo(i, imageName, tail, AsposeBarcodeUtils.ParseBarcodeIndex(value.Substring(sepIndex + 1))));
                            }
                        }
                    }
                }
            }
        }

        public static BarcodeImageInfo GetBarcodeInfo(string reportID, XlsFile xlsTemplate, string data)
        {
            if (xlsTemplate != null)
            {
                if (!ReportBarcodeManager.Managers.ContainsKey(reportID))
                    ReportBarcodeManager.Managers.Add(reportID, new ReportBarcodeManager(xlsTemplate));
                return ReportBarcodeManager.Managers[reportID].GetBarcodeInfo(data);
            }
            else return null;
        }

        public static void DeleteImage(string reportID, string data)
        {
            ReportBarcodeManager.Managers[reportID].DeleteImage(data);
        }

        private BarcodeImageInfo GetBarcodeInfo(string data)
        {
            foreach (var barcodeInfo in this.images.Values)
                if (data.EndsWith(barcodeInfo.Tail))
                    return barcodeInfo;
            return null;
        }

        private void DeleteImage(string data)
        {
        }
    }
    public static class AsposeBarcodeUtils
    {
        public static int ParseBarcodeIndex(string symbologyCode)
        {
            return Array.IndexOf(AsposeBarcodeDefinitions.IndexedCodes, AsposeBarcodeDefinitions.ParseBarcodeType(symbologyCode));
        }
    }

    public static class AsposeBarcodeDefinitions
    {
        public const int DefaultSymbologyIndex = 7;// Symbology.Code128;
        public const Symbology DefaultSymbology = Symbology.GS1Code128;

        public static readonly MarginsF DefaultMarginsF = new MarginsF(0, 0, 0, 0);
        public static readonly Resolution DefaultResolution = new Aspose.BarCode.Resolution(96F, 96F, Aspose.BarCode.ResolutionMode.Graphics);

        public static Symbology[] IndexedCodes = new Symbology[]
        {
            Symbology.Codabar,	        // 0
            Symbology.Code11,	        // 1
            Symbology.Code39Standard,	// 2
            Symbology.Code39Extended,	// 3
            Symbology.Code93Standard,	// 4
            Symbology.Code93Extended,	// 5
            Symbology.Code128,	        // 6
            Symbology.GS1Code128,	    // 7
            Symbology.EAN8,	            // 8
            Symbology.EAN13,	        // 9
            Symbology.EAN14,	        // 10
            Symbology.SCC14,	        // 11
            Symbology.SSCC18,	        // 12
            Symbology.UPCA,	            // 13
            Symbology.UPCE,	            // 14
            Symbology.ISBN,	            // 15
            Symbology.ISSN,	            // 16
            Symbology.ISMN,	            // 17
            Symbology.Standard2of5,	    // 18
            Symbology.Interleaved2of5,	// 19
            Symbology.Matrix2of5,	    // 20
            Symbology.ItalianPost25,	// 21
            Symbology.IATA2of5,	        // 22
            Symbology.ITF14,	        // 23
            Symbology.ITF6,	            // 24
            Symbology.MSI,	            // 25
            Symbology.VIN,	            // 26
            Symbology.DeutschePostIdentcode,	// 27
            Symbology.DeutschePostLeitcode,	    // 28
            Symbology.OPC,	            // 29
            Symbology.PZN,	            // 30
            Symbology.Pharmacode,	    // 31
            Symbology.DataMatrix,	    // 32
            Symbology.QR,	            // 33
            Symbology.Aztec,	        // 34
            Symbology.Pdf417,	        // 35
            Symbology.MacroPdf417,	    // 36
            Symbology.AustraliaPost,	// 37
            Symbology.Postnet,	        // 38
            Symbology.Planet,	        // 39
            Symbology.OneCode,	        // 40
            Symbology.RM4SCC,	        // 41
            Symbology.DatabarOmniDirectional,	// 42
            Symbology.DatabarTruncated,	// 43
            Symbology.DatabarLimited,	// 44
            Symbology.DatabarExpanded,	// 45
            Symbology.SingaporePost,	// 46
            Symbology.GS1DataMatrix,	// 47
            Symbology.AustralianPosteParcel,	// 48
            Symbology.SwissPostParcel,	// 49
        };

        public static Symbology ParseBarcodeType(string symbologyCode)
        {
            if (symbologyCode.Length <= 0)
                return DefaultSymbology;
            else if (symbologyCode[0] >= '0' && symbologyCode[0] <= '9')
            {
                int index;
                if (!Int32.TryParse(symbologyCode, out index) || index >= IndexedCodes.Length || index < 0)
                    return DefaultSymbology;
                return IndexedCodes[index];
            }
            else
            {
                Aspose.BarCode.Symbology symbology;
                if (!Enum.TryParse<Aspose.BarCode.Symbology>(symbologyCode, out symbology))
                    return DefaultSymbology;
                return symbology;
            }
        }

        public static System.Drawing.Imaging.ImageFormat ToImageFormat(BarCodeImageFormat format)
        {
            switch (format)
            {
                case BarCodeImageFormat.Bmp:
                    return System.Drawing.Imaging.ImageFormat.Bmp;
                case BarCodeImageFormat.Gif:
                    return System.Drawing.Imaging.ImageFormat.Gif;
                case BarCodeImageFormat.Jpeg:
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case BarCodeImageFormat.Png:
                    return System.Drawing.Imaging.ImageFormat.Png;
                case BarCodeImageFormat.Tiff:
                    return System.Drawing.Imaging.ImageFormat.Tiff;
                default:
                    throw new NotImplementedException(format.ToString());
            }
        }

        public static BarCodeBuilder CreateBuider(string bardata, int symbologyIndex, float width, float height)
        {
            return new Aspose.BarCode.BarCodeBuilder(bardata, AsposeBarcodeDefinitions.IndexedCodes[symbologyIndex])
            {
                //AlwaysShowChecksum = false,
                //AspectRatio = 0F,
                //AustralianPosteParcelCodeSet = Aspose.BarCode.AustralianPosteParcelCodeSet.Auto,
                //AustraliaPostFormatControlCode = Aspose.BarCode.AustraliaPostFormatControlCode.Standard,
                AutoSize = true,
                AztectErrorLevel = 23,
                BackColor = System.Drawing.Color.White,
                BarHeight = height,
                //BorderColor = System.Drawing.Color.Gray,
                //BorderDashStyle = Aspose.BarCode.BorderDashStyle.Dot,
                //BorderVisible = false,
                //BorderWidth = 0.6F,
                //CaptionAbove = new Aspose.BarCode.Caption("", false, System.Drawing.StringAlignment.Near, 0F, System.Drawing.Color.Black, new System.Drawing.Font("Arial", 8F)),
                //CaptionBelow = new Aspose.BarCode.Caption("", false, System.Drawing.StringAlignment.Near, 0F, System.Drawing.Color.Black, new System.Drawing.Font("Arial", 8F)),
                //CodabarStartSymbol = Aspose.BarCode.CodabarSymbol.A,
                //CodabarStopSymbol = Aspose.BarCode.CodabarSymbol.A,
                //Code128CodeSet = Aspose.BarCode.Code128CodeSet.Auto,
                CodeLocation = Aspose.BarCode.CodeLocation.Below,
                //CodeText = bardata.Substring(0, bardata.Length - 1),
                CodeTextAlignment = System.Drawing.StringAlignment.Center,
                CodeTextColor = System.Drawing.Color.Black,
                //CodeTextEncoding = ((System.Text.Encoding)(resources.GetObject("barCodeBuilder.CodeTextEncoding"))),
                CodeTextFont = new System.Drawing.Font("Tahoma", 9, FontStyle.Bold),
                //CodeTextSpace = 0F,
                //Columns = 0,
                //DataMatrixEncodeMode = Aspose.BarCode.DataMatrixEncodeMode.Auto,
                //Display2DText = "0123456789",
                //EnableChecksum = Aspose.BarCode.EnableChecksum.Default,
                //EnableEscape = false,
                ForeColor = System.Drawing.Color.Black,
                GraphicsUnit = System.Drawing.GraphicsUnit.Pixel,
                ImageHeight = height,
                //ImageQuality = Aspose.BarCode.ImageQualityMode.Default,
                ImageWidth = width,
                //ITF14BorderType = Aspose.BarCode.ITF14BorderType.Bar,
                //MacroPdf417FileID = -1,
                //MacroPdf417SegmentID = 0,
                //MacroPdf417SegmentsCount = -1,
                Margins = AsposeBarcodeDefinitions.DefaultMarginsF,
                //Pdf417CompactionMode = Aspose.BarCode.Pdf417CompactionMode.Auto,
                //Pdf417ErrorLevel = Aspose.BarCode.Pdf417ErrorLevel.Level0,
                //Pdf417Truncate = false,
                //PlanetShortBarHeight = 5F,
                //PostnetShortBarHeight = 5F,
                //PrinterName = "",
                //QREncodeMode = Aspose.BarCode.QREncodeMode.Auto,
                //QRErrorLevel = Aspose.BarCode.QRErrorLevel.LevelL,
                Resolution = AsposeBarcodeDefinitions.DefaultResolution,
                //RotationAngleF = 0F,
                //Rows = 0,
                //SupplementData = "",
                //SupplementSpace = 4F,
                //SymbologyType = Aspose.BarCode.Symbology.Code128,
                //TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault,
                //WideNarrowRatio = 3F,
                //xDimension = 1.8F,
                //yDimension = 2F,
            };
        }
    }
    #endregion
}
