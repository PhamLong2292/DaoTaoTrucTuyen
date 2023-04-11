using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Linq;
using OneTSQ.CheckPermissionUtility;
using OneTSQ.Permission;
using OneTSQ.Core.Call.Bussiness.Utility;
using System.Web;
using System.IO;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    public class CaBenhs : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "CaBenhs";
            }
        }
        public override string WebPartTitle
        {
            get
            {
                return "Danh sách ca bệnh hội chẩn";
            }
        }
        public override string Description
        {
            get
            {
                return "Danh sách ca bệnh hội chẩn";

            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CaBenhs), Page);
        }

        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true; //PermissionUtility.CheckPermission(ORenderInfo, new CaBenhPermission().PermissionFunctionCode, OneTSQ.Common.CaBenh.ePermission.Xem.ToString(), new CaBenhPermission().PermissionFunctionCode, CaBenhPermission.StaticPermissionFunctionId, UserId);
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            int pageIndex = string.IsNullOrEmpty(WebEnvironments.Request("PageIndex")) ? 0 : int.Parse(WebEnvironments.Request("PageIndex"));
            string keyword = WebEnvironments.Request("Keyword");
            string cskcbYeuCau = WebEnvironments.Request("CskcbYeuCau");
            string chuyenKhoa = WebEnvironments.Request("ChuyenKhoa");
            byte loaiThoiGian = string.IsNullOrEmpty(WebEnvironments.Request("LoaiThoiGian")) ? (byte)FilterBase.eLoai.TrongThang : Byte.Parse(WebEnvironments.Request("LoaiThoiGian"));
            string tuNgay = WebEnvironments.Request("TuNgay");
            string denNgay = WebEnvironments.Request("DenNgay");
            int? trangThai = string.IsNullOrEmpty(WebEnvironments.Request("TrangThai")) ? null : (int?)int.Parse(WebEnvironments.Request("TrangThai"));
            if (trangThai < 0)
                trangThai = null;
            try
            {
                string cbbChuyenKhoa = "";
                if (!string.IsNullOrEmpty(chuyenKhoa))
                {
                    DM_ChuyenKhoaDaoTaoTtCls oChuyenKhoa = CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().CreateModel(ORenderInfo, chuyenKhoa); ;
                    if (oChuyenKhoa != null)
                        cbbChuyenKhoa += string.Format("<option value={0}>{0} - {1}</option>", oChuyenKhoa.Ma, oChuyenKhoa.Ten);
                }

                string cbbDonViThamVan = "";
                if (!string.IsNullOrEmpty(cskcbYeuCau))
                {               
                    var benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), cskcbYeuCau);
                    if (benhVien != null)
                        cbbDonViThamVan += string.Format("<option value={0}>{0} - {1}</option>\r\n", benhVien.Ma, benhVien.Ma);
                    else
                        cbbDonViThamVan += string.Format("<option value={0}>{0}</option>\r\n", cskcbYeuCau);
                }

                string cbbTrangThai = "";
                cbbTrangThai += string.Format("<option value={0} {1}>{2}</option>", (int)CaBenhCls.eTrangThai.ChoHoiChan, (trangThai == (int)CaBenhCls.eTrangThai.ChoHoiChan ? "selected" : ""), CaBenhParser.TrangThais[(int)CaBenhCls.eTrangThai.ChoHoiChan]);
                cbbTrangThai += string.Format("<option value={0} {1}>{2}</option>", (int)CaBenhCls.eTrangThai.DangHoiChan, (trangThai == (int)CaBenhCls.eTrangThai.DangHoiChan ? "selected" : ""), CaBenhParser.TrangThais[(int)CaBenhCls.eTrangThai.DangHoiChan]);
                cbbTrangThai += string.Format("<option value={0} {1}>{2}</option>", (int)CaBenhCls.eTrangThai.KetThuc, (trangThai == (int)CaBenhCls.eTrangThai.KetThuc ? "selected" : ""), CaBenhParser.TrangThais[(int)CaBenhCls.eTrangThai.KetThuc]);

                #region Javascript
                RetAjaxOut.HtmlContent =
                    WebEnvironments.ProcessJavascript(
                     "<script language=\"javascript\">\r\n" +
                    "   $(document).ready(function() {\r\n" +
                    "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Danh sách ca bệnh hội chẩn") + "';\r\n" +
                    "       $('.selectpicker').selectpicker({ \r\n" +
                    "       }); \r\n" +
                    "       $('#cbbChuyenKhoa').html('" + cbbChuyenKhoa + "');\r\n" +
                    "       $('#cbbDonViThamVan').html('" + cbbDonViThamVan + "');\r\n" +
                    "       CallInitSelect2('cbbChuyenKhoa', '" + WebEnvironments.GetRemoteProcessDataUrl(ChuyenKhoaDaoTaoTtService.StaticServiceId) + "','Chuyên khoa');\r\n" +
                    "       CallInitSelect2('cbbDonViThamVan', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerService.StaticServiceId) + "', 'Đơn vị tham vấn');\r\n" +
                    "       cbbTrangThai.innerHTML = '" + cbbTrangThai + "';\r\n" +
                    "       $(\"#cbbTrangThai\").select2({\r\n" +
                    "          placeholder: \"Trạng thái\",\r\n" +
                    "          allowClear: true\r\n" +
                    "      });\r\n" +
                    "       $('#cbbTrangThai').select2('val', " + (trangThai == null ? "null" : trangThai.ToString()) + ");\r\n" +
                    "       $('#txtKeyword').val('" + keyword + "');\r\n" +
                    "       $('#cbbLoai').select2('val', " + loaiThoiGian + ");\r\n" +
                    "       $('#dtTuNgay').val('" + tuNgay + "');\r\n" +
                    "       $('#dtDenNgay').val('" + denNgay + "');\r\n" +
                    "       $(\"#cbbLoai\").select2({\r\n" +
                    "          placeholder: \"Thời gian\",\r\n" +
                    "          allowClear: false\r\n" +
                    "      });\r\n" +
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
                    "       keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       cskcbYeuCau = document.getElementById('cbbDonViThamVan').value;\r\n" +
                    "       chuyenKhoa = document.getElementById('cbbChuyenKhoa').value;\r\n" +
                    "       trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                    "       loaiThoiGian = parseInt(document.getElementById('cbbLoai').value);\r\n" +
                    "       tuNgay = document.getElementById('dtTuNgay').value;\r\n" +
                    "       denNgay = document.getElementById('dtDenNgay').value;\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.CaBenhs.DrawSearchResult(RenderInfo, CurrentPageIndex, keyword, cskcbYeuCau, chuyenKhoa, trangThai, loaiThoiGian, tuNgay, denNgay).value;\r\n" +
                    "       document.getElementById('divCaBenhs').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +
                    "   function FilterChange(){\r\n" +
                    "       CurrentPageIndex = 0;\r\n" +
                    "       Search();\r\n" +
                    "   }\r\n" +

                #region Refresh danh sách ca bệnh
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
                    "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách ca bệnh hội chẩn") + " </h5> \r\n" +
                    "     </div> \r\n" +
                    "     <div class=\"ibox-content\"> \r\n" +
                    "           <div id=\"divCaBenhs\">" + CaBenhs.DrawSearchResult(ORenderInfo, pageIndex, keyword, cskcbYeuCau, chuyenKhoa, trangThai, loaiThoiGian, tuNgay, denNgay).HtmlContent + "</div>\r\n" +
                    "     </div> \r\n" +
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
        public static AjaxOut DrawSearchResult(RenderInfoCls ORenderInfo, int PageIndex, string keyword, string cskcbYeuCau, string chuyenKhoaMa, int? trangThai, byte loaiThoiGian, string tuNgay, string denNgay)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                OwnerUserCls user = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, userId);
                bool xemPermission = PermissionUtility.CheckPermission(ORenderInfo, new CaBenhPermission().PermissionFunctionCode, OneTSQ.Common.TuVanCaBenh.ePermission.Xem.ToString(), new CaBenhPermission().PermissionFunctionCode, CaBenhPermission.StaticPermissionFunctionId, userId);
                bool tiepNhanPermission = PermissionUtility.CheckPermission(ORenderInfo, new TuVanCaBenhPermission().PermissionFunctionCode, OneTSQ.Common.TuVanCaBenh.ePermission.TiepNhan.ToString(), new TuVanCaBenhPermission().PermissionFunctionCode, TuVanCaBenhPermission.StaticPermissionFunctionId, userId);
                bool lapLichPermission = PermissionUtility.CheckPermission(ORenderInfo, new TuVanCaBenhPermission().PermissionFunctionCode, OneTSQ.Common.TuVanCaBenh.ePermission.LapLich.ToString(), new TuVanCaBenhPermission().PermissionFunctionCode, TuVanCaBenhPermission.StaticPermissionFunctionId, userId);
                bool duyetHcPermission = PermissionUtility.CheckPermission(ORenderInfo, new TuVanCaBenhPermission().PermissionFunctionCode, OneTSQ.Common.TuVanCaBenh.ePermission.DuyetHoiChan.ToString(), new TuVanCaBenhPermission().PermissionFunctionCode, TuVanCaBenhPermission.StaticPermissionFunctionId, userId);
                //Quyền dữ liệu
                //1.Chỉ hiển thị ở 3 trạng thái chờ hội chẩn, đang hội chẩn, kết thúc.
                //2.Người được giữ vai trò chủ trì/ thư ký / duyệt lịch hội chẩn được xác định qua lịch hội chẩn hoặc biên bản hội chẩn được hiển thị ở mọi trạng thái.
                //3.Người được phân quyền tiếp nhận / lập lịch / duyệt hội chẩn trên ca bệnh tư vấn thuộc đơn tư vấn của ca bệnh được hiển thị ở mọi trạng thái.
                //4.Người được phân quyền xem/ tiếp nhận / lập lịch / duyệt hội chẩn trên ca bệnh tư vấn của người tạo được hiển thị ở mọi trạng thái.
                //5.Người thuộc đơn vị tham vấn được xem những ca bệnh thuộc đơn vị mình
                string dataPermissionQuery = string.Format(" and (CABENH.TRANGTHAI={6} or CABENH.TRANGTHAI={7} or CABENH.TRANGTHAI={8}) " +
                                                           " and (" +
                                                                "  {12}=1 " +//Quản trị hệ thống
                                                                "  OR (" +// phê duyệt lịch hội chẩn
                                                                "       select count(1) " +
                                                                "       from LichHoiChan lhc join LichHoiChanCaBenh on LichHoiChanCaBenh.LichHoiChanId = lhc.ID " +
                                                                "       where LichHoiChanCaBenh.CaBenhId = CABENH.ID and lhc.TrangThai in ({9},{10},{11}) and lhc.DUYETBOI = '{0}' " +
                                                                "  ) > 0 " +
                                                                "  OR ( " +//chủ trì, thư ký qua lịch hội chẩn
                                                                "       select count(1) " +
                                                                "       from LichHoiChan lhc join LichHoiChanCaBenh on LichHoiChanCaBenh.LichHoiChanId = lhc.ID " +
                                                                "                            join BacSyOwnerUser bo on lhc.ChuTri = bo.BacSyId " +
                                                                "                            join BacSyOwnerUser bo1 on lhc.ThuKy = bo1.BacSyId " +
                                                                "       where LichHoiChanCaBenh.CaBenhId = CABENH.ID and lhc.TrangThai in ({9},{10},{11}) and (bo.OwnerUserId = '{0}' or bo1.OwnerUserId = '{0}') " +
                                                                "  ) > 0 " +
                                                                "  OR ( " +//chủ trì, thư ký qua biên bản hội chẩn
                                                                "       select count(1) " +
                                                                "       from BienBanHoiChan bbhc join BacSyOwnerUser bo on bbhc.ChuTriHoichan = bo.BacSyId " +
                                                                "                                join BacSyOwnerUser bo1 on bbhc.ThuKy = bo1.BacSyId " +
                                                                "       where bbhc.CABENHID = CABENH.ID and (bo.OwnerUserId = '{0}' or bo1.OwnerUserId = '{0}') " +
                                                                "  ) > 0 " +
                                                               // người thuộc đơn vị tham vấn được xem ca bệnh của đơn vị mình
                                                                "  OR CABENH.DONVITHAMVANID = '{13}' " +
                                                                "  OR ( " +//Tư vấn qua lịch hội chẩn
                                                                "       select count(1) " +
                                                                "       from LichHoiChan lhc join LichHoiChanCaBenh on LichHoiChanCaBenh.LichHoiChanId = lhc.ID " +
                                                                "                            join LapLichThanhVienHoiChan lltvhc on lltvhc.LichHoiChanId = lhc.ID " +
                                                                "                            join BacSyOwnerUser bo on lltvhc.BacSyId = bo.BacSyId " +
                                                                "       where LichHoiChanCaBenh.CaBenhId = CABENH.ID and lhc.TrangThai in ({9},{10},{11}) and (bo.OwnerUserId = '{0}')" +
                                                                "  ) > 0 " +
                                                                "  OR ( " +//Tư vấn qua biên bản hội chẩn
                                                                "       select count(1) " +
                                                                "       from BienBanHoiChan bbhc join ChuyenGiaHoiChan cghc on cghc.BienBanHoiChanId = bbhc.Id " +
                                                                "                           join BacSyOwnerUser bo on cghc.BacSyId = bo.BacSyId " +
                                                                "       where bbhc.CABENHID = CABENH.ID and bo.OwnerUserId = '{0}'" +
                                                                "  ) > 0 " +
                                                                (tiepNhanPermission || lapLichPermission || duyetHcPermission ?
                                                                "  OR ( " +
                                                                "       select count(1) " +
                                                                "       from TableOwnerUserBelongOwner " +
                                                                "       where frkOwnerUserId='{0}'and frkOwnerId=CABENH.DONVITUVANID " +
                                                                "  ) > 0 " : null) +
                                                                "  OR ( " +
                                                                "        select count(1) " +
                                                                "        from TablePermissionDataAccess join TablePermissionData on nvl(TablePermissionDataAccess.AllowAccess,0)=1 and TablePermissionData.frkOwnerUserId=CABENH.TAOBOI and PermissionDataId=frkPermissionDataId " +
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
                                                                "                                   where frkOwnerUserId='{0}'" +
                                                                "                               ) " +
                                                                "                      ) " +
                                                                "              ) " +
                                                                "  ) > 0) ", userId,//0
                                                                            new TuVanCaBenhPermission().PermissionFunctionCode,//1
                                                                            Common.TuVanCaBenh.ePermission.Xem.ToString(),//2
                                                                            Common.TuVanCaBenh.ePermission.TiepNhan.ToString(),//3
                                                                            Common.TuVanCaBenh.ePermission.LapLich.ToString(),//4
                                                                            Common.TuVanCaBenh.ePermission.DuyetHoiChan.ToString(),//5
                                                                            (int)CaBenhCls.eTrangThai.ChoHoiChan,//6
                                                                            (int)CaBenhCls.eTrangThai.DangHoiChan,//7
                                                                            (int)CaBenhCls.eTrangThai.KetThuc,//8
                                                                            (int)LichHoiChanCls.eTrangThai.DaDuyet,//9
                                                                            (int)LichHoiChanCls.eTrangThai.HoanTat,//10
                                                                            (int)LichHoiChanCls.eTrangThai.Huy,//11
                                                                            user.IsSystemAdmin,// 12
                                                                            user.OwnerId);//13

                FilterBase timeFilter = new FilterBase(loaiThoiGian, tuNgay, denNgay);
                CaBenhFilterCls filter = new CaBenhFilterCls()
                {
                    PageIndex = PageIndex,
                    PageSize = 20,
                    Keyword = keyword,
                    DONVITHAMVANID = cskcbYeuCau,
                    CHUYENKHOAMA = chuyenKhoaMa,
                    TRANGTHAI = trangThai,
                    TUNGAY = timeFilter.TuNgayValue,
                    DENNGAY = timeFilter.ToiNgayValue,
                    NGAYLOC = "DUYETHOICHANVAO",
                    DataPermissionQuery = dataPermissionQuery
                };
                long recordTotal = 0;
                CaBenhCls[] caBenhs = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().PageReading(ORenderInfo, filter, ref recordTotal);
                int benhAnQuanlity = caBenhs.Count();
                ReturnPaging RetPaging = WebPaging.GetPaging((int)recordTotal, filter.PageIndex, filter.PageSize, 10, "NextPage");
                string Html = "";
                if (benhAnQuanlity == 0)
                {
                    Html += WebScreen.GetPanelInfo(OSiteParam, WebLanguage.GetLanguage(OSiteParam, "Không có dữ liệu theo điều kiện lọc"), true);
                }
                else
                {
                    Html +=
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th class=\"th-func-20\">STT</th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã BN") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Họ tên BN") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "NS") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "GT") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Loại hội chẩn") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Chuyên khoa") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị tham vấn") + " </th> \r\n" +
                        "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    int sodong = 0;
                    for (int iIndex = 0; iIndex < benhAnQuanlity; iIndex++)
                    {
                        sodong++;
                        var caBenhUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "CaBenh", new WebParamCls[] {
                            new WebParamCls("CaBenhId", caBenhs[iIndex].ID),
                            new WebParamCls("RecordTotal", recordTotal.ToString()),
                            new WebParamCls("ChiSoMang", filter.PageIndex * filter.PageSize + iIndex),
                            new WebParamCls("Keyword", keyword),
                            new WebParamCls("CskcbYeuCau", cskcbYeuCau),
                            new WebParamCls("ChuyenKhoa", chuyenKhoaMa),
                            new WebParamCls("LoaiThoiGian", loaiThoiGian),
                            new WebParamCls("TuNgay", tuNgay),
                            new WebParamCls("DenNgay", denNgay),
                            new WebParamCls("TrangThai", trangThai == null ? -1 : trangThai.Value)});
                        OwnerCls donViThamVan = string.IsNullOrEmpty(caBenhs[iIndex].DONVITHAMVANID) ? null : CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, caBenhs[iIndex].DONVITHAMVANID);
                        DM_ChuyenKhoaDaoTaoTtCls chuyenKhoa = null;
                        if (!string.IsNullOrEmpty(caBenhs[iIndex].CHUYENKHOAMA))
                        {
                            chuyenKhoa = CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().CreateModel(ORenderInfo, caBenhs[iIndex].CHUYENKHOAMA);
                        }
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td class=\"td-right\" style='text-align: center; vertical-align: middle;'><a href='" + caBenhUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở ca bệnh") + "' " + (caBenhs[iIndex].CAPCUU == 1 ? "style='color: red;'" : null) + ">" + (filter.PageIndex * filter.PageSize + iIndex + 1).ToString("#,##0") + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + caBenhUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở ca bệnh") + "' " + (caBenhs[iIndex].CAPCUU == 1 ? "style='color: red;'" : null) + ">" + caBenhs[iIndex].MABN + "</td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + caBenhUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở ca bệnh") + "' " + (caBenhs[iIndex].CAPCUU == 1 ? "style='color: red;'" : null) + ">" + caBenhs[iIndex].HOTENBN_ENCODED + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + caBenhUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở ca bệnh") + "' " + (caBenhs[iIndex].CAPCUU == 1 ? "style='color: red;'" : null) + ">" + (caBenhs[iIndex].NGAYSINH != null ? (int?)caBenhs[iIndex].NGAYSINH.Value.Year : null) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + caBenhUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở ca bệnh") + "' " + (caBenhs[iIndex].CAPCUU == 1 ? "style='color: red;'" : null) + ">" + (caBenhs[iIndex].GIOITINH == 1 ? WebLanguage.GetLanguage(OSiteParam, "Nam") : caBenhs[iIndex].GIOITINH == 2 ? WebLanguage.GetLanguage(OSiteParam, "Nữ") : null) + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + caBenhUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở ca bệnh") + "' " + (caBenhs[iIndex].CAPCUU == 1 ? "style='color: red;'" : null) + ">" + CaBenhParser.LoaiHoichans[caBenhs[iIndex].LOAIHOICHAN] + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + caBenhUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở ca bệnh") + "' " + (caBenhs[iIndex].CAPCUU == 1 ? "style='color: red;'" : null) + ">" + (chuyenKhoa != null ? chuyenKhoa.Ten : caBenhs[iIndex].CHUYENKHOAMA) + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + caBenhUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở ca bệnh") + "' " + (caBenhs[iIndex].CAPCUU == 1 ? "style='color: red;'" : null) + ">" + (donViThamVan != null ? donViThamVan.OwnerName : null) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'>" + (caBenhs[iIndex].TRANGTHAI != null ? CaBenhParser.sColorTrangThai[caBenhs[iIndex].TRANGTHAI.Value] : null) + "</td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                        "                 </tbody> \r\n" +
                        "             </table> \r\n" +
                        "         <div class=\"col-md-12\">\r\n" +
                        "              <div class=\"col-md-1\" style=\"margin-top:26px;padding-left: 0px;\">\r\n" +
                        "               <lable>" + WebLanguage.GetLanguage(OSiteParam, "Tổng số") + ": " + (filter.PageIndex * filter.PageSize + sodong).ToString("#,##0") + "" + "/ " + "" + (int)recordTotal + "</lable>\r\n" +
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
                return "<div class=\"col-md-3\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                       "    <input id=\"txtKeyword\" placeholder=\"Tìm kiếm theo mã BN, họ tên BN\" onkeypress=\"timkiem(event);\" class=\"form-control valueForm\" style=\"border-radius: 4px;\">\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                       "    <select id=\"cbbChuyenKhoa\" class=\"form-control khoaphong_select valueForm\" style=\"border-radius: 4px;\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                       "    <select id=\"cbbDonViThamVan\" class=\"form-control khoaphong_select valueForm\" style=\"border-radius: 4px;\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top:12px; min-width: 180px;\">\r\n" +
                       "    <select id=\"cbbTrangThai\" class=\"form-control khoaphong_select valueForm\" style=\"border-radius: 4px;\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n"
                ;
            }
        }
        #endregion
    }
}
