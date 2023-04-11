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
    [ObjectReport(ID = "5E9016E4-9B71-4CB2-A746-C29145E7D2DF", Type = Report.eType.Flexcel, Name = "PhieuKhangSinhDo", Title = "Phiếu kháng sinh đồ", ReportPath = "\\ReportTemplates\\PhieuKhangSinhDo.xlsx")]
    public class PhieuKhangSinhDo : ObjectReport<XetNghiemKhangSinhDoDraw>
    {
        public override bool CanExecute(RenderInfoCls ORenderInfo, string id)
        {
            var xetnghiem = CallBussinessUtility.CreateBussinessProcess().CreateXetNghiemProcess().CreateModel(ORenderInfo, id);
            return (xetnghiem != null && xetnghiem.TrangThai == (int)OneTSQ.Model.XetNghiem.eTrangThai.HoanTat);
        }
        protected override FlexCel.Report.FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, string xetnghiemid)
        {
            FlexCelReport flexCelReport = new FlexCelReport();
            var xetnghiem = CallBussinessUtility.CreateBussinessProcess().CreateXetNghiemProcess().CreateModel(ORenderInfo, xetnghiemid);
            var maubenhpham = CallBussinessUtility.CreateBussinessProcess().CreateMauBenhPhamProcess().CreateModel(ORenderInfo, xetnghiem.MauBenhPhamID);
            var KetQuas = CallBussinessUtility.CreateBussinessProcess().CreateKetQuaKhangSinhDoProcess().ReadingByXetNghiemId(ORenderInfo, xetnghiemid);
            var tiepnhan = CallBussinessUtility.CreateBussinessProcess().CreateTiepNhanProcess().CreateModel(ORenderInfo, maubenhpham.TiepNhanID);
            var benhnhan = CallBussinessUtility.CreateBussinessProcess().CreateBenhNhanProcess().CreateModel(ORenderInfo, tiepnhan.BenhNhanID);

            flexCelReport.SetValue("SoPhieu", tiepnhan.SoPhieu);
            flexCelReport.SetValue("DonViTen", OneTSQ.Core.Model.KeyLicenseCls.DonViTen);
            flexCelReport.SetValue("MaBN", benhnhan.Ma);
            flexCelReport.SetValue("SoVaoVien", tiepnhan.SoVaoVien);
            flexCelReport.SetValue("TenBN", benhnhan.Ten);
            flexCelReport.SetValue("TuoiBN", tiepnhan.Tuoi);
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
            string vikhuan = "";
            if(!string.IsNullOrEmpty(xetnghiem.ViKhuanID))
            {
                var vk = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucViKhuanProcess().CreateModel(ORenderInfo, xetnghiem.ViKhuanID);
                if(vk != null)
                    vikhuan = vk.Ten;
            }
            flexCelReport.SetValue("ViKhuan", vikhuan);
            flexCelReport.SetValue("ChanDoan", chandoanbenh);
            flexCelReport.SetValue("NgayIn", xetnghiem.ThoiGianHoanTat ?? DateTime.Now);
            flexCelReport.SetValue("KetQua", xetnghiem.KetQua);
            string dichvu_ = "";
            var dv = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucDichVuProcess().CreateModel(ORenderInfo, xetnghiem.DichVuID);
            {
                if (dv != null)
                    dichvu_ = dv.Ten;
            }
            flexCelReport.SetValue("DichVu", dichvu_);
            var bentrais = new List<KetQuaKhangSinhDoCls>();
            var benphais = new List<KetQuaKhangSinhDoCls>();
            for (int i = 0; i < KetQuas.Length; i++)
            {
                if (i < ((KetQuas.Length + 1)/2))
                {
                    var kq = new KetQuaKhangSinhDoCls();
                    var dmks = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucKhangSinhProcess().CreateModel(ORenderInfo, KetQuas[i].KhangSinhID);
                    if (dmks != null)
                        kq.TenKhangSinh = (i + 1) + ". " + dmks.Ten;
                    kq.GiaTri = KetQuas[i].GiaTri;
                    bentrais.Add(kq);
                }
                else
                {
                    var kq = new KetQuaKhangSinhDoCls();
                    var dmks = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucKhangSinhProcess().CreateModel(ORenderInfo, KetQuas[i].KhangSinhID);
                    if (dmks != null)
                        kq.TenKhangSinh = (i + 1) + ". " + dmks.Ten;
                    kq.GiaTri = KetQuas[i].GiaTri;
                    benphais.Add(kq);
                }
            }
            flexCelReport.AddTable("BenTrai", bentrais.OrderBy(o => o.TenKhangSinh).ToList());
            flexCelReport.AddTable("BenPhai", benphais.OrderBy(o => o.TenKhangSinh).ToList());
            return flexCelReport;
        }
        class KetQuaKhangSinhDoCls
        {
            public string TenKhangSinh { get; set; }
            public string GiaTri { get; set; }
        }
    }
}
