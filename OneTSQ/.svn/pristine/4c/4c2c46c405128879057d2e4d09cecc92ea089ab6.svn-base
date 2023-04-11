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
    public class DashboardModule5 : DashboardModuleTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "DashboardModule5";
            }
        }

        public override string ServiceName
        {
            get
            {
                return "Kế hoạch chăm sóc sức khỏe cán bộ 2021 - 2022";
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(DashboardModule5), ORenderInfo.WebPage);
            }
        }

        public override AjaxOut Draw(RenderInfoCls ORenderInfo, DashboardParamCls ODashboardParam)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                //RetAjaxOut.HtmlContent =
                #region Javascript
                //WebEnvironments.ProcessJavascript(
                string Html =
                "<script language=javascript>\r\n" +
                "        $(document).ready(function() {\r\n" +
                "            var data2 = [\r\n" +
                "                [gd(2018, 9, 1), 7], [gd(2018, 9, 2), 6], [gd(2018, 9, 3), 4], [gd(2018, 9, 4), 8],\r\n" +
                "                [gd(2018, 9, 5), 9], [gd(2018, 9, 6), 7], [gd(2018, 9, 7), 5], [gd(2018, 9, 8), 4],\r\n" +
                "                [gd(2018, 9, 9), 7], [gd(2018, 9, 10), 8], [gd(2018, 9, 11), 9], [gd(2018, 9, 12), 6],\r\n" +
                "                [gd(2018, 9, 13), 4], [gd(2018, 9, 14), 5], [gd(2018, 9, 15), 11], [gd(2018, 9, 16), 8],\r\n" +
                "                [gd(2018, 9, 17), 8], [gd(2018, 9, 18), 11], [gd(2018, 9, 19), 11], [gd(2018, 9, 20), 6],\r\n" +
                "                [gd(2018, 9, 21), 6], [gd(2018, 9, 22), 8], [gd(2018, 9, 23), 11], [gd(2018, 9, 24), 13],\r\n" +
                "                [gd(2018, 9, 25), 7], [gd(2018, 9, 26), 9], [gd(2018, 9, 27), 9], [gd(2018, 9, 28), 8],\r\n" +
                "                [gd(2018, 9, 29), 5], [gd(2018, 9, 30), 8], [gd(2018, 9, 31), 25]\r\n" +
                "            ];\r\n" +
                "            var data3 = [\r\n" +
                "                [gd(2018, 9, 1), 80], [gd(2018, 9, 2), 50], [gd(2018, 9, 3), 60], [gd(2018, 9, 4), 70],\r\n" +
                "                [gd(2018, 9, 5), 50], [gd(2018, 9, 6), 45], [gd(2018, 9, 7), 80], [gd(2018, 9, 8), 58],\r\n" +
                "                [gd(2018, 9, 9), 46], [gd(2018, 9, 10), 87], [gd(2018, 9, 11), 68], [gd(2018, 9, 12), 70],\r\n" +
                "                [gd(2018, 9, 13), 50], [gd(2018, 9, 14), 60], [gd(2018, 9, 15), 70], [gd(2018, 9, 16), 78],\r\n" +
                "                [gd(2018, 9, 17), 34], [gd(2018, 9, 18), 88], [gd(2018, 9, 19), 88], [gd(2018, 9, 20), 88],\r\n" +
                "                [gd(2018, 9, 21), 98], [gd(2018, 9, 22), 44], [gd(2018, 9, 23), 99], [gd(2018, 9, 24), 56],\r\n" +
                "                [gd(2018, 9, 25), 78], [gd(2018, 9, 26), 66], [gd(2018, 9, 27), 88], [gd(2018, 9, 28), 90],\r\n" +
                "                [gd(2018, 9, 29), 17], [gd(2018, 9, 30), 55], [gd(2018, 9, 31), 99]\r\n" +
                "            ];\r\n" +
                "            var dataset = [\r\n" +
                "                {\r\n" +
                "                    label: \"Số liệu kế hoạch\",\r\n" +
                "                    data: data3,\r\n" +
                "                    color: \"#1ab394\",\r\n" +
                "                    bars: {\r\n" +
                "                        show: true,\r\n" +
                "                        align: \"center\",\r\n" +
                "                        barWidth: 24 * 60 * 60 * 600,\r\n" +
                "                        lineWidth:0\r\n" +
                "                    }\r\n" +
                "                }, {\r\n" +
                "                    label: \"Thực tế\",\r\n" +
                "                    data: data2,\r\n" +
                "                    yaxis: 2,\r\n" +
                "                    color: \"#1C84C6\",\r\n" +
                "                    lines: {\r\n" +
                "                        lineWidth:1,\r\n" +
                "                            show: true,\r\n" +
                "                            fill: true,\r\n" +
                "                        fillColor: {\r\n" +
                "                            colors: [{\r\n" +
                "                                opacity: 0.2\r\n" +
                "                            }, {\r\n" +
                "                                opacity: 0.4\r\n" +
                "                            }]\r\n" +
                "                        }\r\n" +
                "                    },\r\n" +
                "                    splines: {\r\n" +
                "                        show: false,\r\n" +
                "                        tension: 0.6,\r\n" +
                "                        lineWidth: 1,\r\n" +
                "                        fill: 0.1\r\n" +
                "                    },\r\n" +
                "                }\r\n" +
                "            ];\r\n" +
                "            var options = {\r\n" +
                "                xaxis: {\r\n" +
                "                    mode: \"time\",\r\n" +
                "                    tickSize: [3, \"day\"],\r\n" +
                "                    tickLength: 0,\r\n" +
                "                    axisLabel: \"Date\",\r\n" +
                "                    axisLabelUseCanvas: true,\r\n" +
                "                    axisLabelFontSizePixels: 12,\r\n" +
                "                    axisLabelFontFamily: 'Arial',\r\n" +
                "                    axisLabelPadding: 10,\r\n" +
                "                    color: \"#d5d5d5\"\r\n" +
                "                },\r\n" +
                "                yaxes: [{\r\n" +
                "                    position: \"left\",\r\n" +
                "                    max: 107,\r\n" +
                "                    color: \"#d5d5d5\",\r\n" +
                "                    axisLabelUseCanvas: true,\r\n" +
                "                    axisLabelFontSizePixels: 12,\r\n" +
                "                    axisLabelFontFamily: 'Arial',\r\n" +
                "                    axisLabelPadding: 3\r\n" +
                "                }, {\r\n" +
                "                    position: \"right\",\r\n" +
                "                    clolor: \"#d5d5d5\",\r\n" +
                "                    axisLabelUseCanvas: true,\r\n" +
                "                    axisLabelFontSizePixels: 12,\r\n" +
                "                    axisLabelFontFamily: ' Arial',\r\n" +
                "                    axisLabelPadding: 67\r\n" +
                "                }\r\n" +
                "                ],\r\n" +
                "                legend: {\r\n" +
                "                    noColumns: 1,\r\n" +
                "                    labelBoxBorderColor: \"#000000\",\r\n" +
                "                    position: \"nw\"\r\n" +
                "                },\r\n" +
                "                grid: {\r\n" +
                "                    hoverable: false,\r\n" +
                "                    borderWidth: 0\r\n" +
                "                }\r\n" +
                "            };\r\n" +
                "            function gd(year, month, day) {\r\n" +
                "                return new Date(year, month - 1, day).getTime();\r\n" +
                "            }\r\n" +
                "            var previousPoint = null, previousLabel = null;\r\n" +
                "            $.plot($(\"#flot-dashboard-chart\"), dataset, options);\r\n" +
                "        });\r\n" +
                "</script>\r\n";
                #endregion

                #region Html
                //WebEnvironments.ProcessHtml(
                Html += "<div class=\"row\">\r\n" +
                    "<div class=\"col-lg-9\">\r\n" +
                    "    <div class=\"flot-chart\">\r\n" +
                    "        <div class=\"flot-chart-content\" id=\"flot-dashboard-chart\"></div>\r\n" +
                    "    </div>\r\n" +
                    "</div>\r\n" +
                    "<div class=\"col-lg-3\">\r\n" +
                    "    <ul class=\"stat-list\">\r\n";

                var OLoaiKhamChar = CallBussinessUtility.CreateBussinessProcess().CreateKhamSucKhoeProcess().ReadingDotKhamChar(ORenderInfo, new KhamSucKhoeFilterCls());
                int tong = OLoaiKhamChar.Sum(o => o.data);
                foreach (var OLoaiKhamCha in OLoaiKhamChar.OrderBy(o => o.stt))
                {
                    int percent = (OLoaiKhamCha.data * 100) / tong ;
                    Html +=
                    "        <li>\r\n" +
                    "            <h2 class=\"no-margins\">"+ OLoaiKhamCha.data + "</h2>\r\n" +
                    "            <small>"+ OLoaiKhamCha.label + "</small>\r\n" +
                    "            <div class=\"stat-percent\">"+ percent + "% <i class=\"fa "+(OLoaiKhamCha.stt == 3 ? "fa-level-down" : "fa-level-up") +" text-navy\"></i></div>\r\n" +
                    "            <div class=\"progress progress-mini\">\r\n" +
                    "                <div style=\"width: "+ percent + "%;\" class=\"progress-bar\"></div>\r\n" +
                    "            </div>\r\n" +
                    "        </li>\r\n";
                }
                
                //"        <li>\r\n" +
                //"            <h2 class=\"no-margins \">191</h2>\r\n" +
                //"            <small>Sức khỏe loại II</small>\r\n" +
                //"            <div class=\"stat-percent\">60% <i class=\"fa fa-level-down text-navy\"></i></div>\r\n" +
                //"            <div class=\"progress progress-mini\">\r\n" +
                //"                <div style=\"width: 60%;\" class=\"progress-bar\"></div>\r\n" +
                //"            </div>\r\n" +
                //"        </li>\r\n" +
                //"        <li>\r\n" +
                //"            <h2 class=\"no-margins \">6</h2>\r\n" +
                //"            <small>Sức khỏe loại III</small>\r\n" +
                //"            <div class=\"stat-percent\">22% <i class=\"fa fa-bolt text-navy\"></i></div>\r\n" +
                //"            <div class=\"progress progress-mini\">\r\n" +
                //"                <div style=\"width: 2%;\" class=\"progress-bar\"></div>\r\n" +
                //"            </div>\r\n" +
                //"        </li>\r\n" +

                Html +=
                    "        </ul>\r\n" +
                    "    </div>\r\n" +
                    "</div>\r\n" +
                    "</div>\r\n";
                #endregion
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
