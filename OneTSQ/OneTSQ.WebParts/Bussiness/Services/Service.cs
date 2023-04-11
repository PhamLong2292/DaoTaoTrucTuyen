using OnlineTour.Bussiness.Utility;
using OnlineTour.Model;
using OnlineTour.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTour.WebParts
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

        public override AjaxOut CheckPermission(SiteParam OSiteParam)
        {
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
                AjaxOut RetAjaxOutCheckPermission = CheckPermission(OSiteParam);
                if (RetAjaxOutCheckPermission.RetBoolean == false)
                {
                    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Không có quyền truy cập tính năng này"), false);
                    return RetAjaxOut;
                }


                ServiceTypeFilterCls
                    OServiceTypeFilter = new ServiceTypeFilterCls();
                OServiceTypeFilter.ActiveOnly = 1;
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                ServiceTypeCls[]
                    ServiceTypes = OnlineTourBussinessUtility.CreateBussinessProcess().CreateServiceTypeProcess().Reading(ActionSqlParam, OServiceTypeFilter);

                string ServiceTypeId = "";
                string SelectServiceTypeText = "<select style=\"width:100%\"  onchange=\"javascript:CallReading();\" id=\"drpSelectServiceType\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < ServiceTypes.Length; iIndex++)
                {
                    SelectServiceTypeText +=
                        "   <option value=\"" + ServiceTypes[iIndex].ServiceTypeId + "\">" + ServiceTypes[iIndex].ServiceTypeName + "</option>\r\n";
                    if (iIndex == 0)
                    {
                        ServiceTypeId = ServiceTypes[iIndex].ServiceTypeId;
                    }
                }
                SelectServiceTypeText += "</select>\r\n";

                AssetSupplierFilterCls
                   OAssetSupplierFilter = new AssetSupplierFilterCls();
                OAssetSupplierFilter.ActiveOnly = 1;
                AssetSupplierCls[]
                    AssetSuppliers = OnlineTourBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Reading(ActionSqlParam, OAssetSupplierFilter);

                string SelectAssetSupplierText =
                    " <select style=\"width:100%\" onchange=\"javascript:CallReading();\" id=\"drpSelectFilterAssetSupplier\" class=\"form-control select2\">\r\n" +
                    "       <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn nhà cung cấp") + "</option>\r\n";
                for (int iIndex = 0; iIndex < AssetSuppliers.Length; iIndex++)
                {
                    SelectAssetSupplierText +=
                        "   <option value=\"" + AssetSuppliers[iIndex].AssetSupplierId + "\">" + AssetSuppliers[iIndex].AssetSupplierName + "</option>\r\n";
                }
                SelectAssetSupplierText += "</select>\r\n";

                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +
                    "   function CallAddForm()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ServiceTypeId=document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.Service.ServerSideAddService(RenderInfo, ServiceTypeId).value;\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n" +


                    "   function CallUpdateForm(ServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.Service.ServerSideUpdateService(RenderInfo, ServiceId).value;\r\n" +
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


                    "           AjaxOut = OnlineTour.WebParts.Service.ServerSideDelete(RenderInfo, ServiceId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           CallReading();\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "dịch vụ đã được xóa thành công!") + ".\", \"success\"); \r\n" +
                    "       }); \r\n" +

                    "   }\r\n" +


                    "   function CallReading()\r\n" +
                    "   {\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='" + WebLanguage.GetLanguageProcessing(OSiteParam) + "';\r\n" +
                    "       setTimeout('RealCallReading()',10);\r\n" +
                    "   }\r\n" +

                    "   function RealCallReading()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       AssetSupplierId = document.getElementById('drpSelectFilterAssetSupplier').value;\r\n" +
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       OnlineTour.WebParts.Service.ServerSideDrawSearchResult(RenderInfo, ServiceTypeId, AssetSupplierId, Keyword, CallBackReading);\r\n" +
                    "   }\r\n" +

                    "   function CallBackReading(res)\r\n" +
                    "   {\r\n" +
                    "       AjaxOut = res.value;\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='';\r\n" +
                    "       document.getElementById('divServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
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
                        "             <div class=\"col-md-4\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Loại hình dịch vụ") + "</div>\r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + SelectServiceTypeText + "</div>\r\n" +
                        "             </div>\r\n" +
                        "             <div class=\"col-md-4\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Nhà cung cấp") + "</div>\r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + SelectAssetSupplierText + "</div>\r\n" +
                        "             </div>\r\n" +
                        "             <div class=\"col-md-4\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                        "                 <div><input onkeypress=\"if(event.keyCode==13){CallReading();}\"  id=\"txtKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                        "             </div> \r\n" +
                        "         </div> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "                 <div class=\"col-md-12\">\r\n" +
                        "                       <button onclick=\"javascript:CallReading();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Lọc") + "</button>\r\n" +
                        "                       <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:CallAddForm();\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                        "                 </div> \r\n" +
                        "         </div>\r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n" +
                        "               <div id=\"divServiceContent\">" + ServerSideDrawSearchResult(ORenderInfo, ServiceTypeId, null, "").HtmlContent + "</div>\r\n" +
                        "         </div> \r\n" +
                        "     </div> \r\n" +
                        " </div> \r\n" +

                        "</div>\r\n" +
                        "<div id=\"divActionForm\" style=\"display:none\"></div>\r\n"
                        ) +
                        "<script>\r\n" +
                        "   $('#drpSelectAssetSupplier').select2();\r\n"+
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
            string ServiceTypeId,
            string AssetSupplierId,
            string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                ServiceFilterCls
                    OServiceFilter = new ServiceFilterCls();
                OServiceFilter.ServiceTypeId = ServiceTypeId;
                OServiceFilter.AssetSupplierId = AssetSupplierId;
                OServiceFilter.Keyword = Keyword;

                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ServiceCls[] 
                    Services = OnlineTourBussinessUtility.CreateBussinessProcess().CreateServiceProcess().Reading(OActionSqlParam, OServiceFilter);
                string Html = "";
                if (Services.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có dữ liệu theo điều kiện lọc", true);
                }
                else
                {
                    Html +=
                        "   <div class=\"search-result-info\">"+WebLanguage.GetLanguage(OSiteParam,"Tìm thấy")+" ("+Services.Length.ToString("#,##0")+") "+WebLanguage.GetLanguage(OSiteParam,"dữ liệu theo điều kiện lọc")+"</div>\r\n"+
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Loại hình") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Nhà cung cấp") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị tính") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Giá mua") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Giá bán") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Giá web") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < Services.Length; iIndex++)
                    {
                        string CityUrl = WebScreen.BuildUrl(OwnerCode, new City().WebPartId,
                            new WebParamCls[]
                            {
                                new WebParamCls("ServiceId",Services[iIndex].ServiceId)
                            });
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td>" + Services[iIndex].ServiceTypeName + "</td> \r\n" +
                            "                     <td>" + Services[iIndex].AssetSupplierName + "</td> \r\n" +
                            "                     <td><span style=\"color:green;font-weight:bold\">" + Services[iIndex].ServiceName + "</span><div>(" + Services[iIndex].StdServiceName + ")</div></td> \r\n" +
                            "                     <td>" + Services[iIndex].UnitName + "</td> \r\n" +
                            "                     <td>" + Services[iIndex].BuyPrice + "</td> \r\n" +
                            "                     <td>" + Services[iIndex].SalePrice + "</td> \r\n" +
                            "                     <td>" + Services[iIndex].WebPrice + "</td> \r\n" +
                            "                     <td>" + (Services[iIndex].Active == 1 ? WebLanguage.GetLanguage(OSiteParam, "Sử dụng") : WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng")) + "</td> \r\n" +
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
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode,
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
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode,
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
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateServiceProcess().Delete(OActionSqlParam, ServiceId);
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
