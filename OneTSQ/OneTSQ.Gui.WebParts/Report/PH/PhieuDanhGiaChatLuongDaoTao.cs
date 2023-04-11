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
    public class PhieuDanhGiaChatLuongDaoTaos : ListWebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "PhieuDanhGiaChatLuongDaoTaos";
            }
        }
        public override string WebPartTitle
        {
            get
            {
                return "Danh sách phiếu đánh giá chất lượng đào tạo";
            }
        }
        public override string Description
        {
            get
            {
                return "Danh sách phiếu đánh giá chất lượng đào tạo";

            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(PhieuDanhGiaChatLuongDaoTaos), Page);
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
                bool xemPermission = PermissionUtility.CheckPermission(ORenderInfo, new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Xem.ToString(), new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoPermission.StaticPermissionFunctionId, UserId);
                bool themPermission = PermissionUtility.CheckPermission(ORenderInfo, new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Them.ToString(), new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoPermission.StaticPermissionFunctionId, UserId);
                bool suaPermission = PermissionUtility.CheckPermission(ORenderInfo, new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Sua.ToString(), new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoPermission.StaticPermissionFunctionId, UserId);
                bool xoaPermission = PermissionUtility.CheckPermission(ORenderInfo, new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Xoa.ToString(), new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoPermission.StaticPermissionFunctionId, UserId);
                bool guiPermission = PermissionUtility.CheckPermission(ORenderInfo, new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Gui.ToString(), new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoPermission.StaticPermissionFunctionId, UserId);
                var trangThais = xemPermission || themPermission || suaPermission || xoaPermission || guiPermission ? PhieuDanhGiaChatLuongDaoTaoParser.TrangThais : PhieuDanhGiaChatLuongDaoTaoParser.TrangThais.Where(o => o.Key != (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.Moi);

                string cbbTrangThai = "";
                foreach (var tt in trangThais)
                    cbbTrangThai += "<option value=" + tt.Key + ">" + tt.Value + "</option>";

                #region Javascript
                RetAjaxOut.HtmlContent =
                    WebEnvironments.ProcessJavascript(
                     "<script language=\"javascript\">\r\n" +
                    "   $(document).ready(function() {\r\n" +
                    "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Danh sách phiếu đánh giá chất lượng đào tạo") + "';\r\n" +
                    "       CallInitSelect2('cbbBenhVienThamVan', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerService.StaticServiceId) + "', 'Đơn vị tham vấn');\r\n" +
                    "       $(\"#cbbQuy\").select2({\r\n" +
                    "           placeholder: '" + WebLanguage.GetLanguage(OSiteParam, "Quý") + "',\r\n" +
                    "           allowClear: true\r\n" +
                    "       });\r\n" +
                    "       $('.yearpicker').datetimepicker({ \r\n" +
                    "           format: 'YYYY', \r\n" +
                    "       }); \r\n" +
                    "       FilterChange();\r\n" +
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
                    "       cskcbYeuCau = document.getElementById('cbbBenhVienThamVan').value;\r\n" +
                    "       quy = parseInt(document.getElementById('cbbQuy').value);\r\n" +
                    "       nam = parseInt(document.getElementById('dtNam').value);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.PhieuDanhGiaChatLuongDaoTaos.DrawSearchResult(RenderInfo, CurrentPageIndex, cskcbYeuCau, quy, nam).value;\r\n" +
                    "       document.getElementById('divPhieuDanhGiaChatLuongDaoTaos').innerHTML=AjaxOut.HtmlContent;\r\n" +
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
                    "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách phiếu đánh giá chất lượng đào tạo") + " </h5> \r\n" +
                    "     </div> \r\n" +
                    "     <table width=100%  class=\"ibox-content\"> \r\n" +
                    "         <tr> \r\n" +
                    "             <td colspan=9> \r\n" +
                    "               <div id=\"divPhieuDanhGiaChatLuongDaoTaos\"></div>\r\n" +
                    "             </td> \r\n" +
                    "         </tr> \r\n" +
                    "     </table> \r\n" +
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
        public static AjaxOut DrawSearchResult(RenderInfoCls ORenderInfo, int PageIndex, string cskcbYeuCau, int? quy, int? nam)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OwnerUserCls user = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, userId);
                bool tongHopPermission = PermissionUtility.CheckPermission(ORenderInfo, new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoCls.ePermission.TongHop.ToString(), new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode, PhieuDanhGiaChatLuongDaoTaoPermission.StaticPermissionFunctionId, userId);
                //Quyền dữ liệu
                //1. Người tạo được hiển thị ở mọi trạng thái.
                //2. Người thuộc đơn vị tư vấn của phiếu và được phân quyền tổng hợp được hiển thị ở trạng thái khác trạng thái mới.
                //3. Người được phân quyền xem/sửa/xóa/gửi phiếu trên phiếu của người khác được hiển thị ở mọi trạng thái.
                string dataPermissionQuery = string.Format(" and (PhieuDanhGiaChatLuongDaoTao.TAOBOI = '{0}' " +
                                                                " OR {7} = 1 " +
                                                                (tongHopPermission ?
                                                                "  OR (PhieuDanhGiaChatLuongDaoTao.TRANGTHAI<>{6} " +
                                                                "   and ( " +
                                                                "        select count(1) " +
                                                                "        from TableOwnerUserBelongOwner " +
                                                                "        where (frkOwnerUserId='{0}')and frkOwnerId=PhieuDanhGiaChatLuongDaoTao.BenhVienTuVan_ID " +
                                                                "       ) > 0 " +
                                                                "  ) " : null) +
                                                                "  OR ( " +
                                                                "        select count(1) " +
                                                                "        from TablePermissionDataAccess join TablePermissionData on nvl(TablePermissionDataAccess.AllowAccess,0)=1 and TablePermissionData.frkOwnerUserId=PhieuDanhGiaChatLuongDaoTao.TAOBOI and PermissionDataId=frkPermissionDataId " +
                                                                "                                        join TablePermissionFunction on PermissionFunctionId=TablePermissionData.frkPermissionFunctionId " +
                                                                "                                        join TablePermissionFunctionItem on TablePermissionFunctionItem.frkPermissionFunctionId=PermissionFunctionId and PermissionFunctionItemId=frkPermissionFunctionItemId " +
                                                                "        where PermissionFunctionCode='{1}' " +
                                                                "              and (PermissionFunctionItemCode='{2}' or PermissionFunctionItemCode='{3}' or PermissionFunctionItemCode='{4}' or PermissionFunctionItemCode='{5}') " +
                                                                "              and ( " +
                                                                "                   TablePermissionDataAccess.frkOwnerUserId='{0}'" +//user
                                                                "                   or TablePermissionDataAccess.frkRoleId in (select frkRoleId from TableOwnerUserBelongRole where frkOwnerUserId='{0}') " +//--role
                                                                "                   or TablePermissionDataAccess.frkGroupRoleId in " +//GroupRole
                                                                "                      ( " +
                                                                "                           select frkGroupRoleId from TableRoleBelongGroupRole " +
                                                                "                           where frkRoleId in  " +
                                                                "                               ( " +
                                                                "                                   select frkRoleId from TableOwnerUserBelongRole  " +
                                                                "                                   where frkOwnerUserId='{0}' " +
                                                                "                               ) " +
                                                                "                      ) " +
                                                                "              ) " +
                                                                "  ) > 0) ", userId,
                                                                            new PhieuDanhGiaChatLuongDaoTaoPermission().PermissionFunctionCode,
                                                                            PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Xem.ToString(),
                                                                            PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Sua.ToString(),
                                                                            PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Xoa.ToString(),
                                                                            PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Gui.ToString(),
                                                                            (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.Moi,
                                                                            user.IsSystemAdmin);

                PhieuDanhGiaChatLuongDaoTaoFilterCls filter = new PhieuDanhGiaChatLuongDaoTaoFilterCls()
                {
                    PageIndex = PageIndex,
                    PageSize = 20,
                    BENHVIENTHAMVAN_ID = cskcbYeuCau,
                    QUY = quy,
                    NAM = nam,
                    DataPermissionQuery = dataPermissionQuery
                };
                long recordTotal = 0;
                PhieuDanhGiaChatLuongDaoTaoCls[] PhieuDanhGiaChatLuongDaoTaos = CallBussinessUtility.CreateBussinessProcess().CreatePhieuDanhGiaChatLuongDaoTaoProcess().PageReading(ORenderInfo, filter, ref recordTotal);
                int PhieuDanhGiaChatLuongDaoTaoTotal = PhieuDanhGiaChatLuongDaoTaos.Count();
                ReturnPaging RetPaging = WebPaging.GetPaging((int)recordTotal, filter.PageIndex, filter.PageSize, 10, "NextPage");
                string Html = "";
                if (PhieuDanhGiaChatLuongDaoTaoTotal == 0)
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
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Bệnh viện tham vấn") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Bệnh viện tư vấn") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Quý") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Năm") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < PhieuDanhGiaChatLuongDaoTaoTotal; iIndex++)
                    {
                        var PhieuDanhGiaChatLuongDaoTaoUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "PhieuDanhGiaChatLuongDaoTao",
                            new WebParamCls[] { new WebParamCls("Id", PhieuDanhGiaChatLuongDaoTaos[iIndex].ID) });
                        OwnerCls BenhVienThamVan = string.IsNullOrEmpty(PhieuDanhGiaChatLuongDaoTaos[iIndex].BENHVIENTHAMVAN_ID) ? null : CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, PhieuDanhGiaChatLuongDaoTaos[iIndex].BENHVIENTHAMVAN_ID);
                        OwnerCls BenhVienTuVan = string.IsNullOrEmpty(PhieuDanhGiaChatLuongDaoTaos[iIndex].BENHVIENTUVAN_ID) ? null : CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, PhieuDanhGiaChatLuongDaoTaos[iIndex].BENHVIENTUVAN_ID);

                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + PhieuDanhGiaChatLuongDaoTaoUrl + "' target='_blank' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (filter.PageIndex * filter.PageSize + iIndex + 1).ToString("#,##0") + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + PhieuDanhGiaChatLuongDaoTaoUrl + "' target='_blank' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (BenhVienThamVan != null ? BenhVienThamVan.OwnerName : null) + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + PhieuDanhGiaChatLuongDaoTaoUrl + "' target='_blank' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (BenhVienTuVan != null ? BenhVienTuVan.OwnerName : null) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + PhieuDanhGiaChatLuongDaoTaoUrl + "' target='_blank' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuDanhGiaChatLuongDaoTaoParser.Quys[PhieuDanhGiaChatLuongDaoTaos[iIndex].QUY] + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + PhieuDanhGiaChatLuongDaoTaoUrl + "' target='_blank' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuDanhGiaChatLuongDaoTaos[iIndex].NAM + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + PhieuDanhGiaChatLuongDaoTaoUrl + "' target='_blank' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (PhieuDanhGiaChatLuongDaoTaoParser.sColorTrangThai[PhieuDanhGiaChatLuongDaoTaos[iIndex].TRANGTHAI]) + "</a></td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "         <div class=\"col-md-12\">\r\n" +
                        "              <div class=\"col-md-1\" style=\"margin-top:26px;padding-left: 0px;\">\r\n" +
                        "               <lable>" + WebLanguage.GetLanguage(OSiteParam, "Tổng số") + ": " + (filter.PageIndex * filter.PageSize + PhieuDanhGiaChatLuongDaoTaoTotal).ToString("#,##0") + "" + "/ " + "" + (int)recordTotal + "</lable>\r\n" +
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

        #region Override Func Core
        public override string divFilter
        {
            get
            {
                int quyHienTai;
                if (DateTime.Now.Month <= 3)
                    quyHienTai = 1;
                else if (DateTime.Now.Month <= 6)
                    quyHienTai = 2;
                else if (DateTime.Now.Month <= 9)
                    quyHienTai = 3;
                else quyHienTai = 4;
                string cbbQuy = "<select id = 'cbbQuy'  class='form-control' >\r\n";
                foreach (var quy in BcDanhGiaChatLuongDaoTaoParser.Quys)
                    cbbQuy += string.Format("<option value={0} {1}>{2}</option>\r\n", quy.Key, quy.Key == quyHienTai ? "selected" : null, BcDanhGiaChatLuongDaoTaoParser.Quys[quy.Key]);
                cbbQuy += "</select>\r\n";
                return "<div class=\"col-md-1\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                       "    <select id=\"cbbBenhVienThamVan\" class=\"form-control\" style=\"border-radius: 4px;\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                            cbbQuy +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                       "    <input type='text' data-mask='9999' class='form-control yearpicker' id='dtNam' required  value='" + DateTime.Now.Year + "'>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                       "    <button id='btnTimKiem' class='btn btn-sm  mr-10px' onclick='FilterChange()' style='margin-top: 0px; height: 28px; background-color:#e26614; color:white;'><strong>Tìm kiếm</strong></button>\r\n" +
                       "</div>\r\n"
                ;
            }
        }
        #endregion
    }
}
