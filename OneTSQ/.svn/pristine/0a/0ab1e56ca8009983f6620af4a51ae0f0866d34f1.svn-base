using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichLyThuyetChiTietCls
    {
        public string ID;
        public string LICHLYTHUYET_ID = "";
        public DateTime NGAY;
        public DateTime? THOIGIAN;
        public DateTime? THOIGIANKETTHUC;
        public string NOIDUNG = "";
        public string GIANGVIEN_ID = "";
        public string GHICHU = "";
        public int HINHTHUCHOC;
        public enum eHinhThucHoc
        {
            TapTrung = 0,
            TrucTuyen = 1
        }
    }
}

public class DT_LichLyThuyetChiTietParser
{
    public readonly static Dictionary<int, string> HinhThucHocs = new Dictionary<int, string>
    {
        { (int)DT_LichLyThuyetChiTietCls.eHinhThucHoc.TapTrung, "Tập trung" },
        { (int)DT_LichLyThuyetChiTietCls.eHinhThucHoc.TrucTuyen, "Trực tuyến" }
    };
    public static DT_LichLyThuyetChiTietCls CreateInstance()
    {
        DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet = new DT_LichLyThuyetChiTietCls();
        return ODT_LichLyThuyetChiTiet;
    }


    public static DT_LichLyThuyetChiTietCls ParseFromDataRow(DataRow dr)
    {
        DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet = new DT_LichLyThuyetChiTietCls();
        ODT_LichLyThuyetChiTiet.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_LichLyThuyetChiTiet.LICHLYTHUYET_ID = CoreXmlUtility.GetString(dr, "LICHLYTHUYET_ID", true);
        ODT_LichLyThuyetChiTiet.NGAY = CoreXmlUtility.GetDate(dr, "NGAY", true);
        ODT_LichLyThuyetChiTiet.THOIGIAN = CoreXmlUtility.GetDateOrNull(dr, "THOIGIAN", true);
        ODT_LichLyThuyetChiTiet.THOIGIANKETTHUC = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANKETTHUC", true);
        ODT_LichLyThuyetChiTiet.NOIDUNG = CoreXmlUtility.GetString(dr, "NOIDUNG", true);
        ODT_LichLyThuyetChiTiet.GIANGVIEN_ID = CoreXmlUtility.GetString(dr, "GIANGVIEN_ID", true);
        ODT_LichLyThuyetChiTiet.GHICHU = CoreXmlUtility.GetString(dr, "GHICHU", true);
        ODT_LichLyThuyetChiTiet.HINHTHUCHOC = CoreXmlUtility.GetInt(dr, "HINHTHUCHOC", true);
        return ODT_LichLyThuyetChiTiet;
    }

    public static DT_LichLyThuyetChiTietCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_LichLyThuyetChiTietCls[] DT_LichLyThuyetChiTiets = new DT_LichLyThuyetChiTietCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_LichLyThuyetChiTiets[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_LichLyThuyetChiTiets;
    }


    public static DT_LichLyThuyetChiTietCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_LichLyThuyetChiTietCls[] DT_LichLyThuyetChiTiets = ParseFromDataTable(ds.Tables[0]);
        return DT_LichLyThuyetChiTiets;
    }


    public static DT_LichLyThuyetChiTietCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichLyThuyetChiTiet;
    }


    public static XmlCls GetXml(DT_LichLyThuyetChiTietCls[] DT_LichLyThuyetChiTiets)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHLYTHUYET_ID");
        ds.Tables["info"].Columns.Add("NGAY", typeof(DateTime));
        ds.Tables["info"].Columns.Add("THOIGIAN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("THOIGIANKETTHUC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NOIDUNG");
        ds.Tables["info"].Columns.Add("GIANGVIEN_ID");
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Columns.Add("HINHTHUCHOC", typeof(int));
        for (int iIndex = 0; iIndex < DT_LichLyThuyetChiTiets.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_LichLyThuyetChiTiets[iIndex].ID,
                DT_LichLyThuyetChiTiets[iIndex].LICHLYTHUYET_ID,
                DT_LichLyThuyetChiTiets[iIndex].NGAY,
                DT_LichLyThuyetChiTiets[iIndex].THOIGIAN,
                DT_LichLyThuyetChiTiets[iIndex].THOIGIANKETTHUC,
                DT_LichLyThuyetChiTiets[iIndex].NOIDUNG,
                DT_LichLyThuyetChiTiets[iIndex].GIANGVIEN_ID,
                DT_LichLyThuyetChiTiets[iIndex].GHICHU,
                DT_LichLyThuyetChiTiets[iIndex].HINHTHUCHOC
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_LichLyThuyetChiTietCls ODT_LichLyThuyetChiTiet)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHLYTHUYET_ID");
        ds.Tables["info"].Columns.Add("NGAY", typeof(DateTime));
        ds.Tables["info"].Columns.Add("THOIGIAN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("THOIGIANKETTHUC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NOIDUNG");
        ds.Tables["info"].Columns.Add("GIANGVIEN_ID");
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Columns.Add("HINHTHUCHOC", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
            {
                ODT_LichLyThuyetChiTiet.ID,
                ODT_LichLyThuyetChiTiet.LICHLYTHUYET_ID,
                ODT_LichLyThuyetChiTiet.NGAY,
                ODT_LichLyThuyetChiTiet.THOIGIAN,
                ODT_LichLyThuyetChiTiet.THOIGIANKETTHUC,
                ODT_LichLyThuyetChiTiet.NOIDUNG,
                ODT_LichLyThuyetChiTiet.GIANGVIEN_ID,
                ODT_LichLyThuyetChiTiet.GHICHU,
                ODT_LichLyThuyetChiTiet.HINHTHUCHOC
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
