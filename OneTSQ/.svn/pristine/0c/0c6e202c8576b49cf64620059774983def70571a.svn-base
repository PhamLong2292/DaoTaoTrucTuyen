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
    public class CreateQuotationTemplateStepThree : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "CreateQuotationTemplateStepThree";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Xây dựng mẫu chào giá - Chọn dịch vụ";
            }
        }

        public override string Description
        {
            get
            {
                return "Xây dựng mẫu  chào giá - Chọn dịch vụ";
            }
        }


        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CreateQuotationTemplateStepThree),Page);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(QuotationTemplateItemItineraryProcess), Page);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(QuotationTemplateServiceProcess), Page);
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
                return new string[] { new ProcessingQuotationTemplateList().WebPartId };
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


                string QuotationTemplateId = (string)WebEnvironments.Request("Id");
                if (string.IsNullOrEmpty(QuotationTemplateId))
                {
                    QuotationTemplateId = "";
                }

                string DayIndex = (string)WebEnvironments.Request("DayIndex");
                if (string.IsNullOrEmpty(DayIndex))
                {
                    DayIndex = "0";
                }
                RetAjaxOut.HtmlContent =

                    "<style>\r\n" +
                    ".leftSidebar {\r\n" +
                    "    width: 25%;\r\n" +
                    "    float: left;\r\n" +
                    "    padding: 0 30px 0 0;\r\n" +
                    "    position: relative;\r\n" +
                    "}\r\n" +

                    ".rightSidebar {\r\n" +
                    "    position: relative;\r\n" +
                    "}\r\n" +
                    "</style>\r\n" +

                    "<input id=\"txtQuotationTemplateId\" type=\"hidden\" value=\"" + QuotationTemplateId + "\">\r\n" +
                    "<script>\r\n" +

                    "   function CallActionSaveTemplate(QuotationTemplateId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       Subject = document.getElementById('txtQuotationTemplateTemplateName').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideAddQuotationTemplateTemplate(RenderInfo, QuotationTemplateId, Subject).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "       CallBackFromLookUpForm();\r\n" +
                    "   }\r\n" +

                    //"   function CallPreview(QuotationTemplateId)\r\n" +
                    //"   {\r\n" +
                    //"       RenderInfo=CreateRenderInfo();\r\n" +
                    //"       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideCallPreview(RenderInfo, QuotationTemplateId).value;\r\n" +
                    //"       if(AjaxOut.Error)\r\n" +
                    //"       {\r\n" +
                    //"           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    //"           return;\r\n" +
                    //"       }\r\n" +
                    //"       window.open(AjaxOut.RetUrl,'_blank');\r\n"+
                    //"   }\r\n" +

                    //"   function CallCaculateMoney(QuotationTemplateId)\r\n" +
                    //"   {\r\n" +
                    //"       RenderInfo=CreateRenderInfo();\r\n" +
                    //"       alert('Đang làm chưa xong :)');\r\n"+
                    //"   }\r\n" +

                    //"   function CallMakeTemplate(QuotationTemplateId)\r\n" +
                    //"   {\r\n" +
                    //"       RenderInfo=CreateRenderInfo();\r\n" +

                    //"       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideDrawAddMakeTemplateForm(RenderInfo, QuotationTemplateId).value;\r\n" +
                    //"       if(AjaxOut.Error)\r\n" +
                    //"       {\r\n" +
                    //"           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    //"           return;\r\n" +
                    //"       }\r\n" +
                    //"       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    //"       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    //"       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    //"   }\r\n" +


                    //"   function CallExportPdf(QuotationTemplateId)\r\n" +
                    //"   {\r\n" +
                    //"       RenderInfo=CreateRenderInfo();\r\n" +
                    //"       document.getElementById('divProcessingExport').innerHTML='"+WebLanguage.GetLanguage(OSiteParam,"Đang xuất dữ liệu")+"...';\r\n"+
                    //"       setTimeout('RealCallExportPdf(\"'+QuotationTemplateId+'\")',10);\r\n" +
                    //"   }\r\n" +

                    //"   function RealCallExportPdf(QuotationTemplateId)\r\n" +
                    //"   {\r\n" +
                    //"       RenderInfo=CreateRenderInfo();\r\n" +
                    //"       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideCallExportPdf(RenderInfo, QuotationTemplateId).value;\r\n" +
                    //"       document.getElementById('divProcessingExport').innerHTML='';\r\n"+
                    //"       if(AjaxOut.Error)\r\n" +
                    //"       {\r\n" +
                    //"           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    //"           return;\r\n" +
                    //"       }\r\n" +
                    //"       document.getElementById('divProcessingExport').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    //"   }\r\n" +


                    "   function SaveUpdateQuotationTemplateIndexItem(QuotationTemplateItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndexQuotationTemplateItem'+QuotationTemplateItemId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideSaveQuotationTemplateItemIndex(RenderInfo, QuotationTemplateItemId, SortIndex).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +

                    "   function SaveUpdateQuantityItem(QuotationTemplateServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       QuotationTemplateId= document.getElementById('txtQuotationTemplateId').value;\r\n" +
                    "       Quantity = document.getElementById('txtQuantity'+QuotationTemplateServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideSaveUpdateQuantity(RenderInfo, QuotationTemplateId, QuotationTemplateServiceId, Quantity).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+QuotationTemplateServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('txtQuantity'+QuotationTemplateServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n" +

                    "   function SaveUpdateNoOfServiceDayssItem(QuotationTemplateServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       QuotationTemplateId= document.getElementById('txtQuotationTemplateId').value;\r\n" +
                    "       NoOfServiceDays = document.getElementById('txtNoOfServiceDays'+QuotationTemplateServiceId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideSaveUpdateNoOfServiceDays(RenderInfo, QuotationTemplateId, QuotationTemplateServiceId, NoOfServiceDays).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('txtSubTotal'+QuotationTemplateServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('txtNoOfServiceDays'+QuotationTemplateServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n" +

                    //"   function SaveUpdateStartDateItem(QuotationTemplateServiceId)\r\n" +
                    //"   {\r\n" +
                    //"       RenderInfo=CreateRenderInfo();\r\n" +
                    //"       QuotationTemplateId= document.getElementById('txtQuotationTemplateId').value;\r\n" +
                    //"       StartDate = document.getElementById('txtStartDateItem'+QuotationTemplateServiceId).value;\r\n" +
                    //"       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideSaveUpdateStartDate(RenderInfo, QuotationTemplateId, QuotationTemplateServiceId, StartDate).value;\r\n" +
                    //"       if(AjaxOut.Error)\r\n" +
                    //"       {\r\n" +
                    //"           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    //"           return;\r\n" +
                    //"       }\r\n" +
                    //"       document.getElementById('txtSubTotal'+QuotationTemplateServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    //"       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    //"   }\r\n" +



                    //"   function SaveUpdateStartTimeItem(QuotationTemplateServiceId)\r\n" +
                    //"   {\r\n" +
                    //"       RenderInfo=CreateRenderInfo();\r\n" +
                    //"       QuotationTemplateId= document.getElementById('txtQuotationTemplateId').value;\r\n" +
                    //"       StartTime = document.getElementById('txtStartTimeItem'+QuotationTemplateServiceId).value;\r\n" +
                    //"       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideSaveUpdateStartTimeItem(RenderInfo, QuotationTemplateId, QuotationTemplateServiceId, StartTime).value;\r\n" +
                    //"       if(AjaxOut.Error)\r\n" +
                    //"       {\r\n" +
                    //"           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    //"           return;\r\n" +
                    //"       }\r\n" +
                    //"       document.getElementById('txtSubTotal'+QuotationTemplateServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    //"       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    //"   }\r\n" +


                    //"   function SaveUpEndTimeItem(QuotationTemplateServiceId)\r\n" +
                    //"   {\r\n" +
                    //"       RenderInfo=CreateRenderInfo();\r\n" +
                    //"       QuotationTemplateId= document.getElementById('txtQuotationTemplateId').value;\r\n" +
                    //"       EndTime = document.getElementById('txtEndTimeItem'+QuotationTemplateServiceId).value;\r\n" +
                    //"       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideSaveUpdateEndTimeItem(RenderInfo, QuotationTemplateId, QuotationTemplateServiceId, EndTime).value;\r\n" +
                    //"       if(AjaxOut.Error)\r\n" +
                    //"       {\r\n" +
                    //"           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    //"           return;\r\n" +
                    //"       }\r\n" +
                    //"       document.getElementById('txtSubTotal'+QuotationTemplateServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    //"       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    //"   }\r\n" +

                    //"   function SaveUpdatePriceItem(QuotationTemplateServiceId)\r\n" +
                    //"   {\r\n" +
                    //"       RenderInfo=CreateRenderInfo();\r\n" +
                    //"       QuotationTemplateId= document.getElementById('txtQuotationTemplateId').value;\r\n" +
                    //"       Price = document.getElementById('txtPrice'+QuotationTemplateServiceId).value;\r\n" +
                    //"       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideSaveUpdatePrice(RenderInfo, QuotationTemplateId, QuotationTemplateServiceId, Price).value;\r\n" +
                    //"       if(AjaxOut.Error)\r\n" +
                    //"       {\r\n" +
                    //"           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    //"           return;\r\n" +
                    //"       }\r\n" +
                    //"       document.getElementById('txtSubTotal'+QuotationTemplateServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    //"       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    //"       document.getElementById('txtPrice'+QuotationTemplateServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    //"   }\r\n" +

                    //"   function SaveUpdateExtraFeeItem(QuotationTemplateServiceId)\r\n" +
                    //"   {\r\n" +
                    //"       RenderInfo=CreateRenderInfo();\r\n" +
                    //"       QuotationTemplateId= document.getElementById('txtQuotationTemplateId').value;\r\n" +
                    //"       ExtraFee = document.getElementById('txtExtraFee'+QuotationTemplateServiceId).value;\r\n" +
                    //"       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideSaveUpdateExtraFee(RenderInfo, QuotationTemplateId, QuotationTemplateServiceId, ExtraFee).value;\r\n" +
                    //"       if(AjaxOut.Error)\r\n" +
                    //"       {\r\n" +
                    //"           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    //"           return;\r\n" +
                    //"       }\r\n" +
                    //"       document.getElementById('txtSubTotal'+QuotationTemplateServiceId).value=AjaxOut.InfoMessage;\r\n" +
                    //"       document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    //"       document.getElementById('txtExtraFee'+QuotationTemplateServiceId).value=AjaxOut.RetExtraParam1;\r\n" +
                    //"   }\r\n" +


                    "   function AddQuotationTemplateItemForm(QuotationTemplateId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideDrawAddQuotationTemplateItemForm(RenderInfo, QuotationTemplateId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "       $('#drpSelectLocationService').select2();\r\n" +
                    "       $('#drpSelectServiceTypeCategory').select2();\r\n" +
                    "   }\r\n" +

                    "   function CallAddQuotationTemplateItemAction(QuotationTemplateId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       LocationServiceId = document.getElementById('drpSelectLocationService').value;\r\n" +
                    "       HotelCategoryId = document.getElementById('drpSelectServiceTypeCategory').value;\r\n" +
                    "       Night = parseInt(document.getElementById('txtNight').value,10);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideAddQuotationTemplateItem(RenderInfo, QuotationTemplateId, LocationServiceId, HotelCategoryId, Night).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +


                    "   function CallSortForm(QuotationTemplateId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideDrawSortForm(RenderInfo, QuotationTemplateId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "   }\r\n" +




                    "   function CallAddSectionService(QuotationTemplateItemId,ServiceTypeId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationTemplateServiceProcess.ServerSideAddSectionService(RenderInfo, QuotationTemplateItemId, ServiceTypeId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(document.getElementById('imgicon'+QuotationTemplateItemId+ServiceTypeId)!=null)\r\n" +
                    "       {\r\n" +
                    "           document.getElementById('imgicon'+QuotationTemplateItemId+ServiceTypeId).style='opacity:1';\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divQuotationTemplateItemContent'+QuotationTemplateItemId).innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +


                    "   function RemoveQuotationTemplateSessionService(QuotationTemplateItemId, QuotationTemplateSectionServiceId)\r\n" +

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


                    "           AjaxOut = OneTSQ.WebParts.QuotationTemplateServiceProcess.ServerSideRemoveSectionService(RenderInfo, QuotationTemplateItemId, QuotationTemplateSectionServiceId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           document.getElementById('divQuotationTemplateItemContent'+QuotationTemplateItemId).innerHTML=AjaxOut.HtmlContent;\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "Nhóm dịch vụ được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +


                    "   }\r\n" +

                    "   function CallActionDeleteQuotationTemplateService(QuotationTemplateId, QuotationTemplateSectionServiceId, QuotationTemplateServiceId)\r\n" +
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


                    "           AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideDeleteQuotationTemplateService(RenderInfo, QuotationTemplateId, QuotationTemplateSectionServiceId, QuotationTemplateServiceId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           document.getElementById('divListQuotationTemplateSectionService'+QuotationTemplateSectionServiceId).innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "           document.getElementById('divMoneyStatiticInfo').innerHTML=AjaxOut.RetExtraParam1;\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "Dịch vụ được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +


                    "   }\r\n" +


                    "   function DeleteQuotationTemplateItem(QuotationTemplateId, QuotationTemplateItemId)\r\n" +

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


                    "           AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideDeleteQuotationTemplateItem(RenderInfo, QuotationTemplateId, QuotationTemplateItemId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "       }); \r\n" +


                    "   }\r\n" +


                    "   function CallBackFromLookUpForm()\r\n" +
                    "   {\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='block';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML='';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='none';\r\n" +
                    "   }\r\n" +



                    "   function AddPlusService(QuotationTemplateItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationTemplateServiceProcess.ServerSideDrawAddServiceToQuotationTemplateItemForm(RenderInfo, QuotationTemplateItemId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "   }\r\n" +

                    "   function CallLookupItineraryForm(QuotationTemplateItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationTemplateItemItineraryProcess.ServerSideDrawSearchItinerary(RenderInfo, QuotationTemplateItemId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "   }\r\n" +


                    "   function CallDoSearchItinerary(QuotationTemplateItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ActiveQuotationTemplateItemId = QuotationTemplateItemId;\r\n" +
                    "       document.getElementById('txtCurrentPageIndexItinerary').value='0';\r\n" +
                    "       document.getElementById('divProcessingSearchItinerary').innerHTML='" + WebLanguage.GetLanguageProcessing(OSiteParam) + "';\r\n" +
                    "       setTimeout('RealCallDoSearchItinerary(\"'+QuotationTemplateItemId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function NextPageSearchItinerary(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('txtCurrentPageIndexItinerary').value=PageIndex;\r\n" +
                    "       document.getElementById('divProcessingSearchItinerary').innerHTML='" + WebLanguage.GetLanguageProcessing(OSiteParam) + "';\r\n" +
                    "       setTimeout('RealCallDoSearchItinerary(\"'+ActiveQuotationTemplateItemId+'\")',10);\r\n" +
                    "   }\r\n" +


                    "   function RealCallDoSearchItinerary(QuotationTemplateItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       Keyword = document.getElementById('txtSearchKeywordItinerary').value;\r\n" +
                    "       CurrentPageIndex =  parseInt(document.getElementById('txtCurrentPageIndexItinerary').value,10);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationTemplateItemItineraryProcess.ServerSideDrawSearchItineraryResult(RenderInfo, QuotationTemplateItemId, Keyword, CurrentPageIndex).value;\r\n" +
                    "       document.getElementById('divProcessingSearchItinerary').innerHTML='';\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divSearchItineraryResultContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +


                    "   function CallPreviewQuotationTemplateItinerary(QuotationTemplateItemItineraryId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationTemplateItemItineraryProcess.ServerSidePreviewQuotationTemplateItemItinerary(RenderInfo, QuotationTemplateItemItineraryId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('ModalTitle').innerHTML=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       $('#divFormModal').modal();\r\n" +
                    "   }\r\n" +

                    "   function CallActionViewItinerary(ItineraryId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationTemplateItemItineraryProcess.ServerSidePreviewItinerary(RenderInfo, ItineraryId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('ModalTitle').innerHTML=AjaxOut.InfoMessage;\r\n" +
                    "       document.getElementById('divModalContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       $('#divFormModal').modal();\r\n" +
                    "   }\r\n" +


                    "   function RemoveItinerary(QuotationTemplateItemId, QuotationTemplateItemItineraryId)\r\n" +
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


                    "           AjaxOut = OneTSQ.WebParts.QuotationTemplateItemItineraryProcess.ServerSideRemoveItinerary(RenderInfo, QuotationTemplateItemId, QuotationTemplateItemItineraryId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           document.getElementById('divQuotationTemplateItemContent'+QuotationTemplateItemId).innerHTML=AjaxOut.HtmlContent;\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "Hành trình được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +


                    "   }\r\n" +



                    //"   function CallActionUpdateCustomerInfo(QuotationTemplateId)\r\n" +
                    //"   {\r\n"+
                    //"       RenderInfo=CreateRenderInfo();\r\n" +

                    //"       HotelCategoryId = document.getElementById('drpSelectServiceTypeCategory').value;\r\n" +

                    //"       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideSaveCustomerInfo(RenderInfo, QuotationTemplateId, HotelCategoryId).value;\r\n" +
                    //"       if(AjaxOut.Error)\r\n" +
                    //"       {\r\n" +
                    //"           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    //"           return;\r\n" +
                    //"       }\r\n" +
                    //"       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    //"   }\r\n"+

                    "   function CallActionUpdateExtraInfo(QuotationTemplateId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       HotelCategoryId = document.getElementById('drpSelectServiceTypeCategory').value;\r\n" +
                    "       Subject = document.getElementById('txtQuotationTemplateSubject').value;\r\n" +
                    "       Css = document.getElementById('txtCss').value;\r\n" +
                    "       Header = tinyMCE.get('txtHeader').getContent();\r\n" +
                    "       Footer = tinyMCE.get('txtFooter').getContent();\r\n" +
                    "       Include = tinyMCE.get('txtInclude').getContent();\r\n" +
                    "       Exclude = tinyMCE.get('txtExclude').getContent();\r\n" +
                    "       SpecialCondition = tinyMCE.get('txtSpecialCondition').getContent();\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepThree.ServerSideSaveExtraInfo(RenderInfo, QuotationTemplateId, HotelCategoryId, Subject, Header, Footer, Include, Exclude, SpecialCondition, Css).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "   }\r\n" +

                    "   function CallActionAddItineraryToQuotationTemplateItem(QuotationTemplateItemId, ItineraryId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.QuotationTemplateItemItineraryProcess.ServerSideAddItineraryToQuotationTemplateItem(RenderInfo, QuotationTemplateItemId, ItineraryId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divQuotationTemplateItemContent'+QuotationTemplateItemId).innerHTML = AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +




                    "   ActiveQuotationTemplateItemId=null;\r\n" +
                    "   ActiveQuotationTemplateSectionServiceId=null;\r\n" +

                    "   function CallLookupServiceForm(QuotationTemplateItemId, QuotationTemplateSectionServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ActiveQuotationTemplateItemId = QuotationTemplateItemId;\r\n" +
                    "       ActiveQuotationTemplateSectionServiceId = QuotationTemplateSectionServiceId;\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.QuotationTemplateServiceProcess.ServerSideDrawSearchService(RenderInfo, QuotationTemplateItemId, QuotationTemplateSectionServiceId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divRightInfoContent').style.display='none';\r\n" +
                    "       document.getElementById('divLookUpServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divLookUpServiceContent').style.display='block';\r\n" +
                    "       document.getElementById('txtSearchKeywordService').focus();\r\n" +
                    "       $('#drpSelectSupplier').select2();\r\n" +
                    //"       setTimeout('CallDoSearchService(\"'+QuotationTemplateItemId+'\",\"'+QuotationTemplateSectionServiceId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function NextPageSearchService(CurrentPageIndex)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('txtCurrentPageIndexService').value = CurrentPageIndex;\r\n" +
                    "       document.getElementById('divProcessingSearchService').innerHTML='" + WebLanguage.GetLanguageProcessing(OSiteParam) + "';\r\n" +
                    "       setTimeout('RealCallDoSearchService(\"'+ActiveQuotationTemplateItemId+'\",\"'+ActiveQuotationTemplateSectionServiceId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function CallDoSearchService(QuotationTemplateItemId, QuotationTemplateSectionServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('txtCurrentPageIndexService').value = '0';\r\n" +
                    "       document.getElementById('divProcessingSearchService').innerHTML='" + WebLanguage.GetLanguageProcessing(OSiteParam) + "';\r\n" +
                    "       setTimeout('RealCallDoSearchService(\"'+QuotationTemplateItemId+'\",\"'+QuotationTemplateSectionServiceId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function RealCallDoSearchService(QuotationTemplateItemId, QuotationTemplateSectionServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       SupplierId = document.getElementById('drpSelectSupplier').value;\r\n" +
                    "       Keyword = document.getElementById('txtSearchKeywordService').value;\r\n" +
                    "       CurrentPageIndex = parseInt(document.getElementById('txtCurrentPageIndexService').value,10);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationTemplateServiceProcess.ServerSideDrawSearchServiceResult(RenderInfo, QuotationTemplateItemId, QuotationTemplateSectionServiceId, SupplierId, Keyword, CurrentPageIndex).value;\r\n" +
                    "       document.getElementById('divProcessingSearchService').innerHTML='';\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divSearchServiceResultContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +



                    "   function CallActionAddServiceToQuotationTemplateItem(QuotationTemplateItemId, QuotationTemplateSectionServiceId, ServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationTemplateServiceProcess.ServerSideAddServiceToQuotationTemplateItem(RenderInfo, QuotationTemplateItemId, QuotationTemplateSectionServiceId, ServiceId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divQuotationTemplateItemContent'+QuotationTemplateItemId).innerHTML = AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divMoneyStatiticInfo').innerHTML = AjaxOut.RetExtraParam1;\r\n" +
                    "   }\r\n" +


                    "   function CallPreviewQuotationTemplateService(QuotationTemplateItemServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.QuotationTemplateServiceProcess.ServerSidePreviewQuotationTemplateItemService(RenderInfo, QuotationTemplateItemServiceId).value;\r\n" +
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

                     ServerSideDrawCreateQuotationTemplateStepThree(ORenderInfo, QuotationTemplateId, DayIndex).HtmlContent +

                    "<script>\r\n" +
                    "    $(document).ready(function(){ \r\n" +


                    " $(\".touchspin3\").TouchSpin({ \r\n" +
                    "    verticalbuttons: true, \r\n" +
                    "    buttondown_class: 'btn btn-white', \r\n" +
                    "    buttonup_class: 'btn btn-white' \r\n" +
                    " }); \r\n" +

                    " $('.CssDate').datepicker({\r\n" +
                    "   format: 'dd/mm/yyyy'\r\n" +
                    " });\r\n" +

                    "    }); \r\n" +
                    "</script>\r\n" +
                    "<style>\r\n" +
                    "   td{padding:1px !important;margin:0px;font-size:10px !important}\r\n" +
                    "   th{padding:1px !important;margin:0px;font-size:10px !important}\r\n" +
                    "   .form-control{font-size:10px;padding:1px; !important;height:20px!important}\r\n" +
                    "   li{padding:1px !important;margin:0px;font-size:10px !important}\r\n" +
                    "   .ibox-content{padding:3px !important;}\r\n" +
                    "   .ibox-title{padding:3px !important;min-height:30px!important; height:30px!important}\r\n" +
                    "</style>\r\n";



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
        public static AjaxOut ServerSideDrawCreateQuotationTemplateStepThree(RenderInfoCls ORenderInfo, string QuotationTemplateId, string DayIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                QuotationTemplateCls
                     OQuotationTemplate = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateModel(ORenderInfo, QuotationTemplateId);

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string BackUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new ProcessingQuotationTemplateList().WebPartId);
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
                    "        <h5>" + WebLanguage.GetLanguage(OSiteParam, "Xây dựng chào giá - Chọn dịch vụ") + "</h5> \r\n" +
                    "        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:window.open('" + BackUrl + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button>\r\n" +
                    //"        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallPreview('" + QuotationTemplateId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Xuất Html") + "</strong></button>\r\n" +
                    //"        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallCaculateMoney('" + QuotationTemplateId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tính giá") + "</strong></button>\r\n"+
                    //"        <button  style=\"margin-top:2px; float:right\" class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallMakeTemplate('" + QuotationTemplateId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tạo template") + "</strong></button>\r\n" +
                    "    </div> \r\n" +
                    "    <div id=\"divProcessingExport\" style=\"text-align:right;padding:4px; color:maroon;font-size:16px;font-weight:bold\"></div>\r\n"+
                    "    <div class=\"ibox-content\"> \r\n" +
                    "       <div><table><tr>  <td style=\"padding:3px !important;\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Số ngày") + ":</strong></td><td style=\"padding:3px !important;\"><span style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + OQuotationTemplate.NoOfDays.ToString("#,##0") + "</span></td></tr></table></div>\r\n" +
                    "       <div style=\"padding:3px !important;\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm hành trình") + "\" href=\"javascript:AddQuotationTemplateItemForm('" + QuotationTemplateId + "');\"><img src=\"images/plus.png\" style=\"padding-left:5px;height:16px\"/></a></div>\r\n"+

                    "       <div class=\"row\">\r\n" +
                    "           <div class=\"col-md-8 wrapper wrapper-content\" id=\"sortable-view\">\r\n" +
                                    ServerSideDrawList(ORenderInfo, QuotationTemplateId, int.Parse(DayIndex)).HtmlContent +
                    "           </div>\r\n" +
                    "           <div class=\"col-md-4 rightSidebar\">\r\n" +
                    "               <div id=\"divRightInfoContent\" >\r\n" +
                                        ServerSideDrawRightInfo(ORenderInfo, QuotationTemplateId).HtmlContent +
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
        public static AjaxOut ServerSideDrawList(RenderInfoCls ORenderInfo, string QuotationTemplateId, int DayIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                QuotationTemplateItemCls[]
                    QuotationTemplateItems = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().ReadingQuotationTemplateItems(ORenderInfo, QuotationTemplateId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                Html += "<table><tr>\r\n";
                for (int iIndex = 0; iIndex <= QuotationTemplateItems.Length; iIndex++)
                {
                    string Url=WebScreen.BuildUrl(OSiteParam,OwnerCode,new CreateQuotationTemplateStepThree().WebPartId,new WebParamCls[]
                    {
                        new WebParamCls("Id",QuotationTemplateId),
                        new WebParamCls("DayIndex",iIndex),
                    });
                    Html += "<td style=\"padding:2px\"><a href=\""+Url+"\" target=\"_self\">"+FunctionUtility.GetNumberImage(iIndex)+"</a></td>\r\n";
                }
                Html += "<td style=\"padding:2px\"><a href=\"javascript:CallSortForm('"+QuotationTemplateId+"');\" target=\"_self\"><img src=\"images/sort.png\" /></a></td>\r\n";
                Html += "</tr></table>\r\n";
                for (int iIndex = 0; iIndex < QuotationTemplateItems.Length; iIndex++)
                {
                    bool Allow = true;
                    if (DayIndex > 0)
                    {
                        if (DayIndex == QuotationTemplateItems[iIndex].DayIndex)
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
                            "<img  style=\"opacity:1\" onclick=\"javascript:CallLookupItineraryForm('" + QuotationTemplateItems[iIndex].QuotationTemplateItemId + "');\"  id=\"imgiconitinerary\"  title=\"Itinerary\"   class=\"icon-service\" src=\"images/icon-itinerary.png\" />";
                        string AppendStyle = "";
                        ServiceTypeCls[]
                            ItemServiceType = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().ReadingServiceType(ORenderInfo, QuotationTemplateItems[iIndex].QuotationTemplateItemId);

                        for (int iIndexServiceType = 0; iIndexServiceType < ItemServiceType.Length; iIndexServiceType++)
                        {
                            if (ItemServiceType[iIndexServiceType].AllowService == 1)
                            {
                                AppendStyle = "style=\"opacity:0.25\"";
                                if (ItemServiceType[iIndexServiceType].CheckService == 1)
                                {
                                    AppendStyle = "style=\"opacity:1\"";
                                }
                                DrawPlugInButtonText += "<img " + AppendStyle + " onclick=\"javascript:CallAddSectionService('" + QuotationTemplateItems[iIndex].QuotationTemplateItemId + "','" + ItemServiceType[iIndexServiceType].ServiceTypeId + "');\"  id=\"imgicon" + QuotationTemplateItems[iIndex].QuotationTemplateItemId + ItemServiceType[iIndexServiceType].ServiceTypeId + "\"  title=\"" + ItemServiceType[iIndexServiceType].ServiceTypeName + "\"   class=\"icon-service\" src=\"images/icon-" + ItemServiceType[iIndexServiceType].ServiceTypeId + ".png\" /> <input id=\"chk" + QuotationTemplateItems[iIndex].QuotationTemplateItemId + ItemServiceType[iIndexServiceType].ServiceTypeId + "\"    type=checkbox style=\"display:none\" " + (ItemServiceType[iIndexServiceType].CheckService == 1 ? "CHECKED" : "") + ">";
                            }
                        }

                        DrawPlugInButtonText +=
                            " <a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm dịch vụ vào hành  trình") + "\" href=\"javascript:AddPlusService('" + QuotationTemplateItems[iIndex].QuotationTemplateItemId + "');\"><img src=\"images/plus.png\" style=\"padding-left:5px;height:16px\"/></a>";

                        string AppendNight = "<td>" + WebLanguage.GetLanguage(OSiteParam, "Đêm") + ": " + QuotationTemplateItems[iIndex].Night.ToString("#,##0") + " <img style=\"height:16px\" src=\"images/night.png\"/></td>";
                        
                        Html +=
                            "    <div class=\"ibox \">\r\n" +
                            "        <div class=\"ibox-title\" style=\"background-color:#ada8a0;padding-top:0px;height:35px\">\r\n" +
                            "            <h5><table><tr><td>" + FunctionUtility.GetNumberImage((iIndex + 1)) + "</td><td style=\"width:2px;text-align:center\">|<td><td><span style=\"color:#337ab7;\">" + QuotationTemplateItems[iIndex].LocationServiceName + "</span><td><td>" + DrawPlugInButtonText + "</td> <td style=\"width:2px;text-align:center\">|<td>"+ AppendNight+"    <td> <a href=\"javascript:DeleteQuotationTemplateItem('" + QuotationTemplateId + "','" + QuotationTemplateItems[iIndex].QuotationTemplateItemId + "');\"><img style=\"height:16px\" src=\"/images/trash.png\"></a></td>  </tr></table></h5>\r\n" +
                            "        </div>\r\n" +
                            "        <div class=\"ibox-content\" id=\"divQuotationTemplateItemContent" + QuotationTemplateItems[iIndex].QuotationTemplateItemId + "\">\r\n" +
                                        ServerSideDrawQuotationTemplateItem(ORenderInfo, QuotationTemplateItems[iIndex].QuotationTemplateItemId).HtmlContent +
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
        public static AjaxOut ServerSideDrawRightInfo(RenderInfoCls ORenderInfo, string QuotationTemplateId)
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
                    "        <li class=\"active\"><a data-toggle=\"tab\" href=\"#tab-1\">" + WebLanguage.GetLanguage(OSiteParam,"Thông tin chào giá")+"</a></li> \r\n" +
                    //"        <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-2\">"+WebLanguage.GetLanguage(OSiteParam,"Khác")+"</a></li> \r\n" +
                    "    </ul> \r\n" +
                    "    <div class=\"tab-content\"> \r\n" +

                    "        <div id=\"tab-\" class=\"tab-pane active\"> \r\n" +
                    "            <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +
                                        ServerSideDrawGeneralInfo(ORenderInfo, QuotationTemplateId).HtmlContent+
                    "            </div> \r\n" +
                    "        </div> \r\n" +
                    //"        <div id=\"tab-2\" class=\"tab-pane\"> \r\n" +
                    //"            <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +
                    //                    ServerSideDrawExtraInfo(ORenderInfo, QuotationTemplateId).HtmlContent +
                    //"            </div> \r\n" +
                    //"        </div> \r\n" +
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
        public static AjaxOut ServerSideDrawGeneralInfo(RenderInfoCls ORenderInfo, string QuotationTemplateId)
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

                QuotationTemplateCls
                    OQuotationTemplate = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateModel(ORenderInfo, QuotationTemplateId);
                string SelectCategoryRoomText =
                     "<select id=\"drpSelectServiceTypeCategory\" class=\"form-control\">\r\n" +
                     "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n";
                for (int iIndex = 0; iIndex < ServiceTypeCategories.Length; iIndex++)
                {
                    if (OQuotationTemplate.frkHotelCategoryId.Equals(ServiceTypeCategories[iIndex].ServiceTypeCategoryId))
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
                           "                <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Loại khách sạn") + "</label> " + SelectCategoryRoomText + "</div> \r\n" +
                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề") + "</label> <input id=\"txtQuotationTemplateSubject\" class=\"form-control\" value=\"" + OQuotationTemplate.Subject + "\" ></div> \r\n" +
                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đầu trang") + "</label> <textarea id=\"txtHeader\">" + OQuotationTemplate.Header + "</textarea></div> \r\n" +

                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Bao gồm") + "</label> <textarea id=\"txtInclude\">" + OQuotationTemplate.Include + "</textarea></div> \r\n" +
                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Không bao gồm") + "</label> <textarea id=\"txtExclude\">" + OQuotationTemplate.Exclude + "</textarea></div> \r\n" +
                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt") + "</label> <textarea id=\"txtSpecialCondition\">" + OQuotationTemplate.SpecialCondition + "</textarea></div> \r\n" +

                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Cuối trang") + "</label> <textarea id=\"txtFooter\">" + OQuotationTemplate.Footer + "</textarea></div> \r\n" +
                           "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Css") + "</label> <textarea class=\"form-control\" id=\"txtCss\">" + OQuotationTemplate.Css + "</textarea></div> \r\n" +

                           "                 <div> \r\n" +
                           "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdateExtraInfo('" + QuotationTemplateId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật") + "</strong></button> \r\n" +
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



        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut ServerSideDrawExtraInfo(RenderInfoCls ORenderInfo, string QuotationTemplateId)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    string Html = "";
        //    try
        //    {
        //        SiteParam
        //            OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        QuotationTemplateCls
        //            OQuotationTemplate = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateModel(ORenderInfo, QuotationTemplateId);

        //        Html =
        //            " <div class=\"ibox-content\"> \r\n" +
        //            "     <div class=\"row\"> \r\n" +
        //            "         <div class=\"col-sm-12\">\r\n" +
        //            "             <div> \r\n" +
        //            "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề") + "</label> <input id=\"txtQuotationTemplateSubject\" class=\"form-control\" value=\"" + OQuotationTemplate.Subject + "\" ></div> \r\n" +
        //            "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đầu trang") + "</label> <textarea id=\"txtHeader\">" + OQuotationTemplate.Header + "</textarea></div> \r\n" +
                    
        //            "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Bao gồm") + "</label> <textarea id=\"txtInclude\">"+OQuotationTemplate.Include+"</textarea></div> \r\n" +
        //            "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Không bao gồm") + "</label> <textarea id=\"txtExclude\">" + OQuotationTemplate.Exclude + "</textarea></div> \r\n" +
        //            "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt")+ "</label> <textarea id=\"txtSpecialCondition\">" + OQuotationTemplate.SpecialCondition + "</textarea></div> \r\n" +

        //            "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Cuối trang") + "</label> <textarea id=\"txtFooter\">" + OQuotationTemplate.Footer + "</textarea></div> \r\n" +
        //            "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Css") + "</label> <textarea class=\"form-control\" id=\"txtCss\">" + OQuotationTemplate.Css + "</textarea></div> \r\n" +
        //            "                 <div> \r\n" +
        //            "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdateExtraInfo('" + QuotationTemplateId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật") + "</strong></button> \r\n" +
        //            "                 </div> \r\n" +
        //            "             </div> \r\n" +
        //            "         </div> \r\n" +
        //            "     </div> \r\n" +
        //            " </div> \r\n";

        //        Html = WebEnvironments.EncryptHtml(Html);


        //        RetAjaxOut.HtmlContent = Html;
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideSaveCustomerInfo(
            RenderInfoCls ORenderInfo, 
            string QuotationTemplateId,
            string HotelCategoryId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);


                QuotationTemplateCls
                    OQuotationTemplate = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateModel(ORenderInfo, QuotationTemplateId);

                OQuotationTemplate.NoOfDays = 1;
                OQuotationTemplate.frkHotelCategoryId = HotelCategoryId;

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().Save(ORenderInfo, QuotationTemplateId, OQuotationTemplate);
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
            string QuotationTemplateId,
            string HotelCategoryId,
            string Subject, 
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
                QuotationTemplateCls
                    OQuotationTemplate = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateModel(ORenderInfo, QuotationTemplateId);
                OQuotationTemplate.Subject = Subject;
                OQuotationTemplate.frkHotelCategoryId = HotelCategoryId;
                OQuotationTemplate.Header = Header;
                OQuotationTemplate.Footer = Footer;
                OQuotationTemplate.Include = Include;
                OQuotationTemplate.Exclude = Exclude;
                OQuotationTemplate.SpecialCondition = SpecialCondition;
                OQuotationTemplate.Css = Css;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().Save(ORenderInfo, QuotationTemplateId, OQuotationTemplate);
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
        public static AjaxOut ServerSideDeleteQuotationTemplateItem(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateId,
            string QuotationTemplateItemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().DeleteQuotationTemplateItem(ORenderInfo, QuotationTemplateItemId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationTemplateStepThree().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("Id",QuotationTemplateId),
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
        public static AjaxOut ServerSideAddQuotationTemplateItem(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateId, 
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

                QuotationTemplateItemCls
                    OQuotationTemplateItem = new QuotationTemplateItemCls();
                OQuotationTemplateItem.QuotationTemplateItemId = System.Guid.NewGuid().ToString();
                OQuotationTemplateItem.Night = Night;
                OQuotationTemplateItem.frkLocationServiceId = LocationServiceId;
                OQuotationTemplateItem.frkHotelCategoryId = HotelCategoryId;
                OQuotationTemplateItem.frkQuotationTemplateId = QuotationTemplateId;

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().AddQuotationTemplateItem(ORenderInfo, QuotationTemplateId, OQuotationTemplateItem);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationTemplateStepThree().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("Id",QuotationTemplateId),
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
        public static AjaxOut ServerSideDrawAddQuotationTemplateItemForm(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateId)
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
                    "                        <button onclick=\"javascript:CallAddQuotationTemplateItemAction('"+QuotationTemplateId+"');\" class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideDrawQuotationTemplateItem(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateItemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationTemplateItemCls
                    OQuotationTemplateItem = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateQuotationTemplateItemModel(ORenderInfo, QuotationTemplateItemId);

                QuotationTemplateItemItineraryCls[]
                    QuotationTemplateItemItineraries = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().ReadingQuotationTemplateItemItineraries(ORenderInfo, QuotationTemplateItemId);
                if (QuotationTemplateItemItineraries.Length > 0)
                {
                    Html +=
                        "<div style=\"height:30px;background-color:whitesmoke;font-size:10px\"><table><tr><td style=\"width:20px\"><img style=\"height:16px\" src=\"images/icon-itinerary.png\" /></td><td><h4 style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + WebLanguage.GetLanguage(OSiteParam, "Hành trình") + "</h4></td></tr></table></div>\r\n" +
                        "   <ul>\r\n";
                    for (int iIndex = 0; iIndex < QuotationTemplateItemItineraries.Length; iIndex++)
                    {
                        Html += "<li style=\"list-style-type:none;padding:1px\"><a href=\"javascript:CallPreviewQuotationTemplateItinerary('" + QuotationTemplateItemItineraries[iIndex].QuotationTemplateItemItineraryId + "');\"><img style=\"height:16px\" src=\"images/marker.png\"/></a> <a href=\"javascript:RemoveItinerary('" + QuotationTemplateItemId + "','" + QuotationTemplateItemItineraries[iIndex].QuotationTemplateItemItineraryId + "');\"><img style=\"height:16px\" src=\"/images/trash.png\"></a> <a href=\"javascript:CallPreviewQuotationTemplateItinerary('" + QuotationTemplateItemItineraries[iIndex].QuotationTemplateItemItineraryId + "');\">" + QuotationTemplateItemItineraries[iIndex].Subject + "</a></li>\r\n";
                    }
                    Html += "</ul>\r\n";
                }

                QuotationTemplateSectionServiceCls[]
                   QuotationTemplateSectionServices = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().ReadingQuotationTemplateSectionServices(ORenderInfo, QuotationTemplateItemId);
                if (QuotationTemplateSectionServices.Length > 0)
                {
                    for (int iIndex = 0; iIndex < QuotationTemplateSectionServices.Length; iIndex++)
                    {
                        Html +=
                            "<div style=\"height:25px;font-size:10px; background-color:whitesmoke;margin-bottom:2px\"><table><tr><td style=\"width:20px\"><img style=\"height:16px\" src=\"images/icon-" + QuotationTemplateSectionServices[iIndex].frkServiceTypeId + ".png\" /></td><td style=\"width:350px\"><h5 style=\"color:#337ab7;font-weight:bold;padding:1px !important\">" + QuotationTemplateSectionServices[iIndex].ServiceTypeName + "</h5></td> <td><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm chi tiết dịch vụ") + "\" href=\"javascript:CallLookupServiceForm('" + QuotationTemplateItemId + "','" + QuotationTemplateSectionServices[iIndex].QuotationTemplateSectionServiceId + "');\"><img src=\"images/plus.png\" style=\"padding-left:5px;height:16px\"/></a></td> <td><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa nhóm dịch vụ") + "\" href=\"javascript:RemoveQuotationTemplateSessionService('" + QuotationTemplateItemId + "','" + QuotationTemplateSectionServices[iIndex].QuotationTemplateSectionServiceId + "');\"><img src=\"images/remove.png\" style=\"padding-left:5px;height:16px\"/></a></td></tr></table></div>\r\n" +
                            "<div id=\"divListQuotationTemplateSectionService" + QuotationTemplateSectionServices[iIndex].QuotationTemplateSectionServiceId + "\">\r\n" +
                                ServerSideDrawQuotationTemplateService(ORenderInfo, QuotationTemplateSectionServices[iIndex].QuotationTemplateSectionServiceId).HtmlContent +
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
        public static AjaxOut ServerSideDeleteQuotationTemplateService(
            RenderInfoCls ORenderInfo, 
            string QuotationTemplateId,
            string QuotationTemplateSectionServiceId, 
            string QuotationTemplateServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().DeleteQuotationTemplateService(ORenderInfo, QuotationTemplateServiceId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.HtmlContent = ServerSideDrawQuotationTemplateService(ORenderInfo, QuotationTemplateSectionServiceId).HtmlContent;
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
        public static AjaxOut ServerSideSaveQuotationTemplateItemIndex(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateItemId,
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

                QuotationTemplateItemCls
                    OQuotationTemplateItem = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateQuotationTemplateItemModel(ORenderInfo, QuotationTemplateItemId);
                OQuotationTemplateItem.DayIndex = int.Parse(SortIndex);

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().SaveQuotationTemplateItem(ORenderInfo, QuotationTemplateItemId, OQuotationTemplateItem);
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
            string QuotationTemplateId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationTemplateItemCls[]
                    QuotationTemplateItems = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().ReadingQuotationTemplateItems(ORenderInfo, QuotationTemplateId);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string Url = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationTemplateStepThree().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("id",QuotationTemplateId)
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
                for (int iIndex = 0; iIndex < QuotationTemplateItems.Length; iIndex++)
                {
                    Html +=
                        "                 <tr id=\"tr" + QuotationTemplateItems[iIndex].QuotationTemplateItemId + "\"> \r\n" +
                        "                     <td class=\"td-center\" style=\"width:40px\"><input onblur=\"javascript:SaveUpdateQuotationTemplateIndexItem('" + QuotationTemplateItems[iIndex].QuotationTemplateItemId + "');\" class=\"form-control\" style=\"text-align:right; width:40px;background-color:lightyellow\" id=\"txtSortIndexQuotationTemplateItem" + QuotationTemplateItems[iIndex].QuotationTemplateItemId + "\" value=\"" + QuotationTemplateItems[iIndex].DayIndex.ToString("#,##0") + "\"></td> \r\n" +
                        "                     <td style=\"width:180px\">" + QuotationTemplateItems[iIndex].LocationServiceName + "</td> \r\n" +
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
            string QuotationTemplateId)
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
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên mẫu") + "</label> <input id=\"txtQuotationTemplateTemplateName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên mẫu") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionSaveTemplate('"+QuotationTemplateId+"');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideDrawQuotationTemplateService(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateServiceSectionId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationTemplateServiceCls[]
                    QuotationTemplateServices = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().ReadingQuotationTemplateServices(ORenderInfo, QuotationTemplateServiceSectionId);
                QuotationTemplateSectionServiceCls
                    OQuotationTemplateSectionService = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateQuotationTemplateSectionServiceModel(ORenderInfo, QuotationTemplateServiceSectionId);
                QuotationTemplateItemCls
                    QuotationTemplateItem = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateQuotationTemplateItemModel(ORenderInfo, OQuotationTemplateSectionService.frkQuotationTemplateItemId);
                string QuotationTemplateId = QuotationTemplateItem.frkQuotationTemplateId;

                Html =
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "D.vụ") + " </th> \r\n" +
                        "                     <th style=\"width:100px\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < QuotationTemplateServices.Length; iIndex++)
                {
                    Html +=
                        "                 <tr id=\"tr" + QuotationTemplateServices[iIndex].QuotationTemplateServiceId + "\"> \r\n" +
                        "                     <td style=\"width:180px\">" + QuotationTemplateServices[iIndex].ServiceName + "</td> \r\n" +
                        "                     <td style=\"width:20px\" class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "\" href=\"javascript:CallActionDeleteQuotationTemplateService('" + QuotationTemplateId + "','" + QuotationTemplateServices[iIndex].frkQuotationTemplateSectionServiceId + "','" + QuotationTemplateServices[iIndex].QuotationTemplateServiceId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
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


        
        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut ServerSideCallPreview(
        //    RenderInfoCls ORenderInfo,
        //    string QuotationTemplateId)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        RetAjaxOut.RetUrl = "preview.aspx?sv=QuotationTemplate&id=" + QuotationTemplateId;
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}

        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut ServerSideCallExportPdf(
        //    RenderInfoCls ORenderInfo,
        //    string QuotationTemplateId)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam
        //            OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSession.CheckSessionTimeOut(ORenderInfo);
        //        RetAjaxOut = QuotationTemplatePreviewUtility.Preview(ORenderInfo,QuotationTemplateId,"pdf");
        //        if (RetAjaxOut.Error) throw new Exception(RetAjaxOut.InfoMessage);
        //        byte[] Bytes = FunctionUtility.GeneratePdfFromFile(RetAjaxOut.RetUrl);
        //        MediaInfoCls
        //            OMediaInfo = new MediaInfoCls();
        //        OMediaInfo.LoginName = "system";
        //        OMediaInfo.MediaInfoId = QuotationTemplateId;
        //        OMediaInfo.Month = System.DateTime.Now.Month;
        //        OMediaInfo.Year = System.DateTime.Now.Year;
        //        OMediaInfo.Overwrite = true;
        //        OMediaInfo.Section = "QuotationTemplate";
        //        OMediaInfo.SiteId = ORenderInfo.SiteId;
        //        OMediaInfo.UploadFileName = QuotationTemplateId + ".pdf";

        //        RetAjaxOut = CallTempServiceUtility.UploadTemp(OMediaInfo, Bytes);
        //        RetAjaxOut.HtmlContent = WebLanguage.GetLanguage(OSiteParam,"Hoàn thành xuất Pdf")+" -> "+"<a target=\"_blank\" href=\"" + RetAjaxOut.RetUrl + "\">" + WebLanguage.GetLanguage(OSiteParam, "Mở pdf") + "</a>\r\n";
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}

      

    }
}
