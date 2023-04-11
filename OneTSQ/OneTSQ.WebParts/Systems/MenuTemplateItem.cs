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
    public class MenuTemplateItem : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "MenuTemplateItem";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Danh mục chức năng thuộc nhóm";
            }
        }

        public override string Description
        {
            get
            {
                return "Danh mục chức năng thuộc nhóm";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(MenuTemplateItem),Page);
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

                string MenuTemplateGroupFunctionId = (string)WebEnvironments.Request("MenuTemplateGroupFunctionId");
                if (string.IsNullOrEmpty(MenuTemplateGroupFunctionId)) throw new Exception("Tham số không hợp lệ");

                ActionSqlParamCls
                  OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                MenuTemplateGroupFunctionCls
                    OMenuTemplateGroupFunction = OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateGroupFunctionProcess().CreateModel(OActionSqlParam, MenuTemplateGroupFunctionId);
                if (OMenuTemplateGroupFunction == null) throw new Exception("Nhóm menu chức năng không tìm thấy");

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string BackUrl = WebScreen.BuildUrl(OwnerCode, new MenuTemplateGroupFunction().WebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("menutemplateid",OMenuTemplateGroupFunction.frkMenuTemplateId)
                    });

                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +
                    "   function CallAddForm()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       MenuTemplateGroupFunctionId= document.getElementById('txtMenuTemplateGroupFunctionId').value;\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.MenuTemplateItem.ServerSideDrawAddForm(RenderInfo, MenuTemplateGroupFunctionId).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       $('#drpSelectWebPart').select2();\r\n"+
                    "   }\r\n" +


                    "   function CallUpdateForm(MenuTemplateItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.MenuTemplateItem.ServerSideDrawUpdateForm(RenderInfo, MenuTemplateItemId).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       document.getElementById('drpSelectWebPart').focus();\r\n" +
                    "   }\r\n" +

                    "   function CallConfig(MenuTemplateItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.MenuTemplateItem.ServerSideConfigUrl(RenderInfo, MenuTemplateItemId).value;\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n"+

                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                    "       document.getElementById('divActionForm').innerHTML='';\r\n" +
                    "       document.getElementById('divActionForm').style.display='none';\r\n" +
                    "       document.getElementById('divListForm').style.display='block';\r\n" +
                    "   }\r\n" +

                    "   function CallActionAdd()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       MenuTemplateGroupFunctionId= document.getElementById('txtMenuTemplateGroupFunctionId').value;\r\n" +
                    "       WebPartId= document.getElementById('drpSelectWebPart').value;\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +

                    "       AjaxOut = OnlineTour.WebParts.MenuTemplateItem.ServerSideAdd(RenderInfo, MenuTemplateGroupFunctionId, WebPartId, SortIndex).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n"+
                    "       {\r\n"+
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n"+
                    "       CallReading();\r\n"+
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "       document.getElementById('drpSelectWebPart').selectedIndex=0;\r\n"+
                    "       document.getElementById('drpSelectWebPart').focus();\r\n"+
                    "   }\r\n" +


                    "   function CallActionUpdate(MenuTemplateItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       MenuTemplateItemName = document.getElementById('txtMenuTemplateItemName').value;\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.MenuTemplateItem.ServerSideUpdate(RenderInfo, MenuTemplateItemId, MenuTemplateItemName, SortIndex).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallReading();\r\n"+
                    "       CallBack();\r\n" +
                    "   }\r\n" +

                    "   function CallActionDelete(MenuTemplateItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n"+
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \""+WebLanguage.GetLanguage(OSiteParam,"Đang thực hiện xóa menu chức năng này ra khỏi hệ thống")+"!\", \r\n"+
                    "               type: \"warning\", \r\n"+
                    "               showCancelButton: true, \r\n"+
                    "               confirmButtonColor: \"#DD6B55\", \r\n"+
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam,"Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n"+
                    "           }, function () { \r\n"+


                    "           AjaxOut = OnlineTour.WebParts.MenuTemplateItem.ServerSideDelete(RenderInfo, MenuTemplateItemId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           CallReading();\r\n" +

                    "           swal(\""+WebLanguage.GetLanguage(OSiteParam,"Đã xóa")+"!\", \""+WebLanguage.GetLanguage(OSiteParam,"menu chức năng đã được xóa thành công!")+".\", \"success\"); \r\n"+
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
                    "       MenuTemplateGroupFunctionId = document.getElementById('txtMenuTemplateGroupFunctionId').value;\r\n" +
                    "       OnlineTour.WebParts.MenuTemplateItem.ServerSideDrawSearchResult(RenderInfo, MenuTemplateGroupFunctionId, CallBackReading);\r\n" +
                    "   }\r\n" +

                    "   function CallBackReading(res)\r\n" +
                    "   {\r\n" +
                    "       AjaxOut = res.value;\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='';\r\n" +
                    "       document.getElementById('divMenuTemplateItemContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +

                    "   function SaveIndex(MenuTemplateItemId)\r\n"+
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndex'+MenuTemplateItemId).value;\r\n" +
                    "       AjaxOut  = OnlineTour.WebParts.MenuTemplateItem.ServerSideSaveIndex(RenderInfo, MenuTemplateItemId, SortIndex).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           document.getElementById('divProcessingSave').innerHTML = AjaxOut.HtmlContent;\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "   }\r\n" +

                    "   function SaveDisplayText(MenuTemplateItemId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       DisplayText = document.getElementById('txtDisplayText'+MenuTemplateItemId).value;\r\n"+
                    "       AjaxOut  = OnlineTour.WebParts.MenuTemplateItem.ServerSideSaveDisplayText(RenderInfo, MenuTemplateItemId, DisplayText).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           document.getElementById('divProcessingSave').innerHTML = AjaxOut.HtmlContent;\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +

                    "   }\r\n" +

                    "</script>\r\n") +
                    WebEnvironments.EncryptHtml(
                        "<input id=\"txtMenuTemplateGroupFunctionId\" type=\"hidden\" value=\"" + MenuTemplateGroupFunctionId + "\">\r\n" +
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +
                        "     <div class=\"ibox-title\"> \r\n" +
                        "         <h5>" + OMenuTemplateGroupFunction.MenuTemplateGroupFunctionName + " </h5> \r\n" +
                        "     </div> \r\n" +

                        "     <div>\r\n"+
                        "         <button onclick=\"javascript:CallReading();\" type=\"button\" class=\"btn btn-sm btn-primary mr-5px\"> " + WebLanguage.GetLanguage(OSiteParam, "Lọc chức năng") + "</button>\r\n" +
                        "         <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:CallAddForm();\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm chức năng") + "</button>\r\n" +
                        "         <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:window.open('" + BackUrl + "','_self');\"> " + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</button>\r\n" +
                        "     </div> \r\n"+

                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n"+
                        "               <div id=\"divMenuTemplateItemContent\">"+  ServerSideDrawSearchResult(ORenderInfo,MenuTemplateGroupFunctionId).HtmlContent +"</div>\r\n"+
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
        public static AjaxOut ServerSideDrawSearchResult(RenderInfoCls ORenderInfo, string MenuTemplateGroupFunctionId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                
                MenuTemplateItemCls[]
                    MenuTemplateItems = OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateItemProcess().Reading(OActionSqlParam, MenuTemplateGroupFunctionId);
                string Html = "";
                if (MenuTemplateItems.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có dữ liệu theo điều kiện lọc", true);
                }
                else
                {
                    Html +=
                        //"   <div class=\"search-result-info\">" + WebLanguage.GetLanguage(OSiteParam, "Tìm thấy") + " (" + MenuTemplateItems.Length.ToString("#,##0") + ") " + WebLanguage.GetLanguage(OSiteParam, "dữ liệu theo điều kiện lọc") + "</div>\r\n" +
                        "         <div id=\"divProcessingSave\"></div>\r\n"+
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên chức năng") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < MenuTemplateItems.Length; iIndex++)
                    {
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td style=\"width:70px\"><input style=\"width:50px\" class=\"form-control\" onblur=\"javascript:SaveIndex('" + MenuTemplateItems[iIndex].MenuTemplateItemId + "');\" id=\"txtSortIndex" + MenuTemplateItems[iIndex].MenuTemplateItemId + "\" value=\"" + MenuTemplateItems[iIndex].SortIndex + "\"></td> \r\n" +
                            "                     <td><input class=\"form-control\" onblur=\"javascript:SaveDisplayText('" + MenuTemplateItems[iIndex].MenuTemplateItemId + "');\" id=\"txtDisplayText" + MenuTemplateItems[iIndex].MenuTemplateItemId + "\" value=\"" + MenuTemplateItems[iIndex].DisplayText + "\"></td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Sửa menu chức năng") + "\" href=\"javascript:CallUpdateForm('" + MenuTemplateItems[iIndex].MenuTemplateItemId + "');\"><i class=\""+WebScreen.GetEditGridIcon()+"\"></i></a></td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa menu chức năng") + "\" href=\"javascript:CallActionDelete('" + MenuTemplateItems[iIndex].MenuTemplateItemId + "');\"><i class=\""+WebScreen.GetDeleteGridIcon()+"\"></i></a></td> \r\n" +
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
        public static AjaxOut ServerSideDrawAddForm(RenderInfoCls ORenderInfo, string MenuTemplateGroupFunctionId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                WebPartFunctionFilterCls
                    OWebPartFunctionFilter = new WebPartFunctionFilterCls();
                OWebPartFunctionFilter.ActiveOnly = 1;

                WebPartFunctionCls[]
                    WebPartFunctions = OnlineTourBussinessUtility.CreateBussinessProcess().CreateWebPartFunctionProcess().Reading(OActionSqlParam, OWebPartFunctionFilter);
                string SelectWebPartFunctionText =
                    "<select id=\"drpSelectWebPart\" class=\"form-control select2\">\r\n"+
                    "    <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam,"") + "</option>\r\n";
                for (int iIndex = 0; iIndex < WebPartFunctions.Length; iIndex++)
                {
                    SelectWebPartFunctionText += 
                        "    <option value=\"" + WebPartFunctions[iIndex].WebPartFunctionId + "\">" + WebPartFunctions[iIndex].WebPartFunctionName + "</option>\r\n";
                }
                SelectWebPartFunctionText+=
                    "</select>\r\n";
                MenuTemplateItemCls[]
                    MenuTemplateItems = OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateItemProcess().Reading(OActionSqlParam, MenuTemplateGroupFunctionId);

                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Chức năng") + "</label> " + SelectWebPartFunctionText + "</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input value=\"" + MenuTemplateItems.Length+1 + "\" id=\"txtSortIndex\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
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
        public static AjaxOut ServerSideDrawUpdateForm(
            RenderInfoCls ORenderInfo, 
            string MenuTemplateItemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                MenuTemplateItemCls
                    OMenuTemplateItem = OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateItemProcess().CreateModel(OActionSqlParam, MenuTemplateItemId);
                if (OMenuTemplateItem == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "menu chức năng đã bị xóa hoặc không tìm thấy"));
                }

                string Html =
                       " <div class=\"ibox-content\"> \r\n" +
                       "     <div class=\"row\"> \r\n" +
                       "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Sửa chữa") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên menu chức năng") + "</label> <input id=\"txtMenuTemplateItemName\" value=\"" + OMenuTemplateItem.DisplayText + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên menu chức năng") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input id=\"txtSortIndex\" value=\"" + OMenuTemplateItem.SortIndex + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + MenuTemplateItemId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
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
            string MenuTemplateGroupFunctionId,
            string WebPartFunctionId,
            string SortIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                if (string.IsNullOrEmpty(MenuTemplateGroupFunctionId)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chức năng không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex)==false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));

                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                WebPartFunctionCls
                    OWebPartFunction = OnlineTourBussinessUtility.CreateBussinessProcess().CreateWebPartFunctionProcess().CreateModel(OActionSqlParam, WebPartFunctionId);
                if (OWebPartFunction == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chức năng không tìm thấy"));
                }

                MenuTemplateItemCls 
                    OMenuTemplateItem = new MenuTemplateItemCls();
                OMenuTemplateItem.MenuTemplateItemId = System.Guid.NewGuid().ToString();
                OMenuTemplateItem.DisplayText = OWebPartFunction.WebPartFunctionName;
                OMenuTemplateItem.SortIndex = int.Parse(SortIndex);
                OMenuTemplateItem.frkWebPartFunctionId = WebPartFunctionId;
                OMenuTemplateItem.frkMenuTemplateGroupFunctionId = MenuTemplateGroupFunctionId;
                
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateItemProcess().Add(OActionSqlParam, OMenuTemplateItem);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đưa chức năng vào menu thành công");
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
            string MenuTemplateItemId, 
            string DisplayText,
            string SortIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                     OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);


                if (string.IsNullOrEmpty(DisplayText)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên menu chức năng không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));


                MenuTemplateItemCls
                    OMenuTemplateItem = OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateItemProcess().CreateModel(OActionSqlParam, MenuTemplateItemId);
                OMenuTemplateItem.DisplayText = DisplayText;
                OMenuTemplateItem.SortIndex = int.Parse(SortIndex);
                
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateItemProcess().Save(OActionSqlParam, MenuTemplateItemId, OMenuTemplateItem);
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
            string MenuTemplateItemId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateItemProcess().Delete(OActionSqlParam, MenuTemplateItemId);
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
        public static AjaxOut ServerSideSaveIndex(
            RenderInfoCls ORenderInfo,
            string MenuTemplateItemId,
            string SortIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                if (FunctionUtilities.checkInteger(SortIndex) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Số thứ tự không hợp lệ"));
                MenuTemplateItemCls
                    OMenuTemplateItem = OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateItemProcess().CreateModel(OActionSqlParam, MenuTemplateItemId);
                OMenuTemplateItem.SortIndex = int.Parse(SortIndex);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateItemProcess().Save(OActionSqlParam, MenuTemplateItemId, OMenuTemplateItem);
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
        public static AjaxOut ServerSideSaveDisplayText(
            RenderInfoCls ORenderInfo,
            string MenuTemplateItemId,
            string DisplayText)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                if (string.IsNullOrEmpty(DisplayText)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa nhập nội dung"));
                MenuTemplateItemCls
                    OMenuTemplateItem = OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateItemProcess().CreateModel(OActionSqlParam, MenuTemplateItemId);
                OMenuTemplateItem.DisplayText = DisplayText;

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateMenuTemplateItemProcess().Save(OActionSqlParam, MenuTemplateItemId, OMenuTemplateItem);
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
