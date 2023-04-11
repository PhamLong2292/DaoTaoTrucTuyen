using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class BienBanHoiChanToanLichCls 
    { 
        public string LICHHOICHANID; 
        public DateTime BATDAU;
        public DateTime? KETTHUC;
        public string DIADIEM; 
        public string CHUTRI; 
        public string THUKY; 
        public int? YKIENNOIDUNGHOICHAN;
        public int? YKIENCACHTHUCTRINHBAY;
        public int? YKIENTHAIDOTHANHVIEN;
        public string YKIENCUABVVETINH; 
        public string TAOBOI; 
        public DateTime TAOVAO;
        public string SUABOI; 
        public DateTime? SUAVAO;
        public int TRANGTHAI;
        public enum eYKienNoiDungHoiChan
        {
            PhongPhu = 0,
            BinhThuong = 1,
            ChuaPhongPhu = 2
        }
        public enum eYKienCachThucTrinhBay
        {
            KhoaHoc = 0,
            BinhThuong = 1,
            ChuaKhoaHoc = 2
        }
        public enum eYKienThaiDoThanhVien
        {
            TichCuc = 0,
            BinhThuong = 1,
            ChuaTichCuc = 2
        }
    }
}

public class BienBanHoiChanToanLichParser
{
    public readonly static Dictionary<int, string> YKienNoiDungHoiChans = new Dictionary<int, string>()
    {
        { (int)BienBanHoiChanToanLichCls.eYKienNoiDungHoiChan.PhongPhu, "Phong phú" },
        { (int)BienBanHoiChanToanLichCls.eYKienNoiDungHoiChan.BinhThuong, "Bình thường" },
        { (int)BienBanHoiChanToanLichCls.eYKienNoiDungHoiChan.ChuaPhongPhu, "Chưa phong phú" }
    };
    public readonly static Dictionary<int, string> YKienCachThucTrinhBays = new Dictionary<int, string>()
    {
        { (int)BienBanHoiChanToanLichCls.eYKienCachThucTrinhBay.KhoaHoc, "Khoa học" },
        { (int)BienBanHoiChanToanLichCls.eYKienCachThucTrinhBay.BinhThuong, "Bình thường" },
        { (int)BienBanHoiChanToanLichCls.eYKienCachThucTrinhBay.ChuaKhoaHoc, "Chưa khoa học" }
    };
    public readonly static Dictionary<int, string> YKienThaiDoThanhViens = new Dictionary<int, string>()
    {
        { (int)BienBanHoiChanToanLichCls.eYKienThaiDoThanhVien.TichCuc, "Tích cực" },
        { (int)BienBanHoiChanToanLichCls.eYKienThaiDoThanhVien.BinhThuong, "Bình thường" },
        { (int)BienBanHoiChanToanLichCls.eYKienThaiDoThanhVien.ChuaTichCuc, "Chưa tích cực" }
    };
    public static BienBanHoiChanToanLichCls CreateInstance() 
    { 
        BienBanHoiChanToanLichCls OBienBanHoiChanToanLich = new BienBanHoiChanToanLichCls(); 
        return OBienBanHoiChanToanLich; 
    } 


    public static BienBanHoiChanToanLichCls ParseFromDataRow(DataRow dr) 
    { 
        BienBanHoiChanToanLichCls OBienBanHoiChanToanLich = new BienBanHoiChanToanLichCls(); 
        OBienBanHoiChanToanLich.LICHHOICHANID = CoreXmlUtility.GetString(dr, "LICHHOICHANID", true);
        OBienBanHoiChanToanLich.BATDAU = CoreXmlUtility.GetDate(dr, "BATDAU", true);
        OBienBanHoiChanToanLich.KETTHUC = CoreXmlUtility.GetDateOrNull(dr, "KETTHUC", true);
        OBienBanHoiChanToanLich.DIADIEM = CoreXmlUtility.GetString(dr, "DIADIEM", true);
        OBienBanHoiChanToanLich.CHUTRI = CoreXmlUtility.GetString(dr, "CHUTRI", true);
        OBienBanHoiChanToanLich.THUKY = CoreXmlUtility.GetString(dr, "THUKY", true);
        OBienBanHoiChanToanLich.YKIENNOIDUNGHOICHAN = CoreXmlUtility.GetIntOrNull(dr, "YKIENNOIDUNGHOICHAN", true);
        OBienBanHoiChanToanLich.YKIENCACHTHUCTRINHBAY = CoreXmlUtility.GetIntOrNull(dr, "YKIENCACHTHUCTRINHBAY", true);
        OBienBanHoiChanToanLich.YKIENTHAIDOTHANHVIEN = CoreXmlUtility.GetIntOrNull(dr, "YKIENTHAIDOTHANHVIEN", true);
        OBienBanHoiChanToanLich.YKIENCUABVVETINH = CoreXmlUtility.GetString(dr, "YKIENCUABVVETINH", true);
        OBienBanHoiChanToanLich.TAOBOI = CoreXmlUtility.GetString(dr, "TAOBOI", true);
        OBienBanHoiChanToanLich.TAOVAO = CoreXmlUtility.GetDate(dr, "TAOVAO", true);
        OBienBanHoiChanToanLich.SUABOI = CoreXmlUtility.GetString(dr, "SUABOI", true);
        OBienBanHoiChanToanLich.SUAVAO = CoreXmlUtility.GetDateOrNull(dr, "SUAVAO", true);
        OBienBanHoiChanToanLich.TRANGTHAI = CoreXmlUtility.GetInt(dr, "TRANGTHAI", true);
        return OBienBanHoiChanToanLich;
    }

    public static BienBanHoiChanToanLichCls[] ParseFromDataTable(DataTable dtTable) 
    {
        BienBanHoiChanToanLichCls[] BienBanHoiChanToanLichs = new BienBanHoiChanToanLichCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            BienBanHoiChanToanLichs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return BienBanHoiChanToanLichs;
    }


    public static BienBanHoiChanToanLichCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        BienBanHoiChanToanLichCls[] BienBanHoiChanToanLichs = ParseFromDataTable(ds.Tables[0]);
        return BienBanHoiChanToanLichs;
    }

public static long CountFromDataTable(DataTable dtTable)
{
    if (dtTable != null && dtTable.Rows.Count > 0)
    {
        return Int64.Parse(dtTable.Rows[0][0].ToString());
    }
    else
        return 0;
}

    public static BienBanHoiChanToanLichCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        BienBanHoiChanToanLichCls OBienBanHoiChanToanLich = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBienBanHoiChanToanLich;
    }


    public static XmlCls GetXml(BienBanHoiChanToanLichCls[] BienBanHoiChanToanLichs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("BATDAU",typeof(DateTime));
        ds.Tables["info"].Columns.Add("KETTHUC",typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEM");
        ds.Tables["info"].Columns.Add("CHUTRI");
        ds.Tables["info"].Columns.Add("THUKY");
        ds.Tables["info"].Columns.Add("YKIENNOIDUNGHOICHAN",typeof(int?));
        ds.Tables["info"].Columns.Add("YKIENCACHTHUCTRINHBAY",typeof(int?));
        ds.Tables["info"].Columns.Add("YKIENTHAIDOTHANHVIEN",typeof(int?));
        ds.Tables["info"].Columns.Add("YKIENCUABVVETINH");
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO",typeof(DateTime));
        ds.Tables["info"].Columns.Add("SUABOI");
        ds.Tables["info"].Columns.Add("SUAVAO",typeof(DateTime?));
        ds.Tables["info"].Columns.Add("TRANGTHAI",typeof(int));
        for (int iIndex = 0; iIndex < BienBanHoiChanToanLichs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                BienBanHoiChanToanLichs[iIndex].LICHHOICHANID,
                BienBanHoiChanToanLichs[iIndex].BATDAU,
                BienBanHoiChanToanLichs[iIndex].KETTHUC,
                BienBanHoiChanToanLichs[iIndex].DIADIEM,
                BienBanHoiChanToanLichs[iIndex].CHUTRI,
                BienBanHoiChanToanLichs[iIndex].THUKY,
                BienBanHoiChanToanLichs[iIndex].YKIENNOIDUNGHOICHAN,
                BienBanHoiChanToanLichs[iIndex].YKIENCACHTHUCTRINHBAY,
                BienBanHoiChanToanLichs[iIndex].YKIENTHAIDOTHANHVIEN,
                BienBanHoiChanToanLichs[iIndex].YKIENCUABVVETINH,
                BienBanHoiChanToanLichs[iIndex].TAOBOI,
                BienBanHoiChanToanLichs[iIndex].TAOVAO,
                BienBanHoiChanToanLichs[iIndex].SUABOI,
                BienBanHoiChanToanLichs[iIndex].SUAVAO,
                BienBanHoiChanToanLichs[iIndex].TRANGTHAI
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(BienBanHoiChanToanLichCls OBienBanHoiChanToanLich)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("BATDAU",typeof(DateTime));
        ds.Tables["info"].Columns.Add("KETTHUC",typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEM");
        ds.Tables["info"].Columns.Add("CHUTRI");
        ds.Tables["info"].Columns.Add("THUKY");
        ds.Tables["info"].Columns.Add("YKIENNOIDUNGHOICHAN",typeof(int?));
        ds.Tables["info"].Columns.Add("YKIENCACHTHUCTRINHBAY",typeof(int?));
        ds.Tables["info"].Columns.Add("YKIENTHAIDOTHANHVIEN",typeof(int?));
        ds.Tables["info"].Columns.Add("YKIENCUABVVETINH");
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO",typeof(DateTime));
        ds.Tables["info"].Columns.Add("SUABOI");
        ds.Tables["info"].Columns.Add("SUAVAO",typeof(DateTime?));
        ds.Tables["info"].Columns.Add("TRANGTHAI",typeof(int));
            ds.Tables["info"].Rows.Add(new object[]
            {
                OBienBanHoiChanToanLich.LICHHOICHANID,
                OBienBanHoiChanToanLich.BATDAU,
                OBienBanHoiChanToanLich.KETTHUC,
                OBienBanHoiChanToanLich.DIADIEM,
                OBienBanHoiChanToanLich.CHUTRI,
                OBienBanHoiChanToanLich.THUKY,
                OBienBanHoiChanToanLich.YKIENNOIDUNGHOICHAN,
                OBienBanHoiChanToanLich.YKIENCACHTHUCTRINHBAY,
                OBienBanHoiChanToanLich.YKIENTHAIDOTHANHVIEN,
                OBienBanHoiChanToanLich.YKIENCUABVVETINH,
                OBienBanHoiChanToanLich.TAOBOI,
                OBienBanHoiChanToanLich.TAOVAO,
                OBienBanHoiChanToanLich.SUABOI,
                OBienBanHoiChanToanLich.SUAVAO,
                OBienBanHoiChanToanLich.TRANGTHAI
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
