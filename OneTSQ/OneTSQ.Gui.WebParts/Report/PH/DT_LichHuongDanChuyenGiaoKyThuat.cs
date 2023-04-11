using FlexCel.Report;
using OneMES3.DM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.ReportUtility;
using OneTSQ.Utility;
using OneTSQ.WebParts;
using OneTSQ.Core.Model;

namespace OneTSQ.Gui.WebParts.Report
{
    [ObjectReport(ID = "13D3131B-71A2-489C-8DAD-D3A50D026D44", Type = OneTSQ.ReportUtility.Report.eType.Flexcel, Name = "DT_LichHuongDanChuyenGiaoKyThuat", Title = "Lịch hướng dẫn chuyển giao kỹ thuật", ReportPath = @"\ReportTemplates\DT_LichHuongDanChuyenGiaoKyThuat.xlsx")]
    class DT_LichHuongDanChuyenGiaoKyThuat : ObjectReport<DT_ChuyenGiao>
    {
        private class NgayChuyenGiaoCls
        {
            public DateTime ThoiGian { get; set; }
            public string NoiDung { get; set; }
            public int? SoCaHuongDan { get; set; }
            public int? SoCaHoTro { get; set; }
            public string CanBos { get; set; }
        }
        protected override FlexCelReport OnFlexcelBuild(RenderInfoCls ORenderInfo, string lichChuyenGiaoId)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);

            DT_LichChuyenGiaoCls lichChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiaoId);
            DM_KyThuatChuyenGiaoCls kyThuatChuyenGiao = string.IsNullOrEmpty(lichChuyenGiao.KYTHUAT_MA) ? null : CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CreateModel(ORenderInfo, lichChuyenGiao.KYTHUAT_MA);

            OneMES3.DM.Model.BenhVienCls benhVien = null;
            if (!string.IsNullOrEmpty(lichChuyenGiao.BENHVIEN_MA))
            {
                benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), lichChuyenGiao.BENHVIEN_MA);
            }

            BacSyCls canBoChuyenGiao = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, lichChuyenGiao.BACSY_ID);

            OneMES3.DM.Model.ChuyenKhoaCls chuyenKhoa = null;
            if (canBoChuyenGiao != null && !string.IsNullOrEmpty(canBoChuyenGiao.CHUYENKHOAMA))
            {
                chuyenKhoa = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), canBoChuyenGiao.CHUYENKHOAMA);
            }

            OneMES3.DM.Model.ChucDanhCls hocHam = null;
            if (canBoChuyenGiao != null && !string.IsNullOrEmpty(canBoChuyenGiao.CHUCDANHMA))
            {
                hocHam = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChucDanhProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), canBoChuyenGiao.CHUCDANHMA);
            }
            var ngayChuyenGiaos = CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoChiTietProcess().Reading(ORenderInfo, new DT_LichChuyenGiaoChiTietFilterCls() { LICHCHUYENGIAO_ID = lichChuyenGiaoId })
                .Select(o => new NgayChuyenGiaoCls
                {
                    ThoiGian = o.THOIGIAN,
                    NoiDung = o.NOIDUNG,
                    SoCaHuongDan = o.SOCAHUONGDAN,
                    SoCaHoTro = o.SOCAHOTRO,
                    CanBos = o.CANBOS
                });
            FlexCelReport flexCelReport = new FlexCelReport();
            flexCelReport.SetValue("HoTen", canBoChuyenGiao == null ? null : canBoChuyenGiao.HOTEN);
            flexCelReport.SetValue("HocHam", hocHam == null ? null : hocHam.Ten);
            flexCelReport.SetValue("ChuyenKhoa", chuyenKhoa == null ? null : chuyenKhoa.Ten);
            flexCelReport.SetValue("KyThuatChuyenGiao", kyThuatChuyenGiao == null ? null : kyThuatChuyenGiao.Ten);
            flexCelReport.SetValue("BenhVienNhanChuyenGiao", benhVien == null ? null : benhVien.Ten);
            flexCelReport.AddTable("NgayChuyenGiao", ngayChuyenGiaos);

            return flexCelReport;
        }
    }
}
