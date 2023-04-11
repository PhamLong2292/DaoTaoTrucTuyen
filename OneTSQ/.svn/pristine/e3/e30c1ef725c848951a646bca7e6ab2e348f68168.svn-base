using FlexCel.Report;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.ReportUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebParts
{
    [ObjectReport(ID = "FFBDFD2E-B1C6-4BF0-9A14-5505BA762057", Type = Report.eType.Flexcel, Name = "PhieuViSinhInChung", Title = "Phiếu vi sinh in chung", ReportPath = "\\ReportTemplates\\PhieuViSinhInChung.xlsx")]
    public class PhieuViSinhInChung : ObjectReport<XetNghiemDraw>
    {
        public override bool CanExecute(RenderInfoCls ORenderInfo, string id)
        {
            var xetnghiem = CallBussinessUtility.CreateBussinessProcess().CreateXetNghiemProcess().CreateModel(ORenderInfo, id);
            if (xetnghiem != null)
            {
                var dv = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucDichVuProcess().CreateModel(ORenderInfo, xetnghiem.DichVuID);
                if (dv != null)
                    return (dv.BaoCao == (byte)DanhMucDichVuCls.eBaoCao.ViSinh);
                else
                    return false;
            }
            return false;
        }
        protected override FlexCel.Report.FlexCelReport OnFlexcelBuild(Model.RenderInfoCls ORenderInfo, string xetnghiemid)
        {
            FlexCelReport flexCelReport = new FlexCelReport();
            var xetnghiem = CallBussinessUtility.CreateBussinessProcess().CreateXetNghiemProcess().CreateModel(ORenderInfo, xetnghiemid);
            var maubenhpham = CallBussinessUtility.CreateBussinessProcess().CreateMauBenhPhamProcess().CreateModel(ORenderInfo, xetnghiem.MauBenhPhamID);
            var xetnghiems = CallBussinessUtility.CreateBussinessProcess().CreateXetNghiemProcess().ReadingByMauBenhPhamID(ORenderInfo, xetnghiem.MauBenhPhamID);
            var tiepnhan = CallBussinessUtility.CreateBussinessProcess().CreateTiepNhanProcess().CreateModel(ORenderInfo, maubenhpham.TiepNhanID);
            var benhnhan = CallBussinessUtility.CreateBussinessProcess().CreateBenhNhanProcess().CreateModel(ORenderInfo, tiepnhan.BenhNhanID);

            flexCelReport.SetValue("SoPhieu", tiepnhan.SoPhieu);
            flexCelReport.SetValue("MaBN", benhnhan.Ma);
            flexCelReport.SetValue("SoVaoVien", tiepnhan.SoVaoVien);
            flexCelReport.SetValue("TenBN", benhnhan.Ten);
            string tuoibn = "";
            if (benhnhan.NgaySinh != null && tiepnhan.ThoiGian != null)
            {
                tuoibn = OneTSQ.Utility.FunctionUtility.TinhTuoiMH(benhnhan.NgaySinh.Value, tiepnhan.ThoiGian);
            }
            flexCelReport.SetValue("TuoiBN", tuoibn);
            flexCelReport.SetValue("GioiTinh", benhnhan.GioiTinh ? 1 : 0);
            flexCelReport.SetValue("DiaChi", benhnhan.DiaChi);
            if (!string.IsNullOrEmpty(xetnghiem.KhoaPhongChiDinhID))
            {
                var kp = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucKhoaPhongProcess().CreateModel(ORenderInfo, xetnghiem.KhoaPhongChiDinhID);
                if (kp != null)
                    flexCelReport.SetValue("KhoaPhongChiDinh", kp.Ten);
                else
                    flexCelReport.SetValue("KhoaPhongChiDinh", "");
            }
            else
                flexCelReport.SetValue("KhoaPhongChiDinh", "");
            if (!string.IsNullOrEmpty(xetnghiem.BacSiChiDinhID))
            {
                var nd = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucNguoiDungProcess().CreateModel(ORenderInfo, xetnghiem.BacSiChiDinhID);
                if (nd != null)
                    flexCelReport.SetValue("BacSiChiDinh", nd.Ten);
                else
                    flexCelReport.SetValue("BacSiChiDinh", "");
            }
            else
                flexCelReport.SetValue("BacSiChiDinh", "");
            flexCelReport.SetValue("NgayChiDinh", tiepnhan.ThoiGian.ToString("HH:mm dd/MM/yyyy"));
            flexCelReport.SetValue("NgayThucHien", xetnghiem.ThoiGianHoanTat != null ? xetnghiem.ThoiGianHoanTat.Value.ToString("HH:mm dd/MM/yyyy") : "");
            string chandoanbenh = "";
            if (string.IsNullOrEmpty(tiepnhan.IcdID))
                chandoanbenh = tiepnhan.ChanDoan;
            else if (!string.IsNullOrEmpty(tiepnhan.ChanDoan))
            {
                var icd = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucIcdProcess().CreateModel(ORenderInfo, tiepnhan.IcdID);
                if (icd != null)
                    chandoanbenh = "(" + icd.Ma + ") " + icd.Ten + ", " + tiepnhan.ChanDoan;
                else
                    chandoanbenh = tiepnhan.ChanDoan;
            }
            else
            {
                var icd = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucIcdProcess().CreateModel(ORenderInfo, tiepnhan.IcdID);
                if (icd != null)
                    chandoanbenh = "(" + icd.Ma + ") " + icd.Ten;
            }

            flexCelReport.SetValue("ChanDoan", chandoanbenh);
            flexCelReport.SetValue("NgayIn", xetnghiem.ThoiGianHoanTat ?? DateTime.Now);

            var ketquas = new List<KetQuaXetNghiemInChungCls>();
            foreach (var xn in xetnghiems)
            {
                var kq = new KetQuaXetNghiemInChungCls();
                if (!string.IsNullOrEmpty(xn.DichVuID))
                {
                    var dv = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucDichVuProcess().CreateModel(ORenderInfo, xn.DichVuID);
                    if (dv != null && dv.BaoCao == (int)DanhMucDichVuCls.eBaoCao.ViSinh)
                    {
                        kq.DichVu = dv.Ten;
                        kq.KetQua = xn.KetQua;
                        ketquas.Add(kq);
                    }
                }
            }

            flexCelReport.AddTable("BenhNhan", ketquas);
            return flexCelReport;
        }
        class KetQuaXetNghiemInChungCls
        {
            public string DichVu { get; set; }
            public string KetQua { get; set; }
        }
    }
}
