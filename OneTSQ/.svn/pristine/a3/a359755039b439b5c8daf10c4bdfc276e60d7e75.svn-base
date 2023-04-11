using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_DiemDanhLyThuyetCls
    {
        public string ID;
        public string LICHLYTHUYETCHITIET_ID = "";
        public string HOCVIEN_ID = "";
    }
}

public class DT_DiemDanhLyThuyetParser
{
    public static DT_DiemDanhLyThuyetCls CreateInstance()
    {
        DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet = new DT_DiemDanhLyThuyetCls();
        return ODT_DiemDanhLyThuyet;
    }


    public static DT_DiemDanhLyThuyetCls ParseFromDataRow(DataRow dr)
    {
        DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet = new DT_DiemDanhLyThuyetCls();
        ODT_DiemDanhLyThuyet.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_DiemDanhLyThuyet.LICHLYTHUYETCHITIET_ID = CoreXmlUtility.GetString(dr, "LICHLYTHUYETCHITIET_ID", true);
        ODT_DiemDanhLyThuyet.HOCVIEN_ID = CoreXmlUtility.GetString(dr, "HOCVIEN_ID", true);
        return ODT_DiemDanhLyThuyet;
    }

    public static DT_DiemDanhLyThuyetCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_DiemDanhLyThuyetCls[] DT_DiemDanhLyThuyets = new DT_DiemDanhLyThuyetCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_DiemDanhLyThuyets[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_DiemDanhLyThuyets;
    }


    public static DT_DiemDanhLyThuyetCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_DiemDanhLyThuyetCls[] DT_DiemDanhLyThuyets = ParseFromDataTable(ds.Tables[0]);
        return DT_DiemDanhLyThuyets;
    }


    public static DT_DiemDanhLyThuyetCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_DiemDanhLyThuyet;
    }


    public static XmlCls GetXml(DT_DiemDanhLyThuyetCls[] DT_DiemDanhLyThuyets)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHLYTHUYETCHITIET_ID");
        ds.Tables["info"].Columns.Add("HOCVIEN_ID");
        for (int iIndex = 0; iIndex < DT_DiemDanhLyThuyets.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_DiemDanhLyThuyets[iIndex].ID,
                DT_DiemDanhLyThuyets[iIndex].LICHLYTHUYETCHITIET_ID,
                DT_DiemDanhLyThuyets[iIndex].HOCVIEN_ID
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHLYTHUYETCHITIET_ID");
        ds.Tables["info"].Columns.Add("HOCVIEN_ID");
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_DiemDanhLyThuyet.ID,
                ODT_DiemDanhLyThuyet.LICHLYTHUYETCHITIET_ID,
                ODT_DiemDanhLyThuyet.HOCVIEN_ID
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
