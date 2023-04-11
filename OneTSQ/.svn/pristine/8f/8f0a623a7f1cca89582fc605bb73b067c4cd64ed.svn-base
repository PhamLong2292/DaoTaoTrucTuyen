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
    public class StdService : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "StdService";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Danh mục dịch vụ chuẩn";
            }
        }

        public override string Description
        {
            get
            {
                return "Danh mục dịch vụ chuẩn";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(StdService),Page);
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


                ServiceTypeFilterCls
                    OServiceTypeFilter = new ServiceTypeFilterCls();
                OServiceTypeFilter.ActiveOnly = 1;
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                ServiceTypeCls[]
                    ServiceTypes = OnlineTourBussinessUtility.CreateBussinessProcess().CreateServiceTypeProcess().Reading(ActionSqlParam, OServiceTypeFilter);

                string SelectServiceTypeText = "<select onchange=\"javascript:CallReading();\" id=\"drpSelectServiceType\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < ServiceTypes.Length; iIndex++)
                {
                    SelectServiceTypeText +=
                        "   <option value=\"" + ServiceTypes[iIndex].ServiceTypeId + "\">" + ServiceTypes[iIndex].ServiceTypeName + "</option>\r\n";
                }
                SelectServiceTypeText += "</select>\r\n";
                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +
                    "   function CallAddForm()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.StdService.ServerSideDrawAddForm(RenderInfo).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       document.getElementById('txtStdServiceName').focus();\r\n" +
                    "   }\r\n" +


                    "   function CallUpdateForm(StdServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.StdService.ServerSideDrawUpdateForm(RenderInfo, StdServiceId).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       document.getElementById('txtStdServiceName').focus();\r\n" +
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
                    "       ServiceTypeId = document.getElementById('drpSelectServiceType').value;\r\n" +
                    "       StdServiceName = document.getElementById('txtStdServiceName').value;\r\n" +
                    "       ShortDescription = document.getElementById('txtShortDescription').value;\r\n" +
                    "       Description = document.getElementById('txtDescription').value;\r\n" +
                    "       UnitId = document.getElementById('drpSelectUnit').value;\r\n" +
                    "       SeoTitle = document.getElementById('txtSeoTitle').value;\r\n" +
                    "       SeoKeyword = document.getElementById('txtSeoKeyword').value;\r\n" +
                    "       SeoDescription = document.getElementById('txtSeoDescription').value;\r\n" +
                    "       SeoAuthor = document.getElementById('txtSeoAuthor').value;\r\n" +

                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +

                    "       AjaxOut = OnlineTour.WebParts.StdService.ServerSideAdd(RenderInfo, ServiceTypeId, StdServiceName, ShortDescription, Description, UnitId, SeoTitle, SeoKeyword, SeoAuthor, SeoDescription, SortIndex, Active).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n"+
                    "       {\r\n"+
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n"+
                    "       CallReading();\r\n"+
                    "       document.getElementById('txtStdServiceName').value='';\r\n"+
                    "       document.getElementById('txtShortDescription').value='';\r\n" +
                    "       document.getElementById('txtDescription').value='';\r\n" +
                    "       document.getElementById('txtSeoTitle').value='';\r\n" +
                    "       document.getElementById('txtSeoKeyword').value='';\r\n" +
                    "       document.getElementById('txtSeoDescription').value='';\r\n" +
                    "       document.getElementById('txtSeoAuthor').value='';\r\n" +
                    "       document.getElementById('txtStdServiceName').focus();\r\n" +
                    "       document.getElementById('txtSortIndex').value=parseInt(SortIndex,10)+1;\r\n"+
                    "   }\r\n" +


                    "   function CallActionUpdate(StdServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       StdServiceName = document.getElementById('txtStdServiceName').value;\r\n" +
                    "       ShortDescription = document.getElementById('txtShortDescription').value;\r\n" +
                    "       Description = document.getElementById('txtDescription').value;\r\n" +
                    "       UnitId = document.getElementById('drpSelectUnit').value;\r\n" +
                    "       SeoTitle = document.getElementById('txtSeoTitle').value;\r\n" +
                    "       SeoKeyword = document.getElementById('txtSeoKeyword').value;\r\n" +
                    "       SeoDescription = document.getElementById('txtSeoDescription').value;\r\n" +
                    "       SeoAuthor = document.getElementById('txtSeoAuthor').value;\r\n" +

                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.StdService.ServerSideUpdate(RenderInfo, StdServiceId, StdServiceName, ShortDescription, Description, UnitId, SeoTitle, SeoKeyword, SeoAuthor, SeoDescription, SortIndex, Active).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallReading();\r\n"+
                    "       CallBack();\r\n" +
                    "   }\r\n" +

                    "   function CallActionDelete(StdServiceId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n"+
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \""+WebLanguage.GetLanguage(OSiteParam,"Đang thực hiện xóa dịch vụ chuẩn này ra khỏi hệ thống")+"!\", \r\n"+
                    "               type: \"warning\", \r\n"+
                    "               showCancelButton: true, \r\n"+
                    "               confirmButtonColor: \"#DD6B55\", \r\n"+
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam,"Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n"+
                    "           }, function () { \r\n"+


                    "           AjaxOut = OnlineTour.WebParts.StdService.ServerSideDelete(RenderInfo, StdServiceId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           CallReading();\r\n" +

                    "           swal(\""+WebLanguage.GetLanguage(OSiteParam,"Đã xóa")+"!\", \""+WebLanguage.GetLanguage(OSiteParam,"dịch vụ chuẩn đã được xóa thành công!")+".\", \"success\"); \r\n"+
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
                    "       OnlineTour.WebParts.StdService.ServerSideDrawSearchResult(RenderInfo, Keyword, CallBackReading);\r\n" +
                    "   }\r\n" +

                    "   function CallBackReading(res)\r\n" +
                    "   {\r\n" +
                    "       AjaxOut = res.value;\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='';\r\n" +
                    "       document.getElementById('divStdServiceContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +

                    "</script>\r\n") +
                    WebEnvironments.EncryptHtml(
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +
                        "     <div class=\"ibox-title\"> \r\n" +
                        "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh mục dịch vụ chuẩn") + " </h5> \r\n" +
                        "     </div> \r\n" +
                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "             <div class=\"col-sm-12\"> \r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Loại hình dịch vụ") + "</div>\r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + SelectServiceTypeText + "</div>\r\n" +
                        "                 <div style=\"margin-bottom:5px\">" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa lọc") + "</div>\r\n" +
                        "                 <div><input onkeypress=\"if(event.keyCode==13){CallReading();}\"  id=\"txtKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Điền từ khóa tìm kiếm") + "\" class=\"input-sm form-control\"></div>\r\n" +
                        "                 <div>\r\n" +
                        "                       <button onclick=\"javascript:CallReading();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Lọc") + "</button>\r\n" +
                        "                       <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:CallAddForm();\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                        "                 </div> \r\n" +
                        "             </div> \r\n" +
                        "         </div> \r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n"+
                        "               <div id=\"divStdServiceContent\">"+  ServerSideDrawSearchResult(ORenderInfo,"").HtmlContent +"</div>\r\n"+
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

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                StdServiceFilterCls
                    OStdServiceFilter = new StdServiceFilterCls();
                OStdServiceFilter.Keyword = Keyword;

                string OwnerCode=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                StdServiceCls[] 
                    Coutries = OnlineTourBussinessUtility.CreateBussinessProcess().CreateStdServiceProcess().Reading(OActionSqlParam, OStdServiceFilter);
                string Html = "";
                if (Coutries.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có dữ liệu theo điều kiện lọc", true);
                }
                else
                {
                    Html +=
                        "   <div class=\"search-result-info\">"+WebLanguage.GetLanguage(OSiteParam,"Tìm thấy")+" ("+Coutries.Length.ToString("#,##0")+") "+WebLanguage.GetLanguage(OSiteParam,"dữ liệu theo điều kiện lọc")+"</div>\r\n"+
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Loại hình") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ chuẩn") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị tính") + " </th> \r\n" +
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
                                new WebParamCls("StdServiceId",Coutries[iIndex].StdServiceId)
                            });
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td>" + Coutries[iIndex].ServiceTypeName + "</td> \r\n" +
                            "                     <td>" + Coutries[iIndex].StdServiceName + "</td> \r\n" +
                            "                     <td>" + Coutries[iIndex].UnitName + "</td> \r\n" +
                            "                     <td>" + (Coutries[iIndex].Active == 1 ? WebLanguage.GetLanguage(OSiteParam, "Sử dụng") : WebLanguage.GetLanguage(OSiteParam, "Ngưng sử dụng")) + "</td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Sửa dịch vụ chuẩn") + "\" href=\"javascript:CallUpdateForm('" + Coutries[iIndex].StdServiceId + "');\"><i class=\""+WebScreen.GetEditGridIcon()+"\"></i></a></td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa dịch vụ chuẩn") + "\" href=\"javascript:CallActionDelete('" + Coutries[iIndex].StdServiceId + "');\"><i class=\""+WebScreen.GetDeleteGridIcon()+"\"></i></a></td> \r\n" +
                            "                     <td class=\"td-center\"><button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:window.open('"+CityUrl+"','_self');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thành phố") + "</strong></button></td> \r\n" +
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
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                UnitFilterCls
                    OUnitFilter = new UnitFilterCls();
                UnitCls[]
                    Units = OnlineTourBussinessUtility.CreateBussinessProcess().CreateUnitProcess().Reading(ActionSqlParam, OUnitFilter);

                string SelectUnitText = "<select id=\"drpSelectUnit\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < Units.Length; iIndex++)
                {
                    SelectUnitText += "   <option value=\"" + Units[iIndex].UnitId + "\">" + Units[iIndex].UnitName + "</option>\r\n";
                }
                SelectUnitText += "</select>\r\n";
                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "                <div class=\"row\">\r\n"+
                       "                <div class=\"col-md-6\">\r\n"+
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ chuẩn") + "</label> <input id=\"txtStdServiceName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên dịch vụ chuẩn") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả ngắn") + "</label> <textarea id=\"txtShortDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả ngắn") + "\" class=\"form-control\"></textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả") + "</label> <textarea id=\"txtDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả đầy đủ") + "\" class=\"form-control\"></textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị tính") + "</label> "+SelectUnitText+"</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input value=\"1\" id=\"txtSortIndex\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> "+SelectActiveText+"</div> \r\n" +
                       "                </div>\r\n"+
                       "                <div class=\"col-md-6\">\r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "</label> <input id=\"txtSeoTitle\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "</label> <input id=\"txtSeoKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "</label> <input id=\"txtSeoDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "</label> <input id=\"txtSeoAuthor\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "\" class=\"form-control\"></div> \r\n" +
                       "                </div>\r\n"+
                       "               </div>\r\n" +
                       
                       "             </div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideDrawUpdateForm(RenderInfoCls ORenderInfo, string StdServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                StdServiceCls
                    OStdService = OnlineTourBussinessUtility.CreateBussinessProcess().CreateStdServiceProcess().CreateModel(OActionSqlParam, StdServiceId);
                if (OStdService == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "dịch vụ chuẩn đã bị xóa hoặc không tìm thấy"));
                }
                string SelectActiveText =
                 "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                 "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                 "   <option value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                 "</select>\r\n";

                if (OStdService.Active == 1)
                {
                    SelectActiveText =
                 "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                 "   <option value=\"0\">" + WebLanguage.GetLanguage(OSiteParam, "Không sử dụng") + "</option>\r\n" +
                 "   <option selected value=\"1\">" + WebLanguage.GetLanguage(OSiteParam, "Có sử dụng") + "</option>\r\n" +
                 "</select>\r\n";
                }

                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                
                UnitFilterCls
                    OUnitFilter = new UnitFilterCls();
                UnitCls[]
                    Units = OnlineTourBussinessUtility.CreateBussinessProcess().CreateUnitProcess().Reading(ActionSqlParam, OUnitFilter);

                string SelectUnitText = "<select id=\"drpSelectUnit\" class=\"form-control\">\r\n";
                for (int iIndex = 0; iIndex < Units.Length; iIndex++)
                {
                    if (Units[iIndex].UnitId.Equals(OStdService.frkUnitId))
                    {
                        SelectUnitText += "   <option selected value=\"" + Units[iIndex].UnitId + "\">" + Units[iIndex].UnitName + "</option>\r\n";
                    }
                    else
                    {
                        SelectUnitText += "   <option value=\"" + Units[iIndex].UnitId + "\">" + Units[iIndex].UnitName + "</option>\r\n";
                    }
                }
                SelectUnitText += "</select>\r\n";
                string Html =
                       " <div class=\"ibox-content\"> \r\n" +
                       "     <div class=\"row\"> \r\n" +
                       "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Sửa chữa") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "               <div class=\"row\">\r\n"+
                       "                <div class=\"col-md-6\">\r\n"+
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ chuẩn") + "</label> <input id=\"txtStdServiceName\" value=\"" + OStdService.StdServiceName + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên dịch vụ chuẩn") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả ngắn") + "</label> <textarea id=\"txtShortDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả ngắn") + "\" class=\"form-control\">"+OStdService.ShortDescription+"</textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả") + "</label> <textarea id=\"txtDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập mô tả đầy đủ") + "\" class=\"form-control\">"+OStdService.Description+"</textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị tính") + "</label> " + SelectUnitText + "</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input value=\""+OStdService.SortIndex+"\" id=\"txtSortIndex\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</label> " + SelectActiveText + "</div> \r\n" +
                       "                </div>\r\n"+
                       "                <div class=\"col-md-6\">\r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "</label> <input value=\"" + OStdService.SeoTitle + "\" id=\"txtSeoTitle\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề SEO") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "</label> <input value=\"" + OStdService.SeoKeyword + "\" id=\"txtSeoKeyword\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa SEO") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "</label> <input value=\"" + OStdService.SeoDescription + "\" id=\"txtSeoDescription\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Mô tả SEO") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "</label> <input value=\"" + OStdService.SeoAuthor + "\" id=\"txtSeoAuthor\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tác giả SEO") + "\" class=\"form-control\"></div> \r\n" +
                       "                </div>\r\n"+
                       "               </div>\r\n"+

                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + StdServiceId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
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
            string ServiceTypeId,
            string StdServiceName,
            string ShortDescription,
            string Description,
            string UnitId,
            string SeoTitle,
            string SeoAuthor,
            string SeoKeyword,
            string SeoDescription,
            string SortIndex,
            int Active)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                if (string.IsNullOrEmpty(StdServiceName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ chuẩn không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex)==false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

        
                StdServiceCls 
                    OStdService = new StdServiceCls();
                OStdService.StdServiceId = System.Guid.NewGuid().ToString();
                OStdService.frkServiceTypeId = ServiceTypeId;
                OStdService.StdServiceName = StdServiceName;
                OStdService.frkUnitId = UnitId;
                OStdService.ShortDescription = ShortDescription;
                OStdService.Description = Description;
                OStdService.SeoTitle = SeoTitle;
                OStdService.SeoAuthor = SeoAuthor;
                OStdService.SeoKeyword = SeoKeyword;
                OStdService.SeoDescription = SeoDescription;
                OStdService.SortIndex = int.Parse(SortIndex);
                OStdService.Active = Active;

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateStdServiceProcess().Add(OActionSqlParam, OStdService);
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
            string StdServiceId, 
            string StdServiceName, 
            string ShortDescription, 
            string Description, 
            string UnitId, 
            string SeoTitle, 
            string SeoKeyword, 
            string SeoAuthor, 
            string SeoDescription, 
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


                if (string.IsNullOrEmpty(StdServiceName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên dịch vụ chuẩn không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));


                StdServiceCls
                    OStdService = OnlineTourBussinessUtility.CreateBussinessProcess().CreateStdServiceProcess().CreateModel(OActionSqlParam, StdServiceId);
                OStdService.StdServiceName = StdServiceName;
                OStdService.frkUnitId = UnitId;
                OStdService.ShortDescription = ShortDescription;
                OStdService.Description = Description;
                OStdService.SeoTitle = SeoTitle;
                OStdService.SeoAuthor = SeoAuthor;
                OStdService.SeoKeyword = SeoKeyword;
                OStdService.SeoDescription = SeoDescription;
                OStdService.SortIndex = int.Parse(SortIndex);
                OStdService.Active = Active;
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateStdServiceProcess().Save(OActionSqlParam, StdServiceId, OStdService);
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
            string StdServiceId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateStdServiceProcess().Delete(OActionSqlParam, StdServiceId);
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
