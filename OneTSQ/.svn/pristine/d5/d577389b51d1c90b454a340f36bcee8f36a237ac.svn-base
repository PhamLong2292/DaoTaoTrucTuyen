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
    [ObjectReport(ID = "2D780F6A-9A7A-425D-81A4-93DB29F9E223", Type = OneTSQ.ReportUtility.Report.eType.Flexcel, Name = "PhieuKhaoSatBVVT", Title = "Phiếu khảo sát bệnh viện vệ tinh ", ReportPath = @"\ReportTemplates\PhieuKhaoSatBenhVien.xlsx")]
    public class PhieuKhaoSatBVVT : ObjectReport<PhieuKhaoSatBenhVienVeTinh>
    {        
        protected override FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, string PhieuKhaoSatId)
        {
            FlexCelReport flexCelReport = new FlexCelReport();
            PhieuKhaoSatBenhVienVeTinhCls PhieuKhaoSat = CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().CreateModel(ORenderInfo, PhieuKhaoSatId);
            if (PhieuKhaoSat == null)
                PhieuKhaoSat = new PhieuKhaoSatBenhVienVeTinhCls();                  
            var benhvien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), PhieuKhaoSat.BENHVIEN_ID);
            DaoTaoNhanLucCls DTNL = CallBussinessUtility.CreateBussinessProcess().CreateDaoTaoNhanLucProcess().CreateModel(ORenderInfo, PhieuKhaoSat.ID);
            DT_KhoaHocCls KhoaHocs = null;
            if (DTNL != null)
            {
                KhoaHocs = string.IsNullOrEmpty(DTNL.DM_TENKHOAHOC_ID) ? null : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, DTNL.DM_TENKHOAHOC_ID);
            }
            flexCelReport.SetValue("DonViDaoTao", "BỆNH VIỆN YHCT NGHỆ AN");
            flexCelReport.SetValue("Ngay", DateTime.Now.ToString("dd"));
            flexCelReport.SetValue("Thang", DateTime.Now.ToString("MM"));
            flexCelReport.SetValue("Nam", DateTime.Now.ToString("yyyy"));
            flexCelReport.SetValue("TenDonVi", string.IsNullOrEmpty(benhvien.Ten) ? null : benhvien.Ten);
            flexCelReport.SetValue("NgayThanhLap", (PhieuKhaoSat.NGAYTHANHLAP == null ? null : PhieuKhaoSat.NGAYTHANHLAP.Value.ToString("dd/MM/yyyy")));
            flexCelReport.SetValue("SoLuongBacSi",  PhieuKhaoSat.SOLUONGBACSI);
            flexCelReport.SetValue("SoLuongYSi", PhieuKhaoSat.SOLUONGYSI );
            flexCelReport.SetValue("SoLuongDieuDuong", PhieuKhaoSat.SOLUONGDIEUDUONG);
            flexCelReport.SetValue("SoLuongKTV", PhieuKhaoSat.SOLUONGKTV);
            flexCelReport.SetValue("SoLuongDuocSi", PhieuKhaoSat.SOLUONGDUOCSI);
            flexCelReport.SetValue("SoLuongKhac", PhieuKhaoSat.SOLUONGKHAC);
            flexCelReport.SetValue("SoGiuongKeHoach", PhieuKhaoSat.SOGIUONGKEHOACH);
            flexCelReport.SetValue("SoGiuongThucTe", PhieuKhaoSat.SOGIUONGTHUCTE);
            flexCelReport.SetValue("SoBuongBenh", PhieuKhaoSat.SOBUONGBENH);
            flexCelReport.SetValue("SoPKThuThat", PhieuKhaoSat.SOPKTHUTHUAT);
            flexCelReport.SetValue("TrangThietBi", string.IsNullOrEmpty(PhieuKhaoSat.TRANGTHIETBI) ? null : PhieuKhaoSat.TRANGTHIETBI);
            flexCelReport.SetValue("SoLanKhamYHCT", PhieuKhaoSat.SOLANKHAMYHCT);
            flexCelReport.SetValue("SoBNNoiTru", PhieuKhaoSat.SOBNNOITRU);
            flexCelReport.SetValue("SoLanThuThuat", PhieuKhaoSat.SOLANTHUTHUAT);
            flexCelReport.SetValue("NoiDungDaoTao", KhoaHocs.TENKHOAHOC);
            flexCelReport.SetValue("SoLuongCanBo", DTNL.SOLUONG);
            return flexCelReport;
        }
        public string StripHTML(string html)
        {
            var regex = new System.Text.RegularExpressions.Regex("<[^>]+>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return System.Web.HttpUtility.HtmlDecode((regex.Replace(html, "")));
        }
    }
}