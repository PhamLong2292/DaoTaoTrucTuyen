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
    public class Language : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "Language";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Danh mục ngôn ngữ";
            }
        }

        public override string Description
        {
            get
            {
                return "Danh mục ngôn ngữ";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Language), Page);
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
                    "   function CallAddForm()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.Language.ServerSideDrawAddForm(RenderInfo).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       document.getElementById('txtLanguageCode').focus();\r\n" +
                    "   }\r\n" +


                    "   function CallUpdateForm(LanguageId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.Language.ServerSideDrawUpdateForm(RenderInfo, LanguageId).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       document.getElementById('txtLanguageCode').focus();\r\n" +
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
                    "       LanguageCode = document.getElementById('txtLanguageCode').value;\r\n" +
                    "       LanguageName = document.getElementById('txtLanguageName').value;\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.Language.ServerSideAdd(RenderInfo, LanguageCode, LanguageName, SortIndex, Active).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallReading();\r\n" +
                    "       document.getElementById('txtLanguageName').value='';\r\n" +
                    "       document.getElementById('txtLanguageName').focus();\r\n" +
                    "       document.getElementById('txtSortIndex').value=parseInt(SortIndex,10)+1;\r\n" +
                    "   }\r\n" +


                    "   function CallActionUpdate(LanguageId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       LanguageCode = document.getElementById('txtLanguageCode').value;\r\n" +
                    "       LanguageName = document.getElementById('txtLanguageName').value;\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.Language.ServerSideUpdate(RenderInfo, LanguageId, LanguageCode, LanguageName, SortIndex, Active).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallReading();\r\n" +
                    "       CallBack();\r\n" +
                    "   }\r\n" +

                    "   function CallActionDelete(LanguageId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n" +
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa ngôn ngữ này ra khỏi hệ thống") + "!\", \r\n" +
                    "               type: \"warning\", \r\n" +
                    "               showCancelButton: true, \r\n" +
                    "               confirmButtonColor: \"#DD6B55\", \r\n" +
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n" +
                    "           }, function () { \r\n" +


                    "           AjaxOut = OnlineTour.WebParts.Language.ServerSideDelete(RenderInfo, LanguageId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           CallReading();\r\n" +

                    "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "ngôn ngữ đã được xóa thành công!") + ".\", \"success\"); \r\n" +
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
                    "       Keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       OnlineTour.WebParts.Language.ServerSideDrawSearchResult(RenderInfo, Keyword, CallBackReading);\r\n" +
                    "   }\r\n" +

                    "   function CallBackReading(res)\r\n" +
                    "   {\r\n" +
                    "       AjaxOut = res.value;\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='';\r\n" +
                    "       document.getElementById('divLanguageContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +

                    "</script>\r\n") +
                    WebEnvironments.EncryptHtml(
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +
                        "     <div class=\"ibox-title\"> \r\n" +
                        "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh mục ngôn ngữ") + " </h5> \r\n" +
                        "     </div> \r\n" +
                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "             <div class=\"col-sm-12\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                        "                 <div><input onkeypress=\"if(event.keyCode==13){CallReading();}\"  id=\"txtKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                        "                 <div>\r\n" +
                        "                       <button onclick=\"javascript:CallReading();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Lọc") + "</button>\r\n" +
                        "                       <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:CallAddForm();\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                        "                 </div> \r\n" +
                        "             </div> \r\n" +
                        "         </div> \r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n" +
                        "               <div id=\"divLanguageContent\">" + ServerSideDrawSearchResult(ORenderInfo, "").HtmlContent + "</div>\r\n" +
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
        public static AjaxOut ServerSideDrawSearchResult(RenderInfoCls ORenderInfo, string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                LanguageFilterCls
                    OLanguageFilter = new LanguageFilterCls();
                OLanguageFilter.Keyword = Keyword;

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                LanguageCls[]
                    Coutries = OnlineTourBussinessUtility.CreateBussinessProcess().CreateLanguageProcess().Reading(OActionSqlParam, OLanguageFilter);
                string Html = "";
                if (Coutries.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có dữ liệu theo điều kiện lọc", true);
                }
                else
                {
                    Html +=
                        "   <div class=\"search-result-info\">" + WebLanguage.GetLanguage(OSiteParam, "Tìm thấy") + " (" + Coutries.Length.ToString("#,##0") + ") " + WebLanguage.GetLanguage(OSiteParam, "dữ liệu theo điều kiện lọc") + "</div>\r\n" +
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên ngôn ngữ") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th style=\"width:100px\"></th> \r\n" +

                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < Coutries.Length; iIndex++)
                    {
                        string CityUrl = WebScreen.BuildUrl(OwnerCode, new City().WebPartId,
                            new WebParamCls[]
                            {
                                new WebParamCls("LanguageId",Coutries[iIndex].LanguageId)
                            });
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td>" + Coutries[iIndex].LanguageCode + "</td> \r\n" +
                            "                     <td>" + Coutries[iIndex].LanguageName + "</td> \r\n" +
                            "                     <td>" + (Coutries[iIndex].Active == 1 ? WebLanguage.GetLanguage(OSiteParam, "Sử dụng") : WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng")) + "</td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Sửa ngôn ngữ") + "\" href=\"javascript:CallUpdateForm('" + Coutries[iIndex].LanguageId + "');\"><i class=\"" + WebScreen.GetEditGridIcon() + "\"></i></a></td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa ngôn ngữ") + "\" href=\"javascript:CallActionDelete('" + Coutries[iIndex].LanguageId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            "                     <td class=\"td-center\"><button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:window.open('" + CityUrl + "','_self');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "</strong></button></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
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
                    "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                    "   <option selected  value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                    "</select>\r\n";
                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + "</label> <input id=\"txtLanguageCode\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mã") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên ngôn ngữ") + "</label> <input id=\"txtLanguageName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên ngôn ngữ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input id=\"txtSortIndex\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> " + SelectActiveText + "</div> \r\n" +
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
        public static AjaxOut ServerSideDrawUpdateForm(RenderInfoCls ORenderInfo, string LanguageId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                LanguageCls
                    OLanguage = OnlineTourBussinessUtility.CreateBussinessProcess().CreateLanguageProcess().CreateModel(OActionSqlParam, LanguageId);
                if (OLanguage == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "ngôn ngữ đã bị xóa hoặc không tìm thấy"));
                }
                string SelectActiveText =
                 "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                 "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                 "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                 "</select>\r\n";

                if (OLanguage.Active == 1)
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
                       "             <div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mã") + "</label> <input id=\"txtLanguageCode\" value=\"" + OLanguage.LanguageCode + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mã") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên ngôn ngữ") + "</label> <input id=\"txtLanguageName\" value=\"" + OLanguage.LanguageName + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên ngôn ngữ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input id=\"txtSortIndex\" value=\"" + OLanguage.SortIndex + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> " + SelectActiveText + "</div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + LanguageId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideAdd(
            RenderInfoCls ORenderInfo,
            string LanguageCode,
            string LanguageName,
            string SortIndex,
            int Active)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                if (string.IsNullOrEmpty(LanguageName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên ngôn ngữ không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);


                LanguageCls
                    OLanguage = new LanguageCls();
                OLanguage.LanguageId = System.Guid.NewGuid().ToString();
                OLanguage.LanguageCode = LanguageCode;
                OLanguage.LanguageName = LanguageName;
                OLanguage.SortIndex = int.Parse(SortIndex);
                OLanguage.Active = Active;

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateLanguageProcess().Add(OActionSqlParam, OLanguage);
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
            string LanguageId,
            string LanguageCode,
            string LanguageName,
            string SortIndex,
            int Active)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                     OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);


                if (string.IsNullOrEmpty(LanguageName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên ngôn ngữ không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));


                LanguageCls
                    OLanguage = OnlineTourBussinessUtility.CreateBussinessProcess().CreateLanguageProcess().CreateModel(OActionSqlParam, LanguageId);
                OLanguage.LanguageCode = LanguageCode;
                OLanguage.LanguageName = LanguageName;
                OLanguage.SortIndex = int.Parse(SortIndex);
                OLanguage.Active = Active;
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateLanguageProcess().Save(OActionSqlParam, LanguageId, OLanguage);
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
            string LanguageId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateLanguageProcess().Delete(OActionSqlParam, LanguageId);
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
