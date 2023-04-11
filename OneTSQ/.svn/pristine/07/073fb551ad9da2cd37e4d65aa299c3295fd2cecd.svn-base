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
    public class AddAssetSupplier : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "AddAssetSupplier";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Thêm nhà cung cấp";
            }
        }

        public override string Description
        {
            get
            {
                return "Thêm nhà cung cấp";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AddAssetSupplier), Page);
        }

        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { "AssetSupplier" };
            }
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


                RetAjaxOut.HtmlContent =
                    ServerSideDrawAddForm(ORenderInfo).HtmlContent;    
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
        public static AjaxOut ServerSideDrawAddForm(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);


                string PageIndex = (string)WebEnvironments.Request("pageindex");
                if (FunctionUtility.checkInteger(PageIndex) == false) PageIndex = "0";

                string Status = (string)WebEnvironments.Request("status");
                if (FunctionUtility.checkInteger(Status) == false) Status = "0";

                string ServiceTypeId = (string)WebEnvironments.Request("servicetypeid");
                if (string.IsNullOrEmpty(ServiceTypeId))
                {
                    ServiceTypeId = "";
                }
                string FilterTypeId = (string)WebEnvironments.Request("filtertypeid");
                if (string.IsNullOrEmpty(FilterTypeId))
                {
                    FilterTypeId = "";
                }
                string Keyword = (string)WebEnvironments.Request("keyword");
                if (string.IsNullOrEmpty(Keyword))
                {
                    Keyword = "";
                }

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId;
                string BackUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new AssetSupplier().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("ServiceTypeId",ServiceTypeId),
                        new WebParamCls("FilterTypeId",FilterTypeId),
                        new WebParamCls("Status",Status),
                        new WebParamCls("Keyword",Keyword),
                        new WebParamCls("PageIndex",PageIndex),
                    });

                  
                 CityFilterCls
                    OCityFilter = new CityFilterCls();
                OCityFilter.ActiveOnly = 1;

                CityCls[]
                    Citites = CallBussinessUtility.CreateBussinessProcess().CreateCityProcess().Reading(ORenderInfo, OCityFilter);

                string SelectCityText = "<select id=\"drpSelectCity\" class=\"form-control select2\">\r\n";
                for (int iIndex = 0; iIndex < Citites.Length; iIndex++)
                {
                    SelectCityText += "   <option value=\"" + Citites[iIndex].CityId + "\">" + Citites[iIndex].CityName + "</option>\r\n";
                }
                SelectCityText += "</select>\r\n";
                string SelectActiveText =
                 "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                 "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                 "   <option selected value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                 "</select>\r\n";

               
                string Html =
                        "<script language=javascript>\r\n" +
                        "   function CallActionAdd()\r\n" +
                        "   {\r\n" +
                        "       RenderInfo=CreateRenderInfo();\r\n" +
                        "       OwnerId= document.getElementById('txtOwnerId').value;\r\n" +
                        "       CityId= document.getElementById('drpSelectCity').value;\r\n" +
                        "       ServiceTypeId = document.getElementById('txtServiceType').value;\r\n" +
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

                        "       AjaxOut = OneTSQ.WebParts.AddAssetSupplier.ServerSideAdd(RenderInfo, CityId, OwnerId, ServiceTypeId, AssetSupplierCode, AssetSupplierName, Address, Hotline, Fax, Vat, LicenceSale, WebSite, GoogleAddress, AssetSupplierLevel, SortIndex, Active).value;\r\n" +
                        "       if(AjaxOut.Error)\r\n" +
                        "       {\r\n" +
                        "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                        "           return;\r\n" +
                        "       }\r\n" +
                        "       window.open('" + BackUrl + "','_self');\r\n" +
                        "   }\r\n" +

                        "</script>\r\n"+

                       "<input id=\"txtOwnerId\" type=\"hidden\" value=\"" + WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId + "\">\r\n" +
                       "<input id=\"txtServiceType\" type=\"hidden\" value=\"" + ServiceTypeId + "\">\r\n" +
                       " <div class=\"ibox-content\"> \r\n" +
                       "     <div class=\"row\"> \r\n" +
                       "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</h3> \r\n" +
                       "             <div class=\"row\"> \r\n" +
                       "                <div class=\"col-md-6\">\r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "</label> " + SelectCityText + "</div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + "</label> <input id=\"txtAssetSupplierCode\" type=\"textbox\"  placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mã nhà cung cấp") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên nhà cung cấp") + "</label> <input id=\"txtAssetSupplierName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên nhà cung cấp") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "</label> <input id=\"txtAddress\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số Hotline") + "</label> <input id=\"txtHotline\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Hotline") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số Fax") + "</label> <input id=\"txtFax\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Fax") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số Vat") + "</label> <input id=\"txtVat\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mã số thuế") + "\" class=\"form-control\"></div> \r\n" +
                       "                </div>\r\n" +
                       "                <div class=\"col-md-6\">\r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số đăng ký kinh doanh") + "</label> <input id=\"txtLicenceSale\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Số đăng ký kinh doanh") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ website") + "</label> <input id=\"txtWebSite\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ website") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ google") + "</label> <input id=\"txtGoogleAddress\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ google") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đánh giá") + "</label> <input id=\"txtAssetSupplierLevel\" value=\"5\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Đánh giá xếp hạng nhập giá trị 1-5") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input id=\"txtSortIndex\" value=\"1\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> " + SelectActiveText + "</div> \r\n" +
                       "                </div>\r\n" +
                       "            </div>\r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:window.open('"+BackUrl+"','_self');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideAdd(
            RenderInfoCls ORenderInfo,
            string CityId,
            string OwnerId,
            string ServiceTypeId,
            string AssetSupplierCode,
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
                WebSession.CheckSessionTimeOut(ORenderInfo);

                //if (string.IsNullOrEmpty(AssetSupplierCode)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Mã nhà cung cấp không hợp lệ"));
                if (string.IsNullOrEmpty(AssetSupplierName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên nhà cung cấp không hợp lệ"));
                if (FunctionUtility.checkInteger(SortIndex) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));

                AssetSupplierCls
                    OAssetSupplier = new AssetSupplierCls();
                OAssetSupplier.AssetSupplierId = System.Guid.NewGuid().ToString();
                OAssetSupplier.frkCityId = CityId;
                OAssetSupplier.AssetSupplierCode = AssetSupplierCode;
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

                CallBussinessUtility.CreateBussinessProcess().CreateAssetSupplierProcess().Add(ORenderInfo, OAssetSupplier);
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
