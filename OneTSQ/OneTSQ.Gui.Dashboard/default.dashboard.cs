using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OneTSQ.Gui.Dashboard
{
    public class DefaultDashboard : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "default.dashboard";
            }
        }

        public override string WebPartTitle
        {
            get
            {
                return "Màn hình thông tin";
            }
        }

        public override string Description
        {
            get
            {
                return "Màn hình thông tin";
            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, Page Page)
        {
            SiteParam
                 OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DefaultDashboard), Page);

            string DashboardId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserAccessInfo.frkDashboardId;
            if (!string.IsNullOrEmpty(DashboardId))
            {
                DashboardItemFilterCls
                    ODashboardItemFilter = new DashboardItemFilterCls();
                ODashboardItemFilter.DashboardId = DashboardId;
                ODashboardItemFilter.Visible = 1;
                DashboardItemCls[]
                    DashboardItems = CoreCallBussinessUtility.CreateBussinessProcess().CreateDashboardProcess().ReadingDashboardItem(ORenderInfo, ODashboardItemFilter);
                for (int iIndexItem = 0; iIndexItem < DashboardItems.Length; iIndexItem++)
                {
                    IDashboardModule
                        FoundDashboardModule = DashboardModuleUtility.Find(OSiteParam, DashboardItems[iIndexItem].DashboardModuleId);
                    if (FoundDashboardModule != null)
                    {
                        FoundDashboardModule.RegisterAjaxPro(ORenderInfo);
                    }
                }
            }
        }

        
        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true;// WebPermissionUtility.CheckPermission(OSiteParam, DictionaryPermission.StaticPermissionFunctionId, "Access", DictionaryPermission.StaticPermissionFunctionCode, DictionaryPermission.StaticPermissionFunctionId, UserId, false);
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }

        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string DashboardId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserAccessInfo.frkDashboardId;
                if (string.IsNullOrEmpty(DashboardId))
                {
                    return RetAjaxOut;
                }
                DashboardCls
                    ODashboard = CoreCallBussinessUtility.CreateBussinessProcess().CreateDashboardProcess().CreateModel(ORenderInfo, DashboardId);
                if (ODashboard == null)
                {
                    return RetAjaxOut;
                }
                string[] ItemZones = ODashboard.Zones.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                RetAjaxOut.HtmlContent =
                   "<div class=\"wrapper wrapper-content\">\r\n" +
                   "    <div class=\"row\">\r\n" +
                            ODashboard.HtmlLayout+
                   "    </div>\r\n" +
                   //"    <div class=\"row\"><div class=\"col-md-12\">" + LoadZone(ORenderInfo, 5).HtmlContent + "</div></div>\r\n" +                   
                   "</div>\r\n";

                for (int iIndex = 0; iIndex < ItemZones.Length; iIndex++)
                {
                    ItemZones[iIndex] = ItemZones[iIndex].Trim();
                    RetAjaxOut.HtmlContent = RetAjaxOut.HtmlContent.Replace("{" + ItemZones[iIndex] + "}", LoadZone(ORenderInfo, int.Parse(ItemZones[iIndex])).HtmlContent);
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.HtmlContent = ex.Message.ToString();
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

       

        public virtual AjaxOut LoadZone(RenderInfoCls ORenderInfo, int ZoneIndex)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                string DashboardId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserAccessInfo.frkDashboardId;
                if (!string.IsNullOrEmpty(DashboardId))
                {
                    DashboardItemFilterCls
                        ODashboardItemFilter = new DashboardItemFilterCls();
                    ODashboardItemFilter.DashboardId = DashboardId;
                    ODashboardItemFilter.ActiveZoneIndex = 1;
                    ODashboardItemFilter.ZoneIndex = ZoneIndex;
                    ODashboardItemFilter.Visible = 1;
                    

                    ODashboardItemFilter.DashboardId = DashboardId;
                    DashboardItemCls[]
                        DashboardItems = CoreCallBussinessUtility.CreateBussinessProcess().CreateDashboardProcess().ReadingDashboardItem(ORenderInfo, ODashboardItemFilter);
                    for (int iIndexItem = 0; iIndexItem < DashboardItems.Length; iIndexItem++)
                    {
                        IDashboardModule 
                            FoundDashboardModule = DashboardModuleUtility.Find(OSiteParam, DashboardItems[iIndexItem].DashboardModuleId);
                        if (FoundDashboardModule != null)
                        {
                            DashboardParamCls 
                                ODashboardParam = new DashboardParamCls();

                            RetAjaxOut.HtmlContent =
                                "<div id=\"ibox float-e-margins\" style=\"padding-bottom:20px\">\r\n" +
                                "   <div class=\"ibox-title\"> \r\n"+
                                "       <h5>"+ DashboardItems[iIndexItem].ModuleTitle + "</h5> \r\n"+
                                "   </div>\r\n"+
                                "   <div class=\"ibox-content\">\r\n"+
                                        FoundDashboardModule.Draw(ORenderInfo, ODashboardParam).HtmlContent +
                                "   </div>\r\n"+
                                "</div>\r\n";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.HtmlContent = ex.Message.ToString();
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }

        public override bool NeedCheckPermission
        {
            get
            {
                return false;
            }
        }

    }
}
