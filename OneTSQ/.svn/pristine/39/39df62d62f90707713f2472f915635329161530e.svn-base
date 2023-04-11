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
    public class QuotationTemplateItemItineraryProcess
    {
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDrawSearchItinerary(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateItemId)
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
                    "    <div style=\"text-align:right\"><button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallDoSearchItinerary('" + QuotationTemplateItemId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tìm kiếm") + "</strong></button> <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBackFromLookUpForm();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button></div> \r\n" +
                    "     <div class=\"ibox-content\"> \r\n" +
                    "         <div class=\"row\"> \r\n" +
                    "             <div class=\"col-sm-12\"> \r\n" +
                    "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                    "                 <div><input onkeypress=\"if(event.keyCode==13){CallDoSearchItinerary('" + QuotationTemplateItemId + "');}\"  id=\"txtSearchKeywordItinerary\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                    "             </div> \r\n" +
                    "         </div> \r\n" +
                    "         <div id=\"divProcessingSearchItinerary\" style=\"padding:3px;color:green;height:20px\" class=\"processing\"></div>\r\n" +
                    "         <div id=\"divSearchItineraryResultContent\">" + ServerSideDrawSearchItineraryResult(ORenderInfo, QuotationTemplateItemId, "", 0).HtmlContent + "</div>\r\n" +
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
            string QuotationTemplateItemId, 
            string Keyword,
            int CurrentPageIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                QuotationTemplateItemCls
                    OQuotationTemplateItem = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateQuotationTemplateItemModel(ORenderInfo, QuotationTemplateItemId);

                ItineraryFilterCls
                    OItineraryFilter = new ItineraryFilterCls();
                OItineraryFilter.CityId = OQuotationTemplateItem.frkLocationServiceId;
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
                            "                     <td class=\"td-center\" style=\"width:20px\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Chọn hành trình") + "\" href=\"javascript:CallActionAddItineraryToQuotationTemplateItem('" + QuotationTemplateItemId + "','" + Itineraries[iIndex].ItineraryId + "');\"><i class=\"" + WebScreen.GetPublishGridIcon() + "\"></i></a></td> \r\n" +
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
        public static AjaxOut ServerSideAddItineraryToQuotationTemplateItem(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateItemId,
            string ItineraryId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationTemplateItemCls
                    OQuotationTemplateItem = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateQuotationTemplateItemModel(ORenderInfo, QuotationTemplateItemId);

                ItineraryCls
                    OItinerary = CallBussinessUtility.CreateBussinessProcess().CreateItineraryProcess().CreateModel(ORenderInfo, ItineraryId);
                QuotationTemplateItemItineraryCls
                    OQuotationTemplateItemItinerary = new QuotationTemplateItemItineraryCls();
                OQuotationTemplateItemItinerary.QuotationTemplateItemItineraryId = System.Guid.NewGuid().ToString();
                OQuotationTemplateItemItinerary.frkQuotationTemplateItemId = QuotationTemplateItemId;
                OQuotationTemplateItemItinerary.frkSrcItineraryId = ItineraryId;
                OQuotationTemplateItemItinerary.Subject = OItinerary.Subject;
                OQuotationTemplateItemItinerary.Itinerary = OItinerary.Body;
                OQuotationTemplateItemItinerary.ItemSortIndex = 1;

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().AddQuotationTemplateItemItinerary(ORenderInfo, QuotationTemplateItemId, OQuotationTemplateItemItinerary);
                RetAjaxOut.HtmlContent = CreateQuotationTemplateStepThree.ServerSideDrawQuotationTemplateItem(ORenderInfo, QuotationTemplateItemId).HtmlContent; ;
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
        public static AjaxOut ServerSidePreviewQuotationTemplateItemItinerary(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateItemItineraryId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationTemplateItemItineraryCls
                    OQuotationTemplateItemItinerary = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateQuotationTemplateItemItineraryModel(ORenderInfo, QuotationTemplateItemItineraryId);

                RetAjaxOut.HtmlContent = OQuotationTemplateItemItinerary.Itinerary;
                RetAjaxOut.InfoMessage = OQuotationTemplateItemItinerary.Subject;
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
            string QuotationTemplateItemId,
            string QuotationTemplateItemItineraryId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().DeleteQuotationTemplateItemItinerary(ORenderInfo, QuotationTemplateItemItineraryId);

                RetAjaxOut = CreateQuotationTemplateStepThree.ServerSideDrawQuotationTemplateItem(ORenderInfo, QuotationTemplateItemId);
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
