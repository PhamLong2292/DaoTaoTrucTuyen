using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ChatUtility
{
    public class ChatCommentUtility
    {
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static string GetPlugInJavascript(RenderInfoCls ORenderInfo,string ChatSubject, string[] OwnerUserIds)
        {
            SiteParam
                OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

            string OwnerUserIdText = "";
            for (int iIndex = 0; iIndex < OwnerUserIds.Length; iIndex++)
            {
                OwnerUserIdText = OwnerUserIdText + OwnerUserIds[iIndex];
                if (iIndex < OwnerUserIds.Length - 1)
                {
                    OwnerUserIdText += ";";
                }
            }
            return
                "<input id=\"txtOwnerUserIds\" type=\"hidden\" value=\""+OwnerUserIdText+"\">\r\n"+
                "<div id=\"txtChatSubject\" style=\"display:none\">" + ChatSubject + "</div>\r\n" +
                "<script>\r\n" +
                "   function AddChat(ObjectType,ObjectId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +
                "       OwnerUserIds = document.getElementById('txtOwnerUserIds').value;\r\n"+
                "       ChatSubject = document.getElementById('txtChatSubject').innerHTML;\r\n" +
                "       ChatContent = document.getElementById('txtChatContent').value;\r\n" +
                "       AjaxOut = OneTSQ.ChatUtility.ChatCommentUtility.ServerSideAddChat(RenderInfo, ObjectType, ObjectId, ChatSubject, ChatContent, OwnerUserIds).value;\r\n" +
                "       if(AjaxOut.Error)\r\n" +
                "       {\r\n" +
                "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "           return;\r\n" +
                "       }\r\n" +
                
                "       document.getElementById('divListChatContent').innerHTML=AjaxOut.HtmlContent;\r\n"+
                "       document.getElementById('txtChatContent').value='';\r\n"+
                "   }\r\n" +

                "   function RemoveChat(ObjectType, ObjectId, ChatId, OwnerUserId)\r\n" +
                "   {\r\n" +
                "       RenderInfo=CreateRenderInfo();\r\n" +


                "       swal({ \r\n" +
                "               title: \"" + WebLanguage.GetLanguageConfirmDelete(OSiteParam) + "\", \r\n" +
                "               text: \"" + WebLanguage.GetLanguage(OSiteParam, "Đang thực hiện xóa ra khỏi hệ thống") + "!\", \r\n" +
                "               type: \"warning\", \r\n" +
                "               showCancelButton: true, \r\n" +
                "               confirmButtonColor: \"#DD6B55\", \r\n" +
                "               confirmButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Thực hiện xóa") + "\", \r\n" +
                "               cancelButtonText: \"" + WebLanguage.GetLanguage(OSiteParam, "Hủy bỏ") + "\", \r\n" +
                "               closeOnConfirm: false \r\n" +
                "           }, function () { \r\n" +


                "           AjaxOut = OneTSQ.ChatUtility.ChatCommentUtility.ServerSideDeleteChat(RenderInfo, ObjectType, ObjectId, ChatId, OwnerUserId).value;\r\n" +
                "           if(AjaxOut.Error)\r\n" +
                "           {\r\n" +
                "               callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "               return;\r\n" +
                "           }\r\n" +
                "           document.getElementById('divListChatContent').innerHTML=AjaxOut.HtmlContent;\r\n" + 

                "           swal(\"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa") + "!\", \"" + WebLanguage.GetLanguage(OSiteParam, "Đã xóa thành công!") + ".\", \"success\"); \r\n" +
                "       }); \r\n" +

              
                "   }\r\n" +
                "</script>\r\n";
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDrawChat(RenderInfoCls ORenderInfo, string ObjectType, string ObjectId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string Html =

                "<div id=\"divListChatContent\">" + ServerSideReadingChat(ORenderInfo, ObjectType, ObjectId, OwnerUserId).HtmlContent + "</div>\r\n" +
                "<hr/>\r\n" +
                "<div style=\"background: #eee;padding: 15px;height: 450px; overflow-y: auto;\" > \r\n" +
                //"    <h3>" + WebLanguage.GetLanguage(OSiteParam, "Thảo luận") + "</h3> \r\n" +

                "    <div class=\"form-group\"> \r\n" +
                "        <label>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung") + "</label> \r\n" +
                "        <textarea id=\"txtChatContent\" class=\"form-control\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Nội dung thảo luận") + "\" rows=\"3\"></textarea> \r\n" +
                "    </div> \r\n" +
                "    <button onclick=\"javascript:AddChat('" + ObjectType+"','"+ ObjectId + "');\" class=\"btn btn-primary btn-block\">" + WebLanguage.GetLanguage(OSiteParam, "Thảo luận") + "</button> \r\n" +

                "</div> \r\n";

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
        public static AjaxOut ServerSideReadingChat(
            RenderInfoCls ORenderInfo,
            string ObjectType,
            string ObjectId,
            string OwnerUserId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                ChatFilterCls
                    ChatFilter = new ChatFilterCls();
                ChatFilter.OwnerUserId = OwnerUserId;
                ChatFilter.ObjectType = ObjectType;
                ChatFilter.ObjectId = ObjectId;
                ChatCls[] Chats = CoreCallBussinessUtility.CreateBussinessProcess().CreateChatProcess().Reading(ORenderInfo, ChatFilter);

                string Html =
                    "<div class=\"chat-discussion\">\r\n";
                if (Chats.Length > 0)
                {
                    for (int iIndex = 0; iIndex < Chats.Length; iIndex++)
                    {
                        Html +=
                        " <div class=\"chat-message left\"> \r\n" +
                        "     <img style=\"width:60px\" src=\"/themes/img/profile_small.jpg\" alt=\"\"> \r\n" +
                        "     <div class=\"message\"> \r\n" +
                        "         <a class=\"message-author\" style=\"color:green;font-weight:bold;font-size:12px\" > " + Chats[iIndex].SendFromOwnerUserLoginName+ " ("+ Chats[iIndex].SendFromOwnerUserFullName + ") </a> \r\n" +
                        "         <div class=\"message-date\" style=\"margin-left:4px;border:solid 1px silver;background-color:whitesmoke;padding:3px;border-radius:4px; font-weight:bold;color:green;font-size:12px\"> " + Chats[iIndex].SendTime.ToString("dd/MM/yyyy HH:mm") + " <a style=\"color:maroon;font-size:18px;font-weight:bold\" href=\"javascript:RemoveChat('" + ObjectType+"','"+ObjectId + "','" + Chats[iIndex].ChatId + "','" + OwnerUserId + "');\"><i class=\"fa fa-trash-o\"></i></a> </div> \r\n" +
                        "         <div style=\"min-height:40px\" class=\"message-content\"> \r\n" +
                                        Chats[iIndex].Message +
                        "         </div> \r\n" +
                        "     </div> \r\n" +
                        " </div> \r\n";
                    }
                }
                else
                {

                }

                Html += "</div>\r\n";
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
        public static AjaxOut ServerSideDeleteChat(RenderInfoCls ORenderInfo, string ObjectType, string ObjectId, string ChatId, string OwnerUserId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                CoreCallBussinessUtility.CreateBussinessProcess().CreateChatProcess().Delete(ORenderInfo, ChatId, OwnerUserId);
                RetAjaxOut = ServerSideReadingChat(ORenderInfo, ObjectType, ObjectId, OwnerUserId);
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
        public static AjaxOut ServerSideAddChat(
            RenderInfoCls ORenderInfo, 
            string ObjectType,
            string ObjectId, 
            string Subject,
            string ChatContent,
            string SharedOwnerUserIdText)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                if (string.IsNullOrEmpty(ChatContent))
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa nhập nội dung"));
                }
                string[] SharedOwnerUserIds = SharedOwnerUserIdText.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string OwnerId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerId;
                OwnerUserFilterCls
                    OwnerUserFilter=new OwnerUserFilterCls();
                OwnerUserFilter.ActiveOnly=1;
                OwnerUserFilter.OwnerId=OwnerId;
                
                Hashtable hs=new Hashtable();
                OwnerUserCls[]
                    OwnerUsers=CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().Reading(ORenderInfo,OwnerUserFilter);
                Collection<string> 
                    ColOwnerUsers=new Collection<string>{};
                for(int iIndex=0;iIndex<OwnerUsers.Length;iIndex++)
                {
                    if(hs[OwnerUsers[iIndex].OwnerUserId]==null)
                    {
                        hs.Add(OwnerUsers[iIndex].OwnerUserId,OwnerUsers[iIndex].OwnerUserId);
                        ColOwnerUsers.Add(OwnerUsers[iIndex].OwnerUserId);
                    }
                }
                for(int iIndex=0;iIndex<SharedOwnerUserIds.Length;iIndex++)
                {
                    if (hs[SharedOwnerUserIds[iIndex]] == null)
                    {
                        hs.Add(SharedOwnerUserIds[iIndex], SharedOwnerUserIds[iIndex]);
                        ColOwnerUsers.Add(SharedOwnerUserIds[iIndex]);
                    }
                }
                string OwnerUserId=WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                if(hs[OwnerUserId]==null)
                {
                    hs.Add(OwnerUserId,OwnerUserId);
                    ColOwnerUsers.Add(OwnerUserId);
                }

                ChatCls
                    OChat = new ChatCls();
                OChat.ChatId = System.Guid.NewGuid().ToString();
                OChat.Subject = Subject;
                OChat.Message = ChatContent;
                OChat.ObjectId = ObjectId;
                OChat.ObjectType = ObjectType;
                OChat.SendFromOwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OChat.SendTime = System.DateTime.Now;
                OChat.OwnerUserIds = ColOwnerUsers.ToArray();
                
                
                CoreCallBussinessUtility.CreateBussinessProcess().CreateChatProcess().Add(ORenderInfo, OChat);
                RetAjaxOut = ChatCommentUtility.ServerSideReadingChat(ORenderInfo, ObjectType, ObjectId, OwnerUserId);
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
