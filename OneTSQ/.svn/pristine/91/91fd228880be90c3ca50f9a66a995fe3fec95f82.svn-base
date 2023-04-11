using OneTSQ.Core.Model;
using OneTSQ.TempService;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OneTSQ.UploadUtility
{
    public class UploadMediaToServerUtility
    {

        public static void RegisterAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page WebPage)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(UploadMediaToServerUtility), WebPage);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(WebEnvironments), WebPage);
        }




        public static AjaxOut Draw(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

                string SessionId = System.Guid.NewGuid().ToString();
                string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;

                RetAjaxOut.HtmlContent =
                    WebEnvironments.ProcessJavascript(
                    "<script language=javascript>\r\n" +
                    WebEnvironments.GetMainJavascript(OSiteParam, ORenderInfo) +

                    "   function fileImageSelected()\r\n" +
                    "   {\r\n" +
                    "        RenderInfo=CreateRenderInfo();\r\n" +
                    "        document.getElementById('divProcessingUploadImage').innerHTML='" + WebLanguage.GetLanguage(OSiteParam, "Đang upload...") + "';\r\n" +
                    "        setTimeout('RealFileImageSelected()',10);\r\n" +
                    "   }\r\n" +

                    "   function RealFileImageSelected()\r\n" +
                    "   {\r\n" +
                    "        RenderInfo=CreateRenderInfo();\r\n" +
                    "        document.getElementById('divProcessingUploadImage').innerHTML='" + WebLanguage.GetLanguage(OSiteParam, "Đang upload...") + "';\r\n" +
                    "        var file = document.getElementById('ImageUpload').files[0];\r\n" +
                    "        if (file) {\r\n" +
                    //"            document.getElementById('divFileName').innerHTML = '" + WebLanguage.GetLanguage(OSiteParam, "Tên tệp") + "  : '+file.name;\r\n" +
                    //"            document.getElementById('divFileType').innerHTML = '" + WebLanguage.GetLanguage(OSiteParam, "Kiểu tệp") + " : '+file.type;\r\n" +
                    //"            document.getElementById('divFileSize').innerHTML = '" + WebLanguage.GetLanguage(OSiteParam, "Kích cỡ ") + " : '+file.size + 'bytes';\r\n" +


                    "        fileUploadValue=document.getElementById('ImageUpload').value;\r\n" +

                    "        var fd = new FormData();\r\n" +
                    "        fd.append(\"fileToUpImageUploadloadAvatar\", document.getElementById('ImageUpload').files[0]);\r\n" +
                    "        var xhr = new XMLHttpRequest();\r\n" +
                    "        xhr.addEventListener(\"load\", uploadCompleteImage, false);\r\n" +
                    "        xhr.open(\"POST\", \"" + WebConfig.GetUploadHandler(OSiteParam, SessionId, UserId, LoginName) + "\");\r\n" +
                    "        xhr.send(fd);\r\n" +

                    "        }\r\n" +

                    "   }\r\n" +


                    " function uploadCompleteImage(evt) {\r\n" +
                    "    /* This event is raised when the server send back a response */\r\n" +
                    "    RenderInfo=CreateRenderInfo();\r\n" +
                    "    XmlInfoResult=evt.target.responseText;\r\n" +

                    "    AjaxOut = OneTSQ.UploadUtility.UploadMediaToServerUtility.ServerSideUploadFileImageToTempFolder(RenderInfo, XmlInfoResult).value;\r\n" +

                    "    if(AjaxOut.Error)\r\n" +
                    "    {\r\n" +
                    "        document.getElementById('divProcessingUploadImage').innerHTML='';\r\n" +
                    "        alert(AjaxOut.InfoMessage);\r\n" +
                    "        return;\r\n" +
                    "    }\r\n" +
                    "    document.getElementById('divProcessingUploadImage').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "    document.getElementById('txtChangeImage').value='1';\r\n" +
                    "    document.getElementById('txtImageSaveFile').value=AjaxOut.SaveFile;\r\n" +
                    " }\r\n" +

                    " function GetUrlParam(varName, url){ \r\n" +
                    "   var ret = ''; \r\n" +
                    "   if(!url) \r\n" +
                    "     url = self.location.href; \r\n" +
                    "   if(url.indexOf('?') > -1){ \r\n" +
                    "      url = url.substr(url.indexOf('?') + 1); \r\n" +
                    "      url = url.split('&'); \r\n" +
                    "      for(i = 0; i < url.length; i++){ \r\n" +
                    "        var tmp = url[i].split('='); \r\n" +
                    "        if(tmp[0] && tmp[1] && tmp[0] == varName){ \r\n" +
                    "          ret = tmp[1]; \r\n" +
                    "          break; \r\n" +
                    "        } \r\n" +
                    "      } \r\n" +
                    "   } \r\n" +

                    "   return ret; \r\n" +
                    " }; \r\n" +

                    " function setFile() \r\n" +
                    " {\r\n" +
                    "      RenderInfo=CreateRenderInfo();\r\n" +
                    "      ImageSaveFile = document.getElementById('txtImageSaveFile').value;\r\n" +
                    "      AjaxOut = OneTSQ.UploadUtility.UploadMediaToServerUtility.ServerSideUploadFileImageToMedia(RenderInfo, ImageSaveFile).value;\r\n" +
                    "      if(AjaxOut.Error)\r\n" +
                    "      {\r\n" +
                    "           alert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "      }\r\n" +
                    "      insertPath=AjaxOut.RetUrl;\r\n" +
                    "      var win = (window.opener ? window.opener : window.parent); \r\n" +
                    "      if(win.document.getElementById(GetUrlParam('input'))==null)\r\n" +
                    "      {\r\n" +
                    "           document.getElementById('spanFilePath').value=insertPath;\r\n" +
                    "           document.getElementById('spanFilePath').style.display='block';\r\n" +
                    "           return;\r\n" +
                    "      }\r\n" +
                    "      win.document.getElementById(GetUrlParam('input')).value = insertPath; \r\n" +
                    "      if (typeof(win.ImageDialog) != \"undefined\") { \r\n" +
                    "          if (win.ImageDialog.getImageData) \r\n" +
                    "              win.ImageDialog.getImageData(); \r\n" +
                    "          if (win.ImageDialog.showPreviewImage) \r\n" +
                    "              win.ImageDialog.showPreviewImage(insertPath); \r\n" +
                    "      } \r\n" +
                    "      win.tinyMCE.activeEditor.windowManager.close(); \r\n" +
                    " }\r\n" +

                    "</script>\r\n") +

                    WebEnvironments.ProcessHtml(
                        "<input id=\"txtChangeImage\" value=\"0\" type=hidden>\r\n" +
                        "<input id=\"txtImageSaveFile\" value=\"\" type=hidden>\r\n" +
                        "<input id=\"txtOwnerId\" type=\"hidden\" value=\"" + WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId + "\" />\r\n" +
                        "<div id=\"divListForm\">\r\n" +
                        " <div class=\"ibox float-e-margins\"> \r\n" +
                        "     <div class=\"ibox-content\"> \r\n" +
                        "         <div class=\"row\"> \r\n" +

                        "                <div class=\"form-group\"><label style=\"font-weight:bold;color:green\">" + WebLanguage.GetLanguage(OSiteParam, "Upload ảnh hoặc video lên máy chủ.<br>Chú ý: Video yêu cầu file mp4<br>") + "</label> \r\n" +
                        "                    <input type=\"file\" id=\"ImageUpload\" name=\"ImageUpload\" onchange=\"fileImageSelected();\">\r\n" +
                        "                    <div id=\"divProcessingUploadImage\" style=\"color:green;font-weight:bold\"></div>\r\n" +
                        "                </div> \r\n" +

                        "         </div> \r\n" +
                        "     </div> \r\n" +
                        " </div> \r\n" +
                        "</div>\r\n"
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

        static bool CheckImage(string CheckFile)
        {
            CheckFile = CheckFile.ToLower();
            string Ext = new System.IO.FileInfo(CheckFile).Extension;
            if (Ext.Equals(".png") || Ext.Equals(".jpg") || Ext.Equals(".jpeg") || Ext.Equals(".bmp"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideUploadFileImageToTempFolder(
            RenderInfoCls ORenderInfo,
            string XmlResultInfo)
        {
            AjaxOut OAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                UploadHandlerResultCls
                    OUploadHandlerResult = UploadHandlerResultParser.ParseFromXml(XmlResultInfo);
                if (OUploadHandlerResult.Error) throw new Exception(OUploadHandlerResult.InfoMessage);
                string SaveFile = OUploadHandlerResult.SaveFile;
                string Url = OUploadHandlerResult.UploadUrl;

                OAjaxOut.HtmlContent =
                    "Upload Image [" + OUploadHandlerResult.UploadFileName + "] xong\r\n";

                if (CheckImage(OUploadHandlerResult.UploadFileName))
                {
                    OAjaxOut.HtmlContent +=
                        "<div><img style=\"width:400px\" src=\"" + Url + "\"/></div>\r\n";
                }
                else
                {
                    string Ext = new System.IO.FileInfo(SaveFile).Extension.ToLower();
                    //if (Ext.IndexOf("mp4") == -1)
                    //{
                    //    throw new Exception("Video yêu cầu kiểu file là mp4");
                    //}
                }
                OAjaxOut.HtmlContent +=
                    "<div style=\"margin-top:10px\"><input onclick=\"javascript:setFile();\" style=\"cursor:pointer\" type=button class=\"btn blue\" value=\"Đưa vào bài viết\"></div>\r\n";
                OAjaxOut.SaveFile = SaveFile;
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
        public static AjaxOut ServerSideUploadFileImageToMedia(
            RenderInfoCls ORenderInfo,
            string ImageSaveFile)
        {
            AjaxOut OAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                //string WsUploadMediaRoot = WebConfig.GetWsUploadMediaRoot();
                byte[] bytes = FunctionUtility.GetBytesFromFile(ImageSaveFile);
                DateTime EntryDate = System.DateTime.Now;

                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                MediaInfoCls
                    OMediaInfo = new MediaInfoCls();
                OMediaInfo.MediaInfoId = System.Guid.NewGuid().ToString();
                OMediaInfo.Month = System.DateTime.Now.Month;
                OMediaInfo.Year = System.DateTime.Now.Year;
                OMediaInfo.Section = "editor";
                OMediaInfo.Overwrite = true;
                OMediaInfo.LoginName = LoginName;
                OMediaInfo.SiteId = ORenderInfo.SiteId;
                OMediaInfo.UploadFileName = OMediaInfo.MediaInfoId.Substring(0, 8) + "." + new System.IO.FileInfo(ImageSaveFile).Name;
                XmlCls OXml = MediaInfoParser.GetXml(new MediaInfoCls[] { OMediaInfo });

                OAjaxOut = OneTSQ.MediaService.MediaServiceUtility.UploadMedia(WebConfig.GetSecurityCode(), OXml.XmlData, OXml.XmlDataSchema, bytes);
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
//