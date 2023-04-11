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
    public class ManagerCustomer : WebPartTemplate
    {
        public static string StaticWebPartId
        {
            get
            {
                return "ManagerCustomer";
            }
        }
        public override string WebPartId
        {
            get
            {
                return StaticWebPartId;
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Quản lý khách hàng";
            }
        }

        public override string Description
        {
            get
            {
                return "Quản lý khách hàng";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(ManagerCustomer),Page);
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

                    "   function CallActionSave(CustomerId)\r\n" +
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

                    "       AjaxOut = OnlineTour.WebParts.ManagerCustomer.ServerSideCreateObject(RenderInfo,CustomerId).value;\r\n" +
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
                    "       AjaxOut = OnlineTour.WebParts.ManagerCustomer.ServerSide(RenderInfo, CustomerId, SaveCustomer).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "   }\r\n" +




                    "</script>\r\n") +
                  ServerSideDrawUpdateForm(ORenderInfo).HtmlContent;
                
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
        public static AjaxOut ServerSideDrawUpdateForm(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                string CustomerId = (string)WebEnvironments.Request("CustomerId");
                ActionSqlParamCls
                    OActionSqlParam=WebEnvironments.CreateActionSqlParam(OSiteParam);
                CustomerCls
                    OCustomer = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCustomerProcess().CreateModel(OActionSqlParam, CustomerId);

                
                string SelectActiveText =
                    "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">"+WebLanguage.GetLanguage(OSiteParam,"Không sử dụng")+"</option>\r\n" +
                    "   <option selected  value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                    "</select>\r\n";
                if (OCustomer.Active == 0)
                {
                    SelectActiveText =
                    "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                    "   <option selected value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                    "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                    "</select>\r\n";
                }
               
                CountryFilterCls 
                    OCountryFilter = new CountryFilterCls();
                OCountryFilter.ActiveOnly = 1;
                CountryCls[] 
                    Countries = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCountryProcess().Reading(OActionSqlParam, OCountryFilter);

                string SelectCountryText = 
                    "<select id=\"drpSelectCountry\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < Countries.Length; iIndex++)
                {
                    if (OCustomer.frkCountryId.Equals(Countries[iIndex].CountryId))
                    {
                        SelectCountryText += "    <option selected value=\"" + Countries[iIndex].CountryId + "\">" + Countries[iIndex].CountryName + "</option>\r\n";
                    }
                    else
                    {
                        SelectCountryText += "    <option value=\"" + Countries[iIndex].CountryId + "\">" + Countries[iIndex].CountryName + "</option>\r\n";
                    }
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
                    if (OCustomer.frkCurrencyId.Equals(Currencies[iIndex].CurrencyId))
                    {
                        SelectCurrencyText += "    <option selected value=\"" + Currencies[iIndex].CurrencyId + "\">" + Currencies[iIndex].CurrencyName + "</option>\r\n";
                    }
                    else
                    {
                        SelectCurrencyText += "    <option value=\"" + Currencies[iIndex].CurrencyId + "\">" + Currencies[iIndex].CurrencyName + "</option>\r\n";
                    }
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
                    if (OCustomer.frkLanguageId.Equals(Languages[iIndex].LanguageId))
                    {
                        SelectLanguageText += "    <option selected value=\"" + Languages[iIndex].LanguageId + "\">" + Languages[iIndex].LanguageName + "</option>\r\n";
                    }
                    else
                    {
                        SelectLanguageText += "    <option value=\"" + Languages[iIndex].LanguageId + "\">" + Languages[iIndex].LanguageName + "</option>\r\n";
                    }
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
                    if (OCustomer.frkCustomerLevelId.Equals(CustomerLevels[iIndex].CustomerLevelId))
                    {
                        SelectCustomerLevelText += "    <option selected value=\"" + CustomerLevels[iIndex].CustomerLevelId + "\">" + CustomerLevels[iIndex].CustomerLevelName + "</option>\r\n";
                    }
                    else
                    {
                        SelectCustomerLevelText += "    <option value=\"" + CustomerLevels[iIndex].CustomerLevelId + "\">" + CustomerLevels[iIndex].CustomerLevelName + "</option>\r\n";
                    }
                }
                SelectCustomerLevelText += "</select>\r\n";

                SiteParam SiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string BackUrl = WebScreen.BuildUrl(OwnerCode, new Customer().WebPartId);

                string group = (string)WebEnvironments.Request("group");
                if (string.IsNullOrEmpty(group)) group = "profile";

                RefItemCls[]
                    RefItems = new RefItemCls[]
                    {
                        new RefItemCls("profile",WebLanguage.GetLanguage(OSiteParam,"Hồ sơ"),"",0,"fa fa-folder-open menu-icon"),
                        new RefItemCls("email",WebLanguage.GetLanguage(OSiteParam,"Email"),"",0,"fa fa-envelope-square menu-icon"),
                        new RefItemCls("invoices",WebLanguage.GetLanguage(OSiteParam,"Hóa đơn"),"",0,"fa fa-money menu-icon"),
                        new RefItemCls("payments",WebLanguage.GetLanguage(OSiteParam,"Thanh toán"),"",0,"fa  fa-line-chart menu-icon"),
                        new RefItemCls("proposals",WebLanguage.GetLanguage(OSiteParam,"Chào giá"),"",0,"fa fa-external-link menu-icon"),
                        new RefItemCls("estimates",WebLanguage.GetLanguage(OSiteParam,"Ước lượng"),"",0,"fa fa-book menu-icon"),
                        new RefItemCls("expenses",WebLanguage.GetLanguage(OSiteParam,"Chi phí"),"",0,"fa fa-heartbeat menu-icon"),
                        new RefItemCls("contracts",WebLanguage.GetLanguage(OSiteParam,"Hợp đồng"),"",0,"fa fa-file menu-icon"),
                        new RefItemCls("projects",WebLanguage.GetLanguage(OSiteParam,"Dự án"),"",0,"fa fa-file-powerpoint-o"),
                        new RefItemCls("tickets",WebLanguage.GetLanguage(OSiteParam,"Ticket"),"",0,"fa  fa-ticket  menu-icon"),
                        new RefItemCls("tasks",WebLanguage.GetLanguage(OSiteParam,"Công việc"),"",0,"fa fa-tasks menu-icon"),
                        new RefItemCls("attachments",WebLanguage.GetLanguage(OSiteParam,"Tài liệu"),"",0,"fa fa-paperclip menu-icon"),
                        new RefItemCls("reminders",WebLanguage.GetLanguage(OSiteParam,"Nhắc việc"),"",0,"fa fa-clock-o menu-icon"),
                        new RefItemCls("notes",WebLanguage.GetLanguage(OSiteParam,"Ghi chú"),"",0,"fa fa-file-text-o menu-icon"),
                        new RefItemCls("map",WebLanguage.GetLanguage(OSiteParam,"Liên hệ"),"",0,"fa fa-map-marker menu-icon"),
                    };

                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Quản lý khách hàng") + "</h3> \r\n" +
                    "             <div class=\"row\"> \r\n" +
                    "               <div class=\"col-md-2\">\r\n" +
                    "               <div class=\"tabs-left\">\r\n" +
                    "               <ul class=\"nav\">\r\n";



                for (int iIndex = 0; iIndex < RefItems.Length; iIndex++)
                {
                    if (RefItems[iIndex].Code.Equals(group))
                    {
                        Html +=
                            "               <li style=\"background-color:silver\" class=\"\">\r\n" +
                            "                    <a style=\"padding:6px;color:maroon;font-weight:bold\" data-group=\"" + RefItems[iIndex].Code + "\" href=\"" + WebScreen.BuildUrl(OSiteParam, OwnerCode, StaticWebPartId, new WebParamCls[] { new WebParamCls("CustomerId", CustomerId), new WebParamCls("group", RefItems[iIndex].Code) }) + "\" ><i class=\"" + RefItems[iIndex].Class + "\" aria-hidden=\"true\"></i>" + RefItems[iIndex].Subject + "</a>\r\n" +
                            "               </li>\r\n";
                    }
                    else
                    {
                        Html +=
                            "               <li class=\"\">\r\n" +
                            "                    <a style=\"padding:6px;color:green;font-weight:bold\" data-group=\"" + RefItems[iIndex].Code + "\" href=\"" + WebScreen.BuildUrl(OSiteParam, OwnerCode, StaticWebPartId, new WebParamCls[] { new WebParamCls("CustomerId", CustomerId), new WebParamCls("group", RefItems[iIndex].Code) }) + "\" ><i class=\"" + RefItems[iIndex].Class + "\" aria-hidden=\"true\"></i>" + RefItems[iIndex].Subject + "</a>\r\n" +
                            "               </li>\r\n";
                    }
                }
                Html +=
                    "           </ul>\r\n" +
                    "           </div>\r\n" +
                    "               </div>\r\n" +
                    "               <div class=\"col-md-10\">\r\n";
                if (group.Equals("profile"))
                {
                    Html +=
                        "                    <div class=\"tabs-container\"> \r\n" +
                        "                        <ul class=\"nav nav-tabs\"> \r\n" +
                        "                            <li class=\"active\"><a data-toggle=\"tab\" href=\"#tab-1\"> " + WebLanguage.GetLanguage(OSiteParam, "Thông tin chung") + " </a></li> \r\n" +
                        "                            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-2\">" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ hóa đơn") + "</a></li> \r\n" +
                        "                            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-3\">" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ giao hàng") + "</a></li> \r\n" +
                        "                            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-4\">" + WebLanguage.GetLanguage(OSiteParam, "Người liên hệ") + "</a></li> \r\n" +
                        "                            <li class=\"\"><a data-toggle=\"tab\" href=\"#tab-5\">" + WebLanguage.GetLanguage(OSiteParam, "Quản lý bởi") + "</a></li> \r\n" +
                        "                        </ul> \r\n" +
                        "                        <div class=\"tab-content\"> \r\n" +
                        "                            <div id=\"tab-1\" class=\"tab-pane active\"> \r\n" +
                        "                                <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +

                        "                                   <div class=\"row\">\r\n" +
                        "                                       <div class=\"col-md-6\">\r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên khách hàng") + "</label> <input value=\"" + OCustomer.Company + "\" id=\"txtCompany\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên khách hàng") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "</label> <input value=\"" + OCustomer.Address + "\"  id=\"txtAddress\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số điện thoại") + "</label> <input value=\"" + OCustomer.Phone + "\"  value=\"" + OCustomer.Phone + "\"  id=\"txtPhone\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Số điện thoại") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Email") + "</label> <input id=\"txtEmail\" value=\"" + OCustomer.Email + "\"  type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Email") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã số thuế") + "</label> <input id=\"txtVat\" value=\"" + OCustomer.Vat + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Vat") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Phân nhóm") + "</label> " + SelectCustomerLevelText + "</div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiền tệ") + "</label> " + SelectCurrencyText + "</div> \r\n" +
                        "                                       </div>\r\n" +

                        "                                       <div class=\"col-md-6\">\r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Ngôn ngữ") + "</label> " + SelectLanguageText + "</div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Quốc gia") + "</label>" + SelectCountryText + " </div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "</label> <input value=\"\"+value=\"" + OCustomer.City + "\" id=\"txtCityName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Website") + "</label> <input id=\"txtWebsite\" value=\"" + OCustomer.WebSite + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Website") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Zip code") + "</label> <input id=\"txtZipcode\" value=\"" + OCustomer.ZipCode + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Zip code") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input value=\"" + OCustomer.SortIndex + "\" id=\"txtSortIndex\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> " + SelectActiveText + "</div> \r\n" +
                        "                                       </div>\r\n" +
                        "                                   </div>\r\n" +


                        "                                </div> \r\n" +
                        "                            </div> \r\n" +
                        "                            <div id=\"tab-2\" class=\"tab-pane\"> \r\n" +
                        "                                <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +
                        "                                   <div class=\"row\">\r\n" +
                        "                                       <div class=\"col-md-6\">\r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ thanh toán") + "</label> <input value=\"" + OCustomer.BillingAddress + "\"  id=\"txtBillingAddress\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ thanh toán") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "</label> <input value=\"" + OCustomer.BillingCity + "\"  id=\"txtBillingCity\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "\" class=\"form-control\"></div> \r\n" +
                        "                                       </div>\r\n" +

                        "                                       <div class=\"col-md-6\">\r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Quận huyện") + "</label> <input value=\"" + OCustomer.BillingState + "\"   id=\"txtBillingState\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Quận huyện") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Khác") + "</label> <input value=\"" + OCustomer.BillingZipCode + "\"   id=\"txtBillingZipCode\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Khác") + "\" class=\"form-control\"></div> \r\n" +
                        "                                       </div>\r\n" +
                        "                                   </div>\r\n" +
                        "                                </div> \r\n" +
                        "                            </div> \r\n" +

                        "                            <div id=\"tab-3\" class=\"tab-pane\"> \r\n" +
                        "                                <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +
                         "                                   <div class=\"row\">\r\n" +
                        "                                       <div class=\"col-md-6\">\r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ giao hàng") + "</label> <input value=\"" + OCustomer.ShippingAddress + "\"   id=\"txtShippingAddress\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ giao hàng") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "</label> <input value=\"" + OCustomer.ShippingCity + "\" id=\"txtShippingCity\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "\" class=\"form-control\"></div> \r\n" +
                        "                                       </div>\r\n" +

                        "                                       <div class=\"col-md-6\">\r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Quận huyện") + "</label> <input value=\"" + OCustomer.ShippingState + "\" id=\"txtShippingState\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Quận huyện") + "\" class=\"form-control\"></div> \r\n" +
                        "                                           <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Khác") + "</label> <input id=\"txtShippingZipCode\" value=\"" + OCustomer.ShippingZipCode + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Khác") + "\" class=\"form-control\"></div> \r\n" +
                        "                                       </div>\r\n" +
                        "                                   </div>\r\n" +
                        "                                </div> \r\n" +
                        "                            </div> \r\n" +

                        "                            <div id=\"tab-4\" class=\"tab-pane\"> \r\n" +
                        "                                <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +

                        "                                </div> \r\n" +
                        "                            </div> \r\n" +

                        "                            <div id=\"tab-5\" class=\"tab-pane\"> \r\n" +
                        "                                <div class=\"panel-body\" style=\"min-height:400px\"> \r\n" +

                        "                                </div> \r\n" +
                        "                            </div> \r\n" +
                        "                        </div> \r\n" +
                        "                    </div>    \r\n" +
                        "                       <div style=\"margin-top:10px\"> \r\n" +
                        "                           <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionSave('" + CustomerId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                        "                           <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:window.open('" + BackUrl + "','_self');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                        "                       </div> \r\n" +
                        "                  </div>\r\n";
                }
                else
                {
                    ReferenceObjectFilterCls
                      OReferenceObjectFilter = new ReferenceObjectFilterCls();
                    OReferenceObjectFilter.SrcObjectId = CustomerId;
                    OReferenceObjectFilter.RefecenceObjectType = group;
                    ReferenceObjectCls[]
                        ReferenceObjects = OnlineTourBussinessUtility.CreateBussinessProcess().CreateReferenceObjectProcess().Reading(OActionSqlParam, OReferenceObjectFilter);
                    Html +=
                        "    <h3>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách - ") + WebLanguage.GetLanguage(OSiteParam, group) + "</h3>\r\n" +
                        "    <hr />\r\n" +
                       "         <div class=\"table-responsive\"> \r\n" +
                       "             <table class=\"table table-striped\"> \r\n" +
                       "                 <thead> \r\n" +
                       "                 <tr> \r\n" +
                       "                     <th class=\"th-func-20\"></th> \r\n" +
                       "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                       "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Hồ sơ") + " </th> \r\n" +
                       "                     <th class=\"th-func-20\"></th> \r\n" +
                       "                 </tr> \r\n" +
                       "                 </thead> \r\n" +
                       "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < ReferenceObjects.Length; iIndex++)
                    {
                        Html +=
                            "                 <tr id=\"tr" + ReferenceObjects[iIndex].ReferenceObjectId + "\"> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td>" + ReferenceObjects[iIndex].LinkObjectCode + "</td> \r\n" +
                            "                     <td>" + ReferenceObjects[iIndex].LinkObjectSubject + "</td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa khỏi hồ sơ") + "\" href=\"javascript:CallActionDeleteReferenceObject('" + ReferenceObjects[iIndex].ReferenceObjectId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n";
                }
                Html+=   
                    "                </div>\r\n"+


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
        public static AjaxOut ServerSide(
            RenderInfoCls ORenderInfo, 
            string CustomerId,
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
                
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateCustomerProcess().Save(OActionSqlParam, CustomerId, OCustomer);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Cập nhật khách hàng thành công");
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
