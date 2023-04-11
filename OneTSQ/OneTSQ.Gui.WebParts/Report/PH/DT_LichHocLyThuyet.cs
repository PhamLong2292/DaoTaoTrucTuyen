using FlexCel.Report;
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

namespace OneTSQ.WebParts
{
    [ObjectReport(ID = "07B3DE27-161A-4139-8E8A-589A43ACC66B", Type = OneTSQ.ReportUtility.Report.eType.Flexcel, Name = "DT_LichHocLyThuyet", Title = "Lịch học lý thuyết", ReportPath = @"\ReportTemplates\DT_LichHocLyThuyet.xlsx")]
    public class DT_LichHocLyThuyet : ObjectReport<DT_LopHoc>
    {
        private class LichLyThuyetChiTiet
        {
            public string Thu { get; set; }
            public DateTime Ngay { get; set; }
            public string TuGio { get; set; }
            public string DenGio { get; set; }
            public string NoiDung { get; set; }
            public string GiangVien { get; set; }
            public string GhiChu { get; set; }
        }
        protected override FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, string khoaHocId)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            FlexCelReport flexCelReport = new FlexCelReport();

            DT_LichLyThuyetCls lichLyThuyet = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetProcess().CreateModel(ORenderInfo, khoaHocId);
            OwnerUserCls nguoiTao = null;
            if (lichLyThuyet == null)
                lichLyThuyet = new DT_LichLyThuyetCls() { ID = khoaHocId };
            else nguoiTao = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, lichLyThuyet.NGUOITAO_ID);
            DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, khoaHocId);
            BacSyCls ptChuyenMon = string.IsNullOrEmpty(lichLyThuyet.PTCHUYENMON_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichLyThuyet.PTCHUYENMON_ID);
            BacSyCls lanhDao = string.IsNullOrEmpty(lichLyThuyet.LANHDAO_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichLyThuyet.LANHDAO_ID);

            var lichLyThuyetChiTiets = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichLyThuyetChiTietProcess().Reading(ORenderInfo, new DT_LichLyThuyetChiTietFilterCls() { LICHLYTHUYET_ID = lichLyThuyet.ID })
                .Select(o => new LichLyThuyetChiTiet
                {
                    Thu = GetDayOfWeek(OSiteParam, o.NGAY.DayOfWeek),
                    Ngay = o.NGAY,
                    TuGio = o.THOIGIAN == null ? null : o.THOIGIAN.Value.ToString("HH:mm"),
                    DenGio = o.THOIGIANKETTHUC == null ? null : o.THOIGIANKETTHUC.Value.ToString("HH:mm"),
                    NoiDung = o.NOIDUNG,
                    GiangVien = string.IsNullOrEmpty(o.GIANGVIEN_ID) ? null : GetTenGiangVien(ORenderInfo, o.GIANGVIEN_ID),
                    GhiChu = o.GHICHU
                });
            DateTime ngayIn = DateTime.Today;
            //flexCelReport.SetValue("DonViDaoTao", donVi != null ? donVi.OwnerName.ToUpper() : null);
            flexCelReport.SetValue("DonViDaoTao", "BỆNH VIỆN YHCT NGHỆ AN");
            flexCelReport.SetValue("NgayIn", "Hà Nội, ngày " + ngayIn.Day + " tháng " + ngayIn.Month + " năm " + ngayIn.Year);
            flexCelReport.SetValue("TenKhoaHoc", khoaHoc.TENKHOAHOC);
            flexCelReport.SetValue("SoKhoaHoc", khoaHoc.KHOA);
            flexCelReport.SetValue("DiaDiem", lichLyThuyet.DIADIEM);
            flexCelReport.SetValue("ThoiGian", (lichLyThuyet.BATDAU == null ? null : lichLyThuyet.BATDAU.Value.ToString("dd/MM/yyyy")) + " - " + (lichLyThuyet.KETTHUC == null ? null : lichLyThuyet.KETTHUC.Value.ToString("dd/MM/yyyy")));
            flexCelReport.SetValue("TongSoBuoi", lichLyThuyetChiTiets.Count());
            flexCelReport.SetValue("NguoiLap", nguoiTao != null ? nguoiTao.FullName : null);
            flexCelReport.SetValue("PtChuyenMon", ptChuyenMon == null ? null : ptChuyenMon.HOTEN);
            flexCelReport.SetValue("LanhDao", lanhDao == null ? null : lanhDao.HOTEN);
            flexCelReport.AddTable("LichLyThuyetChiTiet", lichLyThuyetChiTiets);
            return flexCelReport;
        }
        private string GetTenGiangVien(RenderInfoCls ORenderInfo, string id)
        {
            BacSyCls giangVien = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, id);
            if (giangVien == null)
                return null;
            return giangVien.HOTEN;
        }
        private string GetDayOfWeek(SiteParam oSiteParam, DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Monday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ hai");
            else if (dayOfWeek == DayOfWeek.Tuesday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ ba");
            else if (dayOfWeek == DayOfWeek.Wednesday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ tư");
            else if (dayOfWeek == DayOfWeek.Thursday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ năm");
            else if (dayOfWeek == DayOfWeek.Friday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ sáu");
            else if (dayOfWeek == DayOfWeek.Saturday)
                return WebLanguage.GetLanguage(oSiteParam, "Thứ bảy");
            else if (dayOfWeek == DayOfWeek.Sunday)
                return WebLanguage.GetLanguage(oSiteParam, "Chủ nhật");
            return null;
        }
    }
}