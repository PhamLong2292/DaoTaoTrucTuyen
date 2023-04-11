using OneTSQ.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    public class TestVideoStream : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "TestVideoStream";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "TestVideoStream";
            }
        }

        public override string Description
        {
            get
            {
                return "TestVideoStream";
            }
        }
        public override void RegAjaxPro(SiteParam OSiteParam, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(TestVideoStream), Page);
        }
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                var currentUser = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                RetAjaxOut.HtmlContent =
                #region Javascript
                WebEnvironments.ProcessJavascript(
                               " <script type=\"text/javascript\">\r\n" +
                                    "   function ShowStream()\r\n" +
                                    "   {\r\n" +
                                    "       RenderInfo=CreateRenderInfo();\r\n" +
                                    "       webLink = document.getElementById('txtWebLink').value;\r\n" +
                                    "       AjaxOut = OneTSQ.WebParts.TestVideoStream.ShowStream(RenderInfo, webLink).value;\r\n" +
                                    "       document.getElementById('divShowStream').innerHTML=AjaxOut.HtmlContent;\r\n" +
                                    "   }\r\n" +
                               "</script>\r\n") +
                #endregion
                #region Html
                WebEnvironments.ProcessHtml(
               "<div class=\"ibox float-e-margins\"> \r\n" +
                "     <div class=\"ibox-title\"> \r\n" +
                "         <h5><strong>Stream</strong></h5> \r\n" +
                "     </div> \r\n" +
                "     <div class=\"ibox-content\"> \r\n" +
                "        <div class = 'row' >\r\n" +
                "          <div class=\"col-lg-4\" >\r\n" +
                "               <input id='txtWebLink' type='text' value='' class='form-control valueForm' required placeholder = 'Nhập địa chỉ web'>\r\n" +
                "          </div>\r\n" +
                "          <div class=\"col-lg-4\">\r\n" +
                "               <button type='button' class='btn btn-sm btn-default'  onclick='ShowStream()'>" + WebLanguage.GetLanguage(OSiteParam, "Show Stream") + "</button>\r\n" +
                "          </div>\r\n" +
                "        </div>\r\n" +
                "        <div id='divShowStream' class = 'row'>\r\n" +
                "        </div>\r\n" +
                "    </div>\r\n" +
                "</div>\r\n"
                );
                #endregion

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
        public static AjaxOut ShowStream(RenderInfoCls ORenderInfo, string webLink)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string url = "https://wcs5-eu.flashphoner.com:8888/embed_player?urlServer=wss://wcs5-eu.flashphoner.com:8443&streamName=" + webLink + "&mediaProviders=,Flash,MSE,WSPlayer?autoplay=1'";
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.HtmlContent = WebEnvironments.ProcessHtml(
               "<div class=\"ibox float-e-margins\"> \r\n" +
                "     <iframe id='fp_embed_player' src='" + url + "' marginwidth='0' marginheight='0' frameborder='0' width='700' height='700' scrolling='no' allowfullscreen='allowfullscreen'></iframe> \r\n" +
                "</div>\r\n" +
                "<style>" +
                "   #remoteVideo{width: 700px !important; height: 700px !important;}" +
                "</style>"
                );
            //var address = "rtsp://123.24.130.16/extron";
            //var output = "<object width=\"640\" height=\"480\" id=\"qt\" classid=\"clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B\" codebase=\"http://www.apple.com/qtactivex/qtplugin.cab\">";
            //output += "<param name=\"src\" value=\"' + adsress + '\">";
            //output += "<param name=\"autoplay\" value=\"true\">";
            //output += "<param name=\"controller\" value=\"false\">";
            //output += "<embed id=\"plejer\" name=\"plejer\" src=\"/poster.mov\" bgcolor=\"000000\" width=\"640\" height=\"480\" scale=\"ASPECT\" qtsrc=\"" + address + "\"  kioskmode=\"true\" showlogo=false\" autoplay=\"true\" controller=\"false\" pluginspage=\"http://www.apple.com/quicktime/download/\">";
            //output += "</embed></object>";
            //RetAjaxOut.HtmlContent = output;
            return RetAjaxOut;
        }
    }
}
