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
    public class AddCustomer : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "AddCustomer";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Thêm khách hàng";
            }
        }

        public override string Description
        {
            get
            {
                return "Thêm khách hàng";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(AddCustomer),Page);
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
                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +

                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                    "       document.getElementById('divActionForm').innerHTML='';\r\n" +
                    "       document.getElementById('divActionForm').style.display='none';\r\n" +
                    "       document.getElementById('divListForm').style.display='block';\r\n" +
                    "   }\r\n" +

                    "   function CallActionAdd()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       Company = document.getElementById('txtCompany').value;\r\n" +
                    "       Address = document.getElementById('txtAddress').value;\r\n" +
                    "       Phone = document.getElementById('txtPhone').value;\r\n" +
                    "       Email = document.getElementById('txtEmail').value;\r\n" +
                    "       CountryId = document.getElementById('drpSelectCountry').value;\r\n" +

                    "       City = document.getElementById('txtCityName').value;\r\n" +
                    "       ZipCode = document.getElementById('txtZipcode').value;\r\n" +
                    "       Vat = document.getElementById('txtVat').value;\r\n" +
                    "       Website = document.getElementById('txtWebsite').value;\r\n" +
                    "       CustomerLevelId= document.getElementById('drpSelectCustomerLevel').value;\r\n" +
                    "       CurrencyId= document.getElementById('drpSelectCurrency').value;\r\n" +
                    "       LanguageId= document.getElementById('drpSelectLanguage').value;\r\n" +
                    "       SortIndex = 1;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +

                    "       AjaxOut = OnlineTour.WebParts.AddCustomer.ServerSideCreateObject(RenderInfo,null).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       SaveCustomer = AjaxOut.RetObject;\r\n" +
                    "       SaveCustomer.Company=Company;\r\n" +
                    "       SaveCustomer.Address=Address;\r\n" +
                    "       SaveCustomer.Phone=Phone;\r\n" +
                    "       SaveCustomer.Email=Email;\r\n" +
                    "       SaveCustomer.frkCountryId=CountryId;\r\n" +
                    "       SaveCustomer.frkCurrencyId=CurrencyId;\r\n" +
                    "       SaveCustomer.frkLanguageId=LanguageId;\r\n" +
                    "       SaveCustomer.City=City;\r\n" +
                    "       SaveCustomer.ZipCode=ZipCode;\r\n" +
                    "       SaveCustomer.Website=Website;\r\n" +
                    "       SaveCustomer.frkCustomerLevelId=CustomerLevelId;\r\n" +
                    "       SaveCustomer.SortIndex=SortIndex;\r\n" +
                    "       SaveCustomer.Active=Active;\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.AddCustomer.ServerSideAdd(RenderInfo, SaveCustomer).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallReading();\r\n" +
                    "       document.getElementById('txtCompany').value='';\r\n" +
                    "       document.getElementById('txtCompany').focus();\r\n" +
                    "       document.getElementById('txtSortIndex').value=parseInt(SortIndex,10)+1;\r\n" +
                    "   }\r\n" +




                    "</script>\r\n") +
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
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string SelectActiveText =
                    "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">"+WebLanguage.GetLanguage(OSiteParam,"Không sử dụng")+"</option>\r\n" +
                    "   <option selected  value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                    "</select>\r\n";

                ActionSqlParamCls
                    OActionSqlParam=WebEnvironments.CreateActionSqlParam(OSiteParam);
               
                CountryFilterCls 
                    OCountryFilter = new CountryFilterCls();
                OCountryFilter.ActiveOnly = 1;
                CountryCls[] 
                    Countries = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCountryProcess().Reading(OActionSqlParam, OCountryFilter);

                string SelectCountryText = 
                    "<select id=\"drpSelectCountry\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < Countries.Length; iIndex++)
                {
                    SelectCountryText += "    <option value=\"" + Countries[iIndex].CountryId + "\">" + Countries[iIndex].CountryName + "</option>\r\n";
                }
                SelectCountryText+="</select>\r\n";


                CurrencyFilterCls
                 OCurrencyFilter = new CurrencyFilterCls();
                OCurrencyFilter.ActiveOnly = 1;
                CurrencyCls[]
                    Currencies = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCurrencyProcess().Reading(OActionSqlParam, OCurrencyFilter);

                string SelectCurrencyText =
                    "<select id=\"drpSelectCurrency\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < Currencies.Length; iIndex++)
                {
                    SelectCurrencyText += "    <option value=\"" + Currencies[iIndex].CurrencyId + "\">" + Currencies[iIndex].CurrencyName + "</option>\r\n";
                }
                SelectCurrencyText += "</select>\r\n";

                LanguageFilterCls
                 OLanguageFilter = new LanguageFilterCls();
                OLanguageFilter.ActiveOnly = 1;
                LanguageCls[]
                    Languages = OnlineTourBussinessUtility.CreateBussinessProcess().CreateLanguageProcess().Reading(OActionSqlParam, OLanguageFilter);

                string SelectLanguageText =
                    "<select id=\"drpSelectLanguage\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < Languages.Length; iIndex++)
                {
                    SelectLanguageText += "    <option value=\"" + Languages[iIndex].LanguageId + "\">" + Languages[iIndex].LanguageName + "</option>\r\n";
                }
                SelectLanguageText += "</select>\r\n";

                

                CustomerLevelFilterCls
                    OCustomerLevelFilter = new CustomerLevelFilterCls();
                OCustomerLevelFilter.ActiveOnly = 1;
                CustomerLevelCls[]
                    CustomerLevels = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCustomerLevelProcess().Reading(OActionSqlParam, OCustomerLevelFilter);

                string SelectCustomerLevelText =
                    "<select id=\"drpSelectCustomerLevel\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < CustomerLevels.Length; iIndex++)
                {
                    SelectCustomerLevelText += "    <option value=\"" + CustomerLevels[iIndex].CustomerLevelId + "\">" + CustomerLevels[iIndex].CustomerLevelName + "</option>\r\n";
                }
                SelectCustomerLevelText += "</select>\r\n";

                SiteParam SiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string BackUrl = WebScreen.BuildUrl(OwnerCode, new Customer().WebPartId);

                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</h3> \r\n" +
                    "             <div> \r\n" +
                    "                    <div class=\"tabs-container\"> \r\n" +
                    "                        <ul class=\"nav nav-tabs\"> \r\n" +
                    "                            <li class=\"active\"><a data-toggle=\"tab\" href=\"#tab-1\"> " + WebLanguage.GetLanguage(OSiteParam, "Thông tin chung") + " </a></li> \r\n" +
                    "                            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-2\">" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ hóa đơn") + "</a></li> \r\n" +
                    "                            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-3\">" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ giao hàng") + "</a></li> \r\n" +
                    "                            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-4\">" + WebLanguage.GetLanguage(OSiteParam, "Người liên hệ") + "</a></li> \r\n" +
                    "                        </ul> \r\n" +
                    "                        <div class=\"tab-content\"> \r\n" +
                    "                            <div id=\"tab-1\" class=\"tab-pane active\"> \r\n" +
                    "                                <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +

                    "                                   <div class=\"row\">\r\n" +
                    "                                       <div class=\"col-md-6\">\r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên khách hàng") + "</label> <input id=\"txtCompany\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên khách hàng") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "</label> <input id=\"txtAddress\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số điện thoại") + "</label> <input id=\"txtPhone\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Số điện thoại") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Email") + "</label> <input id=\"txtEmail\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Email") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã số thuế") + "</label> <input id=\"txtVat\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Vat") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Phân nhóm") + "</label> " + SelectCustomerLevelText + "</div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiền tệ") + "</label> " + SelectCurrencyText + "</div> \r\n" +
                    "                                       </div>\r\n" +

                    "                                       <div class=\"col-md-6\">\r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Ngôn ngữ") + "</label> " + SelectLanguageText + "</div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Quốc gia") + "</label>" + SelectCountryText + " </div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "</label> <input id=\"txtCityName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Website") + "</label> <input id=\"txtWebsite\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Website") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Zip code") + "</label> <input id=\"txtZipcode\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Zip code") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input value=\"1\" id=\"txtSortIndex\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> " + SelectActiveText + "</div> \r\n" +
                    "                                       </div>\r\n" +
                    "                                   </div>\r\n" +


                    "                                </div> \r\n" +
                    "                            </div> \r\n" +
                    "                            <div id=\"tab-2\" class=\"tab-pane\"> \r\n" +
                    "                                <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +
                    "                                   <div class=\"row\">\r\n" +
                    "                                       <div class=\"col-md-6\">\r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ thanh toán") + "</label> <input id=\"txtBillingAddress\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ thanh toán") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "</label> <input id=\"txtBillingCity\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "\" class=\"form-control\"></div> \r\n" +
                    "                                       </div>\r\n" +

                    "                                       <div class=\"col-md-6\">\r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Quận huyện") + "</label> <input id=\"txtBillingState\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Quận huyện") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Khác") + "</label> <input id=\"txtBillingZipCode\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Khác") + "\" class=\"form-control\"></div> \r\n" +
                    "                                       </div>\r\n" +
                    "                                   </div>\r\n" +
                    "                                </div> \r\n" +
                    "                            </div> \r\n" +

                    "                            <div id=\"tab-3\" class=\"tab-pane\"> \r\n" +
                    "                                <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +
                     "                                   <div class=\"row\">\r\n" +
                    "                                       <div class=\"col-md-6\">\r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ giao hàng") + "</label> <input id=\"txtShippingAddress\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ giao hàng") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "</label> <input id=\"txtShippingCity\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "\" class=\"form-control\"></div> \r\n" +
                    "                                       </div>\r\n" +

                    "                                       <div class=\"col-md-6\">\r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Quận huyện") + "</label> <input id=\"txtShippingState\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Quận huyện") + "\" class=\"form-control\"></div> \r\n" +
                    "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Khác") + "</label> <input id=\"txtShippingZipCode\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Khác") + "\" class=\"form-control\"></div> \r\n" +
                    "                                       </div>\r\n" +
                    "                                   </div>\r\n" +
                    "                                </div> \r\n" +
                    "                            </div> \r\n" +

                    "                            <div id=\"tab-4\" class=\"tab-pane\"> \r\n" +
                    "                                <div class=\"panel-body\"> \r\n" +

                    "                                </div> \r\n" +
                    "                            </div> \r\n" +
                    "                        </div> \r\n" +
                    "                    </div>    \r\n" +





                    "                 <div style=\"margin-top:10px\"> \r\n" +
                    "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                    "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:window.open('" + BackUrl + "','_self');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideCreateObject(RenderInfoCls ORenderInfo,string CustomerId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                CustomerCls
                    OCustomer = new CustomerCls();
                if (!string.IsNullOrEmpty(CustomerId))
                {
                    OCustomer = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCustomerProcess().CreateModel(ActionSqlParam, CustomerId);
                }
                else
                {
                    OCustomer.frkOwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).frkOwnerId;
                }

                RetAjaxOut.RetObject = OCustomer;
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
            CustomerCls OCustomer)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateCustomerProcess().Add(OActionSqlParam, OCustomer);
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
