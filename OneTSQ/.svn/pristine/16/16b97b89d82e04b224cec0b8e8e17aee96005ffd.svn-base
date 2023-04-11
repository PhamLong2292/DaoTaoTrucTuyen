using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_KhoaHocCls
    {
        public string ID;
        public string MA = "";
        public string TEN = "";
        public string TENKHOAHOC = "";
        public int KHOA;
        public int? THOILUONG;
        public string LOAITHOILUONG = "";
        public DateTime? NGAYKHAIGIANGDUKIEN;
        public DateTime? HANNOPHOSO;
        public DateTime? NGAYBEGIANGDUKIEN;
        public int? SOLUONGHOCVIENDUKIEN;
        public decimal? HOCPHI;
        public int LOAIKHOAHOC;
        public int LOAIDAOTAO;
        public string DONVIHOTRO_MA = "";
        public string MOTA = "";
        public string ROOMID = "";
        public string CONVID = "";
        public int LOAIHINHDAOTAO;
        public string DOITUONG;
        public string TIEUCHUAN;
        public int TRANGTHAI;
        public string NGUOITAO_ID = "";
        public DateTime NGAYTAO;
        public string NGUOISUA_ID = "";
        public DateTime? NGAYSUA;
        public enum eTrangThai
        {
            Moi = 0,
            ChoDuyet,
            Duyet,
            TuChoi,
            KetThuc
        }
        public enum ePermission
        {
            Xem = 0,
            Them,
            Sua,
            Xoa,
            GuiDuyet,
            PheDuyet
        }
        public enum eLopHocTrangThai
        {
            Moi = 0,
            CoHocVien,
            KetThuc
        }
        public enum eLopHocPermission
        {
            Xem = 0,
            QuanLyKeHoach,
            QuanLyTaiLieu,
            DiemDanh,
            NhapKetQuaDaoTao,
            KetThuc
        }
        public enum eLoaiKhoaHoc
        {
            NganSachNhaNuoc = 0,
            Khac
        }
        public enum eLoaiDaoTao
        {
            KeHoach = 0,
            DeAn1816,
            DuAnBVVT,
            CongTacCDT,
            NangCaoNV,
            DeAnKCB
        }
        public enum eLoaiHinhDaoTao
        {
            ChungNhan = 0,
            ChungChi = 1
        }
        public enum eDoiTuong
        {
            BacSy = 0,
            YSy,
            DieuDuong,
            KyThuatVien,
            Khac
        }
        public enum eLoaiThoiLuong
        {
            D = 0,
            W = 1,
            M = 2
        }
        public static string XepLoai(decimal? lyThuyet, decimal? thucHanh)
        {
            if (lyThuyet == null || thucHanh == null)
                return null;
            decimal diem = (lyThuyet.Value + thucHanh.Value) / 2;
            if (diem >= 9 && lyThuyet >= 8 && thucHanh >= 8)
                return "Xuất sắc";
            if (diem >= 8 && lyThuyet >= 7 && thucHanh >= 7)
                return "Giỏi";
            if (diem >= 7 && lyThuyet >= 6 && thucHanh >= 6)
                return "Khá";
            if (diem >= 6 && lyThuyet >= 5 && thucHanh >= 5)
                return "TB khá";
            if (diem >= 5 && lyThuyet >= 5 && thucHanh >= 5)
                return "Trung bình";
            if (diem >= 4)
                return "Yếu";
            return "Kém";
        }
    }
}

public class DT_KhoaHocParser
{
    public readonly static Dictionary<int, string> TrangThais = new Dictionary<int, string>()
    {
        { (int)DT_KhoaHocCls.eTrangThai.Moi, "Mới" },
        { (int)DT_KhoaHocCls.eTrangThai.ChoDuyet, "Chờ duyệt" },
        { (int)DT_KhoaHocCls.eTrangThai.Duyet, "Duyệt" },
        { (int)DT_KhoaHocCls.eTrangThai.TuChoi, "Từ chối duyệt" },
        { (int)DT_KhoaHocCls.eTrangThai.KetThuc, "Kết thúc" }
    };
    public readonly static Dictionary<int, string> sColorTrangThai = new Dictionary<int, string>()
    {
        { (int)DT_KhoaHocCls.eTrangThai.Moi, "<span style=\"background-color:orange;color:white;\" class=\"badge\" >Mới</span>" },
        { (int)DT_KhoaHocCls.eTrangThai.ChoDuyet, "<span style=\"background-color:violet; color:black;\" class=\"badge\">Chờ duyệt</span>" },
        { (int)DT_KhoaHocCls.eTrangThai.Duyet, "<span style=\"background-color:green;color:white;\" class=\"badge\" >Duyệt</span>" },
        { (int)DT_KhoaHocCls.eTrangThai.TuChoi, "<span style=\"background-color:red;color:white;\" class=\"badge\" >Từ chối duyệt</span>" },
        { (int)DT_KhoaHocCls.eTrangThai.KetThuc, "<span style=\"background-color:blue;color:white;\" class=\"badge\" >Kết thúc</span>" }
    };
    public readonly static Dictionary<int, string> LopHocTrangThais = new Dictionary<int, string>()
    {
        { (int)DT_KhoaHocCls.eLopHocTrangThai.Moi, "Mới" },
        { (int)DT_KhoaHocCls.eLopHocTrangThai.CoHocVien, "Có học viên" },
        { (int)DT_KhoaHocCls.eLopHocTrangThai.KetThuc, "Kết thúc" }
    };
    public readonly static Dictionary<int, string> sColorLopHocTrangThai = new Dictionary<int, string>()
    {
        { (int)DT_KhoaHocCls.eLopHocTrangThai.Moi, "<span style=\"background-color:orange;color:white;\" class=\"badge\" >Mới</span>" },
        { (int)DT_KhoaHocCls.eLopHocTrangThai.CoHocVien, "<span style=\"background-color:violet; color:black;\" class=\"badge\">Có học viên</span>" },
        { (int)DT_KhoaHocCls.eLopHocTrangThai.KetThuc, "<span style=\"background-color:green;color:white;\" class=\"badge\" >Kết thúc</span>" }
    };
    public readonly static Dictionary<int, string> LoaiKhoaHocs = new Dictionary<int, string>()
    {
        { (int)DT_KhoaHocCls.eLoaiKhoaHoc.NganSachNhaNuoc, "Ngân sách nhà nước" },
        { (int)DT_KhoaHocCls.eLoaiKhoaHoc.Khac, "Khác" }
    };
    public readonly static Dictionary<int, string> LoaiDaoTaos = new Dictionary<int, string>()
    {
        { (int)DT_KhoaHocCls.eLoaiDaoTao.KeHoach, "Đào tạo theo kế hoạch" },
        { (int)DT_KhoaHocCls.eLoaiDaoTao.DeAn1816, "Đề án 1816" },
        { (int)DT_KhoaHocCls.eLoaiDaoTao.DuAnBVVT, "Dự án bệnh viện vệ tinh" },
        { (int)DT_KhoaHocCls.eLoaiDaoTao.CongTacCDT, "Công tác Chỉ đạo tuyến" },
        { (int)DT_KhoaHocCls.eLoaiDaoTao.NangCaoNV, "Nâng cao nghiệp vụ" },
        { (int)DT_KhoaHocCls.eLoaiDaoTao.DeAnKCB, "Đề án khám bệnh, chữa bệnh từ xa" }
    };
    public readonly static Dictionary<int, string> LoaiHinhDaoTaos = new Dictionary<int, string>()
    {
        { (int)DT_KhoaHocCls.eLoaiHinhDaoTao.ChungNhan, "Chứng nhận" },
        { (int)DT_KhoaHocCls.eLoaiHinhDaoTao.ChungChi, "Chứng chỉ" }
    };
    public readonly static Dictionary<int, string> DoiTuongs = new Dictionary<int, string>()
    {
        { (int)DT_KhoaHocCls.eDoiTuong.BacSy, "Bác sỹ" },
        { (int)DT_KhoaHocCls.eDoiTuong.YSy, "Y sỹ" },
        { (int)DT_KhoaHocCls.eDoiTuong.DieuDuong, "Điều dưỡng" },
        { (int)DT_KhoaHocCls.eDoiTuong.KyThuatVien, "Kỹ thuật viên" },
        { (int)DT_KhoaHocCls.eDoiTuong.Khac, "Khác" }
    };
    public readonly static Dictionary<string, string> LoaiThoiLuongs = new Dictionary<string, string>()
    {
        { DT_KhoaHocCls.eLoaiThoiLuong.D.ToString(), "Ngày" },
        { DT_KhoaHocCls.eLoaiThoiLuong.W.ToString(), "Tuần" },
        { DT_KhoaHocCls.eLoaiThoiLuong.M.ToString(), "Tháng" }
    };
    public static DT_KhoaHocCls CreateInstance()
    {
        DT_KhoaHocCls ODT_KhoaHoc = new DT_KhoaHocCls();
        return ODT_KhoaHoc;
    }


    public static DT_KhoaHocCls ParseFromDataRow(DataRow dr)
    {
        DT_KhoaHocCls ODT_KhoaHoc = new DT_KhoaHocCls();
        ODT_KhoaHoc.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_KhoaHoc.MA = CoreXmlUtility.GetString(dr, "MA", true);
        ODT_KhoaHoc.TEN = CoreXmlUtility.GetString(dr, "TEN", true);
        ODT_KhoaHoc.TENKHOAHOC = CoreXmlUtility.GetString(dr, "TENKHOAHOC", true);
        ODT_KhoaHoc.KHOA = CoreXmlUtility.GetInt(dr, "KHOA", true);
        ODT_KhoaHoc.THOILUONG = CoreXmlUtility.GetIntOrNull(dr, "THOILUONG", true);
        ODT_KhoaHoc.LOAITHOILUONG = CoreXmlUtility.GetString(dr, "LOAITHOILUONG", true);
        ODT_KhoaHoc.NGAYKHAIGIANGDUKIEN = CoreXmlUtility.GetDateOrNull(dr, "NGAYKHAIGIANGDUKIEN", true);
        ODT_KhoaHoc.HANNOPHOSO = CoreXmlUtility.GetDateOrNull(dr, "HANNOPHOSO", true);
        ODT_KhoaHoc.NGAYBEGIANGDUKIEN = CoreXmlUtility.GetDateOrNull(dr, "NGAYBEGIANGDUKIEN", true);
        ODT_KhoaHoc.SOLUONGHOCVIENDUKIEN = CoreXmlUtility.GetIntOrNull(dr, "SOLUONGHOCVIENDUKIEN", true);
        ODT_KhoaHoc.HOCPHI = CoreXmlUtility.GetDecimalOrNull(dr, "HOCPHI", true);
        ODT_KhoaHoc.LOAIKHOAHOC = CoreXmlUtility.GetInt(dr, "LOAIKHOAHOC", true);
        ODT_KhoaHoc.LOAIDAOTAO = CoreXmlUtility.GetInt(dr, "LOAIDAOTAO", true);
        ODT_KhoaHoc.DONVIHOTRO_MA = CoreXmlUtility.GetString(dr, "DONVIHOTRO_MA", true);
        ODT_KhoaHoc.MOTA = CoreXmlUtility.GetString(dr, "MOTA", true);
        ODT_KhoaHoc.ROOMID = CoreXmlUtility.GetString(dr, "ROOMID", true);
        ODT_KhoaHoc.CONVID = CoreXmlUtility.GetString(dr, "CONVID", true);
        ODT_KhoaHoc.LOAIHINHDAOTAO = CoreXmlUtility.GetInt(dr, "LOAIHINHDAOTAO", true);
        ODT_KhoaHoc.DOITUONG = CoreXmlUtility.GetString(dr, "DOITUONG", true);
        ODT_KhoaHoc.TIEUCHUAN = CoreXmlUtility.GetString(dr, "TIEUCHUAN", true);
        ODT_KhoaHoc.TRANGTHAI = CoreXmlUtility.GetInt(dr, "TRANGTHAI", true);
        ODT_KhoaHoc.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_KhoaHoc.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        ODT_KhoaHoc.NGUOISUA_ID = CoreXmlUtility.GetString(dr, "NGUOISUA_ID", true);
        ODT_KhoaHoc.NGAYSUA = CoreXmlUtility.GetDateOrNull(dr, "NGAYSUA", true);
        return ODT_KhoaHoc;
    }

    public static DT_KhoaHocCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_KhoaHocCls[] DT_KhoaHocs = new DT_KhoaHocCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_KhoaHocs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_KhoaHocs;
    }


    public static DT_KhoaHocCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_KhoaHocCls[] DT_KhoaHocs = ParseFromDataTable(ds.Tables[0]);
        return DT_KhoaHocs;
    }


    public static DT_KhoaHocCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_KhoaHocCls ODT_KhoaHoc = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_KhoaHoc;
    }


    public static XmlCls GetXml(DT_KhoaHocCls[] DT_KhoaHocs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("MA");
        ds.Tables["info"].Columns.Add("TEN");
        ds.Tables["info"].Columns.Add("TENKHOAHOC");
        ds.Tables["info"].Columns.Add("KHOA", typeof(int));
        ds.Tables["info"].Columns.Add("THOILUONG", typeof(int?));
        ds.Tables["info"].Columns.Add("LOAITHOILUONG");
        ds.Tables["info"].Columns.Add("NGAYKHAIGIANGDUKIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("HANNOPHOSO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGAYBEGIANGDUKIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("SOLUONGHOCVIENDUKIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("HOCPHI", typeof(decimal?));
        ds.Tables["info"].Columns.Add("LOAIKHOAHOC", typeof(int));
        ds.Tables["info"].Columns.Add("LOAIDAOTAO", typeof(int));
        ds.Tables["info"].Columns.Add("DONVIHOTRO_MA");
        ds.Tables["info"].Columns.Add("MOTA");
        ds.Tables["info"].Columns.Add("ROOMID");
        ds.Tables["info"].Columns.Add("CONVID");
        ds.Tables["info"].Columns.Add("LOAIHINHDAOTAO", typeof(int));
        ds.Tables["info"].Columns.Add("DOITUONG");
        ds.Tables["info"].Columns.Add("TIEUCHUAN");
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        for (int iIndex = 0; iIndex < DT_KhoaHocs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_KhoaHocs[iIndex].ID,
                DT_KhoaHocs[iIndex].MA,
                DT_KhoaHocs[iIndex].TEN,
                DT_KhoaHocs[iIndex].TENKHOAHOC,
                DT_KhoaHocs[iIndex].KHOA,
                DT_KhoaHocs[iIndex].THOILUONG,
                DT_KhoaHocs[iIndex].LOAITHOILUONG,
                DT_KhoaHocs[iIndex].NGAYKHAIGIANGDUKIEN,
                DT_KhoaHocs[iIndex].HANNOPHOSO,
                DT_KhoaHocs[iIndex].NGAYBEGIANGDUKIEN,
                DT_KhoaHocs[iIndex].SOLUONGHOCVIENDUKIEN,
                DT_KhoaHocs[iIndex].HOCPHI,
                DT_KhoaHocs[iIndex].LOAIKHOAHOC,
                DT_KhoaHocs[iIndex].LOAIDAOTAO,
                DT_KhoaHocs[iIndex].DONVIHOTRO_MA,
                DT_KhoaHocs[iIndex].MOTA,
                DT_KhoaHocs[iIndex].ROOMID,
                DT_KhoaHocs[iIndex].CONVID,
                DT_KhoaHocs[iIndex].LOAIHINHDAOTAO,
                DT_KhoaHocs[iIndex].DOITUONG,
                DT_KhoaHocs[iIndex].TIEUCHUAN,
                DT_KhoaHocs[iIndex].TRANGTHAI,
                DT_KhoaHocs[iIndex].NGUOITAO_ID,
                DT_KhoaHocs[iIndex].NGAYTAO,
                DT_KhoaHocs[iIndex].NGUOISUA_ID,
                DT_KhoaHocs[iIndex].NGAYSUA
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_KhoaHocCls ODT_KhoaHoc)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("MA");
        ds.Tables["info"].Columns.Add("TEN");
        ds.Tables["info"].Columns.Add("TENKHOAHOC");
        ds.Tables["info"].Columns.Add("KHOA", typeof(int));
        ds.Tables["info"].Columns.Add("THOILUONG", typeof(int?));
        ds.Tables["info"].Columns.Add("LOAITHOILUONG");
        ds.Tables["info"].Columns.Add("NGAYKHAIGIANGDUKIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("HANNOPHOSO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGAYBEGIANGDUKIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("SOLUONGHOCVIENDUKIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("HOCPHI", typeof(decimal?));
        ds.Tables["info"].Columns.Add("LOAIKHOAHOC", typeof(int));
        ds.Tables["info"].Columns.Add("LOAIDAOTAO", typeof(int));
        ds.Tables["info"].Columns.Add("DONVIHOTRO_MA");
        ds.Tables["info"].Columns.Add("MOTA");
        ds.Tables["info"].Columns.Add("ROOMID");
        ds.Tables["info"].Columns.Add("CONVID");
        ds.Tables["info"].Columns.Add("LOAIHINHDAOTAO", typeof(int));
        ds.Tables["info"].Columns.Add("DOITUONG");
        ds.Tables["info"].Columns.Add("TIEUCHUAN");
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_KhoaHoc.ID,
                ODT_KhoaHoc.MA,
                ODT_KhoaHoc.TEN,
                ODT_KhoaHoc.TENKHOAHOC,
                ODT_KhoaHoc.KHOA,
                ODT_KhoaHoc.THOILUONG,
                ODT_KhoaHoc.LOAITHOILUONG,
                ODT_KhoaHoc.NGAYKHAIGIANGDUKIEN,
                ODT_KhoaHoc.HANNOPHOSO,
                ODT_KhoaHoc.NGAYBEGIANGDUKIEN,
                ODT_KhoaHoc.SOLUONGHOCVIENDUKIEN,
                ODT_KhoaHoc.HOCPHI,
                ODT_KhoaHoc.LOAIKHOAHOC,
                ODT_KhoaHoc.LOAIDAOTAO,
                ODT_KhoaHoc.DONVIHOTRO_MA,
                ODT_KhoaHoc.MOTA,
                ODT_KhoaHoc.ROOMID,
                ODT_KhoaHoc.CONVID,
                ODT_KhoaHoc.LOAIHINHDAOTAO,
                ODT_KhoaHoc.DOITUONG,
                ODT_KhoaHoc.TIEUCHUAN,
                ODT_KhoaHoc.TRANGTHAI,
                ODT_KhoaHoc.NGUOITAO_ID,
                ODT_KhoaHoc.NGAYTAO,
                ODT_KhoaHoc.NGUOISUA_ID,
                ODT_KhoaHoc.NGAYSUA
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
