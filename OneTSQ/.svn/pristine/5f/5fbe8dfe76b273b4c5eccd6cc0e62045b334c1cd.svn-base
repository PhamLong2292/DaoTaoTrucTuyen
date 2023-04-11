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
    [ObjectReport(ID = "BC8CDB54-DE7C-41AA-A72A-281962B6764E", Type = Report.eType.Flexcel, Name = "PhieuXetNghiemHoaSinhMau", Title = "Phiếu xét nghiệm sinh hóa", ReportPath = "\\ReportTemplates\\PhieuXNHoaSinhMau.xlsx")]
    public class PhieuXetNghiemHoaSinhMau : ObjectReport<XetNghiemDraw>
    {
        public override bool CanExecute(Model.RenderInfoCls ORenderInfo, string id)
        {
            var xetnghiem = CallBussinessUtility.CreateBussinessProcess().CreateXetNghiemProcess().CreateModel(ORenderInfo, id);
            if(xetnghiem != null)
            {
                var dv = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucDichVuProcess().CreateModel(ORenderInfo, xetnghiem.DichVuID);
                if (dv != null)
                    return (dv.BaoCao == (byte)DanhMucDichVuCls.eBaoCao.SinhHoaMau);
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
            var tiepnhan = CallBussinessUtility.CreateBussinessProcess().CreateTiepNhanProcess().CreateModel(ORenderInfo, maubenhpham.TiepNhanID);
            var benhnhan = CallBussinessUtility.CreateBussinessProcess().CreateBenhNhanProcess().CreateModel(ORenderInfo, tiepnhan.BenhNhanID);
                        
            flexCelReport.SetValue("TenBN", benhnhan.Ten);
            string tuoibn = "";
            if (benhnhan.NgaySinh != null && tiepnhan.ThoiGian != null)
            {
                tuoibn = OneTSQ.Utility.FunctionUtility.TinhTuoiMH(benhnhan.NgaySinh.Value, tiepnhan.ThoiGian);
            }
            flexCelReport.SetValue("TuoiBN", tuoibn);
            flexCelReport.SetValue("GioiTinh", benhnhan.GioiTinh? 1 : 0);
            flexCelReport.SetValue("DiaChi", benhnhan.DiaChi);
            flexCelReport.SetValue("SoTheBhyt", tiepnhan.SoTheBhyt);
            if (!string.IsNullOrEmpty(tiepnhan.SoTheBhyt))
                flexCelReport.SetValue("DiaChiThe", benhnhan.DiaChiThonXom);
            else
                flexCelReport.SetValue("DiaChiThe", "");
            if (!string.IsNullOrEmpty(xetnghiem.KhoaPhongChiDinhID))
            {
                var kp = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucKhoaPhongProcess().CreateModel(ORenderInfo, xetnghiem.KhoaPhongChiDinhID);
                if(kp != null)
                    flexCelReport.SetValue("KhoaPhongChiDinh", kp.Ten);
                else
                    flexCelReport.SetValue("KhoaPhongChiDinh", "");
            }
            else
                flexCelReport.SetValue("KhoaPhongChiDinh", "");
            flexCelReport.SetValue("Buong", tiepnhan.Buong);
            flexCelReport.SetValue("Giuong", tiepnhan.Giuong);
            string chandoanbenh = "";
            if(string.IsNullOrEmpty(tiepnhan.IcdID))
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
            flexCelReport.SetValue("ChiDinhVao", tiepnhan.ThoiGian);
            flexCelReport.SetValue("CapCuu", tiepnhan.CapCuu);
            flexCelReport.SetValue("NgayIn", xetnghiem.ThoiGian);  
            var chisos = CallBussinessUtility.CreateBussinessProcess().CreateDanhMucChiSoProcess().Reading(ORenderInfo, xetnghiem.DichVuID);
            var ketquas = CallBussinessUtility.CreateBussinessProcess().CreateKetQuaXetNghiemProcess().ReadingByXetNghiemId(ORenderInfo, xetnghiemid).ToDictionary(o => o.ChiSoXetNghiemID);
            foreach (var item in chisos)
            {
                flexCelReport.SetValue(item.Ma, ketquas.Keys.Contains(item.ID) ? ketquas[item.ID].GiaTri : "");
            }          
            
            return flexCelReport;
        }
    }
}
