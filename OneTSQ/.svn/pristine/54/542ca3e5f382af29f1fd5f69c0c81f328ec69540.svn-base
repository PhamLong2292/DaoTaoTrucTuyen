using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichThucHanhHocVienCls
    {
        public string LICHTHUCHANH_ID;
        public string HOCVIEN_ID;
    }
}

public class DT_LichThucHanhHocVienParser
{
    public static DT_LichThucHanhHocVienCls CreateInstance()
    {
        DT_LichThucHanhHocVienCls ODT_LichThucHanhHocVien = new DT_LichThucHanhHocVienCls();
        return ODT_LichThucHanhHocVien;
    }


    public static DT_LichThucHanhHocVienCls ParseFromDataRow(DataRow dr)
    {
        DT_LichThucHanhHocVienCls ODT_LichThucHanhHocVien = new DT_LichThucHanhHocVienCls();
        ODT_LichThucHanhHocVien.LICHTHUCHANH_ID = CoreXmlUtility.GetString(dr, "LICHTHUCHANH_ID", true);
        ODT_LichThucHanhHocVien.HOCVIEN_ID = CoreXmlUtility.GetString(dr, "HOCVIEN_ID", true);
        return ODT_LichThucHanhHocVien;
    }

    public static DT_LichThucHanhHocVienCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_LichThucHanhHocVienCls[] DT_LichThucHanhHocViens = new DT_LichThucHanhHocVienCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_LichThucHanhHocViens[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_LichThucHanhHocViens;
    }


    public static DT_LichThucHanhHocVienCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_LichThucHanhHocVienCls[] DT_LichThucHanhHocViens = ParseFromDataTable(ds.Tables[0]);
        return DT_LichThucHanhHocViens;
    }


    public static DT_LichThucHanhHocVienCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichThucHanhHocVienCls ODT_LichThucHanhHocVien = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichThucHanhHocVien;
    }


    public static XmlCls GetXml(DT_LichThucHanhHocVienCls[] DT_LichThucHanhHocViens)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("LICHTHUCHANH_ID");
        ds.Tables["info"].Columns.Add("HOCVIEN_ID");
        for (int iIndex = 0; iIndex < DT_LichThucHanhHocViens.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_LichThucHanhHocViens[iIndex].LICHTHUCHANH_ID,
                DT_LichThucHanhHocViens[iIndex].HOCVIEN_ID
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_LichThucHanhHocVienCls ODT_LichThucHanhHocVien)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("LICHTHUCHANH_ID");
        ds.Tables["info"].Columns.Add("HOCVIEN_ID");
        ds.Tables["info"].Rows.Add(new object[]
            {
                ODT_LichThucHanhHocVien.LICHTHUCHANH_ID,
                ODT_LichThucHanhHocVien.HOCVIEN_ID
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
