using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.CallTempService;
using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebParts
{
    public class QuotationPreviewUtility
    {
        public static AjaxOut Preview(RenderInfoCls ORenderInfo)
        {
           string QuotationId=(string)WebEnvironments.Request("Id");
           string ViewType = (string)WebEnvironments.Request("ViewType");
           if (string.IsNullOrEmpty(ViewType)) ViewType = "html";
           return Preview(ORenderInfo, QuotationId, ViewType);
        }



        public static AjaxOut Preview(RenderInfoCls ORenderInfo,string QuotationId,string ViewType)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
               // WebSession.CheckSessionTimeOut(ORenderInfo);
                string Html =
                     "<style>\r\n";
                if (ViewType.Equals("html"))
                {
                    Html +=
                        "   body{background-color:gray;}\r\n";
                }
                else
                {
                    Html +=
                        "   body{background-color:white;}\r\n";
                }

                QuotationCls
                  OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);

                Html +=
                    OQuotation.Css+
                    //"   .CssBody{font-family:tahoma; backgound-color:white !important; width:850px!important;text-align:justify;}\r\n" +
                    //"   .CssBody img{width:100%;}\r\n" +
                    //"   .CssItinerarySection {page-break-before: always; background-color:white !important;padding-left:100px;padding-right:100px;padding-top:15px;padding-bottom:15px}\r\n" +
                    //"   .CssItinerarySectionNotPageBreak {background-color:white !important;padding-left:100px;padding-right:100px;padding-top:15px;padding-bottom:15px}\r\n" +
                    //"   .CssServiceLine {background-color:white !important;padding-left:100px;padding-right:100px;}\r\n" +
                    //"   .CssItinerarySection p{text-align:justify !important;}\r\n" +
                    //"   p{font-size:22px !important; line-height:35px !important;}\r\n"+
                    //"   li{padding:4px;font-size:22px !important; line-height:35px !important;}\r\n" +
                    //"   @media print {\r\n"+
                    //"       .CssItinerarySection {page-break-before: always;}\r\n" +
                    //"   }\r\n"+
                    //"   table, th, td {\r\n"+
                    //"       border: 1px solid black;\r\n"+
                    //"       border-spacing:0px;\r\n"+
                    //"   }\r\n"+
                    //"   td{vertical-align:top; padding:10px;}\r\n" +
                    //"   .tdheader{width:150px; font-size:20px;font-weight:bold;}\r\n"+
                "</style>\r\n" +
                    "<center>\r\n" +
                    "<div class=\"CssBody\" style=\"background-color:white !important\">\r\n" +
                    "<div class=\"CssItinerarySection\">\r\n" +
                    "   <div style=\"font-size:40px;font-weight:bold;text-align:right; margin-top:400px;color:gray\">"+OQuotation.QuotationSubject+"</div>\r\n" +
                    "   <div style=\"font-size:22px;font-weight:bold;text-align:right; margin-top:30px;color:gray\">"+FunctionUtility.GetEnglishDateText(OQuotation.StartDate )+ " - "+FunctionUtility.GetEnglishDateText(OQuotation.EndDate) +"</div>\r\n" +
                    "</div>\r\n"+

                    "<div class=\"CssItinerarySection\">" +
                            OQuotation.Header+
                    "</div>\r\n";

                if (ViewType.Equals("html"))
                {
                    Html += "<div style=\"height:600px\"></div>\r\n";
                }
                
              
                QuotationItemCls[]
                    QuotationItems = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingQuotationItems(ORenderInfo, QuotationId);
                for (int iIndex = 0; iIndex < QuotationItems.Length; iIndex++)
                {
                    QuotationItemItineraryCls[]
                        QuotationItemItineraries = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingQuotationItemItineraries(ORenderInfo, QuotationItems[iIndex].QuotationItemId);
                    if (QuotationItemItineraries.Length > 0)
                    {
                        for (int jIndex = 0; jIndex < QuotationItemItineraries.Length; jIndex++)
                        {
                            QuotationItemItineraryCls
                                OQuotationItemItinerary = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationItemItineraryModel(ORenderInfo, QuotationItemItineraries[jIndex].QuotationItemItineraryId);
                            Html +=
                                "<div class=\"CssItinerarySection\">" +
                                "   <h2 style=\"color:green\">" + FunctionUtility.GetEnglishDateText(OQuotation.StartDate.AddDays(QuotationItems[iIndex].DayIndex - 1)) + ": " + OQuotationItemItinerary.Subject + "</h2>\r\n" +
                                    OQuotationItemItinerary.Itinerary +
                                 "</div>\r\n";
                        }
                    }
                    else
                    {
                        Html+=
                            "<div class=\"CssItinerarySection\">" +
                            "   <h2 style=\"color:green\">" + FunctionUtility.GetEnglishDateText(OQuotation.StartDate.AddDays(QuotationItems[iIndex].DayIndex - 1)) + "</h2>\r\n"+
                            "</div>\r\n";
                    }



                    QuotationSectionServiceCls[]
                            QuotationSectionServices = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingQuotationSectionServices(ORenderInfo, QuotationItems[iIndex].QuotationItemId);
                    for (int iIndexSectionService = 0; iIndexSectionService < QuotationSectionServices.Length; iIndexSectionService++)
                    {
                        QuotationServiceCls[]
                            QuotationServices = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingQuotationServices(ORenderInfo, QuotationSectionServices[iIndexSectionService].QuotationSectionServiceId);
                        if (QuotationServices.Length > 0)
                        {
                            ServiceTypeCls
                                OServiceType = CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeProcess().CreateModel(ORenderInfo, QuotationSectionServices[iIndexSectionService].frkServiceTypeId);

                            if (OServiceType.DisplayQuotation == 1)
                            {
                                bool HasDraw = false;
                                Html +=
                                    "<div class=\"CssServiceLine\">";

                                if (QuotationSectionServices[iIndexSectionService].frkServiceTypeId.ToLower().Equals("hotel"))
                                {
                                    HasDraw = true;
                                  
                                    for (int iIndexService = 0; iIndexService < QuotationServices.Length; iIndexService++)
                                    {
                                        string SupplierName = QuotationServices[iIndexService].SupplierName + " / ";
                                        if (OServiceType.DisplaySupplierName == 0)
                                        {
                                            SupplierName = "";
                                        }
                                        Html += "<div><img src=\"/images/icon-hotel.png\" style=\"height:16px;width:16px;\"><span style=\"padding-left:5px\">" + SupplierName + QuotationServices[iIndexService].ServiceName;
                                        if (!string.IsNullOrEmpty(QuotationServices[iIndexService].Note))
                                        {
                                            Html += " /  " + QuotationServices[iIndexService].Note;
                                        }
                                        Html += "</span></div>\r\n";
                                    }
                                }
                                if (QuotationSectionServices[iIndexSectionService].frkServiceTypeId.ToLower().Equals("flight"))
                                {
                                    HasDraw = true;


                                    for (int iIndexService = 0; iIndexService < QuotationServices.Length; iIndexService++)
                                    {
                                        string SupplierName = QuotationServices[iIndexService].SupplierName + " / ";
                                        if (OServiceType.DisplaySupplierName == 0)
                                        {
                                            SupplierName = "";
                                        }
                                        Html += "<div><img src=\"/images/icon-flight.png\" style=\"height:16px;width:16px;\"><span style=\"padding-left:5px\">" + SupplierName+QuotationServices[iIndexService].ServiceName + " / " + QuotationServices[iIndexService].StartTime + " - " + QuotationServices[iIndexService].EndTime;
                                        if (!string.IsNullOrEmpty(QuotationServices[iIndexService].Note))
                                        {
                                            Html += " / " + QuotationServices[iIndexService].Note;
                                        }
                                        Html += "</span></div>";

                                    }

                                }
                                if (!HasDraw)
                                {
                                    for (int iIndexService = 0; iIndexService < QuotationServices.Length; iIndexService++)
                                    {

                                        if (OServiceType.DisplayQuotation == 1)
                                        {
                                            string SupplierName = QuotationServices[iIndexService].SupplierName + " / ";
                                            if (OServiceType.DisplaySupplierName == 0)
                                            {
                                                SupplierName = "";
                                            }
                                            Html += "<div style=\"padding:4px\"><img src=\"/images/icon-" + QuotationServices[iIndexService].ServiceTypeId + ".png\" style=\"height:16px;width:16px;\"><span style=\"padding-left:5px\">" + SupplierName + QuotationServices[iIndexService].ServiceName;
                                            if (!string.IsNullOrEmpty(QuotationServices[iIndexService].Note))
                                            {
                                                Html += " / " + QuotationServices[iIndexService].Note;
                                            }
                                            Html += "</span></div>\r\n";
                                        }
                                    }

                                }

                                Html += "</div>\r\n";
                            }
                        }
                    }
                }
                Html +=
                          "<div class=\"CssItinerarySection\">" +
                          "   <h2 style=\"color:green\">Package details</h2>\r\n" +
                          "     <table style=\"width:100%;border:solid 1px gray\">\r\n" +
                          "         <tr>\r\n" +
                          "             <td class=\"tdheader\">Include</td>\r\n" +
                          "             <td>" + OQuotation.Include + "</td>\r\n" +
                          "         </tr>\r\n" +
                          "         <tr>\r\n" +
                          "             <td class=\"tdheader\">Exclude</td>\r\n" +
                          "             <td>" + OQuotation.Exclude + "</td>\r\n" +
                          "         </tr>\r\n";
                if (!string.IsNullOrEmpty(OQuotation.SpecialCondition))
                {
                    Html +=
                              "         <tr>\r\n" +
                              "             <td class=\"tdheader\">Special conditions</td>\r\n" +
                              "             <td>" + OQuotation.SpecialCondition + "</td>\r\n" +
                              "         </tr>\r\n";
                }
                    Html+=
                          "     </table>\r\n"+
                           "</div>\r\n";
                Html +=
                    "<div class=\"CssItinerarySection\">" +
                        OQuotation.Footer +
                    "</div>\r\n";
                //Html+=
                //    "<div class=\"CssItinerarySectionNotPageBreak\">" +
                //    " <center style=\"font-size:20px\">\r\n"+
                //    "    <div style=\"font-size:22px; font-weight:bold\">We look forward to hosting you!</div>\r\n" +
                //    "    <div>Smiling Albino - Bangkok, Thailand</div>\r\n" +
                //    "    <div>http://www.smilingalbino.com/</div>\r\n" +
                //    "    <div>Ph: +66-2-107-2540</div>\r\n" +
                //    "    <div>\"See a different part of the world, differently\"</div>\r\n" +
                //    " </center>\r\n" +
                //    "</div>\r\n";

                Html +=
                    "</div>\r\n" +
                    "</center>\r\n";


                RetAjaxOut.HtmlContent = Html;
                RetAjaxOut.RetUrl = OSiteParam.HttpRoot + "/preview.aspx?sv=quotation&id=" + QuotationId + "&ViewType=" + ViewType;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

    }
}
