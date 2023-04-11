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
    public class DashboardModule8: DashboardModuleTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "DashboardModule8";
            }
        }

        public override string ServiceName
        {
            get
            {
                return "Sức khỏe cán bộ theo đợt khám 2020";
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(DashboardModule8), ORenderInfo.WebPage);
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
                    "       AjaxOut = OneTSQ.Gui.DashboardModule8.ServerSideLoadLoaiKhamTheoDotKham(RenderInfo).value;\r\n" +
                    "       if(AjaxOut.Error) {\r\n" +
                    "           callGallAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       if(AjaxOut.RetObject != null){\r\n" +
                    "           var datax = ['2020', 0];\r\n" +
                    "           var i;\r\n" +
                    "           for (i = 0; i < AjaxOut.RetObject.length; i++)\r\n" +
                    "           {\r\n" +
                    "               datax.push([AjaxOut.RetObject[i].data]);\r\n" +
                    "           }\r\n" +
                    "       }\r\n" +
                    "       c3.generate({\r\n" +
                    "            bindto: '#lineChart',\r\n" +
                    "            size: {\r\n" +
                    "               height: 200,\r\n" +
                    "            },\r\n" +
                    "            data:{\r\n" +
                    "                columns: [\r\n" +
                    "                     datax\r\n" +
                    //"                    ['2021', 0, 121, 191, 6, 0],\r\n" +
                    //"                    ['2022', 0, 150, 162, 6, 0]\r\n" +
                    "                ],\r\n" +
                    "                colors:{\r\n" +
                    "                    2020: '#1ab394',\r\n" +
                    //"                    2021: '#337ab7'\r\n" +
                    //"                    2022: '#BABABA'\r\n" +
                    "                }\r\n" +
                    "            }\r\n" +
                    "       });\r\n" +
                    "    });\r\n" +
                    "</script>\r\n") +
                #endregion
                #region Html
                    WebEnvironments.ProcessHtml(
                    "<div>\r\n" +
                    "   <div id=\"lineChart\"></div>\r\n" +
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
