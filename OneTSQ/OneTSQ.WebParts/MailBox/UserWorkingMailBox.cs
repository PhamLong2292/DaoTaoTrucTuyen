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
    public class UserWorkingMailBox : WebPartTemplate
    {
        public static string StaticWebPartId
        {
            get
            {
                return "UserWorkingMailBox".ToLower();
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
                return "Xử lý công việc qua email";
            }
        }

        public override string Description
        {
            get
            {
                return "Xử lý công việc qua email";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(UserWorkingMailBox),Page);
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

        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string SessionId = System.Guid.NewGuid().ToString();
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Pop3EmailUserId = WebEnvironments.Request("Pop3EmailUserId");
                if (string.IsNullOrEmpty(Pop3EmailUserId)) Pop3EmailUserId = "";
                
                string EmailFolderId = (string)WebEnvironments.Request("EmailFolderId");
                if (string.IsNullOrEmpty(EmailFolderId)) EmailFolderId = "";

                if (string.IsNullOrEmpty(Pop3EmailUserId))
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tham số không hợp lệ"));
                }

                Pop3EmailUserCls
                    OPop3EmailUser = OnlineTourBussinessUtility.CreateBussinessProcess().CreatePop3EmailUserProcess().CreateModel(ActionSqlParam, Pop3EmailUserId);
                if (OPop3EmailUser == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Không tìm thấy mailbox"));
                }
                RetAjaxOut.HtmlContent =
                    ViewEmail.GetPluginJavascript(OSiteParam) +
                    
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +

                    "   function loadEmailItem(Pop3EmailUserId, FolderId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       selectedPop3EmailUserId=Pop3EmailUserId;\r\n" +
                    "       selectedFolderId=FolderId;\r\n" +
                    "       viewType=document.getElementById('txtViewType').value;\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.UserWorkingMailBox.ServerSideGetUrl(RenderInfo, Pop3EmailUserId, FolderId, viewType).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +


                    "   function ViewEmail(Pop3EmailUserId, EmailId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('divProcessing'+EmailId).innerHTML='" + WebLanguage.GetLanguage(OSiteParam, "Đang đọc dữ liệu...") + "';\r\n" +
                    "       setTimeout('RealViewEmail(\"'+Pop3EmailUserId+'\", \"'+EmailId+'\")',10);\r\n" +
                    "   }\r\n" +

                    "   function RealViewEmail(Pop3EmailUserId, EmailId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       ViewType = document.getElementById('txtViewType').value;\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.UserWorkingMailBox.ServerSideViewEmail(RenderInfo, Pop3EmailUserId, EmailId, ViewType).value;\r\n" +

                    //"       document.getElementById('divProcessing'+EmailId).innerHTML='';\r\n" +

                    "       document.getElementById('divListEmailContent').style.display='none';\r\n" +
                    "       document.getElementById('divViewEmailContent').innerHTML=AjaxOut.HtmlContent;\r\n" +

                    "       document.getElementById('divProcessing'+EmailId).innerHTML='';\r\n" +
                    "       document.getElementById('divViewEmailContent').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "       document.getElementById('divViewEmailContent').style.display='block';\r\n" +
                    "   }\r\n" +

                    "   function ClientBackFromAjaxViewEmail()\r\n"+
                    "   {\r\n"+
                    "       document.getElementById('divListEmailContent').style.display='block';\r\n" +
                    "       document.getElementById('divViewEmailContent').innerHTML='';\r\n" +
                    "       document.getElementById('divProcessing'+EmailId).innerHTML='';\r\n" +
                    "       document.getElementById('divViewEmailContent').style.display='none';\r\n" +
                    "   }\r\n"+

                    "   function CallBackFromComposeEmail()\r\n" +
                    "   {\r\n"+
                    "       document.getElementById('divViewEmailContent').style.display='block';\r\n" +
                    "       document.getElementById('divComposeEmail').style.display='none';\r\n"+
                    "       document.getElementById('divComposeEmail').innerHTML='';\r\n" +
                    "   }\r\n"+

                    "   function ComposeEmail(Pop3EmailUserId, EmailId)\r\n"+
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.UserWorkingMailBox.ServerSideComposeEmailUrl(RenderInfo, Pop3EmailUserId, EmailId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +
                    "</script>\r\n") +

                    "<input id=\"txtPop3EmailUserId\" type=\"hidden\">\r\n" +
                    "<div id=\"divComposeEmail\"></div>\r\n" +
                    "<div id=\"divAssignToProjectForm\"></div>\r\n" +
                    "<div id=\"divViewEmailContent\"></div>\r\n" +
                    "<div id=\"divListEmailContent\">\r\n" +
                        ServerSideReading(ORenderInfo, Pop3EmailUserId, EmailFolderId, "").HtmlContent +
                    "</div>\r\n" +

                    "<script>\r\n" +
                    "function CallInitTreeView()\r\n"+
                    "{\r\n"+

                    "$('#treeFolder').jstree({ \r\n" +
                    "    \"core\" : { \r\n" +
                    "        \"themes\" : { \r\n" +
                    "            \"responsive\": false \r\n" +
                    "        } \r\n" +
                    "    }, \r\n" +
                    "    \"types\" : { \r\n" +
                    "        \"default\" : { \r\n" +
                    "            \"icon\" : \"fa fa-folder icon-state-warning icon-lg\" \r\n" +
                    "        }, \r\n" +
                    "        \"file\" : { \r\n" +
                    "            \"icon\" : \"fa fa-file icon-state-warning icon-lg\" \r\n" +
                    "        } \r\n" +
                    "    }, \r\n" +
                    "    \"plugins\": [\"types\"] \r\n" +
                    "}); \r\n" +

                    "  $('#treeFolder').on('select_node.jstree', function(e,data) {  \r\n" +
                    "     var link = $('#' + data.selected).find('a');  \r\n" +
                    "     if (link.attr(\"href\") != \"#\" && link.attr(\"href\") != \"javascript:;\" && link.attr(\"href\") != \"\") {  \r\n" +
                    "         if (link.attr(\"target\") == \"_blank\") {  \r\n" +
                    "             link.attr(\"href\").target = \"_blank\";  \r\n" +
                    "         }  \r\n" +
                    "         document.location.href = link.attr(\"href\");  \r\n" +
                    "         return false;  \r\n" +
                    "     }  \r\n" +
                    "   });  \r\n" +
                    " }\r\n"+

                    "   $(\"#treeFolder\").jstree(\"select_node\", \"#"+EmailFolderId+"\"); \r\n"+
                    " CallInitTreeView();\r\n"+
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
        public static AjaxOut ServerSideReading(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailFolderId,
            string Keyword)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string ViewType = WebEnvironments.Request("type");
                string HeadingTitle = "Inbox";
                if (string.IsNullOrEmpty(ViewType))
                {
                    ViewType = "inbox";
                }
                Pop3EmailUserCls
                    OPop3EmailUser = OnlineTourBussinessUtility.CreateBussinessProcess().CreatePop3EmailUserProcess().CreateModel(ActionSqlParam, Pop3EmailUserId);
                EmailFilterCls
                    OEmailFilter=new EmailFilterCls();
                //OEmailFilter.AssignToUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).UserId;
                OEmailFilter.Keyword = Keyword;
                OEmailFilter.Pop3EmailUserId = Pop3EmailUserId;
                EmailCls[]
                    ViewEmails = null;
                if (ViewType.ToLower().Trim().Equals("new"))
                {
                    HeadingTitle = "New";
                    OEmailFilter.HasRead = 0;
                    OEmailFilter.Inbox = 1;
                    ViewEmails = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().Reading(ActionSqlParam, OEmailFilter);
                }
                if (ViewType.ToLower().Trim().Equals("inbox"))
                {
                    HeadingTitle = "Inbox";
                    OEmailFilter.Inbox = 1;
                    OEmailFilter.EmailFolderId = null;
                    ViewEmails = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().Reading(ActionSqlParam, OEmailFilter);
                }
                if (ViewType.ToLower().Trim().Equals("folder"))
                {
                    EmailFolderCls
                        OEmailFolder = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().CreateModel(ActionSqlParam, Pop3EmailUserId, EmailFolderId);
                    OEmailFilter.Inbox = 1;
                    HeadingTitle = "Folder: " + OEmailFolder.EmailFolderName;
                    OEmailFilter.EmailFolderId = EmailFolderId;
                    ViewEmails = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().Reading(ActionSqlParam, OEmailFilter);
                }
                if (ViewType.ToLower().Trim().Equals("sent"))
                {
                    HeadingTitle = "Sent";
                    OEmailFilter.SentEmail = 1;
                    ViewEmails = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().Reading(ActionSqlParam, OEmailFilter);
                }
                if (ViewType.ToLower().Trim().Equals("trash"))
                {
                    HeadingTitle = "Trash";
                    OEmailFilter.Trash = 1;
                    ViewEmails = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().Reading(ActionSqlParam, OEmailFilter);
                }
                if (ViewType.ToLower().Trim().Equals("draft"))
                {
                    HeadingTitle = "Draft";
                    OEmailFilter.Draft = 1;
                    ViewEmails = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().Reading(ActionSqlParam, OEmailFilter);
                }

                MailBoxInfoCls
                    OGetMailBoxInfo = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().GetMailBoxInfo(ActionSqlParam, Pop3EmailUserId);


                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;

                string NewEmailUrl = WebScreen.BuildUrl(OwnerCode, new UserWorkingMailBox().WebPartId,
                   new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("type","new"),
                });

                string InBoxUrl = WebScreen.BuildUrl(OwnerCode, new UserWorkingMailBox().WebPartId,
                    new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("type","inbox"),
                });

                string SentUrl = WebScreen.BuildUrl(OwnerCode, new UserWorkingMailBox().WebPartId,
                  new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("type","sent"),
                });

                string TrashUrl = WebScreen.BuildUrl(OwnerCode, new UserWorkingMailBox().WebPartId,
                  new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("type","trash"),
                });

                string DraftUrl = WebScreen.BuildUrl(OwnerCode, new UserWorkingMailBox().WebPartId,
                  new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("type","draft"),
                });

                int DraftTotal = OGetMailBoxInfo.Draft;
                int SentTotal = OGetMailBoxInfo.Sent;
                int TrashTotal = OGetMailBoxInfo.Trash;
                int InboxTotal = OGetMailBoxInfo.Inbox;
                int NotReadTotal = OGetMailBoxInfo.NotRead;

                string ComposeEmailUrl = WebScreen.BuildUrl(OwnerCode, ComposeEmail.StaticWebPartId, new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId)
                });

                string Html =
                       " <input type=hidden id=\"txtViewType\" value=\"" + ViewType + "\">\r\n" +

                       " <div class=\"row\"> \r\n" +
                       " <div style=\"font-size:16px;color:green;font-weight:bold;margin-bottom:10px;margin-left:20px\">Hộp thư: " + OPop3EmailUser.Pop3Name + "</div> \r\n" +
                       " <div class=\"col-lg-3\"> \r\n" +
                       "     <div class=\"ibox float-e-margins\"> \r\n" +
                       "         <div class=\"ibox-content mailbox-content\"> \r\n" +
                       "             <div class=\"file-manager\"> \r\n";

                bool AllowCompose = false;
                if (OPop3EmailUser.frkOwnerUserId.Equals(WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId))
                {
                    AllowCompose = true;
                }
                if (AllowCompose)
                {
                    Html +=
                           "                 <a class=\"btn btn-block btn-primary compose-mail\" target=\"_self\" href=\"" + ComposeEmailUrl + "\">" + WebLanguage.GetLanguage(OSiteParam, "Soạn Email") + "</a> \r\n";
                }
                Html+=
                       "                 <div class=\"space-25\"></div> \r\n" +
                       
                       "                 <ul class=\"folder-list m-b-md\" style=\"padding: 0\"> \r\n" +
                       "                     <li><a href=\"" + NewEmailUrl + "\"> <i class=\"fa fa-inbox \"></i> " + WebLanguage.GetLanguage(OSiteParam, "New") + " <span class=\"label label-warning pull-right\">" + NotReadTotal.ToString("#,##0") + "</span> </a></li> \r\n" +
                       "                     <li><a href=\"" + InBoxUrl +"\"> <i class=\"fa fa-inbox \"></i> " + WebLanguage.GetLanguage(OSiteParam, "Inbox") + " <span class=\"label label-warning pull-right\">" + InboxTotal.ToString("#,##0") + "</span> </a></li> \r\n"+
                       "                     <li><a href=\"" + SentUrl + "\"> <i class=\"fa fa-inbox \"></i> " + WebLanguage.GetLanguage(OSiteParam, "Sent") + " <span class=\"label label-warning pull-right\">" + SentTotal.ToString("#,##0") + "</span> </a></li> \r\n" +
                       "                     <li><a href=\"" + TrashUrl + "\"> <i class=\"fa fa-inbox \"></i> " + WebLanguage.GetLanguage(OSiteParam, "Trash") + " <span class=\"label label-warning pull-right\">" + TrashTotal.ToString("#,##0") + "</span> </a></li> \r\n" +
                       "                     <li><a href=\"" + DraftUrl + "\"> <i class=\"fa fa-inbox \"></i> " + WebLanguage.GetLanguage(OSiteParam, "Draft") + " <span class=\"label label-warning pull-right\">" + DraftTotal.ToString("#,##0") + "</span> </a></li> \r\n" +
                       "                 </ul> \r\n" +
                       "                 <div class=\"clearfix\"></div> \r\n";
                if (OPop3EmailUser != null)
                {
                    string ConfigFolderUrl = WebScreen.BuildUrl(OwnerCode, new EmailFolder().WebPartId, 
                        new WebParamCls[]
                        {
                            new WebParamCls("Pop3EmailUserId",OPop3EmailUser.Pop3EmailUserId)
                        });
                    Html +=
                       "                 <h3><a href=\"" + ConfigFolderUrl + "\">" + WebLanguage.GetLanguage(OSiteParam, "Thư mục") + "</a></h3>\r\n";

                    
                    if (AllowCompose)
                    {
                        Html +=
                           "                 <div style=\"margin-top:2px;margin-bottom:2px\"><input onclick=\"javascript:window.open('" + ConfigFolderUrl + "','_self');\" type=button class\"btn btn-xs\" value=\"" + WebLanguage.GetLanguage(OSiteParam, "Thiết lập") + "\"></div>\r\n";
                    }
                    Html+=
                       "                 <div id=\"treeFolder\" style=\"margin-left:-10px\">" + ServerSideDrawFolder(ORenderInfo, Pop3EmailUserId, OPop3EmailUser.Pop3Name).HtmlContent + "</div>\r\n";
                }
                Html+=
                       "             </div> \r\n" +
                       "         </div> \r\n" +
                       "     </div> \r\n" +
                       " </div> \r\n" +
                       "         <div class=\"col-lg-9\"> \r\n" +
                       "         <div class=\"mail-box-header\"> \r\n" +

                       "             <div style=\"margin-bottom:5px\">\r\n" +
                       "                <h3> \r\n" +
                       "                     " + HeadingTitle + " (" + ViewEmails.Length.ToString("#,##0") + ") \r\n" +
                       "                 </h3> \r\n" +
                       "            </div>\r\n" +

                       "             <div class=\"mail-search\"> \r\n" +
                       "                 <div class=\"input-group\"> \r\n" +
                       "                     <input onkeypress=\"if(event.keyCode==13)doSearch();\" type=\"text\" value=\"" + Keyword + "\" class=\"form-control input-sm\" id=\"txtKeyword\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tìm kiếm...") + "\"> \r\n" +
                       "                     <div class=\"input-group-btn\"> \r\n" +
                       "                         <button type=\"submit\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:doSearch();\"> \r\n" +
                       "                             " + WebLanguage.GetLanguage(OSiteParam, "Tìm kiếm") + " \r\n" +
                       "                         </button> \r\n" +
                       "                     </div> \r\n" +
                       "                 </div> \r\n" +
                       "             </div> \r\n" +
                       
                       "             <div id=\"divProcessing\" style=\"color:maroon;font-weight:bold\"></div>\r\n" +
                       "         </div> \r\n" +
                       "             <div class=\"mail-box\" style=\"min-height:400px\"> \r\n" +

                       "             <table class=\"table table-hover table-mail\"> \r\n" +
                       "             <tbody> \r\n";
                for (int iIndex = 0; iIndex < ViewEmails.Length; iIndex++)
                {
                    string Css = "unread";
                    if (ViewEmails[iIndex].HasRead == 1)
                    {
                        Css = "read";
                    }
                    Html +=
                        "             <tr class=\"" + Css + "\" id=\"tr" + ViewEmails[iIndex].EmailId + "\"> \r\n"+
                        "                 <td class=\"check-mail\"> \r\n" +
                                            (ViewEmails[iIndex].IsRequest==1?"R":"")+
                        "                 </td> \r\n";

                    if (ViewEmails[iIndex].Draft == 0)
                    {
                        Html +=
                            "                 <td class=\"mail-subject\" style=\"width:35%\"><a style=\"cursor:pointer\" href=\"javascript:ViewEmail('" + Pop3EmailUserId + "','" + ViewEmails[iIndex].EmailId + "');\"><strong>" + ViewEmails[iIndex].SentFromInfo + "</strong><br>" + ViewEmails[iIndex].Subject + "</a><div style=\"color:maroon;font-weight:bold\" id=\"divProcessing" + ViewEmails[iIndex].EmailId + "\"><div></td> \r\n";
                    }
                    else
                    {
                        Html +=
                            "                 <td class=\"mail-subject\" style=\"width:35%\"><a style=\"cursor:pointer\" href=\"javascript:ComposeEmail('" + Pop3EmailUserId + "','" + ViewEmails[iIndex].EmailId + "');\"><strong>" + ViewEmails[iIndex].SentFromInfo + "</strong><br>" + ViewEmails[iIndex].Subject + "</a><div style=\"color:maroon;font-weight:bold\" id=\"divProcessing" + ViewEmails[iIndex].EmailId + "\"><div></td> \r\n";
                    }
          
                    if (ViewEmails[iIndex].TotalAttached > 0)
                    {
                        Html +=
                            "   <td class=\"\">" + ViewEmails[iIndex].TotalAttached.ToString("#,##0") + " <i class=\"fa fa-paperclip\"></i></td> \r\n";
                    }
                    else
                    {
                        Html +=
                            "   <td class=\"\"></td> \r\n";
                    }
                    Html +=
                        "                 <td class=\"text-right mail-date\" style=\"width:180px\">" + ViewEmails[iIndex].DateInfo + "</td> \r\n" +
                        "                 <td class=\"text-right mail-date\" style=\"width:180px\">" + ViewEmails[iIndex].ExpiredDateInfo + "</td> \r\n" +
                        "                 <td class=\"text-right\">" + ViewEmails[iIndex].TotalReply + "</td>\r\n"+
                        "             </tr> \r\n";
                }

                Html +=
                    "             </tbody> \r\n" +
                    "             </table> \r\n";


                if (ViewEmails.Length == 0)
                {
                    Html +=
                        "<div style=\"text-align:center;margin-bottom:10px;font-size:18px;font-weight:bold\">" + WebLanguage.GetLanguage(OSiteParam, "Không có thư nào trong mục này") + "</div>\r\n" +
                        "<center><img src=\"img/email.jpg\" style=\"height:400px\" /></center>\r\n";
                }
                Html+=
                    "   </div>\r\n"+
                    "         </div> \r\n" +
                    "     </div> \r\n";
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


        public static AjaxOut ServerSideDrawFolder(RenderInfoCls ORenderInfo, string Pop3EmailUserId, string Subject)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string AssetCode = OSiteParam.AssetCode;
                string AssetLevelId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).CurrentAssetLevelId;
                EmailFolderCls[]
                    SimpleFolders = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().Reading(ActionSqlParam, Pop3EmailUserId, null);
                string Html =
                        "<div id=\"treeFolder\" class=\"tree-demo\"> \r\n" +
                        "        <ul> \r\n" +
                        "            <li class=\"context-menu-one\" data-jstree='{ \"opened\" : true }'> \r\n" +
                        "               <a href=\"javascript:loadEmailItem('" + Pop3EmailUserId + "',null);\">" + Subject + "</a> \r\n";
                if (SimpleFolders.Length > 0)
                {
                    Html +=
                        "   <ul> \r\n";
                    for (int iIndex = 0; iIndex < SimpleFolders.Length; iIndex++)
                    {
                        Html +=
                            "            <li id=\"" + SimpleFolders[iIndex].EmailFolderId + "\" class=\"context-menu-one\" data-jstree='{ \"opened\" : true }'> \r\n" +
                            "               <a href=\"javascript:loadEmailItem('" + Pop3EmailUserId + "','" + SimpleFolders[iIndex].EmailFolderId + "');\"> " + SimpleFolders[iIndex].EmailFolderName + " (" + SimpleFolders[iIndex].TotalEmail.ToString("#,##0") + ") </a> \r\n" +
                                            ServerSideReadingSubFolder(ORenderInfo, Pop3EmailUserId, SimpleFolders[iIndex].EmailFolderId).HtmlContent +
                            "            </li> \r\n";
                    }
                    Html +=
                        "                </ul> \r\n";
                }
                Html +=
                        "            </li> \r\n" +
                        "        </ul> \r\n" +
                        "    </div> \r\n";

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
        public static AjaxOut ServerSideReadingSubFolder(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailFolderId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            string Html = "";
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam=WebEnvironments.CreateActionSqlParam(OSiteParam);
                EmailFolderCls[]
                    SubSimpleFolders = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailFolderProcess().Reading(ActionSqlParam, Pop3EmailUserId, EmailFolderId);
                if (SubSimpleFolders.Length > 0)
                {
                    Html +=
                        "   <ul> \r\n";
                    for (int iIndex = 0; iIndex < SubSimpleFolders.Length; iIndex++)
                    {
                        Html +=
                            "                    <li id=\"" + SubSimpleFolders[iIndex].EmailFolderId + "\"  class=\"context-menu-one\" data-jstree='{ \"opened\" : true }'> \r\n" +
                            "                        <a href=\"javascript:loadEmailItem('" + Pop3EmailUserId + "','" + SubSimpleFolders[iIndex].EmailFolderId + "');\"> " + SubSimpleFolders[iIndex].EmailFolderName + " (" + SubSimpleFolders[iIndex].TotalEmail.ToString("#,##0") + ")</a> \r\n" +
                                                     ServerSideReadingSubFolder(ORenderInfo, Pop3EmailUserId, SubSimpleFolders[iIndex].EmailFolderId).HtmlContent +
                            "                    </li> \r\n";
                    }
                    Html +=
                        "                </ul> \r\n";
                }
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
        public static AjaxOut ServerSideGetUrl(RenderInfoCls ORenderInfo, string Pop3EmailUserId, string EmailFolderId, string viewType)
        {
            AjaxOut OAjaxOut = new AjaxOut();

            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                if (!string.IsNullOrEmpty(EmailFolderId)) viewType = "folder";

                OAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, StaticWebPartId,
                    new WebParamCls[]
                    {
                        new WebParamCls("type",viewType),
                        new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                        new WebParamCls("EmailFolderId",EmailFolderId),
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
        public static AjaxOut ServerSideViewEmail(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId,
            string ViewType,
            string NotifyId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                RetAjaxOut = ViewEmail.ServerSideViewEmail(ORenderInfo, Pop3EmailUserId, EmailId, NotifyId);
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
        public static AjaxOut ServerSideComposeEmailUrl(
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
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                RetAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, ComposeEmail.StaticWebPartId, new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("EmailId",EmailId),
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

    }
}

