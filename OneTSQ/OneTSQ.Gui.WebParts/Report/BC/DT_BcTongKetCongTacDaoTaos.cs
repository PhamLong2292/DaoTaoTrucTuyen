using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Linq;
using System.Data;
using OneTSQ.CheckPermissionUtility;
using OneTSQ.Permission;
using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    public class DT_BcTongKetCongTacDaoTaos : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "DT_BcTongKetCongTacDaoTaos";
            }
        }
        public override string WebPartTitle
        {
            get
            {
                return "Danh sách Báo cáo tổng kết công tác đào tạo";
            }
        }
        public override string Description
        {
            get
            {
                return "Danh sách Báo cáo tổng kết công tác đào tạo";

            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DT_BcTongKetCongTacDaoTaos), Page);
        }

        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true;
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                //bool xemPermission = PermissionUtility.CheckPermission(ORenderInfo, new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoCls.ePermission.Xem.ToString(), new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoPermission.StaticPermissionFunctionId, UserId);
                //bool themPermission = PermissionUtility.CheckPermission(ORenderInfo, new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoCls.ePermission.Them.ToString(), new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoPermission.StaticPermissionFunctionId, UserId);
                //bool suaPermission = PermissionUtility.CheckPermission(ORenderInfo, new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoCls.ePermission.Sua.ToString(), new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoPermission.StaticPermissionFunctionId, UserId);
                //bool xoaPermission = PermissionUtility.CheckPermission(ORenderInfo, new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoCls.ePermission.Xoa.ToString(), new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode, DT_BcTongKetCongTacDaoTaoPermission.StaticPermissionFunctionId, UserId);

                #region Javascript
                RetAjaxOut.HtmlContent =
                    WebEnvironments.ProcessJavascript(
                     "<script language=\"javascript\">\r\n" +
                    "   $(document).ready(function() {\r\n" +
                    "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Danh sách Báo cáo tổng kết công tác đào tạo") + "';\r\n" +
                    //"       FilterChange();\r\n" +
                    "   });\r\n" +

                    "   var CurrentPageIndex=0;\r\n" +

                    "   function NextPage(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       CurrentPageIndex = PageIndex;\r\n" +
                    "       setTimeout('Search()',10);\r\n" +
                    "   }\r\n" +

                    "   function Search()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       loaiThoiGian = parseInt(document.getElementById('cbbLoai').value);\r\n" +
                    "       tuNgay = document.getElementById('dtTuNgay').value;\r\n" +
                    "       denNgay = document.getElementById('dtDenNgay').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DT_BcTongKetCongTacDaoTaos.DrawSearchResult(RenderInfo, CurrentPageIndex, loaiThoiGian, tuNgay, denNgay).value;\r\n" +
                    "       document.getElementById('divDT_BcTongKetCongTacDaoTaos').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +
                    "   function FilterChange(){\r\n" +
                    "       CurrentPageIndex=0;\r\n" +
                    "       Search();\r\n" +
                    "   }\r\n" +
                #region Refresh danh sách phiếu
                    "   function RefreshList(){\r\n" +
                    "       Search();\r\n" +
                    "   }\r\n" +
                #endregion
                    "</script>\r\n") +
                #endregion
                #region html
                    WebEnvironments.ProcessHtml(
                    "<div id=\"divListForm\">\r\n" +
                    " <div class=\"ibox float-e-margins\"> \r\n" +
                    "     <div class=\"ibox-title\"> \r\n" +
                    "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách Báo cáo tổng kết công tác đào tạo") + " </h5> \r\n" +
                    "     </div> \r\n" +
                    "     <div id=\"divDT_BcTongKetCongTacDaoTaos\">\r\n" +
                    DrawSearchResult(ORenderInfo, 0, (int)FilterBase.eLoai.TrongNgay, null, null).HtmlContent +
                    "     </div>\r\n" +
                    " </div> \r\n" +
                    "</div>\r\n" +
                    "<div id=\"divActionForm\" style=\"display:none\"></div>\r\n");
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
        public static AjaxOut DrawSearchResult(RenderInfoCls ORenderInfo, int PageIndex, byte? loaiThoiGian, string tuNgay, string denNgay)
        {
            FilterBase filter = loaiThoiGian == null ? null : new FilterBase(loaiThoiGian, tuNgay, denNgay);
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OwnerUserCls user = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, userId);
                //Quyền dữ liệu: Những người sau được truy cập báo cáo:
                //1. Người tạo.
                //3. Người được phân quyền xem/sửa/xóa trên báo cáo của người khác.
                //string dataPermissionQuery = string.Format(" and (DT_BcTongKetCongTacDaoTao.NGUOITAO_ID = '{0}' " +
                //                                                " OR {5} = 1 " +
                //                                                " OR ( " +
                //                                                "        select count(1) " +
                //                                                "        from TablePermissionDataAccess join TablePermissionData on nvl(TablePermissionDataAccess.AllowAccess,0)=1 and TablePermissionData.frkOwnerUserId=DT_BcTongKetCongTacDaoTao.NGUOITAO_ID and PermissionDataId=frkPermissionDataId " +
                //                                                "                                        join TablePermissionFunction on PermissionFunctionId=TablePermissionData.frkPermissionFunctionId " +
                //                                                "                                        join TablePermissionFunctionItem on TablePermissionFunctionItem.frkPermissionFunctionId=PermissionFunctionId and PermissionFunctionItemId=frkPermissionFunctionItemId " +
                //                                                "        where PermissionFunctionCode='{1}' " +
                //                                                "              and (PermissionFunctionItemCode='{2}' or PermissionFunctionItemCode='{3}' or PermissionFunctionItemCode='{4}') " +
                //                                                "              and ( " +
                //                                                "                   TablePermissionDataAccess.frkOwnerUserId='{0}'" +//user
                //                                                "                   or TablePermissionDataAccess.frkRoleId in (select frkRoleId from TableOwnerUserBelongRole where frkOwnerUserId='{0}') " +//--role
                //                                                "                   or TablePermissionDataAccess.frkGroupRoleId in " +//GroupRole
                //                                                "                      ( " +
                //                                                "                           select frkGroupRoleId from TableRoleBelongGroupRole " +
                //                                                "                           where frkRoleId in  " +
                //                                                "                               ( " +
                //                                                "                                   select frkRoleId from TableOwnerUserBelongRole  " +
                //                                                "                                   where frkOwnerUserId='{0}' " +
                //                                                "                               ) " +
                //                                                "                      ) " +
                //                                                "              ) " +
                //                                                "  ) > 0) ", userId,
                //                                                            new DT_BcTongKetCongTacDaoTaoPermission().PermissionFunctionCode,
                //                                                            DT_BcTongKetCongTacDaoTaoCls.ePermission.Xem.ToString(),
                //                                                            DT_BcTongKetCongTacDaoTaoCls.ePermission.Sua.ToString(),
                //                                                            DT_BcTongKetCongTacDaoTaoCls.ePermission.Xoa.ToString(),
                //                                                            user.IsSystemAdmin);

                DT_BcTongKetCongTacDaoTaoFilterCls bcFilter = new DT_BcTongKetCongTacDaoTaoFilterCls()
                {
                    PageIndex = PageIndex,
                    PageSize = 20,
                    TuNgay = filter == null ? null : (DateTime?)filter.TuNgayValue,
                    DenNgay = filter == null ? null : (DateTime?)filter.ToiNgayValue,
                   // DataPermissionQuery = dataPermissionQuery
                };
                long recordTotal = 0;
                DT_BcTongKetCongTacDaoTaoCls[] DT_BcTongKetCongTacDaoTaos = CallBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().PageReading(ORenderInfo, bcFilter, ref recordTotal);
                int DT_BcTongKetCongTacDaoTaoTotal = DT_BcTongKetCongTacDaoTaos.Count();
                ReturnPaging RetPaging = WebPaging.GetPaging((int)recordTotal, bcFilter.PageIndex, bcFilter.PageSize, 10, "NextPage");
                string Html = "";
                if (DT_BcTongKetCongTacDaoTaoTotal == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Không có dữ liệu theo điều kiện lọc"), true);
                }
                else
                {
                    Html +=
                        "         <div class=\"table-responsive col-xs-12\" style='margin-top: 5px;'> \r\n" +
                        "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\">STT</th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Từ ngày") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Đến ngày") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Người tạo") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ngày lập") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Người sửa") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Ngày sửa") + " </th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < DT_BcTongKetCongTacDaoTaoTotal; iIndex++)
                    {
                        var DT_BcTongKetCongTacDaoTaoUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "DT_BcTongKetCongTacDaoTao",
                            new WebParamCls[] { new WebParamCls("Id", DT_BcTongKetCongTacDaoTaos[iIndex].ID) });
                        OwnerUserCls nguoiTao = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, DT_BcTongKetCongTacDaoTaos[iIndex].NGUOITAO_ID);
                        OwnerUserCls nguoiSua = string.IsNullOrEmpty(DT_BcTongKetCongTacDaoTaos[iIndex].NGUOISUA_ID) ? null : CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, DT_BcTongKetCongTacDaoTaos[iIndex].NGUOISUA_ID);

                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_BcTongKetCongTacDaoTaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở báo cáo") + "'>" + (bcFilter.PageIndex * bcFilter.PageSize + iIndex + 1).ToString("#,##0") + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_BcTongKetCongTacDaoTaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở báo cáo") + "'>" + DT_BcTongKetCongTacDaoTaos[iIndex].TUNGAY.ToString("dd/MM/yyyy") + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_BcTongKetCongTacDaoTaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở báo cáo") + "'>" + DT_BcTongKetCongTacDaoTaos[iIndex].DENNGAY.ToString("dd/MM/yyyy") + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + DT_BcTongKetCongTacDaoTaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở báo cáo") + "'>" + (nguoiTao != null ? nguoiTao.FullName : null) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_BcTongKetCongTacDaoTaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở báo cáo") + "'>" + DT_BcTongKetCongTacDaoTaos[iIndex].NGAYTAO.ToString("dd/MM/yyyy") + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + DT_BcTongKetCongTacDaoTaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở báo cáo") + "'>" + (nguoiSua != null ? nguoiSua.FullName : null) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + DT_BcTongKetCongTacDaoTaoUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở báo cáo") + "'>" + (DT_BcTongKetCongTacDaoTaos[iIndex].NGAYSUA == null ? null : DT_BcTongKetCongTacDaoTaos[iIndex].NGAYSUA.Value.ToString("dd/MM/yyyy")) + "</a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "         <div class=\"col-md-12\">\r\n" +
                        "              <div class=\"col-md-1\" style=\"margin-top:26px;padding-left: 0px;\">\r\n" +
                        "               <lable>" + WebLanguage.GetLanguage(OSiteParam, "Tổng số") + ": " + (bcFilter.PageIndex * bcFilter.PageSize + DT_BcTongKetCongTacDaoTaoTotal).ToString("#,##0") + "" + "/ " + "" + (int)recordTotal + "</lable>\r\n" +
                        "              </div>\r\n" +
                        "             <div class=\"col-md-10\" style=\"margin-top:20px;\">\r\n" +
                        RetPaging.PagingText +
                        "             </div>\r\n" +
                        "         </div>\r\n" +
                        "       </div>\r\n" +
                        "<style>\r\n" +
                        "table th{text-align: center; vertical-align: middle;}\r\n" +
                        "</style>\r\n"
                        ;
                }
                Html = WebEnvironments.ProcessHtml(Html);
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
