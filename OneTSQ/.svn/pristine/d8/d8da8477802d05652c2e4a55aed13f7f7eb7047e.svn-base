using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_DiemDanhThucHanhCls
    {
        public string ID;
        public string LICHTHUCHANHCHITIET_ID = "";
        public string HOCVIEN_ID = "";
    }
}

public class DT_DiemDanhThucHanhParser
{
    public static DT_DiemDanhThucHanhCls CreateInstance()
    {
        DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh = new DT_DiemDanhThucHanhCls();
        return ODT_DiemDanhThucHanh;
    }


    public static DT_DiemDanhThucHanhCls ParseFromDataRow(DataRow dr)
    {
        DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh = new DT_DiemDanhThucHanhCls();
        ODT_DiemDanhThucHanh.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_DiemDanhThucHanh.LICHTHUCHANHCHITIET_ID = CoreXmlUtility.GetString(dr, "LICHTHUCHANHCHITIET_ID", true);
        ODT_DiemDanhThucHanh.HOCVIEN_ID = CoreXmlUtility.GetString(dr, "HOCVIEN_ID", true);
        return ODT_DiemDanhThucHanh;
    }

    public static DT_DiemDanhThucHanhCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_DiemDanhThucHanhCls[] DT_DiemDanhThucHanhs = new DT_DiemDanhThucHanhCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_DiemDanhThucHanhs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_DiemDanhThucHanhs;
    }


    public static DT_DiemDanhThucHanhCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_DiemDanhThucHanhCls[] DT_DiemDanhThucHanhs = ParseFromDataTable(ds.Tables[0]);
        return DT_DiemDanhThucHanhs;
    }


    public static DT_DiemDanhThucHanhCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_DiemDanhThucHanh;
    }


    public static XmlCls GetXml(DT_DiemDanhThucHanhCls[] DT_DiemDanhThucHanhs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHTHUCHANHCHITIET_ID");
        ds.Tables["info"].Columns.Add("HOCVIEN_ID");
        for (int iIndex = 0; iIndex < DT_DiemDanhThucHanhs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_DiemDanhThucHanhs[iIndex].ID,
                DT_DiemDanhThucHanhs[iIndex].LICHTHUCHANHCHITIET_ID,
                DT_DiemDanhThucHanhs[iIndex].HOCVIEN_ID
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHTHUCHANHCHITIET_ID");
        ds.Tables["info"].Columns.Add("HOCVIEN_ID");
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_DiemDanhThucHanh.ID,
                ODT_DiemDanhThucHanh.LICHTHUCHANHCHITIET_ID,
                ODT_DiemDanhThucHanh.HOCVIEN_ID
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
