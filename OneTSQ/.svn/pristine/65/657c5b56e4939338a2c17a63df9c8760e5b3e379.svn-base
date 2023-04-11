using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;

namespace OneTSQ.Model
{
    public class DangKyDeTaiCls
    {
        public string ID;
        public int LOAIHINH;
        public string MA;
        public string TENDETAI;
        public int? CAPDETAI;
        public string CHUNHIEM_ID;
        public string CHUCDANH_ID;
        public string DIENTHOAI;
        public string EMAIL;
        public DateTime? THOIGIANDANGKY;
        public DateTime? THOIGIANDUYETDK;
        public string NGUOIDUYET_ID;
        public decimal KINHPHIDUKIEN;
        public string YNGHIATHUCTIEN;
        public string YNGHIAKHOAHOC;
        public string TINHKHATHI;
        public string MUCTIEU;
        public string NOIDUNGCHUYEU;
        public string PHUONGPHAPNGHIENCUU;
        public string DUKIENKETQUA;
        public string KHANANGDIACHIAPDUNG;
        public int? TRANGTHAI;
        public int? ISNEW;
        public enum eTrangThai
        {
            Moi = 0,
            ChoDuyet = 1,
            DaXetDuyet = 2,
            TuChoi = 3
        }
        public enum TacVu
        {
            Luu = 0,
            hoantat = 1,
            Xoa = 2,
        }
        public enum eISNEW
        {
            ChuaGan = 0,
            DaGan = 1,        
        }
        public enum eCapDeTai
        {
            CoSo = 0,
            Tinh = 1,
            NhaNuoc = 2,
        }
        public enum eLoaiHinh
        {         
            DeTai = 0,        
            SangKien = 1,
            DeCuong = 2,
        }
    }
}

public class DangKyDeTaiParser
{
    public readonly static Dictionary<int, string> TrangThais = new Dictionary<int, string>()
        {
            { (int)DangKyDeTaiCls.eTrangThai.Moi, "Mới" },
            { (int)DangKyDeTaiCls.eTrangThai.ChoDuyet, "Chờ duyệt" },
            { (int)DangKyDeTaiCls.eTrangThai.DaXetDuyet, "Đã xét duyệt" },
            { (int)DangKyDeTaiCls.eTrangThai.TuChoi, "Từ chối duyệt" }
        };
    public readonly static Dictionary<int, string> sColorTrangThai = new Dictionary<int, string>()
        {
            { (int)DangKyDeTaiCls.eTrangThai.Moi, "<span style=\"background-color:orange;color:white;\" class=\"badge\" >Mới</span>" },
            { (int)DangKyDeTaiCls.eTrangThai.ChoDuyet, "<span style=\"background-color:violet; color:black;\" class=\"badge\">Chờ duyệt</span>" },
            { (int)DangKyDeTaiCls.eTrangThai.DaXetDuyet, "<span style=\"background-color:green;color:white;\" class=\"badge\" >Đã xét duyệt</span>" },
            { (int)DangKyDeTaiCls.eTrangThai.TuChoi, "<span style=\"background-color:red;color:white;\" class=\"badge\" >Từ chối duyệt</span>" },
        };
    public readonly static Dictionary<int, string> ISNEWs = new Dictionary<int, string>()
        {
            { (int)DangKyDeTaiCls.eISNEW.ChuaGan, "Chưa gán" },
            { (int)DangKyDeTaiCls.eISNEW.DaGan, "Đã gán" },
        };
    public readonly static Dictionary<int, string> CapDeTais = new Dictionary<int, string>()
        {
            { (int)DangKyDeTaiCls.eCapDeTai.CoSo, "Cơ sở" },
            { (int)DangKyDeTaiCls.eCapDeTai.Tinh, "Tỉnh" },
            { (int)DangKyDeTaiCls.eCapDeTai.NhaNuoc, "Nhà nước" },
        };
    public readonly static Dictionary<int, string> LoaiHinhs = new Dictionary<int, string>()
        {                
            { (int)DangKyDeTaiCls.eLoaiHinh.DeTai, "Đề tài" },
            { (int)DangKyDeTaiCls.eLoaiHinh.SangKien, "Sáng kiến" },
            { (int)DangKyDeTaiCls.eLoaiHinh.DeCuong, "Đề cương" },
        };
    public static DangKyDeTaiCls CreateInstance()
    {
        DangKyDeTaiCls ODangKyDeTai = new DangKyDeTaiCls();
        return ODangKyDeTai;
    }
    public static DangKyDeTaiCls ParseFromDataRow(DataRow dr)
    {
        DangKyDeTaiCls ODangKyDeTai = new DangKyDeTaiCls();
        ODangKyDeTai.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODangKyDeTai.LOAIHINH = CoreXmlUtility.GetInt(dr, "LOAIHINH", true);
        ODangKyDeTai.MA = CoreXmlUtility.GetString(dr, "MA", true);
        ODangKyDeTai.TENDETAI = CoreXmlUtility.GetString(dr, "TENDETAI", true);
        ODangKyDeTai.CAPDETAI = CoreXmlUtility.GetInt(dr, "CAPDETAI", true);
        ODangKyDeTai.CHUNHIEM_ID = CoreXmlUtility.GetString(dr, "CHUNHIEM_ID", true);
        ODangKyDeTai.CHUCDANH_ID = CoreXmlUtility.GetString(dr, "CHUCDANH_ID", true);
        ODangKyDeTai.DIENTHOAI = CoreXmlUtility.GetString(dr, "DIENTHOAI", true);
        ODangKyDeTai.EMAIL = CoreXmlUtility.GetString(dr, "EMAIL", true);
        ODangKyDeTai.THOIGIANDANGKY = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANDANGKY", true);
        ODangKyDeTai.THOIGIANDUYETDK = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANDUYETDK", true);
        ODangKyDeTai.NGUOIDUYET_ID = CoreXmlUtility.GetString(dr, "NGUOIDUYET_ID", true);
        ODangKyDeTai.KINHPHIDUKIEN = CoreXmlUtility.GetDecimal(dr, "KINHPHIDUKIEN", true);
        ODangKyDeTai.YNGHIATHUCTIEN = CoreXmlUtility.GetString(dr, "YNGHIATHUCTIEN", true);
        ODangKyDeTai.YNGHIAKHOAHOC = CoreXmlUtility.GetString(dr, "YNGHIAKHOAHOC", true);
        ODangKyDeTai.TINHKHATHI = CoreXmlUtility.GetString(dr, "TINHKHATHI", true);
        ODangKyDeTai.MUCTIEU = CoreXmlUtility.GetString(dr, "MUCTIEU", true);
        ODangKyDeTai.NOIDUNGCHUYEU = CoreXmlUtility.GetString(dr, "NOIDUNGCHUYEU", true);
        ODangKyDeTai.PHUONGPHAPNGHIENCUU = CoreXmlUtility.GetString(dr, "PHUONGPHAPNGHIENCUU", true);
        ODangKyDeTai.DUKIENKETQUA = CoreXmlUtility.GetString(dr, "DUKIENKETQUA", true);
        ODangKyDeTai.KHANANGDIACHIAPDUNG = CoreXmlUtility.GetString(dr, "KHANANGDIACHIAPDUNG", true);
        ODangKyDeTai.TRANGTHAI = CoreXmlUtility.GetInt(dr, "TRANGTHAI", true);
        ODangKyDeTai.ISNEW = CoreXmlUtility.GetInt(dr, "ISNEW", true);
        return ODangKyDeTai;
    }

    public static DangKyDeTaiCls[] ParseFromDataTable(DataTable dtTable)
    {
        DangKyDeTaiCls[] DangKyDeTais = new DangKyDeTaiCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DangKyDeTais[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DangKyDeTais;
    }


    public static DangKyDeTaiCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DangKyDeTaiCls[] DangKyDeTais = ParseFromDataTable(ds.Tables[0]);
        return DangKyDeTais;
    }


    public static DangKyDeTaiCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DangKyDeTaiCls ODangKyDeTai = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODangKyDeTai;
    }


    public static XmlCls GetXml(DangKyDeTaiCls[] DangKyDeTais)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LOAIHINH", typeof(int?));
        ds.Tables["info"].Columns.Add("MA");
        ds.Tables["info"].Columns.Add("TENDETAI");
        ds.Tables["info"].Columns.Add("CAPDETAI", typeof(int?));
        ds.Tables["info"].Columns.Add("CHUNHIEM_ID");
        ds.Tables["info"].Columns.Add("CHUCDANH_ID");
        ds.Tables["info"].Columns.Add("DIENTHOAI");
        ds.Tables["info"].Columns.Add("EMAIL");
        ds.Tables["info"].Columns.Add("THOIGIANDANGKY", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("THOIGIANDUYETDK", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGUOIDUYET_ID");
        ds.Tables["info"].Columns.Add("KINHPHIDUKIEN");
        ds.Tables["info"].Columns.Add("YNGHIATHUCTIEN");
        ds.Tables["info"].Columns.Add("YNGHIAKHOAHOC");
        ds.Tables["info"].Columns.Add("TINHKHATHI");
        ds.Tables["info"].Columns.Add("MUCTIEU");
        ds.Tables["info"].Columns.Add("NOIDUNGCHUYEU");
        ds.Tables["info"].Columns.Add("PHUONGPHAPNGHIENCUU");
        ds.Tables["info"].Columns.Add("DUKIENKETQUA");
        ds.Tables["info"].Columns.Add("KHANANGDIACHIAPDUNG");      
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Columns.Add("ISNEW", typeof(int?));
        for (int iIndex = 0; iIndex < DangKyDeTais.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DangKyDeTais[iIndex].ID,
                DangKyDeTais[iIndex].LOAIHINH,
                DangKyDeTais[iIndex].MA,
                DangKyDeTais[iIndex].TENDETAI,
                DangKyDeTais[iIndex].CAPDETAI,
                DangKyDeTais[iIndex].CHUNHIEM_ID,
                DangKyDeTais[iIndex].CHUCDANH_ID,
                DangKyDeTais[iIndex].DIENTHOAI,
                DangKyDeTais[iIndex].EMAIL,
                DangKyDeTais[iIndex].THOIGIANDANGKY,
                DangKyDeTais[iIndex].THOIGIANDUYETDK,
                DangKyDeTais[iIndex].NGUOIDUYET_ID,
                DangKyDeTais[iIndex].KINHPHIDUKIEN,
                DangKyDeTais[iIndex].YNGHIATHUCTIEN,
                DangKyDeTais[iIndex].YNGHIAKHOAHOC,
                DangKyDeTais[iIndex].TINHKHATHI,
                DangKyDeTais[iIndex].MUCTIEU,
                DangKyDeTais[iIndex].NOIDUNGCHUYEU,
                DangKyDeTais[iIndex].PHUONGPHAPNGHIENCUU,
                DangKyDeTais[iIndex].DUKIENKETQUA,
                DangKyDeTais[iIndex].KHANANGDIACHIAPDUNG,
                DangKyDeTais[iIndex].TRANGTHAI,
                DangKyDeTais[iIndex].ISNEW,         
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DangKyDeTaiCls ODangKyDeTai)
    {
        DataSet ds = new DataSet();
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LOAIHINH", typeof(int?));
        ds.Tables["info"].Columns.Add("MA");
        ds.Tables["info"].Columns.Add("TENDETAI");
        ds.Tables["info"].Columns.Add("CAPDETAI", typeof(int?));
        ds.Tables["info"].Columns.Add("CHUNHIEM_ID");
        ds.Tables["info"].Columns.Add("CHUCDANH_ID");
        ds.Tables["info"].Columns.Add("DIENTHOAI");
        ds.Tables["info"].Columns.Add("EMAIL");
        ds.Tables["info"].Columns.Add("THOIGIANDANGKY", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("THOIGIANDUYETDK", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGUOIDUYET_ID");
        ds.Tables["info"].Columns.Add("KINHPHIDUKIEN");
        ds.Tables["info"].Columns.Add("YNGHIATHUCTIEN");
        ds.Tables["info"].Columns.Add("YNGHIAKHOAHOC");
        ds.Tables["info"].Columns.Add("TINHKHATHI");
        ds.Tables["info"].Columns.Add("MUCTIEU");
        ds.Tables["info"].Columns.Add("NOIDUNGCHUYEU");
        ds.Tables["info"].Columns.Add("PHUONGPHAPNGHIENCUU");
        ds.Tables["info"].Columns.Add("DUKIENKETQUA");
        ds.Tables["info"].Columns.Add("KHANANGDIACHIAPDUNG");
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Columns.Add("ISNEW", typeof(int?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODangKyDeTai.ID,
                ODangKyDeTai.LOAIHINH,
                ODangKyDeTai.MA,
                ODangKyDeTai.TENDETAI,
                ODangKyDeTai.CAPDETAI,
                ODangKyDeTai.CHUNHIEM_ID,
                ODangKyDeTai.CHUCDANH_ID,
                ODangKyDeTai.DIENTHOAI,
                ODangKyDeTai.EMAIL,
                ODangKyDeTai.THOIGIANDANGKY,
                ODangKyDeTai.THOIGIANDUYETDK,
                ODangKyDeTai.NGUOIDUYET_ID,
                ODangKyDeTai.KINHPHIDUKIEN,
                ODangKyDeTai.YNGHIATHUCTIEN,
                ODangKyDeTai.YNGHIAKHOAHOC,
                ODangKyDeTai.TINHKHATHI,
                ODangKyDeTai.MUCTIEU,
                ODangKyDeTai.NOIDUNGCHUYEU,
                ODangKyDeTai.PHUONGPHAPNGHIENCUU,
                ODangKyDeTai.DUKIENKETQUA,
                ODangKyDeTai.KHANANGDIACHIAPDUNG,
                ODangKyDeTai.TRANGTHAI,
                ODangKyDeTai.ISNEW,
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
