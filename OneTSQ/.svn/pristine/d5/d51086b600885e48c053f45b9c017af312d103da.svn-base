using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Model;using OneTSQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Gui
{
    public class DashboardModule2: DashboardModuleTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "DashboardModule2";
            }
        }

        public override string ServiceName
        {
            get
            {
                return "Thiết bị xét nghiệm";
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(DashboardModule2), ORenderInfo.WebPage);
            }
        }

        public override AjaxOut Draw(RenderInfoCls ORenderInfo, DashboardParamCls ODashboardParam)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string Html = "<div class=\"ibox-content\" style=\"border-style: none;\">\r\n" +
                            "    <table class=\"table table-stripped small\">\r\n" +
                            "        <tbody>\r\n";
                var thietbixns = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucThietBiProcess().Reading(ORenderInfo, new DanhMucThietBiFilterCls());
                foreach (var tb in thietbixns)
                {
                    long count = CallBussinessUtility.CreateBussinessProcess().CreateXetNghiemProcess().ThongKeSoLuongTheoThietBi(ORenderInfo, tb.ID);
                    Html += "        <tr>\r\n" +
                            "            <td>\r\n" +
                            "                <i class=\"fa fa-circle text-navy\">&nbsp;&nbsp;&nbsp;" + tb.Ten + "</i>\r\n" +
                            "            </td>\r\n" +
                            "            <td><lable>" + count + "</lable></td>\r\n" +
                            "        </tr>\r\n";
                }
                Html += "        </tbody>\r\n" +
                        "    </table>\r\n" +
                        "</div>";

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
