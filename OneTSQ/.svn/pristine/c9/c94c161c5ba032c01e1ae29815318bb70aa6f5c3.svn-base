using OnlineTour.Bussiness.Utility;
using OnlineTour.Model;
using OnlineTour.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineTour.WebParts
{
    public class EmailFolder : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "EmailFolder";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Thư mục email";
            }
        }

        public override string Description
        {
            get
            {
                return "Thư mục email";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(EmailFolder),Page);
        }

        //public override AjaxOut CheckPermission(SiteParam OSiteParam)
        //{
        //    string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).UserId;
        //    bool HasPermission = WebPermissionUtility.CheckPermission(OSiteParam, DictionaryPermission.StaticPermissionFunctionId, "Access", DictionaryPermission.StaticPermissionFunctionCode, DictionaryPermission.StaticPermissionFunctionId, UserId, false);
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    RetAjaxOut.RetBoolean = HasPermission;

        //    return RetAjaxOut;
        //}

        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                
                //AjaxOut RetAjaxOutCheckPermission = CheckPermission(OSiteParam);
                //if (RetAjaxOutCheckPermission.RetBoolean == false)
                //{
                //    RetAjaxOut.HtmlContent = WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Không có quyền truy cập tính năng này") + "<br>" + DictionaryPermission.StaticPermissionFunctionCode + ".Access", false);
                //    return RetAjaxOut;
                //}

                string Pop3EmailUserId = (string)WebEnvironments.Request("Pop3EmailUserId");
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string BackUrl = WebScreen.BuildUrl(OwnerCode, "userworkingmailbox", new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                });
                Pop3EmailUserCls
                    OPop3EmailUser = OnlineTourBussinessUtility.CreateBussinessProcess().CreatePop3EmailUserProcess().CreateModel(ActionSqlParam, Pop3EmailUserId);
                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +
                    "   function CallAddForm(ParentEmailFolderId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.EmailFolder.ServerSideDrawAddForm(RenderInfo, ParentEmailFolderId).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       document.getElementById('txtEmailFolderName').focus();\r\n" +
                    "   }\r\n" +


                    "   function CallUpdateForm(Pop3EmailUserId, EmailFolderId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.EmailFolder.ServerSideDrawUpdateForm(RenderInfo, Pop3EmailUserId, EmailFolderId).value;\r\n" +
                    "       document.getElementById('divActionForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divActionForm').style.display='block';\r\n" +
                    "       document.getElementById('divListForm').style.display='none';\r\n" +
                    "       document.getElementById('txtEmailFolderName').focus();\r\n" +
                    "   }\r\n" +

                    "   function CallBack()\r\n" +
                    "   {\r\n" +
                    "       document.getElementById('divActionForm').innerHTML='';\r\n" +
                    "       document.getElementById('divActionForm').style.display='none';\r\n" +
                    "       document.getElementById('divListForm').style.display='block';\r\n" +
                    "   }\r\n" +

                    "   function CallActionAdd(ParentEmailFolderId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       EmailFolderName = document.getElementById('txtEmailFolderName').value;\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +

                    "       AjaxOut = OnlineTour.WebParts.EmailFolder.ServerSideAdd(RenderInfo, '"+Pop3EmailUserId+"', EmailFolderName, ParentEmailFolderId, SortIndex).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n"+
                    "       {\r\n"+
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n"+
                    "       CallReading();\r\n"+
                    "       document.getElementById('txtEmailFolderName').value='';\r\n"+
                    "       document.getElementById('txtEmailFolderName').focus();\r\n" +
                    "       document.getElementById('txtSortIndex').value=parseInt(SortIndex,10)+1;\r\n"+
                    "   }\r\n" +


                    "   function CallActionUpdate(Pop3EmailUserId, EmailFolderId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       EmailFolderName = document.getElementById('txtEmailFolderName').value;\r\n" +
                    "       SortIndex = document.getElementById('txtSortIndex').value;\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.EmailFolder.ServerSideUpdate(RenderInfo, Pop3EmailUserId, EmailFolderId, EmailFolderName, SortIndex).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       CallReading();\r\n"+
                    "       CallBack();\r\n" +
                    "   }\r\n" +

                    "   function CallActionDelete(Pop3EmailUserId, EmailFolderId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       swal({ \r\n"+
                    "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                    "               text: \""+WebLanguage.GetLanguage(OSiteParam,"Đang thực hiện xóa loại báo cáo này ra khỏi hệ thống")+"!\", \r\n"+
                    "               type: \"warning\", \r\n"+
                    "               showCancelButton: true, \r\n"+
                    "               confirmButtonColor: \"#DD6B55\", \r\n"+
                    "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam,"Thực hiện xóa") + "\", \r\n" +
                    "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                    "               closeOnConfirm: false \r\n"+
                    "           }, function () { \r\n"+


                    "           AjaxOut = OnlineTour.WebParts.EmailFolder.ServerSideDelete(RenderInfo, Pop3EmailUserId, EmailFolderId).value;\r\n" +
                    "           if(AjaxOut.Error)\r\n" +
                    "           {\r\n" +
                    "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "               return;\r\n" +
                    "           }\r\n" +
                    "           CallReading();\r\n" +

                    "           swal(\""+WebLanguage.GetLanguage(OSiteParam,"Đã xóa")+"!\", \""+WebLanguage.GetLanguage(OSiteParam,"Loại báo cáo đã được xóa thành công!")+".\", \"success\"); \r\n"+
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
                    "       Pop3EmailUserId = document.getElementById('txtPop3EmailUserId').value;\r\n" +
                    "       OnlineTour.WebParts.EmailFolder.ServerSideDrawSearchResult(RenderInfo, Pop3EmailUserId, CallBackReading);\r\n" +
                    "   }\r\n" +

                    "   function CallBackReading(res)\r\n" +
                    "   {\r\n" +
                    "       AjaxOut = res.value;\r\n" +
                    "       document.getElementById('divProcessing').innerHTML='';\r\n" +
                    "       document.getElementById('divEmailFolderContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +

                    "</script>\r\n") +
                    WebEnvironments.EncryptHtml(
                        "<input type=hidden id=\"txtPop3EmailUserId\" value=\"" + Pop3EmailUserId + "\">\r\n" +
                        "<h3>"+OPop3EmailUser.Pop3Name+"</h3>\r\n"+
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +
                        "     <div class=\"ibox-title\"> \r\n" +
                        "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Thư mục email") + " </h5> \r\n" +
                        "     </div> \r\n" +
                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div class=\"row\"> \r\n" +
                        "             <div class=\"col-sm-12\"> \r\n" +
                        "                 <div class=\"input-group\">\r\n" +
                        "                   <span class=\"input-group-btn\"> \r\n" +
                        "                       <button type=\"button\" style=\"margin-right:5px\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:CallAddForm(null);\"> " + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "</button>\r\n" +
                        "                       <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:window.open('"+BackUrl+"','_self');\"> " + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</button>\r\n" +
                        "                   </span>\r\n" +
                        "                 </div> \r\n" +
                        "             </div> \r\n" +
                        "         </div> \r\n" +
                        "         <div id=\"divProcessing\" class=\"processing\"></div>\r\n"+
                        "               <div id=\"divEmailFolderContent\">" + ServerSideDrawSearchResult(ORenderInfo, Pop3EmailUserId).HtmlContent + "</div>\r\n" +
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
        public static AjaxOut ServerSideDrawSearchResult(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                EmailFolderCls[]
                 SimpleFolders = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().Reading(ActionSqlParam, Pop3EmailUserId, null);
            

                string Html = "";
                if (SimpleFolders.Length == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, "Không có thư mục", true);
                }
                else
                {
                    Html +=
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên thư mục") + " </th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    int LevelIndex = 5;
                    for (int iIndex = 0; iIndex < SimpleFolders.Length; iIndex++)
                    {
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td>" + SimpleFolders[iIndex].EmailFolderName + "</td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "\" href=\"javascript:CallUpdateForm('" + SimpleFolders[iIndex].frkPop3EmailUserId+"','"+ SimpleFolders[iIndex].EmailFolderId + "');\"><i class=\"" + WebScreen.GetEditGridIcon() + "\"></i></a></td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "\" href=\"javascript:CallActionDelete('" + SimpleFolders[iIndex].frkPop3EmailUserId+"','"+  SimpleFolders[iIndex].EmailFolderId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                            "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "\" href=\"javascript:CallAddForm('"+ SimpleFolders[iIndex].EmailFolderId + "');\"><i class=\"" + WebScreen.GetAddGridIcon() + "\"></i></a></td> \r\n" +
                            "                 </tr> \r\n"+
                        ServerSideDrawSubEmailFolder(ORenderInfo, Pop3EmailUserId, SimpleFolders[iIndex].EmailFolderId, LevelIndex).HtmlContent;
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
        public static AjaxOut ServerSideDrawSubEmailFolder(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string ParentEmailFolderId,
            int LevelIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                EmailFolderCls[]
                    SimpleFolders = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().Reading(ActionSqlParam, Pop3EmailUserId, ParentEmailFolderId);


                string Html = "";
                
                for (int iIndex = 0; iIndex < SimpleFolders.Length; iIndex++)
                {
                    string AppendText = "";
                    for (int iLevelIndex = 0; iLevelIndex < LevelIndex; iLevelIndex++)
                    {
                        AppendText = AppendText + "-";
                    }
                    AppendText = AppendText + " ";
                    Html +=
                        "                 <tr> \r\n" +
                        "                     <td>" + AppendText + SimpleFolders[iIndex].EmailFolderName + "</td> \r\n" +
                        "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Sửa") + "\" href=\"javascript:CallUpdateForm('" + SimpleFolders[iIndex].frkPop3EmailUserId+"','"+ SimpleFolders[iIndex].EmailFolderId + "');\"><i class=\"" + WebScreen.GetEditGridIcon() + "\"></i></a></td> \r\n" +
                        "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa") + "\" href=\"javascript:CallActionDelete('" + SimpleFolders[iIndex].frkPop3EmailUserId+"','"+ SimpleFolders[iIndex].EmailFolderId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                        "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Thêm") + "\" href=\"javascript:CallAddForm('" + SimpleFolders[iIndex].frkPop3EmailUserId+"','"+ SimpleFolders[iIndex].EmailFolderId + "');\"><i class=\"" + WebScreen.GetAddGridIcon() + "\"></i></a></td> \r\n" +
                        "                 </tr> \r\n";
                    LevelIndex = LevelIndex + 5;
                    Html += ServerSideDrawSubEmailFolder(ORenderInfo, Pop3EmailUserId, SimpleFolders[iIndex].EmailFolderId, LevelIndex).HtmlContent;
                    LevelIndex = LevelIndex - 5;
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
        public static AjaxOut ServerSideDrawAddForm(RenderInfoCls ORenderInfo,string ParentEmailFolderId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string Html =
                    " <div class=\"ibox-content\"> \r\n" +
                    "     <div class=\"row\"> \r\n" +
                    "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên thư mục") + "</label> <input id=\"txtEmailFolderName\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên thư mục") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input id=\"txtSortIndex\" value=\"1\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAdd('"+ParentEmailFolderId+"');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
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
            string Pop3EmailUserId,
            string EmailFolderName,
            string ParentEmailFolderId,
            string SortIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
          

                if (string.IsNullOrEmpty(EmailFolderName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên loại báo cáo không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex)==false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));

                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                
                EmailFolderCls 
                    OEmailFolder = new EmailFolderCls();
                OEmailFolder.EmailFolderId = System.Guid.NewGuid().ToString();
                OEmailFolder.EmailFolderName = EmailFolderName;
                OEmailFolder.frkPop3EmailUserId = Pop3EmailUserId;
                OEmailFolder.frkParentEmailFolderId = ParentEmailFolderId;
                OEmailFolder.SortIndex = int.Parse(SortIndex);
                OEmailFolder.frkFolderOwnerUserId = OwnerUserId;
                
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().Add(ActionSqlParam, OEmailFolder);
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
            string Pop3EmailUserId,
            string EmailFolderId, 
            string EmailFolderName,
            string SortIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
          
                if (string.IsNullOrEmpty(EmailFolderName)) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tên thư mục không hợp lệ"));
                if (FunctionUtilities.checkInteger(SortIndex) == false) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thứ tự nhập không hợp lệ"));


                EmailFolderCls
                    OEmailFolder = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().CreateModel(ActionSqlParam, Pop3EmailUserId, EmailFolderId);
                OEmailFolder.EmailFolderName = EmailFolderName;
                OEmailFolder.SortIndex = int.Parse(SortIndex);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().Save(ActionSqlParam, EmailFolderId, OEmailFolder);
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
            string Pop3EmailUserId,
            string EmailFolderId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().Delete(ActionSqlParam, Pop3EmailUserId, EmailFolderId);
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
            string Pop3EmailUserId, 
            string EmailFolderId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                EmailFolderCls
                    OEmailFolder = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().CreateModel(ActionSqlParam, Pop3EmailUserId, EmailFolderId);
                if (OEmailFolder == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Thư mục đã bị xóa hoặc không tìm thấy"));
                }
               
                string Html =
                       " <div class=\"ibox-content\"> \r\n" +
                       "     <div class=\"row\"> \r\n" +
                       "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Sửa chữa") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên thư mục") + "</label> <input id=\"txtEmailFolderName\" value=\"" + OEmailFolder.EmailFolderName + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tên thư mục") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "</label> <input id=\"txtSortIndex\" value=\"" + OEmailFolder.SortIndex+ "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thứ tự sắp xếp") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + Pop3EmailUserId+"','"+EmailFolderId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
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
    }
}
