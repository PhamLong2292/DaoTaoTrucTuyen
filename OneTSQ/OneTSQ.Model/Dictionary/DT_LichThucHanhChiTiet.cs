using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichThucHanhChiTietCls
    {
        public string ID;
        public string LICHTHUCHANH_ID = "";
        public DateTime NGAY;
        public DateTime? THOIGIAN;
        public DateTime? THOIGIANKETTHUC;
        public string NOIDUNG = "";
        public string GIANGVIEN_ID = "";
        public string GHICHU = "";
    }
}

public class DT_LichThucHanhChiTietParser
{
    public static DT_LichThucHanhChiTietCls CreateInstance()
    {
        DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet = new DT_LichThucHanhChiTietCls();
        return ODT_LichThucHanhChiTiet;
    }


    public static DT_LichThucHanhChiTietCls ParseFromDataRow(DataRow dr)
    {
        DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet = new DT_LichThucHanhChiTietCls();
        ODT_LichThucHanhChiTiet.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_LichThucHanhChiTiet.LICHTHUCHANH_ID = CoreXmlUtility.GetString(dr, "LICHTHUCHANH_ID", true);
        ODT_LichThucHanhChiTiet.NGAY = CoreXmlUtility.GetDate(dr, "NGAY", true);
        ODT_LichThucHanhChiTiet.THOIGIAN = CoreXmlUtility.GetDateOrNull(dr, "THOIGIAN", true);
        ODT_LichThucHanhChiTiet.THOIGIANKETTHUC = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANKETTHUC", true);
        ODT_LichThucHanhChiTiet.NOIDUNG = CoreXmlUtility.GetString(dr, "NOIDUNG", true);
        ODT_LichThucHanhChiTiet.GIANGVIEN_ID = CoreXmlUtility.GetString(dr, "GIANGVIEN_ID", true);
        ODT_LichThucHanhChiTiet.GHICHU = CoreXmlUtility.GetString(dr, "GHICHU", true);
        return ODT_LichThucHanhChiTiet;
    }

    public static DT_LichThucHanhChiTietCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_LichThucHanhChiTietCls[] DT_LichThucHanhChiTiets = new DT_LichThucHanhChiTietCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_LichThucHanhChiTiets[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_LichThucHanhChiTiets;
    }


    public static DT_LichThucHanhChiTietCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_LichThucHanhChiTietCls[] DT_LichThucHanhChiTiets = ParseFromDataTable(ds.Tables[0]);
        return DT_LichThucHanhChiTiets;
    }


    public static DT_LichThucHanhChiTietCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichThucHanhChiTiet;
    }


    public static XmlCls GetXml(DT_LichThucHanhChiTietCls[] DT_LichThucHanhChiTiets)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHTHUCHANH_ID");
        ds.Tables["info"].Columns.Add("NGAY", typeof(DateTime));
        ds.Tables["info"].Columns.Add("THOIGIAN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("THOIGIANKETTHUC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NOIDUNG");
        ds.Tables["info"].Columns.Add("GIANGVIEN_ID");
        ds.Tables["info"].Columns.Add("GHICHU");
        for (int iIndex = 0; iIndex < DT_LichThucHanhChiTiets.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_LichThucHanhChiTiets[iIndex].ID,
                DT_LichThucHanhChiTiets[iIndex].LICHTHUCHANH_ID,
                DT_LichThucHanhChiTiets[iIndex].NGAY,
                DT_LichThucHanhChiTiets[iIndex].THOIGIAN,
                DT_LichThucHanhChiTiets[iIndex].THOIGIANKETTHUC,
                DT_LichThucHanhChiTiets[iIndex].NOIDUNG,
                DT_LichThucHanhChiTiets[iIndex].GIANGVIEN_ID,
                DT_LichThucHanhChiTiets[iIndex].GHICHU
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_LichThucHanhChiTietCls ODT_LichThucHanhChiTiet)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHTHUCHANH_ID");
        ds.Tables["info"].Columns.Add("NGAY", typeof(DateTime));
        ds.Tables["info"].Columns.Add("THOIGIAN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("THOIGIANKETTHUC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NOIDUNG");
        ds.Tables["info"].Columns.Add("GIANGVIEN_ID");
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_LichThucHanhChiTiet.ID,
                ODT_LichThucHanhChiTiet.LICHTHUCHANH_ID,
                ODT_LichThucHanhChiTiet.NGAY,
                ODT_LichThucHanhChiTiet.THOIGIAN,
                ODT_LichThucHanhChiTiet.THOIGIANKETTHUC,
                ODT_LichThucHanhChiTiet.NOIDUNG,
                ODT_LichThucHanhChiTiet.GIANGVIEN_ID,
                ODT_LichThucHanhChiTiet.GHICHU
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
