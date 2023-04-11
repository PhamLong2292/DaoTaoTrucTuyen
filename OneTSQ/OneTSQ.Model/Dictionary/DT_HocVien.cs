using System;
using System.Data;
using OneTSQ.Model;
using OneMES3.SYS.Core.Model;

namespace OneTSQ.Model
{
    public class DT_HocVienCls
    {
        public string ID;
        public string MA;
        public string HOTEN = "";
        public string TEN = "";
        public DateTime? NGAYSINH;
        public string NOISINH_MA = "";
        public int? GIOITINH;
        public string DANTOC_MA = "";
        public string CMTND = "";
        public DateTime? CMTND_NGAYCAP;
        public string CMTND_NOICAP = "";
        public string KHOAPHONGCONGTAC = "";
        public string DONVICONGTAC_MA = "";
        public int? SONAMKINHNGHIEM;
        public string DIACHISONHA = "";
        public string DIACHIHANHCHINH_MA = "";
        public string DIENTHOAI = "";
        public string EMAIL = "";
        public string NOIDUNGANH = "";
        public string EXTANH = "";
        public string TOTNGHIEP_MA = "";
        public int? NAMTOTNGHIEP;
        public string TRUONGCAPBANG = "";
        public string CHUYENMON_MA = "";
        public string CHUYENNGANH_MA = "";
        public string USERNAME = "";
        public string NGUOITAO_ID = "";
        public DateTime NGAYTAO;
        public string NGUOISUA_ID = "";
        public DateTime? NGAYSUA;
    }
}

public class DT_HocVienParser
{
    public static DT_HocVienCls CreateInstance()
    {
        DT_HocVienCls ODT_HocVien = new DT_HocVienCls();
        return ODT_HocVien;
    }


    public static DT_HocVienCls ParseFromDataRow(DataRow dr)
    {
        DT_HocVienCls ODT_HocVien = new DT_HocVienCls();
        ODT_HocVien.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_HocVien.MA = CoreXmlUtility.GetString(dr, "MA", true);
        ODT_HocVien.HOTEN = CoreXmlUtility.GetString(dr, "HOTEN", true);
        ODT_HocVien.TEN = CoreXmlUtility.GetString(dr, "TEN", true);
        ODT_HocVien.NGAYSINH = CoreXmlUtility.GetDateOrNull(dr, "NGAYSINH", true);
        ODT_HocVien.NOISINH_MA = CoreXmlUtility.GetString(dr, "NOISINH_MA", true);
        ODT_HocVien.GIOITINH = CoreXmlUtility.GetIntOrNull(dr, "GIOITINH", true);
        ODT_HocVien.DANTOC_MA = CoreXmlUtility.GetString(dr, "DANTOC_MA", true);
        ODT_HocVien.CMTND = CoreXmlUtility.GetString(dr, "CMTND", true);
        ODT_HocVien.CMTND_NGAYCAP = CoreXmlUtility.GetDateOrNull(dr, "CMTND_NGAYCAP", true);
        ODT_HocVien.CMTND_NOICAP = CoreXmlUtility.GetString(dr, "CMTND_NOICAP", true);
        ODT_HocVien.KHOAPHONGCONGTAC = CoreXmlUtility.GetString(dr, "KHOAPHONGCONGTAC", true);
        ODT_HocVien.DONVICONGTAC_MA = CoreXmlUtility.GetString(dr, "DONVICONGTAC_MA", true);
        ODT_HocVien.SONAMKINHNGHIEM = CoreXmlUtility.GetIntOrNull(dr, "SONAMKINHNGHIEM", true);
        ODT_HocVien.DIACHISONHA = CoreXmlUtility.GetString(dr, "DIACHISONHA", true);
        ODT_HocVien.DIACHIHANHCHINH_MA = CoreXmlUtility.GetString(dr, "DIACHIHANHCHINH_MA", true);
        ODT_HocVien.DIENTHOAI = CoreXmlUtility.GetString(dr, "DIENTHOAI", true);
        ODT_HocVien.EMAIL = CoreXmlUtility.GetString(dr, "EMAIL", true);
        ODT_HocVien.NOIDUNGANH = CoreXmlUtility.GetString(dr, "NOIDUNGANH", true);
        ODT_HocVien.EXTANH = CoreXmlUtility.GetString(dr, "EXTANH", true);
        ODT_HocVien.TOTNGHIEP_MA = CoreXmlUtility.GetString(dr, "TOTNGHIEP_MA", true);
        ODT_HocVien.NAMTOTNGHIEP = CoreXmlUtility.GetIntOrNull(dr, "NAMTOTNGHIEP", true);
        ODT_HocVien.TRUONGCAPBANG = CoreXmlUtility.GetString(dr, "TRUONGCAPBANG", true);
        ODT_HocVien.CHUYENMON_MA = CoreXmlUtility.GetString(dr, "CHUYENMON_MA", true);
        ODT_HocVien.CHUYENNGANH_MA = CoreXmlUtility.GetString(dr, "CHUYENNGANH_MA", true);
        ODT_HocVien.USERNAME = CoreXmlUtility.GetString(dr, "USERNAME", true);
        ODT_HocVien.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_HocVien.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        ODT_HocVien.NGUOISUA_ID = CoreXmlUtility.GetString(dr, "NGUOISUA_ID", true);
        ODT_HocVien.NGAYSUA = CoreXmlUtility.GetDateOrNull(dr, "NGAYSUA", true);
        return ODT_HocVien;
    }

    public static DT_HocVienCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_HocVienCls[] DT_HocViens = new DT_HocVienCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_HocViens[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_HocViens;
    }


    public static DT_HocVienCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_HocVienCls[] DT_HocViens = ParseFromDataTable(ds.Tables[0]);
        return DT_HocViens;
    }


    public static DT_HocVienCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_HocVienCls ODT_HocVien = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_HocVien;
    }


    public static XmlCls GetXml(DT_HocVienCls[] DT_HocViens)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("MA");
        ds.Tables["info"].Columns.Add("HOTEN");
        ds.Tables["info"].Columns.Add("TEN");
        ds.Tables["info"].Columns.Add("NGAYSINH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NOISINH_MA");
        ds.Tables["info"].Columns.Add("GIOITINH", typeof(int?));
        ds.Tables["info"].Columns.Add("DANTOC_MA");
        ds.Tables["info"].Columns.Add("CMTND");
        ds.Tables["info"].Columns.Add("CMTND_NGAYCAP", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("CMTND_NOICAP");
        ds.Tables["info"].Columns.Add("KHOAPHONGCONGTAC");
        ds.Tables["info"].Columns.Add("DONVICONGTAC_MA");
        ds.Tables["info"].Columns.Add("SONAMKINHNGHIEM", typeof(int?));
        ds.Tables["info"].Columns.Add("DIACHISONHA");
        ds.Tables["info"].Columns.Add("DIACHIHANHCHINH_MA");
        ds.Tables["info"].Columns.Add("DIENTHOAI");
        ds.Tables["info"].Columns.Add("EMAIL");
        ds.Tables["info"].Columns.Add("NOIDUNGANH");
        ds.Tables["info"].Columns.Add("EXTANH");
        ds.Tables["info"].Columns.Add("TOTNGHIEP_MA");
        ds.Tables["info"].Columns.Add("NAMTOTNGHIEP", typeof(int?));
        ds.Tables["info"].Columns.Add("TRUONGCAPBANG");
        ds.Tables["info"].Columns.Add("CHUYENMON_MA");
        ds.Tables["info"].Columns.Add("CHUYENNGANH_MA");
        ds.Tables["info"].Columns.Add("USERNAME");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        for (int iIndex = 0; iIndex < DT_HocViens.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_HocViens[iIndex].ID,
                DT_HocViens[iIndex].MA,
                DT_HocViens[iIndex].HOTEN,
                DT_HocViens[iIndex].TEN,
                DT_HocViens[iIndex].NGAYSINH,
                DT_HocViens[iIndex].NOISINH_MA,
                DT_HocViens[iIndex].GIOITINH,
                DT_HocViens[iIndex].DANTOC_MA,
                DT_HocViens[iIndex].CMTND,
                DT_HocViens[iIndex].CMTND_NGAYCAP,
                DT_HocViens[iIndex].CMTND_NOICAP,
                DT_HocViens[iIndex].KHOAPHONGCONGTAC,
                DT_HocViens[iIndex].DONVICONGTAC_MA,
                DT_HocViens[iIndex].SONAMKINHNGHIEM,
                DT_HocViens[iIndex].DIACHISONHA,
                DT_HocViens[iIndex].DIACHIHANHCHINH_MA,
                DT_HocViens[iIndex].DIENTHOAI,
                DT_HocViens[iIndex].EMAIL,
                DT_HocViens[iIndex].NOIDUNGANH,
                DT_HocViens[iIndex].EXTANH,
                DT_HocViens[iIndex].TOTNGHIEP_MA,
                DT_HocViens[iIndex].NAMTOTNGHIEP,
                DT_HocViens[iIndex].TRUONGCAPBANG,
                DT_HocViens[iIndex].CHUYENMON_MA,
                DT_HocViens[iIndex].CHUYENNGANH_MA,
                DT_HocViens[iIndex].USERNAME,
                DT_HocViens[iIndex].NGUOITAO_ID,
                DT_HocViens[iIndex].NGAYTAO,
                DT_HocViens[iIndex].NGUOISUA_ID,
                DT_HocViens[iIndex].NGAYSUA
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_HocVienCls ODT_HocVien)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("MA");
        ds.Tables["info"].Columns.Add("HOTEN");
        ds.Tables["info"].Columns.Add("TEN");
        ds.Tables["info"].Columns.Add("NGAYSINH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NOISINH_MA");
        ds.Tables["info"].Columns.Add("GIOITINH", typeof(int?));
        ds.Tables["info"].Columns.Add("DANTOC_MA");
        ds.Tables["info"].Columns.Add("CMTND");
        ds.Tables["info"].Columns.Add("CMTND_NGAYCAP", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("CMTND_NOICAP");
        ds.Tables["info"].Columns.Add("KHOAPHONGCONGTAC");
        ds.Tables["info"].Columns.Add("DONVICONGTAC_MA");
        ds.Tables["info"].Columns.Add("SONAMKINHNGHIEM", typeof(int?));
        ds.Tables["info"].Columns.Add("DIACHISONHA");
        ds.Tables["info"].Columns.Add("DIACHIHANHCHINH_MA");
        ds.Tables["info"].Columns.Add("DIENTHOAI");
        ds.Tables["info"].Columns.Add("EMAIL");
        ds.Tables["info"].Columns.Add("NOIDUNGANH");
        ds.Tables["info"].Columns.Add("EXTANH");
        ds.Tables["info"].Columns.Add("TOTNGHIEP_MA");
        ds.Tables["info"].Columns.Add("NAMTOTNGHIEP", typeof(int?));
        ds.Tables["info"].Columns.Add("TRUONGCAPBANG");
        ds.Tables["info"].Columns.Add("CHUYENMON_MA");
        ds.Tables["info"].Columns.Add("CHUYENNGANH_MA");
        ds.Tables["info"].Columns.Add("USERNAME");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_HocVien.ID,
                ODT_HocVien.MA,
                ODT_HocVien.HOTEN,
                ODT_HocVien.TEN,
                ODT_HocVien.NGAYSINH,
                ODT_HocVien.NOISINH_MA,
                ODT_HocVien.GIOITINH,
                ODT_HocVien.DANTOC_MA,
                ODT_HocVien.CMTND,
                ODT_HocVien.CMTND_NGAYCAP,
                ODT_HocVien.CMTND_NOICAP,
                ODT_HocVien.KHOAPHONGCONGTAC,
                ODT_HocVien.DONVICONGTAC_MA,
                ODT_HocVien.SONAMKINHNGHIEM,
                ODT_HocVien.DIACHISONHA,
                ODT_HocVien.DIACHIHANHCHINH_MA,
                ODT_HocVien.DIENTHOAI,
                ODT_HocVien.EMAIL,
                ODT_HocVien.NOIDUNGANH,
                ODT_HocVien.EXTANH,
                ODT_HocVien.TOTNGHIEP_MA,
                ODT_HocVien.NAMTOTNGHIEP,
                ODT_HocVien.TRUONGCAPBANG,
                ODT_HocVien.CHUYENMON_MA,
                ODT_HocVien.CHUYENNGANH_MA,
                ODT_HocVien.USERNAME,
                ODT_HocVien.NGUOITAO_ID,
                ODT_HocVien.NGAYTAO,
                ODT_HocVien.NGUOISUA_ID,
                ODT_HocVien.NGAYSUA
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
