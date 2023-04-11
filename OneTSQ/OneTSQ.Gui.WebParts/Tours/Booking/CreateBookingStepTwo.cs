using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.Utility;

namespace OneTSQ.WebParts
{
    public class CreateBookingStepTwo : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "CreateBookingStepTwo";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Xây dựng chào giá - Chọn các điểm dịch vụ";
            }
        }

        public override string Description
        {
            get
            {
                return "Xây dựng chào giá - Chọn các điểm dịch vụ";
            }
        }


        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(CreateBookingStepTwo),Page);
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
                RetAjaxOut.HtmlContent =
                    "<input id=\"txtBookingId\" type=\"hidden\" value=\"" + BookingId + "\">\r\n" +
                    "<script>\r\n" +
                    "   function CallProcessStepTwo(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepTwo.ServerSideProcess(RenderInfo, BookingId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function ProcessCheckService(BookingItemId, ServiceTypeId)\r\n"+
                    "   {\r\n"+
                    "       ORenderInfo = CreateRenderInfo();\r\n"+
                    "       checked = !document.getElementById('chk'+BookingItemId+ServiceTypeId).checked;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepTwo.ServerSideProcessCheckService(ORenderInfo, BookingItemId, ServiceTypeId, checked).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n"+
                    "       {\r\n"+
                    "           sweetAlert(AjaxOut.InfoMessage);\r\n"+
                    "           return;\r\n"+
                    "       }\r\n"+
                    "       document.getElementById('chk'+BookingItemId+ServiceTypeId).checked=checked;\r\n" +
                    "       AppendStyle = 'opacity:0.25\';\r\n"+
                    "       if(checked)\r\n"+
                    "       {\r\n"+
                    "           AppendStyle='opacity:1';\r\n" +
                    "       }\r\n"+
                    "       document.getElementById('imgicon'+BookingItemId+ServiceTypeId).style = AppendStyle;\r\n" +
                    "   }\r\n"+

                    "   function AddRow(BookingId)\r\n" +
                    "   {\r\n" +
                    "       ORenderInfo = CreateRenderInfo();\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepTwo.ServerSideProcessAddRow(ORenderInfo, BookingId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           sweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divListServices').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "       $('.SelectList').select2();\r\n" +
                    "   }\r\n" +

                    "   function RemoveRow(BookingId, BookingItemId)\r\n" +
                    "   {\r\n" +
                    "       ORenderInfo = CreateRenderInfo();\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepTwo.ServerSideRemoveRow(ORenderInfo, BookingId, BookingItemId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           sweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divListServices').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "       $('.SelectList').select2();\r\n" +
                    "   }\r\n" +

                    "   function SaveNight(BookingItemId)\r\n"+
                    "   {\r\n" +
                    "       ORenderInfo = CreateRenderInfo();\r\n" +
                    "       Night = document.getElementById('txtNight'+BookingItemId).value;\r\n"+
                    "       oldNight = document.getElementById('spanNight'+BookingItemId).innerHTML;\r\n" +
                    "       if(Night==oldNight)return;\r\n"+
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepTwo.ServerSideSaveNight(ORenderInfo, BookingItemId, Night).value;\r\n" +
                    "       document.getElementById('spanNight'+BookingItemId).innerHTML = Night;\r\n"+
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           sweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +

                    "   function SaveLocationService(BookingItemId)\r\n"+
                    "   {\r\n" +
                    "       ORenderInfo = CreateRenderInfo();\r\n" +
                    "       LocationServiceId = document.getElementById('drpSelectLocationService'+BookingItemId).value;\r\n"+
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepTwo.ServerSideSaveLocationService(ORenderInfo, BookingItemId, LocationServiceId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           sweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +

                    "   function SaveHotelCategory(BookingItemId)\r\n" +
                    "   {\r\n" +
                    "       ORenderInfo = CreateRenderInfo();\r\n" +
                    "       HotelCategoryId = document.getElementById('drpSelectHotelCategory'+BookingItemId).value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CreateBookingStepTwo.ServerSideSaveHotelCategory(ORenderInfo, BookingItemId, HotelCategoryId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           sweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +

                    "</script>\r\n" +
                    
                    ServerSideDrawCreateBookingStepTwo(ORenderInfo).HtmlContent +

                    "<script>\r\n" +
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
        public static AjaxOut ServerSideDrawCreateBookingStepTwo(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string BookingId=(string)WebEnvironments.Request("Id");
                
                Html =
                    "<div class=\"ibox float-e-margins\"> \r\n" +
                    "    <div class=\"ibox-title\"> \r\n" +
                    "        <h5>" + WebLanguage.GetLanguage(OSiteParam, "Xây dựng chào giá - chọn các điểm dịch vụ") + "</h5> \r\n" +
                    "    </div> \r\n" +
                    "    <div class=\"ibox-content\"> \r\n" +
                    "        <div style=\"text-align:right\"><button  onclick=\"javascript:CallProcessStepTwo('" + BookingId + "');\" class=\"btn btn-sm btn-primary pull-right m-t-n-xs\" type=\"button\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tiếp tục") + "</strong></button></div>\r\n" +
                    "        <div class=\"row\" id=\"divListServices\" style=\"clear:both\"> \r\n" +
                                ServerSideDrawList(ORenderInfo, BookingId).HtmlContent +
                    "        </div> \r\n" +
                    "    </div> \r\n" +
                    "</div> \r\n" +

                    "<script>\r\n" +
                    "   $('.SelectList').select2();\r\n"+
                    "</script>\r\n";

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
        public static AjaxOut ServerSideDrawList(RenderInfoCls ORenderInfo, string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                BookingItemCls[]
                    BookingItems = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().ReadingBookingItems(ORenderInfo, BookingId);
                ServiceTypeCategoryFilterCls
                     OServiceTypeCategoryFilter = new ServiceTypeCategoryFilterCls();
                OServiceTypeCategoryFilter.ActiveOnly = 1;
                ServiceTypeCategoryCls[]
                    ServiceTypeCategories = CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeCategoryProcess().Reading(ORenderInfo, OServiceTypeCategoryFilter);

                CityFilterCls
                     OCityFilter = new CityFilterCls();
                OCityFilter.ActiveOnly = 1;
                CityCls[]
                    Cities = CallBussinessUtility.CreateBussinessProcess().CreateCityProcess().Reading(ORenderInfo, OCityFilter);

                
                Html+=
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr style=\"background-color:whitesmoke\"> \r\n" +
                        "                     <th style=\"width:220px\">" + WebLanguage.GetLanguage(OSiteParam, "Điểm đến hoặc dịch vụ") + " </th> \r\n" +
                        "                     <th style=\"width:70px\">" + WebLanguage.GetLanguage(OSiteParam, "Đêm") + " </th> \r\n" +
                        "                     <th style=\"width:220px\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Loại khách sạn") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < BookingItems.Length; iIndex++)
                {
                    string SelectCategoryRoomText =
                        "<select onchange=\"javascript:SaveHotelCategory('" + BookingItems[iIndex].BookingItemId + "');\"  style=\"width:100%\" id=\"drpSelectHotelCategory" + BookingItems[iIndex].BookingItemId + "\" class=\"form-control select2 SelectList\">\r\n" +
                        "   <option value=\"\"></option>\r\n";
                    for (int iCat = 0; iCat < ServiceTypeCategories.Length; iCat++)
                    {
                        if (ServiceTypeCategories[iCat].ServiceTypeCategoryId.Equals(BookingItems[iIndex].frkHotelCategoryId))
                        {
                            SelectCategoryRoomText +=
                                "   <option selected value=\"" + ServiceTypeCategories[iCat].ServiceTypeCategoryId + "\">" + ServiceTypeCategories[iCat].ServiceTypeCategoryName + "</option>\r\n";
                        }
                        else
                        {
                            SelectCategoryRoomText +=
                                "   <option value=\"" + ServiceTypeCategories[iCat].ServiceTypeCategoryId + "\">" + ServiceTypeCategories[iCat].ServiceTypeCategoryName + "</option>\r\n";
                        }
                    }
                    SelectCategoryRoomText +=
                        "</select>\r\n";

                    string SelectCityText =
                        "<select onchange=\"javascript:SaveLocationService('" + BookingItems[iIndex].BookingItemId + "');\" style=\"width:100%\" id=\"drpSelectLocationService" + BookingItems[iIndex].BookingItemId + "\" class=\"form-control select2 SelectList\">\r\n" +
                        "   <option value=\"\">???</option>\r\n";
                    for (int iCat = 0; iCat < Cities.Length; iCat++)
                    {
                        if (Cities[iCat].CityId.Equals(BookingItems[iIndex].frkLocationServiceId))
                        {
                            SelectCityText +=
                                "   <option selected value=\"" + Cities[iCat].CityId + "\">" + Cities[iCat].CityName + "</option>\r\n";
                        }
                        else
                        {
                            SelectCityText +=
                                "   <option value=\"" + Cities[iCat].CityId + "\">" + Cities[iCat].CityName + "</option>\r\n";
                        }
                    }
                    SelectCityText +=
                        "</select>\r\n";

                    string DrawPlugInButtonText = "";
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
                            DrawPlugInButtonText += "<img " + AppendStyle + " onclick=\"javascript:ProcessCheckService('" + BookingItems[iIndex].BookingItemId + "','" + ItemServiceType[iIndexServiceType].ServiceTypeId + "');\"  id=\"imgicon" + BookingItems[iIndex].BookingItemId + ItemServiceType[iIndexServiceType].ServiceTypeId + "\"  title=\"" + ItemServiceType[iIndexServiceType].ServiceTypeName + "\"   class=\"icon-service\" src=\"images/icon-" + ItemServiceType[iIndexServiceType].ServiceTypeId + ".png\" /> <input id=\"chk" + BookingItems[iIndex].BookingItemId+ItemServiceType[iIndexServiceType].ServiceTypeId + "\"    type=checkbox style=\"display:none\" " + (ItemServiceType[iIndexServiceType].CheckService == 1 ? "CHECKED" : "") + ">";
                        }
                    }
                    Html +=
                        "                 <tr> \r\n" +
                        "                     <td>" + SelectCityText + "</td> \r\n" +
                        "                     <td><span id=\"spanNight" + BookingItems[iIndex].BookingItemId + "\" style=\"display:none\">" + BookingItems[iIndex].Night.ToString("###0") + "</span><input onblur=\"javascript:SaveNight('" + BookingItems[iIndex].BookingItemId + "');\" style=\"text-align:center\" id=\"txtNight" + BookingItems[iIndex].BookingItemId + "\" class=\"form-control\" style=\"background-color:lightyellow;padding:1px !important\" value=\"" + BookingItems[iIndex].Night.ToString("###0") + "\"></td> \r\n" +
                        "                     <td  style=\"text-align:center\">" + DrawPlugInButtonText + "</td>\r\n" +
                        "                     <td>" + SelectCategoryRoomText + "</td> \r\n" +
                        "                     <td><button style=\"margin-top:3px\" onclick=\"javascript:RemoveRow('" + BookingId+"','"+ BookingItems[iIndex].BookingItemId + "');\" class=\"btn btn-sm btn-primary pull-right m-t-n-xs\" type=\"button\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "-") + "</strong></button></td>\r\n" +
                        "                     <td></td>\r\n"+
                        "                 </tr> \r\n";
                }
                Html +=
                      "                 <tr> \r\n" +
                      "                     <td colspan=6 style=\"text-align:left\">\r\n"+
                      
                      "                     </td> \r\n" +
                      "                      <td><button  style=\"margin-top:10px\" onclick=\"javascript:AddRow('" + BookingId + "');\" class=\"btn btn-sm btn-primary pull-right m-t-n-xs\" type=\"button\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "+") + "</strong></button></td>\r\n" +
                      "                 </tr> \r\n";
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "   </div>\r\n";


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
        public static AjaxOut ServerSideProcessCheckService(
            RenderInfoCls ORenderInfo,
            string BookingItemId,
            string ServiceTypeId,
            bool   Checked)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().SetCheckServiceType(ORenderInfo, BookingItemId, ServiceTypeId, Checked);
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
        public static AjaxOut ServerSideProcessAddRow(
            RenderInfoCls ORenderInfo,
            string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                BookingItemCls
                    OBookingItem = new BookingItemCls();
                OBookingItem.BookingItemId = System.Guid.NewGuid().ToString();
                OBookingItem.frkBookingId = BookingId;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().AddBookingItem(ORenderInfo, BookingId, OBookingItem);
                RetAjaxOut.HtmlContent = ServerSideDrawList(ORenderInfo, BookingId).HtmlContent;
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
        public static AjaxOut ServerSideRemoveRow(
            RenderInfoCls ORenderInfo,
            string BookingId,
            string BookingItemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().DeleteBookingItem(ORenderInfo, BookingItemId);
                RetAjaxOut.HtmlContent = ServerSideDrawList(ORenderInfo, BookingId).HtmlContent;
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
        public static AjaxOut ServerSideSaveNight(RenderInfoCls ORenderInfo, string BookingItemId, string Night)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                if (FunctionUtility.checkInteger(Night) == false) throw new Exception("Số đêm không hợp lệ");
                BookingItemCls 
                    OBookingItem = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingItemModel(ORenderInfo, BookingItemId);
                OBookingItem.Night = int.Parse(Night);
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
        public static AjaxOut ServerSideSaveLocationService(RenderInfoCls ORenderInfo, string BookingItemId, string LocationServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                BookingItemCls
                    OBookingItem = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingItemModel(ORenderInfo, BookingItemId);
                OBookingItem.frkLocationServiceId = LocationServiceId;
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
        public static AjaxOut ServerSideSaveHotelCategory(RenderInfoCls ORenderInfo, string BookingItemId, string HotelCategoryId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                BookingItemCls
                    OBookingItem = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateBookingItemModel(ORenderInfo, BookingItemId);
                OBookingItem.frkHotelCategoryId = HotelCategoryId;
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
        public static AjaxOut ServerSideProcess(RenderInfoCls ORenderInfo, string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().ResetBookingItemBlank(ORenderInfo, BookingId);
                
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateBookingStepThree().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Id", BookingId)
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
