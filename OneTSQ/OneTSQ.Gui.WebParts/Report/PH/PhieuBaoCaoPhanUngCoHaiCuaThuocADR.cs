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
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    [ObjectReport(ID = "C53CDF71-8BA3-43F2-A011-E009CB7329F2", Type = OneTSQ.ReportUtility.Report.eType.Flexcel, Name = "PhieuBaoCaoPhanUngCoHaiADR", Title = "Phiếu báo cáo phản ứng có hại ADR", ReportPath = @"\ReportTemplates\PhieuBaoCaoPhanUngCoHaiADR.xlsx")]
    public class PhieuBaoCaoPhanUngCoHaiADR : ObjectReport<PhieuADR>
    {
        private class ThuocADR
        {
            public string HANG_ID { get; set; }
            public string DANGBAOCHE { get; set; }
            public string NHASANXUAT { get; set; }
            public string SOLOSX { get; set; }
            public string LIEUDUNG1LAN { get; set; }
            public string SOLANDUNG { get; set; }
            public string DUONGDUNG { get; set; }
            public DateTime? NGAYVAOVIEN { get; set; }
            public DateTime? NGAYRAVIEN { get; set; }
            public string LYDODUNGTHUOC { get; set; }
            public string PHANUNGCAITHIEN { get; set; }
            public string PHANUNGXUATHIEN { get; set; }
            public int? LOAITHUOC { get; set; }
            public string PHIEU_ID { get; set; }
        }

        private class ThuocADRDongThoi
        {          
            public string HANG_ID { get; set; }
            public string DANGBAOCHE { get; set; }
            public DateTime? NGAYVAOVIEN { get; set; }
            public DateTime? NGAYRAVIEN { get; set; }
        }

        protected override FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, string PhanUngCoHaiADRId)
        {
            FlexCelReport flexCelReport = new FlexCelReport();        
            PhieuBaoCaoPhanUngCoHaiADRCls PhanUngCoHaiADR = CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().CreateModel(ORenderInfo, PhanUngCoHaiADRId);
            if (PhanUngCoHaiADR == null)
                PhanUngCoHaiADR = new PhieuBaoCaoPhanUngCoHaiADRCls();
            var chucvu = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucVuProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), PhanUngCoHaiADR.ChucVu_Id);
            DepartmentCls OKhoaPhong = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateDepartmentProcess().CreateModel(ORenderInfo, PhanUngCoHaiADR.NoiBaoCao_Id);
            OwnerUserCls ONguoiLap = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, PhanUngCoHaiADR.NguoiLap_Id);
            var ThuocADRs = CallBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Reading(ORenderInfo, new ThuocADRFilterCls() { Phieu_ID = PhanUngCoHaiADR.Id, LoaiThuoc=  1 })
            .Select(o => new ThuocADR
            {

                HANG_ID = string.IsNullOrEmpty(o.HANG_ID) ? null : GetTenThuoc(ORenderInfo, o.HANG_ID),
                DANGBAOCHE = o.DANGBAOCHE,
                NHASANXUAT = o.NHASANXUAT,
                SOLOSX = o.SOLOSX,
                LIEUDUNG1LAN = o.LIEUDUNG1LAN,
                SOLANDUNG = o.SOLANDUNG,
                DUONGDUNG = o.DUONGDUNG,
                NGAYVAOVIEN = o.NGAYVAOVIEN,
                NGAYRAVIEN = o.NGAYRAVIEN,
                LYDODUNGTHUOC = o.LYDODUNGTHUOC,
                PHANUNGCAITHIEN = o.PHANUNGCAITHIEN == null ? null : ThuocADRParser.TraLois[o.PHANUNGCAITHIEN.Value],
                PHANUNGXUATHIEN = o.PHANUNGXUATHIEN == null ? null : ThuocADRParser.TraLois[o.PHANUNGXUATHIEN.Value],
            });

          var ThuocADRDongThois = CallBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Reading(ORenderInfo, new ThuocADRFilterCls() { Phieu_ID = PhanUngCoHaiADR.Id, LoaiThuoc = 2 })
          .Select(o => new ThuocADRDongThoi
           {
              HANG_ID = string.IsNullOrEmpty(o.HANG_ID) ? null : GetTenThuoc(ORenderInfo, o.HANG_ID),
              DANGBAOCHE = o.DANGBAOCHE,
              NGAYVAOVIEN = o.NGAYVAOVIEN,
              NGAYRAVIEN = o.NGAYRAVIEN,
          });
            flexCelReport.SetValue("DonViChuQuan", "SỞ Y TẾ NGHỆ AN");
            flexCelReport.SetValue("BenhVienTen", "BỆNH VIỆN YHCT NGHỆ AN");     
            flexCelReport.SetValue("SoBCDonVi", PhanUngCoHaiADR.SoBcDonVi);
            flexCelReport.SetValue("SoBCQuocGia", PhanUngCoHaiADR.SoBCQuocGia);
            flexCelReport.SetValue("HoTen", string.IsNullOrEmpty(PhanUngCoHaiADR.HoTen) ? null : PhanUngCoHaiADR.HoTen);
            flexCelReport.SetValue("NoiBaoCao", string.IsNullOrEmpty(PhanUngCoHaiADR.NoiBaoCao_Id) ? null : OKhoaPhong.DepartmentName);
            flexCelReport.SetValue("NgaySinh", (PhanUngCoHaiADR.NgaySinh == null ? null : PhanUngCoHaiADR.NgaySinh.Value.ToString("dd/MM/yyyy")));
            flexCelReport.SetValue("GTNam", PhanUngCoHaiADR.GioiTinh == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eGioiTinh.Nam ? "x" : null);
            flexCelReport.SetValue("GTNu", PhanUngCoHaiADR.GioiTinh == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eGioiTinh.Nu ? "x" : null);
            flexCelReport.SetValue("CanNang", PhanUngCoHaiADR.CanNang);
            flexCelReport.SetValue("NgayXuatHien", (PhanUngCoHaiADR.NgayXuatHienPU == null ? null : PhanUngCoHaiADR.NgayXuatHienPU.Value.ToString("dd/MM/yyyy")));
            flexCelReport.SetValue("ThoiGianPhanUng", string.IsNullOrEmpty(PhanUngCoHaiADR.ThoiGianPhanUng) ? null : PhanUngCoHaiADR.ThoiGianPhanUng);
            flexCelReport.SetValue("MoTaADR", string.IsNullOrEmpty(PhanUngCoHaiADR.MoTaADR) ? null : PhanUngCoHaiADR.MoTaADR);
            flexCelReport.SetValue("XetNghiemLienQuan", string.IsNullOrEmpty(PhanUngCoHaiADR.XetNghiemLienQuan) ? null : PhanUngCoHaiADR.XetNghiemLienQuan);
            flexCelReport.SetValue("TienSuBenhSu", string.IsNullOrEmpty(PhanUngCoHaiADR.TienSuBenhSu) ? null : PhanUngCoHaiADR.TienSuBenhSu);
            flexCelReport.SetValue("XuTri", string.IsNullOrEmpty(PhanUngCoHaiADR.XuTri) ? null : PhanUngCoHaiADR.XuTri);

            flexCelReport.SetValue("TuVong", PhanUngCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TuVong ? "x" : null);
            flexCelReport.SetValue("NhapVien", PhanUngCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.NhapVien ? "x" : null);
            flexCelReport.SetValue("DiTatThaiNhi", PhanUngCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.DiTatThaiNhi ? "x" : null);
            flexCelReport.SetValue("DeDoaTinhMang", PhanUngCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.DeDoaTinhMang ? "x" : null);
            flexCelReport.SetValue("TanTat", PhanUngCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TanTat ? "x" : null);
            flexCelReport.SetValue("KhongNghiemTrong", PhanUngCoHaiADR.MucDoNghiemTrong == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongNghiemTrong ? "x" : null);

            flexCelReport.SetValue("TuVongDoADR", PhanUngCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TuVongDoADR ? "x" : null);
            flexCelReport.SetValue("ChuaHoiPhuc", PhanUngCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ChuaHoiPhuc ? "x" : null);
            flexCelReport.SetValue("HoiPhucCoDiChung", PhanUngCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.HoiPhucCoDiChung ? "x" : null);
            flexCelReport.SetValue("TuVongKhongLqThuoc", PhanUngCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TuVongKhongLqThuoc ? "x" : null);
            flexCelReport.SetValue("DangHoiPhuc", PhanUngCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.DangHoiPhuc ? "x" : null);
            flexCelReport.SetValue("HoiPhucKhongDiChung", PhanUngCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.HoiPhucKhongDiChung ? "x" : null);
            flexCelReport.SetValue("KhongRo", PhanUngCoHaiADR.KetQuaSauXuTri == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongRo ? "x" : null);

            flexCelReport.SetValue("ChacChan", PhanUngCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ChacChan ? "x" : null);
            flexCelReport.SetValue("KhongChacChan", PhanUngCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongChacChan ? "x" : null);
            flexCelReport.SetValue("CoKhaNang", PhanUngCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.CoKhaNang ? "x" : null);
            flexCelReport.SetValue("ChuaPhanLoai", PhanUngCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ChuaPhanLoai ? "x" : null);
            flexCelReport.SetValue("CoThe", PhanUngCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.CoThe ? "x" : null);
            flexCelReport.SetValue("KhongThePhanLoai", PhanUngCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongThePhanLoai ? "x" : null);
            flexCelReport.SetValue("Khac", PhanUngCoHaiADR.ThuocVaADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.Khac ? "x" : null);
            flexCelReport.SetValue("MoTaThuocVaADR", string.IsNullOrEmpty(PhanUngCoHaiADR.MoTaThuocVaADR) ? null : PhanUngCoHaiADR.MoTaThuocVaADR);

            flexCelReport.SetValue("ThangWHO", PhanUngCoHaiADR.ThangThamDinhADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ThangWHO ? "x" : null);
            flexCelReport.SetValue("ThangNaranjo", PhanUngCoHaiADR.ThangThamDinhADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ThangNaranjo ? "x" : null);
            flexCelReport.SetValue("ThangKhac", PhanUngCoHaiADR.ThangThamDinhADR == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ThangKhac ? "x" : null);
            flexCelReport.SetValue("MoTaThangThamDinh", string.IsNullOrEmpty(PhanUngCoHaiADR.MoTaThangThamDinh) ? null : PhanUngCoHaiADR.MoTaThangThamDinh);

            flexCelReport.SetValue("BinhLuan", string.IsNullOrEmpty(PhanUngCoHaiADR.BinhLuan) ? null : PhanUngCoHaiADR.BinhLuan);
            flexCelReport.SetValue("NguoiLap", string.IsNullOrEmpty(PhanUngCoHaiADR.NguoiLap_Id) ? null : ONguoiLap.OwnerName);
            flexCelReport.SetValue("LanDau", PhanUngCoHaiADR.DangBaoCao == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eBaoCao.LanDau ? "x" : null);
            flexCelReport.SetValue("BoSung", PhanUngCoHaiADR.DangBaoCao == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eBaoCao.BoSung ? "x" : null);
            flexCelReport.SetValue("DienThoai", string.IsNullOrEmpty(PhanUngCoHaiADR.DienThoai) ? null : PhanUngCoHaiADR.DienThoai);
            flexCelReport.SetValue("ChucVu", string.IsNullOrEmpty(PhanUngCoHaiADR.ChucVu_Id) ? null : chucvu.Ten);
            flexCelReport.SetValue("Email", string.IsNullOrEmpty(PhanUngCoHaiADR.Email) ? null : PhanUngCoHaiADR.Email);
            flexCelReport.SetValue("NgayBaoCao", (PhanUngCoHaiADR.NgayLap == null ? null : PhanUngCoHaiADR.NgayLap.Value.ToString("dd/MM/yyyy")));
            flexCelReport.AddTable("ThuocADRs", ThuocADRs);
            flexCelReport.AddTable("ThuocADRDongThoi", ThuocADRDongThois);

            return flexCelReport;
        }
        private string GetTenThuoc(RenderInfoCls ORenderInfo, string id)
        {
            var Ohang = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateHangProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), id);
            if (Ohang == null)
                return null;
            return Ohang.Ten;
        }
        public string StripHTML(string html)
        {
            var regex = new System.Text.RegularExpressions.Regex("<[^>+>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return System.Web.HttpUtility.HtmlDecode((regex.Replace(html, "")));
        }
    }
}