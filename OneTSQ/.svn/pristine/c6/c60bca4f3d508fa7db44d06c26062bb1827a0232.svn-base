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
                string ServiceTypeId = (string)WebEnvironments.Request("ServiceTypeId");
                if (string.IsNullOrEmpty(ServiceTypeId))
                {
                    ServiceTypeId="";
                }
                string FilterTypeId = (string)WebEnvironments.Request("FilterTypeId");
                if (string.IsNullOrEmpty(ServiceTypeId))
                {
                    FilterTypeId="";
                }
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId;
                string BackUrl = WebScreen.BuildUrl(OwnerCode, new Country().WebPartId);

                ServiceTypeFilterCls
                  OServiceTypeFilter = new ServiceTypeFilterCls();

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                ServiceTypeCls[]
                  ServiceTypes = OnlineTourBussinessUtility.CreateBussinessProcess().CreateServiceTypeProcess().Reading(OActionSqlParam, OServiceTypeFilter);
                string SelectTypeText = "<select id=\"drpSelectServiceType\" onchange=\"javascript:OpenLink();\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < ServiceTypes.Length; iIndex++)
                {
                    if (ServiceTypes[iIndex].ServiceTypeId.Equals(ServiceTypeId))
                    {
                        SelectTypeText += "   <option selected value=\"" + ServiceTypes[iIndex].ServiceTypeId + "\">" + ServiceTypes[iIndex].ServiceTypeName + "</option>\r\n";
                    }
                    else
                    {
                        SelectTypeText += "   <option value=\"" + ServiceTypes[iIndex].ServiceTypeId + "\">" + ServiceTypes[iIndex].ServiceTypeName + "</option>\r\n";
                    }
                }
                SelectTypeText += "</select>\r\n";

                string SelectFilterTypeText =
                    "<select id=\"drpSelectFilterType\" class=\"form-control\" onchange=\"javascript:OpenLink();\">\r\n" +
                    "   <option value=\"0\">Chỉ lọc của đơn vị mình</option>\r\n"+
                    "   <option value=\"1\">Lọc toàn bộ</option>\r\n" +
                    "</select>\r\n";

                if (FilterTypeId.Equals("1"))
                {
                    SelectFilterTypeText =
                       "<select id=\"drpSelectFilterType\" class=\"form-control\" onchange=\"javascript:OpenLink();\">\r\n" +
                       "   <option value=\"0\">Chỉ lọc của đơn vị mình</option>\r\n" +
                       "   <option selected value=\"1\">Lọc toàn bộ</option>\r\n" +
                       "</select>\r\n";
                }
                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +
                    "   function OpenLink()\r\n"+
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       FilterTypeId = document.getElementById('drpSelectFilterType').value;\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n"+
                    "       AjaxOut = OnlineTour.WebParts.AssetSupplier.ServerSideOpenLink(RenderInfo, ServiceTypeId, FilterTypeId).value;\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n" +


                    "   function CallAddForm()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.AssetSupplier.ServerSideDrawAddForm(RenderInfo).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       document.getElementById('txtAssetSupplierName').focus();\r\n" +
                    "   }\r\n" +


                    "   function CallUpdateForm(AssetSupplierId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.AssetSupplier.ServerSideDrawUpdateForm(RenderInfo, AssetSupplierId).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       document.getElementById('txtAssetSupplierName').focus();\r\n" +
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

                    "       AjaxOut = OnlineTour.WebParts.AssetSupplier.ServerSideAdd(RenderInfo, CityId, OwnerId, ServiceTypeId, AssetSupplierName, Address, Hotline, Fax, Vat, LicenceSale, WebSite, GoogleAddress, AssetSupplierLevel, SortIndex, Active).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n"+
                    "       {\r\n"+
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n"+
                    "       CallReading();\r\n"+
                    "       document.getElementById('txtAssetSupplierName').value='';\r\n"+
                    "       document.getElementById('txtAddress').value='';\r\n" +
                    "       document.getElementById('txtHotline').value='';\r\n" +
                    "       document.getElementById('txtFax').value='';\r\n" +
                    "       document.getElementById('txtVat').value='';\r\n" +
                    "       document.getElementById('txtLicenceSale').value='';\r\n" +
                    "       document.getElementById('txtWebSite').value='';\r\n" +
                    "       document.getElementById('txtGoogleAddress').value='';\r\n" +
                    "       document.getElementById('txtAssetSupplierLevel').value='';\r\n" +
                    "       document.getElementById('txtAssetSupplierName').focus();\r\n" +
                    "       document.getElementById('txtSortIndex').value=parseInt(SortIndex,10)+1;\r\n"+
                    "   }\r\n" +


                    "   function CallActionUpdate(AssetSupplierId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       CityId= document.getElementById('drpSelectCity').value;\r\n" +
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
                    "       AjaxOut = OnlineTour.WebParts.AssetSupplier.ServerSideUpdate(RenderInfo, AssetSupplierId, CityId, AssetSupplierName, Address, Hotline, Fax, Vat, LicenceSale, WebSite, GoogleAddress, AssetSupplierLevel, SortIndex, Active).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallReading();\r\n"+
                    "       CallBack();\r\n" +
                    "   }\r\n" +

                    "   function CallActionDelete(AssetSupplierId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n"+
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \""+WebLanguage.GetLanguage(OSiteParam,"Đang thực hiện xóa nhà cung cấp này ra khỏi hệ thống")+"!\", \r\n"+
                    "               type: \"warning\", \r\n"+
                    "               showCancelButton: true, \r\n"+
                    "               confirmButtonColor: \"#DD6B55\", \r\n"+
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam,"Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n"+
                    "           }, function () { \r\n"+


                    "           AjaxOut = OnlineTour.WebParts.AssetSupplier.ServerSideDelete(RenderInfo, AssetSupplierId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           CallReading();\r\n" +

                    "           swal(\""+WebLanguage.GetLanguage(OSiteParam,"Đã xóa")+"!\", \""+WebLanguage.GetLanguage(OSiteParam,"nhà cung cấp đã được xóa thành công!")+".\", \"success\"); \r\n"+
                    "       }); \r\n" +
                    
                    "   }\r\n" +


                    "   function CallReading()\r\n" +
                    "   {\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='"+WebLanguage.GetLanguageProcessing(OSiteParam)+"';\r\n"+
                    "       setTimeout('RealCallReading()',10);\r\n"+
                    "   }\r\n" +

                    "   function RealCallReading()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       OwnerId = document.getElementById('txtOwnerId').value;\r\n" +
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       FilterTypeId= document.getElementById('drpSelectFilterType').value;\r\n" +
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       OnlineTour.WebParts.AssetSupplier.ServerSideDrawSearchResult(RenderInfo, OwnerId, ServiceTypeId, FilterTypeId, Keyword, CallBackReading);\r\n" +
                    "   }\r\n" +

                    "   function CallBackReading(res)\r\n" +
                    "   {\r\n" +
                    "       AjaxOut = res.value;\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='';\r\n" +
                    "       document.getElementById('divAssetSupplierContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +

                    "</script>\r\n") +
                    WebEnvironments.EncryptHtml(
                        "<input id=\"txtOwnerId\" type=\"hidden\" value=\"" + WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId + "\">\r\n" +
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +
                        "     <div class=\"ibox-title\"> \r\n" +
                        "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh mục nhà cung cấp") + " </h5> \r\n" +
                        "     </div> \r\n" +
                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "             <div class=\"col-md-4\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Phạm vi lọc") + "</div>\r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + SelectFilterTypeText + "</div>\r\n" +
                        "             </div>\r\n" +
                        "             <div class=\"col-md-4\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Loại dịch vụ") + "</div>\r\n" +
                        "                 <div style=\"margin-bottom:5px\">"+SelectTypeText+"</div>\r\n"+
                        "             </div>\r\n" +
                        "             <div class=\"col-md-4\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                        "                 <div><input onkeypress=\"if(event.keyCode==13){CallReading();}\"  id=\"txtKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                        "             </div>\r\n"+
                        "         </div>\r\n" +
                        "                <div class=\"row\">\r\n" +
                        "                   <div class=\"col-md-12\">\r\n"+
                        "                       <button onclick=\"javascript:CallReading();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Lọc") + "</button>\r\n" +
                        "                       <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:CallAddForm();\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                        "                   </div>\r\n"+
                        "                </div> \r\n" +
                        "             </div> \r\n" +
                        "         </div> \r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n"+
                        "               <div id=\"divAssetSupplierContent\">" + ServerSideDrawSearchResult(ORenderInfo, OwnerId, ServiceTypeId, FilterTypeId, "").HtmlContent + "</div>\r\n" +
                        "         </div> \r\n" +
                        "     </div> \r\n" +
                        " </div> \r\n" +

                        "</div>\r\n" +
                        "<div id=\"divActionForm\" style=\"display:none\"></div>\r\n"
                        );

                
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
            string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                AssetSupplierFilterCls
                    OAssetSupplierFilter = new AssetSupplierFilterCls();
                OAssetSupplierFilter.Keyword = Keyword;
                OAssetSupplierFilter.ServiceTypeId = ServiceTypeId;
                if (FilterTypeId.Equals("0"))
                {
                    OAssetSupplierFilter.OwnerId = OwnerId;
                }
                AssetSupplierCls[] 
                    AssetSuppliers = OnlineTourBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Reading(OActionSqlParam, OAssetSupplierFilter);
                string Html = "";
                if (AssetSuppliers.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có dữ liệu theo điều kiện lọc", true);
                }
                else
                {
                    Html +=
                        "   <div class=\"search-result-info\">"+WebLanguage.GetLanguage(OSiteParam,"Tìm thấy")+" ("+AssetSuppliers.Length.ToString("#,##0")+") "+WebLanguage.GetLanguage(OSiteParam,"dữ liệu theo điều kiện lọc")+"</div>\r\n"+
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Owner") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên nhà cung cấp") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Điện thoại") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < AssetSuppliers.Length; iIndex++)
                    {
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td>" + AssetSuppliers[iIndex].OwnerCode + "</td> \r\n" +
                            "                     <td>" + AssetSuppliers[iIndex].CityName + "</td> \r\n" +
                            "                     <td>" + AssetSuppliers[iIndex].AssetSupplierName + "</td> \r\n" +
                            "                     <td>" + AssetSuppliers[iIndex].Address + "</td> \r\n" +
                            "                     <td>" + AssetSuppliers[iIndex].Hotline + "</td> \r\n" +

                            "                     <td>" + (AssetSuppliers[iIndex].Active == 1 ? WebLanguage.GetLanguage(OSiteParam, "Sử dụng") : WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng")) + "</td> \r\n";

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
        public static AjaxOut ServerSideDrawAddForm(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string SelectActiveText =
                    "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">"+WebLanguage.GetLanguage(OSiteParam,"Không sử dụng")+"</option>\r\n" +
                    "   <option selected  value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                    "</select>\r\n";
                ActionSqlParamCls
                    OActionSqlParam=WebEnvironments.CreateActionSqlParam(OSiteParam);
                CityFilterCls
                    OCityFilter = new CityFilterCls();
                OCityFilter.ActiveOnly = 1;

                CityCls[]
                    Citites = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCityProcess().Reading(OActionSqlParam, OCityFilter);

                string SelectCityText = "<select id=\"drpSelectCity\" class=\"form-control select2\">\r\n";
                for (int iIndex = 0; iIndex < Citites.Length; iIndex++)
                {
                    SelectCityText += "   <option value=\""+Citites[iIndex].CityId+"\">" + Citites[iIndex].CityName + "</option>\r\n";
                }
                SelectCityText += "</select>\r\n";
                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</h3> \r\n" +
                       "             <div class=\"row\"> \r\n" +
                       "                <div class=\"col-md-6\">\r\n"+
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "</label> " + SelectCityText + "</div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên nhà cung cấp") + "</label> <input id=\"txtAssetSupplierName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên nhà cung cấp") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "</label> <input id=\"txtAddress\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số Hotline") + "</label> <input id=\"txtHotline\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Hotline") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số Fax") + "</label> <input id=\"txtFax\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Fax") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số Vat") + "</label> <input id=\"txtVat\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mã số thuế") + "\" class=\"form-control\"></div> \r\n" +
                       "                </div>\r\n"+
                       "                <div class=\"col-md-6\">\r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số đăng ký kinh doanh") + "</label> <input id=\"txtLicenceSale\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Số đăng ký kinh doanh") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ website") + "</label> <input id=\"txtWebSite\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ website") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ google") + "</label> <input id=\"txtGoogleAddress\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ google") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đánh giá") + "</label> <input id=\"txtAssetSupplierLevel\" value=\"5\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Đánh giá xếp hạng nhập giá trị 1-5") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input id=\"txtSortIndex\" value=\"1\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> "+SelectActiveText+"</div> \r\n" +
                       "                </div>\r\n"+
                       "            </div>\r\n"+
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideDrawUpdateForm(RenderInfoCls ORenderInfo, string AssetSupplierId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
               
                AssetSupplierCls
                    OAssetSupplier = OnlineTourBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().CreateModel(OActionSqlParam, AssetSupplierId);
                if (OAssetSupplier == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "nhà cung cấp đã bị xóa hoặc không tìm thấy"));
                }

                 CityFilterCls
                    OCityFilter = new CityFilterCls();
                OCityFilter.ActiveOnly = 1;

                CityCls[]
                    Citites = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCityProcess().Reading(OActionSqlParam, OCityFilter);

                string SelectCityText = "<select id=\"drpSelectCity\" class=\"form-control select2\">\r\n";
                for (int iIndex = 0; iIndex < Citites.Length; iIndex++)
                {
                    if (Citites[iIndex].CityId.Equals(OAssetSupplier.frkCityId))
                    {
                        SelectCityText += "   <option selected value=\"" + Citites[iIndex].CityId + "\">" + Citites[iIndex].CityName + "</option>\r\n";
                    }
                    else
                    {
                        SelectCityText += "   <option value=\"" + Citites[iIndex].CityId + "\">" + Citites[iIndex].CityName + "</option>\r\n";
                    }
                }
                SelectCityText += "</select>\r\n";
                string SelectActiveText =
                 "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                 "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                 "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                 "</select>\r\n";

                if (OAssetSupplier.Active == 1)
                {
                    SelectActiveText =
                 "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                 "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                 "   <option selected value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                 "</select>\r\n";
                }
                string Html =
                       " <div class=\"ibox-content\"> \r\n" +
                       "     <div class=\"row\"> \r\n" +
                       "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Sửa chữa") + "</h3> \r\n" +
                       "             <div class=\"row\"> \r\n" +
                       "                <div class=\"col-md-6\"> \r\n" +
                       "                     <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "</label> " + SelectCityText + "</div> \r\n" +
                       "                     <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên nhà cung cấp") + "</label> <input id=\"txtAssetSupplierName\" value=\"" + OAssetSupplier.AssetSupplierName + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên nhà cung cấp") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "</label> <input id=\"txtAddress\" value=\""+OAssetSupplier.Address+"\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số Hotline") + "</label> <input id=\"txtHotline\" value=\"" + OAssetSupplier.Hotline + "\"  type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Hotline") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số Fax") + "</label> <input id=\"txtFax\" value=\"" + OAssetSupplier.Fax + "\"  type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Fax") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số Vat") + "</label> <input id=\"txtVat\" value=\"" + OAssetSupplier.Vat + "\"  type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mã số thuế") + "\" class=\"form-control\"></div> \r\n" +
                       "                </div>\r\n"+
                       "                <div class=\"col-md-6\">\r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số đăng ký kinh doanh") + "</label> <input value=\"" + OAssetSupplier.LicenceSale + "\"  id=\"txtLicenceSale\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Số đăng ký kinh doanh") + "\" class=\"form-control\"></div> \r\n" +
                       "                     <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ website") + "</label> <input id=\"txtWebSite\" value=\"" + OAssetSupplier.WebSite + "\"  type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ website") + "\" class=\"form-control\"></div> \r\n" +
                       "                     <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ website") + "</label> <input id=\"txtGoogleAddress\" value=\""+OAssetSupplier.GoogleAddress+"\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ google") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đánh giá") + "</label> <input id=\"txtAssetSupplierLevel\" value=\"" + OAssetSupplier.AssetSupplierLevel + "\"   type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Đánh giá xếp hạng") + "\" class=\"form-control\"></div> \r\n" +

                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input id=\"txtSortIndex\" value=\"" + OAssetSupplier.SortIndex + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> " + SelectActiveText + "</div> \r\n" +
                       "                </div>\r\n" +
                       
                       "             </div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + AssetSupplierId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                       "                 </div> \r\n" +
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
        public static AjaxOut ServerSideAdd(
            RenderInfoCls ORenderInfo, 
            string CityId,
            string OwnerId,
            string ServiceTypeId,
            string AssetSupplierName,
            string Address,
            string Hotline,
            string Fax,
            string Vat,
            string LicenceSale,
            string WebSite,
            string GoogleAddress,
            string AssetSupplierLevel,
            string SortIndex,
            int Active)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                if (string.IsNullOrEmpty(AssetSupplierName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên nhà cung cấp không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex)==false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

        
                AssetSupplierCls 
                    OAssetSupplier = new AssetSupplierCls();
                OAssetSupplier.AssetSupplierId = System.Guid.NewGuid().ToString();
                OAssetSupplier.frkCityId = CityId;
                OAssetSupplier.AssetSupplierName = AssetSupplierName;
                OAssetSupplier.Address = Address;
                OAssetSupplier.Hotline = Hotline;
                OAssetSupplier.Fax = Fax;
                OAssetSupplier.Vat = Vat;
                OAssetSupplier.LicenceSale = LicenceSale;
                OAssetSupplier.WebSite = WebSite;
                OAssetSupplier.GoogleAddress = GoogleAddress;
                OAssetSupplier.AssetSupplierLevel = AssetSupplierLevel;
                OAssetSupplier.frkServiceTypeId = ServiceTypeId;
                OAssetSupplier.frkOwnerId = OwnerId;

                OAssetSupplier.SortIndex = int.Parse(SortIndex);
                OAssetSupplier.Active = Active;

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Add(OActionSqlParam, OAssetSupplier);
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
            string AssetSupplierId, 
            string CityId,
            string AssetSupplierName,
            string Address,
            string Hotline,
            string Fax,
            string Vat,
            string LicenceSale,
            string WebSite,
            string GoogleAddress,
            string AssetSupplierLevel,
            string SortIndex,
            int    Active)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                     OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);


                if (string.IsNullOrEmpty(AssetSupplierName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên nhà cung cấp không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));


                AssetSupplierCls
                    OAssetSupplier = OnlineTourBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().CreateModel(OActionSqlParam, AssetSupplierId);
                OAssetSupplier.AssetSupplierName = AssetSupplierName;
                OAssetSupplier.frkCityId = CityId;
                OAssetSupplier.Address = Address;
                OAssetSupplier.Hotline = Hotline;
                OAssetSupplier.Fax = Fax;
                OAssetSupplier.Vat = Vat;
                OAssetSupplier.LicenceSale = LicenceSale;
                OAssetSupplier.WebSite = WebSite;
                OAssetSupplier.GoogleAddress = GoogleAddress;
                OAssetSupplier.AssetSupplierLevel = AssetSupplierLevel;
                
                OAssetSupplier.SortIndex = int.Parse(SortIndex);
                OAssetSupplier.Active = Active;
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Save(OActionSqlParam, AssetSupplierId, OAssetSupplier);
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
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Delete(OActionSqlParam, AssetSupplierId);
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
            string FilterTypeId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, new AssetSupplier().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("ServiceTypeId",ServiceTypeId),
                    new WebParamCls("FilterTypeId",FilterTypeId),

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
