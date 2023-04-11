using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Model;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Gui
{
    public class DashboardModule6: DashboardModuleTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "DashboardModule6";
            }
        }

        public override string ServiceName
        {
            get
            {
                return "Đánh giá tình hình sức khỏe cán bộ 2020";
            }
        }
        public override bool CheckPermissionAllowShow(RenderInfoCls ORenderInfo, DashboardParamCls ODashboardParam)
        {
            return base.CheckPermissionAllowShow(ORenderInfo, ODashboardParam);
        }

        public override void RegisterAjaxPro(RenderInfoCls ORenderInfo)
        {
            if (ORenderInfo.WebPage != null)
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(DashboardModule6), ORenderInfo.WebPage);
            }
        }

        public override AjaxOut Draw(RenderInfoCls ORenderInfo, DashboardParamCls ODashboardParam)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                RetAjaxOut.HtmlContent =
                #region Javascript
                WebEnvironments.ProcessJavascript(
                    "<script language=javascript>\r\n" +
                    "   $(document).ready(function() {\r\n"+
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.Gui.DashboardModule6.ServerSideLoadLoaiKhamTheoDotKham(RenderInfo).value;\r\n" +
                    "       if(AjaxOut.Error) {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       var data = AjaxOut.RetObject;\r\n" +
                    "       var plotObj = $.plot($(\"#flot-pie-chart\"), data, {\r\n" +
                    "           series: {\r\n" +
                    "               pie: {\r\n" +
                    "                   show: true\r\n" +
                    "               }\r\n" +
                    "           },\r\n" +
                    "           grid: {\r\n" +
                    "               hoverable: true\r\n" +
                    "           },\r\n" +
                    "           tooltip: true,\r\n" +
                    "           tooltipOpts: {\r\n" +
                    "               content: \"%p.0%, %s\", // show percentages, rounding to 2 decimal places\r\n" +
                    "               shifts: {\r\n" +
                    "                   x: 20,\r\n" +
                    "                   y: 0\r\n" +
                    "               },\r\n" +
                    "               defaultTheme: false\r\n" +
                    "           }\r\n" +
                    "       });\r\n" +
                    "    });\r\n" +
                    "</script>\r\n") +
                #endregion
                #region Html
                    WebEnvironments.ProcessHtml(        
                    "<div class=\"flot-chart\">\r\n" +
                    "   <div class=\"flot-chart-pie-content\" id=\"flot-pie-chart\"></div>\r\n" +
                    "</div>\r\n");
                #endregion
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = RetAjaxOut.HtmlContent= ex.Message.ToString();
            }

            return RetAjaxOut;
        }
        
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideLoadLoaiKhamTheoDotKham(RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                var OLoaiKhamChar = CallBussinessUtility.CreateBussinessProcess().CreateKhamSucKhoeProcess().ReadingDotKhamChar(ORenderInfo, new KhamSucKhoeFilterCls());
                RetAjaxOut.RetObject = OLoaiKhamChar;
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
