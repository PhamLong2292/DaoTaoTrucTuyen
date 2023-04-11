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
    public class UpdatePop3EmailUser : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "update.pop3emailuser";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Thiết lập cấu hình email";
            }
        }

        public override string Description
        {
            get
            {
                return "Thiết lập cấu hình email";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(UpdatePop3EmailUser), Page);
        }

        //public override AjaxOut CheckPermission(SiteParam OSiteParam)
        //{
        //    string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
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

                string Pop3EmailUserId = WebEnvironments.Request("pop3emailuserid");
                if (string.IsNullOrEmpty(Pop3EmailUserId))
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tham số không hợp lệ"));
                }
                RetAjaxOut.HtmlContent =
                    WebEnvironments.EncryptJavascript(
                    "<script language=javascript>\r\n" +
                    "   function CallActionUpdate(Pop3EmailUserId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +

                    "       Pop3Name = document.getElementById('txtPop3Name').value;\r\n" +
                    "       Pop3Server = document.getElementById('txtPop3Server').value;\r\n" +
                    "       Pop3Account = document.getElementById('txtPop3Account').value;\r\n" +
                    "       Pop3Password = document.getElementById('txtPop3Password').value;\r\n" +
                    "       Pop3Port = document.getElementById('txtPop3Port').value;\r\n" +
                    "       SSL = parseInt(document.getElementById('drpSelectSSL').value,10);\r\n" +

                    "       SmtpServer = document.getElementById('txtSmtpServer').value;\r\n" +
                    "       SmtpAccount = document.getElementById('txtSmtpAccount').value;\r\n" +
                    "       SmtpPassword = document.getElementById('txtSmtpPassword').value;\r\n" +
                    "       SmtpPort = document.getElementById('txtSmtpPort').value;\r\n" +
                    "       SmtpSSL = parseInt(document.getElementById('drpSelectSmtpSSL').value,10);\r\n" +
                    "       FromAddress = document.getElementById('txtFromAddress').value;\r\n" +
                    "       FromName = document.getElementById('txtFromName').value;\r\n" +
                    "       AutoReply = tinyMCE.get('txtAutoReply').getContent();\r\n" +
                    "       ActiveAutoReply = parseInt(document.getElementById('drpSelectAutoAnswer').value,10);\r\n" +
                    "       Signer = tinyMCE.get('txtSigner').getContent();\r\n" +
                    "       AutoReplyMinutes = document.getElementById('txtAutoReplyMinutes').value;\r\n" +
                    "       Active = parseInt(document.getElementById('drpSelectActive').value,10);\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.UpdatePop3EmailUser.ServerSideUpdate(RenderInfo, Pop3EmailUserId, Pop3Name, Pop3Server, Pop3Account, Pop3Password, Pop3Port, SSL, SmtpServer, SmtpAccount, SmtpPassword, SmtpPort, SmtpSSL, FromAddress, FromName, Signer, AutoReply, ActiveAutoReply, AutoReplyMinutes, Active).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "   }\r\n" +
                    "</script>\r\n") +
                    WebEnvironments.EncryptHtml(
                            ServerSideDrawUpdateForm(ORenderInfo, Pop3EmailUserId).HtmlContent
                        ) +
                        "<script>\r\n" +
                            WebScreen.GetMceEditor("txtAutoReply")+
                            WebScreen.GetMceEditor("txtSigner") +
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
        public static AjaxOut ServerSideDrawUpdateForm(RenderInfoCls ORenderInfo, string Pop3EmailUserId)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                ActionSqlParamCls
                   OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);

                Pop3EmailUserCls
                    OPop3EmailUser = OnlineTourBussinessUtility.CreateBussinessProcess().CreatePop3EmailUserProcess().CreateModel(OActionSqlParam, Pop3EmailUserId);
                if (OPop3EmailUser == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Không tìm thấy cấu hình email"));
                }

                string SelectSslText =
                    "<select id=\"drpSelectSSL\" class=\"form-control\">\r\n"+
                    "   <option value=\"0\">Không sử dụng</option>\r\n"+
                    "   <option value=\"1\">Sử dụng</option>\r\n" +
                    "</select>\r\n";
                if (OPop3EmailUser.SSL == 1)
                {
                    SelectSslText =
                    "<select id=\"drpSelectSSL\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">Không sử dụng</option>\r\n" +
                    "   <option value=\"1\" selected>Sử dụng</option>\r\n" +
                    "</select>\r\n";
                }


                string SelectSmtpSslText =
                   "<select id=\"drpSelectSmtpSSL\" class=\"form-control\">\r\n" +
                   "   <option value=\"0\">Không sử dụng</option>\r\n" +
                   "   <option value=\"1\">Sử dụng</option>\r\n" +
                   "</select>\r\n";
                if (OPop3EmailUser.SmtpSSL == 1)
                {
                    SelectSmtpSslText =
                    "<select id=\"drpSelectSmtpSSL\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">Không sử dụng</option>\r\n" +
                    "   <option value=\"1\" selected>Sử dụng</option>\r\n" +
                    "</select>\r\n";
                }


                string SelectActiveText =
                   "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                   "   <option value=\"0\">Không sử dụng</option>\r\n" +
                   "   <option value=\"1\">Sử dụng</option>\r\n" +
                   "</select>\r\n";
                if (OPop3EmailUser.Active == 1)
                {
                    SelectActiveText =
                    "<select id=\"drpSelectActive\" class=\"form-control\">\r\n" +
                    "   <option value=\"0\">Không sử dụng</option>\r\n" +
                    "   <option value=\"1\" selected>Sử dụng</option>\r\n" +
                    "</select>\r\n";
                }

                
                string SelectAutoAnswerText =
                   "<select id=\"drpSelectAutoAnswer\" class=\"form-control\">\r\n" +
                   "   <option value=\"0\">Không tự động</option>\r\n" +
                   "   <option value=\"1\">Có tự động</option>\r\n" +
                   "</select>\r\n";
                if (OPop3EmailUser.Active == 1)
                {
                    SelectAutoAnswerText =
                       "<select id=\"drpSelectAutoAnswer\" class=\"form-control\">\r\n" +
                       "   <option value=\"0\">Không tự động</option>\r\n" +
                       "   <option selected value=\"1\">Có tự động</option>\r\n" +
                       "</select>\r\n";
                }

                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string BackUrl = WebScreen.BuildUrl(OwnerCode, new EmailSettings().WebPartId);
                string Html =
                       " <div class=\"ibox-content\"> \r\n" +
                       "     <div class=\"row\"> \r\n" +
                       "         <div class=\"col-sm-12\"><h3 class=\"m-t-none m-b\">" + WebLanguage.GetLanguage(OSiteParam, "Thiết lập email inbox") + "</h3> \r\n" +
                       "             <div> \r\n" +
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + Pop3EmailUserId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:window.open('" + BackUrl + "','_self');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button> \r\n" +
                       "                 </div> \r\n" +
                       "            <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên hộp thư") + "</label> <input id=\"txtPop3Name\" value=\"" + OPop3EmailUser.Pop3Name + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tên tài khoản email") + "\" class=\"form-control\"></div> \r\n" +
                       "            <div class=\"row\" style=\"margin-top:10px\">\r\n"+
                       "            <div class=\"col-md-6\">\r\n"+
                       
                       "                 <div class=\"form-group\"><h2><strong>Pop3</strong></h2></div>\r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Máy chủ") + "</label> <input id=\"txtPop3Server\" value=\"" + OPop3EmailUser.Pop3Server + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Máy chủ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tài khoản") + "</label> <input id=\"txtPop3Account\" value=\"" + OPop3EmailUser.Pop3Account + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tài khoản") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mật khẩu") + "</label> <input id=\"txtPop3Password\"type=\"password\" value=\"" + OPop3EmailUser.Pop3Password + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tài khoản") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Cổng") + "</label> <input id=\"txtPop3Port\" value=\"" + OPop3EmailUser.Pop3Port + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Cổng") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "SSL") + "</label> " + SelectSslText + "</div> \r\n" +
                       "            </div>\r\n"+
                       "            <div class=\"col-md-6\">\r\n"+
                       "                 <div class=\"form-group\"><h2><strong>SMTP</strong></h2></div>\r\n"+

                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Máy chủ") + "</label> <input id=\"txtSmtpServer\" value=\"" + OPop3EmailUser.SmtpServer + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Máy chủ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tài khoản") + "</label> <input id=\"txtSmtpAccount\" value=\"" + OPop3EmailUser.SmtpAccount + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tài khoản") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Mật khẩu") + "</label> <input id=\"txtSmtpPassword\"type=\"password\" value=\"" + OPop3EmailUser.SmtpPassword + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Tài khoản") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Cổng") + "</label> <input id=\"txtSmtpPort\" value=\"" + OPop3EmailUser.SmtpPort + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Cổng") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Gửi từ địa chỉ") + "</label> <input id=\"txtFromAddress\" value=\"" + OPop3EmailUser.FromAddress + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ địa chỉ") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tên người gửi") + "</label> <input id=\"txtFromName\" value=\"" + OPop3EmailUser.FromName + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Từ tên") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "SSL") + "</label> " + SelectSmtpSslText + "</div> \r\n" +
                       "            </div>\r\n" +
                       "         </div>\r\n" +//row

                        "         <div class=\"row\">\r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Nội dung trả lời tự động") + "</label> <textarea id=\"txtAutoReply\" class=\"form-control\">" + OPop3EmailUser.AutoReply + "</textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Chữ ký cuối thư") + "</label> <textarea id=\"txtSigner\" class=\"form-control\">" + OPop3EmailUser.Signer + "</textarea></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Tự động trả lời") + "</label> " + SelectAutoAnswerText + "</div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian tự động") + "</label> <input id=\"txtAutoReplyMinutes\" value=\"" + OPop3EmailUser.AutoReplyMinutes + "\" type=\"textbox\" placeholder=\"" + WebLanguage.GetLanguage(OSiteParam, "Thời gian tự động trả lời sau ? phút") + "\" class=\"form-control\"></div> \r\n" +
                       "                 <div class=\"form-group\"><label>" + WebLanguage.GetLanguage(OSiteParam, "Sử dụng") + "</label>" + SelectActiveText + "</div> \r\n" +
                       "         </div>\r\n" +
                       
                       "                 <div> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs mr-5px\" type=\"button\" onclick=\"javascript:CallActionUpdate('" + Pop3EmailUserId + "');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Chấp nhận") + "</strong></button> \r\n" +
                       "                     <button class=\"btn btn-sm btn-primary m-t-n-xs\" type=\"button\" onclick=\"javascript:window.open('"+BackUrl+"','_self');\"><strong>" + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</strong></button> \r\n" +
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
        public static AjaxOut ServerSideUpdate(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string Pop3Name,
            string Pop3Server,
            string Pop3Account,
            string Pop3Password,
            string Pop3Port,
            int SSL,

            string SmtpServer,
            string SmtpAccount,
            string SmtpPassword,
            string SmtpPort,
            int SmtpSSL,
            string FromAddress,
            string FromName,
            string Signer, 
            string AutoReply, 
            int ActiveAutoReply,
            int AutoReplyMinutes,
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
                Pop3EmailUserCls
                    OPop3EmailUser = OnlineTourBussinessUtility.CreateBussinessProcess().CreatePop3EmailUserProcess().CreateModel(OActionSqlParam,  Pop3EmailUserId);

                OPop3EmailUser.Pop3Name = Pop3Name;
                OPop3EmailUser.Pop3Server = Pop3Server;
                OPop3EmailUser.Pop3Account = Pop3Account;
                OPop3EmailUser.Pop3Password = Pop3Password;
                OPop3EmailUser.Pop3Port = Pop3Port;
                OPop3EmailUser.SSL = SSL;

                OPop3EmailUser.SmtpServer = SmtpServer;
                OPop3EmailUser.SmtpAccount = SmtpAccount;
                OPop3EmailUser.SmtpPassword = SmtpPassword;
                OPop3EmailUser.SmtpPort = SmtpPort;
                OPop3EmailUser.SmtpSSL = SSL;
                OPop3EmailUser.FromAddress = FromAddress;
                OPop3EmailUser.FromName = FromName;
                OPop3EmailUser.AutoReply = AutoReply;
                OPop3EmailUser.ActiveAutoReply = ActiveAutoReply;
                OPop3EmailUser.Signer = Signer;
                OPop3EmailUser.AutoReplyMinutes = AutoReplyMinutes;
                OPop3EmailUser.Active = Active;
                
                OnlineTourBussinessUtility.CreateBussinessProcess().CreatePop3EmailUserProcess().Save(OActionSqlParam, OPop3EmailUser);
                RetAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Thiết lập tham số email thành công");
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

        public override bool CheckValid
        {
            get
            {
                string Pop3EmailUserId = WebEnvironments.Request("Pop3EmailUserId");
                if (string.IsNullOrEmpty(Pop3EmailUserId))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
