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
    public class ViewEmail : WebPartTemplate
    {
        public static string StaticWebPartId
        {
            get
            {
                return "ViewEmail".ToLower();
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
                return "Xem email";
            }
        }

        public override string Description
        {
            get
            {
                return "Xem email";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ViewEmail), Page);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ComposeEmail), Page);
        }

        public override AjaxOut CheckPermission(SiteParam OSiteParam)
        {
            string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true;// WebPermissionUtility.CheckPermission(OSiteParam, EmailPermission.StaticPermissionFunctionId, "Access", EmailPermission.StaticPermissionFunctionCode, EmailPermission.StaticPermissionFunctionId, UserId, false);
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }

        public static string GetPluginJavascript(SiteParam OSiteParam)
        {
            return
                "<script language=javascript>\r\n" +
                "   function CallActionDeleteReferenceObject(ReferenceObjectId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +

                "       swal({ \r\n" +
                "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa email ra khỏi hồ sơ") + "!\", \r\n" +
                "               type: \"warning\", \r\n" +
                "               showCancelButton: true, \r\n" +
                "               confirmButtonColor: \"#DD6B55\", \r\n" +
                "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                "               closeOnConfirm: false \r\n" +
                "           }, function () { \r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideDeleteReferenceObject(RenderInfo, ReferenceObjectId).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                "          callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "          return;\r\n" +
                "       }\r\n" +
                "       document.getElementById('tr'+ReferenceObjectId).style.display='none';\r\n" +
                "       });\r\n"+
                "   }\r\n" +

                "   function ClientDeleteEmail(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideDeleteEmail(RenderInfo, Pop3EmailUserId, EmailId).value;\r\n" +
                "        if(AjaxOut.Error)\r\n" +
                "        {\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "        }\r\n" +
                "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                "   }\r\n" +

                "   function ClientMoveToTrash(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideMoveToTrash(RenderInfo, Pop3EmailUserId, EmailId).value;\r\n" +
                "        if(AjaxOut.Error)\r\n" +
                "        {\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "        }\r\n" +
                "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                "   }\r\n" +

                "   function ClientRecoverFromTrash(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideRecovertFromTrash(RenderInfo, Pop3EmailUserId, EmailId).value;\r\n" +
                "        if(AjaxOut.Error)\r\n" +
                "        {\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "        }\r\n" +
                "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                "   }\r\n" +

                "   function ShowReplyEmail()\r\n"+
                "   {\r\n"+
                "       if(document.getElementById('divListEmailRelationContent').style.display=='block')\r\n"+
                "       {\r\n"+
                "           document.getElementById('divListEmailRelationContent').style.display='none';\r\n"+
                "       }\r\n"+
                "       else\r\n"+
                "       {\r\n"+
                "           document.getElementById('divListEmailRelationContent').style.display='block';\r\n" +
                "       }\r\n"+
                "   }\r\n"+

                "   function resizeIframe(obj)\r\n" +
                "   {\r\n" +
                "       obj.style.height = obj.contentWindow.document.body.scrollHeight + 'px';\r\n" +
                "   }\r\n" +

                "   function DownloadViewEmailAttachedFile(Pop3EmailUserId, EmailAttachedFileId)\r\n" +
                "   {\r\n" +
                "        RenderInfo=CreateRenderInfo();\r\n" +
                "        setTimeout('RealDownloadViewEmailAttachedFile(\"'+Pop3EmailUserId+'\", \"'+EmailAttachedFileId+'\")',10);\r\n" +
                "   }\r\n" +

                "   function RealDownloadViewEmailAttachedFile(Pop3EmailUserId, EmailAttachedFileId)\r\n" +
                "   {\r\n" +
                "        RenderInfo=CreateRenderInfo();\r\n" +
                "        AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideDownloadViewAttachFile(RenderInfo, Pop3EmailUserId, EmailAttachedFileId).value;\r\n" +
                "        if(AjaxOut.Error)\r\n" +
                "        {\r\n" +
                "           alert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "        }\r\n" +
                "        window.open(AjaxOut.RetUrl,'_blank');\r\n" +
                "   }\r\n" +

                "   function ReplyComposeEmail(Pop3EmailUserId, EmailId, ReplyType)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideComposeEmailUrl(RenderInfo, Pop3EmailUserId, EmailId, ReplyType).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                "          alert(AjaxOut.InfoMessage);\r\n" +
                "          return;\r\n" +
                "       }\r\n" +
                "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                "   }\r\n" +


                "   function ClientCloseEmail(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       NotifyId = document.getElementById('txtNotifyId').value;\r\n"+
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideCloseEmail(RenderInfo, Pop3EmailUserId, EmailId, NotifyId).value;\r\n" +
                "        if(AjaxOut.Error)\r\n" +
                "        {\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "        }\r\n" +
                "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                "   }\r\n" +

                "   function ClientOpenEmail(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       NotifyId = document.getElementById('txtNotifyId').value;\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideOpenEmail(RenderInfo, Pop3EmailUserId, EmailId, NotifyId).value;\r\n" +
                "        if(AjaxOut.Error)\r\n" +
                "        {\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "        }\r\n" +
                "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                "   }\r\n" +

                "   function CallDeleteEmailFollowUp(EmailFollowUpId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideDeleteEmailFollowUp(RenderInfo, EmailFollowUpId).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       document.getElementById('tr'+EmailFollowUpId).style.display='none';\r\n" +
                "   }\r\n" +

                //"   function ClientAssignToProject(Pop3EmailUserId, EmailId)\r\n" +
                //"   {\r\n" +
                //"       RenderInfo=CreateRenderInfo();\r\n" +
                //"       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideDrawAssignToProjectForm(RenderInfo, Pop3EmailUserId, EmailId).value;\r\n" +


                //"       document.getElementById('divAssignToProjectForm').style.display='block';\r\n" +
                //"       document.getElementById('divAssignToProjectForm').innerHTML=AjaxOut.HtmlContent;\r\n" +
                //"       document.getElementById('divViewEmailContent').style.display='none';\r\n" +

                //"       $('#drpSelectProject').select2();\r\n" +
                //"       $('.CssDate').datepicker({\r\n" +
                //"           format: 'dd/mm/yyyy'\r\n" +
                //"       });\r\n" +
                //"   }\r\n" +

                //"   function BackFromAssignToProject()\r\n" +
                //"   {\r\n" +
                //"       RenderInfo=CreateRenderInfo();\r\n" +
                //"       document.getElementById('divAssignToProjectForm').style.display='none';\r\n" +
                //"       document.getElementById('divAssignToProjectForm').innerHTML='';\r\n" +
                //"       document.getElementById('divViewEmailContent').style.display='block';\r\n" +
                //"   }\r\n" +

                //"   function CallAssignEmailToProject(Pop3EmailUserId, EmailId)\r\n" +
                //"   {\r\n" +
                //"       RenderInfo=CreateRenderInfo();\r\n" +
                //"       ProjectId = document.getElementById('drpSelectProject').value;\r\n" +
                //"       ProjectMileStoneId = document.getElementById('drpSelectProjectMileStone').value;\r\n" +
                //"       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideAddEmailToProject(RenderInfo, Pop3EmailUserId, EmailId, ProjectId, ProjectMileStoneId).value;\r\n" +
                //"       if(AjaxOut.Error)\r\n" +
                //"       {\r\n" +
                //"           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                //"           return;\r\n" +
                //"       }\r\n" +
                //"       BackFromAssignToProject();\r\n" +
                //"   }\r\n" +

                "   function ClientMoveToEmailFolder(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideDrawMoveToEmailFolderForm(RenderInfo, Pop3EmailUserId, EmailId).value;\r\n" +


                "       document.getElementById('divComposeEmail').style.display='block';\r\n" +
                "       document.getElementById('divComposeEmail').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "       document.getElementById('divListEmailContent').style.display='none';\r\n" +
                "       document.getElementById('divViewEmailContent').style.display='none';\r\n" +

                //"       $('#drpSelectProject').select2();\r\n" +
                //"       $('.CssDate').datepicker({\r\n" +
                //"           format: 'dd/mm/yyyy'\r\n" +
                //"       });\r\n" +
                "   }\r\n" +

                "   function CallMoveToEmailFolder(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       MoveToFolderId = document.getElementById('drpSelectEmailFolder').value;\r\n" +
                //"       viewType= document.getElementById('txtViewType').value;\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideMoveEmailFolder(RenderInfo, Pop3EmailUserId, EmailId, MoveToFolderId).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                "   }\r\n" +

                "   function ClientBackFromMoveFolder()\r\n"+
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       document.getElementById('divComposeEmail').style.display='none';\r\n" +
                "       document.getElementById('divComposeEmail').innerHTML='';\r\n" +
                "       document.getElementById('divListEmailContent').style.display='none';\r\n" +
                "       document.getElementById('divViewEmailContent').style.display='block';\r\n" +
                "   }\r\n" +

                "   function DetectProjectMileStone(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       ProjectId = document.getElementById('drpSelectProject').value;\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideDrawProjectMileStoneForm(RenderInfo, ProjectId, Pop3EmailUserId, EmailId).value;\r\n" +
                "       document.getElementById('divSelectProjectMileStone').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "   }\r\n" +

                "   function AddFollowUpForm(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideDrawFollowUpForm(RenderInfo, Pop3EmailUserId,  EmailId).value;\r\n" +
                "       document.getElementById('divComposeEmail').style.display='block';\r\n" +
                "       document.getElementById('divComposeEmail').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "       document.getElementById('divListEmailContent').style.display='none';\r\n" +
                "       document.getElementById('divViewEmailContent').style.display='none';\r\n" +
                "   }\r\n" +


                "   function CallActionAddFollowUp(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       Subject = document.getElementById('txtFollowUpSubject').value;\r\n" +
                "       FollowUpNote = document.getElementById('txtFollowUpNote').value;\r\n" +

                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideAddFollowUp(RenderInfo, Pop3EmailUserId, EmailId, Subject, FollowUpNote).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
            //"           document.getElementById('divProcessingAddFollowUp').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       document.getElementById('txtFollowUpSubject').value='';\r\n" +
                "       document.getElementById('txtFollowUpNote').value='';\r\n" +
                "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "       document.getElementById('divEmailFollowUpContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "   }\r\n" +


                "   function CallActionUpdateFollowUp( EmailFollowUpId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       Subject = document.getElementById('txtFollowUpSubject').value;\r\n" +
                "       FollowUpNote = document.getElementById('txtFollowUpNote').value;\r\n" +

                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideUpdateFollowUp(RenderInfo, EmailId, Subject, FollowUpNote).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
            //"           document.getElementById('divProcessingAddFollowUp').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       document.getElementById('txtFollowUpSubject').value='';\r\n" +
                "       document.getElementById('txtFollowUpNote').value='';\r\n" +
                "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "       document.getElementById('divEmailFollowUpContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "   }\r\n" +

                "   function CallActionSaveFollowUp(EmailFollowUpId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       Subject = document.getElementById('txtFollowUpSubject').value;\r\n" +
                "       FollowUpNote = document.getElementById('txtFollowUpNote').value;\r\n" +

                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideSaveFollowUp(RenderInfo, EmailFollowUpId, Subject, FollowUpNote).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       document.getElementById('divEmailFollowUpContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "       CallBackFromFollowUp();\r\n" +
                "   }\r\n" +

                "   function CallBackFromFollowUp()\r\n" +
                "   {\r\n" +
                "       document.getElementById('divComposeEmail').style.display='none';\r\n" +
                "       if(document.getElementById('divViewEmailContent').innerHTML=='')\r\n" +
                "       {\r\n" +
                "           document.getElementById('divListEmailContent').style.display='block';\r\n" +
                "           document.getElementById('divViewEmailContent').style.display='none';\r\n" +
                "       }\r\n" +
                "       else\r\n" +
                "       {\r\n" +
                "           document.getElementById('divListEmailContent').style.display='none';\r\n" +
                "           document.getElementById('divViewEmailContent').style.display='block';\r\n" +
                "       }\r\n" +
                "   }\r\n" +




                "   function AddToCaseProfile(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideDrawAddToCaseProfileForm(RenderInfo, Pop3EmailUserId,  EmailId).value;\r\n" +
                "       document.getElementById('divComposeEmail').style.display='block';\r\n" +
                "       document.getElementById('divComposeEmail').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "       document.getElementById('divListEmailContent').style.display='none';\r\n" +
                "       document.getElementById('divViewEmailContent').style.display='none';\r\n" +
                "       $('#drpSelectCaseProfile').select2();\r\n"+
                "   }\r\n" +


                "   function CallActionAddToCaseProfile(Pop3EmailUserId, EmailId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       CaseProfileId = document.getElementById('drpSelectCaseProfile').value;\r\n" +
                "       RefSubject = document.getElementById('txtRefSubject').value;\r\n" +
                "       RefNote = document.getElementById('txtRefNote').value;\r\n" +
                "       AjaxOut = OnlineTour.WebParts.ViewEmail.ServerSideAddCaseProfile(RenderInfo, Pop3EmailUserId, EmailId, CaseProfileId, RefSubject, RefNote).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                //"           document.getElementById('divProcessingAddFollowUp').innerHTML=AjaxOut.HtmlContent;\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "       CallBack();\r\n"+
                "   }\r\n" +

                "   function CallBack()\r\n" +
                "   {\r\n" +
                "       document.getElementById('divComposeEmail').style.display='none';\r\n" +
                "       if(document.getElementById('divViewEmailContent').innerHTML=='')\r\n" +
                "       {\r\n" +
                "           document.getElementById('divListEmailContent').style.display='block';\r\n" +
                "           document.getElementById('divViewEmailContent').style.display='none';\r\n" +
                "       }\r\n" +
                "       else\r\n" +
                "       {\r\n" +
                "           document.getElementById('divListEmailContent').style.display='none';\r\n" +
                "           document.getElementById('divViewEmailContent').style.display='block';\r\n" +
                "       }\r\n" +
                "   }\r\n" +
                "</script>\r\n" +
                ComposeEmail.GetPluginJavascript(OSiteParam);
        }

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
                string EmailId = (string)WebEnvironments.Request("EmailId");
                string NotifyId = (string)WebEnvironments.Request("NotifyId");
                if (string.IsNullOrEmpty(EmailId))
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tham số email không tìm thấy"));
                }

                string SessionId = System.Guid.NewGuid().ToString();
                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;

                //if (!string.IsNullOrEmpty(NotifyId))
                //{
                //    OnlineTourBussinessUtility.CreateBussinessProcess().CreateNotifyProcess(OSiteParam).SetView(null, OSiteParam, NotifyId, UserId);
                //}


                RetAjaxOut.HtmlContent =
                    GetPluginJavascript(OSiteParam)+
                    
                    "<div id=\"divComposeEmail\"></div>\r\n" +
                    "<div id=\"divViewEmail\"></div>\r\n" +
                    "<div id=\"divAssignToProjectForm\"></div>\r\n" +
                    "<div id=\"divSearchContent\"></div>\r\n" +
                    "<div id=\"divViewEmailContent\">\r\n" +
                        ServerSideViewEmail(ORenderInfo, Pop3EmailUserId, EmailId, NotifyId).HtmlContent +
                    "</div>\r\n";

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
        public static AjaxOut ServerSideViewEmail(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId,
            string NotifyId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                EmailCls
                    OEmail = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CreateModelSetView(ActionSqlParam, Pop3EmailUserId, EmailId, OwnerUserId);
                
                bool AllowCompose = false;
                Pop3EmailUserCls
                    OPop3EmailUser = OnlineTourBussinessUtility.CreateBussinessProcess().CreatePop3EmailUserProcess().CreateModel(ActionSqlParam, Pop3EmailUserId);
                if (OPop3EmailUser.frkOwnerUserId.Equals(WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId))
                {
                    AllowCompose = true;
                }

                string Html =
                    "<input id=\"txtNotifyId\" value=\"" + NotifyId + "\" type=\"hidden\">\r\n" +
                    "<div class=\"row\">\r\n"+
                    "<div class=\"col-md-10\">\r\n"+
                    "<div class=\"mail-box-header\"> \r\n";

                if (AllowCompose)
                {
                    Html +=
                        "    <div class=\"tooltip-demo\"> \r\n" +
                        "        <a href=\"javascript:ReplyComposeEmail('" + OEmail.frkPop3EmailUserId + "','" + EmailId + "',0);\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Reply\"><i class=\"fa fa-reply\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Reply") + "</a> \r\n" +
                        "        <a href=\"javascript:ReplyComposeEmail('" + OEmail.frkPop3EmailUserId + "','" + EmailId + "',1);\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Reply All\"><i class=\"fa fa-reply\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Reply all") + "</a> \r\n" +
                        "        <a href=\"javascript:AddFollowUpForm('" + OEmail.frkPop3EmailUserId + "','" + EmailId + "');\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Thêm followup\"><i class=\"fa fa-newspaper-o\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Thêm followUp") + "</a> \r\n";

                    if (OEmail.Closed == 0)
                    {
                        Html +=
                            "        <a href=\"javascript:ClientCloseEmail('" + Pop3EmailUserId + "','" + OEmail.EmailId + "','" + NotifyId + "');\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Đóng kết thúc\"><i class=\"fa fa-folder\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Đóng kết thúc") + "</a>\r\n";
                    }
                    else
                    {
                        Html +=
                            "        <a href=\"javascript:ClientOpenEmail('" + Pop3EmailUserId + "','" + OEmail.EmailId + "','" + NotifyId + "');\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Mở theo dõi lại\"><i class=\"fa fa-folder-open-o\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Mở theo dõi") + "</a>\r\n";
                    }

                    Html +=
                        //"        <a href=\"javascript:ClientAssignToProject('" + Pop3EmailUserId + "','" + OEmail.EmailId + "');\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Đưa vào dự án\"><i class=\"fa fa-laptop\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Đưa vào dự án") + "</a>\r\n" +
                        "        <a href=\"javascript:ClientMoveToEmailFolder('" + Pop3EmailUserId + "','" + EmailId + "');\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Chuyển thư mục\"> " + WebLanguage.GetLanguage(OSiteParam, "Chuyển thư mục") + "</a> \r\n" +
                        "        <a href=\"#.\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Print email\"><i class=\"fa fa-print\"></i> </a> \r\n";

                    if (OEmail.Trash == 0)
                    {
                        Html +=
                            "        <a href=\"javascript:ClientMoveToTrash('" + Pop3EmailUserId + "','" + EmailId + "');\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Move to trash\"><i class=\"fa fa-trash-o\"></i> </a> \r\n";
                    }
                    else
                    {
                        Html +=
                            "        <a href=\"javascript:ClientDeleteEmail('" + Pop3EmailUserId + "','" + EmailId + "');\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Xóa hẳn\"><i class=\"fa fa-trash-o\"></i> </a> \r\n" +
                            "        <a href=\"javascript:ClientRecoverFromTrash('" + Pop3EmailUserId + "','" + EmailId + "');\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Khôi phục lại thư từ thùng rác\"><i class=\"fa fa-undo\"></i> </a> \r\n";
                    }

                    if (!string.IsNullOrEmpty(NotifyId))
                    {
                        Html +=
                            "        <a href=\"javascript:CloseNotify('" + NotifyId + "');\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"" + WebLanguage.GetLanguage(OSiteParam, "Đóng thông báo") + "\"><i class=\"fa fa-bell-slash-o\"></i> </a> \r\n";
                    }
                    Html +=
                        "        <a href=\"javascript:ClientBackFromAjaxViewEmail();\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "\"><i class=\"fa fa-backward\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + " </a> \r\n" +
                        "    </div> \r\n";
                }
                else
                {
                    Html+=
                        " <div class=\"tooltip-demo\">\r\n" +
                        "        <a href=\"javascript:ClientBackFromAjaxViewEmail();\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "\"><i class=\"fa fa-backward\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + " </a> \r\n" +
                        " </div> \r\n";
                }
                Html+=
                    "    <h2 style=\"margin-top:12px\"> \r\n" +
                    "        " + WebLanguage.GetLanguage(OSiteParam, "Xem nội dung thư") + " \r\n" +
                    "    </h2> \r\n" +
                    "    <div class=\"mail-tools tooltip-demo m-t-md\"> \r\n" +
                    "        <h3> \r\n" +
                                 OEmail.SubjectInfo +
                    "        </h3> \r\n" +
                    "        <h4> \r\n" +
                    "             To: " + OEmail.ToAddresInfo +
                    "        </h4> \r\n";
                if (!string.IsNullOrEmpty(OEmail.CcAddresInfo))
                {
                    Html +=
                        "        <h4> \r\n" +
                        "             Cc: " + OEmail.CcAddresInfo +
                        "        </h4> \r\n";
                }
                if (!string.IsNullOrEmpty(OEmail.BccAddresInfo))
                {
                    Html +=
                        "        <h4> \r\n" +
                        "             Bcc: " + OEmail.BccAddresInfo +
                        "        </h4> \r\n";
                }


                if (OEmail.ActiveSentEmail == 1)
                {
                    if (OEmail.HasSentEmail == 0)
                    {
                        Html +=
                            "<h5 style=\"color:maroon\"> \r\n" +
                            WebLanguage.GetLanguage(OSiteParam, "Thư đang xử lý gửi đi...") +
                            " </h5> \r\n";
                    }
                    else
                    {
                        string AppendTime = "";
                        if (OEmail.SentEmailDate.Year > 1)
                        {
                            AppendTime = " - " + WebLanguage.GetLanguage(OSiteParam, "lúc") + ": " + OEmail.SentEmailDate.ToString("dd/MM/yyyy HH:mm");
                        }
                        Html +=
                           "<h5 style=\"color:maroon\"> \r\n" +
                                WebLanguage.GetLanguage(OSiteParam, "Thư đã gửi thành công") + AppendTime +
                           "</h5> \r\n";
                    }
                }

                string AppendProjectInfo = "";
                //if (!string.IsNullOrEmpty(OEmail.frkProjectId))
                //{
                //    string AppendComplete = "";
                //    string AppendMileStone = "";
                //    ProjectCls
                //        OProject=OnlineTourBussinessUtility.CreateBussinessProcess().CreateProjectProcess(OSiteParam).CreateModel(null,OSiteParam,OEmail.frkProjectId);

                //    if (OProject != null)
                //    {
                //        AppendComplete = " -> "+WebLanguage.GetLanguage(OSiteParam,"Hoàn thành")+": "+OProject.ProjectPercentComplete + "%";
                //        ProjectMileStoneCls
                //            OProjectMileStone = OnlineTourBussinessUtility.CreateBussinessProcess().CreateProjectProcess(OSiteParam).CreateModelProjectMileStone(null, OSiteParam, OEmail.frkProjectMileStoneId);

                //        if (OProjectMileStone != null)
                //        {
                //            AppendMileStone += " - " + OProjectMileStone.ProjectMileStoneName;
                //        }

                //        AppendProjectInfo = "<div style=\"margin-top:10px;color:green\">" + WebLanguage.GetLanguage(OSiteParam, "Dự án") + ": " + OProject.ProjectName + AppendMileStone + AppendComplete + "</div>\r\n";
                //    }
                //}
                Html +=
                    "        <h5> \r\n" +
                    "            <span class=\"pull-right font-noraml\">" + OEmail.DateInfo + "</span> \r\n" +
                    "            <span class=\"font-noraml\">" + WebLanguage.GetLanguage(OSiteParam, "Người gửi") + ": </span>"+
                    "            <span>"+ OEmail.SentFromInfo + "</span> \r\n" +
                                 AppendProjectInfo+
                    "        </h5> \r\n";

                if (OEmail.ExpiredDate.Year > 1)
                {
                    Html += "<h5>" + WebLanguage.GetLanguage(OSiteParam, "Hạn xử lý") + ": " + OEmail.ExpiredDateInfo + "</h5>\r\n";
                }
                Html +=

                    "    </div> \r\n";

                EmailRelationCls[] 
                    EmailRelations = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().ReadingEmailRelations(ActionSqlParam, EmailId);
                if (EmailRelations.Length>0)
                {
                    
                    Html +=
                        "   <div class=\"font-noraml\" style=\"font-size:16px\">" + WebLanguage.GetLanguage(OSiteParam, "Số lần xử lý") + ": <a title=\"Tắt bật danh sách email đã xử lý\" href=\"javascript:ShowReplyEmail();\">" + EmailRelations.Length + "</a></div> \r\n" +
                        "   <div style=\"display:none\" id=\"divListEmailRelationContent\">\r\n"+
                        "   <ul>\r\n";
                    for (int iIndex = 0; iIndex < EmailRelations.Length; iIndex++)
                    {
                        string ViewEmailRelationUrl = WebScreen.BuildUrl(OwnerCode, ViewEmail.StaticWebPartId,
                        new WebParamCls[]
                        {
                            new WebParamCls("Pop3EmailUserId",EmailRelations[iIndex].frkRelPop3EmailUserId),
                            new WebParamCls("EmailId",EmailRelations[iIndex].frkRelEmailId),
                        });

                        Html += " <li style=\"padding:4px;list-style-type:square\"><a href=\"" + ViewEmailRelationUrl + "\" target=\"_self\">" + EmailRelations[iIndex].FromEmailAddress + "(" + EmailRelations[iIndex].FromName + "): " + EmailRelations[iIndex].Subject + "</a> =>  " + EmailRelations[iIndex].EmailDate.ToString("dd/MM/yyyy HH:mm") + "</li>";
                    }
                    Html +=
                        "   </ul>\r\n" +
                        "</div>\r\n";
                }
                Html +=
                   "</div> \r\n" +
                   "    <div id=\"divViewRelationEmailContent\"></div>\r\n";

                if (!string.IsNullOrEmpty(OEmail.SrcEmailId))
                {
                    string Url = WebScreen.BuildUrl(OwnerCode, new ViewEmail().WebPartId,
                        new WebParamCls[]{
                        new WebParamCls("EmailId",OEmail.SrcEmailId)
                    });
                    Html +=
                        "    <h4 style=\"margin-top:12px\"><a href=\"" + Url + "\">" + WebLanguage.GetLanguage(OSiteParam, "Xem nội dung email chính") + "</a></h4>\r\n";
                }

                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string SaveFile = System.Guid.NewGuid().ToString() + ".html";
                string UrlFrame = OSiteParam.HttpTempPathRoot + "/" + LoginName + "/email" + "/" + SaveFile;
                UrlFrame = WebConfig.ConvertHttpRoot(OSiteParam, UrlFrame);
                string SaveTempDir = OSiteParam.TempPathRoot + "\\" + LoginName + "\\email";
                SaveTempDir = WebConfig.ConvertPathRoot(OSiteParam, SaveTempDir);
                System.IO.Directory.CreateDirectory(SaveTempDir);
                SaveFile = SaveTempDir + "\\" + SaveFile;
                System.IO.File.WriteAllText(SaveFile, OEmail.Body, Encoding.Unicode);

                Html +=
                    "    <div class=\"mail-box\"> \r\n" +


                    "    <div class=\"mail-body\"> \r\n" +
                    "            <iframe src=\"" + UrlFrame + "\" width=100% frameborder=\"0\" scrolling=\"no\" onload=\"resizeIframe(this)\"></iframe>\r\n" +
                    "    </div> \r\n";

                if (OEmail.TotalAttached > 0)
                {
                    EmailAttachedFileCls[]
                        EmailAttachedFiles = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().ReadingEmailAttachedFiles(ActionSqlParam, Pop3EmailUserId, EmailId);


                    Html +=
                        " <div style=\"font-size:15px;font-weight:bold;padding-left:20px\">" + WebLanguage.GetLanguage(OSiteParam, "File gắn kèm") + "</div>\r\n" +
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < EmailAttachedFiles.Length; iIndex++)
                    {
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td style=\"width:20px\" class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td style=\"width:20px\">\r\n" +
                            "                           <div class=\"icon\"> \r\n" +
                            "                                <i class=\"fa fa-file\"></i> \r\n" +
                            "                            </div> \r\n" +
                            "                     </td>\r\n" +
                            "                     <td><a href=\"javascript:DownloadViewEmailAttachedFile('" + Pop3EmailUserId + "','" + EmailAttachedFiles[iIndex].EmailAttachedFileId + "');\">" + EmailAttachedFiles[iIndex].FileName + "</a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n";
                }


                Html += "</div>\r\n" +
                    "</div>\r\n" +
                    " <div class=\"col-md-2\">\r\n" +

                    "            <div class=\"row\">\r\n" +
                    "               <ul class=\"nav\">\r\n" +
                    "                   <li class=\"\">\r\n" +
                    "                       <a style=\"padding:5px;color:maroon;font-weight:bold\" data-group=\"invoices\" href=\"javascript:AddToCaseProfile('" + Pop3EmailUserId+"','"+ EmailId + "');\"><i class=\"fa fa-file-text menu-icon\" aria-hidden=\"true\"></i>Đưa vào hồ sơ</a>\r\n" +
                    "                   </li>\r\n" +

                    "                   <li class=\"\">\r\n" +
                    "                       <a style=\"padding:5px;color:maroon;font-weight:bold\" data-group=\"invoices\" href=\"javascript:AddToProject('" + EmailId + "');\"><i class=\"fa fa-file-text menu-icon\" aria-hidden=\"true\"></i>Đưa vào dự án</a>\r\n" +
                    "                   </li>\r\n" +
                    "                 </ul>\r\n" +
                    "            </div>\r\n" + 


                      "</div>\r\n" +
                      "</div>\r\n";
               Html +=
                    "   <div class=\"mail-body\" id=\"divEmailFollowUpContent\">" + ServerSideDrawEmailFollowUps(ORenderInfo,Pop3EmailUserId, EmailId).HtmlContent + "</div>\r\n";

                //Html=HttpUtility.HtmlDecode(Html);
                RetAjaxOut.HtmlContent = Html;
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
        public static AjaxOut ServerSideDownloadViewAttachFile(RenderInfoCls RenderInfo, string Pop3EmailUserId, string EmailAttachedFileId)
        {
            AjaxOut OAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(RenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                OAjaxOut.RetUrl = "ProcessDownload.aspx?ServiceId=" + new ProcessDownloadAttachedEmailService().ServiceId + "&ObjectId=" + Pop3EmailUserId+"."+EmailAttachedFileId;
            }
            catch (Exception ex)
            {
                OAjaxOut.Error = true;
                OAjaxOut.InfoMessage = ex.Message.ToString();
                OAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return OAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideCloseNotify(RenderInfoCls RenderInfo, string NotifyId)
        {
            AjaxOut OAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(RenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                //NotifyCls
                //    ONotify = OnlineTourBussinessUtility.CreateBussinessProcess().CreateNotifyProcess(OSiteParam).CreateModel(null, OSiteParam, NotifyId);
                //if (ONotify == null)
                //{
                //    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Không tìm thấy thông báo"));
                //}
                //string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                //OnlineTourBussinessUtility.CreateBussinessProcess().CreateNotifyProcess(OSiteParam).CloseNotify(null, OSiteParam, NotifyId, UserId);
                OAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đóng thông báo xử lý thành công");
            }
            catch (Exception ex)
            {
                OAjaxOut.Error = true;
                OAjaxOut.InfoMessage = ex.Message.ToString();
                OAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return OAjaxOut;
        }


        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut ServerSideDrawAssignForm(
        //    RenderInfoCls ORenderInfo,
        //    string EmailId,
        //    string NotifyId)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam
        //            OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSessionUtility.CheckSessionTimeOut(OSiteParam);

        //        OwnerUserFilterCls
        //            OUserFilter = new UserFilterCls();

        //        UserCls[]
        //            Users = OnlineTourBussinessUtility.CreateBussinessProcess().CreateUserProcess(OSiteParam).Reading(null, OSiteParam, OUserFilter);

        //        string SelectUserText =
        //         "<select id=\"drpSelectSale\" class=\"form-control select2\">\r\n";
        //        for (int iIndex = 0; iIndex < Users.Length; iIndex++)
        //        {
        //            SelectUserText += "   <option value=\"" + Users[iIndex].UserId + "\">" + Users[iIndex].FullName + " - " + Users[iIndex].LoginName + "</option>\r\n";
        //        }
        //        SelectUserText +=

        //         "</select>\r\n";

        //        string Html =
        //               " <div class=\"ibox-content\"> \r\n" +
        //               "     <div class=\"row\"> \r\n" +
        //               "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Phân công xử lý email") + "</h3> \r\n" +
        //               "             <div> \r\n" +
        //               "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Người xử lý") + "</label> " + SelectUserText + "</div> \r\n" +
        //               "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Hạn xử lý") + "</label><input type=\"text\" class=\"form-control CssDate\" id=\"txtExpiredDate\" value=\"" + System.DateTime.Now.AddDays(3).ToString("dd/MM/yyyy") + "\"></div> \r\n" +
        //               "                 <div> \r\n" +
        //               "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallAssignEmailToUserAction('" + EmailId + "','" + NotifyId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "</strong></button> \r\n" +
        //               "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBackFromAssignEmail();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
        //               "                 </div> \r\n" +
        //               "             </div> \r\n" +
        //               "         </div> \r\n" +
        //               "     </div> \r\n" +
        //               " </div> \r\n";

        //        Html = WebEnvironments.EncryptHtml(Html);
        //        RetAjaxOut.HtmlContent = Html;
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}

        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut ServerSideDrawMoveEmailForm(
        //    RenderInfoCls ORenderInfo,
        //    string EmailId,
        //    string NotifyId)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam
        //            OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSessionUtility.CheckSessionTimeOut(OSiteParam);


        //        string Html =
        //            " <div><a href=\"javascript:ClientBackFromAjaxViewEmail();\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Reply\"> " + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</a></div>\r\n" +
        //            " <h3>" + WebLanguage.GetLanguage(OSiteParam, "Chuyển email vào yêu cầu khác") + "</h3>\r\n" +
        //            " <div>" + WebLanguage.GetLanguage(OSiteParam, "Từ khóa") + "</div>\r\n" +
        //            " <div style=\"margin-top:4px;margin-bottom:4px\"><input onkeypress=\"if(event.keyCode==13)SearchEmail('" + EmailId + "','" + NotifyId + "');\" id=\"txtSearchKeyword\" class=\"form-control\"></div>\r\n" +
        //            " <div id=\"divSearchEmailResult\">\r\n" +
        //                ServerSideSearchEmailForMove(ORenderInfo, EmailId, NotifyId, "").HtmlContent +
        //            " </div>\r\n";

        //        Html = WebEnvironments.EncryptHtml(Html);
        //        RetAjaxOut.HtmlContent = Html;
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}

        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut ServerSideSearchEmailForMove(
        //    RenderInfoCls ORenderInfo,
        //    string EmailId,
        //    string NotifyId,
        //    string Keyword)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam
        //            OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSessionUtility.CheckSessionTimeOut(OSiteParam);

        //        EmailFilterCls
        //            OEmailFilter = new EmailFilterCls();
        //        OEmailFilter.HasClosed = 0;
        //        OEmailFilter.Keyword = Keyword;
        //        OEmailFilter.Task = 0;
        //        OEmailFilter.IsRequest = 1;

        //        EmailCls[]
        //            Emails = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess(OSiteParam).Reading(null, OSiteParam, OEmailFilter);

        //        string Html =
        //               "   <div class=\"search-result-info\">" + WebLanguage.GetLanguage(OSiteParam, "Tìm thấy") + " (" + Emails.Length.ToString("#,##0") + ") " + WebLanguage.GetLanguage(OSiteParam, "dữ liệu theo điều kiện lọc") + "</div>\r\n" +
        //               "         <div class=\"table-responsive\"> \r\n" +
        //               "             <table class=\"table table-striped\"> \r\n" +
        //               "                 <thead> \r\n" +
        //               "                 <tr> \r\n" +
        //               "                     <th class=\"th-func-20\"></th> \r\n" +
        //               "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Gửi từ") + " </th> \r\n" +
        //               "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề") + " </th> \r\n" +
        //               "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian") + " </th> \r\n" +
        //               "                     <th class=\"th-func-20\"></th> \r\n" +
        //               "                 </tr> \r\n" +
        //               "                 </thead> \r\n" +
        //               "                 <tbody> \r\n";
        //        for (int iIndex = 0; iIndex < Emails.Length; iIndex++)
        //        {
        //            Html +=
        //                "                 <tr> \r\n" +
        //                "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
        //                "                     <td>" + Emails[iIndex].SentFromInfo + "</td> \r\n" +
        //                "                     <td><a href=\"javascript:ViewEmailFromSearch('" + Emails[iIndex].EmailId + "');\">" + Emails[iIndex].Subject + "</a></td> \r\n" +
        //                "                     <td>" + Emails[iIndex].DateSent.ToString("dd/MM/yyyy HH:mm") + "</td> \r\n" +
        //                "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Gắn vào") + "\" href=\"javascript:CallMoveLinkAction('" + EmailId + "','" + Emails[iIndex].EmailId + "');\">" + WebLanguage.GetLanguage(OSiteParam, "Chuyển") + "</a></td> \r\n" +
        //                "                 </tr> \r\n";
        //        }
        //        Html +=
        //            "                 </tbody> \r\n" +
        //            "             </table> \r\n" +
        //            "       </div>\r\n";

        //        Html = WebEnvironments.EncryptHtml(Html);
        //        RetAjaxOut.HtmlContent = Html;
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}


        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut ServerSideAssignSale(
        //    RenderInfoCls ORenderInfo,
        //    string EmailId,
        //    string UserId,
        //    string ExpiredDate,
        //    string NotifyId)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam
        //            OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSessionUtility.CheckSessionTimeOut(OSiteParam);
        //        if (!FunctionUtilities.checkVnDate(ExpiredDate))
        //        {
        //            throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Ngày hết hạn không hợp lệ"));
        //        }

        //        //string NotifyUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).UserId;
        //        //OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess(OSiteParam).AssignEmailToUser(null, OSiteParam, EmailId, UserId, FunctionUtilities.VNDateToDate(ExpiredDate));
        //        //OnlineTourBussinessUtility.CreateBussinessProcess().CreateNotifyProcess(OSiteParam).CloseNotify(null, OSiteParam, NotifyId, NotifyUserId);
        //        //RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Phân email thành công");
        //        //RetAjaxOut.RetUrl = WebScreen.HomeUrl();
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideCloseEmail(
            RenderInfoCls ORenderInfo, 
            string Pop3EmailUserId, 
            string EmailId, 
            string NotifyId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CloseEmail(OActionSqlParam, Pop3EmailUserId, EmailId, OwnerUserId);
                //OnlineTourBussinessUtility.CreateBussinessProcess().CreateNotifyProcess(OSiteParam).CloseNotify(OActionSqlParam, NotifyId, UserId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đóng email thành công");
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, ViewEmail.StaticWebPartId, new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("EmailId",EmailId),
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
        public static AjaxOut ServerSideOpenEmail(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId,
            string NotifyId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().OpenEmail(OActionSqlParam, Pop3EmailUserId, EmailId, OwnerUserId);
                //OnlineTourBussinessUtility.CreateBussinessProcess().CreateNotifyProcess(OSiteParam).CloseNotify(null, OSiteParam, NotifyId, OwnerUserId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Mở theo dõi email thành công");
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, ViewEmail.StaticWebPartId, new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("EmailId",EmailId),
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
        public static AjaxOut ServerSideDeleteEmail(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
          
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().Delete(OActionSqlParam, Pop3EmailUserId, EmailId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Xóa email thành công");
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, UserWorkingMailBox.StaticWebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                        new WebParamCls("type","inbox"),
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
        public static AjaxOut ServerSideMoveToTrash(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
          
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().MoveToTrash(OActionSqlParam, Pop3EmailUserId, EmailId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Chuyển thư vào thùng rác thành công");
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, UserWorkingMailBox.StaticWebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                        new WebParamCls("type","trash"),
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
        public static AjaxOut ServerSideRecovertFromTrash(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);


                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().RecoverFromTrash(OActionSqlParam, Pop3EmailUserId, EmailId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Khôi phục thư từ thùng rác thành công");
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, UserWorkingMailBox.StaticWebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                        new WebParamCls("type","inbox"),
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
        public static AjaxOut ServerSideComposeEmailUrl(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId,
            int ReplyType)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
             
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, ComposeEmail.StaticWebPartId, new WebParamCls[]
                {
                    new WebParamCls("SrcPop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("SrcEmailId",EmailId),
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("ReplyType",ReplyType),
                });
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
        public static AjaxOut ServerSideDrawAddToCaseProfileForm(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                    ActionSqlParam=WebEnvironments.CreateActionSqlParam(OSiteParam);

                CaseProfileFilterCls
                    OCaseProfileFilter = new CaseProfileFilterCls();
                OCaseProfileFilter.ActiveOnly = 1;
                OCaseProfileFilter.OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;

                CaseProfileCls[]
                    CaseProfiles = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().Reading(ActionSqlParam, OCaseProfileFilter);

                string SelectCaseProfileText =
                    " <select id=\"drpSelectCaseProfile\" class=\"form-control select2\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn hồ sơ") + "</option>\r\n";
                for (int iIndex = 0; iIndex < CaseProfiles.Length; iIndex++)
                {
                    SelectCaseProfileText += "    <option value=\"" + CaseProfiles[iIndex].CaseProfileId + "\">" + CaseProfiles[iIndex].CaseProfileName + "</option>\r\n";
                }

                SelectCaseProfileText+=
                    "</select>\r\n";
                string Html =
                      " <div class=\"ibox-content\"> \r\n" +
                      "     <div class=\"row\"> \r\n" +
                      "         <div class=\"col-md-4\">\r\n" +
                      "             <h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Danh sách hồ sơ đang thuộc") + "</h3> \r\n";

                ReferenceObjectFilterCls
                    OReferenceObjectFilter = new ReferenceObjectFilterCls();
                OReferenceObjectFilter.LinkObjectId = EmailId;

                ReferenceObjectCls[]
                    ReferenceObjects=OnlineTourBussinessUtility.CreateBussinessProcess().CreateReferenceObjectProcess().Reading(ActionSqlParam,OReferenceObjectFilter);


                Html +=
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
                        "                     <td>" + ReferenceObjects[iIndex].RefObjectCode + "</td> \r\n" +
                        "                     <td>" + ReferenceObjects[iIndex].RefObjectSubject + "</td> \r\n" +
                        "                     <td class=\"td-center\"><a title=\"" + WebLanguage.GetLanguage(OSiteParam, "Xóa khỏi hồ sơ") + "\" href=\"javascript:CallActionDeleteReferenceObject('" + ReferenceObjects[iIndex].ReferenceObjectId + "');\"><i class=\"" + WebScreen.GetDeleteGridIcon() + "\"></i></a></td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "       </div>\r\n";

                Html+=
                      "         </div>\r\n"+
                      "         <div class=\"col-md-8\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Đưa vào hồ sơ theo dõi") + "</h3> \r\n" +
                      "             <div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Chọn hồ sơ") + "</label> " + SelectCaseProfileText + "</div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề") + "</label> <input id=\"txtRefSubject\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nhập tiêu đề") + "\" class=\"form-control\" value=\""+WebLanguage.GetLanguage(OSiteParam,"Đưa email vào hồ sơ")+"\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + "</label> <textarea id=\"txtRefNote\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Ghi chú") + "\" class=\"form-control\" style=\"height:200px\"></textarea></div> \r\n" +
                      "                 <div> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAddToCaseProfile('" + Pop3EmailUserId+"','"+ EmailId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "</strong></button> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBack();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                      "                 </div> \r\n" +
                      "             </div> \r\n" +
                      "         </div> \r\n" +
                      "     </div> \r\n" +
                      " </div> \r\n";
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
        public static AjaxOut ServerSideAddCaseProfile(
            RenderInfoCls ORenderInfo, 
            string Pop3EmailUserId, 
            string EmailId, 
            string CaseProfileId,
            string RefSubject, 
            string RefNote)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                EmailCls 
                    OEmail = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CreateModel(OActionSqlParam, Pop3EmailUserId, EmailId);

                CaseProfileCls 
                    OCaseProfile = OnlineTourBussinessUtility.CreateBussinessProcess().CreateCaseProfileProcess().CreateModel(OActionSqlParam, CaseProfileId);

                ReferenceObjectCls
                    OReferenceObject = new ReferenceObjectCls();
                OReferenceObject.ReferenceObjectId = System.Guid.NewGuid().ToString();
                OReferenceObject.RefSubject = RefSubject;
                OReferenceObject.RefNote = RefNote;
                OReferenceObject.RefecenceObjectType = "email";
                OReferenceObject.SrcObjectId = CaseProfileId;
                OReferenceObject.LinkObjectId = EmailId;
                
                OReferenceObject.LinkObjectCode = OEmail.UIId;
                OReferenceObject.LinkObjectSubject = OEmail.Subject;

                OReferenceObject.RefObjectCode = OCaseProfile.CaseProfileCode;
                OReferenceObject.RefObjectSubject = OCaseProfile.CaseProfileName;

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateReferenceObjectProcess().Add(OActionSqlParam, OReferenceObject);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đưa vào hồ sơ thành công");
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
        public static AjaxOut ServerSideDrawFollowUpForm(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                string Html =
                   " <div class=\"ibox-content\"> \r\n" +
                   "     <div class=\"row\"> \r\n" +
                   "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thêm mới followup") + "</h3> \r\n" +
                      "             <div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề") + "</label> <input  id=\"txtFollowUpSubject\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề") + "\" class=\"form-control\"></div> \r\n" +
                      "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + "</label> <textarea id=\"txtFollowUpNote\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + "\" class=\"form-control\" style=\"height:200px\"></textarea></div> \r\n" +
                      "                 <div> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionAddFollowUp('" + Pop3EmailUserId+"','"+ EmailId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                      "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:CallBackFromFollowUp();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
                      "                 </div> \r\n" +
                      "             </div> \r\n" +
                      "         </div> \r\n" +
                      "     </div> \r\n" +
                      " </div> \r\n";
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
        public static AjaxOut ServerSideAddFollowUp(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId,
            string Subject,
            string FollowUpComment)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                if (string.IsNullOrEmpty(Subject)) throw new Exception("Chưa nhập tiêu đề");
                if (string.IsNullOrEmpty(FollowUpComment)) throw new Exception("Chưa nhập nội dung phản hồi báo cáo");

                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                EmailCls
                    OEmail = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CreateModel(OActionSqlParam, Pop3EmailUserId, EmailId);
                EmailFollowUpCls
                    OEmailFollowUp = new EmailFollowUpCls();
                OEmailFollowUp.EmailFollowUpId = System.Guid.NewGuid().ToString();
                OEmailFollowUp.frkPop3EmailUserId = Pop3EmailUserId;
                OEmailFollowUp.EmailSubject = OEmail.Subject;
                OEmailFollowUp.EmailDate = OEmail.DateSent;
                OEmailFollowUp.FollowUpSubject = Subject;
                OEmailFollowUp.FollowUpComment = FollowUpComment;
                OEmailFollowUp.CommentDate = System.DateTime.Now;
                OEmailFollowUp.frkCommentByOwnerUserId = OwnerUserId;
                OEmailFollowUp.frkEmailId = EmailId;
                OEmailFollowUp.IsTask = 0;
                OEmailFollowUp.PercentComplete = 0;

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().AddEmailFollowUp(OActionSqlParam, Pop3EmailUserId, EmailId, OEmailFollowUp);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thêm mới followup thành công");
                RetAjaxOut.HtmlContent = ServerSideDrawEmailFollowUps(ORenderInfo, Pop3EmailUserId, EmailId).HtmlContent;
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
        public static AjaxOut ServerSideDeleteEmailFollowUp(
            RenderInfoCls ORenderInfo,
            string EmailFollowUpId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().DeleteEmailFollowUp(OActionSqlParam, EmailFollowUpId);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Xóa followup thành công");
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
        public static AjaxOut ServerSideDrawEmailFollowUps(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                EmailFollowUpCls[]
                    EmailFollowUps = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().ReadingEmailFollowUps(OActionSqlParam, EmailId);
                string Html = "";
                if (EmailFollowUps.Length > 0)
                {
                    Html +=
                        " <h3>" + WebLanguage.GetLanguage(OSiteParam, "FollowUps") + "</h3>\r\n" +
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-striped\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\"></th> \r\n" +
                         "                     <th style=\"width:20%\">" + WebLanguage.GetLanguage(OSiteParam, "Thông tin") + " </th> \r\n" +
                        "                     <th style=\"width:20%\">" + WebLanguage.GetLanguage(OSiteParam, "Tiêu đề") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + " </th> \r\n" +
                        "                     <th style=\"width:150px\">" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                        "                     <th style=\"width:20px\"></th> \r\n" +
                        "                     <th style=\"width:20px\"></th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < EmailFollowUps.Length; iIndex++)
                    {
                        string ApprovedStatus = WebLanguage.GetLanguage(OSiteParam, "Chờ xử lý");
                        if (EmailFollowUps[iIndex].Approved == 1)
                        {
                            ApprovedStatus = "<span style=\"color:green;font-weight:bold\">" + WebLanguage.GetLanguage(OSiteParam, "Duyệt") + "</span>\r\n<br>" + EmailFollowUps[iIndex].ApprovedByUserLoginName + "<br>" + EmailFollowUps[iIndex].ApprovedDate.ToString("dd/MM/yyyy HH:mm");
                        }
                        if (EmailFollowUps[iIndex].Approved == 2)
                        {
                            ApprovedStatus = "<span style=\"color:maroon;font-weight:bold\">" + WebLanguage.GetLanguage(OSiteParam, "Không duyệt") + "</span>\r\n<br>" + EmailFollowUps[iIndex].ApprovedByUserLoginName + "<br>" + EmailFollowUps[iIndex].ApprovedDate.ToString("dd/MM/yyyy HH:mm");
                        }

                        string Url = WebScreen.BuildUrl(OwnerCode, new ViewEmailFollowUp().WebPartId,
                            new WebParamCls[]
                            {
                                new WebParamCls("EmailFollowUpId",EmailFollowUps[iIndex].EmailFollowUpId)
                            });
                        string DeleteButton = "";
                        if (EmailFollowUps[iIndex].Approved == 0 && EmailFollowUps[iIndex].frkCommentByOwnerUserId.Equals(OwnerUserId))
                        {
                            DeleteButton = "<input type=button class=\"btn btn-sm\" value=\"Xoá\" onclick=\"javascript:CallDeleteEmailFollowUp('" + EmailFollowUps[iIndex].EmailFollowUpId + "');\">";
                        }
                        Html +=
                            "                 <tr id=\"tr" + EmailFollowUps[iIndex].EmailFollowUpId + "\"> \r\n" +
                            "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                            "                     <td>" + EmailFollowUps[iIndex].CommentDate.ToString("dd/MM/yyyy HH:mm") + "<br>" + EmailFollowUps[iIndex].CreateByUserLoginName + "</td> \r\n" +
                            "                     <td>" + EmailFollowUps[iIndex].FollowUpSubject + "</td> \r\n" +
                            "                     <td>" + EmailFollowUps[iIndex].FollowUpComment + "</td> \r\n" +
                            "                     <td>" + ApprovedStatus + "</td> \r\n" +
                            "                     <td><input type=button class=\"btn btn-sm\" value=\"Xem\" onclick=\"javascript:window.open('" + Url + "','_self');\"></td> \r\n" +
                            "                     <td>" + DeleteButton + "</td> \r\n" +
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


        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut ServerSideDrawAssignToProjectForm(
        //    RenderInfoCls ORenderInfo,
        //    string Pop3EmailUserId,
        //    string EmailId)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam
        //            OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSessionUtility.CheckSessionTimeOut(OSiteParam);

        //        EmailCls
        //            OEmail=OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess(OSiteParam).CreateModel(null,OSiteParam,Pop3EmailUserId,EmailId);
        //        if(OEmail==null)throw new Exception(WebLanguage.GetLanguage(OSiteParam,"Email đã bị xóa không tìm thấy"));
        //        ProjectFilterCls
        //             OProjectFilter = new ProjectFilterCls();

        //        ProjectCls[]
        //            Projects = OnlineTourBussinessUtility.CreateBussinessProcess().CreateProjectProcess(OSiteParam).Reading(null, OSiteParam, OProjectFilter);

        //        string ProjectId = "";
        //        if (Projects.Length > 0)
        //        {
        //            ProjectId = Projects[0].ProjectId;
        //        }
        //        string SelectProjectText =
        //         "<select id=\"drpSelectProject\" onchange=\"javascript:DetectProjectMileStone('"+Pop3EmailUserId+"','"+ EmailId+"');\" class=\"form-control select2\">\r\n";
        //        for (int iIndex = 0; iIndex < Projects.Length; iIndex++)
        //        {
        //            if (!string.IsNullOrEmpty(Projects[iIndex].ProjectId))
        //            {
        //                if (Projects[iIndex].ProjectId.Equals(OEmail.frkProjectId))
        //                {
        //                    SelectProjectText += "   <option selected value=\"" + Projects[iIndex].ProjectId + "\">" + Projects[iIndex].ProjectName + "</option>\r\n";
        //                }
        //                else
        //                {
        //                    SelectProjectText += "   <option value=\"" + Projects[iIndex].ProjectId + "\">" + Projects[iIndex].ProjectName + "</option>\r\n";
        //                }
        //            }
        //            else
        //            {
        //                SelectProjectText += "   <option value=\"" + Projects[iIndex].ProjectId + "\">" + Projects[iIndex].ProjectName + "</option>\r\n";
        //            }
        //        }
        //        SelectProjectText +=

        //         "</select>\r\n";

        //        string Html =
        //               " <div class=\"ibox-content\"> \r\n" +
        //               "     <div class=\"row\"> \r\n" +
        //               "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Đưa vào dự án") + "</h3> \r\n" +
        //               "             <div> \r\n" +
        //               "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Dự án") + "</label> " + SelectProjectText + "</div> \r\n" +
        //               "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Cột mốc") + "</label><div id=\"divSelectProjectMileStone\">" + ServerSideDrawProjectMileStoneForm(ORenderInfo, ProjectId, Pop3EmailUserId, EmailId).HtmlContent + "</div></div> \r\n" +
        //               "                 <div> \r\n" +
        //               "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallAssignEmailToProject('" + Pop3EmailUserId+"','"+EmailId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "</strong></button> \r\n" +
        //               "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:BackFromAssignToProject();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
        //               "                 </div> \r\n" +
        //               "             </div> \r\n" +
        //               "         </div> \r\n" +
        //               "     </div> \r\n" +
        //               " </div> \r\n";

        //        Html = WebEnvironments.EncryptHtml(Html);
        //        RetAjaxOut.HtmlContent = Html;
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}


        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut ServerSideDrawProjectMileStoneForm(
        //    RenderInfoCls ORenderInfo,
        //    string ProjectId,
        //    string Pop3EmailUserId,
        //    string EmailId)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam
        //            OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSessionUtility.CheckSessionTimeOut(OSiteParam);
        //        EmailCls
        //         OEmail = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess(OSiteParam).CreateModel(null, OSiteParam, Pop3EmailUserId, EmailId);
        //        if (OEmail == null) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Email đã bị xóa không tìm thấy"));
       
        //        ProjectMileStoneCls[]
        //            ProjectMileStones = OnlineTourBussinessUtility.CreateBussinessProcess().CreateProjectProcess(OSiteParam).ReadingProjectMileStones(null, OSiteParam, ProjectId);

        //        string SelectProjectMileStoneText =
        //         "<select id=\"drpSelectProjectMileStone\" class=\"form-control\">\r\n" +
        //         "  <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chọn cột mốc") + "</option>\r\n";
        //        for (int iIndex = 0; iIndex < ProjectMileStones.Length; iIndex++)
        //        {
        //            if (!string.IsNullOrEmpty(OEmail.frkProjectMileStoneId))
        //            {
        //                if (OEmail.frkProjectMileStoneId.Equals(ProjectMileStones[iIndex].ProjectMileStoneId))
        //                {
        //                    SelectProjectMileStoneText += "   <option selected value=\"" + ProjectMileStones[iIndex].ProjectMileStoneId + "\">" + ProjectMileStones[iIndex].ProjectMileStoneName + "</option>\r\n";
        //                }
        //                else
        //                {
        //                    SelectProjectMileStoneText += "   <option value=\"" + ProjectMileStones[iIndex].ProjectMileStoneId + "\">" + ProjectMileStones[iIndex].ProjectMileStoneName + "</option>\r\n";
        //                }
        //            }
        //            else
        //            {
        //                SelectProjectMileStoneText += "   <option value=\"" + ProjectMileStones[iIndex].ProjectMileStoneId + "\">" + ProjectMileStones[iIndex].ProjectMileStoneName + "</option>\r\n";
        //            }
        //        }
        //        SelectProjectMileStoneText +=

        //         "</select>\r\n";

        //        RetAjaxOut.HtmlContent = SelectProjectMileStoneText;
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}


        //[AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        //public static AjaxOut ServerSideAddEmailToProject(
        //    RenderInfoCls ORenderInfo,
        //    string Pop3EmailUserId,
        //    string EmailId,
        //    string ProjectId,
        //    string ProjectMileStoneId)
        //{
        //    AjaxOut RetAjaxOut = new AjaxOut();
        //    try
        //    {
        //        SiteParam
        //            OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
        //        WebSessionUtility.CheckSessionTimeOut(OSiteParam);
        //        OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess(OSiteParam).AssignEmailToProject(null, OSiteParam, Pop3EmailUserId, EmailId, ProjectId, ProjectMileStoneId);
        //        RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Đưa email vào dự án thành công");
        //    }
        //    catch (Exception ex)
        //    {
        //        RetAjaxOut.Error = true;
        //        RetAjaxOut.InfoMessage = ex.Message.ToString();
        //        RetAjaxOut.HtmlContent = ex.Message.ToString();
        //    }
        //    return RetAjaxOut;
        //}


        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDrawMoveToEmailFolderForm(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                EmailCls
                    OEmail = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CreateModel(OActionSqlParam, Pop3EmailUserId, EmailId);
                EmailFolderCls[]
                    EmailFolders = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().Reading(OActionSqlParam, OEmail.frkPop3EmailUserId, null);
                string SelectEmailFolderText =
                    "<select id=\"drpSelectEmailFolder\" class=\"form-control\">\r\n" +
                    "   <option value=\"\">" + WebLanguage.GetLanguage(OSiteParam, "Chuyển về gốc") + "</option>\r\n";
                int LevelIndex = 1;
                for (int iIndex = 0; iIndex < EmailFolders.Length; iIndex++)
                {
                    SelectEmailFolderText += "<option value=\"" + EmailFolders[iIndex].EmailFolderId + "\"> --" + EmailFolders[iIndex].EmailFolderName + "</option>\r\n";
                    LevelIndex++;
                    SelectEmailFolderText += DrawSubEmailFolderForLookUp(ORenderInfo, OEmail.frkPop3EmailUserId, EmailFolders[iIndex].EmailFolderId, LevelIndex).HtmlContent;
                    LevelIndex--;
                }
                SelectEmailFolderText += "<select>\r\n";


                string Html =
                       " <div class=\"ibox-content\"> \r\n" +
                       "     <div class=\"row\"> \r\n" +
                       "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Đưa vào thư mục") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thư mục") + "</label> " + SelectEmailFolderText + "</div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallMoveToEmailFolder('" + Pop3EmailUserId+"','"+ EmailId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:ClientBackFromMoveFolder();\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Bỏ qua") + "</strong></button> \r\n" +
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


        public static AjaxOut DrawSubEmailFolderForLookUp(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailFolderId,
            int LevelIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                EmailFolderCls[]
                    EmailFolders = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().Reading(OActionSqlParam, Pop3EmailUserId, EmailFolderId);
                string SelectEmailFolderText = "";
                for (int iIndex = 0; iIndex < EmailFolders.Length; iIndex++)
                {
                    string AppendText = " ";
                    for (int iIndexLevelIndex = 0; iIndexLevelIndex < LevelIndex; iIndexLevelIndex++)
                    {
                        AppendText = AppendText + "--";
                    }
                    SelectEmailFolderText += "<option value=\"" + EmailFolders[iIndex].EmailFolderId + "\">" + AppendText + " " + EmailFolders[iIndex].EmailFolderName + "</option>\r\n";
                    LevelIndex++;
                    SelectEmailFolderText += DrawSubEmailFolderForLookUp(ORenderInfo, Pop3EmailUserId, EmailFolders[iIndex].EmailFolderId, LevelIndex).HtmlContent;
                    LevelIndex--;
                }

                RetAjaxOut.HtmlContent = SelectEmailFolderText;
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
        public static AjaxOut ServerSideMoveEmailFolder(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId, 
            string EmailId, 
            string MoveToFolderId)
        {
            AjaxOut OAjaxOut = new AjaxOut();

            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().MoveFolder(OActionSqlParam, Pop3EmailUserId, EmailId, MoveToFolderId);
                OAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, UserWorkingMailBox.StaticWebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                        new WebParamCls("EmailFolderId",MoveToFolderId),
                    });
            }
            catch (Exception ex)
            {
                OAjaxOut.Error = true;
                OAjaxOut.InfoMessage = ex.Message.ToString();
                OAjaxOut.HtmlContent = ex.Message.ToString();
            }

            return OAjaxOut;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDeleteReferenceObject(
            RenderInfoCls ORenderInfo,
            string ReferenceObjectId)
        {
            AjaxOut OAjaxOut = new AjaxOut();

            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                OnlineTourBussinessUtility.CreateBussinessProcess().CreateReferenceObjectProcess().Delete(OActionSqlParam, ReferenceObjectId);
            }
            catch (Exception ex)
            {
                OAjaxOut.Error = true;
                OAjaxOut.InfoMessage = ex.Message.ToString();
                OAjaxOut.HtmlContent = ex.Message.ToString();
            }

            return OAjaxOut;
        }

    }

}

