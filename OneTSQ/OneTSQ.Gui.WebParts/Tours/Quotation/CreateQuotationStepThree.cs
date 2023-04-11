using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.CallTempService;
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
    public class CreateQuotationStepThree : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "CreateQuotationStepThree";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Xây dựng chào giá - Chọn dịch vụ";
            }
        }

        public override string Description
        {
            get
            {
                return "Xây dựng chào giá - Chọn dịch vụ";
            }
        }


        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CreateQuotationStepThree),Page);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(QuotationItemItineraryProcess), Page);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(QuotationServiceProcess), Page);
        }

        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true;
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }

        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { new CreateQuotationStepOne().WebPartId };
            }
        }

        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                AjaxOut RetAjaxOutCheckPermission = CheckPermission(ORenderInfo);
                if (RetAjaxOutCheckPermission.RetBoolean == false)
                {
                    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Không có quyền truy cập tính năng này"), false);
                    return RetAjaxOut;
                }


                string QuotationId = (string)WebEnvironments.Request("Id");
                if (string.IsNullOrEmpty(QuotationId))
                {
                    QuotationId = "";
                }

                string DayIndex = (string)WebEnvironments.Request("DayIndex");
                if (string.IsNullOrEmpty(DayIndex))
                {
                    DayIndex = "0";
                }
                RetAjaxOut.HtmlContent =

                    "<style>\r\n"+
                    ".leftSidebar {\r\n"+
				    "    width: 25%;\r\n"+
				    "    float: left;\r\n"+
				    "    padding: 0 30px 0 0;\r\n"+
				    "    position: relative;\r\n"+
			        "}\r\n"+

			        ".rightSidebar {\r\n"+
				    "    position: relative;\r\n"+
                    "}\r\n" +
                    "</style>\r\n"+

                    "<input id=\"txtQuotationId\" type=\"hidden\" value=\"" + QuotationId + "\">\r\n" +
                    "<script>\r\n" +

                    "   function CallActionSaveTemplate(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       Subject = document.getElementById('txtQuotationTemplateName').value;\r\n"+
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideAddQuotationTemplate(RenderInfo, QuotationId, Subject).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "       CallBackFromLookUpForm();\r\n"+
                    "   }\r\n" +

                    "   function CallActionCreateBooking(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       Subject = document.getElementById('txtBookingSubject').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideCreateBooking(RenderInfo, QuotationId, Subject).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "       CallBackFromLookUpForm();\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n" +


                    "   function CallPreview(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideCallPreview(RenderInfo, QuotationId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_blank');\r\n"+
                    "   }\r\n" +

                    "   function CallCaculateMoney(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideCaculateMoney(RenderInfo, QuotationId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function CallMakeTemplate(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideDrawAddMakeTemplateForm(RenderInfo, QuotationId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "   }\r\n" +

                    "   function CallMakeBookingFromQuotation(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideDrawAddMakeBooking(RenderInfo, QuotationId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "   }\r\n" +


                    "   function CallExportPdf(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('divProcessingExport').innerHTML='"+WebLanguage.GetLanguage(OSiteParam,"Đang xuất dữ liệu")+"...';\r\n"+
                    "       setTimeout('RealCallExportPdf(\"'+QuotationId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function RealCallExportPdf(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideCallExportPdf(RenderInfo, QuotationId).value;\r\n" +
                    "       document.getElementById('divProcessingExport').innerHTML='';\r\n"+
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divProcessingExport').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "   }\r\n" +

                    "   function CallCloseQuotation(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideCallCloseQuotation(RenderInfo, QuotationId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n" +

                    "   function CallPendingQuotation(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideCallPendingQuotation(RenderInfo, QuotationId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function CallReOpenQuotation(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideCallReOpenQuotation(RenderInfo, QuotationId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +


                    "   function SaveUpdateQuotationIndexItem(QuotationItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndexQuotationItem'+QuotationItemId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideSaveQuotationItemIndex(RenderInfo, QuotationItemId, SortIndex).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +



                    "   function SaveUpdateQuantityItem(QuotationServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       QuotationId= document.getElementById('txtQuotationId').value;\r\n"+
                    "       Quantity = document.getElementById('txtQuantity'+QuotationServiceId).value;\r\n"+
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideSaveUpdateQuantity(RenderInfo, QuotationId, QuotationServiceId, Quantity).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+QuotationServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "       document.getElementById('txtQuantity'+QuotationServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n"+

                    "   function SaveUpdateNoOfServiceDayssItem(QuotationServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       QuotationId= document.getElementById('txtQuotationId').value;\r\n"+
                    "       NoOfServiceDays = document.getElementById('txtNoOfServiceDays'+QuotationServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideSaveUpdateNoOfServiceDays(RenderInfo, QuotationId, QuotationServiceId, NoOfServiceDays).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+QuotationServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "       document.getElementById('txtNoOfServiceDays'+QuotationServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n"+

                    "   function SaveUpdateStartDateItem(QuotationServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       QuotationId= document.getElementById('txtQuotationId').value;\r\n"+
                    "       StartDate = document.getElementById('txtStartDateItem'+QuotationServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideSaveUpdateStartDate(RenderInfo, QuotationId, QuotationServiceId, StartDate).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+QuotationServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "   }\r\n"+



                    "   function SaveUpdateStartTimeItem(QuotationServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       QuotationId= document.getElementById('txtQuotationId').value;\r\n" +
                    "       StartTime = document.getElementById('txtStartTimeItem'+QuotationServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideSaveUpdateStartTimeItem(RenderInfo, QuotationId, QuotationServiceId, StartTime).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+QuotationServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +


                    "   function SaveUpEndTimeItem(QuotationServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       QuotationId= document.getElementById('txtQuotationId').value;\r\n" +
                    "       EndTime = document.getElementById('txtEndTimeItem'+QuotationServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideSaveUpdateEndTimeItem(RenderInfo, QuotationId, QuotationServiceId, EndTime).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+QuotationServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +

                    "   function SaveUpdatePriceItem(QuotationServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       QuotationId= document.getElementById('txtQuotationId').value;\r\n" +
                    "       Price = document.getElementById('txtPrice'+QuotationServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideSaveUpdatePrice(RenderInfo, QuotationId, QuotationServiceId, Price).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+QuotationServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('txtPrice'+QuotationServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n" +

                    "   function SaveUpdateExtraFeeItem(QuotationServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       QuotationId= document.getElementById('txtQuotationId').value;\r\n" +
                    "       ExtraFee = document.getElementById('txtExtraFee'+QuotationServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideSaveUpdateExtraFee(RenderInfo, QuotationId, QuotationServiceId, ExtraFee).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+QuotationServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('txtExtraFee'+QuotationServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n" +


                    "   function AddQuotationItemForm(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideDrawAddQuotationItemForm(RenderInfo, QuotationId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "       $('#drpSelectLocationService').select2();\r\n"+
                    "       $('#drpSelectServiceTypeCategory').select2();\r\n" +
                    "   }\r\n"+

                    "   function CallAddQuotationItemAction(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       LocationServiceId = document.getElementById('drpSelectLocationService').value;\r\n"+
                    "       HotelCategoryId = document.getElementById('drpSelectServiceTypeCategory').value;\r\n"+
                    "       Night = parseInt(document.getElementById('txtNight').value,10);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideAddQuotationItem(RenderInfo, QuotationId, LocationServiceId, HotelCategoryId, Night).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n"+


                    "   function CallSortForm(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideDrawSortForm(RenderInfo, QuotationId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "   }\r\n"+



                    
                    "   function CallAddSectionService(QuotationItemId,ServiceTypeId)\r\n"+
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationServiceProcess.ServerSideAddSectionService(RenderInfo, QuotationItemId, ServiceTypeId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(document.getElementById('imgicon'+QuotationItemId+ServiceTypeId)!=null)\r\n"+
                    "       {\r\n"+
                    "           document.getElementById('imgicon'+QuotationItemId+ServiceTypeId).style='opacity:1';\r\n"+
                    "       }\r\n"+
                    "       document.getElementById('divQuotationItemContent'+QuotationItemId).innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "   }\r\n" +


                    "   function RemoveQuotationSessionService(QuotationItemId, QuotationSectionServiceId)\r\n" +

                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa nhóm dịch vụ") + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +


                    "           AjaxOut = OneTSQ.WebParts.QuotationServiceProcess.ServerSideRemoveSectionService(RenderInfo, QuotationItemId, QuotationSectionServiceId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           document.getElementById('divQuotationItemContent'+QuotationItemId).innerHTML=AjaxOut.HtmlContent;\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "Nhóm dịch vụ được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +


                    "   }\r\n" +

                    "   function CallActionDeleteQuotationService(QuotationId, QuotationSectionServiceId, QuotationServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa dịch vụ") + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +


                    "           AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideDeleteQuotationService(RenderInfo, QuotationId, QuotationSectionServiceId, QuotationServiceId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           document.getElementById('divListQuotationSectionService'+QuotationSectionServiceId).innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "           document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.RetExtraParam1;\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "Dịch vụ được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +


                    "   }\r\n" +


                    "   function DeleteQuotationItem(QuotationId, QuotationItemId)\r\n" +

                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                        
                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa điểm đến hành trình") + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +


                    "           AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideDeleteQuotationItem(RenderInfo, QuotationId, QuotationItemId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "       }); \r\n" +


                    "   }\r\n" +


                    "   function CallBackFromLookUpForm()\r\n" +
                    "   {\r\n"+
                    "       document.getElementById('divRightInfoContent').style.display='block';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML='';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='none';\r\n"+
                    "   }\r\n"+

                    

                    "   function AddPlusService(QuotationItemId)\r\n" +
                    "   {\r\n"+
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationServiceProcess.ServerSideDrawAddServiceToQuotationItemForm(RenderInfo, QuotationItemId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n"+
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n"+
                    "   }\r\n"+

                    "   function CallLookupItineraryForm(QuotationItemId)\r\n" +
                    "   {\r\n"+
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationItemItineraryProcess.ServerSideDrawSearchItinerary(RenderInfo, QuotationItemId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n"+
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n"+
                    "   }\r\n"+

                    
                    "   function CallDoSearchItinerary(QuotationItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ActiveQuotationItemId = QuotationItemId;\r\n"+
                    "       document.getElementById('txtCurrentPageIndexItinerary').value='0';\r\n" +
                    "       document.getElementById('divProcessingSearchItinerary').innerHTML='"+WebLanguage.GetLanguageProcessing(OSiteParam)+"';\r\n"+
                    "       setTimeout('RealCallDoSearchItinerary(\"'+QuotationItemId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function NextPageSearchItinerary(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('txtCurrentPageIndexItinerary').value=PageIndex;\r\n"+
                    "       document.getElementById('divProcessingSearchItinerary').innerHTML='"+WebLanguage.GetLanguageProcessing(OSiteParam)+"';\r\n"+
                    "       setTimeout('RealCallDoSearchItinerary(\"'+ActiveQuotationItemId+'\")',10);\r\n" +
                    "   }\r\n" +


                    "   function RealCallDoSearchItinerary(QuotationItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       Keyword = document.getElementById('txtSearchKeywordItinerary').value;\r\n" +
                    "       CurrentPageIndex =  parseInt(document.getElementById('txtCurrentPageIndexItinerary').value,10);\r\n"+
                    "       AjaxOut = OneTSQ.WebParts.QuotationItemItineraryProcess.ServerSideDrawSearchItineraryResult(RenderInfo, QuotationItemId, Keyword, CurrentPageIndex).value;\r\n" +
                    "       document.getElementById('divProcessingSearchItinerary').innerHTML='';\r\n"+
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divSearchItineraryResultContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +


                    "   function CallPreviewQuotationItinerary(QuotationItemItineraryId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationItemItineraryProcess.ServerSidePreviewQuotationItemItinerary(RenderInfo, QuotationItemItineraryId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('ModalTitle').innerHTML=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       $('#divFormModal').modal();\r\n"+
                    "   }\r\n" +

                    "   function CallActionViewItinerary(ItineraryId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationItemItineraryProcess.ServerSidePreviewItinerary(RenderInfo, ItineraryId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('ModalTitle').innerHTML=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       $('#divFormModal').modal();\r\n"+
                    "   }\r\n" +


                    "   function RemoveItinerary(QuotationItemId, QuotationItemItineraryId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +



                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa") + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +


                    "           AjaxOut = OneTSQ.WebParts.QuotationItemItineraryProcess.ServerSideRemoveItinerary(RenderInfo, QuotationItemId, QuotationItemItineraryId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           document.getElementById('divQuotationItemContent'+QuotationItemId).innerHTML=AjaxOut.HtmlContent;\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "Hành trình được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +

                    
                    "   }\r\n" +



                    "   function CallActionUpdateCustomerInfo(QuotationId)\r\n" +
                    "   {\r\n"+
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       StartDate = document.getElementById('txtStartDate').value;\r\n" +
                    "       Adults = document.getElementById('txtAdults').value;\r\n" +
                    "       Children = document.getElementById('txtChildren').value;\r\n" +
                    "       NoOfRoom = document.getElementById('txtNoOfRoom').value;\r\n" +
                    "       HotelCategoryId = document.getElementById('drpSelectServiceTypeCategory').value;\r\n" +


                    "       CustomerName = document.getElementById('txtCustomerName').value;\r\n" +
                    "       CustomerAddress = document.getElementById('txtCustomerAddress').value;\r\n" +
                    "       CustomerMobile = document.getElementById('txtCustomerMobile').value;\r\n" +
                    "       CustomerEmail = document.getElementById('txtCustomerEmail').value;\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideSaveCustomerInfo(RenderInfo, QuotationId, StartDate, Adults, Children, NoOfRoom, HotelCategoryId, CustomerName, CustomerAddress, CustomerMobile, CustomerEmail).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "   }\r\n"+

                    "   function CallActionUpdateExtraInfo(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       QuotationSubject = document.getElementById('txtQuotationSubject').value;\r\n"+
                    "       Css = document.getElementById('txtCss').value;\r\n" +
                    "       Header = tinyMCE.get('txtHeader').getContent();\r\n" +
                    "       Footer = tinyMCE.get('txtFooter').getContent();\r\n" +
                    "       Include = tinyMCE.get('txtInclude').getContent();\r\n" +
                    "       Exclude = tinyMCE.get('txtExclude').getContent();\r\n" +
                    "       SpecialCondition = tinyMCE.get('txtSpecialCondition').getContent();\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepThree.ServerSideSaveExtraInfo(RenderInfo, QuotationId, QuotationSubject, Header, Footer, Include, Exclude, SpecialCondition, Css).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "   }\r\n" +

                    "   function CallActionAddItineraryToQuotationItem(QuotationItemId, ItineraryId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.QuotationItemItineraryProcess.ServerSideAddItineraryToQuotationItem(RenderInfo, QuotationItemId, ItineraryId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divQuotationItemContent'+QuotationItemId).innerHTML = AjaxOut.HtmlContent;\r\n"+
                    "   }\r\n" +

                    


                    "   ActiveQuotationItemId=null;\r\n"+
                    "   ActiveQuotationSectionServiceId=null;\r\n"+

                    "   function CallLookupServiceForm(QuotationItemId, QuotationSectionServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ActiveQuotationItemId = QuotationItemId;\r\n"+
                    "       ActiveQuotationSectionServiceId = QuotationSectionServiceId;\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.QuotationServiceProcess.ServerSideDrawSearchService(RenderInfo, QuotationItemId, QuotationSectionServiceId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "       document.getElementById('txtSearchKeywordService').focus();\r\n"+
                    "       $('#drpSelectSupplier').select2();\r\n"+
                    //"       setTimeout('CallDoSearchService(\"'+QuotationItemId+'\",\"'+QuotationSectionServiceId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function NextPageSearchService(CurrentPageIndex)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('txtCurrentPageIndexService').value = CurrentPageIndex;\r\n"+
                    "       document.getElementById('divProcessingSearchService').innerHTML='" + WebLanguage.GetLanguageProcessing(OSiteParam) + "';\r\n" +
                    "       setTimeout('RealCallDoSearchService(\"'+ActiveQuotationItemId+'\",\"'+ActiveQuotationSectionServiceId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function CallDoSearchService(QuotationItemId, QuotationSectionServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('txtCurrentPageIndexService').value = '0';\r\n" +
                    "       document.getElementById('divProcessingSearchService').innerHTML='" + WebLanguage.GetLanguageProcessing(OSiteParam) + "';\r\n" +
                    "       setTimeout('RealCallDoSearchService(\"'+QuotationItemId+'\",\"'+QuotationSectionServiceId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function RealCallDoSearchService(QuotationItemId, QuotationSectionServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       SupplierId = document.getElementById('drpSelectSupplier').value;\r\n" +
                    "       Keyword = document.getElementById('txtSearchKeywordService').value;\r\n" +
                    "       CurrentPageIndex = parseInt(document.getElementById('txtCurrentPageIndexService').value,10);\r\n"+
                    "       AjaxOut = OneTSQ.WebParts.QuotationServiceProcess.ServerSideDrawSearchServiceResult(RenderInfo, QuotationItemId, QuotationSectionServiceId, SupplierId, Keyword, CurrentPageIndex).value;\r\n" +
                    "       document.getElementById('divProcessingSearchService').innerHTML='';\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divSearchServiceResultContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +



                    "   function CallActionAddServiceToQuotationItem(QuotationItemId, QuotationSectionServiceId, ServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationServiceProcess.ServerSideAddServiceToQuotationItem(RenderInfo, QuotationItemId, QuotationSectionServiceId, ServiceId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divQuotationItemContent'+QuotationItemId).innerHTML = AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML = AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n" +


                    "   function CallPreviewQuotationService(QuotationItemServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationServiceProcess.ServerSidePreviewQuotationItemService(RenderInfo, QuotationItemServiceId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('ModalTitle').innerHTML=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       $('#divFormModal').modal();\r\n" +
                    "   }\r\n" +



                    "</script>\r\n" +

                     ServerSideDrawCreateQuotationStepThree(ORenderInfo, QuotationId, DayIndex).HtmlContent +

                    "<script>\r\n" +
                    "    $(document).ready(function(){ \r\n"+


                    " $(\".touchspin3\").TouchSpin({ \r\n" +
                    "    verticalbuttons: true, \r\n" +
                    "    buttondown_class: 'btn btn-white', \r\n" +
                    "    buttonup_class: 'btn btn-white' \r\n" +
                    " }); \r\n" +

                    " $('.CssDate').datepicker({\r\n" +
                    "   format: 'dd/mm/yyyy'\r\n" +
                    " });\r\n" +

                    "    }); \r\n"+
                    "</script>\r\n";

                
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.HtmlContent = ex.Message.ToString();
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

      

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDrawCreateQuotationStepThree(RenderInfoCls ORenderInfo, string QuotationId, string DayIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                QuotationCls
                     OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);


                Html =
                    "<div id=\"divQuotation\">\r\n"+
                    "<input id=\"txtDayIndex\" value=\"" + DayIndex + "\" type=\"hidden\">\r\n" +
                    "<div class=\"modal inmodal\" id=\"divFormModal\" tabindex=\"-1\" role=\"dialog\" aria-hidden=\"true\"> \r\n" +
                    "    <div class=\"modal-dialog\"> \r\n" +
                    "    <div class=\"modal-content animated bounceInRight\"> \r\n" +
                    "            <div class=\"modal-header\"> \r\n" +
                    "            <button type=\"button\" class=\"close\" data-dismiss=\"modal\"><span aria-hidden=\"true\">&times;</span><span class=\"sr-only\">Close</span></button>\r\n" +
                    "                <h4 class=\"modal-title\" id=\"ModalTitle\"></h4> \r\n" +
                    "            </div> \r\n" +
                    "            <div class=\"modal-body\" id=\"divModalContent\"> \r\n" +

                    "            </div> \r\n" +
                    "            <div class=\"modal-footer\"> \r\n" +
                    "                <button type=\"button\" class=\"btn btn-white\" data-dismiss=\"modal\">Close</button> \r\n" +
                    //"                <button type=\"button\" class=\"btn btn-primary\">Save changes</button> \r\n" +
                    "            </div> \r\n" +
                    "        </div> \r\n" +
                    "    </div> \r\n" +
                    "</div> \r\n" +


                    "<div class=\"ibox float-e-margins\"> \r\n" +
                    "    <div class=\"ibox-title\" style=\"min-height:43px !important;padding-top:4px\"> \r\n" +
                    "        <h5 style=\"padding-top:8px;padding-left:10px;color:#337ab7;font-size:20px\">Id: " + OQuotation.AutoId.ToString()+"</h5> \r\n";

                if (OQuotation.frkQuotationStatusId.Equals("pending"))
                {
                        Html +=
                            "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallCloseQuotation('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Đóng chào giá") + "</strong></button>\r\n"+
                            "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallReOpenQuotation('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Mở lại chào giá") + "</strong></button>\r\n";
                }
                else
                {
                    if (OQuotation.frkQuotationStatusId.Equals("open"))
                    {
                        Html +=
                            "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallPendingQuotation('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tạm dừng") + "</strong></button>\r\n"+
                            "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallCloseQuotation('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Đóng chào giá") + "</strong></button>\r\n";
                    }
                    else
                    {
                        Html +=
                            "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallReOpenQuotation('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Mở lại chào giá") + "</strong></button>\r\n";
                    }
                }
                Html+=
                    "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallExportPdf('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Xuất Pdf") + "</strong></button>\r\n" +
                    "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallPreview('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Xuất Html") + "</strong></button>\r\n" +
                    "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallCaculateMoney('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tính giá tự động") + "</strong></button>\r\n"+
                    "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallMakeTemplate('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tạo mẫu") + "</strong></button>\r\n" +
                    "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallMakeBookingFromQuotation('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tạo booking") + "</strong></button>\r\n" +
                    "    </div> \r\n" +
                    "    <div id=\"divProcessingExport\" style=\"text-align:right;padding:4px; color:maroon;font-size:16px;font-weight:bold\"></div>\r\n"+
                    "    <div class=\"ibox-content\"> \r\n" +
                    "       <div><table><tr><td style=\"padding:3px !important;\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Ngày khởi hành") + ":</strong></td><td style=\"padding:3px !important;\"><span style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + OQuotation.StartDate.ToString("dd/MM/yyyy") + "</span></td><td style=\"padding:3px !important;\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Ngày kết thúc") + ":</strong></td><td style=\"padding:3px !important;\"><span style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + OQuotation.EndDate.ToString("dd/MM/yyyy") + "</span></td><td style=\"padding:3px !important;\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Số ngày") + ":</strong></td><td style=\"padding:3px !important;\"><span style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + OQuotation.NoOfDays.ToString("#,##0") + "</span></td></tr></table></div>\r\n" +
                    "       <div style=\"padding:3px !important;\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin") + "</strong>:<img src=\"images/adult.png\" style=\"padding-left:5px;height:16px\"/><span style=\"color:#337ab7;font-weight:bold;padding:1px !important;font-size:16px\"> " + OQuotation.Adults.ToString("#,##0") + "</span> <img src=\"images/children.png\" style=\"padding-left:5px;height:16px\"/> <span style=\"color:#337ab7;font-weight:bold;padding:1px !important;font-size:16px\">" + OQuotation.Children + "</span>   <img src=\"images/room.png\" style=\"padding-left:5px;height:16px\"/> <span style=\"color:#337ab7;font-weight:bold;padding:1px !important;font-size:16px\">" + OQuotation.NoOfRoom.ToString("#,##0") + "</span>    <a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm hành trình") + "\" href=\"javascript:AddQuotationItemForm('" + QuotationId + "');\"><img src=\"images/plus.png\" style=\"padding-left:5px;height:16px\"/></a></div>\r\n"+

                    "       <div class=\"row\">\r\n" +
                    "           <div class=\"col-md-8 wrapper wrapper-content\" id=\"sortable-view\">\r\n" +
                                    ServerSideDrawList(ORenderInfo, QuotationId, int.Parse(DayIndex)).HtmlContent +
                    "           </div>\r\n" +
                    "           <div class=\"col-md-4 rightSidebar\">\r\n" +
                    "               <div id=\"divRightInfoContent\" >\r\n" +
                                        ServerSideDrawRightInfo(ORenderInfo, QuotationId).HtmlContent +
                    "               </div>\r\n"+
                    "               <div id=\"divLookUpServiceContent\">\r\n" +

                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "       </div>\r\n" +
                    "   </div>\r\n" +
                    "</div> \r\n" +
                    "</div>\r\n"+//divQuotation

                    "<script>\r\n" +
                    WebScreen.GetMceEditor("txtInclude",200)+
                    WebScreen.GetMceEditor("txtExclude", 200) +
                    WebScreen.GetMceEditor("txtHeader", 200) +
                    WebScreen.GetMceEditor("txtFooter", 200) +
                    WebScreen.GetMceEditor("txtSpecialCondition", 200) +
                    " $('.CssDate').datepicker({\r\n" +
                    "   format: 'dd/mm/yyyy'\r\n" +
                    " });\r\n" +

                    " $(document).ready(function() { \r\n"+
				    "     $('.rightSidebar') \r\n"+
				    "    .theiaStickySidebar({ \r\n"+
    			    "			additionalMarginTop: 30 \r\n"+
				    "	    }); \r\n"+
                    " }); \r\n" +

                    "</script>\r\n"+

                    "<style>\r\n" +
                    "   #divQuotation td{padding:1px !important;margin:0px;font-size:10px !important}\r\n" +
                    "   #divQuotation th{padding:1px !important;margin:0px;font-size:10px !important}\r\n" +
                    "   #divQuotation .form-control{font-size:10px;padding:1px; !important;height:20px!important}\r\n" +
                    "   #divQuotation li{padding:1px !important;margin:0px;font-size:10px !important}\r\n" +
                    "   #divQuotation .ibox-content{padding:3px !important;}\r\n" +
                    "   #divQuotation .ibox-title{padding:3px !important;min-height:30px!important; height:30px!important}\r\n" +
                    "</style>\r\n";

                Html = WebEnvironments.EncryptHtml(Html);
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
        public static AjaxOut ServerSideDrawList(RenderInfoCls ORenderInfo, string QuotationId, int DayIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                QuotationItemCls[]
                    QuotationItems = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingQuotationItems(ORenderInfo, QuotationId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                Html += "<table><tr>\r\n";
                for (int iIndex = 0; iIndex <= QuotationItems.Length; iIndex++)
                {
                    string Url=WebScreen.BuildUrl(OSiteParam,OwnerCode,new CreateQuotationStepThree().WebPartId,new WebParamCls[]
                    {
                        new WebParamCls("Id",QuotationId),
                        new WebParamCls("DayIndex",iIndex),
                    });
                    Html += "<td style=\"padding:2px\"><a href=\""+Url+"\" target=\"_self\">"+FunctionUtility.GetNumberImage(iIndex)+"</a></td>\r\n";
                }
                Html += "<td style=\"padding:2px\"><a href=\"javascript:CallSortForm('"+QuotationId+"');\" target=\"_self\"><img src=\"images/sort.png\" /></a></td>\r\n";
                Html += "</tr></table>\r\n";
                for (int iIndex = 0; iIndex < QuotationItems.Length; iIndex++)
                {
                    bool Allow = true;
                    if (DayIndex > 0)
                    {
                        if (DayIndex == QuotationItems[iIndex].DayIndex)
                        {
                            Allow = true;
                        }
                        else
                        {
                            Allow = false;
                        }
                    }

                    if (Allow)
                    {
                        string DrawPlugInButtonText =
                            "<img  style=\"opacity:1\" onclick=\"javascript:CallLookupItineraryForm('" + QuotationItems[iIndex].QuotationItemId + "');\"  id=\"imgiconitinerary\"  title=\"Itinerary\"   class=\"icon-service\" src=\"images/icon-itinerary.png\" />";
                        string AppendStyle = "";
                        ServiceTypeCls[]
                            ItemServiceType = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingServiceType(ORenderInfo, QuotationItems[iIndex].QuotationItemId);

                        for (int iIndexServiceType = 0; iIndexServiceType < ItemServiceType.Length; iIndexServiceType++)
                        {
                            if (ItemServiceType[iIndexServiceType].AllowService == 1)
                            {
                                AppendStyle = "style=\"opacity:0.25\"";
                                if (ItemServiceType[iIndexServiceType].CheckService == 1)
                                {
                                    AppendStyle = "style=\"opacity:1\"";
                                }
                                DrawPlugInButtonText += "<img " + AppendStyle + " onclick=\"javascript:CallAddSectionService('" + QuotationItems[iIndex].QuotationItemId + "','" + ItemServiceType[iIndexServiceType].ServiceTypeId + "');\"  id=\"imgicon" + QuotationItems[iIndex].QuotationItemId + ItemServiceType[iIndexServiceType].ServiceTypeId + "\"  title=\"" + ItemServiceType[iIndexServiceType].ServiceTypeName + "\"   class=\"icon-service\" src=\"images/icon-" + ItemServiceType[iIndexServiceType].ServiceTypeId + ".png\" /> <input id=\"chk" + QuotationItems[iIndex].QuotationItemId + ItemServiceType[iIndexServiceType].ServiceTypeId + "\"    type=checkbox style=\"display:none\" " + (ItemServiceType[iIndexServiceType].CheckService == 1 ? "CHECKED" : "") + ">";
                            }
                        }

                        DrawPlugInButtonText +=
                            " <a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm dịch vụ vào hành  trình") + "\" href=\"javascript:AddPlusService('" + QuotationItems[iIndex].QuotationItemId + "');\"><img src=\"images/plus.png\" style=\"padding-left:5px;height:16px\"/></a>";

                        string AppendNight = "<td>" + WebLanguage.GetLanguage(OSiteParam, "Đêm") + ": " + QuotationItems[iIndex].Night.ToString("#,##0") + " <img style=\"height:16px\" src=\"images/night.png\"/></td>";
                        
                        Html +=
                            "    <div class=\"ibox \">\r\n" +
                            "        <div class=\"ibox-title\" style=\"background-color:#ada8a0;padding-top:0px;height:35px\">\r\n" +
                            "            <h5><table><tr><td>" + FunctionUtility.GetNumberImage((iIndex + 1)) + "</td><td style=\"width:2px;text-align:center\">|<td><td><span style=\"color:#337ab7;\">" + QuotationItems[iIndex].LocationServiceName + "</span><td><td>" + DrawPlugInButtonText + "</td> <td style=\"width:2px;text-align:center\">|<td>"+ AppendNight+"    <td> <a href=\"javascript:DeleteQuotationItem('" + QuotationId + "','" + QuotationItems[iIndex].QuotationItemId + "');\"><img style=\"height:16px\" src=\"/images/trash.png\"></a></td>  </tr></table></h5>\r\n" +
                            "        </div>\r\n" +
                            "        <div class=\"ibox-content\" id=\"divQuotationItemContent" + QuotationItems[iIndex].QuotationItemId + "\">\r\n" +
                                        ServerSideDrawQuotationItem(ORenderInfo, QuotationItems[iIndex].QuotationItemId).HtmlContent +
                            "        </div>\r\n" +
                            "    </div>\r\n";
                    }
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
        public static AjaxOut ServerSideDrawRightInfo(RenderInfoCls ORenderInfo, string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                Html =

                    "<div class=\"tabs-container\">\r\n" +
                    "    <ul class=\"nav nav-tabs\"> \r\n" +
                    "        <li class=\"active\"><a data-toggle=\"tab\" href=\"#tab-1\">"+WebLanguage.GetLanguage(OSiteParam,"Giá")+"</a></li> \r\n" +
                    "        <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-2\">"+WebLanguage.GetLanguage(OSiteParam,"Chung")+"</a></li> \r\n" +
                    "        <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-3\">"+WebLanguage.GetLanguage(OSiteParam,"Khác")+"</a></li> \r\n" +
                    "    </ul> \r\n" +
                    "    <div class=\"tab-content\"> \r\n" +
                    "        <div id=\"tab-1\" class=\"tab-pane active\"> \r\n" +
                    "            <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +
                    "                   " + ServerSideStaticPriceInfo(ORenderInfo, QuotationId).HtmlContent + "\r\n" +
                    "            </div> \r\n" +
                    "        </div> \r\n" +
                    "        <div id=\"tab-2\" class=\"tab-pane\"> \r\n" +
                    "            <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +
                                        ServerSideDrawGeneralInfo(ORenderInfo, QuotationId).HtmlContent+
                    "            </div> \r\n" +
                    "        </div> \r\n" +
                    "        <div id=\"tab-3\" class=\"tab-pane\"> \r\n" +
                    "            <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +
                                        ServerSideDrawExtraInfo(ORenderInfo, QuotationId).HtmlContent +
                    "            </div> \r\n" +
                    "        </div> \r\n" +
                    "    </div> \r\n" +


                    "</div> \r\n";

                    
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
        public static AjaxOut ServerSideStaticPriceInfo(
            RenderInfoCls ORenderInfo, 
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                Html =
                    "<h3 style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + WebLanguage.GetLanguage(OSiteParam, "Bảng giá dự kiến") + "</h3>\r\n" +
                    "<div id=\"divMoneyStatiticInfo\">\r\n" +
                        ServerSideDrawMoneyStaticInfo(ORenderInfo, QuotationId).HtmlContent +
                    "</div>\r\n";
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
        public static AjaxOut ServerSideDrawGeneralInfo(RenderInfoCls ORenderInfo, string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                  ServiceTypeCategoryFilterCls
                    OServiceTypeCategoryFilter = new ServiceTypeCategoryFilterCls();
                OServiceTypeCategoryFilter.ActiveOnly = 1;

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ServiceTypeCategoryCls[]
                    ServiceTypeCategories = CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeCategoryProcess().Reading(ORenderInfo, OServiceTypeCategoryFilter);

                QuotationCls
                    OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);
                string SelectCategoryRoomText =
                     "<select id=\"drpSelectServiceTypeCategory\" class=\"form-control\">\r\n" +
                     "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n";
                for (int iIndex = 0; iIndex < ServiceTypeCategories.Length; iIndex++)
                {
                    if (OQuotation.frkHotelCategoryId.Equals(ServiceTypeCategories[iIndex].ServiceTypeCategoryId))
                    {
                        SelectCategoryRoomText +=
                            "   <option selected value=\"" + ServiceTypeCategories[iIndex].ServiceTypeCategoryId + "\">" + ServiceTypeCategories[iIndex].ServiceTypeCategoryName + "</option>\r\n";
                    }
                    else
                    {
                        SelectCategoryRoomText +=
                            "   <option value=\"" + ServiceTypeCategories[iIndex].ServiceTypeCategoryId + "\">" + ServiceTypeCategories[iIndex].ServiceTypeCategoryName + "</option>\r\n";
                    }
                }
                SelectCategoryRoomText +=
                    "</select>\r\n";

               Html =
                           " <div class=\"ibox-content\"> \r\n" +
                           "     <div class=\"row\"> \r\n" +
                           "         <div class=\"col-sm-12\"> \r\n" +
                           "             <div> \r\n" +
                           "                <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Ngày khởi hành") + "</label> <input id=\"txtStartDate\" type=\"text\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Ngày khởi hành") + "\" class=\"form-control CssDate\" value=\"" + OQuotation.StartDate.ToString("dd/MM/yyyy") + "\"></div> \r\n" +
                           "                <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số phòng") + "</label> <input class=\"form-control touchspin3\" type=\"text\" value=\"" + OQuotation.NoOfRoom.ToString("#,##0") + "\" id=\"txtNoOfRoom\"></div> \r\n" +
                           "                <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số khách") + "</label> <table><tr><td>" + WebLanguage.GetLanguage(OSiteParam, "Người lớn") + "<td><td>" + WebLanguage.GetLanguage(OSiteParam, "Trẻ em") + "</td></tr>   <tr><td><input class=\"form-control touchspin3\" type=\"text\" value=\"" + OQuotation.Adults.ToString("#,##0") + "\" id=\"txtAdults\"><td><td><input class=\"form-control touchspin3\" type=\"text\" value=\"" + OQuotation.Children.ToString("#,##0") + "\" id=\"txtChildren\"></td></tr> </table></div> \r\n" +
                           "                <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Loại khách sạn") + "</label> " + SelectCategoryRoomText + "</div> \r\n" +

                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên khách") + "</label> <input id=\"txtCustomerName\" value=\"" + OQuotation.CustomerName + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên khách hàng") + "\" class=\"form-control\"></div> \r\n" +
                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "</label> <input id=\"txtCustomerAddress\" value=\"" + OQuotation.CustomerAddress + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ khách hàng") + "\" class=\"form-control\"></div> \r\n" +
                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điện thoại") + "</label> <input id=\"txtCustomerMobile\" value=\"" + OQuotation.CustomerMobile + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điện thoại khách hàng") + "\" class=\"form-control\"></div> \r\n" +
                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Email") + "</label> <input id=\"txtCustomerEmail\" value=\"" + OQuotation.CustomerEmail + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Email khách hàng") + "\" class=\"form-control\"></div> \r\n" +

                           "                 <div> \r\n" +
                           "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdateCustomerInfo('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật") + "</strong></button> \r\n" +
                           "                 </div> \r\n" +
                           "             </div> \r\n" +
                           "         </div> \r\n" +
                           "     </div> \r\n" +
                           " </div> \r\n";

                    Html = WebEnvironments.EncryptHtml(Html);
                

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
        public static AjaxOut ServerSideDrawExtraInfo(RenderInfoCls ORenderInfo, string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                QuotationCls
                    OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);

                Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\">\r\n" +
                    "             <div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề") + "</label> <input id=\"txtQuotationSubject\" class=\"form-control\" value=\"" + OQuotation.QuotationSubject + "\" ></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đầu trang") + "</label> <textarea id=\"txtHeader\">" + OQuotation.Header + "</textarea></div> \r\n" +
                    
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Bao gồm") + "</label> <textarea id=\"txtInclude\">"+OQuotation.Include+"</textarea></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Không bao gồm") + "</label> <textarea id=\"txtExclude\">" + OQuotation.Exclude + "</textarea></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt")+ "</label> <textarea id=\"txtSpecialCondition\">" + OQuotation.SpecialCondition + "</textarea></div> \r\n" +

                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Cuối trang") + "</label> <textarea id=\"txtFooter\">" + OQuotation.Footer + "</textarea></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Css") + "</label> <textarea class=\"form-control\" id=\"txtCss\">" + OQuotation.Css + "</textarea></div> \r\n" +
                    "                 <div> \r\n" +
                    "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdateExtraInfo('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật") + "</strong></button> \r\n" +
                    "                 </div> \r\n" +
                    "             </div> \r\n" +
                    "         </div> \r\n" +
                    "     </div> \r\n" +
                    " </div> \r\n";

                Html = WebEnvironments.EncryptHtml(Html);


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
        public static AjaxOut ServerSideSaveCustomerInfo(
            RenderInfoCls ORenderInfo, 
            string QuotationId,
            string StartDate, 
            string Adults, 
            string Children, 
            string NoOfRoom,
            string HotelCategoryId,
            string CustomerName,
            string CustomerAddress,
            string CustomerMobile,
            string CustomerEmail)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);


                if (FunctionUtility.checkVnDate(StartDate) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ngày bắt đầu không hợp lệ"));
                if (FunctionUtility.checkInteger(Adults) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số người lớn không hợp lệ"));
                if (FunctionUtility.checkInteger(Children) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số trẻ em không hợp lệ"));
                if (FunctionUtility.checkInteger(NoOfRoom) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số phòng không hợp lệ"));


                QuotationCls
                    OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);

                OQuotation.StartDate = FunctionUtility.VNDateToDate(StartDate);
                OQuotation.NoOfDays = 1;
                OQuotation.NoOfRoom = int.Parse(NoOfRoom);
                OQuotation.Adults = int.Parse(Adults);
                OQuotation.Children = int.Parse(Children);
                OQuotation.frkHotelCategoryId = HotelCategoryId;

                OQuotation.CustomerName = CustomerName;
                OQuotation.CustomerMobile = CustomerMobile;
                OQuotation.CustomerAddress = CustomerAddress;
                OQuotation.CustomerEmail = CustomerEmail;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().Save(ORenderInfo, QuotationId, OQuotation);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Cập nhật thông tin thành công");
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
        public static AjaxOut ServerSideSaveExtraInfo(
            RenderInfoCls ORenderInfo,
            string QuotationId,
            string QuotationSubject, 
            string Header, 
            string Footer,
            string Include,
            string Exclude,
            string SpecialCondition,
            string Css)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                QuotationCls
                    OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);
                OQuotation.QuotationSubject = QuotationSubject;
                OQuotation.Header = Header;
                OQuotation.Footer = Footer;
                OQuotation.Include = Include;
                OQuotation.Exclude = Exclude;
                OQuotation.SpecialCondition = SpecialCondition;
                OQuotation.Css = Css;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().Save(ORenderInfo, QuotationId, OQuotation);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Cập nhật thông tin thành công");
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
        public static AjaxOut ServerSideDeleteQuotationItem(
            RenderInfoCls ORenderInfo,
            string QuotationId,
            string QuotationItemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().DeleteQuotationItem(ORenderInfo, QuotationItemId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationStepThree().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("Id",QuotationId),
                });
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
        public static AjaxOut ServerSideAddQuotationItem(
            RenderInfoCls ORenderInfo,
            string QuotationId, 
            string LocationServiceId, 
            string HotelCategoryId, 
            int Night)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                if (string.IsNullOrEmpty(LocationServiceId)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa xác định điểm đến"));
                //if (string.IsNullOrEmpty(HotelCategoryId)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa xác định loại hình dịch vụ khác sạn"));

                QuotationItemCls
                    OQuotationItem = new QuotationItemCls();
                OQuotationItem.QuotationItemId = System.Guid.NewGuid().ToString();
                OQuotationItem.Night = Night;
                OQuotationItem.frkLocationServiceId = LocationServiceId;
                OQuotationItem.frkHotelCategoryId = HotelCategoryId;
                OQuotationItem.frkQuotationId = QuotationId;

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().AddQuotationItem(ORenderInfo, QuotationId, OQuotationItem);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationStepThree().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("Id",QuotationId),
                });
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
        public static AjaxOut ServerSideDrawAddQuotationItemForm(
            RenderInfoCls ORenderInfo,
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html="";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);


                ServiceTypeCategoryFilterCls
                    OServiceTypeCategoryFilter = new ServiceTypeCategoryFilterCls();
                OServiceTypeCategoryFilter.ActiveOnly = 1;

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ServiceTypeCategoryCls[]
                    ServiceTypeCategories = CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeCategoryProcess().Reading(ORenderInfo, OServiceTypeCategoryFilter);

                string SelectCategoryRoomText =
                    "<select id=\"drpSelectServiceTypeCategory\" class=\"form-control\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n";
                for (int iIndex = 0; iIndex < ServiceTypeCategories.Length; iIndex++)
                {
                    SelectCategoryRoomText +=
                        "   <option value=\"" + ServiceTypeCategories[iIndex].ServiceTypeCategoryId + "\">" + ServiceTypeCategories[iIndex].ServiceTypeCategoryName + "</option>\r\n";
                }
                SelectCategoryRoomText +=
                    "</select>\r\n";


                CityFilterCls
                  OCityFilter = new CityFilterCls();
                OCityFilter.ActiveOnly = 1;
                CityCls[]
                    Cities = CallBussinessUtility.CreateBussinessProcess().CreateCityProcess().Reading(ORenderInfo, OCityFilter);


                string SelectCityText =
                        "<select id=\"drpSelectLocationService\" class=\"form-control select2 SelectList\">\r\n" +
                        "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn địa điểm") + "</option>\r\n";
                for (int iCat = 0; iCat < Cities.Length; iCat++)
                {
                    SelectCityText +=
                        "   <option value=\"" + Cities[iCat].CityId + "\">" + Cities[iCat].CityName + "</option>\r\n";
                }
                SelectCityText +=
                    "</select>\r\n";

                Html =
                    "               <h3 style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm hành trình") + "</h3>\r\n" +
                    "                <div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa điểm") + "</label> "+SelectCityText+"</div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số đêm") + "</label> <input id=\"txtNight\" type=\"text\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Số đêm") + "\" value=\"1\" class=\"form-control\"></div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Loại khách sạn") + "</label> " + SelectCategoryRoomText + "</div> \r\n" +
                    "                    <div> \r\n" +
                    "                        <button onclick=\"javascript:CallAddQuotationItemAction('"+QuotationId+"');\" class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "</strong></button> \r\n" +
                    "                        <button onclick=\"javascript:CallBackFromLookUpForm();\" style=\"margin-left:5px\" class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button> \r\n" +
                    "                    </div> \r\n" +
                    "                </div> \r\n";

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
        public static AjaxOut ServerSideDrawQuotationItem(
            RenderInfoCls ORenderInfo,
            string QuotationItemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationItemCls
                    OQuotationItem = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationItemModel(ORenderInfo, QuotationItemId);

                QuotationItemItineraryCls[]
                    QuotationItemItineraries = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingQuotationItemItineraries(ORenderInfo, QuotationItemId);
                if (QuotationItemItineraries.Length > 0)
                {
                    Html +=
                        "<div style=\"height:30px;background-color:whitesmoke;font-size:10px\"><table><tr><td style=\"width:20px\"><img style=\"height:16px\" src=\"images/icon-itinerary.png\" /></td><td><h5 style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + WebLanguage.GetLanguage(OSiteParam, "Hành trình") + "</h5></td></tr></table></div>\r\n" +
                        "   <ul>\r\n";
                    for (int iIndex = 0; iIndex < QuotationItemItineraries.Length; iIndex++)
                    {
                        Html += "<li style=\"list-style-type:none;padding:1px\"><a href=\"javascript:CallPreviewQuotationItinerary('" + QuotationItemItineraries[iIndex].QuotationItemItineraryId + "');\"><img style=\"height:16px\" src=\"images/marker.png\"/></a> <a href=\"javascript:RemoveItinerary('" + QuotationItemId + "','" + QuotationItemItineraries[iIndex].QuotationItemItineraryId + "');\"><img style=\"height:16px\" src=\"/images/trash.png\"></a> <a href=\"javascript:CallPreviewQuotationItinerary('" + QuotationItemItineraries[iIndex].QuotationItemItineraryId + "');\">" + QuotationItemItineraries[iIndex].Subject + "</a></li>\r\n";
                    }
                    Html += "</ul>\r\n";
                }

                QuotationSectionServiceCls[]
                   QuotationSectionServices = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingQuotationSectionServices(ORenderInfo, QuotationItemId);
                if (QuotationSectionServices.Length > 0)
                {
                    for (int iIndex = 0; iIndex < QuotationSectionServices.Length; iIndex++)
                    {
                        Html +=
                            "<div style=\"height:25px;font-size:10px; background-color:whitesmoke;margin-bottom:2px\"><table><tr><td style=\"width:20px\"><img style=\"height:16px\" src=\"images/icon-" + QuotationSectionServices[iIndex].frkServiceTypeId + ".png\" /></td><td style=\"width:350px\"><h4 style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + QuotationSectionServices[iIndex].ServiceTypeName + "</h4></td> <td><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm chi tiết dịch vụ") + "\" href=\"javascript:CallLookupServiceForm('" + QuotationItemId + "','" + QuotationSectionServices[iIndex].QuotationSectionServiceId + "');\"><img src=\"images/plus.png\" style=\"padding-left:5px;height:16px\"/></a></td> <td><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa nhóm dịch vụ") + "\" href=\"javascript:RemoveQuotationSessionService('" + QuotationItemId + "','" + QuotationSectionServices[iIndex].QuotationSectionServiceId + "');\"><img src=\"images/remove.png\" style=\"padding-left:5px;height:16px\"/></a></td></tr></table></div>\r\n" +
                            "<div id=\"divListQuotationSectionService" + QuotationSectionServices[iIndex].QuotationSectionServiceId + "\">\r\n" +
                                ServerSideDrawQuotationService(ORenderInfo, QuotationSectionServices[iIndex].QuotationSectionServiceId).HtmlContent +
                            "</div>\r\n";
                        
                    }

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
        public static AjaxOut ServerSideDeleteQuotationService(
            RenderInfoCls ORenderInfo, 
            string QuotationId,
            string QuotationSectionServiceId, 
            string QuotationServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().DeleteQuotationService(ORenderInfo, QuotationServiceId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.HtmlContent = ServerSideDrawQuotationService(ORenderInfo, QuotationSectionServiceId).HtmlContent;
                RetAjaxOut.RetExtraParam1 = ServerSideDrawMoneyStaticInfo(ORenderInfo, QuotationId).HtmlContent;
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
        public static AjaxOut ServerSideSaveUpdateQuantity(
            RenderInfoCls ORenderInfo, 
            string QuotationId,
            string QuotationServiceId,
            string Quantity)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                Quantity = Quantity.Replace(",", "");
                if (FunctionUtility.checkDecimal(Quantity) == false)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số lượng không hợp lệ"));
                }

                QuotationServiceCls
                    OQuotationService = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationServiceModel(ORenderInfo, QuotationServiceId);
                OQuotationService.Quantity = decimal.Parse(Quantity);
                OQuotationService.SubTotal = OQuotationService.CaculateSubTotal;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().SaveQuotationService(ORenderInfo, QuotationServiceId, OQuotationService);

                //RetAjaxOut.HtmlContent = ServerSideDrawQuotationService(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.InfoMessage = OQuotationService.SubTotal.ToString("#,##0");
                RetAjaxOut.RetExtraParam1 = decimal.Parse(Quantity).ToString("#,##0");
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
        public static AjaxOut ServerSideSaveUpdateNoOfServiceDays(
            RenderInfoCls ORenderInfo,
            string QuotationId,
            string QuotationServiceId,
            string NoOfServiceDays)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                NoOfServiceDays = NoOfServiceDays.Replace(",", "");
                if (FunctionUtility.checkDecimal(NoOfServiceDays) == false)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số lượng không hợp lệ"));
                }

                QuotationServiceCls
                    OQuotationService = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationServiceModel(ORenderInfo, QuotationServiceId);
                OQuotationService.NoOfServiceDays = int.Parse(NoOfServiceDays);
                OQuotationService.SubTotal = OQuotationService.CaculateSubTotal;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().SaveQuotationService(ORenderInfo, QuotationServiceId, OQuotationService);

                //RetAjaxOut.HtmlContent = ServerSideDrawQuotationService(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.InfoMessage = OQuotationService.SubTotal.ToString("#,##0");
                RetAjaxOut.RetExtraParam1 = decimal.Parse(NoOfServiceDays).ToString("#,##0");
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
        public static AjaxOut ServerSideSaveUpdateStartDate(
            RenderInfoCls ORenderInfo,
            string QuotationId,
            string QuotationServiceId,
            string StartDate)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                if (string.IsNullOrEmpty(StartDate))
                {
                    return RetAjaxOut;
                }
                if (FunctionUtility.checkVnDate(StartDate) == false)
                {
                    //throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ngày bắt đầu không hợp lệ"));
                    return RetAjaxOut;
                }

                QuotationServiceCls
                    OQuotationService = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationServiceModel(ORenderInfo, QuotationServiceId);
                OQuotationService.StartDate = FunctionUtility.VNDateToDate(StartDate);
                OQuotationService.SubTotal = OQuotationService.CaculateSubTotal;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().SaveQuotationService(ORenderInfo, QuotationServiceId, OQuotationService);

                //RetAjaxOut.HtmlContent = ServerSideDrawQuotationService(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.InfoMessage = OQuotationService.SubTotal.ToString("#,##0");
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
        public static AjaxOut ServerSideSaveUpdateStartTimeItem(
            RenderInfoCls ORenderInfo,
            string QuotationId,
            string QuotationServiceId,
            string StartTime)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                
                QuotationServiceCls
                    OQuotationService = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationServiceModel(ORenderInfo, QuotationServiceId);
                OQuotationService.StartTime = StartTime;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().SaveQuotationService(ORenderInfo, QuotationServiceId, OQuotationService);

                //RetAjaxOut.HtmlContent = ServerSideDrawQuotationService(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.InfoMessage = OQuotationService.SubTotal.ToString("#,##0");
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
        public static AjaxOut ServerSideSaveUpdateEndTimeItem(
           RenderInfoCls ORenderInfo,
           string QuotationId,
           string QuotationServiceId,
           string EndTime)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;

                
                QuotationServiceCls
                    OQuotationService = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationServiceModel(ORenderInfo, QuotationServiceId);
                OQuotationService.EndTime = EndTime;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().SaveQuotationService(ORenderInfo, QuotationServiceId, OQuotationService);

                //RetAjaxOut.HtmlContent = ServerSideDrawQuotationService(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.InfoMessage = OQuotationService.SubTotal.ToString("#,##0");
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
        public static AjaxOut ServerSideSaveUpdatePrice(
            RenderInfoCls ORenderInfo,
            string QuotationId,
            string QuotationServiceId,
            string Price)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                Price = Price.Replace(",", "");
                if (FunctionUtility.checkDecimal(Price) == false)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số lượng không hợp lệ"));
                }

                QuotationServiceCls
                    OQuotationService = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationServiceModel(ORenderInfo, QuotationServiceId);
                OQuotationService.Price = decimal.Parse(Price);
                OQuotationService.SubTotal = OQuotationService.CaculateSubTotal;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().SaveQuotationService(ORenderInfo, QuotationServiceId, OQuotationService);

                //RetAjaxOut.HtmlContent = ServerSideDrawQuotationService(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.InfoMessage = OQuotationService.SubTotal.ToString("#,##0");
                RetAjaxOut.RetExtraParam1 = decimal.Parse(Price).ToString("#,##0");
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
        public static AjaxOut ServerSideSaveUpdateExtraFee(
            RenderInfoCls ORenderInfo,
            string QuotationId,
            string QuotationServiceId,
            string ExtraFee)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ExtraFee = ExtraFee.Replace(",", "");
                if (FunctionUtility.checkDecimal(ExtraFee) == false)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số lượng không hợp lệ"));
                }

                QuotationServiceCls
                    OQuotationService = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationServiceModel(ORenderInfo, QuotationServiceId);
                OQuotationService.ExtraFee = decimal.Parse(ExtraFee);
                OQuotationService.SubTotal = OQuotationService.CaculateSubTotal;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().SaveQuotationService(ORenderInfo, QuotationServiceId, OQuotationService);

                //RetAjaxOut.HtmlContent = ServerSideDrawQuotationService(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, QuotationId).HtmlContent;
                RetAjaxOut.InfoMessage = OQuotationService.SubTotal.ToString("#,##0");
                RetAjaxOut.RetExtraParam1 = decimal.Parse(ExtraFee).ToString("#,##0");
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
        public static AjaxOut ServerSideSaveQuotationItemIndex(
            RenderInfoCls ORenderInfo,
            string QuotationItemId,
            string SortIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                SortIndex = SortIndex.Replace(",", "");
                if (FunctionUtility.checkInteger(SortIndex) == false)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số thứ tự không hợp lệ"));
                }

                QuotationItemCls
                    OQuotationItem = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationItemModel(ORenderInfo, QuotationItemId);
                OQuotationItem.DayIndex = int.Parse(SortIndex);

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().SaveQuotationItem(ORenderInfo, QuotationItemId, OQuotationItem);
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
        public static AjaxOut ServerSideDrawSortForm(
            RenderInfoCls ORenderInfo,
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationItemCls[]
                    QuotationItems = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingQuotationItems(ORenderInfo, QuotationId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string Url = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationStepThree().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("id",QuotationId)
                });
                Html =
                        "         <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:window.open('"+Url+"','_self');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button> \r\n" +
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Điểm đến") + " </th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < QuotationItems.Length; iIndex++)
                {
                    Html +=
                        "                 <tr id=\"tr" + QuotationItems[iIndex].QuotationItemId + "\"> \r\n" +
                        "                     <td class=\"td-center\" style=\"width:40px\"><input onblur=\"javascript:SaveUpdateQuotationIndexItem('" + QuotationItems[iIndex].QuotationItemId + "');\" class=\"form-control\" style=\"text-align:right; width:40px;background-color:lightyellow\" id=\"txtSortIndexQuotationItem" + QuotationItems[iIndex].QuotationItemId + "\" value=\"" + QuotationItems[iIndex].DayIndex.ToString("#,##0") + "\"></td> \r\n" +
                        "                     <td style=\"width:180px\">" + QuotationItems[iIndex].LocationServiceName + "</td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "       </div>\r\n";


                Html = WebEnvironments.EncryptHtml(Html);
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
        public static AjaxOut ServerSideDrawAddMakeTemplateForm(
            RenderInfoCls ORenderInfo,
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
               
                Html =
                       " <div class=\"ibox-content\"> \r\n" +
                       "     <div class=\"row\"> \r\n" +
                       "         <div class=\"col-sm-12\"> \r\n" +
                       "             <div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên mẫu") + "</label> <input id=\"txtQuotationTemplateName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên mẫu") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionSaveTemplate('"+QuotationId+"');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBackFromLookUpForm();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                       "                 </div> \r\n" +
                       "             </div> \r\n" +
                       "         </div> \r\n" +
                       "     </div> \r\n" +
                       " </div> \r\n";


                Html = WebEnvironments.EncryptHtml(Html);
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
        public static AjaxOut ServerSideDrawAddMakeBooking(
            RenderInfoCls ORenderInfo,
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationCls
                    OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);

                Html =
                       " <div class=\"ibox-content\"> \r\n" +
                       "     <div class=\"row\"> \r\n" +
                       "         <div class=\"col-sm-12\"> \r\n" +
                       "             <div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên mẫu") + "</label> <input id=\"txtBookingSubject\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên booking") + "\" class=\"form-control\" value=\""+ OQuotation.QuotationSubject + "\"></div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionCreateBooking('" + QuotationId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBackFromLookUpForm();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                       "                 </div> \r\n" +
                       "             </div> \r\n" +
                       "         </div> \r\n" +
                       "     </div> \r\n" +
                       " </div> \r\n";


                Html = WebEnvironments.EncryptHtml(Html);
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
        public static AjaxOut ServerSideDrawQuotationService(
            RenderInfoCls ORenderInfo, 
            string QuotationServiceSectionId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationServiceCls[]
                    QuotationServices = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingQuotationServices(ORenderInfo, QuotationServiceSectionId);
                QuotationSectionServiceCls
                    OQuotationSectionService = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationSectionServiceModel(ORenderInfo, QuotationServiceSectionId);
                QuotationItemCls
                    QuotationItem = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationItemModel(ORenderInfo, OQuotationSectionService.frkQuotationItemId);
                string QuotationId = QuotationItem.frkQuotationId;

                if (OQuotationSectionService.CalculateType.ToLower().Equals("pax"))
                {
                    Html =
                            "         <div class=\"table-responsive\"> \r\n" +
                            "             <table class=\"table table-striped\"> \r\n" +
                            "                 <thead> \r\n" +
                            "                 <tr> \r\n" +
                            "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "D.vụ") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Sl") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Giá") + " </th> \r\n" +
                            //"                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "P.Phí") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + " </th> \r\n" +
                            "                     <th style=\"width:100px\"></th> \r\n" +
                            "                 </tr> \r\n" +
                            "                 </thead> \r\n" +
                            "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < QuotationServices.Length; iIndex++)
                    {
                        Html +=
                            "                 <tr id=\"tr" + QuotationServices[iIndex].QuotationServiceId + "\"> \r\n" +
                            "                     <td style=\"width:180px\">" + QuotationServices[iIndex].ServiceName + "</td> \r\n" +
                            "                     <td style=\"width:40px\"><input class=\"form-control\" style=\"text-align:right;width:40px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateQuantityItem('" + QuotationServices[iIndex].QuotationServiceId + "');\" id=\"txtQuantity" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].Quantity.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:120px;background-color:lightyellow\" onblur=\"javascript:SaveUpdatePriceItem('" + QuotationServices[iIndex].QuotationServiceId + "');\" id=\"txtPrice" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].Price.ToString("#,##0") + "\"></td> \r\n" +
                            //"                     <td style=\"width:100px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateExtraFeeItem('" + QuotationServices[iIndex].QuotationServiceId + "');\" id=\"txtExtraFee" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].ExtraFee.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:120px;background-color:lightyellow\" id=\"txtSubTotal" + QuotationServices[iIndex].QuotationServiceId + "\" READONLY value=\"" + QuotationServices[iIndex].SubTotal.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:20px\" class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "\" href=\"javascript:CallActionDeleteQuotationService('" + QuotationId+"','"+QuotationServices[iIndex].frkQuotationSectionServiceId + "','" + QuotationServices[iIndex].QuotationServiceId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n";
                }
                if (OQuotationSectionService.CalculateType.ToLower().Equals("date"))
                {
                    Html =
                            "         <div class=\"table-responsive\"> \r\n" +
                            "             <table class=\"table table-striped\"> \r\n" +
                            "                 <thead> \r\n" +
                            "                 <tr> \r\n" +
                            "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "D.vụ") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "B.đầu") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Đêm") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Sl") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Giá") + " </th> \r\n" +
                            //"                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "P.Phí") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + " </th> \r\n" +
                            "                     <th style=\"width:100px\"></th> \r\n" +
                            "                 </tr> \r\n" +
                            "                 </thead> \r\n" +
                            "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < QuotationServices.Length; iIndex++)
                    {
                        string StartDateText = "";
                        if (QuotationServices[iIndex].StartDate.Year > 1)
                        {
                            StartDateText = QuotationServices[iIndex].StartDate.ToString("dd/MM/yyyy");
                        }
                        Html +=
                            "                 <tr id=\"tr" + QuotationServices[iIndex].QuotationServiceId + "\"> \r\n" +
                            "                     <td >" + QuotationServices[iIndex].ServiceName + "</td> \r\n" +
                            "                     <td style=\"width:70px\"><input style=\"text-align:center\" onblur=\"javascript:SaveUpdateStartDateItem('" + QuotationServices[iIndex].QuotationServiceId + "');\"  class=\"form-control CssDate\" id=\"txtStartDateItem" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + StartDateText + "\"></td> \r\n" +
                            "                     <td style=\"width:40px\"><input class=\"form-control\" style=\"text-align:right;width:40px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateNoOfServiceDayssItem('" + QuotationServices[iIndex].QuotationServiceId + "');\" id=\"txtNoOfServiceDays" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].NoOfServiceDays.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:30px\"><input class=\"form-control\" style=\"text-align:right;width:50px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateQuantityItem('" + QuotationServices[iIndex].QuotationServiceId + "');\" id=\"txtQuantity" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].Quantity.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" onblur=\"javascript:SaveUpdatePriceItem('" + QuotationServices[iIndex].QuotationServiceId + "');\" id=\"txtPrice" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].Price.ToString("#,##0") + "\"></td> \r\n" +
                            //"                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateExtraFeeItem('" + QuotationServices[iIndex].QuotationServiceId + "');\" id=\"txtExtraFee" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].ExtraFee.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" id=\"txtSubTotal" + QuotationServices[iIndex].QuotationServiceId + "\" READONLY value=\"" + QuotationServices[iIndex].SubTotal.ToString("#,##0") + "\"></td> \r\n" +

                            "                     <td style=\"width:20px\" class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "\" href=\"javascript:CallActionDeleteQuotationService('" + QuotationId+"','"+QuotationServices[iIndex].frkQuotationSectionServiceId + "','" + QuotationServices[iIndex].QuotationServiceId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n";
                }


                if (OQuotationSectionService.CalculateType.ToLower().Equals("time"))
                {
                    Html =
                            "         <div class=\"table-responsive\"> \r\n" +
                            "             <table class=\"table table-striped\"> \r\n" +
                            "                 <thead> \r\n" +
                            "                 <tr> \r\n" +
                            "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "D.vụ") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "B.Đầu") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "K.Thúc") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Sl") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Giá") + " </th> \r\n" +
                            //"                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "P.Phí") + " </th> \r\n" +
                            "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Tổng") + " </th> \r\n" +
                            "                     <th style=\"width:100px\"></th> \r\n" +
                            "                 </tr> \r\n" +
                            "                 </thead> \r\n" +
                            "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < QuotationServices.Length; iIndex++)
                    {
                        Html +=
                            "                 <tr id=\"tr" + QuotationServices[iIndex].QuotationServiceId + "\"> \r\n" +
                            "                     <td >" + QuotationServices[iIndex].ServiceName + "</td> \r\n" +
                            "                     <td style=\"width:60px\"><input style=\"text-align:center\" onblur=\"javascript:SaveUpdateStartTimeItem('" + QuotationServices[iIndex].QuotationServiceId + "');\"  class=\"form-control\" id=\"txtStartTimeItem" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].StartTime + "\"></td> \r\n" +
                            "                     <td style=\"width:60px\"><input style=\"text-align:center\" onblur=\"javascript:SaveUpEndTimeItem('" + QuotationServices[iIndex].QuotationServiceId + "');\"  class=\"form-control\" id=\"txtEndTimeItem" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].EndTime + "\"></td> \r\n" +
                            "                     <td style=\"width:30px\"><input class=\"form-control\" style=\"text-align:right;width:30px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateQuantityItem('" + QuotationServices[iIndex].QuotationServiceId + "');\" id=\"txtQuantity" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].Quantity.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" onblur=\"javascript:SaveUpdatePriceItem('" + QuotationServices[iIndex].QuotationServiceId + "');\" id=\"txtPrice" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].Price.ToString("#,##0") + "\"></td> \r\n" +
                            //"                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateExtraFeeItem('" + QuotationServices[iIndex].QuotationServiceId + "');\" id=\"txtExtraFee" + QuotationServices[iIndex].QuotationServiceId + "\" value=\"" + QuotationServices[iIndex].ExtraFee.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" id=\"txtSubTotal" + QuotationServices[iIndex].QuotationServiceId + "\" READONLY value=\"" + QuotationServices[iIndex].SubTotal.ToString("#,##0") + "\"></td> \r\n" +

                            "                     <td style=\"width:20px\" class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "\" href=\"javascript:CallActionDeleteQuotationService('" + QuotationId + "','" + QuotationServices[iIndex].frkQuotationSectionServiceId + "','" + QuotationServices[iIndex].QuotationServiceId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n";
                }

                Html = WebEnvironments.EncryptHtml(Html);
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
        public static AjaxOut ServerSideDrawMoneyStaticInfo(
            RenderInfoCls ORenderInfo,
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                ServiceTypeCls[]
                    ServiceTypes = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingMoneyStatiticsInfo(ORenderInfo, QuotationId);

                Html =
                           "         <div class=\"table-responsive\"> \r\n" +
                           "             <table class=\"table table-striped\"> \r\n" +
                           "                 <thead> \r\n" +
                           "                 <tr> \r\n" +
                           "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "D.vụ") + " </th> \r\n" +
                           "                     <th style=\"text-align:right\">" + WebLanguage.GetLanguage(OSiteParam, "Tiền") + " </th> \r\n" +
                           "                 </tr> \r\n" +
                           "                 </thead> \r\n" +
                           "                 <tbody> \r\n";
                decimal GrandTotal = 0;
                for (int iIndex = 0; iIndex < ServiceTypes.Length; iIndex++)
                {
                    GrandTotal = GrandTotal + ServiceTypes[iIndex].GrandTotal;
                    Html +=
                        "                 <tr id=\"tr" + ServiceTypes[iIndex].ServiceTypeId + "\"> \r\n" +
                        "                     <td style=\"width:180px\">" + ServiceTypes[iIndex].ServiceTypeName + "</td> \r\n" +
                        "                     <td style=\"text-align:right\">" + ServiceTypes[iIndex].GrandTotal.ToString("#,##0")+"</td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                        "                 <tr style=\"color:#337ab7;font-weight:bold;padding:1px !important;font-size:18px\"> \r\n" +
                        "                     <td style=\"width:180px\">" + WebLanguage.GetLanguage(OSiteParam, "Tổng dịch vụ") +"</td> \r\n" +
                        "                     <td style=\"text-align:right\">" + GrandTotal.ToString("#,##0") + "</td> \r\n" +
                        "                 </tr> \r\n";
                Html +=

                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "       </div>\r\n";

                Html = WebEnvironments.EncryptHtml(Html);
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
        public static AjaxOut ServerSideCallPreview(
            RenderInfoCls ORenderInfo,
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                RetAjaxOut.RetUrl = "preview.aspx?sv=quotation&id=" + QuotationId;
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
        public static AjaxOut ServerSideCaculateMoney(
            RenderInfoCls ORenderInfo,
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CaculatePrice(ORenderInfo, QuotationId);
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationStepThree().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Id",QuotationId)
                    });
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
        public static AjaxOut ServerSideCallExportPdf(
            RenderInfoCls ORenderInfo,
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                RetAjaxOut = QuotationPreviewUtility.Preview(ORenderInfo,QuotationId,"pdf");
                if (RetAjaxOut.Error) throw new Exception(RetAjaxOut.InfoMessage);
                byte[] Bytes = FunctionUtility.GeneratePdfFromFile(RetAjaxOut.RetUrl);
                MediaInfoCls
                    OMediaInfo = new MediaInfoCls();
                OMediaInfo.LoginName = "system";
                OMediaInfo.MediaInfoId = QuotationId;
                OMediaInfo.Month = System.DateTime.Now.Month;
                OMediaInfo.Year = System.DateTime.Now.Year;
                OMediaInfo.Overwrite = true;
                OMediaInfo.Section = "quotation";
                OMediaInfo.SiteId = ORenderInfo.SiteId;
                OMediaInfo.UploadFileName = QuotationId + ".pdf";

                RetAjaxOut = CallTempServiceUtility.UploadTemp(OMediaInfo, Bytes);
                RetAjaxOut.HtmlContent = WebLanguage.GetLanguage(OSiteParam,"Hoàn thành xuất Pdf")+" -> "+"<a target=\"_blank\" href=\"" + RetAjaxOut.RetUrl + "\">" + WebLanguage.GetLanguage(OSiteParam, "Mở pdf") + "</a>\r\n";
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
        public static AjaxOut ServerSideCallCloseQuotation(
            RenderInfoCls ORenderInfo,
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                QuotationCls
                    OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);
                OQuotation.frkQuotationStatusId = "closed";
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().Save(ORenderInfo, QuotationId, OQuotation);
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationStepThree().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Id",QuotationId)
                    });
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
        public static AjaxOut ServerSideCallPendingQuotation(
            RenderInfoCls ORenderInfo,
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                QuotationCls
                    OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);
                OQuotation.frkQuotationStatusId = "pending";
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().Save(ORenderInfo, QuotationId, OQuotation);
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationStepThree().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Id",QuotationId)
                    });
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
        public static AjaxOut ServerSideCallReOpenQuotation(
           RenderInfoCls ORenderInfo,
           string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                QuotationCls
                    OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);
                OQuotation.frkQuotationStatusId = "open";
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().Save(ORenderInfo, QuotationId, OQuotation);
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationStepThree().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Id",QuotationId)
                    });
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
        public static AjaxOut ServerSideAddQuotationTemplate(
            RenderInfoCls ORenderInfo,
            string QuotationId,
            string Subject)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                if (string.IsNullOrEmpty(Subject)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tiêu đề mẫu chưa nhập"));
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().SaveQuotationTemplate(ORenderInfo, QuotationId, Subject);

                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Tạo mẫu thành công");
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
        public static AjaxOut ServerSideCreateBooking(
            RenderInfoCls ORenderInfo,
            string QuotationId,
            string Subject)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                if (string.IsNullOrEmpty(Subject)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tiêu đề booking chưa nhập"));
                QuotationCls
                    OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);
                CreateBookingFromQuotationParamCls
                    OCreateBookingFromQuotationParam = new CreateBookingFromQuotationParamCls();
                OCreateBookingFromQuotationParam.Adults = OQuotation.Adults;
                OCreateBookingFromQuotationParam.Children = OQuotation.Children;
                OCreateBookingFromQuotationParam.NoOfRoom = OQuotation.NoOfRoom;
                OCreateBookingFromQuotationParam.OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerId;
                OCreateBookingFromQuotationParam.OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OCreateBookingFromQuotationParam.StartDate = OQuotation.StartDate;
                

                string BookingId = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingFromQuotation(ORenderInfo, OCreateBookingFromQuotationParam, QuotationId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Tạo booking thành công");
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateBookingStepThree().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Id",BookingId)
                    });
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
