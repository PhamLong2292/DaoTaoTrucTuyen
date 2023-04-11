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
    public class ClosedQuotationList : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "ClosedQuotationList";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Chào giá đã kết thúc";
            }
        }

        public override string Description
        {
            get
            {
                return "Chào giá đã kết thúc";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(ClosedQuotationList),Page);
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

                    "   function CallUpdateAction(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.ClosedQuotationList.ServerSiteUpdate(RenderInfo, QuotationId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n"+
                    "       {\r\n"+
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "       }\r\n"+
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +


                    "   function CallActionDelete(QuotationId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n"+
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \""+WebLanguage.GetLanguage(OSiteParam,"Đang thực hiện xóa chào giá này ra khỏi hệ thống")+"!\", \r\n"+
                    "               type: \"warning\", \r\n"+
                    "               showCancelButton: true, \r\n"+
                    "               confirmButtonColor: \"#DD6B55\", \r\n"+
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam,"Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n"+
                    "           }, function () { \r\n"+


                    "           AjaxOut = OneTSQ.WebParts.ClosedQuotationList.ServerSideDelete(RenderInfo, QuotationId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           CallReading();\r\n" +

                    "           swal(\""+WebLanguage.GetLanguage(OSiteParam,"Đã xóa")+"!\", \""+WebLanguage.GetLanguage(OSiteParam,"chào giá đã được xóa thành công!")+".\", \"success\"); \r\n"+
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
                    "       OneTSQ.WebParts.ClosedQuotationList.ServerSideDrawSearchResult(RenderInfo, Keyword, CallBackReading);\r\n" +
                    "   }\r\n" +

                    "   function CallBackReading(res)\r\n" +
                    "   {\r\n" +
                    "       AjaxOut = res.value;\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='';\r\n" +
                    "       document.getElementById('divClosedQuotationListContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +

                    "</script>\r\n") +
                    WebEnvironments.EncryptHtml(
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +
                        "     <div class=\"ibox-title\"> \r\n" +
                        "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Chào giá đang mở") + " </h5> \r\n" +
                        "     </div> \r\n" +
                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "             <div class=\"col-sm-12\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                        "                 <div><input onkeypress=\"if(event.keyCode==13){CallReading();}\"  id=\"txtKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                        "                 <div>\r\n" +
                        "                       <button onclick=\"javascript:CallReading();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Lọc chào giá đã đóng") + "</button>\r\n" +
                        "                 </div> \r\n" +
                        "             </div> \r\n" +
                        "         </div> \r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n"+
                        "               <div id=\"divClosedQuotationListContent\">"+  ServerSideDrawSearchResult(ORenderInfo,"").HtmlContent +"</div>\r\n"+
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

                QuotationFilterCls
                    OQuotationFilter = new QuotationFilterCls();
                OQuotationFilter.Keyword = Keyword;
                OQuotationFilter.OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OQuotationFilter.QuotationStatusId = "closed";
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                QuotationCls[] 
                    QuotationLists = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().Reading(ORenderInfo, OQuotationFilter);
                string Html = "";
                if (QuotationLists.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có dữ liệu theo điều kiện lọc", true);
                }
                else
                {
                    Html +=
                        "   <div class=\"search-result-info\">"+WebLanguage.GetLanguage(OSiteParam,"Tìm thấy")+" ("+QuotationLists.Length.ToString("#,##0")+") "+WebLanguage.GetLanguage(OSiteParam,"dữ liệu theo điều kiện lọc")+"</div>\r\n"+
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
                    for (int iIndex = 0; iIndex < QuotationLists.Length; iIndex++)
                    {
                        string CustomerInfo = "";
                        if (!string.IsNullOrEmpty(QuotationLists[iIndex].CustomerName))
                        {
                            CustomerInfo += "<strong> " + WebLanguage.GetLanguage(OSiteParam, "Tên khách") + ":</strong>" + QuotationLists[iIndex].CustomerName;
                        }
                        if (!string.IsNullOrEmpty(QuotationLists[iIndex].CustomerAddress))
                        {
                            CustomerInfo += " <br> <strong> " + WebLanguage.GetLanguage(OSiteParam, "Địa chỉ") + " </strong>: " + QuotationLists[iIndex].CustomerAddress;
                        }
                        if (!string.IsNullOrEmpty(QuotationLists[iIndex].CustomerMobile))
                        {
                            CustomerInfo += "<br><strong> Mobile </strong>: " + QuotationLists[iIndex].CustomerMobile;
                        }
                        if (!string.IsNullOrEmpty(QuotationLists[iIndex].CustomerEmail))
                        {
                            CustomerInfo +=
                             "<br><strong> Email </strong>: " + QuotationLists[iIndex].CustomerEmail;
                        }
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td><a href=\"javascript:CallUpdateAction('" + QuotationLists[iIndex].QuotationId + "');\">" + QuotationLists[iIndex].AutoId + "</a></td> \r\n" +
                            "                     <td>"+ CustomerInfo + "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + QuotationLists[iIndex].StartDate.ToString("dd/MM/yyyy") + "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + QuotationLists[iIndex].Adults.ToString("#,##0") + "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + QuotationLists[iIndex].Children.ToString("#,##0") + "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + QuotationLists[iIndex].NoOfDays.ToString("#,##0") + "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + QuotationLists[iIndex].NoOfRoom.ToString("#,##0") + "</td> \r\n" +
                            "                     <td style=\"text-align:center\">" + QuotationLists[iIndex].QuotationStatusName + "</td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa chào giá") + "\" href=\"javascript:CallActionDelete('" + QuotationLists[iIndex].QuotationId + "');\"><i class=\""+WebScreen.GetDeleteGridIcon()+"\"></i></a></td> \r\n" +
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
        public static AjaxOut ServerSiteUpdate(RenderInfoCls ORenderInfo, string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                
                QuotationCls
                    OQuotation = CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().CreateModel(ORenderInfo, QuotationId);
                if (OQuotation == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chào giá đã bị xóa hoặc không tìm thấy"));
                }
                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OSiteParam, OwnerCode, new CreateQuotationStepThree().WebPartId, new WebParamCls[]
                {
                    new WebParamCls("Id",QuotationId)
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
            string QuotationId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                
                CallBussinessUtility.CreateBussinessProcess().CreateQuotationProcess().Delete(ORenderInfo, QuotationId);
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
