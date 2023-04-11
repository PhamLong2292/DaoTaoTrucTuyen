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
    public class CreateQuotationStepOne : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "CreateQuotationStepOne";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Xây dựng chào giá - bắt đầu";
            }
        }

        public override string Description
        {
            get
            {
                return "Xây dựng chào giá - bắt đầu";
            }
        }


        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CreateQuotationStepOne), Page);
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
                    "       StartDate = document.getElementById('txtStartDate').value;\r\n"+
                    "       Adults = document.getElementById('txtAdults').value;\r\n" +
                    "       Children = document.getElementById('txtChildren').value;\r\n" +
                    "       NoOfRoom = document.getElementById('txtNoOfRoom').value;\r\n" +
                    "       HotelCategoryId = document.getElementById('drpSelectServiceTypeCategory').value;\r\n"+

                    "       CustomerName = document.getElementById('txtCustomerName').value;\r\n" +
                    "       CustomerAddress = document.getElementById('txtCustomerAddress').value;\r\n" +
                    "       CustomerMobile = document.getElementById('txtCustomerMobile').value;\r\n" +
                    "       CustomerEmail = document.getElementById('txtCustomerEmail').value;\r\n" +
                    "       QuotationAgencyTemplateId = document.getElementById('drpSelectQuotationAgencyTemplate').value;\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepOne.ServerSideProcess(RenderInfo, StartDate, Adults, Children, NoOfRoom, HotelCategoryId, CustomerName, CustomerAddress, CustomerMobile, CustomerEmail, QuotationAgencyTemplateId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +

                    "   function CallProcessStepOneFromTemplate()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       QuotationTemplateId = document.getElementById('drpSelectQuotationTemplate').value;\r\n" +
                    "       StartDate = document.getElementById('txtFromTemplateStartDate').value;\r\n" +
                    "       Adults = document.getElementById('txtFromTemplateAdusts').value;\r\n" +
                    "       Children = document.getElementById('txtFromTemplateChildren').value;\r\n" +
                    "       NoOfRoom = document.getElementById('txtFromTemplateNoOfRoom').value;\r\n" +
                    "       HotelCategoryId = document.getElementById('drpSelectServiceTypeCategory').value;\r\n" +

                    "       AjaxOut = OneTSQ.WebParts.CreateQuotationStepOne.ServerSideProcessFromTemplate(RenderInfo, QuotationTemplateId, StartDate, Adults, Children, NoOfRoom).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +
                    "</script>\r\n" +
                    ServerSideDrawCreateQuotationStepOne(ORenderInfo).HtmlContent +
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
        public static AjaxOut ServerSideDrawCreateQuotationStepOne(RenderInfoCls ORenderInfo)
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
                    "<select id=\"drpSelectQuotationAgencyTemplate\" class=\"form-control\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Toàn bộ") + "</option>\r\n";
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


                QuotationTemplateCls[]
                 QuotationTemplates = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().ReadingQuotationTemplate(ORenderInfo);

                string SelectQuotationTemplateText =
                    "<select id=\"drpSelectQuotationTemplate\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < QuotationTemplates.Length; iIndex++)
                {
                    SelectQuotationTemplateText +=
                        "   <option value=\"" + QuotationTemplates[iIndex].QuotationTemplateId + "\">" + QuotationTemplates[iIndex].Subject + "</option>\r\n";
                }
                SelectQuotationTemplateText +=
                    "</select>\r\n";
                Html =
                    "<div class=\"ibox float-e-margins\"> \r\n" +
                    "    <div class=\"ibox-title\"> \r\n" +
                    "        <h5>" + WebLanguage.GetLanguage(OSiteParam, "Xây dựng chào giá - bắt đầu") + "</h5> \r\n" +
                    "    </div> \r\n" +
                    "    <div class=\"ibox-content\"> \r\n" +
                    "        <div class=\"row\"> \r\n" +
                    "            <div class=\"col-sm-6 b-r\">\r\n" +
                    "                <h3 class=\"m-t-none m-b\">"+WebLanguage.GetLanguage(OSiteParam, "Xây dựng chào giá mới")+"</h3> \r\n" +
                    //"                <p>Sign in today for more expirience.</p> \r\n" +
                    "                <div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên khách hàng") + "</label> <input id=\"txtCustomerName\" type=\"text\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên khách hàng") + "\" class=\"form-control\"></div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "</label> <input id=\"txtCustomerAddress\" type=\"text\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + "\" class=\"form-control\"></div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Điện thoại") + "</label> <input id=\"txtCustomerMobile\" type=\"text\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điện thoại") + "\" class=\"form-control\"></div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Email") + "</label> <input id=\"txtCustomerEmail\" type=\"text\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Email") + "\" class=\"form-control\"></div> \r\n" +

                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Ngày khởi hành") + "</label> <input id=\"txtStartDate\" type=\"text\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Ngày khởi hành") + "\" class=\"form-control CssDate\" value=\"" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\"></div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số phòng") + "</label> <input class=\"form-control touchspin3\" type=\"text\" value=\"1\" id=\"txtNoOfRoom\"></div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số khách") + "</label> <table><tr><td>" + WebLanguage.GetLanguage(OSiteParam, "Người lớn") + "<td><td>" + WebLanguage.GetLanguage(OSiteParam, "Trẻ em") + "</td></tr>   <tr><td><input class=\"form-control  touchspin3\" type=\"text\" value=\"2\" id=\"txtAdults\"><td><td><input class=\"form-control  touchspin3\" type=\"text\" value=\"0\" id=\"txtChildren\"></td></tr> </table></div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Loại khách sạn") + "</label> " + SelectCategoryRoomText + "</div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mẫu đại lý") + "</label> " + SelectQuotationAgencyTemplateText + "</div> \r\n" + 
                    "                    <div> \r\n" +
                    "                        <button onclick=\"javascript:CallProcessStepOne();\" class=\"btn btn-sm btn-primary pull-right m-t-n-xs\" type=\"button\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bắt đầu") + "</strong></button> \r\n" +
                    "                    </div> \r\n" +
                    "                </div> \r\n" +
                    "            </div> \r\n" +
                    "            <div class=\"col-sm-6\">\r\n"+
                    "                <h3>" + WebLanguage.GetLanguage(OSiteParam, "Mẫu có sẵn") + "</h3> \r\n" +
                    "                <div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Chọn mẫu") + "</label> "+ SelectQuotationTemplateText + "</div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Ngày khởi hành") + "</label> <input type=\"text\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Ngày khởi hành") + "\" id=\"txtFromTemplateStartDate\" class=\"form-control CssDate\" value=\"" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\"></div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số phòng") + "</label> <input class=\"touchspin3\" type=\"text\" value=\"1\" id=\"txtFromTemplateNoOfRoom\"></div> \r\n" +
                    "                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Số khách") + "</label> <table><tr><td>" + WebLanguage.GetLanguage(OSiteParam, "Người lớn") + "<td><td>" + WebLanguage.GetLanguage(OSiteParam, "Trẻ em") + "</td></tr>   <tr><td><input class=\"touchspin3\" type=\"text\" value=\"2\" id=\"txtFromTemplateAdusts\"><td><td><input class=\"touchspin3\" type=\"text\" value=\"0\" id=\"txtFromTemplateChildren\"></td></tr> </table></div> \r\n" +
                    //"                    <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Khách sạn") + "</label> " + SelectCategoryRoomText + "</div> \r\n" +

                    "                    <div> \r\n" +
                    "                        <button onclick=\"javascript:CallProcessStepOneFromTemplate();\" class=\"btn btn-sm btn-primary pull-right m-t-n-xs\" type=\"button\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bắt đầu") + "</strong></button> \r\n" +
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
            string StartDate,
            string Adults,
            string Children,
            string NoOfRoom,
            string HotelCategoryId,
            string CustomerName, 
            string CustomerAddress, 
            string CustomerMobile, 
            string CustomerEmail,
            string QuotationAgencyTemplateId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            //string Html = "";
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerId;

                if (FunctionUtility.checkVnDate(StartDate) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ngày bắt đầu không hợp lệ"));
                if (FunctionUtility.checkInteger(Adults) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số người lớn không hợp lệ"));
                if (FunctionUtility.checkInteger(Children) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số trẻ em không hợp lệ"));
                if (FunctionUtility.checkInteger(NoOfRoom) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số phòng không hợp lệ"));

                QuotationCls
                    OQuotation = new QuotationCls();
                OQuotation.QuotationId = System.Guid.NewGuid().ToString();
                OQuotation.StartDate = FunctionUtility.VNDateToDate(StartDate);
                OQuotation.NoOfDays = 1;
                OQuotation.NoOfRoom = int.Parse(NoOfRoom);
                OQuotation.Adults = int.Parse(Adults);
                OQuotation.Children = int.Parse(Children);
                OQuotation.frkHotelCategoryId = HotelCategoryId;
                OQuotation.frkOwnerId = OwnerId;
                OQuotation.frkOwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                
                OQuotation.CustomerName = CustomerName;
                OQuotation.CustomerAddress = CustomerAddress;
                OQuotation.CustomerMobile = CustomerMobile;
                OQuotation.CustomerEmail = CustomerEmail;
                OQuotation.frkQuotationAgencyTemplateId = QuotationAgencyTemplateId;

                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().Add(ORenderInfo, OQuotation);
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationStepThree().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Id", OQuotation.QuotationId)
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
        public static AjaxOut ServerSideProcessFromTemplate(RenderInfoCls ORenderInfo, string QuotationTemplateId, string StartDate, string Adults, string Children, string NoOfRoom)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            //string Html = "";
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerId;

                if (FunctionUtility.checkVnDate(StartDate) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ngày bắt đầu không hợp lệ"));
                if (FunctionUtility.checkInteger(Adults) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số người lớn không hợp lệ"));
                if (FunctionUtility.checkInteger(Children) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số trẻ em không hợp lệ"));
                if (FunctionUtility.checkInteger(NoOfRoom) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số phòng không hợp lệ"));

                CreateQuotationFromTemplateParamCls
                    OCreateQuotationFromTemplateParam = new CreateQuotationFromTemplateParamCls();
                OCreateQuotationFromTemplateParam.StartDate = FunctionUtility.VNDateToDate(StartDate);
                OCreateQuotationFromTemplateParam.Adults = int.Parse(Adults);
                OCreateQuotationFromTemplateParam.Children = int.Parse(Children);
                OCreateQuotationFromTemplateParam.NoOfRoom = int.Parse(NoOfRoom);

                string QuotationId = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateQuotationFromTemplate(ORenderInfo, OCreateQuotationFromTemplateParam, QuotationTemplateId);
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationStepThree().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Id", QuotationId)
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
