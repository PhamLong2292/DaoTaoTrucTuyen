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
    public class CreateBookingStepThree : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "CreateBookingStepThree";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Xây dựng booking - Chọn dịch vụ";
            }
        }

        public override string Description
        {
            get
            {
                return "Xây dựng booking- Chọn dịch vụ";
            }
        }


        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CreateBookingStepThree),Page);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(BookingItemItineraryProcess), Page);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(BookingServiceProcess), Page);
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
                return new string[] { new CreateBookingStepOne().WebPartId };
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


                string BookingId = (string)WebEnvironments.Request("Id");
                if (string.IsNullOrEmpty(BookingId))
                {
                    BookingId = "";
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

                    "<input id=\"txtBookingId\" type=\"hidden\" value=\"" + BookingId + "\">\r\n" +
                    "<script>\r\n" +

                    "   function CallActionSaveTemplate(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       Subject = document.getElementById('txtBookingTemplateName').value;\r\n"+
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideAddBookingTemplate(RenderInfo, BookingId, Subject).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "       CallBackFromLookUpForm();\r\n"+
                    "   }\r\n" +

                    "   function CallPreview(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideCallPreview(RenderInfo, BookingId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_blank');\r\n"+
                    "   }\r\n" +

                    "   function CallCaculateMoney(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideCaculateMoney(RenderInfo, BookingId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function CallMakeTemplate(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideDrawAddMakeTemplateForm(RenderInfo, BookingId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "   }\r\n" +


                    "   function CallExportPdf(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('divProcessingExport').innerHTML='"+WebLanguage.GetLanguage(OSiteParam,"Đang xuất dữ liệu")+"...';\r\n"+
                    "       setTimeout('RealCallExportPdf(\"'+BookingId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function RealCallExportPdf(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideCallExportPdf(RenderInfo, BookingId).value;\r\n" +
                    "       document.getElementById('divProcessingExport').innerHTML='';\r\n"+
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divProcessingExport').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "   }\r\n" +

                    "   function CallCloseBooking(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideCallCloseBooking(RenderInfo, BookingId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n" +

                    "   function CallPendingBooking(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideCallPendingBooking(RenderInfo, BookingId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function CallReOpenBooking(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideCallReOpenBooking(RenderInfo, BookingId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +


                    "   function SaveUpdateBookingIndexItem(BookingItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndexBookingItem'+BookingItemId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideSaveBookingItemIndex(RenderInfo, BookingItemId, SortIndex).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +



                    "   function SaveUpdateQuantityItem(BookingServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       BookingId= document.getElementById('txtBookingId').value;\r\n"+
                    "       Quantity = document.getElementById('txtQuantity'+BookingServiceId).value;\r\n"+
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideSaveUpdateQuantity(RenderInfo, BookingId, BookingServiceId, Quantity).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+BookingServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "       document.getElementById('txtQuantity'+BookingServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n"+

                    "   function SaveUpdateNoOfServiceDayssItem(BookingServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       BookingId= document.getElementById('txtBookingId').value;\r\n"+
                    "       NoOfServiceDays = document.getElementById('txtNoOfServiceDays'+BookingServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideSaveUpdateNoOfServiceDays(RenderInfo, BookingId, BookingServiceId, NoOfServiceDays).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+BookingServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "       document.getElementById('txtNoOfServiceDays'+BookingServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n"+

                    "   function SaveUpdateStartDateItem(BookingServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       BookingId= document.getElementById('txtBookingId').value;\r\n"+
                    "       StartDate = document.getElementById('txtStartDateItem'+BookingServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideSaveUpdateStartDate(RenderInfo, BookingId, BookingServiceId, StartDate).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+BookingServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "   }\r\n"+



                    "   function SaveUpdateStartTimeItem(BookingServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       BookingId= document.getElementById('txtBookingId').value;\r\n" +
                    "       StartTime = document.getElementById('txtStartTimeItem'+BookingServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideSaveUpdateStartTimeItem(RenderInfo, BookingId, BookingServiceId, StartTime).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+BookingServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +


                    "   function SaveUpEndTimeItem(BookingServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       BookingId= document.getElementById('txtBookingId').value;\r\n" +
                    "       EndTime = document.getElementById('txtEndTimeItem'+BookingServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideSaveUpdateEndTimeItem(RenderInfo, BookingId, BookingServiceId, EndTime).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+BookingServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +

                    "   function SaveUpdatePriceItem(BookingServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       BookingId= document.getElementById('txtBookingId').value;\r\n" +
                    "       Price = document.getElementById('txtPrice'+BookingServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideSaveUpdatePrice(RenderInfo, BookingId, BookingServiceId, Price).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+BookingServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('txtPrice'+BookingServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n" +

                    "   function SaveUpdateExtraFeeItem(BookingServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       BookingId= document.getElementById('txtBookingId').value;\r\n" +
                    "       ExtraFee = document.getElementById('txtExtraFee'+BookingServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideSaveUpdateExtraFee(RenderInfo, BookingId, BookingServiceId, ExtraFee).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+BookingServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('txtExtraFee'+BookingServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n" +


                    "   function AddBookingItemForm(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideDrawAddBookingItemForm(RenderInfo, BookingId).value;\r\n" +
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

                    "   function CallAddBookingItemAction(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       LocationServiceId = document.getElementById('drpSelectLocationService').value;\r\n"+
                    "       HotelCategoryId = document.getElementById('drpSelectServiceTypeCategory').value;\r\n"+
                    "       Night = parseInt(document.getElementById('txtNight').value,10);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideAddBookingItem(RenderInfo, BookingId, LocationServiceId, HotelCategoryId, Night).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n"+


                    "   function CallSortForm(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideDrawSortForm(RenderInfo, BookingId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "   }\r\n"+



                    
                    "   function CallAddSectionService(BookingItemId,ServiceTypeId)\r\n"+
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.BookingServiceProcess.ServerSideAddSectionService(RenderInfo, BookingItemId, ServiceTypeId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(document.getElementById('imgicon'+BookingItemId+ServiceTypeId)!=null)\r\n"+
                    "       {\r\n"+
                    "           document.getElementById('imgicon'+BookingItemId+ServiceTypeId).style='opacity:1';\r\n"+
                    "       }\r\n"+
                    "       document.getElementById('divBookingItemContent'+BookingItemId).innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "   }\r\n" +


                    "   function RemoveBookingSessionService(BookingItemId, BookingSectionServiceId)\r\n" +

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


                    "           AjaxOut = OneTSQ.WebParts.BookingServiceProcess.ServerSideRemoveSectionService(RenderInfo, BookingItemId, BookingSectionServiceId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           document.getElementById('divBookingItemContent'+BookingItemId).innerHTML=AjaxOut.HtmlContent;\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "Nhóm dịch vụ được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +


                    "   }\r\n" +

                    "   function CallActionDeleteBookingService(BookingId, BookingSectionServiceId, BookingServiceId)\r\n" +
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


                    "           AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideDeleteBookingService(RenderInfo, BookingId, BookingSectionServiceId, BookingServiceId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           document.getElementById('divListBookingSectionService'+BookingSectionServiceId).innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "           document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.RetExtraParam1;\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "Dịch vụ được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +


                    "   }\r\n" +


                    "   function DeleteBookingItem(BookingId, BookingItemId)\r\n" +

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


                    "           AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideDeleteBookingItem(RenderInfo, BookingId, BookingItemId).value;\r\n" +
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

                    

                    "   function AddPlusService(BookingItemId)\r\n" +
                    "   {\r\n"+
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.BookingServiceProcess.ServerSideDrawAddServiceToBookingItemForm(RenderInfo, BookingItemId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n"+
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n"+
                    "   }\r\n"+

                    "   function CallLookupItineraryForm(BookingItemId)\r\n" +
                    "   {\r\n"+
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.BookingItemItineraryProcess.ServerSideDrawSearchItinerary(RenderInfo, BookingItemId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n"+
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n"+
                    "   }\r\n"+

                    
                    "   function CallDoSearchItinerary(BookingItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ActiveBookingItemId = BookingItemId;\r\n"+
                    "       document.getElementById('txtCurrentPageIndexItinerary').value='0';\r\n" +
                    "       document.getElementById('divProcessingSearchItinerary').innerHTML='"+WebLanguage.GetLanguageProcessing(OSiteParam)+"';\r\n"+
                    "       setTimeout('RealCallDoSearchItinerary(\"'+BookingItemId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function NextPageSearchItinerary(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('txtCurrentPageIndexItinerary').value=PageIndex;\r\n"+
                    "       document.getElementById('divProcessingSearchItinerary').innerHTML='"+WebLanguage.GetLanguageProcessing(OSiteParam)+"';\r\n"+
                    "       setTimeout('RealCallDoSearchItinerary(\"'+ActiveBookingItemId+'\")',10);\r\n" +
                    "   }\r\n" +


                    "   function RealCallDoSearchItinerary(BookingItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       Keyword = document.getElementById('txtSearchKeywordItinerary').value;\r\n" +
                    "       CurrentPageIndex =  parseInt(document.getElementById('txtCurrentPageIndexItinerary').value,10);\r\n"+
                    "       AjaxOut = OneTSQ.WebParts.BookingItemItineraryProcess.ServerSideDrawSearchItineraryResult(RenderInfo, BookingItemId, Keyword, CurrentPageIndex).value;\r\n" +
                    "       document.getElementById('divProcessingSearchItinerary').innerHTML='';\r\n"+
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divSearchItineraryResultContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +


                    "   function CallPreviewBookingItinerary(BookingItemItineraryId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.BookingItemItineraryProcess.ServerSidePreviewBookingItemItinerary(RenderInfo, BookingItemItineraryId).value;\r\n" +
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
                    "       AjaxOut = OneTSQ.WebParts.BookingItemItineraryProcess.ServerSidePreviewItinerary(RenderInfo, ItineraryId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('ModalTitle').innerHTML=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       $('#divFormModal').modal();\r\n"+
                    "   }\r\n" +


                    "   function RemoveItinerary(BookingItemId, BookingItemItineraryId)\r\n" +
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


                    "           AjaxOut = OneTSQ.WebParts.BookingItemItineraryProcess.ServerSideRemoveItinerary(RenderInfo, BookingItemId, BookingItemItineraryId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           document.getElementById('divBookingItemContent'+BookingItemId).innerHTML=AjaxOut.HtmlContent;\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "Hành trình được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +

                    
                    "   }\r\n" +



                    "   function CallActionUpdateCustomerInfo(BookingId)\r\n" +
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

                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideSaveCustomerInfo(RenderInfo, BookingId, StartDate, Adults, Children, NoOfRoom, HotelCategoryId, CustomerName, CustomerAddress, CustomerMobile, CustomerEmail).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "   }\r\n"+

                    "   function CallActionUpdateExtraInfo(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       BookingSubject = document.getElementById('txtBookingSubject').value;\r\n"+
                    "       Css = document.getElementById('txtCss').value;\r\n" +
                    "       Header = tinyMCE.get('txtHeader').getContent();\r\n" +
                    "       Footer = tinyMCE.get('txtFooter').getContent();\r\n" +
                    "       Include = tinyMCE.get('txtInclude').getContent();\r\n" +
                    "       Exclude = tinyMCE.get('txtExclude').getContent();\r\n" +
                    "       SpecialCondition = tinyMCE.get('txtSpecialCondition').getContent();\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepThree.ServerSideSaveExtraInfo(RenderInfo, BookingId, BookingSubject, Header, Footer, Include, Exclude, SpecialCondition, Css).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "   }\r\n" +

                    "   function CallActionAddItineraryToBookingItem(BookingItemId, ItineraryId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.BookingItemItineraryProcess.ServerSideAddItineraryToBookingItem(RenderInfo, BookingItemId, ItineraryId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divBookingItemContent'+BookingItemId).innerHTML = AjaxOut.HtmlContent;\r\n"+
                    "   }\r\n" +

                    


                    "   ActiveBookingItemId=null;\r\n"+
                    "   ActiveBookingSectionServiceId=null;\r\n"+

                    "   function CallLookupServiceForm(BookingItemId, BookingSectionServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ActiveBookingItemId = BookingItemId;\r\n"+
                    "       ActiveBookingSectionServiceId = BookingSectionServiceId;\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.BookingServiceProcess.ServerSideDrawSearchService(RenderInfo, BookingItemId, BookingSectionServiceId).value;\r\n" +
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
                    //"       setTimeout('CallDoSearchService(\"'+BookingItemId+'\",\"'+BookingSectionServiceId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function NextPageSearchService(CurrentPageIndex)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('txtCurrentPageIndexService').value = CurrentPageIndex;\r\n"+
                    "       document.getElementById('divProcessingSearchService').innerHTML='" + WebLanguage.GetLanguageProcessing(OSiteParam) + "';\r\n" +
                    "       setTimeout('RealCallDoSearchService(\"'+ActiveBookingItemId+'\",\"'+ActiveBookingSectionServiceId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function CallDoSearchService(BookingItemId, BookingSectionServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('txtCurrentPageIndexService').value = '0';\r\n" +
                    "       document.getElementById('divProcessingSearchService').innerHTML='" + WebLanguage.GetLanguageProcessing(OSiteParam) + "';\r\n" +
                    "       setTimeout('RealCallDoSearchService(\"'+BookingItemId+'\",\"'+BookingSectionServiceId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function RealCallDoSearchService(BookingItemId, BookingSectionServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       SupplierId = document.getElementById('drpSelectSupplier').value;\r\n" +
                    "       Keyword = document.getElementById('txtSearchKeywordService').value;\r\n" +
                    "       CurrentPageIndex = parseInt(document.getElementById('txtCurrentPageIndexService').value,10);\r\n"+
                    "       AjaxOut = OneTSQ.WebParts.BookingServiceProcess.ServerSideDrawSearchServiceResult(RenderInfo, BookingItemId, BookingSectionServiceId, SupplierId, Keyword, CurrentPageIndex).value;\r\n" +
                    "       document.getElementById('divProcessingSearchService').innerHTML='';\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divSearchServiceResultContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +



                    "   function CallActionAddServiceToBookingItem(BookingItemId, BookingSectionServiceId, ServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.BookingServiceProcess.ServerSideAddServiceToBookingItem(RenderInfo, BookingItemId, BookingSectionServiceId, ServiceId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divBookingItemContent'+BookingItemId).innerHTML = AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML = AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n" +


                    "   function CallPreviewBookingService(BookingItemServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.BookingServiceProcess.ServerSidePreviewBookingItemService(RenderInfo, BookingItemServiceId).value;\r\n" +
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

                     ServerSideDrawCreateBookingStepThree(ORenderInfo, BookingId, DayIndex).HtmlContent +

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
        public static AjaxOut ServerSideDrawCreateBookingStepThree(RenderInfoCls ORenderInfo, string BookingId, string DayIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                BookingCls
                     OBooking = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateModel(ORenderInfo, BookingId);


                Html =
                    "<div id=\"divQuotation\">\r\n" +
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
                    "        <h5 style=\"padding-top:8px;padding-left:10px;color:#337ab7;font-size:20px\">Id: " + OBooking.AutoId.ToString() + "</h5> \r\n";

                if (OBooking.frkBookingStatusId.Equals("pending"))
                {
                        Html +=
                            "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallCloseBooking('" + BookingId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Đóng booking") + "</strong></button>\r\n"+
                            "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallReOpenBooking('" + BookingId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Mở lại booking") + "</strong></button>\r\n";
                }
                else
                {
                    if (OBooking.frkBookingStatusId.Equals("open"))
                    {
                        Html +=
                            "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallPendingBooking('" + BookingId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tạm dừng") + "</strong></button>\r\n"+
                            "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallCloseBooking('" + BookingId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Đóng booking") + "</strong></button>\r\n";
                    }
                    else
                    {
                        Html +=
                            "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallReOpenBooking('" + BookingId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Mở lại booking") + "</strong></button>\r\n";
                    }
                }
                Html+=
                    "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallExportPdf('" + BookingId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Xuất Pdf") + "</strong></button>\r\n" +
                    "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallPreview('" + BookingId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Xuất Html") + "</strong></button>\r\n" +
                    "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallCaculateMoney('" + BookingId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tính giá tự động") + "</strong></button>\r\n"+
                    "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallMakeTemplate('" + BookingId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tạo mẫu") + "</strong></button>\r\n" +
                    "    </div> \r\n" +
                    "    <div id=\"divProcessingExport\" style=\"text-align:right;padding:4px; color:maroon;font-size:16px;font-weight:bold\"></div>\r\n"+
                    "    <div class=\"ibox-content\"> \r\n" +
                    "       <div><table><tr><td style=\"padding:3px !important;\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Ngày khởi hành") + ":</strong></td><td style=\"padding:3px !important;\"><span style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + OBooking.StartDate.ToString("dd/MM/yyyy") + "</span></td><td style=\"padding:3px !important;\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Ngày kết thúc") + ":</strong></td><td style=\"padding:3px !important;\"><span style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + OBooking.EndDate.ToString("dd/MM/yyyy") + "</span></td><td style=\"padding:3px !important;\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Số ngày") + ":</strong></td><td style=\"padding:3px !important;\"><span style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + OBooking.NoOfDays.ToString("#,##0") + "</span></td></tr></table></div>\r\n" +
                    "       <div style=\"padding:3px !important;\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin") + "</strong>:<img src=\"images/adult.png\" style=\"padding-left:5px;height:16px\"/><span style=\"color:#337ab7;font-weight:bold;padding:1px !important;font-size:16px\"> " + OBooking.Adults.ToString("#,##0") + "</span> <img src=\"images/children.png\" style=\"padding-left:5px;height:16px\"/> <span style=\"color:#337ab7;font-weight:bold;padding:1px !important;font-size:16px\">" + OBooking.Children + "</span>   <img src=\"images/room.png\" style=\"padding-left:5px;height:16px\"/> <span style=\"color:#337ab7;font-weight:bold;padding:1px !important;font-size:16px\">" + OBooking.NoOfRoom.ToString("#,##0") + "</span>    <a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm hành trình") + "\" href=\"javascript:AddBookingItemForm('" + BookingId + "');\"><img src=\"images/plus.png\" style=\"padding-left:5px;height:16px\"/></a></div>\r\n"+

                    "       <div class=\"row\">\r\n" +
                    "           <div class=\"col-md-8 wrapper wrapper-content\" id=\"sortable-view\">\r\n" +
                                    ServerSideDrawList(ORenderInfo, BookingId, int.Parse(DayIndex)).HtmlContent +
                    "           </div>\r\n" +
                    "           <div class=\"col-md-4 rightSidebar\">\r\n" +
                    "               <div id=\"divRightInfoContent\" >\r\n" +
                                        ServerSideDrawRightInfo(ORenderInfo, BookingId).HtmlContent +
                    "               </div>\r\n"+
                    "               <div id=\"divLookUpServiceContent\">\r\n" +

                    "               </div>\r\n" +
                    "           </div>\r\n" +
                    "       </div>\r\n" +
                    "   </div>\r\n" +
                    "</div> \r\n" +
                    "</div>\r\n" +//divQuotation
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
        public static AjaxOut ServerSideDrawList(RenderInfoCls ORenderInfo, string BookingId, int DayIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                BookingItemCls[]
                    BookingItems = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().ReadingBookingItems(ORenderInfo, BookingId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                Html += "<table><tr>\r\n";
                for (int iIndex = 0; iIndex <= BookingItems.Length; iIndex++)
                {
                    string Url=WebScreen.BuildUrl(OSiteParam,OwnerCode,new CreateBookingStepThree().WebPartId,new WebParamCls[]
                    {
                        new WebParamCls("Id",BookingId),
                        new WebParamCls("DayIndex",iIndex),
                    });
                    Html += "<td style=\"padding:2px\"><a href=\""+Url+"\" target=\"_self\">"+FunctionUtility.GetNumberImage(iIndex)+"</a></td>\r\n";
                }
                Html += "<td style=\"padding:2px\"><a href=\"javascript:CallSortForm('"+BookingId+"');\" target=\"_self\"><img src=\"images/sort.png\" /></a></td>\r\n";
                Html += "</tr></table>\r\n";
                for (int iIndex = 0; iIndex < BookingItems.Length; iIndex++)
                {
                    bool Allow = true;
                    if (DayIndex > 0)
                    {
                        if (DayIndex == BookingItems[iIndex].DayIndex)
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
                            "<img  style=\"opacity:1\" onclick=\"javascript:CallLookupItineraryForm('" + BookingItems[iIndex].BookingItemId + "');\"  id=\"imgiconitinerary\"  title=\"Itinerary\"   class=\"icon-service\" src=\"images/icon-itinerary.png\" />";
                        string AppendStyle = "";
                        ServiceTypeCls[]
                            ItemServiceType = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().ReadingServiceType(ORenderInfo, BookingItems[iIndex].BookingItemId);

                        for (int iIndexServiceType = 0; iIndexServiceType < ItemServiceType.Length; iIndexServiceType++)
                        {
                            if (ItemServiceType[iIndexServiceType].AllowService == 1)
                            {
                                AppendStyle = "style=\"opacity:0.25\"";
                                if (ItemServiceType[iIndexServiceType].CheckService == 1)
                                {
                                    AppendStyle = "style=\"opacity:1\"";
                                }
                                DrawPlugInButtonText += "<img " + AppendStyle + " onclick=\"javascript:CallAddSectionService('" + BookingItems[iIndex].BookingItemId + "','" + ItemServiceType[iIndexServiceType].ServiceTypeId + "');\"  id=\"imgicon" + BookingItems[iIndex].BookingItemId + ItemServiceType[iIndexServiceType].ServiceTypeId + "\"  title=\"" + ItemServiceType[iIndexServiceType].ServiceTypeName + "\"   class=\"icon-service\" src=\"images/icon-" + ItemServiceType[iIndexServiceType].ServiceTypeId + ".png\" /> <input id=\"chk" + BookingItems[iIndex].BookingItemId + ItemServiceType[iIndexServiceType].ServiceTypeId + "\"    type=checkbox style=\"display:none\" " + (ItemServiceType[iIndexServiceType].CheckService == 1 ? "CHECKED" : "") + ">";
                            }
                        }

                        DrawPlugInButtonText +=
                            " <a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm dịch vụ vào hành  trình") + "\" href=\"javascript:AddPlusService('" + BookingItems[iIndex].BookingItemId + "');\"><img src=\"images/plus.png\" style=\"padding-left:5px;height:16px\"/></a>";

                        string AppendNight = "<td>" + WebLanguage.GetLanguage(OSiteParam, "Đêm") + ": " + BookingItems[iIndex].Night.ToString("#,##0") + " <img style=\"height:16px\" src=\"images/night.png\"/></td>";
                        
                        Html +=
                            "    <div class=\"ibox \">\r\n" +
                            "        <div class=\"ibox-title\" style=\"background-color:#ada8a0;padding-top:0px;height:35px\">\r\n" +
                            "            <h5><table><tr><td>" + FunctionUtility.GetNumberImage((iIndex + 1)) + "</td><td style=\"width:2px;text-align:center\">|<td><td><span style=\"color:#337ab7;\">" + BookingItems[iIndex].LocationServiceName + "</span><td><td>" + DrawPlugInButtonText + "</td> <td style=\"width:2px;text-align:center\">|<td>"+ AppendNight+"    <td> <a href=\"javascript:DeleteBookingItem('" + BookingId + "','" + BookingItems[iIndex].BookingItemId + "');\"><img style=\"height:16px\" src=\"/images/trash.png\"></a></td>  </tr></table></h5>\r\n" +
                            "        </div>\r\n" +
                            "        <div class=\"ibox-content\" id=\"divBookingItemContent" + BookingItems[iIndex].BookingItemId + "\">\r\n" +
                                        ServerSideDrawBookingItem(ORenderInfo, BookingItems[iIndex].BookingItemId).HtmlContent +
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
        public static AjaxOut ServerSideDrawRightInfo(RenderInfoCls ORenderInfo, string BookingId)
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
                    "                   " + ServerSideStaticPriceInfo(ORenderInfo, BookingId).HtmlContent + "\r\n" +
                    "            </div> \r\n" +
                    "        </div> \r\n" +
                    "        <div id=\"tab-2\" class=\"tab-pane\"> \r\n" +
                    "            <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +
                                        ServerSideDrawGeneralInfo(ORenderInfo, BookingId).HtmlContent+
                    "            </div> \r\n" +
                    "        </div> \r\n" +
                    "        <div id=\"tab-3\" class=\"tab-pane\"> \r\n" +
                    "            <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +
                                        ServerSideDrawExtraInfo(ORenderInfo, BookingId).HtmlContent +
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
            string BookingId)
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
                        ServerSideDrawMoneyStaticInfo(ORenderInfo, BookingId).HtmlContent +
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
        public static AjaxOut ServerSideDrawGeneralInfo(RenderInfoCls ORenderInfo, string BookingId)
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

                BookingCls
                    OBooking = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateModel(ORenderInfo, BookingId);
                string SelectCategoryRoomText =
                     "<select id=\"drpSelectServiceTypeCategory\" class=\"form-control\">\r\n" +
                     "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n";
                for (int iIndex = 0; iIndex < ServiceTypeCategories.Length; iIndex++)
                {
                    if (OBooking.frkHotelCategoryId.Equals(ServiceTypeCategories[iIndex].ServiceTypeCategoryId))
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
                           "                <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Ngày khởi hành") + "</label> <input id=\"txtStartDate\" type=\"text\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Ngày khởi hành") + "\" class=\"form-control CssDate\" value=\"" + OBooking.StartDate.ToString("dd/MM/yyyy") + "\"></div> \r\n" +
                           "                <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số phòng") + "</label> <input class=\"form-control touchspin3\" type=\"text\" value=\"" + OBooking.NoOfRoom.ToString("#,##0") + "\" id=\"txtNoOfRoom\"></div> \r\n" +
                           "                <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số khách") + "</label> <table><tr><td>" + WebLanguage.GetLanguage(OSiteParam, "Người lớn") + "<td><td>" + WebLanguage.GetLanguage(OSiteParam, "Trẻ em") + "</td></tr>   <tr><td><input class=\"form-control touchspin3\" type=\"text\" value=\"" + OBooking.Adults.ToString("#,##0") + "\" id=\"txtAdults\"><td><td><input class=\"form-control touchspin3\" type=\"text\" value=\"" + OBooking.Children.ToString("#,##0") + "\" id=\"txtChildren\"></td></tr> </table></div> \r\n" +
                           "                <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Loại khách sạn") + "</label> " + SelectCategoryRoomText + "</div> \r\n" +

                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên khách") + "</label> <input id=\"txtCustomerName\" value=\"" + OBooking.CustomerName + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên khách hàng") + "\" class=\"form-control\"></div> \r\n" +
                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "</label> <input id=\"txtCustomerAddress\" value=\"" + OBooking.CustomerAddress + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ khách hàng") + "\" class=\"form-control\"></div> \r\n" +
                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điện thoại") + "</label> <input id=\"txtCustomerMobile\" value=\"" + OBooking.CustomerMobile + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điện thoại khách hàng") + "\" class=\"form-control\"></div> \r\n" +
                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Email") + "</label> <input id=\"txtCustomerEmail\" value=\"" + OBooking.CustomerEmail + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Email khách hàng") + "\" class=\"form-control\"></div> \r\n" +

                           "                 <div> \r\n" +
                           "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdateCustomerInfo('" + BookingId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideDrawExtraInfo(RenderInfoCls ORenderInfo, string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                BookingCls
                    OBooking = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateModel(ORenderInfo, BookingId);

                Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\">\r\n" +
                    "             <div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề") + "</label> <input id=\"txtBookingSubject\" class=\"form-control\" value=\"" + OBooking.BookingSubject + "\" ></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đầu trang") + "</label> <textarea id=\"txtHeader\">" + OBooking.Header + "</textarea></div> \r\n" +
                    
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Bao gồm") + "</label> <textarea id=\"txtInclude\">"+OBooking.Include+"</textarea></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Không bao gồm") + "</label> <textarea id=\"txtExclude\">" + OBooking.Exclude + "</textarea></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt")+ "</label> <textarea id=\"txtSpecialCondition\">" + OBooking.SpecialCondition + "</textarea></div> \r\n" +

                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Cuối trang") + "</label> <textarea id=\"txtFooter\">" + OBooking.Footer + "</textarea></div> \r\n" +
                    "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Css") + "</label> <textarea class=\"form-control\" id=\"txtCss\">" + OBooking.Css + "</textarea></div> \r\n" +
                    "                 <div> \r\n" +
                    "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdateExtraInfo('" + BookingId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật") + "</strong></button> \r\n" +
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
            string BookingId,
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


                BookingCls
                    OBooking = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateModel(ORenderInfo, BookingId);

                OBooking.StartDate = FunctionUtility.VNDateToDate(StartDate);
                OBooking.NoOfDays = 1;
                OBooking.NoOfRoom = int.Parse(NoOfRoom);
                OBooking.Adults = int.Parse(Adults);
                OBooking.Children = int.Parse(Children);
                OBooking.frkHotelCategoryId = HotelCategoryId;

                OBooking.CustomerName = CustomerName;
                OBooking.CustomerMobile = CustomerMobile;
                OBooking.CustomerAddress = CustomerAddress;
                OBooking.CustomerEmail = CustomerEmail;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().Save(ORenderInfo, BookingId, OBooking);
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
            string BookingId,
            string BookingSubject, 
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
                BookingCls
                    OBooking = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateModel(ORenderInfo, BookingId);
                OBooking.BookingSubject = BookingSubject;
                OBooking.Header = Header;
                OBooking.Footer = Footer;
                OBooking.Include = Include;
                OBooking.Exclude = Exclude;
                OBooking.SpecialCondition = SpecialCondition;
                OBooking.Css = Css;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().Save(ORenderInfo, BookingId, OBooking);
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
        public static AjaxOut ServerSideDeleteBookingItem(
            RenderInfoCls ORenderInfo,
            string BookingId,
            string BookingItemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().DeleteBookingItem(ORenderInfo, BookingItemId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateBookingStepThree().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("Id",BookingId),
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
        public static AjaxOut ServerSideAddBookingItem(
            RenderInfoCls ORenderInfo,
            string BookingId, 
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

                BookingItemCls
                    OBookingItem = new BookingItemCls();
                OBookingItem.BookingItemId = System.Guid.NewGuid().ToString();
                OBookingItem.Night = Night;
                OBookingItem.frkLocationServiceId = LocationServiceId;
                OBookingItem.frkHotelCategoryId = HotelCategoryId;
                OBookingItem.frkBookingId = BookingId;

                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().AddBookingItem(ORenderInfo, BookingId, OBookingItem);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateBookingStepThree().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("Id",BookingId),
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
        public static AjaxOut ServerSideDrawAddBookingItemForm(
            RenderInfoCls ORenderInfo,
            string BookingId)
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
                    "                        <button onclick=\"javascript:CallAddBookingItemAction('"+BookingId+"');\" class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideDrawBookingItem(
            RenderInfoCls ORenderInfo,
            string BookingItemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                BookingItemCls
                    OBookingItem = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingItemModel(ORenderInfo, BookingItemId);

                BookingItemItineraryCls[]
                    BookingItemItineraries = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().ReadingBookingItemItineraries(ORenderInfo, BookingItemId);
                if (BookingItemItineraries.Length > 0)
                {
                    Html +=
                        "<div style=\"height:30px;background-color:whitesmoke;font-size:10px\"><table><tr><td style=\"width:20px\"><img style=\"height:16px\" src=\"images/icon-itinerary.png\" /></td><td><h4 style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + WebLanguage.GetLanguage(OSiteParam, "Hành trình") + "</h4></td></tr></table></div>\r\n" +
                        "   <ul>\r\n";
                    for (int iIndex = 0; iIndex < BookingItemItineraries.Length; iIndex++)
                    {
                        Html += "<li style=\"list-style-type:none;padding:1px\"><a href=\"javascript:CallPreviewBookingItinerary('" + BookingItemItineraries[iIndex].BookingItemItineraryId + "');\"><img style=\"height:16px\" src=\"images/marker.png\"/></a> <a href=\"javascript:RemoveItinerary('" + BookingItemId + "','" + BookingItemItineraries[iIndex].BookingItemItineraryId + "');\"><img style=\"height:16px\" src=\"/images/trash.png\"></a> <a href=\"javascript:CallPreviewBookingItinerary('" + BookingItemItineraries[iIndex].BookingItemItineraryId + "');\">" + BookingItemItineraries[iIndex].Subject + "</a></li>\r\n";
                    }
                    Html += "</ul>\r\n";
                }

                BookingSectionServiceCls[]
                   BookingSectionServices = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().ReadingBookingSectionServices(ORenderInfo, BookingItemId);
                if (BookingSectionServices.Length > 0)
                {
                    for (int iIndex = 0; iIndex < BookingSectionServices.Length; iIndex++)
                    {
                        Html +=
                            "<div style=\"height:25px;font-size:10px; background-color:whitesmoke;margin-bottom:2px\"><table><tr><td style=\"width:20px\"><img style=\"height:16px\" src=\"images/icon-" + BookingSectionServices[iIndex].frkServiceTypeId + ".png\" /></td><td style=\"width:350px\"><h5 style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + BookingSectionServices[iIndex].ServiceTypeName + "</h5></td> <td><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm chi tiết dịch vụ") + "\" href=\"javascript:CallLookupServiceForm('" + BookingItemId + "','" + BookingSectionServices[iIndex].BookingSectionServiceId + "');\"><img src=\"images/plus.png\" style=\"padding-left:5px;height:16px\"/></a></td> <td><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa nhóm dịch vụ") + "\" href=\"javascript:RemoveBookingSessionService('" + BookingItemId + "','" + BookingSectionServices[iIndex].BookingSectionServiceId + "');\"><img src=\"images/remove.png\" style=\"padding-left:5px;height:16px\"/></a></td></tr></table></div>\r\n" +
                            "<div id=\"divListBookingSectionService" + BookingSectionServices[iIndex].BookingSectionServiceId + "\">\r\n" +
                                ServerSideDrawBookingService(ORenderInfo, BookingSectionServices[iIndex].BookingSectionServiceId).HtmlContent +
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
        public static AjaxOut ServerSideDeleteBookingService(
            RenderInfoCls ORenderInfo, 
            string BookingId,
            string BookingSectionServiceId, 
            string BookingServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().DeleteBookingService(ORenderInfo, BookingServiceId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.HtmlContent = ServerSideDrawBookingService(ORenderInfo, BookingSectionServiceId).HtmlContent;
                RetAjaxOut.RetExtraParam1 = ServerSideDrawMoneyStaticInfo(ORenderInfo, BookingId).HtmlContent;
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
            string BookingId,
            string BookingServiceId,
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

                BookingServiceCls
                    OBookingService = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingServiceModel(ORenderInfo, BookingServiceId);
                OBookingService.Quantity = decimal.Parse(Quantity);
                OBookingService.SubTotal = OBookingService.CaculateSubTotal;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().SaveBookingService(ORenderInfo, BookingServiceId, OBookingService);

                //RetAjaxOut.HtmlContent = ServerSideDrawBookingService(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.InfoMessage = OBookingService.SubTotal.ToString("#,##0");
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
            string BookingId,
            string BookingServiceId,
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

                BookingServiceCls
                    OBookingService = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingServiceModel(ORenderInfo, BookingServiceId);
                OBookingService.NoOfServiceDays = int.Parse(NoOfServiceDays);
                OBookingService.SubTotal = OBookingService.CaculateSubTotal;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().SaveBookingService(ORenderInfo, BookingServiceId, OBookingService);

                //RetAjaxOut.HtmlContent = ServerSideDrawBookingService(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.InfoMessage = OBookingService.SubTotal.ToString("#,##0");
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
            string BookingId,
            string BookingServiceId,
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

                BookingServiceCls
                    OBookingService = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingServiceModel(ORenderInfo, BookingServiceId);
                OBookingService.StartDate = FunctionUtility.VNDateToDate(StartDate);
                OBookingService.SubTotal = OBookingService.CaculateSubTotal;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().SaveBookingService(ORenderInfo, BookingServiceId, OBookingService);

                //RetAjaxOut.HtmlContent = ServerSideDrawBookingService(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.InfoMessage = OBookingService.SubTotal.ToString("#,##0");
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
            string BookingId,
            string BookingServiceId,
            string StartTime)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                
                BookingServiceCls
                    OBookingService = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingServiceModel(ORenderInfo, BookingServiceId);
                OBookingService.StartTime = StartTime;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().SaveBookingService(ORenderInfo, BookingServiceId, OBookingService);

                //RetAjaxOut.HtmlContent = ServerSideDrawBookingService(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.InfoMessage = OBookingService.SubTotal.ToString("#,##0");
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
           string BookingId,
           string BookingServiceId,
           string EndTime)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;

                
                BookingServiceCls
                    OBookingService = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingServiceModel(ORenderInfo, BookingServiceId);
                OBookingService.EndTime = EndTime;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().SaveBookingService(ORenderInfo, BookingServiceId, OBookingService);

                //RetAjaxOut.HtmlContent = ServerSideDrawBookingService(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.InfoMessage = OBookingService.SubTotal.ToString("#,##0");
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
            string BookingId,
            string BookingServiceId,
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

                BookingServiceCls
                    OBookingService = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingServiceModel(ORenderInfo, BookingServiceId);
                OBookingService.Price = decimal.Parse(Price);
                OBookingService.SubTotal = OBookingService.CaculateSubTotal;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().SaveBookingService(ORenderInfo, BookingServiceId, OBookingService);

                //RetAjaxOut.HtmlContent = ServerSideDrawBookingService(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.InfoMessage = OBookingService.SubTotal.ToString("#,##0");
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
            string BookingId,
            string BookingServiceId,
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

                BookingServiceCls
                    OBookingService = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingServiceModel(ORenderInfo, BookingServiceId);
                OBookingService.ExtraFee = decimal.Parse(ExtraFee);
                OBookingService.SubTotal = OBookingService.CaculateSubTotal;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().SaveBookingService(ORenderInfo, BookingServiceId, OBookingService);

                //RetAjaxOut.HtmlContent = ServerSideDrawBookingService(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.HtmlContent = ServerSideDrawMoneyStaticInfo(ORenderInfo, BookingId).HtmlContent;
                RetAjaxOut.InfoMessage = OBookingService.SubTotal.ToString("#,##0");
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
        public static AjaxOut ServerSideSaveBookingItemIndex(
            RenderInfoCls ORenderInfo,
            string BookingItemId,
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

                BookingItemCls
                    OBookingItem = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingItemModel(ORenderInfo, BookingItemId);
                OBookingItem.DayIndex = int.Parse(SortIndex);

                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().SaveBookingItem(ORenderInfo, BookingItemId, OBookingItem);
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
            string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                BookingItemCls[]
                    BookingItems = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().ReadingBookingItems(ORenderInfo, BookingId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string Url = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateBookingStepThree().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("id",BookingId)
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
                for (int iIndex = 0; iIndex < BookingItems.Length; iIndex++)
                {
                    Html +=
                        "                 <tr id=\"tr" + BookingItems[iIndex].BookingItemId + "\"> \r\n" +
                        "                     <td class=\"td-center\" style=\"width:40px\"><input onblur=\"javascript:SaveUpdateBookingIndexItem('" + BookingItems[iIndex].BookingItemId + "');\" class=\"form-control\" style=\"text-align:right; width:40px;background-color:lightyellow\" id=\"txtSortIndexBookingItem" + BookingItems[iIndex].BookingItemId + "\" value=\"" + BookingItems[iIndex].DayIndex.ToString("#,##0") + "\"></td> \r\n" +
                        "                     <td style=\"width:180px\">" + BookingItems[iIndex].LocationServiceName + "</td> \r\n" +
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
            string BookingId)
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
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên mẫu") + "</label> <input id=\"txtBookingTemplateName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên mẫu") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionSaveTemplate('"+BookingId+"');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideDrawBookingService(
            RenderInfoCls ORenderInfo, 
            string BookingServiceSectionId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                BookingServiceCls[]
                    BookingServices = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().ReadingBookingServices(ORenderInfo, BookingServiceSectionId);
                BookingSectionServiceCls
                    OBookingSectionService = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingSectionServiceModel(ORenderInfo, BookingServiceSectionId);
                BookingItemCls
                    BookingItem = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingItemModel(ORenderInfo, OBookingSectionService.frkBookingItemId);
                string BookingId = BookingItem.frkBookingId;

                if (OBookingSectionService.CalculateType.ToLower().Equals("pax"))
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
                    for (int iIndex = 0; iIndex < BookingServices.Length; iIndex++)
                    {
                        Html +=
                            "                 <tr id=\"tr" + BookingServices[iIndex].BookingServiceId + "\"> \r\n" +
                            "                     <td style=\"width:180px\">" + BookingServices[iIndex].ServiceName + "</td> \r\n" +
                            "                     <td style=\"width:40px\"><input class=\"form-control\" style=\"text-align:right;width:40px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateQuantityItem('" + BookingServices[iIndex].BookingServiceId + "');\" id=\"txtQuantity" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + BookingServices[iIndex].Quantity.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" onblur=\"javascript:SaveUpdatePriceItem('" + BookingServices[iIndex].BookingServiceId + "');\" id=\"txtPrice" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + BookingServices[iIndex].Price.ToString("#,##0") + "\"></td> \r\n" +
                            //"                     <td style=\"width:100px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateExtraFeeItem('" + BookingServices[iIndex].BookingServiceId + "');\" id=\"txtExtraFee" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + BookingServices[iIndex].ExtraFee.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" id=\"txtSubTotal" + BookingServices[iIndex].BookingServiceId + "\" READONLY value=\"" + BookingServices[iIndex].SubTotal.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:20px\" class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "\" href=\"javascript:CallActionDeleteBookingService('" + BookingId+"','"+BookingServices[iIndex].frkBookingSectionServiceId + "','" + BookingServices[iIndex].BookingServiceId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n";
                }
                if (OBookingSectionService.CalculateType.ToLower().Equals("date"))
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
                    for (int iIndex = 0; iIndex < BookingServices.Length; iIndex++)
                    {
                        string StartDateText = "";
                        if (BookingServices[iIndex].StartDate.Year > 1)
                        {
                            StartDateText = BookingServices[iIndex].StartDate.ToString("dd/MM/yyyy");
                        }
                        Html +=
                            "                 <tr id=\"tr" + BookingServices[iIndex].BookingServiceId + "\"> \r\n" +
                            "                     <td >" + BookingServices[iIndex].ServiceName + "</td> \r\n" +
                            "                     <td style=\"width:70px\"><input style=\"text-align:center\" onblur=\"javascript:SaveUpdateStartDateItem('" + BookingServices[iIndex].BookingServiceId + "');\"  class=\"form-control CssDate\" id=\"txtStartDateItem" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + StartDateText + "\"></td> \r\n" +
                            "                     <td style=\"width:40px\"><input class=\"form-control\" style=\"text-align:right;width:40px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateNoOfServiceDayssItem('" + BookingServices[iIndex].BookingServiceId + "');\" id=\"txtNoOfServiceDays" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + BookingServices[iIndex].NoOfServiceDays.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:30px\"><input class=\"form-control\" style=\"text-align:right;width:30px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateQuantityItem('" + BookingServices[iIndex].BookingServiceId + "');\" id=\"txtQuantity" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + BookingServices[iIndex].Quantity.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" onblur=\"javascript:SaveUpdatePriceItem('" + BookingServices[iIndex].BookingServiceId + "');\" id=\"txtPrice" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + BookingServices[iIndex].Price.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" id=\"txtSubTotal" + BookingServices[iIndex].BookingServiceId + "\" READONLY value=\"" + BookingServices[iIndex].SubTotal.ToString("#,##0") + "\"></td> \r\n" +

                            "                     <td style=\"width:20px\" class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "\" href=\"javascript:CallActionDeleteBookingService('" + BookingId+"','"+BookingServices[iIndex].frkBookingSectionServiceId + "','" + BookingServices[iIndex].BookingServiceId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n";
                }


                if (OBookingSectionService.CalculateType.ToLower().Equals("time"))
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
                    for (int iIndex = 0; iIndex < BookingServices.Length; iIndex++)
                    {
                        Html +=
                            "                 <tr id=\"tr" + BookingServices[iIndex].BookingServiceId + "\"> \r\n" +
                            "                     <td >" + BookingServices[iIndex].ServiceName + "</td> \r\n" +
                            "                     <td style=\"width:60px\"><input style=\"text-align:center\" onblur=\"javascript:SaveUpdateStartTimeItem('" + BookingServices[iIndex].BookingServiceId + "');\"  class=\"form-control\" id=\"txtStartTimeItem" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + BookingServices[iIndex].StartTime + "\"></td> \r\n" +
                            "                     <td style=\"width:60px\"><input style=\"text-align:center\" onblur=\"javascript:SaveUpEndTimeItem('" + BookingServices[iIndex].BookingServiceId + "');\"  class=\"form-control\" id=\"txtEndTimeItem" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + BookingServices[iIndex].EndTime + "\"></td> \r\n" +
                            "                     <td style=\"width:30px\"><input class=\"form-control\" style=\"text-align:right;width:30px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateQuantityItem('" + BookingServices[iIndex].BookingServiceId + "');\" id=\"txtQuantity" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + BookingServices[iIndex].Quantity.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" onblur=\"javascript:SaveUpdatePriceItem('" + BookingServices[iIndex].BookingServiceId + "');\" id=\"txtPrice" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + BookingServices[iIndex].Price.ToString("#,##0") + "\"></td> \r\n" +
                            //"                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" onblur=\"javascript:SaveUpdateExtraFeeItem('" + BookingServices[iIndex].BookingServiceId + "');\" id=\"txtExtraFee" + BookingServices[iIndex].BookingServiceId + "\" value=\"" + BookingServices[iIndex].ExtraFee.ToString("#,##0") + "\"></td> \r\n" +
                            "                     <td style=\"width:70px\"><input class=\"form-control\" style=\"text-align:right;width:70px;background-color:lightyellow\" id=\"txtSubTotal" + BookingServices[iIndex].BookingServiceId + "\" READONLY value=\"" + BookingServices[iIndex].SubTotal.ToString("#,##0") + "\"></td> \r\n" +

                            "                     <td style=\"width:20px\" class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "\" href=\"javascript:CallActionDeleteBookingService('" + BookingId + "','" + BookingServices[iIndex].frkBookingSectionServiceId + "','" + BookingServices[iIndex].BookingServiceId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
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
            string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                ServiceTypeCls[]
                    ServiceTypes = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().ReadingMoneyStatiticsInfo(ORenderInfo, BookingId);

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
            string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                RetAjaxOut.RetUrl = "preview.aspx?sv=Booking&id=" + BookingId;
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
            string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CaculatePrice(ORenderInfo, BookingId);
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
        

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideCallExportPdf(
            RenderInfoCls ORenderInfo,
            string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                RetAjaxOut = BookingPreviewUtility.Preview(ORenderInfo,BookingId,"pdf");
                if (RetAjaxOut.Error) throw new Exception(RetAjaxOut.InfoMessage);
                byte[] Bytes = FunctionUtility.GeneratePdfFromFile(RetAjaxOut.RetUrl);
                MediaInfoCls
                    OMediaInfo = new MediaInfoCls();
                OMediaInfo.LoginName = "system";
                OMediaInfo.MediaInfoId = BookingId;
                OMediaInfo.Month = System.DateTime.Now.Month;
                OMediaInfo.Year = System.DateTime.Now.Year;
                OMediaInfo.Overwrite = true;
                OMediaInfo.Section = "Booking";
                OMediaInfo.SiteId = ORenderInfo.SiteId;
                OMediaInfo.UploadFileName = BookingId + ".pdf";

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
        public static AjaxOut ServerSideCallCloseBooking(
            RenderInfoCls ORenderInfo,
            string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                BookingCls
                    OBooking = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateModel(ORenderInfo, BookingId);
                OBooking.frkBookingStatusId = "closed";
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().Save(ORenderInfo, BookingId, OBooking);
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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideCallPendingBooking(
            RenderInfoCls ORenderInfo,
            string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                BookingCls
                    OBooking = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateModel(ORenderInfo, BookingId);
                OBooking.frkBookingStatusId = "pending";
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().Save(ORenderInfo, BookingId, OBooking);
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
        


        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideCallReOpenBooking(
           RenderInfoCls ORenderInfo,
           string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                BookingCls
                    OBooking = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateModel(ORenderInfo, BookingId);
                OBooking.frkBookingStatusId = "open";
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().Save(ORenderInfo, BookingId, OBooking);
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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideAddBookingTemplate(
            RenderInfoCls ORenderInfo,
            string BookingId,
            string Subject)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                if (string.IsNullOrEmpty(Subject)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tiêu đề mẫu chưa nhập"));
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().SaveQuotationTemplate(ORenderInfo, BookingId, Subject);

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
    }
}
