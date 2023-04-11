﻿using OneTSQ.Bussiness.Utility;
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
    public class QuotationTemplateServiceProcess
    {
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideAddSectionService(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateItemId,
            string ServiceTypeId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                ServiceTypeCls
                    OServiceType = CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeProcess().CreateModel(ORenderInfo, ServiceTypeId);
                QuotationTemplateSectionServiceCls
                    OQuotationTemplateSectionService = new QuotationTemplateSectionServiceCls();
                OQuotationTemplateSectionService.QuotationTemplateSectionServiceId = System.Guid.NewGuid().ToString();
                OQuotationTemplateSectionService.Subject = OServiceType.ServiceTypeName;
                OQuotationTemplateSectionService.SectionSortIndex = 1;
                OQuotationTemplateSectionService.frkQuotationTemplateItemId = QuotationTemplateItemId;
                OQuotationTemplateSectionService.frkServiceTypeId = ServiceTypeId;
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().AddQuotationTemplateSectionService(ORenderInfo, QuotationTemplateItemId, OQuotationTemplateSectionService);

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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideRemoveSectionService(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateItemId,
            string QuotationTemplateSectionServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().DeleteQuotationTemplateSectionService(ORenderInfo, QuotationTemplateSectionServiceId);
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


        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDrawAddServiceToQuotationTemplateItemForm(
            RenderInfoCls ORenderInfo, 
            string QuotationTemplateItemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                ServiceTypeFilterCls
                    OServiceTypeFilter = new ServiceTypeFilterCls();
                OServiceTypeFilter.ActiveOnly=1;

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ServiceTypeCls[]
                    ServiceTypes = CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeProcess().Reading(ORenderInfo, OServiceTypeFilter);
                string Html = "";
               
                Html +=
                    "   <h3 style=\"color:green;font-weight:bold\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm nhóm dịch vụ vào hành trình") + "</h3>\r\n" +
                    "   <div style=\"text-align:right\"><button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBackFromLookUpForm();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button></div> \r\n" +
                    "   <div class=\"search-result-info\">" + WebLanguage.GetLanguage(OSiteParam, "Tìm thấy") + " (" + ServiceTypes.Length.ToString("#,##0") + ") " + WebLanguage.GetLanguage(OSiteParam, "loại hình dịch vụ") + "</div>\r\n" +
                    "         <div class=\"table-responsive\"> \r\n" +
                    "             <table class=\"table table-striped\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\">Icon</th> \r\n" +
                    //"                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên loại hình dịch vụ") + " </th> \r\n" +
                    "                     <th class=\"th-func-20\"></th> \r\n" +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < ServiceTypes.Length; iIndex++)
                {
                    string AssetSupplierUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new AssetSupplier().WebPartId,
                        new WebParamCls[]
                        {
                            new WebParamCls("ServiceTypeId",ServiceTypes[iIndex].ServiceTypeId)
                        });
                    Html +=
                        "                 <tr> \r\n" +
                        "                     <td><img style=\"height:16px\" src=\"/images/icon-" + ServiceTypes[iIndex].ServiceTypeId + ".png\" /></td> \r\n" +
                        //"                     <td>" + ServiceTypes[iIndex].ServiceTypeCode + "</td> \r\n" +
                        "                     <td>" + ServiceTypes[iIndex].ServiceTypeName + "</td> \r\n" +
                        "                     <td class=\"td-center\" style=\"width:20px\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm dịch vụ vào hành trình") + "\" href=\"javascript:CallAddSectionService('" + QuotationTemplateItemId+"','"+ ServiceTypes[iIndex].ServiceTypeId + "');\"><i class=\"" + WebScreen.GetPublishGridIcon() + "\"></i></a></td> \r\n" +
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
        public static AjaxOut ServerSideDrawSearchService(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateItemId,
            string QuotationTemplateSectionServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationTemplateSectionServiceCls
                    OQuotationTemplateSectionService= CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateQuotationTemplateSectionServiceModel(ORenderInfo, QuotationTemplateSectionServiceId);

                AssetSupplierFilterCls
                    OAssetSupplierFilter = new AssetSupplierFilterCls();
                OAssetSupplierFilter.ServiceTypeId = OQuotationTemplateSectionService.frkServiceTypeId;
                OAssetSupplierFilter.ActiveOnly = 1;
                OAssetSupplierFilter.OnlyShowHasService = 1;
                AssetSupplierCls[]
                    AssetSuppliers = CallBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Reading(ORenderInfo, OAssetSupplierFilter);
                string SelectSupplierText =
                    "<select id=\"drpSelectSupplier\" onchange=\"javascript:CallDoSearchService('" + QuotationTemplateItemId + "','" + QuotationTemplateSectionServiceId + "');\" class=\"form-control select2\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ nhà cung cấp") + "</option>\r\n";
                for (int iIndex = 0; iIndex < AssetSuppliers.Length; iIndex++)
                {
                    SelectSupplierText += "   <option value=\"" + AssetSuppliers[iIndex].AssetSupplierId + "\">" + AssetSuppliers[iIndex].AssetSupplierName + " (" + AssetSuppliers[iIndex].CityName + ")</option>\r\n";
                }
                SelectSupplierText += "</select>\r\n";
                Html =
                    "    <input id=\"txtCurrentPageIndexService\" value=\"0\" type=\"hidden\">\r\n"+
                    "    <h3 style=\"color:green;font-weight:bold\">" + WebLanguage.GetLanguage(OSiteParam, "Dịch vụ") + " - " + OQuotationTemplateSectionService.ServiceTypeName + "</h3>\r\n" +
                    "    <div style=\"text-align:right\"><button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallDoSearchService('" + QuotationTemplateItemId + "','"+QuotationTemplateSectionServiceId+"');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Tìm kiếm") + "</strong></button> <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBackFromLookUpForm();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button></div> \r\n" +
                    "     <div class=\"ibox-content\"> \r\n" +
                    "         <div class=\"row\"> \r\n" +
                    "             <div class=\"col-sm-12\"> \r\n" +
                    "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Nhà cung cấp") + "</div>\r\n" +
                    "                 <div style=\"margin-bottom:5px\">" + SelectSupplierText + "</div>\r\n" +
                    "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                    "                 <div><input onkeypress=\"if(event.keyCode==13){CallDoSearchService('" + QuotationTemplateItemId + "','"+QuotationTemplateSectionServiceId+"');}\"  id=\"txtSearchKeywordService\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                    "             </div> \r\n" +
                    "         </div> \r\n" +
                    "         <div id=\"divProcessingSearchService\" style=\"padding:3px;color:green;height:20px\" class=\"processing\"></div>\r\n" +
                    "         <div id=\"divSearchServiceResultContent\"></div>\r\n" +
                    "     </div> \r\n";
                //" + ServerSideDrawSearchServiceResult(ORenderInfo, QuotationTemplateItemId, QuotationTemplateSectionServiceId, "").HtmlContent + "
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
        public static AjaxOut ServerSideDrawSearchServiceResult(
            RenderInfoCls ORenderInfo, 
            string QuotationTemplateItemId, 
            string QuotationTemplateSectionServiceId, 
            string SupplierId,
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

                QuotationTemplateSectionServiceCls
                    OQuotationTemplateSectionService= CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateQuotationTemplateSectionServiceModel(ORenderInfo, QuotationTemplateSectionServiceId);

                ServiceFilterCls
                    OServiceFilter = new ServiceFilterCls();
                //OServiceFilter.CityId = OQuotationTemplateItem.frkLocationServiceId;
                OServiceFilter.Keyword = Keyword;
                OServiceFilter.AssetSupplierId = SupplierId;
                OServiceFilter.ServiceTypeId = OQuotationTemplateSectionService.frkServiceTypeId;

                ServiceCls[]
                    Services = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().Reading(ORenderInfo, OServiceFilter);

                if (Services.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có dịch vụ nào theo điều kiện lọc", true);
                }
                else
                {
                    ReturnPaging OReturnPaging = WebPaging.GetPaging(Services.Length, CurrentPageIndex, 15, 5, "NextPageSearchService");
                    Html +=
                        "   <div class=\"search-result-info\">" + WebLanguage.GetLanguage(OSiteParam, "Tìm thấy") + " (" + Services.Length.ToString("#,##0") + ") " + WebLanguage.GetLanguage(OSiteParam, "dịch vụ theo điều kiện lọc") + "</div>\r\n" +
                        OReturnPaging.PagingText +
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        //"                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Dịch vụ") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "T.Phố") + " </th> \r\n";
                    if (string.IsNullOrEmpty(SupplierId))
                    {
                        Html +=
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "NCC") + " </th> \r\n";
                    }
                     Html+=   
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = OReturnPaging.StartIndex; iIndex < OReturnPaging.EndIndex; iIndex++)
                    {
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td><a href=\"javascript:CallActionAddServiceToQuotationTemplateItem('" + QuotationTemplateItemId + "','" + QuotationTemplateSectionServiceId + "','" + Services[iIndex].ServiceId + "');\">" + Services[iIndex].ServiceName + "</a><div><span style=\"color:gray;font-size:16px;font-weight:bold;font-family:consolas\">" +WebLanguage.GetLanguage(OSiteParam,"Giá")+": " + Services[iIndex].SalePrice.ToString("#,##0") + "</span> " + Services[iIndex].CurrencyCode.ToLower() + "</td> \r\n" +
                            "                     <td><a href=\"javascript:CallActionAddServiceToQuotationTemplateItem('" + QuotationTemplateItemId + "','" + QuotationTemplateSectionServiceId + "','" + Services[iIndex].ServiceId + "');\">" + Services[iIndex].CityName + "</a></td> \r\n";
                        if (string.IsNullOrEmpty(SupplierId))
                        {
                            Html +=
                            "                     <td><a href=\"javascript:CallActionViewService('" + Services[iIndex].ServiceId + "');\">" + Services[iIndex].AssetSupplierName + "</a></td> \r\n";
                        }
                         Html+=   
                            "                     <td class=\"td-center\" style=\"width:20px\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Chọn dịch vụ") + "\" href=\"javascript:CallActionAddServiceToQuotationTemplateItem('" + QuotationTemplateItemId + "','" + QuotationTemplateSectionServiceId+"','"+ Services[iIndex].ServiceId + "');\"><i class=\"" + WebScreen.GetPublishGridIcon() + "\"></i></a></td> \r\n" +
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
        public static AjaxOut ServerSideAddServiceToQuotationTemplateItem(
            RenderInfoCls ORenderInfo,
            string QuotationTemplateItemId,
            string QuotationTemplateSectionServiceId,
            string ServiceId )
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                QuotationTemplateSectionServiceCls
                    OQuotationTemplateSectionService= CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateQuotationTemplateSectionServiceModel(ORenderInfo, QuotationTemplateSectionServiceId);
                if (OQuotationTemplateSectionService == null) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Nhóm dịch vụ bị xóa không tìm thấy"));
                QuotationTemplateItemCls
                    OQuotationTemplateItem = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateQuotationTemplateItemModel(ORenderInfo, QuotationTemplateItemId);
                if (OQuotationTemplateItem == null) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Hành trình bị xóa không tìm thấy"));
                QuotationTemplateCls
                    OQuotationTemplate = CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().CreateModel(ORenderInfo, OQuotationTemplateItem.frkQuotationTemplateId);
                if (OQuotationTemplate == null) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chào giá bị xóa không tìm thấy"));

                ServiceCls
                    OService= CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().CreateModel(ORenderInfo, ServiceId);
                if (OService == null) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Dịch vụ bị xóa không tìm thấy"));
                ServiceTypeCls
                    OServiceType= CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeProcess().CreateModel(ORenderInfo, OService.frkServiceTypeId);
                if (OServiceType == null) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Loại hình dịch vụ bị xóa không tìm thấy"));

                QuotationTemplateServiceCls
                    OQuotationTemplateService = new QuotationTemplateServiceCls();
                OQuotationTemplateService.QuotationTemplateServiceId = System.Guid.NewGuid().ToString();
                OQuotationTemplateService.frkQuotationTemplateSectionServiceId = QuotationTemplateSectionServiceId;
                OQuotationTemplateService.frkServiceId = OService.ServiceId;
                OQuotationTemplateService.ServiceName = OService.ServiceName;
                OQuotationTemplateService.SupplierName = OService.AssetSupplierName;
                OQuotationTemplateService.NoOfServiceDays=1;
                OQuotationTemplateService.Note = "";
                OQuotationTemplateService.frkCurrencyId = OService.frkCurrencyId;

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().AddQuotationTemplateService(ORenderInfo,QuotationTemplateSectionServiceId, OQuotationTemplateService );

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
