using OneTSQ.Bussiness.Utility;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.Utility;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebParts
{
    public class Service : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "Service";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Danh mục dịch vụ";
            }
        }

        public override string Description
        {
            get
            {
                return "Danh mục dịch vụ";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(Service),Page);
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


                string servicetypeid = (string)WebEnvironments.Request("servicetypeid");
                if (string.IsNullOrEmpty(servicetypeid)) servicetypeid = "";
                string assetsupplierid = (string)WebEnvironments.Request("assetsupplierid");
                if (string.IsNullOrEmpty(assetsupplierid)) assetsupplierid = "";
                string filtertype = (string)WebEnvironments.Request("filtertype");
                if (string.IsNullOrEmpty(filtertype)) filtertype = "1";
                if (FunctionUtility.checkInteger(filtertype) == false) filtertype = "1";
                string keyword = (string)WebEnvironments.Request("keyword");
                if (string.IsNullOrEmpty(keyword)) keyword = "";
                string PageIndex = (string)WebEnvironments.Request("pageindex");
                if (string.IsNullOrEmpty(PageIndex)) PageIndex = "0";
                if (FunctionUtility.checkInteger(PageIndex) == false)
                {
                    PageIndex = "0";
                }
                int iPageIndex = int.Parse(PageIndex);

                ServiceTypeFilterCls
                    OServiceTypeFilter = new ServiceTypeFilterCls();
                OServiceTypeFilter.ActiveOnly = 1;
                
                    
                ServiceTypeCls[]
                    ServiceTypes = CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeProcess().Reading(ORenderInfo, OServiceTypeFilter);

                
                string SelectServiceTypeText = "<select style=\"width:100%\"  onchange=\"javascript:CallReading();\" id=\"drpSelectServiceType\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < ServiceTypes.Length; iIndex++)
                {
                    if (!string.IsNullOrEmpty(servicetypeid))
                    {
                        if (ServiceTypes[iIndex].ServiceTypeId.ToLower().Equals(servicetypeid.ToLower()))
                        {
                            SelectServiceTypeText +=
                                "   <option selected value=\"" + ServiceTypes[iIndex].ServiceTypeId + "\">" + ServiceTypes[iIndex].ServiceTypeName + "</option>\r\n";
                        }
                        else
                        {
                            SelectServiceTypeText +=
                                "   <option value=\"" + ServiceTypes[iIndex].ServiceTypeId + "\">" + ServiceTypes[iIndex].ServiceTypeName + "</option>\r\n";
                        }
                    }
                    else
                    {
                        SelectServiceTypeText +=
                            "   <option value=\"" + ServiceTypes[iIndex].ServiceTypeId + "\">" + ServiceTypes[iIndex].ServiceTypeName + "</option>\r\n";
                        if (iIndex == 0)
                        {
                            servicetypeid = ServiceTypes[iIndex].ServiceTypeId;
                        }
                    }
                }
                SelectServiceTypeText += "</select>\r\n";

                AssetSupplierFilterCls
                   OAssetSupplierFilter = new AssetSupplierFilterCls();
                OAssetSupplierFilter.ActiveOnly = 1;
                OAssetSupplierFilter.ServiceTypeId = servicetypeid;
                AssetSupplierCls[]
                    AssetSuppliers = CallBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Reading(ORenderInfo, OAssetSupplierFilter);

                string SelectAssetSupplierText =
                    " <select style=\"width:100%\" onchange=\"javascript:CallReading();\" id=\"drpSelectFilterAssetSupplier\" class=\"form-control select2\">\r\n" +
                    "       <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ nhà cung cấp") + "</option>\r\n";
                for (int iIndex = 0; iIndex < AssetSuppliers.Length; iIndex++)
                {
                    if (AssetSuppliers[iIndex].AssetSupplierId.ToLower().Equals(assetsupplierid.ToLower()))
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

                string SelectFilterTypeText =
                    " <select id=\"drpSelectFilterType\" class=\"form-control\" onchange=\"javascript:CallReading();\" onchange=\"javascript:CallReading();\">\r\n" +
                    "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n" +
                    "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Chưa thiết lập dịch vụ chuẩn") + "</option>\r\n" +
                    "   <option value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Đã thiết lập dịch vụ chuẩn") + "</option>\r\n" +
                    " </select>\r\n";
                if (int.Parse(filtertype) == 1)
                {
                    SelectFilterTypeText =
                    " <select id=\"drpSelectFilterType\" class=\"form-control\" onchange=\"javascript:CallReading();\">\r\n" +
                    "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n" +
                    "   <option selected value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Chưa thiết lập dịch vụ chuẩn") + "</option>\r\n" +
                    "   <option value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Đã thiết lập dịch vụ chuẩn") + "</option>\r\n" +
                    " </select>\r\n";
                }
                if (int.Parse(filtertype) == 2)
                {
                    SelectFilterTypeText =
                    " <select id=\"drpSelectFilterType\" class=\"form-control\" onchange=\"javascript:CallReading();\">\r\n" +
                    "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n" +
                    "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Chưa thiết lập dịch vụ chuẩn") + "</option>\r\n" +
                    "   <option selected value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Đã thiết lập dịch vụ chuẩn") + "</option>\r\n" +
                    " </select>\r\n";
                }

                string SelectStatusText =
                  " <select id=\"drpSelectStatus\" class=\"form-control\" onchange=\"javascript:CallReading();\">\r\n" +
                  "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Đang sử dụng") + "</option>\r\n" +
                  "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng") + "</option>\r\n" +
                  "   <option value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n" +
                  " </select>\r\n";

                string status = (string)WebEnvironments.Request("status");
                if (string.IsNullOrEmpty(status)) status = "1";
                if (FunctionUtility.checkInteger(status) == false) status = "1";
                if (status.Equals("0"))
                {
                    SelectStatusText =
                      " <select id=\"drpSelectStatus\" class=\"form-control\" onchange=\"javascript:CallReading();\">\r\n" +
                      "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Đang sử dụng") + "</option>\r\n" +
                      "   <option selected value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng") + "</option>\r\n" +
                      "   <option value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n" +
                      " </select>\r\n";
                }

                if (status.Equals("2"))
                {
                    SelectStatusText =
                      " <select id=\"drpSelectStatus\" class=\"form-control\" onchange=\"javascript:CallReading();\">\r\n" +
                      "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Đang sử dụng") + "</option>\r\n" +
                      "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng") + "</option>\r\n" +
                      "   <option selected value=\"2\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n" +
                      " </select>\r\n";
                }

                string OwnerUserId=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string SessionId = System.Guid.NewGuid().ToString();
 
                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +
                    "   function CallAddForm()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ServiceTypeId=document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.Service.ServerSideAddService(RenderInfo, ServiceTypeId).value;\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n" +


                    "   function CallUpdateForm(ServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.Service.ServerSideUpdateService(RenderInfo, ServiceId).value;\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                  
                    "   function CallActionDelete(ServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa dịch vụ này ra khỏi hệ thống") + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +


                    "           AjaxOut = OneTSQ.WebParts.Service.ServerSideDelete(RenderInfo, ServiceId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           CallReading();\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "dịch vụ đã được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +

                    "   }\r\n" +


                    "   function NextPage(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       AssetSupplierId = document.getElementById('drpSelectFilterAssetSupplier').value;\r\n" +
                    "       FilterType = parseInt(document.getElementById('drpSelectFilterType').value,10);\r\n" +
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       Status = document.getElementById('drpSelectStatus').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.Service.ServerSideBuildUrl(RenderInfo, ServiceTypeId, AssetSupplierId, FilterType, Keyword, Status, PageIndex).value;\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n" +

                    "   function CallReading()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       AssetSupplierId = document.getElementById('drpSelectFilterAssetSupplier').value;\r\n" +
                    "       FilterType = parseInt(document.getElementById('drpSelectFilterType').value,10);\r\n" +
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       Status = document.getElementById('drpSelectStatus').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.Service.ServerSideBuildUrl(RenderInfo, ServiceTypeId, AssetSupplierId, FilterType, Keyword, Status, 0).value;\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n" +

                    "   function ImportFromExcel()\r\n"+
                    "   {\r\n"+
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.Service.ServerSideDrawImportFromExcel(RenderInfo).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n"+
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "   }\r\n"+

                    "   function fileSelected()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       var fp = $('#fileUpload');\r\n" +
                    "       var items = fp[0].files;\r\n" +

                    "       var fileName = items[0].name; \r\n" +
                    "       var fileSize = items[0].size; \r\n" +
                    "       var fileType = items[0].type;  \r\n" +

                    "       var Info = '<div style=\"padding:4px;background-color:lightyellow;padding:4px;color:maroon;border-radius:5px\">'+ fileName + ' (<b>' + fileSize + '</b> bytes) - Type :' + fileType+'</div>';\r\n" +
                    "       document.getElementById('divUploadFileInfo').innerHTML=Info;\r\n" +
                    "   }\r\n" +

                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('divActionForm').innerHTML='';\r\n" +
                    "       document.getElementById('divActionForm').style.display='none';\r\n" +
                    "       document.getElementById('divListForm').style.display='block';\r\n" +
                    "   }\r\n" +


                    "   function UploadImportExcel()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('divProcessingUploadExcel').innerHTML='"+WebLanguage.GetLanguage(OSiteParam,"Đang tiến hành upload...")+"';\r\n"+
                    "       setTimeout('RealUploadImportExcel()',10);\r\n"+
                    "   }\r\n" +

                    "   function RealUploadImportExcel()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "        var file = document.getElementById('fileUpload').files[0];\r\n" +
                    "        if (file) {\r\n" +
                    //"        fileUpload=document.getElementById('fileUpload').value;\r\n" +

                    "        var fd = new FormData();\r\n" +
                    "        fd.append(\"fileUpload\", document.getElementById('fileUpload').files[0]);\r\n" +
                    "        var xhr = new XMLHttpRequest();\r\n" +
                    "        xhr.addEventListener(\"load\", uploadComplete, false);\r\n" +
                    "        xhr.open(\"POST\", \"" + WebConfig.GetUploadHandler(OSiteParam, SessionId, OwnerUserId, LoginName) + "\");\r\n" +
                    "        xhr.send(fd);\r\n" +

                    "        }\r\n" +
                    "   }\r\n" +

                    "   function uploadComplete(evt)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       XmlInfoResult='';\r\n" +
                    "       if(evt!=null)XmlInfoResult=evt.target.responseText;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.Service.ServerSideImportFromExcel(RenderInfo, XmlInfoResult).value;\r\n" +
                    "       document.getElementById('divProcessingUploadExcel').innerHTML='';\r\n"+
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "   }\r\n" +



                    "</script>\r\n") +
                    WebEnvironments.EncryptHtml(
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +
                        "     <div class=\"ibox-title\"> \r\n" +
                        "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh mục dịch vụ") + " </h5> \r\n" +
                        "     </div> \r\n" +
                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "             <div class=\"col-md-3\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Loại hình dịch vụ") + "</div>\r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + SelectServiceTypeText + "</div>\r\n" +
                        "             </div>\r\n" +
                        "             <div class=\"col-md-3\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Nhà cung cấp") + "</div>\r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + SelectAssetSupplierText + "</div>\r\n" +
                        "             </div>\r\n" +
                        "             <div class=\"col-md-2\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Loại") + "</div>\r\n" +
                        "                 <div>" + SelectFilterTypeText + "</div>\r\n" +
                        "             </div> \r\n" +
                        "             <div class=\"col-md-2\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</div>\r\n" +
                        "                 <div>" + SelectStatusText + "</div>\r\n" +
                        "             </div> \r\n" +
                        "             <div class=\"col-md-2\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                        "                 <div><input onkeypress=\"if(event.keyCode==13){CallReading();}\" value=\"" + keyword + "\"  id=\"txtKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                        "             </div> \r\n" +
                        "         </div> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "                 <div class=\"col-md-12\">\r\n" +
                        "                       <button onclick=\"javascript:CallReading();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Lọc") + "</button>\r\n" +
                        "                       <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:CallAddForm();\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                        "                       <button onclick=\"javascript:window.open('/ExcelTemplates/Services.xlsx','_blank');\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Download file mẫu") + "</button>\r\n" +
                        "                       <button onclick=\"javascript:ImportFromExcel();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Nhập từ Excel") + "</button>\r\n" +
                        "                 </div> \r\n" +
                        "         </div>\r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n" +
                        "               <div id=\"divServiceContent\">" + ServerSideDrawSearchResult(ORenderInfo, servicetypeid, assetsupplierid, int.Parse(filtertype), keyword, int.Parse(status), iPageIndex).HtmlContent + "</div>\r\n" +
                        "         </div> \r\n" +
                        "     </div> \r\n" +
                        " </div> \r\n" +

                        "</div>\r\n" +
                        "<div id=\"divActionForm\" style=\"display:none\"></div>\r\n"
                        ) +
                        "<script>\r\n" +
                        "   $('#drpSelectFilterAssetSupplier').select2();\r\n" +
                        "   $('#drpSelectServiceType').select2();\r\n" +
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
        public static AjaxOut ServerSideBuildUrl(
            RenderInfoCls ORenderInfo,
            string ServiceTypeId,
            string AssetSupplierId,
            int FilterType,
            string Keyword,
            string Status,
            int PageIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode,
                    new Service().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("ServiceTypeId",ServiceTypeId),
                        new WebParamCls("AssetSupplierId",AssetSupplierId),
                        new WebParamCls("FilterType",FilterType),
                        new WebParamCls("Keyword",Keyword.Replace(" ","+")),
                        new WebParamCls("PageIndex",PageIndex),
                        new WebParamCls("Status",Status),
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
        public static AjaxOut ServerSideDrawSearchResult(
            RenderInfoCls ORenderInfo,
            string ServiceTypeId,
            string AssetSupplierId,
            int FilterType,
            string Keyword,
            int Status,
            int iPageIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);


                WebSession.CheckSessionTimeOut(ORenderInfo);

                ServiceFilterCls
                    OServiceFilter = new ServiceFilterCls();
                OServiceFilter.ServiceTypeId = ServiceTypeId;
                OServiceFilter.AssetSupplierId = AssetSupplierId;
                OServiceFilter.Keyword = Keyword;
                OServiceFilter.FilterType = FilterType;
                OServiceFilter.Status = Status;

                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ServiceCls[] 
                    Services = CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().Reading(ORenderInfo, OServiceFilter);
                string Html = "";
                if (Services.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có dữ liệu theo điều kiện lọc", true);
                }
                else
                {
                    ReturnPaging
                        OReturnPaging = WebPaging.GetPaging(Services.Length, iPageIndex, 25, 10, "NextPage");
                    Html +=
                        "   <div class=\"search-result-info\">"+WebLanguage.GetLanguage(OSiteParam,"Tìm thấy")+" ("+Services.Length.ToString("#,##0")+") "+WebLanguage.GetLanguage(OSiteParam,"dữ liệu theo điều kiện lọc")+"</div>\r\n"+
                        OReturnPaging.PagingText+
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Loại") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "T.Phố") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Nhà cung cấp") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ") + " </th> \r\n" +
                        "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Tiền") + " </th> \r\n" +
                        "                     <th style=\"text-align:right\">" + WebLanguage.GetLanguage(OSiteParam, "Mua") + " </th> \r\n" +
                        "                     <th style=\"text-align:right\">" + WebLanguage.GetLanguage(OSiteParam, "Bán") + " </th> \r\n" +
                        "                     <th style=\"text-align:right\">" + WebLanguage.GetLanguage(OSiteParam, "Web") + " </th> \r\n" +
                        "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "T.Thái") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = OReturnPaging.StartIndex; iIndex < OReturnPaging.EndIndex; iIndex++)
                    {
                        string ViewServiceUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new ViewService().WebPartId,
                            new WebParamCls[]
                            {
                                new WebParamCls("ServiceId",Services[iIndex].ServiceId)
                            });

                        string StdServiceName = Services[iIndex].StdServiceName;
                        if (string.IsNullOrEmpty(StdServiceName))
                        {
                            StdServiceName = "<a style=\"color:maroon;font-weight:bold;font-size:10px\">"+ WebLanguage.GetLanguage(OSiteParam, "Chưa thiết lập dịch vụ chuẩn")+"</a>\r\n";
                        }
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td><a target=\"_self\" href=\"" + ViewServiceUrl + "\">" + Services[iIndex].ServiceTypeName + "</a></td> \r\n" +
                            "                     <td>" + Services[iIndex].CityName + "</td> \r\n" +
                            "                     <td>" + Services[iIndex].AssetSupplierName + "</td> \r\n" +
                            "                     <td><a href=\"javascript:CallUpdateForm('" + Services[iIndex].ServiceId + "');\"><span style=\"color:green;font-weight:bold\">" + Services[iIndex].ServiceName + "</span></a><div>"+StdServiceName+"</div></td> \r\n" +
                            "                     <td>" + Services[iIndex].CurrencyCode + "</td> \r\n" +
                            "                     <td style=\"text-align:right;color:green\">" + FunctionUtility.FormatNumber(Services[iIndex].BuyPrice) + "</td> \r\n" +
                            "                     <td style=\"text-align:right;color:green\">" + FunctionUtility.FormatNumber(Services[iIndex].SalePrice) + "</td> \r\n" +
                            "                     <td style=\"text-align:right;color:green\">" + FunctionUtility.FormatNumber(Services[iIndex].WebPrice) + "</td> \r\n" +
                            "                     <td style=\"text-align:center;color:maroon;font-weight:bold\">" + (Services[iIndex].Active == 1 ? "A" : "D") + "</td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Sửa dịch vụ") + "\" href=\"javascript:CallUpdateForm('" + Services[iIndex].ServiceId + "');\"><i class=\""+WebScreen.GetEditGridIcon()+"\"></i></a></td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa dịch vụ") + "\" href=\"javascript:CallActionDelete('" + Services[iIndex].ServiceId + "');\"><i class=\""+WebScreen.GetDeleteGridIcon()+"\"></i></a></td> \r\n" +
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
        public static AjaxOut ServerSideAddService(RenderInfoCls ORenderInfo, string ServiceTypeId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode,
                    new AddService().WebPartId,
                    new WebParamCls[]
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
        public static AjaxOut ServerSideUpdateService(RenderInfoCls ORenderInfo, string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode,
                    new UpdateService().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("ServiceId",ServiceId)
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
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                
                  

                CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().Delete(ORenderInfo, ServiceId);
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
        public static AjaxOut ServerSideDrawImportFromExcel(
            RenderInfoCls ORenderInfo,
            string ServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerId;
                string Html =
                     " <div class=\"ibox-content\"> \r\n" +
                     "     <div class=\"row\"> \r\n" +
                     "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Nhập dữ liệu từ Excel") + "</h3> \r\n" +
                     "             <div class=\"row\"> \r\n" +
                     "                <div class=\"col-md-6\">\r\n" +
                     "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Chọn Excel") + "</label> <input onchange=\"fileSelected()\" type=file id=\"fileUpload\" name=\"fileUpload\"><div id=\"divUploadFileInfo\"></div></div> \r\n" +
                     "                    <div class=\"form-group\"><label></label><div id=divProcessingExcel></div<</div> \r\n" +
                     "                </div>\r\n" +
                     "            </div>\r\n" +
                     "                <div id=\"divProcessingUploadExcel\" style=\"height:20px;color:green;font-weight:bold; margin-bottom:20px\"></div>\r\n"+
                     "                 <div> \r\n" +
                     "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:UploadImportExcel();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                     "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                     "                 </div> \r\n" +
                     "             </div> \r\n" +
                     "         </div> \r\n" +
                     "     </div> \r\n" +
                     " </div> \r\n";
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
        public static AjaxOut ServerSideImportFromExcel(
            RenderInfoCls ORenderInfo,
            string XmlInfoResult)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            Workbook workbook = null;
            Worksheet sheet = null;

            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerId;
                UploadHandlerResultCls
                    OUploadHandlerResult = UploadHandlerResultParser.ParseFromXml(XmlInfoResult);
                if (OUploadHandlerResult.Error) throw new Exception(OUploadHandlerResult.InfoMessage);

                //thực hiện excel ở đay....!
                workbook = new Workbook();
                workbook.LoadFromFile(OUploadHandlerResult.SaveFile);

                sheet = workbook.Worksheets["DVT"];
                //đầu tiên làm danh sách dvt
                for (int iIndex = 2; iIndex <= sheet.Rows.Length; iIndex++)
                {
                    string Code = sheet[iIndex, 1].Value;
                    if (!string.IsNullOrEmpty(Code))
                    {
                        string Name = sheet[iIndex, 2].Value;
                        UnitCls
                            OUnit = new UnitCls();
                        OUnit.UnitId = System.Guid.NewGuid().ToString();
                        OUnit.UnitName = Name;
                        OUnit.UnitCode = Code;
                        OUnit.Active = 1;
                        CallBussinessUtility.CreateBussinessProcess().CreateUnitProcess().Add(ORenderInfo, OUnit);
                    }
                }

                sheet = workbook.Worksheets["QUOCGIA"];
                //đầu tiên làm danh sách dvt
                for (int iIndex =2; iIndex <= sheet.Rows.Length; iIndex++)
                {
                    string Code = sheet[iIndex, 1].Value;
                    if (!string.IsNullOrEmpty(Code))
                    {
                        string Name = sheet[iIndex, 2].Value;
                        CountryCls
                            OCountry = new CountryCls();
                        OCountry.CountryId = System.Guid.NewGuid().ToString();
                        OCountry.CountryName = Name;
                        OCountry.CountryCode = Code;
                        OCountry.Active = 1;
                        CallBussinessUtility.CreateBussinessProcess().CreateCountryProcess().Add(ORenderInfo, OCountry);
                    }
                }

                sheet = workbook.Worksheets["LOAITIEN"];
                //đầu tiên làm danh sách dvt
                for (int iIndex = 2; iIndex <= sheet.Rows.Length; iIndex++)
                {
                    string Code = sheet[iIndex, 1].Value;
                    if (!string.IsNullOrEmpty(Code))
                    {
                        string Name = sheet[iIndex, 2].Value;
                        CurrencyCls
                            OCurrency = new CurrencyCls();
                        OCurrency.CurrencyId = System.Guid.NewGuid().ToString();
                        OCurrency.CurrencyName = Name;
                        OCurrency.CurrencyCode = Code;
                        OCurrency.Active = 1;
                        CallBussinessUtility.CreateBussinessProcess().CreateCurrencyProcess().Add(ORenderInfo, OCurrency);
                    }
                }

                sheet = workbook.Worksheets["THANHPHO"];
                //đầu tiên làm danh sách dvt
                for (int iIndex = 2; iIndex <= sheet.Rows.Length; iIndex++)
                {
                    string Code = sheet[iIndex, 1].Value;
                    if (!string.IsNullOrEmpty(Code))
                    {
                        string Name = sheet[iIndex, 2].Value;
                        string CountryCode = sheet[iIndex, 3].Value;
                        CountryCls
                            OCountry = CallBussinessUtility.CreateBussinessProcess().CreateCountryProcess().CreateModel(ORenderInfo, CountryCode);
                        if (OCountry != null)
                        {
                            CityCls
                                OCity = new CityCls();
                            OCity.CityId = System.Guid.NewGuid().ToString();
                            OCity.CityName = Name;
                            OCity.CityCode = Code;
                            OCity.frkCountryId = OCountry.CountryId;
                            OCity.Active = 1;
                            CallBussinessUtility.CreateBussinessProcess().CreateCityProcess().Add(ORenderInfo, OCity);
                        }
                    }
                }


                sheet = workbook.Worksheets["NCC"];
                //đầu tiên làm danh sách dvt
                for (int iIndex = 2; iIndex <= sheet.Rows.Length; iIndex++)
                {
                    string Code = sheet[iIndex, 1].Value;
                    if (!string.IsNullOrEmpty(Code))
                    {
                        string Name = sheet[iIndex, 2].Value;
                        string Address = sheet[iIndex, 3].Value;
                        string Hotline = sheet[iIndex, 4].Value;
                        string City = sheet[iIndex, 5].Value;
                        CityCls
                            OCity = CallBussinessUtility.CreateBussinessProcess().CreateCityProcess().CreateModel(ORenderInfo, City);

                        if (OCity != null)
                        {
                            AssetSupplierCls
                                OAssetSupplier = new AssetSupplierCls();
                            OAssetSupplier.AssetSupplierId = System.Guid.NewGuid().ToString();
                            OAssetSupplier.AssetSupplierName = Name;
                            OAssetSupplier.AssetSupplierCode = Code;
                            OAssetSupplier.Address = Address;
                            OAssetSupplier.Hotline = Hotline;
                            OAssetSupplier.frkCityId = OCity.CityId;
                            OAssetSupplier.frkOwnerId = OwnerId;
                            OAssetSupplier.Active = 1;
                            CallBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Add(ORenderInfo, OAssetSupplier);
                        }
                    }
                }

                sheet = workbook.Worksheets["DICHVU"];
                for (int iIndex = 2; iIndex <= sheet.Rows.Length; iIndex++)
                {
                    string AssetSupplierCode = sheet[iIndex, 2].Value;
                    if (!string.IsNullOrEmpty(AssetSupplierCode))
                    {
                        string ServiceCode = sheet[iIndex, 3].Value;
                        string ServiceName = sheet[iIndex, 4].Value;
                        string Description = sheet[iIndex, 5].Value;
                        string CurrencyCode = sheet[iIndex, 6].Value;
                        string Unit = sheet[iIndex, 7].Value;
                        decimal BuyPrice = decimal.Parse(sheet[iIndex, 8].Value.ToString());
                        decimal SalePrice = decimal.Parse(sheet[iIndex, 9].Value.ToString());
                        decimal WebPrice = decimal.Parse(sheet[iIndex, 10].Value2.ToString());
                        string ServiceType = sheet[iIndex, 11].Value;


                        ServiceTypeCls
                            OServiceType = CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeProcess().CreateModel(ORenderInfo, ServiceType);

                        AssetSupplierCls
                            OAssetSupplier = CallBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().CreateModel(ORenderInfo, AssetSupplierCode);

                        UnitCls
                            OUnit = CallBussinessUtility.CreateBussinessProcess().CreateUnitProcess().CreateModel(ORenderInfo, Unit);
                      
                        CurrencyCls
                           OCurrency = CallBussinessUtility.CreateBussinessProcess().CreateCurrencyProcess().CreateModel(ORenderInfo, CurrencyCode);

                        if (OAssetSupplier != null && OUnit != null && OCurrency != null && OServiceType!=null)
                        {
                            ServiceCls
                                OService = new ServiceCls();
                            OService.ServiceId = System.Guid.NewGuid().ToString();
                            OService.ServiceCode = ServiceCode;
                            OService.ServiceName = ServiceName;
                            OService.ShortDescription = Description;
                            OService.Description = Description;
                            OService.frkAssetSupplierId = OAssetSupplier.AssetSupplierId;
                            OService.frkOwnerId = OwnerId;
                            OService.SalePrice = SalePrice;
                            OService.BuyPrice = BuyPrice;
                            OService.WebPrice = WebPrice;
                            OService.frkUnitId = OUnit.UnitId;
                            OService.frkCurrencyId = OCurrency.CurrencyId;
                            OService.frkServiceTypeId = OServiceType.ServiceTypeId;
                            
                            CallBussinessUtility.CreateBussinessProcess().CreateServiceProcess().Add(ORenderInfo, OService);
                        }
                    }
                }

                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Cập nhật dịch vụ từ file Excel thành công");
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
