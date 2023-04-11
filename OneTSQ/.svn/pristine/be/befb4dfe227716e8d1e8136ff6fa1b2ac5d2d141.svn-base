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
    public class DashboardModule7: DashboardModuleTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "DashboardModule7";
            }
        }

        public override string ServiceName
        {
            get
            {
                return "Biểu đồ cột tình hình sức khỏe cán bộ 2020";
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(DashboardModule7), ORenderInfo.WebPage);
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
                    "    var barOptions = {\r\n" +
                    "        series: {\r\n" +
                    "            bars: {\r\n" +
                    "                show: true,\r\n" +
                    "                barWidth: 0.6,\r\n" +
                    "                fill: true,\r\n" +
                    "                fillColor: {\r\n" +
                    "                    colors: [{\r\n" +
                    "                        opacity: 0.8\r\n" +
                    "                    }, {\r\n" +
                    "                        opacity: 0.8\r\n" +
                    "                    }]\r\n" +
                    "                }\r\n" +
                    "            }\r\n" +
                    "        },\r\n" +
                    "        xaxis: {\r\n" +
                    "            tickDecimals: 0\r\n" +
                    "        },\r\n" +
                    "        colors: [\"#1ab394\"],\r\n" +
                    "        grid: {\r\n" +
                    "            color: \"#999999\",\r\n" +
                    "            hoverable: true,\r\n" +
                    "            clickable: true,\r\n" +
                    "            tickColor: \"#D4D4D4\",\r\n" +
                    "            borderWidth:0\r\n" +
                    "        },\r\n" +
                    "        legend: {\r\n" +
                    "            show: false\r\n" +
                    "        },\r\n" +
                    "        tooltip: true,\r\n" +
                    "        tooltipOpts: {\r\n" +
                    "            content: \"Loại: %x, Số lượng: %y\"\r\n" +
                    "        }\r\n" +
                    "    };\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       AjaxOut = OneTSQ.Gui.DashboardModule7.ServerSideLoadLoaiKhamTheoDotKham(RenderInfo).value;\r\n" +
                    "       if(AjaxOut.Error) {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetObject != null){\r\n" +
                    "           var datax = [];\r\n" +
                    "           var i;\r\n" +
                    "           for (i = 0; i < AjaxOut.RetObject.length; i++)\r\n" +
                    "           {\r\n" +
                    "               datax.push([ AjaxOut.RetObject[i].stt, AjaxOut.RetObject[i].data]);\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "    var barData = {\r\n" +
                    "        label: \"bar\",\r\n" +
                    "        data: datax\r\n" +
                    "    };\r\n" +
                    "    $.plot($(\"#flot-bar-chart\"), [barData], barOptions);\r\n" +
                    "    });\r\n" +
                    "</script>\r\n") +
                #endregion
                #region Html
                    WebEnvironments.ProcessHtml(
                    "<div class=\"flot-chart\">\r\n" +
                    "   <div class=\"flot-chart-content\" id=\"flot-bar-chart\"></div>\r\n" +
                    "</div>\r\n");
                #endregion
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent= ex.Message.ToString();
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
