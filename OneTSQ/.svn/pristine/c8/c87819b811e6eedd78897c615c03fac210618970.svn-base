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
    public class ComposeEmail : WebPartTemplate
    {
        public static string StaticWebPartId
        {
            get
            {
                return "ComposeEmail".ToLower();
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
                return "Soạn email";
            }
        }

        public override string Description
        {
            get
            {
                return "Soạn email";
            }
        }

        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ComposeEmail), Page);
        }

        public override AjaxOut CheckPermission(SiteParam OSiteParam)
        {
            string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            string SessionId = System.Guid.NewGuid().ToString();
            string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                
            bool HasPermission = true;
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }

        public static string GetPluginJavascript(SiteParam OSiteParam)
        {
            string SessionId = System.Guid.NewGuid().ToString();
            string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                
            return
                "<script language=javascript>\r\n" +
                "   function ShowOffCc()\r\n" +
                "   {\r\n" +
                "       if(document.getElementById('divCc').style.display=='none')\r\n" +
                "       {\r\n" +
                "           document.getElementById('divCc').style.display='block';\r\n" +
                "       }\r\n" +
                "       else\r\n" +
                "       {\r\n" +
                "           document.getElementById('divCc').style.display='none';\r\n" +
                "       }\r\n" +
                "   }\r\n" +

                "   function ShowOffBcc()\r\n" +
                "   {\r\n" +
                "       if(document.getElementById('divBcc').style.display=='none')\r\n" +
                "       {\r\n" +
                "           document.getElementById('divBcc').style.display='block';\r\n" +
                "       }\r\n" +
                "       else\r\n" +
                "       {\r\n" +
                "           document.getElementById('divBcc').style.display='none';\r\n" +
                "       }\r\n" +
                "   }\r\n" +


                "   function fileSelected()\r\n" +
                    "   {\r\n" +
                    "        RenderInfo=CreateRenderInfo();\r\n" +
                    "        document.getElementById('divProcessingSentEmail').innerHTML='" + WebLanguage.GetLanguage(OSiteParam, "Đang upload...") + "';\r\n" +
                    "        setTimeout('RealFileSelected()',10);\r\n" +
                    "   }\r\n" +

                    "   function RealFileSelected()\r\n" +
                    "   {\r\n" +
                    "        RenderInfo=CreateRenderInfo();\r\n" +
                    "        document.getElementById('divProcessingSentEmail').innerHTML='" + WebLanguage.GetLanguage(OSiteParam, "Đang upload...") + "';\r\n" +
                    "        var file = document.getElementById('fileToUpload').files[0];\r\n" +
                    "        if (file) {\r\n" +
                //"            document.getElementById('divFileName').innerHTML = '" + WebLanguage.GetLanguage(OSiteParam, "Tên tệp") + "  : '+file.name;\r\n" +
                //"            document.getElementById('divFileType').innerHTML = '" + WebLanguage.GetLanguage(OSiteParam, "Kiểu tệp") + " : '+file.type;\r\n" +
                //"            document.getElementById('divFileSize').innerHTML = '" + WebLanguage.GetLanguage(OSiteParam, "Kích cỡ ") + " : '+file.size + 'bytes';\r\n" +


                    "        fileUploadValue=document.getElementById('fileToUpload').value;\r\n" +

                    "        var fd = new FormData();\r\n" +
                    "        fd.append(\"fileToUpload\", document.getElementById('fileToUpload').files[0]);\r\n" +
                    "        var xhr = new XMLHttpRequest();\r\n" +
                    "        xhr.addEventListener(\"load\", uploadComplete, false);\r\n" +
                    "        xhr.open(\"POST\", \"" + WebConfig.GetUploadHandler(OSiteParam, SessionId, OwnerUserId, LoginName) + "\");\r\n" +
                    "        xhr.send(fd);\r\n" +

                    "        }\r\n" +

                    "   }\r\n" +


                    " function uploadComplete(evt) {\r\n" +
                    "    /* This event is raised when the server send back a response */\r\n" +
                    "    RenderInfo=CreateRenderInfo();\r\n" +
                    "    XmlInfoResult=evt.target.responseText;\r\n" +
                    "    Xml = document.getElementById('txtXml').value;\r\n" +
                    "    XmlSchema = document.getElementById('txtXmlSchema').value;\r\n" +
                    "    AjaxOut = OnlineTour.WebParts.ComposeEmail.ServerSideUploadFile(RenderInfo, XmlInfoResult, Xml, XmlSchema).value;\r\n" +
                    "    document.getElementById('divProcessingSentEmail').innerHTML='" + WebLanguage.GetLanguage(OSiteParam, "Upload file gắn kèm xong") + "';\r\n" +
                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        alert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "    document.getElementById('txtXml').value=AjaxOut.RetExtraParam1;\r\n" +
                    "    document.getElementById('txtXmlSchema').value=AjaxOut.RetExtraParam2;\r\n" +
                    "    document.getElementById('divListAttachedFile').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    " }\r\n" +

                    "   function RemoveFile(Id)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       Xml = document.getElementById('txtXml').value;\r\n" +
                    "       XmlSchema = document.getElementById('txtXmlSchema').value;\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.ComposeEmail.ServerSideRemoveFile(RenderInfo, Xml, XmlSchema, Id).value;\r\n" +
                    "       document.getElementById('txtXml').value=AjaxOut.RetExtraParam1;\r\n" +
                    "       document.getElementById('txtXmlSchema').value=AjaxOut.RetExtraParam2;\r\n" +
                    "       document.getElementById('divListAttachedFile').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +


                    "   function DeleteDraft(Pop3EmailUserId, EmailId)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    
                    "       AjaxOut = OnlineTour.WebParts.ComposeEmail.ServerSideDeleteDraft(RenderInfo, Pop3EmailUserId, EmailId).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           document.getElementById('divProcessingSentEmail').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl,'_self');\r\n"+
                    "   }\r\n" +

                    "   function SendEmail(SrcPop3EmailUserId, SrcEmailId, Pop3EmailUserId, EmailId, SaveDraft)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       document.getElementById('divProcessingSentEmail').innerHTML='" + WebLanguage.GetLanguage(OSiteParam, "Đang gửi email vui lòng đợi...") + "';\r\n" +
                    "       setTimeout('RealSendEmail(\"'+SrcPop3EmailUserId+'\",\"'+SrcEmailId+'\",\"'+Pop3EmailUserId+'\",\"'+EmailId+'\", '+SaveDraft+')',10);\r\n" +
                    "   }\r\n" +

                    "   function RealSendEmail(SrcPop3EmailUserId, SrcEmailId, Pop3EmailUserId, EmailId, SaveDraft)\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       Subject = document.getElementById('txtSubject').value;\r\n" +
                    "       Body = tinyMCE.get('txtBody').getContent();\r\n" +
                    "       To = document.getElementById('txtTo').value;\r\n" +
                    "       Cc = document.getElementById('txtCc').value;\r\n" +
                    "       Bcc = document.getElementById('txtBcc').value;\r\n" +
                    "       Xml = document.getElementById('txtXml').value;\r\n" +
                    "       XmlSchema = document.getElementById('txtXmlSchema').value;\r\n" +
                    "       AjaxOut = OnlineTour.WebParts.ComposeEmail.ServerSideSendEmail(RenderInfo, SrcPop3EmailUserId, SrcEmailId, Pop3EmailUserId, EmailId, Subject, Body, To, Cc, Bcc, Xml, XmlSchema, SaveDraft).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           document.getElementById('divProcessingSentEmail').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       document.getElementById('divProcessingSentEmail').innerHTML='';\r\n" +
                    "       alert(AjaxOut.InfoMessage);\r\n" +
                    "       if(SaveDraft==0)CallBackFromComposeEmail();\r\n" +
                    "       if(AjaxOut.RetNumber==1)window.open(AjaxOut.RetUrl,'_self');\r\n" +
                    "   }\r\n" +


                    "</script>\r\n";
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

                string SrcPop3EmailUserId= (string)WebEnvironments.Request("SrcPop3EmailUserId");
                string SrcEmailId = (string)WebEnvironments.Request("SrcEmailId");
                string ReplyType = (string)WebEnvironments.Request("ReplyType");
                if (string.IsNullOrEmpty(ReplyType)) ReplyType = "0";

                string EmailId = (string)WebEnvironments.Request("EmailId");
                string Pop3EmailUserId = (string)WebEnvironments.Request("Pop3EmailUserId");
                if (string.IsNullOrEmpty(Pop3EmailUserId))
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tham số hộp thư không tìm thấy"));
                }

                string SessionId = System.Guid.NewGuid().ToString();
                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string BackUrl = WebScreen.BuildUrl(OwnerCode, UserWorkingMailBox.StaticWebPartId, new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("type","inbox"),
                });

                RetAjaxOut.HtmlContent =
                    GetPluginJavascript(OSiteParam) +
                    "<script>\r\n"+
                    "   function CallBackFromComposeEmail()\r\n"+
                    "   {\r\n"+
                    "       window.open('"+BackUrl+"','_self');\r\n"+
                    "   }\r\n"+
                    "</script>\r\n"+
                    "<div id=\"divComposeEmail\"></div>\r\n" +
                    "<div id=\"divSearchContent\"></div>\r\n" +
                    "<div id=\"divComposeEmailContent\">\r\n" +
                        ServerSideComposeEmail(ORenderInfo, SrcPop3EmailUserId, SrcEmailId, Pop3EmailUserId, null, int.Parse(ReplyType)).HtmlContent +
                    "</div>\r\n" +
                    "<script>\r\n" +
                       WebScreen.GetMceEditor("txtBody", 400) +
                    "     $('.tagsinput').tagsinput({ "+
                    "       tagClass: 'label label-primary'"+
                    "     });\r\n"+
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
        public static AjaxOut ServerSideComposeEmail(
            RenderInfoCls ORenderInfo,
            string SrcPop3EmailUserId,
            string SrcEmailId,
            string Pop3EmailUserId,
            string EditEmailId,
            int ReplyType)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                if (string.IsNullOrEmpty(EditEmailId)) EditEmailId = "";
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                Pop3EmailUserCls
                    OPop3EmailUser = OnlineTourBussinessUtility.CreateBussinessProcess().CreatePop3EmailUserProcess().CreateModel(OActionSqlParam, Pop3EmailUserId);
                if (OPop3EmailUser == null) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Không có thông tin hộp thư cá nhân"));
                string Subject = "";
                string Body = OPop3EmailUser.Signer;
                string ToAddress = "";
                string CcAddress = "";
                string BccAddress = "";
                string ParentPop3UserId = "";
                
                if (!string.IsNullOrEmpty(SrcEmailId) && !string.IsNullOrEmpty(SrcPop3EmailUserId))
                {
                    EmailCls
                        OEmail = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CreateModel(OActionSqlParam, SrcPop3EmailUserId, SrcEmailId);
                    Subject = "Re: " + OEmail.Subject;
                    Body = "<br><br>" + WebLanguage.GetLanguage(OSiteParam, "Vào ngày ") + OEmail.DateInfo + " " + OEmail.SentFromInfo + " " + WebLanguage.GetLanguage(OSiteParam, "đã viết:") + "<br>" + OEmail.Body;
                    ToAddress = OEmail.SentFromAddress;
                    ParentPop3UserId = OEmail.frkPop3EmailUserId;
                }

                string XmlAttached="";
                string XmlAttachedSchema = "";

                if (!string.IsNullOrEmpty(EditEmailId))
                {
                    DataSet ds = CreateDataset(XmlAttached, XmlAttachedSchema);

                    //ds.Tables["info"].Columns.Add("Id");
                    //ds.Tables["info"].Columns.Add("FileName");
                    //ds.Tables["info"].Columns.Add("SaveFile");
                    //ds.Tables["info"].Columns.Add("Url");
                    //ds.Tables["info"].Columns.Add("AddedDate", typeof(DateTime));
                    EmailAttachedFileCls[]
                        EmailAttachedFiles = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().ReadingEmailAttachedFiles(OActionSqlParam, Pop3EmailUserId, EditEmailId);
                    string EmailAttachedRootUrl = WebConfig.GetEmailAttachedRoot();
                    for (int iIndex = 0; iIndex < EmailAttachedFiles.Length; iIndex++)
                    {
                        string Url =
                            EmailAttachedRootUrl + "/" + EmailAttachedFiles[iIndex].OwnerLoginName + "/" + EmailAttachedFiles[iIndex].MailBox + "/" + EmailAttachedFiles[iIndex].AttachedMonth.ToString() + "." + EmailAttachedFiles[iIndex].AttachedYear.ToString() + "/" + EmailAttachedFiles[iIndex].EmailAttachedFileId + new System.IO.FileInfo(EmailAttachedFiles[iIndex].FileName).Extension;
                        DataRow NewRow = ds.Tables["info"].NewRow();
                        NewRow["id"] = EmailAttachedFiles[iIndex].EmailAttachedFileId;
                        NewRow["Url"] = Url;
                        NewRow["FileName"] = EmailAttachedFiles[iIndex].FileName;
                        NewRow["SaveFile"] = EmailAttachedFiles[iIndex].SaveFile;
                        NewRow["AddedDate"] = EmailAttachedFiles[iIndex].AttachedDate;
                        NewRow["Status"] = 1;//edit....!

                        ds.Tables["info"].Rows.Add(NewRow);
                    }
                    XmlAttached = ds.GetXml();
                    XmlAttachedSchema = ds.GetXmlSchema();
                    EmailCls
                        OEditEmail = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CreateModel(OActionSqlParam, Pop3EmailUserId, EditEmailId);
                    Subject = OEditEmail.Subject;
                    Body = OEditEmail.Body;



                    EmailAddressInfoCls[]
                        EmailAddressInfos = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().ReadingEmailAddressInfos(OActionSqlParam, Pop3EmailUserId, EditEmailId, null);
                    for (int iIndex = 0; iIndex < EmailAddressInfos.Length; iIndex++)
                    {
                        if (EmailAddressInfos[iIndex].AddressType.Equals("to"))
                        {
                            if (!string.IsNullOrEmpty(ToAddress))
                            {
                                ToAddress += ";";
                            }
                            ToAddress = ToAddress + EmailAddressInfos[iIndex].Address;
                        }

                        if (EmailAddressInfos[iIndex].AddressType.Equals("cc"))
                        {
                            if (!string.IsNullOrEmpty(ToAddress))
                            {
                                CcAddress += ";";
                            }
                            CcAddress = CcAddress + EmailAddressInfos[iIndex].Address;
                        }

                        if (EmailAddressInfos[iIndex].AddressType.Equals("bcc"))
                        {
                            if (!string.IsNullOrEmpty(BccAddress))
                            {
                                BccAddress += ";";
                            }
                            BccAddress = BccAddress + EmailAddressInfos[iIndex].Address;
                        }
                    }
                }
                else
                {
                    if (ReplyType == 1)
                    {
                        EmailAddressInfoCls[]
                            EmailAddressInfos = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().ReadingEmailAddressInfos(OActionSqlParam, SrcPop3EmailUserId, SrcEmailId, null);
                        for (int iIndex = 0; iIndex < EmailAddressInfos.Length; iIndex++)
                        {
                            if (EmailAddressInfos[iIndex].AddressType.Equals("to"))
                            {
                                if (!EmailAddressInfos[iIndex].Address.Equals(ToAddress) && !EmailAddressInfos[iIndex].Address.Equals(OPop3EmailUser.Pop3Account))
                                {
                                    if (!string.IsNullOrEmpty(ToAddress))
                                    {
                                        ToAddress += ";";
                                    }
                                    ToAddress = ToAddress + EmailAddressInfos[iIndex].Address;
                                }
                            }

                            if (EmailAddressInfos[iIndex].AddressType.Equals("cc"))
                            {
                                if (!EmailAddressInfos[iIndex].Address.Equals(ToAddress) && !EmailAddressInfos[iIndex].Address.Equals(OPop3EmailUser.Pop3Account))
                                {
                                    if (!string.IsNullOrEmpty(ToAddress))
                                    {
                                        CcAddress += ";";
                                    }
                                    CcAddress = CcAddress + EmailAddressInfos[iIndex].Address;
                                }
                            }

                            if (EmailAddressInfos[iIndex].AddressType.Equals("bcc") && !EmailAddressInfos[iIndex].Address.Equals(OPop3EmailUser.Pop3Account))
                            {
                                if (!EmailAddressInfos[iIndex].Address.Equals(ToAddress))
                                {
                                    if (!string.IsNullOrEmpty(BccAddress))
                                    {
                                        BccAddress += ";";
                                    }
                                    BccAddress = BccAddress + EmailAddressInfos[iIndex].Address;
                                }
                            }
                        }
                    }
                }


                string Html =
                        "   <textarea id=\"txtXml\" style=\"display:none\">" + XmlAttached + "</textarea>\r\n" +
                        "   <textarea id=\"txtXmlSchema\" style=\"display:none\">" + XmlAttachedSchema + "</textarea>\r\n" +
                        "        <div> \r\n" +
                        "             <div class=\"mail-box-header\"> \r\n" +
                        "                 <div class=\"pull-right tooltip-demo\"> \r\n" +
                        "                     <a href=\"javascript:SendEmail('" + SrcPop3EmailUserId + "','" + SrcEmailId + "','" + Pop3EmailUserId + "','" + EditEmailId + "',1);\" class=\"btn btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Lưu nháp\"><i class=\"fa fa-save\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Lưu nháp") + "</a>\r\n";
                if (!string.IsNullOrEmpty(EditEmailId))
                {
                    Html+=
                        "   <a href=\"javascript:DeleteDraft('" + Pop3EmailUserId + "','" + EditEmailId + "',1);\" class=\"btn btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Lưu nháp\"><i class=\"fa fa-save\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Xóa nháp") + "</a>\r\n";
                }
                Html+=
                        "                     <a href=\"javascript:ShowOffCc();\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Hiển thị gửi cc\"><i class=\"fa fa-users\"></i> " + WebLanguage.GetLanguage(OSiteParam, "cc") + "</a>\r\n" +
                        "                     <a href=\"javascript:ShowOffBcc();\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Hiển thị gửi bcc\"><i class=\"fa fa-user\"></i>" + WebLanguage.GetLanguage(OSiteParam, "bcc") + "</a>\r\n" +

                        "                     <a href=\"#.\" class=\"btn btn-white btn-sm btn-file\" data-toggle=\"tooltip\" data-placement=\"top\" ><i class=\"fa fa-paperclip\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Gắn kèm") + " <input type=\"file\" id=\"fileToUpload\" name=\"fileToUpload\" onchange=\"fileSelected();\"></a> \r\n" +


                        "                     <a href=\"javascript:SendEmail('" + SrcPop3EmailUserId + "','" + SrcEmailId + "','" + Pop3EmailUserId + "','"+EditEmailId+"',0);\" class=\"btn btn-sm btn-primary\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Gửi thử\"><i class=\"fa fa-paper-plane\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Gửi thư") + "</a>\r\n" +
                        "                     <a href=\"javascript:CallBackFromComposeEmail();\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Quay lại\"><i class=\"fa fa-backward\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</a> \r\n" +
                        "                 </div> \r\n" +
                        "                 <div id=\"divProcessingSentEmail\" style=\"font-weight:bold;color:maroon\"></div>\r\n" +
                        "                 <h2> \r\n" +
                        "                     " + WebLanguage.GetLanguage(OSiteParam, "Gửi thư") + " \r\n" +
                        "                 </h2> \r\n" +
                        "             </div> \r\n" +
                        
                        "                 <div class=\"mail-box\"> \r\n" +


                        "                 <div class=\"mail-body\"> \r\n" +

                        "                     <form class=\"form-horizontal\" method=\"get\"> \r\n" +

                        "                         <div class=\"form-group\"><label class=\"col-sm-2 control-label\">Subject:</label> \r\n" +
                        "                             <div class=\"col-sm-10\"><input type=\"text\" class=\"form-control\" value=\"" + Subject + "\" id=\"txtSubject\"></div> \r\n" +
                        "                         </div> \r\n" +

                        "                         <div class=\"form-group\"><label class=\"col-sm-2 control-label\">To:</label> \r\n" +
                        "                             <div class=\"col-sm-10\"><input type=\"text\" class=\"form-control tagsinput\" id=\"txtTo\" value=\"" + ToAddress + "\"></div> \r\n" +
                        "                         </div> \r\n" +

                        "                         <div id=divCc class=\"form-group\" style=\"display:none\"><label class=\"col-sm-2 control-label\">Cc:</label> \r\n" +
                        "                             <div class=\"col-sm-10\"><input type=\"text\" class=\"form-control tagsinput\" id=\"txtCc\" value=\"" + CcAddress + "\"></div> \r\n" +
                        "                         </div> \r\n" +

                        "                         <div id=divBcc class=\"form-group\" style=\"display:none\"><label class=\"col-sm-2 control-label\">Bcc:</label> \r\n" +
                        "                             <div class=\"col-sm-10\"><input type=\"text\" class=\"form-control tagsinput\" id=\"txtBcc\" value=\"" + BccAddress + "\"></div> \r\n" +
                        "                         </div> \r\n" +



                        "                         </form> \r\n" +

                        "                 </div> \r\n" +
                        "                 <div class=\"mail-attachment\" id=\"divListAttachedFile\">" + ServerSideDrawAttachFile(ORenderInfo, XmlAttached, XmlAttachedSchema).HtmlContent + "</div> \r\n" +
                        "                     <div class=\"mail-text h-200\"> \r\n" +

                        "                         <textarea class=\"textarea\" id=\"txtBody\">" + Body + "</textarea> \r\n" +
                        "                         <div class=\"clearfix\"></div> \r\n" +
                        "                     </div> \r\n" +

                        

                    //    "                     <div class=\"mail-body text-right tooltip-demo\"> \r\n" +
                    ////"                     <a href=\"#.\" class=\"btn btn-white btn-sm btn-file\" data-toggle=\"tooltip\" data-placement=\"top\" ><i class=\"fa fa-paperclip\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Gắn kèm") + " <input type=\"file\" id=\"fileToUpload\" name=\"fileToUpload\" onchange=\"fileSelected();\"></a> \r\n" +
                    ////"                         <a href=\"#.\" class=\"btn btn-sm btn-primary\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Send\"><i class=\"fa fa-reply\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Gửi thư") + "</a>\r\n" +
                    //    "                         <a href=\"javascript:CallBackFromComposeEmail();\" class=\"btn btn-white btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Move to draft folder\"><i class=\"fa fa-pencil\"></i> " + WebLanguage.GetLanguage(OSiteParam, "Quay lại") + "</a> \r\n" +
                    //    "                     </div> \r\n" +
                        "                     <div class=\"clearfix\"></div> \r\n" +
                        "                 </div> \r\n" +
                        "             </div> \r\n";
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

       

        public static DataSet CreateDataset(string Xml, string XmlSchema)
        {
            DataSet ds = new DataSet("xml");
            if (string.IsNullOrEmpty(Xml))
            {
                ds.Tables.Add("info");
                ds.Tables["info"].Columns.Add("Id");
                ds.Tables["info"].Columns.Add("FileName");
                ds.Tables["info"].Columns.Add("SaveFile");
                ds.Tables["info"].Columns.Add("Url");
                ds.Tables["info"].Columns.Add("AddedDate", typeof(DateTime));
                ds.Tables["info"].Columns.Add("Status",typeof(int));
            }
            else
            {
                ds.ReadXmlSchema(new StringReader(XmlSchema));
                ds.ReadXml(new StringReader(Xml));
            }
            return ds;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDrawAttachFile(
            RenderInfoCls ORenderInfo,
            string Xml,
            string XmlSchema)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                if (string.IsNullOrEmpty(Xml))
                {
                    RetAjaxOut.HtmlContent = "";
                    return RetAjaxOut;
                }

                DataSet ds = CreateDataset(Xml, XmlSchema);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    RetAjaxOut.HtmlContent = "";
                    return RetAjaxOut;
                }
                string Html =
                            " <div class=\"attachment\"> \r\n"+
                            "         <div class=\"table-responsive\"> \r\n" +
                            "             <table class=\"table table-striped\"> \r\n" +
                            "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < ds.Tables[0].Rows.Count; iIndex++)
                {
                    string Id = XmlUtility.GetString(ds.Tables[0].Rows[iIndex], "Id", true);
                    string FileName = XmlUtility.GetString(ds.Tables[0].Rows[iIndex], "FileName", true);
                    DateTime AddedDate = XmlUtility.GetDate(ds.Tables[0].Rows[iIndex], "AddedDate", true);
                    string Url = XmlUtility.GetString(ds.Tables[0].Rows[iIndex], "Url", true);
                    int Status = XmlUtility.GetInt(ds.Tables[0].Rows[iIndex], "Status", true);
                    if (Status != 2)
                    {
                        Html +=
                              "                 <tr> \r\n" +
                              "                     <td class=\"td-right\">" + (iIndex + 1).ToString("#,##0") + "</td> \r\n" +
                              "                     <td>" + FileName + "</td>\r\n" +
                              "                     <td style=\"width:30px\"><a href=\"" + Url + "\" target=\"_blank\">Xem</a></td> \r\n" +
                              "                     <td style=\"width:30px\"><a href=\"javascript:RemoveFile('" + Id + "');\">Xóa</a></td> \r\n" +
                              "                 </tr> \r\n";
                    }
                }
                Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "       </div>\r\n"+
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
        public static AjaxOut ServerSideUploadFile(RenderInfoCls ORenderInfo, string XmlResultInfo, string Xml, string XmlSchema)
        {
            AjaxOut OAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                UploadHandlerResultCls
                    OUploadHandlerResult = UploadHandlerResultParser.ParseFromXml(XmlResultInfo);
                if (OUploadHandlerResult.Error) throw new Exception(OUploadHandlerResult.InfoMessage);
                string SaveFile = OUploadHandlerResult.SaveFile;
                string Url = OUploadHandlerResult.UploadUrl;
                string Id = System.Guid.NewGuid().ToString();
                DataSet ds = CreateDataset(Xml, XmlSchema);
                ds.Tables[0].Rows.Add(new object[]{
                     Id,OUploadHandlerResult.UploadFileName,SaveFile,Url, System.DateTime.Now,0
                 });

                OAjaxOut.RetExtraParam1 = ds.GetXml();
                OAjaxOut.RetExtraParam2 = ds.GetXmlSchema();
                OAjaxOut = ServerSideDrawAttachFile(ORenderInfo, OAjaxOut.RetExtraParam1, OAjaxOut.RetExtraParam2);
                OAjaxOut.RetExtraParam1 = ds.GetXml();
                OAjaxOut.RetExtraParam2 = ds.GetXmlSchema();
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
        public static AjaxOut ServerSideRemoveFile(
            RenderInfoCls ORenderInfo,
            string Xml,
            string XmlSchema,
            string Id)
        {
            AjaxOut OAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);

                DataSet ds = CreateDataset(Xml, XmlSchema);
                DataView dv = new DataView(ds.Tables[0], "Id='" + Id + "'", "", DataViewRowState.CurrentRows);
                if (dv.Count > 0)
                {
                    string SaveFile = (string)dv[0]["SaveFile"];
                    if (System.IO.File.Exists(SaveFile))
                    {
                        try
                        {
                            System.IO.File.Delete(SaveFile);
                        }
                        catch { }
                    }
                    dv[0]["Status"] = 2;
                    //dv.Delete(0);
                }

                OAjaxOut.RetExtraParam1 = ds.GetXml();
                OAjaxOut.RetExtraParam2 = ds.GetXmlSchema();
                OAjaxOut = ServerSideDrawAttachFile(ORenderInfo, OAjaxOut.RetExtraParam1, OAjaxOut.RetExtraParam2);
                OAjaxOut.RetExtraParam1 = ds.GetXml();
                OAjaxOut.RetExtraParam2 = ds.GetXmlSchema();
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
        public static AjaxOut ServerSideSendEmail(
            RenderInfoCls ORenderInfo,
            string SrcPop3EmailUserId,
            string SrcEmailId,
            string Pop3EmailUserId,
            string EmailId,
            string Subject,
            string Body,
            string To,
            string Cc,
            string Bcc,
            string Xml,
            string XmlSchema,
            int SaveDraft)
        {
            AjaxOut OAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                    ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string OwnerUserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                DataSet ds = CreateDataset(Xml, XmlSchema);

                if (SaveDraft == 0)
                {
                    if (string.IsNullOrEmpty(Subject))
                    {
                        throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa có tiêu đề thư"));
                    }


                    if (string.IsNullOrEmpty(To))
                    {
                        throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa xác định người gửi"));
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Subject))
                    {
                        Subject = "NO SUBJECT";
                    }
                }

                string[] ItemTo = To.Split(new char[] { ';',',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int iCheck = 0; iCheck < ItemTo.Length; iCheck++)
                {
                    ItemTo[iCheck] = ItemTo[iCheck].Trim();
                    if (FunctionUtilities.IsValidEmail(ItemTo[iCheck]) == false)
                    {
                        throw new Exception(ItemTo[iCheck] + " " + WebLanguage.GetLanguage(OSiteParam, "không hợp lệ"));
                    }
                }


                string[] ItemCc = Cc.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int iCheck = 0; iCheck < ItemCc.Length; iCheck++)
                {
                    ItemCc[iCheck] = ItemCc[iCheck].Trim();
                    if (FunctionUtilities.IsValidEmail(ItemCc[iCheck]) == false)
                    {
                        throw new Exception(ItemCc[iCheck] + " " + WebLanguage.GetLanguage(OSiteParam, "không hợp lệ"));
                    }
                }

                string[] ItemBcc = Bcc.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int iCheck = 0; iCheck < ItemBcc.Length; iCheck++)
                {
                    ItemBcc[iCheck] = ItemBcc[iCheck].Trim();
                    if (FunctionUtilities.IsValidEmail(ItemBcc[iCheck]) == false)
                    {
                        throw new Exception(ItemBcc[iCheck] + " " + WebLanguage.GetLanguage(OSiteParam, "không hợp lệ"));
                    }
                }
                //string SmtpEmailUserId=WebSessionUtility.GetCurrentLoginUser(OSiteParam).UserId;
                Pop3EmailUserCls
                    OPop3EmailUser = OnlineTourBussinessUtility.CreateBussinessProcess().CreatePop3EmailUserProcess().CreateModel(ActionSqlParam, Pop3EmailUserId);
                if (OPop3EmailUser == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Chưa thiết lập cấu hình gửi email"));
                }

                string SentFromAddress = WebSessionUtility.GetCurrentLoginUser(OSiteParam).Email;
                string SentFromName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;

                EmailCls
                    SrcEmail = null;
                if (!string.IsNullOrEmpty(SrcPop3EmailUserId) && !string.IsNullOrEmpty(SrcEmailId))
                {
                    SrcEmail = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CreateModel(ActionSqlParam, SrcPop3EmailUserId, SrcEmailId);
                }
                EmailCls OEmail = new EmailCls();
                if (!string.IsNullOrEmpty(EmailId))
                {
                    OEmail = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().CreateModel(ActionSqlParam, Pop3EmailUserId, EmailId);
                    OEmail.Subject = Subject;
                    OEmail.Body = Body;
                    OEmail.SentFromAddress = SentFromAddress;
                    OEmail.SentFromName = SentFromName;
                    OEmail.ItemTo = ItemTo;
                    OEmail.ItemCc = ItemCc;
                    OEmail.ItemBcc = ItemBcc;
                    OEmail.Draft = SaveDraft;
                }
                else
                {
                    OEmail.EmailId = System.Guid.NewGuid().ToString();
                    OEmail.MessageId = OEmail.EmailId;
                    OEmail.UIId = OEmail.EmailId;
                    OEmail.UIId = OEmail.EmailId;
                    OEmail.SrcEmailId = SrcEmailId;
                    OEmail.SrcPop3EmailUserId = SrcPop3EmailUserId;
                    OEmail.Subject = Subject;
                    OEmail.Body = Body;
                    OEmail.TotalAttached = 0;
                    OEmail.DateSent = System.DateTime.Now;
                    OEmail.SentEmail = 1;
                    OEmail.DateInfo = OEmail.DateSent.ToString("dd/MM/yyyy HH:mm:ss");
                    OEmail.MailImportant = "normal";
                    OEmail.SentFromAddress = SentFromAddress;
                    OEmail.SentFromName = SentFromName;
                    OEmail.AssignedDate = OEmail.DateSent;
                    OEmail.CreatedOwnerUserId = OwnerUserId;
                    OEmail.ProcessOwnerUserId = OwnerUserId;
                    OEmail.ProcessPop3EmailDate = OEmail.DateSent;
                    OEmail.AssignToOwnerUserId = OwnerUserId;
                    OEmail.Closed = 0;
                    OEmail.ItemTo = ItemTo;
                    OEmail.ItemCc = ItemCc;
                    OEmail.ItemBcc = ItemBcc;
                    OEmail.Source = "app";
                    OEmail.Draft = SaveDraft;
                }
                if (SrcEmail != null)
                {
                    OEmail.frkComeFromOwnerUserId = SrcEmail.frkComeFromOwnerUserId;
                    if (string.IsNullOrEmpty(OEmail.frkComeFromOwnerUserId))
                    {
                        OEmail.frkComeFromOwnerUserId = null;
                    }
                }
                OEmail.AttachXml = Xml;
                OEmail.AttachXmlSchema = XmlSchema;
                OEmail.frkPop3EmailUserId = Pop3EmailUserId;
                OEmail.EmailAttachedFilePath = WebConfig.GetEmailAttachedPath();
                if (string.IsNullOrEmpty(EmailId))
                {
                    OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().Add(ActionSqlParam, OEmail, OPop3EmailUser);
                }
                else
                {
                    OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().Save(ActionSqlParam, EmailId, OEmail);
                }
                if (SaveDraft == 0)
                {
                    OAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Chuyển email ra hàng đợi gửi thành công");
                }
                else
                {
                    OAjaxOut.InfoMessage = WebLanguage.GetLanguage(OSiteParam, "Cập nhật bản draft email thành công");
                    if (string.IsNullOrEmpty(EmailId))
                    {
                        OAjaxOut.RetNumber=1;
                        EmailId = OEmail.EmailId;
                        OAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, ComposeEmail.StaticWebPartId, new WebParamCls[]
                        {
                            new WebParamCls("SrcPop3EmailUserId",SrcPop3EmailUserId),
                            new WebParamCls("SrcEmailId",SrcEmailId),
                            new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                            new WebParamCls("EmailId",EmailId),
                        });
                    }
                }
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
        public static AjaxOut ServerSideDeleteDraft(
            RenderInfoCls ORenderInfo,
            string Pop3EmailUserId,
            string EmailId)
        {
            AjaxOut OAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSessionUtility.CheckSessionTimeOut(OSiteParam);
                ActionSqlParamCls
                  ActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string OwnerCode = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode;
                OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().Delete(ActionSqlParam, Pop3EmailUserId, EmailId);
                OAjaxOut.RetUrl = WebScreen.BuildUrl(OwnerCode, UserWorkingMailBox.StaticWebPartId, new WebParamCls[]
                {
                    new WebParamCls("Pop3EmailUserId",Pop3EmailUserId),
                    new WebParamCls("type","draft"),
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
    }

}

