using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebParts
{
    public class QuotationItemItineraryProcess
    {
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDrawSearchItinerary(
            RenderInfoCls ORenderInfo,
            string QuotationItemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                Html =
                    "    <input id=\"txtCurrentPageIndexItinerary\" value=\"0\" type=\"hidden\">\r\n" +
                    "    <h3 style=\"color:green;font-weight:bold\">"+WebLanguage.GetLanguage(OSiteParam,"Hành trình")+"</h3>\r\n"+
                    "    <div style=\"text-align:right\"><button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallDoSearchItinerary('" + QuotationItemId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tìm kiếm") + "</strong></button> <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBackFromLookUpForm();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button></div> \r\n" +
                    "     <div class=\"ibox-content\"> \r\n" +
                    "         <div class=\"row\"> \r\n" +
                    "             <div class=\"col-sm-12\"> \r\n" +
                    "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                    "                 <div><input onkeypress=\"if(event.keyCode==13){CallDoSearchItinerary('" + QuotationItemId + "');}\"  id=\"txtSearchKeywordItinerary\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                    "             </div> \r\n" +
                    "         </div> \r\n" +
                    "         <div id=\"divProcessingSearchItinerary\" style=\"padding:3px;color:green;height:20px\" class=\"processing\"></div>\r\n" +
                    "         <div id=\"divSearchItineraryResultContent\">" + ServerSideDrawSearchItineraryResult(ORenderInfo, QuotationItemId, "", 0).HtmlContent + "</div>\r\n" +
                    "     </div> \r\n";

                RetAjaxOut.HtmlContent = Html;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDrawSearchItineraryResult(
            RenderInfoCls ORenderInfo, 
            string QuotationItemId, 
            string Keyword,
            int CurrentPageIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                QuotationItemCls
                    OQuotationItem = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationItemModel(ORenderInfo, QuotationItemId);

                ItineraryFilterCls
                    OItineraryFilter = new ItineraryFilterCls();
                OItineraryFilter.CityId = OQuotationItem.frkLocationServiceId;
                OItineraryFilter.Keyword = Keyword;

                ItineraryCls[]
                    Itineraries = CallBussinessUtility.CreateBussinessProcess().CreateItineraryProcess().Reading(ORenderInfo, OItineraryFilter);

                if (Itineraries.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có hành trình theo điều kiện lọc", true);
                }
                else
                {
                    ReturnPaging
                        OReturnPaging = WebPaging.GetPaging(Itineraries.Length, CurrentPageIndex, 15, 4, "NextPageSearchItinerary");
                    Html +=
                        "   <div class=\"search-result-info\">" + WebLanguage.GetLanguage(OSiteParam, "Tìm thấy") + " (" + Itineraries.Length.ToString("#,##0") + ") " + WebLanguage.GetLanguage(OSiteParam, "hành trình theo điều kiện lọc") + "</div>\r\n" +
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Hành trình") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = OReturnPaging.StartIndex; iIndex < OReturnPaging.EndIndex; iIndex++)
                    {
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td><a href=\"javascript:CallActionViewItinerary('" + Itineraries[iIndex].ItineraryId + "');\">" + Itineraries[iIndex].Subject + "</a></td> \r\n" +
                            "                     <td class=\"td-center\" style=\"width:20px\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Chọn hành trình") + "\" href=\"javascript:CallActionAddItineraryToQuotationItem('" + QuotationItemId + "','" + Itineraries[iIndex].ItineraryId + "');\"><i class=\"" + WebScreen.GetPublishGridIcon() + "\"></i></a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n";
                }

                RetAjaxOut.HtmlContent = Html;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideAddItineraryToQuotationItem(
            RenderInfoCls ORenderInfo,
            string QuotationItemId,
            string ItineraryId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationItemCls
                    OQuotationItem = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationItemModel(ORenderInfo, QuotationItemId);

                ItineraryCls
                    OItinerary = CallBussinessUtility.CreateBussinessProcess().CreateItineraryProcess().CreateModel(ORenderInfo, ItineraryId);
                QuotationItemItineraryCls
                    OQuotationItemItinerary = new QuotationItemItineraryCls();
                OQuotationItemItinerary.QuotationItemItineraryId = System.Guid.NewGuid().ToString();
                OQuotationItemItinerary.frkQuotationItemId = QuotationItemId;
                OQuotationItemItinerary.frkSrcItineraryId = ItineraryId;
                OQuotationItemItinerary.Subject = OItinerary.Subject;
                OQuotationItemItinerary.Itinerary = OItinerary.Body;
                OQuotationItemItinerary.ItemSortIndex = 1;

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().AddQuotationItemItinerary(ORenderInfo, QuotationItemId, OQuotationItemItinerary);
                RetAjaxOut.HtmlContent = CreateQuotationStepThree.ServerSideDrawQuotationItem(ORenderInfo, QuotationItemId).HtmlContent; ;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }


        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSidePreviewQuotationItemItinerary(
            RenderInfoCls ORenderInfo,
            string QuotationItemItineraryId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationItemItineraryCls
                    OQuotationItemItinerary = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationItemItineraryModel(ORenderInfo, QuotationItemItineraryId);

                RetAjaxOut.HtmlContent = OQuotationItemItinerary.Itinerary;
                RetAjaxOut.InfoMessage = OQuotationItemItinerary.Subject;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSidePreviewItinerary(
            RenderInfoCls ORenderInfo,
            string ItineraryId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                ItineraryCls
                    OItinerary = CallBussinessUtility.CreateBussinessProcess().CreateItineraryProcess().CreateModel(ORenderInfo, ItineraryId);

                RetAjaxOut.HtmlContent = OItinerary.Body;
                RetAjaxOut.InfoMessage = OItinerary.Subject;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }



        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideRemoveItinerary(
            RenderInfoCls ORenderInfo,
            string QuotationItemId,
            string QuotationItemItineraryId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().DeleteQuotationItemItinerary(ORenderInfo, QuotationItemItineraryId);

                RetAjaxOut = CreateQuotationStepThree.ServerSideDrawQuotationItem(ORenderInfo, QuotationItemId);
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
