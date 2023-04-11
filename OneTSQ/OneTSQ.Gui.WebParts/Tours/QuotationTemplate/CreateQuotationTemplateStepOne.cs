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
    public class CreateQuotationTemplateStepOne : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "CreateQuotationTemplateStepOne";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Xây dựng mẫu chào giá - bắt đầu";
            }
        }

        public override string Description
        {
            get
            {
                return "Xây dựng mẫu chào giá - bắt đầu";
            }
        }

        public override string[] CheckPermissionWebPartIds
        {
            get
            {
                return new string[] { new ProcessingQuotationTemplateList().WebPartId };
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CreateQuotationTemplateStepOne), Page);
        }

        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true;
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
                    "<script>\r\n" +
                    "   function CallProcessStepOne()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       HotelCategoryId = document.getElementById('drpSelectServiceTypeCategory').value;\r\n"+
                    "       QuotationAgencyTemplateId = document.getElementById('drpSelectQuotationAgencyTemplate').value;\r\n" + 
                    "       Subject = document.getElementById('txtSubject').value;\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationTemplateStepOne.ServerSideProcess(RenderInfo, HotelCategoryId, QuotationAgencyTemplateId, Subject).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +
                    "</script>\r\n" +
                    ServerSideDrawCreateQuotationTemplateStepOne(ORenderInfo).HtmlContent +
                    "<script>\r\n" +
                    
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
        public static AjaxOut ServerSideDrawCreateQuotationTemplateStepOne(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);


                ServiceTypeCategoryFilterCls
                    OServiceTypeCategoryFilter = new ServiceTypeCategoryFilterCls();
                OServiceTypeCategoryFilter.ActiveOnly = 1;

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ServiceTypeCategoryCls[]
                    ServiceTypeCategories = CallBussinessUtility.CreateBussinessProcess().CreateServiceTypeCategoryProcess().Reading(ORenderInfo, OServiceTypeCategoryFilter);

                string SelectCategoryRoomText =
                    "<select id=\"drpSelectServiceTypeCategory\" class=\"form-control\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n";
                for (int iIndex = 0; iIndex < ServiceTypeCategories.Length; iIndex++)
                {
                    SelectCategoryRoomText +=
                        "   <option value=\"" + ServiceTypeCategories[iIndex].ServiceTypeCategoryId + "\">" + ServiceTypeCategories[iIndex].ServiceTypeCategoryName + "</option>\r\n";
                }
                SelectCategoryRoomText +=
                    "</select>\r\n";


                QuotationAgencyTemplateFilterCls
                   OQuotationAgencyTemplateFilter = new QuotationAgencyTemplateFilterCls();
                OQuotationAgencyTemplateFilter.ActiveOnly = 1;


                QuotationAgencyTemplateCls[]
                    QuotationAgencyTemplates = CallBussinessUtility.CreateBussinessProcess().CreateQuotationAgencyTemplateProcess().Reading(ORenderInfo, OQuotationAgencyTemplateFilter);

                string SelectQuotationAgencyTemplateText =
                    "<select id=\"drpSelectQuotationAgencyTemplate\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < QuotationAgencyTemplates.Length; iIndex++)
                {
                    SelectQuotationAgencyTemplateText +=
                        "   <option value=\"" + QuotationAgencyTemplates[iIndex].QuotationAgencyTemplateId + "\">" + QuotationAgencyTemplates[iIndex].Subject + "</option>\r\n";
                }
                SelectQuotationAgencyTemplateText +=
                    "</select>\r\n";

                string SelectFlightStatusText =
                    "<select id=\"drpSelectFlightStatus\" class=\"form-control\">\r\n" +
                    "   <option value=\"1\">"+WebLanguage.GetLanguage(OSiteParam,"Có bay")+"</option>\r\n" +
                    "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không bay") + "</option>\r\n" +
                    "</select>\r\n";


               
                Html =
                    "<div class=\"ibox float-e-margins\"> \r\n" +
                    "    <div class=\"ibox-title\"> \r\n" +
                    "        <h5>" + WebLanguage.GetLanguage(OSiteParam, "Xây dựng chào giá - bắt đầu") + "</h5> \r\n" +
                    "    </div> \r\n" +
                    "    <div class=\"ibox-content\"> \r\n" +
                    "        <div class=\"row\"> \r\n" +
                    "            <div class=\"col-sm-6 b-r\">\r\n" +
                    "                <div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên mẫu chào giá") + "</label> <input id=\"txtSubject\" type=\"text\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên mẫu chào giá") + "\" class=\"form-control\"></div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Loại khách sạn") + "</label> " + SelectCategoryRoomText + "</div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mẫu đại lý") + "</label> " + SelectQuotationAgencyTemplateText + "</div> \r\n" + 
                    "                    <div> \r\n" +
                    "                        <button onclick=\"javascript:CallProcessStepOne();\" class=\"btn btn-sm btn-primary pull-right m-t-n-xs\" type=\"button\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bắt đầu") + "</strong></button> \r\n" +
                    "                    </div> \r\n" +
                    "                </div> \r\n" +
                    "            </div> \r\n" +
                    "        </div> \r\n" +
                    "    </div> \r\n" +
                    "</div> \r\n" +
                    "<script>\r\n" +
                    " $(\".touchspin3\").TouchSpin({ \r\n" +
                    "    verticalbuttons: true, \r\n" +
                    "    buttondown_class: 'btn btn-white', \r\n" +
                    "    buttonup_class: 'btn btn-white' \r\n" +
                    " }); \r\n" +

                    " $('.CssDate').datepicker({\r\n" +
                    "   format: 'dd/mm/yyyy'\r\n" +
                    " });\r\n" +
                    "    var countriesArray = $.map(countries, function (value, key) { return { value: value, data: key }; });\r\n"+

                    "    $('#txtDepartureAirport').autocomplete({\r\n" +
                    "       lookup: countriesArray,\r\n"+

                    "        onSelect: function (suggestion) {\r\n"+
                    "           $('#txtDepartureAirportSelection').html(suggestion.data);\r\n" +
                    "       },\r\n"+
                    "    });\r\n"+
                    "</script>\r\n";

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
        public static AjaxOut ServerSideProcess(
            RenderInfoCls ORenderInfo,
            string HotelCategoryId,
            string QuotationAgencyTemplateId,
            string Subject)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            //string Html = "";
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerId;

                QuotationTemplateCls
                    OQuotationTemplate = new QuotationTemplateCls();
                OQuotationTemplate.QuotationTemplateId = System.Guid.NewGuid().ToString();
                OQuotationTemplate.Subject = Subject;
                OQuotationTemplate.NoOfDays = 1;
                OQuotationTemplate.frkHotelCategoryId = HotelCategoryId;
                OQuotationTemplate.frkOwnerId = OwnerId;
                OQuotationTemplate.frkOwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OQuotationTemplate.frkQuotationAgencyTemplateId = QuotationAgencyTemplateId;

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationTemplateProcess().Add(ORenderInfo, OQuotationTemplate);
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationTemplateStepThree().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Id", OQuotationTemplate.QuotationTemplateId)
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


    }
}
