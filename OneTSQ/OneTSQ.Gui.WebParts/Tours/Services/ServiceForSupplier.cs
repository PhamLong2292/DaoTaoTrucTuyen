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
    public class ServiceForSupplier : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "ServiceForSupplier";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Danh mục dịch vụ theo nhà cung cấp";
            }
        }

        public override string Description
        {
            get
            {
                return "Danh mục dịch vụ theo nhà cung cấp";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ServiceForSupplier), Page);
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

                string StdServiceId = (string)WebEnvironments.Request("StdServiceId");
                StdServiceCls
                    OStdService = CallBussinessUtility.CreateBussinessProcess().CreateStdServiceProcess().CreateModel(ORenderInfo, StdServiceId);
                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +

                    "   function CallReading()\r\n" +
                    "   {\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='" + WebLanguage.GetLanguageProcessing(OSiteParam) + "';\r\n" +
                    "       setTimeout('RealCallReading()',10);\r\n" +
                    "   }\r\n" +

                    "   function RealCallReading()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       OneTSQ.WebParts.ServiceForSupplier.ServerSideDrawSearchResult(RenderInfo, '"+StdServiceId+"',Keyword, CallBackReading);\r\n" +
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
                                    OStdService.StdServiceName +
                        "     </div> \r\n" +
                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "             <div class=\"col-md-12\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                        "                 <div><input onkeypress=\"if(event.keyCode==13){CallReading();}\"  id=\"txtKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                        "             </div> \r\n" +
                        "         </div> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "                 <div class=\"col-md-12\">\r\n" +
                        "                       <button onclick=\"javascript:CallReading();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Lọc") + "</button>\r\n" +
                        "                 </div> \r\n" +
                        "         </div>\r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n" +
                        "               <div id=\"divServiceContent\">" + ServerSideDrawSearchResult(ORenderInfo, StdServiceId, "").HtmlContent + "</div>\r\n" +
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
            string StdServiceId,
            string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                WebSession.CheckSessionTimeOut(ORenderInfo);


                ServiceFilterCls
                    OServiceFilter = new ServiceFilterCls();
                OServiceFilter.StdServiceId = StdServiceId;
                OServiceFilter.Keyword = Keyword;
                
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
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < Services.Length; iIndex++)
                    {
                        string StdServiceName = Services[iIndex].StdServiceName;
                        if (string.IsNullOrEmpty(StdServiceName))
                        {
                            StdServiceName = "<a style=\"color:maroon;font-weight:bold\">"+ WebLanguage.GetLanguage(OSiteParam, "Chưa thiết lập dịch vụ chuẩn")+"</a>\r\n";
                        }
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td>" + Services[iIndex].ServiceTypeName + "</td> \r\n" +
                            "                     <td><span style=\"color:green;font-weigh:bold\">" + Services[iIndex].AssetSupplierName + "</span></td> \r\n" +
                            "                     <td><span style=\"color:green;font-weight:bold\">" + Services[iIndex].ServiceName + "</span><div>"+StdServiceName+"</div></td> \r\n" +
                            "                     <td>" + Services[iIndex].UnitName + "</td> \r\n" +
                            "                     <td>" + FunctionUtility.FormatNumber(Services[iIndex].BuyPrice) + "</td> \r\n" +
                            "                     <td>" + FunctionUtility.FormatNumber(Services[iIndex].SalePrice) + "</td> \r\n" +
                            "                     <td>" + FunctionUtility.FormatNumber(Services[iIndex].WebPrice) + "</td> \r\n" +
                            "                     <td>" + (Services[iIndex].Active == 1 ? WebLanguage.GetLanguage(OSiteParam, "Sử dụng") : WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng")) + "</td> \r\n" +
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
    }
}
