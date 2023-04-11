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
    public class PendingBookingList : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "PendingBookingList";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Booking tạm dừng";
            }
        }

        public override string Description
        {
            get
            {
                return "Booking tạm dừng";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(PendingBookingList),Page);
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
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +

                    "   function CallUpdateAction(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.PendingBookingList.ServerSiteUpdate(RenderInfo, BookingId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n"+
                    "       {\r\n"+
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "       }\r\n"+
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +


                    "   function CallActionDelete(BookingId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n"+
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \""+WebLanguage.GetLanguage(OSiteParam,"Đang thực hiện xóa Booking này ra khỏi hệ thống")+"!\", \r\n"+
                    "               type: \"warning\", \r\n"+
                    "               showCancelButton: true, \r\n"+
                    "               confirmButtonColor: \"#DD6B55\", \r\n"+
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam,"Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n"+
                    "           }, function () { \r\n"+


                    "           AjaxOut = OneTSQ.WebParts.PendingBookingList.ServerSideDelete(RenderInfo, BookingId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           CallReading();\r\n" +

                    "           swal(\""+WebLanguage.GetLanguage(OSiteParam,"Đã xóa")+"!\", \""+WebLanguage.GetLanguage(OSiteParam,"Booking đã được xóa thành công!")+".\", \"success\"); \r\n"+
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
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       OneTSQ.WebParts.PendingBookingList.ServerSideDrawSearchResult(RenderInfo, Keyword, CallBackReading);\r\n" +
                    "   }\r\n" +

                    "   function CallBackReading(res)\r\n" +
                    "   {\r\n" +
                    "       AjaxOut = res.value;\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='';\r\n" +
                    "       document.getElementById('divPendingBookingListContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +

                    "</script>\r\n") +
                    WebEnvironments.EncryptHtml(
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +
                        "     <div class=\"ibox-title\"> \r\n" +
                        "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Booking tạm dừng") + " </h5> \r\n" +
                        "     </div> \r\n" +
                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "             <div class=\"col-sm-12\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                        "                 <div><input onkeypress=\"if(event.keyCode==13){CallReading();}\"  id=\"txtKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                        "                 <div>\r\n" +
                        "                       <button onclick=\"javascript:CallReading();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Lọc Booking tạm dừng") + "</button>\r\n" +
                        "                 </div> \r\n" +
                        "             </div> \r\n" +
                        "         </div> \r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n"+
                        "               <div id=\"divPendingBookingListContent\">"+  ServerSideDrawSearchResult(ORenderInfo,"").HtmlContent +"</div>\r\n"+
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
        public static AjaxOut ServerSideDrawSearchResult(RenderInfoCls ORenderInfo,string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                BookingFilterCls
                    OBookingFilter = new BookingFilterCls();
                OBookingFilter.Keyword = Keyword;
                OBookingFilter.OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OBookingFilter.BookingStatusId = "pending";
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                BookingCls[] 
                    BookingLists = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().Reading(ORenderInfo, OBookingFilter);
                string Html = "";
                if (BookingLists.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có dữ liệu theo điều kiện lọc", true);
                }
                else
                {
                    Html +=
                        "   <div class=\"search-result-info\">"+WebLanguage.GetLanguage(OSiteParam,"Tìm thấy")+" ("+BookingLists.Length.ToString("#,##0")+") "+WebLanguage.GetLanguage(OSiteParam,"dữ liệu theo điều kiện lọc")+"</div>\r\n"+
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                        "                     <th style=\"text-align:left\">" + WebLanguage.GetLanguage(OSiteParam, "Thông tin khách hàng") + " </th> \r\n" +
                        "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Ngày khởi hành") + " </th> \r\n" +
                        "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Người lớn") + " </th> \r\n" +
                        "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Trẻ em") + " </th> \r\n" +
                        "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Số đêm") + " </th> \r\n" +
                        "                     <th style=\"text-align:center\">" + WebLanguage.GetLanguage(OSiteParam, "Số phòng") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < BookingLists.Length; iIndex++)
                    {
                        string CustomerInfo = "";
                        if (!string.IsNullOrEmpty(BookingLists[iIndex].CustomerName)) {
                            CustomerInfo += "<strong> " + WebLanguage.GetLanguage(OSiteParam, "Tên khách") + ":</strong>" + BookingLists[iIndex].CustomerName;
                        }
                        if (!string.IsNullOrEmpty(BookingLists[iIndex].CustomerAddress)) {
                            CustomerInfo += " <br> <strong> " + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + " </strong>: " + BookingLists[iIndex].CustomerAddress;
                        }
                        if (!string.IsNullOrEmpty(BookingLists[iIndex].CustomerMobile))
                        {
                            CustomerInfo += "<br><strong> Mobile </strong>: " + BookingLists[iIndex].CustomerMobile;
                        }
                        if (!string.IsNullOrEmpty(BookingLists[iIndex].CustomerEmail))
                        {
                            CustomerInfo +=
                             "<br><strong> Email </strong>: " + BookingLists[iIndex].CustomerEmail;
                        }
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td><a href=\"javascript:CallUpdateAction('" + BookingLists[iIndex].BookingId + "');\">" + BookingLists[iIndex].AutoId + "</a></td> \r\n" +
                            "                     <td>" +CustomerInfo+ "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + BookingLists[iIndex].StartDate.ToString("dd/MM/yyyy") + "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + BookingLists[iIndex].Adults.ToString("#,##0") + "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + BookingLists[iIndex].Children.ToString("#,##0") + "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + BookingLists[iIndex].NoOfDays.ToString("#,##0") + "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + BookingLists[iIndex].NoOfRoom.ToString("#,##0") + "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + BookingLists[iIndex].BookingStatusName + "</td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa Booking") + "\" href=\"javascript:CallActionDelete('" + BookingLists[iIndex].BookingId + "');\"><i class=\""+WebScreen.GetDeleteGridIcon()+"\"></i></a></td> \r\n" +
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
        public static AjaxOut ServerSiteUpdate(RenderInfoCls ORenderInfo, string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                
                BookingCls
                    OBooking = CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().CreateModel(ORenderInfo, BookingId);
                if (OBooking == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Booking đã bị xóa hoặc không tìm thấy"));
                }
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateBookingStepThree().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("Id",BookingId)
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
            string BookingId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                
                CallBussinessUtility.CreateBussinessProcess().CreateBookingProcess().Delete(ORenderInfo, BookingId);
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
