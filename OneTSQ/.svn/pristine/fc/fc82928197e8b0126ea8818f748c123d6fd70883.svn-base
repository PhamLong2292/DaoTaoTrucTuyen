using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebParts
{
    public class UpdateService : WebPartTemplate
    {
        class RangePriceCls
        {
            public string Subject;
            public int Value;

            public RangePriceCls()
            {
            }

            public RangePriceCls(string _Subject,int _Value)
            {
                Subject = _Subject;
                Value = _Value;
            }
        }
        public override string WebPartId
        {
            get
            {
                return "UpdateService";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Sửa chữa dịch vụ";
            }
        }

        public override string Description
        {
            get
            {
                return "Sửa chữa dịch vụ";
            }
        }

        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { new Service().WebPartId };
            }
        }
        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(UpdateService),Page);
        }

        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true;// WebPermissionUtility.CheckPermission(OSiteParam, DictionaryPermission.StaticPermissionFunctionId, "Access", DictionaryPermission.StaticPermissionFunctionCode, DictionaryPermission.StaticPermissionFunctionId, UserId, false);
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
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

                string OwnerCode=(string)WebEnvironments.Request("OwnerCode");
                string ServiceId = (string)WebEnvironments.Request("ServiceId");
                ServiceCls
                 OService = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo, ServiceId);
                if (OService == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "dịch vụ đã bị xóa hoặc không tìm thấy"));
                }

                string BackUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new Service().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("ServiceTypeId",OService.frkServiceTypeId),
                    new WebParamCls("AssetSupplierId",OService.frkAssetSupplierId)
                });
                RetAjaxOut.HtmlContent =
                    "<script>\r\n" +
                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                   "        window.open('" + BackUrl + "','_self');\r\n" +
                    "   }\r\n" +

                    "   function CallActionDeleteRangeServicePrice(RangeSalePriceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ServiceId='" + ServiceId + "';\r\n" +
                    "       ServiceSessionId = document.getElementById('drpSelectServiceSession').value;\r\n" +


                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa giá theo mùa này ra khỏi hệ thống") + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +


                    "       AjaxOut = OneTSQ.WebParts.UpdateService.ServerSideDeleteRangePrice(RenderInfo, RangeSalePriceId, ServiceId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divListRangePriceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "giá theo mùa đã được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +


                    "   }\r\n" +

                    "   function CallActionAddRangeServicePrice()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ServiceId='" + ServiceId + "';\r\n" +

                    "       StartDate = document.getElementById('txtStartDate').value;\r\n" +
                    "       EndDate = document.getElementById('txtEndDate').value;\r\n" +

                    "       StartTime = document.getElementById('txtStartTime').value;\r\n" +
                    "       EndTime = document.getElementById('txtEndTime').value;\r\n" +

                    "       FromPax = document.getElementById('txtFromPax').value;\r\n" +
                    "       ToPax = document.getElementById('txtToPax').value;\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.UpdateService.ServerSideAddRangePrice(RenderInfo, ServiceId, StartDate, EndDate, StartTime, EndTime, FromPax, ToPax).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divListRangePriceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +


                    ///////////////
                    "   function CallActionAddShared()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ServiceId='" + ServiceId + "';\r\n" +
                    "       OwnerId = document.getElementById('drpSelectOwner').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.UpdateService.ServerSideAddShared(RenderInfo, ServiceId, OwnerId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divListSharedContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +


                    "   function CallActionDeleteServiceSharedOwner(ServiceSharedOwnerId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ServiceId='" + ServiceId + "';\r\n" +

                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa chia sẻ đại lý này ra khỏi hệ thống") + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +


                    "           AjaxOut = OneTSQ.WebParts.UpdateService.ServerSideDeleteServiceSharedOwner(RenderInfo, ServiceSharedOwnerId, ServiceId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           document.getElementById('divListSharedContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "Chia sẻ cho đại lý đã được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +



                    "   }\r\n" +
                    ////////////////

                    "   function CallActionAdd(ServiceTypeId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.UpdateService.ServerSideAdd(RenderInfo, ServiceTypeId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function UpdateSalePrice(RangeServicePriceId, ShowInfo)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       SalePrice = document.getElementById('txtSalePrice'+RangeServicePriceId).value;\r\n" +
                    "       document.getElementById('divProcessingUpdate').innerHTML='';\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.UpdateService.ServerSideUpdateSalePrice(RenderInfo, RangeServicePriceId, SalePrice).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           if(ShowInfo)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           }\r\n" +
                    "           else\r\n" +
                    "           {\r\n" +
                    "               document.getElementById('divProcessingUpdate').innerHTML=AjaxOut.InfoMessage;\r\n" +
                    "           }\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(ShowInfo)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +

                    "   function UpdateWebPrice(RangeServicePriceId, ShowInfo)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       WebPrice = document.getElementById('txtWebPrice'+RangeServicePriceId).value;\r\n" +
                    "       document.getElementById('divProcessingUpdate').innerHTML='';\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.UpdateService.ServerSideUpdateWebPrice(RenderInfo, RangeServicePriceId, WebPrice).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           if(ShowInfo)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           }\r\n" +
                    "           else\r\n" +
                    "           {\r\n" +
                    "               document.getElementById('divProcessingUpdate').innerHTML=AjaxOut.InfoMessage;\r\n" +
                    "           }\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(ShowInfo)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +


                    "   function UpdateSharedMarkupPercent(ServiceSharedOwnerId, ShowInfo)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       MarkUpPercent = document.getElementById('txtServiceSharedOwnerMarkUpPercent'+ServiceSharedOwnerId).value;\r\n" +
                    "       document.getElementById('divProcessingUpdate').innerHTML='';\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.UpdateService.ServerSideUpdateServiceSharedOwner(RenderInfo, ServiceSharedOwnerId, MarkUpPercent).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           if(ShowInfo)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           }\r\n" +
                    "           else\r\n" +
                    "           {\r\n" +
                    "               document.getElementById('divProcessingUpdateShared').innerHTML=AjaxOut.InfoMessage;\r\n" +
                    "           }\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(ShowInfo)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +


                    "   function CallActionUpdate(ServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       AssetSupplierId= document.getElementById('drpSelectAssetSupplier').value;\r\n" +
                    "       StdServiceId= document.getElementById('drpSelectStdService').value;\r\n" +
                    "       ServiceCode = document.getElementById('txtServiceCode').value;\r\n" +
                    "       ServiceName = document.getElementById('txtServiceName').value;\r\n" +
                    "       ShortDescription = document.getElementById('txtShortDescription').value;\r\n" +
                    "       Description = document.getElementById('txtDescription').value;\r\n" +
                    "       UnitId = document.getElementById('drpSelectUnit').value;\r\n" +
                    "       CurrencyId = document.getElementById('drpSelectCurrency').value;\r\n" +

                    "       BuyPrice = document.getElementById('txtBuyPrice').value;\r\n" +
                    "       SalePrice = document.getElementById('txtSalePrice').value;\r\n" +
                    "       WebPrice = document.getElementById('txtWebPrice').value;\r\n" +
                    //"       PriceType= document.getElementById('drpSelectPriceType').value;\r\n" +

                    "       SeoTitle = document.getElementById('txtSeoTitle').value;\r\n" +
                    "       SeoKeyword = document.getElementById('txtSeoKeyword').value;\r\n" +
                    "       SeoDescription = document.getElementById('txtSeoDescription').value;\r\n" +
                    "       SeoAuthor = document.getElementById('txtSeoAuthor').value;\r\n" +


                    "       ServiceToolTips = document.getElementById('txtServiceToolTips').value;\r\n" +
                    "       SpecialConditions = document.getElementById('txtSpecialConditions').value;\r\n" +

                    "       SharedScope= parseInt(document.getElementById('drpSharedScope').value,10);\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +

                    "       AjaxOutService = OneTSQ.WebParts.UpdateService.ServerSideCreateServiceObject(RenderInfo, ServiceId, ServiceTypeExtraInfoIds).value;\r\n" +
                    "       OSaveService = AjaxOutService.RetObject;\r\n" +
                    "       ServiceTypeExtraInfos = AjaxOutService.RetObject1;\r\n"+

                    "       OSaveService.ServiceCode  = ServiceCode;\r\n" +
                    "       OSaveService.ServiceName  = ServiceName;\r\n" +
                    "       OSaveService.frkAssetSupplierId  = AssetSupplierId;\r\n" +
                    "       OSaveService.frkStdServiceId = StdServiceId;\r\n" +
                    "       OSaveService.ShortDescription  = ShortDescription;\r\n" +
                    "       OSaveService.Description  = Description;\r\n" +
                    "       OSaveService.frkUnitId  = UnitId;\r\n" +
                    "       OSaveService.frkCurrencyId = CurrencyId;\r\n" +
                    "       OSaveService.BuyPrice  = BuyPrice;\r\n" +
                    "       OSaveService.SalePrice  = SalePrice;\r\n" +
                    "       OSaveService.WebPrice  = WebPrice;\r\n" +
                    //"       OSaveService.PriceType  = PriceType;\r\n" +

                    "       OSaveService.SharedScope  = SharedScope;\r\n" +
                    "       OSaveService.SeoTitle  = SeoTitle;\r\n" +
                    "       OSaveService.SeoDescription  = SeoDescription;\r\n" +
                    "       OSaveService.SeoAuthor  = SeoAuthor;\r\n" +
                    "       OSaveService.SeoKeyword  = SeoKeyword;\r\n" +
                    "       OSaveService.ServiceToolTips  = ServiceToolTips;\r\n" +
                    "       OSaveService.SpecialConditions  = SpecialConditions;\r\n" +
                    "       OSaveService.Active = Active;\r\n" +

                    
                    "       for(var iIndex=0;iIndex<ServiceTypeExtraInfos.length;iIndex++)\r\n"+
                    "       {\r\n"+
                    "           Id='txtExtraInfo'+ServiceTypeExtraInfos[iIndex].ServiceTypeExtraInfoId;\r\n" +
                    "           Content = tinyMCE.get(Id).getContent();\r\n" +
                    "           ServiceTypeExtraInfos[iIndex].Content = Content;\r\n"+
                    "       }\r\n"+

                    "       AjaxOut = OneTSQ.WebParts.UpdateService.ServerSideUpdate(RenderInfo, ServiceId, OSaveService, ServiceTypeExtraInfos).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "   }\r\n" +

                    //"   function LoadingExtraAttriuteContent()\r\n"+
                    //"   {\r\n"+
                    //"       RenderInfo=CreateRenderInfo();\r\n" +

                    //"   }\r\n"+

                    "</script>\r\n" +
                    ServerSideDrawUpdateForm(ORenderInfo, OService).HtmlContent +
                    "<script>\r\n" +
                    "   $('#drpSelectStdService').select2();\r\n" +
                    "   $('#drpSelectAssetSupplier').select2();\r\n" +
                    //"   $('#drpSelectUnit').select2();\r\n" +
                    //"   $('#drpSelectCurrency').select2();\r\n" +
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
        public static AjaxOut ServerSideDrawUpdateForm(RenderInfoCls ORenderInfo, ServiceCls OService)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                
             
                string SelectActiveText =
                     "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                     "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                     "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                     "</select>\r\n";

                if (OService.Active == 1)
                {
                    SelectActiveText =
                         "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                         "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                         "   <option selected value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                         "</select>\r\n";
                }

                

                StdServiceFilterCls
                  OStdServiceFilter = new StdServiceFilterCls();
                OStdServiceFilter.ActiveOnly = 1;
                OStdServiceFilter.ServiceTypeId = OService.frkServiceTypeId;
                StdServiceCls[]
                    StdServices = CallBussinessUtility.CreateBussinessProcess().CreateStdServiceProcess().Reading(ORenderInfo, OStdServiceFilter);
                string SelectServiceText =
                    "<select id=\"drpSelectStdService\" class=\"form-control select2\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn dịch vụ chuẩn") + "</option>\r\n";
                for (int iIndex = 0; iIndex < StdServices.Length; iIndex++)
                {
                    if (StdServices[iIndex].StdServiceId.Equals(OService.frkStdServiceId))
                    {
                        SelectServiceText += "    <option selected value=\"" + StdServices[iIndex].StdServiceId + "\">" + StdServices[iIndex].StdServiceName + "</option>\r\n";
                    }
                    else
                    {
                        SelectServiceText += "    <option value=\"" + StdServices[iIndex].StdServiceId + "\">" + StdServices[iIndex].StdServiceName + "</option>\r\n";
                    }
                }
                SelectServiceText += "</select>\r\n";



                CurrencyFilterCls
                  OCurrencyFilter = new CurrencyFilterCls();
                OCurrencyFilter.ActiveOnly = 1;
                
                CurrencyCls[]
                    Currencies = CallBussinessUtility.CreateBussinessProcess().CreateCurrencyProcess().Reading(ORenderInfo, OCurrencyFilter);
                string SelectCurrencyText =
                    "<select id=\"drpSelectCurrency\" class=\"form-control select2\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn loại tiền") + "</option>\r\n";
                for (int iIndex = 0; iIndex < Currencies.Length; iIndex++)
                {
                    if (Currencies[iIndex].CurrencyId.Equals(OService.frkCurrencyId))
                    {
                        SelectCurrencyText += "    <option selected value=\"" + Currencies[iIndex].CurrencyId + "\">" + Currencies[iIndex].CurrencyName + "</option>\r\n";
                    }
                    else
                    {
                        SelectCurrencyText += "    <option value=\"" + Currencies[iIndex].CurrencyId + "\">" + Currencies[iIndex].CurrencyName + "</option>\r\n";
                    }
                }
                SelectCurrencyText += "</select>\r\n";

                string SelectShareScopeText =
                    "<select id=\"drpSharedScope\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Cá nhân (private)") + "</option>\r\n" +
                    "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Công khai (public)") + "</option>\r\n" +
                    "   <option value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Giới hạn nhóm (group)") + "</option>\r\n" +
                    "</select>\r\n";
                if (OService.SharedScope == 1)
                {
                    SelectShareScopeText =
                        "<select id=\"drpSharedScope\" class=\"form-control\">\r\n" +
                        "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Cá nhân (private)") + "</option>\r\n" +
                        "   <option selected value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Công khai (public)") + "</option>\r\n" +
                        "   <option value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Giới hạn nhóm (group)") + "</option>\r\n" +
                        "</select>\r\n";
                }
                if (OService.SharedScope == 2)
                {
                    SelectShareScopeText =
                        "<select id=\"drpSharedScope\" class=\"form-control\">\r\n" +
                        "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Cá nhân (private)") + "</option>\r\n" +
                        "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Công khai (public)") + "</option>\r\n" +
                        "   <option selected value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Giới hạn nhóm (group)") + "</option>\r\n" +
                        "</select>\r\n";
                }

                
                RangePriceCls[]
                    RangePrices=new RangePriceCls[]
                    {
                        new RangePriceCls(WebLanguage.GetLanguage(OSiteParam, "Giá đơn lẻ"),0),
                        new RangePriceCls(WebLanguage.GetLanguage(OSiteParam, "Giá theo mùa"),1),
                        new RangePriceCls(WebLanguage.GetLanguage(OSiteParam, "Giá theo giờ"),2),
                        new RangePriceCls(WebLanguage.GetLanguage(OSiteParam, "Giá theo đoàn"),3),
                        new RangePriceCls(WebLanguage.GetLanguage(OSiteParam, "Giá theo mùa theo đoàn"),4),
                        new RangePriceCls(WebLanguage.GetLanguage(OSiteParam, "Giá theo giờ theo đoàn"),5),
                    };

                //string SelectPriceText =
                //  "<select id=\"drpSelectPriceType\" class=\"form-control\">\r\n";
                //for (int iIndex = 0; iIndex < RangePrices.Length; iIndex++)
                //{
                //    if (RangePrices[iIndex].Value == OService.PriceType)
                //    {
                //        SelectPriceText +=
                //            "   <option selected value=\"" + RangePrices[iIndex].Value + "\">" + RangePrices[iIndex].Subject + "</option>\r\n";
                //    }
                //    else
                //    {
                //        SelectPriceText +=
                //            "   <option value=\"" + RangePrices[iIndex].Value + "\">" + RangePrices[iIndex].Subject + "</option>\r\n";
                //    }
                //}
                //SelectPriceText += "</select>\r\n";
               
                UnitFilterCls
                    OUnitFilter = new UnitFilterCls();
                UnitCls[]
                    Units = CallBussinessUtility.CreateBussinessProcess().CreateUnitProcess().Reading(ORenderInfo, OUnitFilter);

                string SelectUnitText = "<select id=\"drpSelectUnit\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < Units.Length; iIndex++)
                {
                    if (Units[iIndex].UnitId.Equals(OService.frkUnitId))
                    {
                        SelectUnitText += "   <option selected value=\"" + Units[iIndex].UnitId + "\">" + Units[iIndex].UnitName + "</option>\r\n";
                    }
                    else
                    {
                        SelectUnitText += "   <option value=\"" + Units[iIndex].UnitId + "\">" + Units[iIndex].UnitName + "</option>\r\n";
                    }
                }
                SelectUnitText += "</select>\r\n";

                AssetSupplierFilterCls
                 OAssetSupplierFilter = new AssetSupplierFilterCls();
                OAssetSupplierFilter.ActiveOnly = 1;
                AssetSupplierCls[]
                    AssetSuppliers = CallBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Reading(ORenderInfo, OAssetSupplierFilter);

                string SelectAssetSupplierText =
                    " <select onchange=\"javascript:CallReading();\" id=\"drpSelectAssetSupplier\" class=\"form-control select2\">\r\n" +
                    "       <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn nhà cung cấp") + "</option>\r\n";
                for (int iIndex = 0; iIndex < AssetSuppliers.Length; iIndex++)
                {
                    if (OService.frkAssetSupplierId.Equals(AssetSuppliers[iIndex].AssetSupplierId))
                    {
                        SelectAssetSupplierText +=
                            "   <option selected value=\"" + AssetSuppliers[iIndex].AssetSupplierId + "\">" + AssetSuppliers[iIndex].AssetSupplierName + "</option>\r\n";
                    }
                    else
                    {
                        SelectAssetSupplierText +=
                            "   <option value=\"" + AssetSuppliers[iIndex].AssetSupplierId + "\">" + AssetSuppliers[iIndex].AssetSupplierName + "</option>\r\n";
                    }
                }
                SelectAssetSupplierText += "</select>\r\n";

                string Html =
                   " <div class=\"ibox-content\"> \r\n" +
                   "     <div class=\"row\"> \r\n" +
                   "                 <div> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + OService.ServiceId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật thay đổi") + "</strong></button> \r\n" +
                      
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd('" + OService.frkServiceTypeId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</strong></button> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button> \r\n" +
                      "                 </div> \r\n" +
                   "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\"><span style=\"font-weight:bold;color:green;font-size:18px\">" +  OService.ServiceName + "</span></h3> \r\n" +
                      "             <div> \r\n" +
                      "             <div class=\"row\"> \r\n" +
                      "                <div class=\"col-md-4\">\r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Chọn dịch vụ chuẩn") + "</label> " + SelectServiceText + "</div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Nhà cung cấp") + "</label> " + SelectAssetSupplierText + "</div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã dịch vụ") + "</label> <input value=\"" + OService.ServiceName + "\" id=\"txtServiceCode\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mã dịch vụ") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ") + "</label> <input value=\"" + OService.ServiceName + "\" id=\"txtServiceName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên dịch vụ") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả ngắn") + "</label> <textarea id=\"txtShortDescription\" style=\"height:200px\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả ngắn") + "\" class=\"form-control\">" + OService.ShortDescription + "</textarea></div> \r\n" +
                      "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả") + "</label> <textarea id=\"txtDescription\" style=\"height:200px\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả đầy đủ") + "\" class=\"form-control\">" + OService.Description + "</textarea></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị tính") + "</label> " + SelectUnitText + "</div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá mua") + "</label> <input value=\"" + FunctionUtility.FormatNumber(OService.BuyPrice) + "\" id=\"txtBuyPrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá mua") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + "</label> <input value=\"" + FunctionUtility.FormatNumber(OService.SalePrice) + "\" id=\"txtSalePrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + "</label> <input value=\"" + FunctionUtility.FormatNumber(OService.WebPrice) + "\" id=\"txtWebPrice\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Loại tiền gốc") + "</label> " + SelectCurrencyText + "</div> \r\n" +

                      "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thông tin chi tiết sản phẩm") + "</label> <input value=\"" + OService.ServiceToolTips + "\" id=\"txtServiceToolTips\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thông tin chi tiết sản phẩm") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt") + "</label> <input id=\"txtSpecialConditions\" value=\"" + OService.SpecialConditions + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điều kiện đặc biệt") + "\" class=\"form-control\"></div> \r\n" +

                      "                 <div class=\"form-group\" style=\"display:none\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input value=\"" + OService.SortIndex + "\" id=\"txtSortIndex\"  type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Phạm vi") + "</label> " + SelectShareScopeText + "</div> \r\n" +

                      //"                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "</label> <input value=\"" + OService.SeoTitle + "\" id=\"txtSeoTitle\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "\" class=\"form-control\"></div> \r\n" +
                      //"                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "</label> <input value=\"" + OService.SeoKeyword + "\"  id=\"txtSeoKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "\" class=\"form-control\"></div> \r\n" +
                      //"                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "</label> <input value=\"" + OService.SeoDescription + "\"  id=\"txtSeoDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "\" class=\"form-control\"></div> \r\n" +
                      //"                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "</label> <input value=\"" + OService.SeoAuthor + "\"   id=\"txtSeoAuthor\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "\" class=\"form-control\"></div> \r\n" +

                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> " + SelectActiveText + "</div> \r\n" +

                      "                </div>\r\n" +
                      "                <div class=\"col-md-8\">\r\n" +
                                            ServerSideDrawTab(ORenderInfo, OService).HtmlContent +
                      "                </div>\r\n" +
                      "            </div>\r\n" +
                      "                 <div> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + OService.ServiceId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Cập nhật thay đổi") + "</strong></button> \r\n" +
                      
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd('" + OService.frkServiceTypeId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</strong></button> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideDrawTab(
            RenderInfoCls ORenderInfo,
            ServiceCls OService)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                AjaxOut 
                    RetAjaxOutExtraAttributes =  DrawExtraAttributeForm(ORenderInfo, OService.ServiceId);
                string Html =

                     " <div class=\"tabs-container\"> \r\n" +
                     "        <ul class=\"nav nav-tabs\"> \r\n" +
                     "            <li class=\"active\"><a data-toggle=\"tab\" href=\"#tab-1\">" + WebLanguage.GetLanguage(OSiteParam, "Giá theo mùa") + "</a></li> \r\n" +
                     //"            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-2\">" + WebLanguage.GetLanguage(OSiteParam, "Thiết lập giá") + "</a></li> \r\n" +
                     "            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-3\">" + WebLanguage.GetLanguage(OSiteParam, "Thông tin mở rộng") + "</a></li> \r\n" +
                     "            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-4\">" + WebLanguage.GetLanguage(OSiteParam, "Thông tin SEO") + "</a></li> \r\n" +
                     "        </ul> \r\n" +
                     "        <div class=\"tab-content\"> \r\n" +
                     "            <div id=\"tab-1\" class=\"tab-pane active\"> \r\n" +
                     "                <div class=\"panel-body\"> \r\n" +
                                            DrawServicePriceRangeForm(ORenderInfo, OService.ServiceId).HtmlContent +
                     "                </div> \r\n" +
                     "            </div> \r\n" +
                     //"            <div id=\"tab-2\" class=\"tab-pane\"> \r\n" +
                     //"                <div class=\"panel-body\"> \r\n" +
                     //                       DrawServiceSharedForm(ORenderInfo, OService.ServiceId).HtmlContent +
                     //"                </div> \r\n" +
                     //"            </div> \r\n" +

                     "            <div id=\"tab-3\" class=\"tab-pane\"> \r\n" +
                     "                <div class=\"panel-body\" id=\"divExtraAttriuteContent\"> \r\n" +
                                           RetAjaxOutExtraAttributes.HtmlContent +
                     "                </div> \r\n" +
                     "            </div> \r\n" +

                     "            <div id=\"tab-4\" class=\"tab-pane\"> \r\n" +
                     "                <div class=\"panel-body\" id=\"divExtraAttriuteContent\"> \r\n" +
                     "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "</label> <input value=\"" + OService.SeoTitle + "\" id=\"txtSeoTitle\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "\" class=\"form-control\"></div> \r\n" +
                     "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "</label> <input value=\"" + OService.SeoKeyword + "\"  id=\"txtSeoKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "\" class=\"form-control\"></div> \r\n" +
                     "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "</label> <input value=\"" + OService.SeoDescription + "\"  id=\"txtSeoDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "\" class=\"form-control\"></div> \r\n" +
                     "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "</label> <input value=\"" + OService.SeoAuthor + "\"   id=\"txtSeoAuthor\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "\" class=\"form-control\"></div> \r\n" +
                     "                </div> \r\n" +
                     "            </div> \r\n" +

                    


                     "        </div> \r\n" +


                     "    </div> \r\n" +
                     "  <script>\r\n" +
                     "      $('#drpSelectServiceSession').select2();\r\n" +
                     "      $('#drpSelectOwner').select2();\r\n" +
                     "      $('.CssDate').datepicker({\r\n" +
                     "           format: 'dd/mm/yyyy'\r\n" +
                     "      });\r\n";
                string[] Ids=(string[])RetAjaxOutExtraAttributes.RetObject;
                string TextIds = "";
                for (int iIndex = 0; iIndex < Ids.Length; iIndex++)
                {
                    Html += WebScreen.GetMceEditor("txtExtraInfo"+Ids[iIndex], 200);
                    TextIds = TextIds + Ids[iIndex];
                    if (iIndex < Ids.Length - 1)
                    {
                        TextIds += ",";
                    }
                }
                Html+=
                    "     var ServiceTypeExtraInfoIds='"+TextIds+"';\r\n"+
                    //"     alert(ServiceTypeExtraInfoIds);\r\n"+
                     "  </script>\r\n";

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
        public static AjaxOut ServerSideUpdate(
            RenderInfoCls ORenderInfo,
            string ServiceId,
            SaveServiceCls OSaveService,
            ServiceTypeExtraInfoCls[] ServiceTypeExtraInfos)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                if (string.IsNullOrEmpty(OSaveService.ServiceName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveService.frkAssetSupplierId)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa chọn nhà cung cấp dịch vụ"));
                if (FunctionUtility.checkDecimal(OSaveService.BuyPrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá mua không hợp lệ"));
                if (FunctionUtility.checkDecimal(OSaveService.SalePrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá bán không hợp lệ"));
                if (FunctionUtility.checkDecimal(OSaveService.WebPrice) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Giá bán trên web không hợp lệ"));
                if (string.IsNullOrEmpty(OSaveService.frkCurrencyId)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa chọn loại tiền gốc"));

                ServiceCls
                    OService = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo, ServiceId);

                OService.frkServiceTypeId = OSaveService.frkServiceTypeId;
                OService.frkAssetSupplierId = OSaveService.frkAssetSupplierId;
                OService.ServiceCode = OSaveService.ServiceCode;
                OService.ServiceName = OSaveService.ServiceName;
                OService.frkUnitId = OSaveService.frkUnitId;

                OService.BuyPrice = decimal.Parse(OSaveService.BuyPrice);
                OService.SalePrice = decimal.Parse(OSaveService.SalePrice);
                OService.WebPrice = decimal.Parse(OSaveService.WebPrice);
                OService.frkCurrencyId = OSaveService.frkCurrencyId;
                OService.ShortDescription = OSaveService.ShortDescription;
                OService.Description = OSaveService.Description;

                OService.SharedScope = OSaveService.SharedScope;
                OService.PriceType = OSaveService.PriceType;

                OService.ServiceToolTips = OSaveService.ServiceToolTips;
                OService.SpecialConditions = OSaveService.SpecialConditions;

                OService.SeoTitle = OSaveService.SeoTitle;
                OService.SeoAuthor = OSaveService.SeoAuthor;
                OService.SeoKeyword = OSaveService.SeoKeyword;
                OService.SeoDescription = OSaveService.SeoDescription;
                OService.frkStdServiceId = OSaveService.frkStdServiceId;
                OService.SortIndex = OSaveService.SortIndex;
                OService.Active = OSaveService.Active;
                OService.ServiceTypeExtraInfos = ServiceTypeExtraInfos;

                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().Save(ORenderInfo, ServiceId, OService);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Cập nhật thay đổi dịch vụ thành công");
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
        public static AjaxOut ServerSideCreateServiceObject(
            RenderInfoCls ORenderInfo,
            string ServiceId,
            string ServiceTypeExtraInfoText)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                ServiceCls
                    OService = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo, ServiceId);
                string[] ServiceTypeExtraInfoIds = ServiceTypeExtraInfoText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                RetAjaxOut.RetObject = OService;

                 ServiceTypeExtraInfoCls[]
                    ServiceTypeExtraInfos=new ServiceTypeExtraInfoCls[ServiceTypeExtraInfoIds.Length];
                for (int iIndex = 0; iIndex < ServiceTypeExtraInfos.Length; iIndex++)
                {
                    ServiceTypeExtraInfos[iIndex] = new ServiceTypeExtraInfoCls();
                    ServiceTypeExtraInfos[iIndex].ServiceTypeExtraInfoId = ServiceTypeExtraInfoIds[iIndex];
                }
                RetAjaxOut.RetObject1 = ServiceTypeExtraInfos;
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
        public static AjaxOut DrawServicePriceRangeForm(
            RenderInfoCls ORenderInfo,
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                ServiceCls
                    OService = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo, ServiceId);

                
                string Html =
                    "<div style=\"font-weight:bold;margin-bottom:4px\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn khoảng ngày") + "</div>\r\n" +
                    "<div style=\"margin-bottom:4px\"><div class=\"input-group\"><input id=\"txtStartDate\" class=\"form-control CssDate\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ ngày") + "\">  <span class=\"input-group-addon\">đến</span> <input id=\"txtEndDate\" class=\"form-control CssDate\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Đến ngày") + "\"></div></div>\r\n" +

                    "<div style=\"font-weight:bold;margin-bottom:4px\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn thời gian") + "</div>\r\n" +
                    "<div style=\"margin-bottom:4px\"><div class=\"input-group\"><input id=\"txtStartTime\" class=\"form-control CssTime\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ thời gian") + "\">  <span class=\"input-group-addon\">đến</span> <input id=\"txtEndTime\" class=\"form-control CssTime\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Đến thời gian") + "\"></div></div>\r\n" +

                    "<div style=\"font-weight:bold;margin-bottom:4px\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn khoảng khách") + "</div>\r\n" +
                    "<div style=\"margin-bottom:4px\"><div class=\"input-group\"><input id=\"txtFromPax\" class=\"form-control\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ số khách") + "\">  <span class=\"input-group-addon\">đến</span> <input id=\"txtToPax\" class=\"form-control\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Đến số khách") + "\"> </div></div>\r\n" +
                    
                    "<div style=\"margin-top:10px;margin-bottom:10px\"><button class=\"btn btn-sm btn-primary m-t-n-xs\" onclick=\"javascript:CallActionAddRangeServicePrice('" + OService.ServiceId + "');\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm khoảng giá") + "</button></div>\r\n"+
                    "<div style=\"font-weight:bold;margin-bottom:10px\">" + WebLanguage.GetLanguage(OSiteParam, "Danh sách giá theo mùa, khách, thời gian") + "</div>\r\n" +
                    "<div id=\"divListRangePriceContent\">" + ServerSideReadingRangeServicePrice(ORenderInfo,ServiceId).HtmlContent + "</div>\r\n";
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
        public static AjaxOut ServerSideReadingRangeServicePrice(RenderInfoCls ORenderInfo, string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;

                RangeServicePriceCls[]
                    RangeServicePrices = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().ReadingRangeServicePrices(ORenderInfo, ServiceId);


                string Html =
                        "         <div id=\"divProcessingUpdate\" style=\"height:20px;color:maroon;font-weight:bold\"></div>\r\n"+
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "T.Ngày") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Đ.Ngày") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "T.Giờ") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Đ.Giờ") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "T.SL") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Đ.SL") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < RangeServicePrices.Length; iIndex++)
                {
                    Html +=
                        "                 <tr> \r\n" +
                        "                     <td style=\"font-size:14px\">" + RangeServicePrices[iIndex].StartDateText + "</td> \r\n" +
                        "                     <td style=\"font-size:14px\">" + RangeServicePrices[iIndex].EndDateText + "</td> \r\n" +
                        "                     <td style=\"font-size:14px\">" + RangeServicePrices[iIndex].StartTime + "</td> \r\n" +
                        "                     <td style=\"font-size:14px\">" + RangeServicePrices[iIndex].EndTime + "</td> \r\n" +
                        "                     <td style=\"font-size:14px\">" + RangeServicePrices[iIndex].FromPax.ToString("#,##0") + "</td> \r\n" +
                        "                     <td style=\"font-size:14px\">" + RangeServicePrices[iIndex].ToPax.ToString("#,##0") + "</td> \r\n" +
                        "                     <td style=\"font-size:14px; width:75px;\"><input onkeypress=\"if(event.keyCode==13)UpdateSalePrice('" + RangeServicePrices[iIndex].RangeServicePriceId + "',1);\" onblur=\"javascript:UpdateSalePrice('" + RangeServicePrices[iIndex].RangeServicePriceId + "',0);\"  style=\"font-family:consola;font-size:14px;font-weight:bold;padding:4px;width:75px;border:solid 1px silver;text-align:right;background-color:lightyellow\" id=\"txtSalePrice" + RangeServicePrices[iIndex].RangeServicePriceId + "\" value=\"" + FunctionUtility.FormatNumber(RangeServicePrices[iIndex].SalePrice) + "\"></td> \r\n" +
                        "                     <td style=\"font-size:14px;width:75px\"><input  onkeypress=\"if(event.keyCode==13)UpdateWebPrice('" + RangeServicePrices[iIndex].RangeServicePriceId + "',1);\" onblur=\"javascript:UpdateWebPrice('" + RangeServicePrices[iIndex].RangeServicePriceId + "',0);\"  style=\"font-family:consola;font-size:14px;font-weight:bold;padding:4px;width:75px;border:solid 1px silver;text-align:right;background-color:lightyellow\" id=\"txtWebPrice" + RangeServicePrices[iIndex].RangeServicePriceId + "\" value=\"" + FunctionUtility.FormatNumber(RangeServicePrices[iIndex].WebPrice) + "\"></td> \r\n" +
                        "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa giá theo mùa") + "\" href=\"javascript:CallActionDeleteRangeServicePrice('" + RangeServicePrices[iIndex].RangeServicePriceId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "       </div>\r\n";

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
        public static AjaxOut ServerSideAdd(RenderInfoCls ORenderInfo, string ServiceTypeId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new AddService().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("ServiceTypeId",ServiceTypeId)
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
        public static AjaxOut ServerSideAddRangePrice(
            RenderInfoCls ORenderInfo, 
            string ServiceId,
            string StartDate,
            string EndDate,
            string StartTime,
            string EndTime,
            string FromPax,
            string ToPax)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;

                if (!string.IsNullOrEmpty(StartDate))
                {
                    if (FunctionUtility.checkVnDate(StartDate) == false)
                    {
                        throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ngày bắt đầu nhập không hợp lệ"));
                    }
                }

                if (!string.IsNullOrEmpty(EndDate))
                {
                    if (FunctionUtility.checkVnDate(EndDate) == false)
                    {
                        throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ngày kết thúc nhập không hợp lệ"));
                    }
                }

                if (!string.IsNullOrEmpty(StartTime))
                {
                    if (FunctionUtility.checkTime(StartTime) == false)
                    {
                        throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thời gian bắt đầu nhập không hợp lệ"));
                    }
                }

                if (!string.IsNullOrEmpty(EndTime))
                {
                    if (FunctionUtility.checkTime(EndTime) == false)
                    {
                        throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thời gian kết thúc nhập không hợp lệ"));
                    }
                }


                if (!string.IsNullOrEmpty(FromPax))
                {
                    if (FunctionUtility.checkInteger(FromPax) == false)
                    {
                        throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số khách bắt đầu không hợp lệ"));
                    }
                }

                if (!string.IsNullOrEmpty(ToPax))
                {
                    if (FunctionUtility.checkInteger(ToPax) == false)
                    {
                        throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số khách kết thúc không hợp lệ"));
                    }
                }

                DateTime dStartDate = new System.DateTime();
                DateTime dEndDate = new System.DateTime();

                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    dStartDate = FunctionUtility.VNDateToDate(StartDate);
                    dEndDate = FunctionUtility.VNDateToDate(EndDate);

                    if (dStartDate > dEndDate)
                    {
                        throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Khoảng ngày không hợp lệ"));
                    }
                }

                DateTime dStartTime = new System.DateTime();
                DateTime dEndTime = new System.DateTime();
                if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
                {
                    dStartTime = FunctionUtility.VNDateToDate(StartTime);
                    dEndTime = FunctionUtility.VNDateToDate(EndTime);

                    if (dStartTime > dEndTime)
                    {
                        throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Khoảng thời gian không hợp lệ"));
                    }
                }

                int dFromPax = 0;
                int dToPax = 0;
                if (!string.IsNullOrEmpty(FromPax) && !string.IsNullOrEmpty(ToPax))
                {
                    dFromPax = int.Parse(FromPax);
                    dToPax = int.Parse(ToPax);
                }

                ServiceCls
                    OService=CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo,ServiceId);
                RangeServicePriceCls
                    ORangeServicePrice = new RangeServicePriceCls();
                ORangeServicePrice.RangeServicePriceId = System.Guid.NewGuid().ToString();
                ORangeServicePrice.frkServiceId = ServiceId;
                
                ORangeServicePrice.StartTime = StartTime;
                ORangeServicePrice.EndTime = EndTime;

                ORangeServicePrice.StartDate = dStartDate;
                ORangeServicePrice.EndDate = dEndDate;

                ORangeServicePrice.FromPax = dFromPax;
                ORangeServicePrice.ToPax = dToPax;

                ORangeServicePrice.SalePrice = OService.SalePrice;
                ORangeServicePrice.WebPrice = OService.WebPrice;

                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().AddRangeServicePrice(ORenderInfo, ServiceId, ORangeServicePrice);
                RetAjaxOut = ServerSideReadingRangeServicePrice(ORenderInfo, ServiceId);
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
        public static AjaxOut ServerSideUpdateSalePrice(
            RenderInfoCls ORenderInfo, 
            string RangeServicePriceId,
            string SalePrice)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                if (FunctionUtility.checkDecimal(SalePrice)==false)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam,"Giá bán không hợp lệ"));
                }
               
                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().SaveRangeServicePrice(ORenderInfo, RangeServicePriceId, decimal.Parse(SalePrice));
                RetAjaxOut.InfoMessage=WebLanguage.GetLanguage(OSiteParam,"Cập nhật giá bán thành công");
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
        public static AjaxOut ServerSideUpdateWebPrice(
            RenderInfoCls ORenderInfo, 
            string RangeServicePriceId,
            string WebPrice)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                if (FunctionUtility.checkDecimal(WebPrice)==false)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam,"Giá bán web không hợp lệ"));
                }
               
                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().SaveRangeServicePriceWeb(ORenderInfo, RangeServicePriceId, decimal.Parse(WebPrice));
                RetAjaxOut.InfoMessage=WebLanguage.GetLanguage(OSiteParam,"Cập nhật giá bán web thành công");
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
        public static AjaxOut ServerSideDeleteRangePrice(
            RenderInfoCls ORenderInfo, 
            string RangeServicePriceId,
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().DeleteRangeServicePrice(ORenderInfo, RangeServicePriceId);
                RetAjaxOut = ServerSideReadingRangeServicePrice(ORenderInfo, ServiceId);
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
        public static AjaxOut DrawServiceSharedForm(
            RenderInfoCls ORenderInfo,
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                ServiceCls
                    OService = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo, ServiceId);

                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerId;
                OwnerFilterCls
                    OwnerFilter = new OwnerFilterCls();
                OwnerFilter.ActiveOnly = 1;
                OwnerCls[]
                    Owners = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().Reading(ORenderInfo, OwnerFilter);
                string SelectOwnerText =
                    "<select id=\"drpSelectOwner\" style=\"width:80%\" class=\"form-control select2\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn đại lý thiết lập giá") + "</option>\r\n";
                for (int iIndex = 0; iIndex < Owners.Length; iIndex++)
                {
                    if (!Owners[iIndex].OwnerId.Equals(OwnerId))
                    {
                        SelectOwnerText +=
                            "   <option value=\"" + Owners[iIndex].OwnerId + "\">" + Owners[iIndex].OwnerCode + " - " + Owners[iIndex].OwnerName + "</option>\r\n";
                    }
                }
                SelectOwnerText += "</select>\r\n";

                string Html =
                    "<div style=\"font-weight:bold;margin-bottom:4px\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn đại lý") + "</div>\r\n" +
                    "<div style=\"margin-bottom:4px\"><div class=\"input-group\">" + SelectOwnerText + "<button class=\"btn btn-sm btn-primary m-t-n-xs\" onclick=\"javascript:CallActionAddShared('" + OService.ServiceId + "');\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn chia sẻ") + "</button></div></div>\r\n" +
                    "<div style=\"font-weight:bold;margin-bottom:4px\">" + WebLanguage.GetLanguage(OSiteParam, "Danh sách đại lý") + "</div>\r\n" +
                    "<div id=\"divListSharedContent\">" + ServerSideReadingServiceSharedOwner(ORenderInfo, ServiceId).HtmlContent + "</div>\r\n";
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
        public static AjaxOut DrawExtraAttributeForm(
            RenderInfoCls ORenderInfo,
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().InitServiceExtraInfo(ORenderInfo, ServiceId);
                ServiceCls
                    OService = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo, ServiceId);

                

                string Html = "";
                
                ServiceTypeExtraInfoCls[]
                    ServiceTypeExtraInfos = OService.ServiceTypeExtraInfos;
                //if (ServiceTypeExtraInfos.Length == 0)
                //{
                //    ServiceTypeExtraInfoFilterCls
                //        OServiceTypeExtraInfoFilter = new ServiceTypeExtraInfoFilterCls();
                //    OServiceTypeExtraInfoFilter.ServiceTypeId = OService.frkServiceTypeId;
                //    ServiceTypeExtraInfos = CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeExtraInfoProcess().Reading(ORenderInfo, OServiceTypeExtraInfoFilter);
                //}
                string[] Ids = new string[ServiceTypeExtraInfos.Length];
                for (int iIndex = 0; iIndex < ServiceTypeExtraInfos.Length; iIndex++)
                {
                    Ids[iIndex] = ServiceTypeExtraInfos[iIndex].ServiceTypeExtraInfoId;
                    Html+=
                        "<div style=\"font-weight:bold;margin-bottom:10px;color:green;font-size:16px\">" + WebLanguage.GetLanguage(OSiteParam, ServiceTypeExtraInfos[iIndex].ExtraFieldType) + "</div>\r\n" +
                        "<div style=\"margin-bottom:20px\"><textarea id=\"txtExtraInfo" + ServiceTypeExtraInfos[iIndex].ServiceTypeExtraInfoId + "\">" + ServiceTypeExtraInfos[iIndex].Content + "</textarea></div>\r\n";
                }

                RetAjaxOut.RetObject = Ids;
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
        public static AjaxOut ServerSideReadingServiceSharedOwner(RenderInfoCls ORenderInfo, string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;

                ServiceSharedOwnerCls[]
                    ServiceSharedOwners = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().ReadingSharedOwner(ORenderInfo, ServiceId);


                string Html =
                        "         <div id=\"divProcessingUpdateShared\" style=\"height:20px;color:maroon;font-weight:bold\"></div>\r\n" +
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã đại lý") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên đại lý") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tỉ lệ tăng/giảm") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < ServiceSharedOwners.Length; iIndex++)
                {
                    Html +=
                        "                 <tr> \r\n" +
                        "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                        "                     <td>" + ServiceSharedOwners[iIndex].OwnerCode + "</td> \r\n" +
                        "                     <td>" + ServiceSharedOwners[iIndex].OwnerName + "</td> \r\n" +
                        "                     <td style=\"width:100px;\"><input onkeypress=\"if(event.keyCode==13)UpdateSharedMarkupPercent('" + ServiceSharedOwners[iIndex].ServiceSharedOwnerId + "',1);\" onblur=\"javascript:UpdateSharedMarkupPercent('" + ServiceSharedOwners[iIndex].ServiceSharedOwnerId + "',0);\"  style=\"font-family:consola;font-size:16px;font-weight:bold;padding:4px;width:100px;border:solid 1px silver;text-align:right;background-color:lightyellow\" id=\"txtServiceSharedOwnerMarkUpPercent" + ServiceSharedOwners[iIndex].ServiceSharedOwnerId + "\" value=\"" + FunctionUtility.FormatNumber(ServiceSharedOwners[iIndex].MarkUpPercent) + "\"></td> \r\n" +
                        "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa chia sẻ đại lý") + "\" href=\"javascript:CallActionDeleteServiceSharedOwner('" + ServiceSharedOwners[iIndex].ServiceSharedOwnerId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "       </div>\r\n";

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
        public static AjaxOut ServerSideAddShared(
            RenderInfoCls ORenderInfo, 
            string ServiceId,
            string OwnerId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                if (string.IsNullOrEmpty(OwnerId))
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam,"Chưa chọn đại lý"));
                }

                
                ServiceCls
                    OService=CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo,ServiceId);
                
                ServiceSharedOwnerCls
                    OServiceSharedOwner = new ServiceSharedOwnerCls();
                OServiceSharedOwner.ServiceSharedOwnerId = System.Guid.NewGuid().ToString();
                OServiceSharedOwner.frkOwnerId = OwnerId;
                OServiceSharedOwner.frkServiceId = ServiceId;
                OServiceSharedOwner.MarkUpPercent = 0;
                

                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().AddSharedOwner(ORenderInfo, ServiceId, OServiceSharedOwner);
                RetAjaxOut = ServerSideReadingServiceSharedOwner(ORenderInfo, ServiceId);
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
        public static AjaxOut ServerSideDeleteServiceSharedOwner(
            RenderInfoCls ORenderInfo,
            string ServiceSharedOwnerId,
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().DeleteSharedOwner(ORenderInfo, ServiceSharedOwnerId);
                RetAjaxOut = ServerSideReadingServiceSharedOwner(ORenderInfo, ServiceId);
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
        public static AjaxOut ServerSideUpdateServiceSharedOwner(
            RenderInfoCls ORenderInfo,
            string ServiceSharedOwnerId,
            string MarkUpPercent)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                if (FunctionUtility.checkInteger(MarkUpPercent) == false)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam,"Tỉ lệ tăng giảm % không hợp lệ"));
                }

                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().SaveSharedOwnerMarkUpPercent(ORenderInfo, ServiceSharedOwnerId, int.Parse(MarkUpPercent));
                RetAjaxOut.InfoMessage=WebLanguage.GetLanguage(OSiteParam,"Cập nhật tỉ lệ tăng giảm thành công");
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        public override bool RunAlone
        {
            get
            {
                return false;
            }
        }            
    }
}
