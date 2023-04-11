using OneTSQ.Core.Model;using OneTSQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Gui
{
    public class DashboardModule4: DashboardModuleTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "DashboardModule4";
            }
        }

        public override string ServiceName
        {
            get
            {
                return "User activity";
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(DashboardModule4), ORenderInfo.WebPage);
            }
        }

        public override AjaxOut Draw(RenderInfoCls ORenderInfo, DashboardParamCls ODashboardParam)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string Html =
                            "<h1 class=\"no-margins\">80,600</h1>\r\n" +
                            "<div class=\"stat-percent font-bold text-danger\">38% <i class=\"fa fa-level-down\"></i></div>\r\n" +
                            "<small>Số liệu đầu tháng:</small>\r\n";

                RetAjaxOut.HtmlContent = Html;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent= ex.Message.ToString();
            }

            return RetAjaxOut;
        }
    }
}
