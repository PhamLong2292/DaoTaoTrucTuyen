using FlexCel.Report;
using OneMES3.DM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.ReportUtility;
using OneTSQ.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts.Report
{
    [ObjectReport(ID = "3340FF8C-D9CD-4A9E-B60F-20C8BA5D8E81", Type = OneTSQ.ReportUtility.Report.eType.Flexcel, Name = "DT_BcKetQuaChuyenGiaoCuaCanBo", Title = "Báo cáo kết quả chuyển giao", ReportPath = @"\ReportTemplates\DT_BcKetQuaChuyenGiaoCuaCanBo.xlsx")]
    class DT_BcKetQuaChuyenGiaoCuaCanBo : ObjectReport<DT_ChuyenGiao>
    {
        protected override FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, string lichChuyenGiaoId)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

            DT_LichChuyenGiaoCls lichChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
            DT_LichChuyenGiaoChiTietCls[] lichChuyenGiaoChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Reading(ORenderInfo, new DT_LichChuyenGiaoChiTietFilterCls() { LICHCHUYENGIAO_ID = lichChuyenGiaoId });
            DT_KetQuaChuyenGiaoCls ketQuaChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
            if (ketQuaChuyenGiao == null)
                ketQuaChuyenGiao = new DT_KetQuaChuyenGiaoCls()
                {
                    SOCAHUONGDAN = lichChuyenGiaoChiTiets.Sum(o => o.SOCAHUONGDAN),
                    SOCAHOTRO = lichChuyenGiaoChiTiets.Sum(o => o.SOCAHOTRO)
                };
            DM_KyThuatChuyenGiaoCls kyThuatChuyenGiao = string.IsNullOrEmpty(lichChuyenGiao.KYTHUAT_MA) ? null : CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiao.KYTHUAT_MA);

            OneMES3.DM.Model.BenhVienCls benhVien = null;
            if (!string.IsNullOrEmpty(lichChuyenGiao.BENHVIEN_MA))
            {
                benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), lichChuyenGiao.BENHVIEN_MA);

            }

            BacSyCls canBoChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichChuyenGiao.BACSY_ID);

            OneMES3.DM.Model.ChuyenMonCls chuyenMon = null;
            if (canBoChuyenGiao != null && !string.IsNullOrEmpty(canBoChuyenGiao.CHUYENMONMA))
            {
                chuyenMon = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenMonProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), canBoChuyenGiao.CHUYENMONMA);
            }

            OneMES3.DM.Model.ChuyenNganhCls chuyenNganh = null;
            if (canBoChuyenGiao != null && !string.IsNullOrEmpty(canBoChuyenGiao.CHUYENMONMA))
            {
                chuyenNganh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenNganhProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), canBoChuyenGiao.CHUYENNGANHMA);
            }

            OneMES3.DM.Model.DonViCongTacCls donViCongTac = null;
            if (canBoChuyenGiao != null && !string.IsNullOrEmpty(canBoChuyenGiao.DONVIMA))
            {
                donViCongTac = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateDonViCongTacProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), canBoChuyenGiao.DONVIMA);
            }
            FlexCelReport flexCelReport = new FlexCelReport();
            flexCelReport.SetValue("HoTen", canBoChuyenGiao == null ? null : canBoChuyenGiao.HOTEN);
            flexCelReport.SetValue("ChuyenMon", chuyenMon == null ? null : chuyenMon.Ten);
            flexCelReport.SetValue("ChuyenNganh", chuyenNganh == null ? null : chuyenNganh.Ten);
            flexCelReport.SetValue("DonViCongTac", donViCongTac == null ? null : donViCongTac.Ten);
            flexCelReport.SetValue("BenhVienNhanChuyenGiao", benhVien == null ? null : benhVien.Ten);
            flexCelReport.SetValue("SoQD", ketQuaChuyenGiao.SOQD);
            flexCelReport.SetValue("NgayQD", string.Format("{0} {1} {2} {3} {4} {5}", WebLanguage.GetLanguage(OSiteParam, "ngày"), ketQuaChuyenGiao.NGAYQD == null ? null : ketQuaChuyenGiao.NGAYQD.Value.ToString("dd"),
                                                                                      WebLanguage.GetLanguage(OSiteParam, "tháng"), ketQuaChuyenGiao.NGAYQD == null ? null : ketQuaChuyenGiao.NGAYQD.Value.ToString("MM"),
                                                                                      WebLanguage.GetLanguage(OSiteParam, "năm"), ketQuaChuyenGiao.NGAYQD == null ? null : ketQuaChuyenGiao.NGAYQD.Value.ToString("yyyy")));
            flexCelReport.SetValue("TuNgay", string.Format("{0} {1} {2} {3} {4} {5}", WebLanguage.GetLanguage(OSiteParam, "ngày"), ketQuaChuyenGiao.TUNGAY == null ? null : ketQuaChuyenGiao.TUNGAY.Value.ToString("dd"),
                                                                                      WebLanguage.GetLanguage(OSiteParam, "tháng"), ketQuaChuyenGiao.TUNGAY == null ? null : ketQuaChuyenGiao.TUNGAY.Value.ToString("MM"),
                                                                                      WebLanguage.GetLanguage(OSiteParam, "năm"), ketQuaChuyenGiao.TUNGAY == null ? null : ketQuaChuyenGiao.TUNGAY.Value.ToString("yyyy")));
            flexCelReport.SetValue("DenNgay", string.Format("{0} {1} {2} {3} {4} {5}", WebLanguage.GetLanguage(OSiteParam, "ngày"), ketQuaChuyenGiao.DENNGAY == null ? null : ketQuaChuyenGiao.DENNGAY.Value.ToString("dd"),
                                                                                      WebLanguage.GetLanguage(OSiteParam, "tháng"), ketQuaChuyenGiao.DENNGAY == null ? null : ketQuaChuyenGiao.DENNGAY.Value.ToString("MM"),
                                                                                      WebLanguage.GetLanguage(OSiteParam, "năm"), ketQuaChuyenGiao.DENNGAY == null ? null : ketQuaChuyenGiao.DENNGAY.Value.ToString("yyyy")));
            flexCelReport.SetValue("KyThuatChuyenGiao", kyThuatChuyenGiao == null ? null : kyThuatChuyenGiao.Ten);
            flexCelReport.SetValue("SoLuotBenhNhan", ketQuaChuyenGiao.SOLUOTBENHNHAN);
            flexCelReport.SetValue("SoCaHuongDan", ketQuaChuyenGiao.SOCAHUONGDAN);
            flexCelReport.SetValue("SoCaHoTro", ketQuaChuyenGiao.SOCAHOTRO);
            flexCelReport.SetValue("SoGioThamGia", ketQuaChuyenGiao.SOGIOTHAMGIA == null ? null : ketQuaChuyenGiao.SOGIOTHAMGIA.Value.ToString("#,##0"));
            flexCelReport.SetValue("ChapHanhThoiGian", ketQuaChuyenGiao.CHAPHANHTHOIGIAN);
            flexCelReport.SetValue("ChapHanhQuyChe", ketQuaChuyenGiao.CHAPHANHQUYCHE);
            flexCelReport.SetValue("PhoiHop", ketQuaChuyenGiao.PHOIHOP);
            flexCelReport.SetValue("HtNhiemVuXs", ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.XuatSac ? "x" : null);
            flexCelReport.SetValue("HtNhiemVuT", ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.Tot ? "x" : null);
            flexCelReport.SetValue("HtNhiemVuBt", ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.BinhThuong ? "x" : null);
            flexCelReport.SetValue("HtNhiemVuKht", ketQuaChuyenGiao.HTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.KhongHoanThanh ? "x" : null);
            flexCelReport.SetValue("DeXuatThoiGian", ketQuaChuyenGiao.DEXUATTHOIGIAN);
            flexCelReport.SetValue("DeXuatCheDo", ketQuaChuyenGiao.DEXUATCHEDO);
            flexCelReport.SetValue("DeXuatDieuKien", ketQuaChuyenGiao.DEXUATDIEUKIEN);
            flexCelReport.SetValue("ThoiGianBaoCao", string.Format("{0} {1} {2} {3} {4} {5}", WebLanguage.GetLanguage(OSiteParam, "ngày"), ketQuaChuyenGiao.THOIGIANBAOCAO == null ? null : ketQuaChuyenGiao.THOIGIANBAOCAO.Value.ToString("dd"),
                                                                                      WebLanguage.GetLanguage(OSiteParam, "tháng"), ketQuaChuyenGiao.THOIGIANBAOCAO == null ? null : ketQuaChuyenGiao.THOIGIANBAOCAO.Value.ToString("MM"),
                                                                                      WebLanguage.GetLanguage(OSiteParam, "năm"), ketQuaChuyenGiao.THOIGIANBAOCAO == null ? null : ketQuaChuyenGiao.THOIGIANBAOCAO.Value.ToString("yyyy")));
            flexCelReport.SetValue("NxTinhThanThaiDoYThuc", ketQuaChuyenGiao.NXTINHTHANTHAIDOYTHUC);
            flexCelReport.SetValue("NxKhaNangThucHienDocLap", ketQuaChuyenGiao.NXKHANANGTHUCHIENDOCLAP);
            flexCelReport.SetValue("NxDungYcDeXuat", ketQuaChuyenGiao.NXDUNGYCDEXUAT);
            flexCelReport.SetValue("NxHtNhiemVuXs", ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.XuatSac ? "x" : null);
            flexCelReport.SetValue("NxHtNhiemVuT", ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.Tot ? "x" : null);
            flexCelReport.SetValue("NxHtNhiemVuBt", ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.BinhThuong ? "x" : null);
            flexCelReport.SetValue("NxHtNhiemVuKht", ketQuaChuyenGiao.NXMUCDOHTNHIEMVU == (int)DT_KetQuaChuyenGiaoCls.eHtNhiemVu.KhongHoanThanh ? "x" : null);
            flexCelReport.SetValue("DeXuatGiaiPhap", ketQuaChuyenGiao.DEXUATGIAIPHAP);
            flexCelReport.SetValue("NoiNhanXet", ketQuaChuyenGiao.NOINHANXET);
            flexCelReport.SetValue("NgayNhanXet", string.Format("{0} {1} {2} {3} {4} {5}", WebLanguage.GetLanguage(OSiteParam, "ngày"), ketQuaChuyenGiao.NGAYNHANXET == null ? null : ketQuaChuyenGiao.NGAYNHANXET.Value.ToString("dd"),
                                                                                      WebLanguage.GetLanguage(OSiteParam, "tháng"), ketQuaChuyenGiao.NGAYNHANXET == null ? null : ketQuaChuyenGiao.NGAYNHANXET.Value.ToString("MM"),
                                                                                      WebLanguage.GetLanguage(OSiteParam, "năm"), ketQuaChuyenGiao.NGAYNHANXET == null ? null : ketQuaChuyenGiao.NGAYNHANXET.Value.ToString("yyyy")));
            flexCelReport.SetValue("NguoiNhanXet", ketQuaChuyenGiao.NGUOINHANXET);

            return flexCelReport;
        }
    }
}