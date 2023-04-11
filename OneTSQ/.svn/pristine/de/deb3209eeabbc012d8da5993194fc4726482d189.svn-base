using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebParts
{
    public class AssetSupplier : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "AssetSupplier";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Danh mục nhà cung cấp";
            }
        }

        public override string Description
        {
            get
            {
                return "Danh mục nhà cung cấp";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(AssetSupplier),Page);
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

                string PageIndex = (string)WebEnvironments.Request("pageindex");
                if (FunctionUtility.checkInteger(PageIndex) == false) PageIndex = "0";

                string ServiceTypeId = (string)WebEnvironments.Request("servicetypeid");
                if (string.IsNullOrEmpty(ServiceTypeId))
                {
                    ServiceTypeId = "";
                }
                string FilterTypeId = (string)WebEnvironments.Request("filtertypeid");
                if (string.IsNullOrEmpty(FilterTypeId))
                {
                    FilterTypeId="";
                }

                string Keyword = (string)WebEnvironments.Request("keyword");
                if (string.IsNullOrEmpty(Keyword))
                {
                    Keyword = "";
                }
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId;
                string BackUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new Country().WebPartId);

                ServiceTypeFilterCls
                  OServiceTypeFilter = new ServiceTypeFilterCls();

                
                    

                ServiceTypeCls[]
                  ServiceTypes = CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeProcess().Reading(ORenderInfo, OServiceTypeFilter);
                string SelectTypeText = "<select id=\"drpSelectServiceType\" onchange=\"javascript:OpenLink();\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < ServiceTypes.Length; iIndex++)
                {
                    if (!string.IsNullOrEmpty(ServiceTypeId))
                    {
                        if (ServiceTypes[iIndex].ServiceTypeId.ToLower().Equals(ServiceTypeId.ToLower()))
                        {
                            SelectTypeText += "   <option selected value=\"" + ServiceTypes[iIndex].ServiceTypeId + "\">" + ServiceTypes[iIndex].ServiceTypeName + "</option>\r\n";
                        }
                        else
                        {
                            SelectTypeText += "   <option value=\"" + ServiceTypes[iIndex].ServiceTypeId + "\">" + ServiceTypes[iIndex].ServiceTypeName + "</option>\r\n";
                        }
                    }
                    else
                    {
                        if (iIndex == 0)
                        {
                            ServiceTypeId = ServiceTypes[iIndex].ServiceTypeId;
                        }
                        SelectTypeText += "   <option value=\"" + ServiceTypes[iIndex].ServiceTypeId + "\">" + ServiceTypes[iIndex].ServiceTypeName + "</option>\r\n";
                    }
                }
                SelectTypeText += "</select>\r\n";

                string SelectFilterTypeText =
                    "<select id=\"drpSelectFilterType\" class=\"form-control\" onchange=\"javascript:OpenLink();\">\r\n" +
                    "   <option value=\"0\">"+WebLanguage.GetLanguage(OSiteParam,"Chỉ lọc của đơn vị mình")+"</option>\r\n"+
                    "   <option selected value=\"1\">"+WebLanguage.GetLanguage(OSiteParam,"Lọc toàn bộ")+"</option>\r\n" +
                    "</select>\r\n";

                if (FilterTypeId.Equals("1"))
                {
                    SelectFilterTypeText =
                       "<select id=\"drpSelectFilterType\" class=\"form-control\" onchange=\"javascript:OpenLink();\">\r\n" +
                       "   <option value=\"0\">"+WebLanguage.GetLanguage(OSiteParam,"Chỉ lọc của đơn vị mình")+"</option>\r\n" +
                       "   <option selected value=\"1\">"+WebLanguage.GetLanguage(OSiteParam,"Lọc toàn bộ")+"</option>\r\n" +
                       "</select>\r\n";
                }


                string Status = (string)WebEnvironments.Request("status");
                if (FunctionUtility.checkInteger(Status) == false)
                {
                    Status = "1";
                }

                string SelectStatusText =
                    "<select id=\"drpSelectStatus\" class=\"form-control\" onchange=\"javascript:OpenLink();\">\r\n" +
                    "   <option value=\"1\">"+WebLanguage.GetLanguage(OSiteParam,"Đang sử dụng")+"</option>\r\n" +
                    "   <option value=\"0\">"+WebLanguage.GetLanguage(OSiteParam,"Ngưng sử dụng")+"</option>\r\n" +
                    "   <option value=\"2\">"+WebLanguage.GetLanguage(OSiteParam,"Toàn bộ")+"</option>\r\n" +
                    "</select>\r\n";

                if (Status.Equals("0"))
                {
                    SelectStatusText =
                    "<select id=\"drpSelectStatus\" class=\"form-control\" onchange=\"javascript:OpenLink();\">\r\n" +
                    "   <option value=\"1\">"+WebLanguage.GetLanguage(OSiteParam,"Đang sử dụng")+"</option>\r\n" +
                    "   <option selected value=\"0\">"+WebLanguage.GetLanguage(OSiteParam,"Ngưng sử dụng")+"</option>\r\n" +
                    "   <option value=\"2\">"+WebLanguage.GetLanguage(OSiteParam,"Toàn bộ")+"</option>\r\n" +
                    "</select>\r\n";
                }

                if (Status.Equals("2"))
                {
                    SelectStatusText =
                    "<select id=\"drpSelectStatus\" class=\"form-control\" onchange=\"javascript:OpenLink();\">\r\n" +
                    "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Đang sử dụng") + "</option>\r\n" +
                    "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng") + "</option>\r\n" +
                    "   <option selected value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n" +
                    "</select>\r\n";
                }

                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +
                    "   function OpenLink()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       FilterTypeId = document.getElementById('drpSelectFilterType').value;\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       Status = document.getElementById('drpSelectStatus').value;\r\n" +
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.AssetSupplier.ServerSideOpenLink(RenderInfo, ServiceTypeId, FilterTypeId, Status, Keyword, 0).value;\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function NextPage(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       FilterTypeId = document.getElementById('drpSelectFilterType').value;\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       Status = document.getElementById('drpSelectStatus').value;\r\n" +
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.AssetSupplier.ServerSideOpenLink(RenderInfo, ServiceTypeId, FilterTypeId, Status, Keyword, PageIndex).value;\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function CallReading()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       PageIndex = parseInt(document.getElementById('txtPageIndex').value,10);\r\n" +
                    "       OwnerId= document.getElementById('txtOwnerId').value;\r\n" +
                    "       FilterTypeId = document.getElementById('drpSelectFilterType').value;\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       Status = document.getElementById('drpSelectStatus').value;\r\n" +
                    "       Keyword= document.getElementById('txtKeyword').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.AssetSupplier.ServerSideDrawSearchResult(RenderInfo, OwnerId, ServiceTypeId, FilterTypeId, Keyword, Status, PageIndex).value;\r\n" +
                    "       document.getElementById('divAssetSupplierContent').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "   }\r\n" +



                    "   function CallAddForm()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       FilterTypeId = document.getElementById('drpSelectFilterType').value;\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       Status = document.getElementById('drpSelectStatus').value;\r\n" +
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.AssetSupplier.ServerSideDrawAddForm(RenderInfo, ServiceTypeId, FilterTypeId, Status, Keyword, 0).value;\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +


                    "   function CallUpdateForm(AssetSupplierId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                     
                    "       FilterTypeId = document.getElementById('drpSelectFilterType').value;\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       Status = document.getElementById('drpSelectStatus').value;\r\n" +
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.AssetSupplier.ServerSideDrawUpdateForm(RenderInfo, AssetSupplierId, ServiceTypeId, FilterTypeId, Status, Keyword, 0).value;\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                    "       document.getElementById('divActionForm').innerHTML='';\r\n" +
                    "       document.getElementById('divActionForm').style.display='none';\r\n" +
                    "       document.getElementById('divListForm').style.display='block';\r\n" +
                    "   }\r\n" +

                    "   function CallActionAdd()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       OwnerId= document.getElementById('txtOwnerId').value;\r\n" +
                    "       CityId= document.getElementById('drpSelectCity').value;\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       AssetSupplierCode = document.getElementById('txtAssetSupplierCode').value;\r\n" +
                    "       AssetSupplierName = document.getElementById('txtAssetSupplierName').value;\r\n" +
                    "       Address = document.getElementById('txtAddress').value;\r\n" +
                    "       Hotline = document.getElementById('txtHotline').value;\r\n" +
                    "       Fax = document.getElementById('txtFax').value;\r\n" +
                    "       Vat = document.getElementById('txtVat').value;\r\n" +
                    "       LicenceSale = document.getElementById('txtLicenceSale').value;\r\n" +
                    "       WebSite = document.getElementById('txtWebSite').value;\r\n" +
                    "       GoogleAddress = document.getElementById('txtGoogleAddress').value;\r\n" +
                    "       AssetSupplierLevel = document.getElementById('txtAssetSupplierLevel').value;\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.AssetSupplier.ServerSideAdd(RenderInfo, CityId, OwnerId, ServiceTypeId, AssetSupplierCode, AssetSupplierName, Address, Hotline, Fax, Vat, LicenceSale, WebSite, GoogleAddress, AssetSupplierLevel, SortIndex, Active).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallReading();\r\n" +
                    "       document.getElementById('txtAssetSupplierName').value='';\r\n" +
                    "       document.getElementById('txtAddress').value='';\r\n" +
                    "       document.getElementById('txtHotline').value='';\r\n" +
                    "       document.getElementById('txtFax').value='';\r\n" +
                    "       document.getElementById('txtVat').value='';\r\n" +
                    "       document.getElementById('txtLicenceSale').value='';\r\n" +
                    "       document.getElementById('txtWebSite').value='';\r\n" +
                    "       document.getElementById('txtGoogleAddress').value='';\r\n" +
                    "       document.getElementById('txtAssetSupplierLevel').value='';\r\n" +
                    "       document.getElementById('txtAssetSupplierName').focus();\r\n" +
                    "       document.getElementById('txtSortIndex').value=parseInt(SortIndex,10)+1;\r\n" +
                    "   }\r\n" +


                    "   function CallActionUpdate(AssetSupplierId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       CityId= document.getElementById('drpSelectCity').value;\r\n" +
                    "       AssetSupplierCode = document.getElementById('txtAssetSupplierCode').value;\r\n" +
                    "       AssetSupplierName = document.getElementById('txtAssetSupplierName').value;\r\n" +
                    "       Address = document.getElementById('txtAddress').value;\r\n" +
                    "       Hotline = document.getElementById('txtHotline').value;\r\n" +
                    "       Fax = document.getElementById('txtFax').value;\r\n" +
                    "       Vat = document.getElementById('txtVat').value;\r\n" +
                    "       LicenceSale = document.getElementById('txtLicenceSale').value;\r\n" +
                    "       WebSite = document.getElementById('txtWebSite').value;\r\n" +
                    "       GoogleAddress = document.getElementById('txtGoogleAddress').value;\r\n" +
                    "       AssetSupplierLevel = document.getElementById('txtAssetSupplierLevel').value;\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.AssetSupplier.ServerSideUpdate(RenderInfo, AssetSupplierId, CityId, AssetSupplierCode, AssetSupplierName, Address, Hotline, Fax, Vat, LicenceSale, WebSite, GoogleAddress, AssetSupplierLevel, SortIndex, Active).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallReading();\r\n" +
                    "       CallBack();\r\n" +
                    "   }\r\n" +

                    "   function CallActionDelete(AssetSupplierId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa nhà cung cấp này ra khỏi hệ thống") + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +


                    "           AjaxOut = OneTSQ.WebParts.AssetSupplier.ServerSideDelete(RenderInfo, AssetSupplierId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    //"           CallReading();\r\n" +
                    "           document.getElementById('tr'+AssetSupplierId).style.display='none';\r\n"+
                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "nhà cung cấp đã được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +

                    "   }\r\n" +
                    "</script>\r\n") +
                    WebEnvironments.EncryptHtml(
                        "<input id=\"txtPageIndex\" type=\"hidden\" value=\"" + PageIndex+ "\">\r\n" +
                        "<input id=\"txtOwnerId\" type=\"hidden\" value=\"" + WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId + "\">\r\n" +
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +
                        "     <div class=\"ibox-title\"> \r\n" +
                        "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh mục nhà cung cấp") + " </h5> \r\n" +
                        "     </div> \r\n" +
                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "             <div class=\"col-md-3\" style=\"display:none\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Phạm vi lọc") + "</div>\r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + SelectFilterTypeText + "</div>\r\n" +
                        "             </div>\r\n" +
                        "             <div class=\"col-md-4\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Loại dịch vụ") + "</div>\r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + SelectTypeText + "</div>\r\n" +
                        "             </div>\r\n" +
                        "             <div class=\"col-md-4\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Loại dịch vụ") + "</div>\r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + SelectStatusText + "</div>\r\n" +
                        "             </div>\r\n" +
                        "             <div class=\"col-md-4\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                        "                 <div><input onkeypress=\"if(event.keyCode==13){OpenLink();}\"  id=\"txtKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" value=\"" + Keyword + "\" class=\"input-sm form-control\"></div>\r\n" +
                        "             </div>\r\n" +
                        "         </div>\r\n" +
                        "                <div class=\"row\">\r\n" +
                        "                   <div class=\"col-md-12\">\r\n" +
                        "                       <button onclick=\"javascript:OpenLink();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Lọc") + "</button>\r\n" +
                        "                       <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:CallAddForm();\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                        "                   </div>\r\n" +
                        "                </div> \r\n" +
                        "             </div> \r\n" +
                        "         </div> \r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n" +
                        "               <div id=\"divAssetSupplierContent\">" + ServerSideDrawSearchResult(ORenderInfo, OwnerId, ServiceTypeId, FilterTypeId, "", int.Parse(Status), int.Parse(PageIndex)).HtmlContent + "</div>\r\n" +
                        "         </div> \r\n" +
                        "     </div> \r\n" +
                        " </div> \r\n" +

                        "</div>\r\n" +
                        "<div id=\"divActionForm\" style=\"display:none\"></div>\r\n"
                        ) +
                        "<script>\r\n" +
                        "   $('#drpSelectServiceType').select2();\r\n"+
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
        public static AjaxOut ServerSideDrawSearchResult(
            RenderInfoCls ORenderInfo, 
            string OwnerId, 
            string ServiceTypeId, 
            string FilterTypeId,
            string Keyword,
            int Status,
            int PageIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                AssetSupplierFilterCls
                    OAssetSupplierFilter = new AssetSupplierFilterCls();
                OAssetSupplierFilter.Keyword = Keyword;
                OAssetSupplierFilter.ServiceTypeId = ServiceTypeId;
                OAssetSupplierFilter.Status = Status;
                if (FilterTypeId.Equals("0"))
                {
                    OAssetSupplierFilter.OwnerId = OwnerId;
                }
                AssetSupplierCls[] 
                    AssetSuppliers = CallBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Reading(ORenderInfo, OAssetSupplierFilter);
                string Html = "";
                if (AssetSuppliers.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có dữ liệu theo điều kiện lọc", true);
                }
                else
                {
                    ReturnPaging
                        OReturnPaging = WebPaging.GetPaging(AssetSuppliers.Length, PageIndex, 25, 10, "NextPage");
                    Html +=
                        "   <div class=\"search-result-info\">"+WebLanguage.GetLanguage(OSiteParam,"Tìm thấy")+" ("+AssetSuppliers.Length.ToString("#,##0")+") "+WebLanguage.GetLanguage(OSiteParam,"dữ liệu theo điều kiện lọc")+"</div>\r\n"+
                        OReturnPaging.PagingText+
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Owner") + " </th> \r\n" +
                        "                     <th style=\"width:70px\">" + WebLanguage.GetLanguage(OSiteParam, "T.Phố") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên nhà cung cấp") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Điện thoại") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = OReturnPaging.StartIndex; iIndex < OReturnPaging.EndIndex; iIndex++)
                    {
                        Html +=
                            "                 <tr id=\"tr" + AssetSuppliers[iIndex].AssetSupplierId + "\"> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td>" + AssetSuppliers[iIndex].OwnerCode + "</td> \r\n" +
                            "                     <td>" + AssetSuppliers[iIndex].CityName + "</td> \r\n" +
                            "                     <td>" + AssetSuppliers[iIndex].AssetSupplierCode + "</td> \r\n" +
                            "                     <td style=\"width:200px\">" + AssetSuppliers[iIndex].AssetSupplierName + "</td> \r\n" +
                            "                     <td>" + AssetSuppliers[iIndex].Address + "</td> \r\n" +
                            "                     <td>" + AssetSuppliers[iIndex].Hotline + "</td> \r\n" +

                            "                     <td style=\"width:120px;text-align:center\">" + (AssetSuppliers[iIndex].Active == 1 ? WebLanguage.GetLanguage(OSiteParam, "Sử dụng") : WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng")) + "</td> \r\n";

                        if (AssetSuppliers[iIndex].frkOwnerId.Equals(OwnerId))
                        {
                            Html +=
                                "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Sửa nhà cung cấp") + "\" href=\"javascript:CallUpdateForm('" + AssetSuppliers[iIndex].AssetSupplierId + "');\"><i class=\"" + WebScreen.GetEditGridIcon() + "\"></i></a></td> \r\n" +
                                "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa nhà cung cấp") + "\" href=\"javascript:CallActionDelete('" + AssetSuppliers[iIndex].AssetSupplierId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n";
                        }
                        else
                        {
                            Html +=
                                "                     <td class=\"td-center\"></td> \r\n" +
                                "                     <td class=\"td-center\"></td> \r\n";
                        }
                        Html+=
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n"+
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
        public static AjaxOut ServerSideDrawAddForm(
            RenderInfoCls ORenderInfo,
            string ServiceTypeId,
            string FilterTypeId,
            string Status,
            string Keyword,
            int PageIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new AddAssetSupplier().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("ServiceTypeId",ServiceTypeId),
                    new WebParamCls("FilterTypeId",FilterTypeId),
                    new WebParamCls("Status",Status),
                    new WebParamCls("Keyword",Keyword),
                    new WebParamCls("PageIndex",PageIndex),
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
        public static AjaxOut ServerSideDrawUpdateForm(
            RenderInfoCls ORenderInfo,
            string AssetSupplierId,
            string ServiceTypeId,
            string FilterTypeId,
            string Status,
            string Keyword,
            int PageIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new UpdateAssetSupplier().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("AssetSupplierId",AssetSupplierId),
                    new WebParamCls("ServiceTypeId",ServiceTypeId),
                    new WebParamCls("FilterTypeId",FilterTypeId),
                    new WebParamCls("Status",Status),
                    new WebParamCls("Keyword",Keyword),
                    new WebParamCls("PageIndex",PageIndex),
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
        public static AjaxOut ServerSideDelete(
            RenderInfoCls ORenderInfo,
            string AssetSupplierId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                
                  

                CallBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Delete(ORenderInfo, AssetSupplierId);
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
        public static AjaxOut ServerSideOpenLink(
            RenderInfoCls ORenderInfo,
            string ServiceTypeId,
            string FilterTypeId,
            string Status,
            string Keyword,
            int PageIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new AssetSupplier().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("ServiceTypeId",ServiceTypeId),
                    new WebParamCls("FilterTypeId",FilterTypeId),
                    new WebParamCls("Status",Status),
                    new WebParamCls("Keyword",Keyword),
                    new WebParamCls("PageIndex",PageIndex),
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

        public override bool RunAlone
        {
            get
            {
                return false;
            }
        }
    }
}
