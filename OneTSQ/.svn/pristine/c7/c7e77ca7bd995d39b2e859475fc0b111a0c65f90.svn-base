using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class LapLichTepDinhKemCls
    {
        public string ID;
        public string LICHHOICHANID = "";
        public string TENTEP = "";
        public string DUONGDAN = "";
        public string TENHIENTHI = "";
    }
}

public class LapLichTepDinhKemParser
{
    public static LapLichTepDinhKemCls CreateInstance()
    {
        LapLichTepDinhKemCls OLapLichTepDinhKem = new LapLichTepDinhKemCls();
        return OLapLichTepDinhKem;
    }


    public static LapLichTepDinhKemCls ParseFromDataRow(DataRow dr)
    {
        LapLichTepDinhKemCls OLapLichTepDinhKem = new LapLichTepDinhKemCls();
        OLapLichTepDinhKem.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OLapLichTepDinhKem.LICHHOICHANID = CoreXmlUtility.GetString(dr, "LICHHOICHANID", true);
        OLapLichTepDinhKem.TENTEP = CoreXmlUtility.GetString(dr, "TENTEP", true);
        OLapLichTepDinhKem.DUONGDAN = CoreXmlUtility.GetString(dr, "DUONGDAN", true);
        OLapLichTepDinhKem.TENHIENTHI = CoreXmlUtility.GetString(dr, "TENHIENTHI", true);
        return OLapLichTepDinhKem;
    }

    public static LapLichTepDinhKemCls[] ParseFromDataTable(DataTable dtTable)
    {
        LapLichTepDinhKemCls[] LapLichTepDinhKems = new LapLichTepDinhKemCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            LapLichTepDinhKems[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return LapLichTepDinhKems;
    }


    public static LapLichTepDinhKemCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        LapLichTepDinhKemCls[] LapLichTepDinhKems = ParseFromDataTable(ds.Tables[0]);
        return LapLichTepDinhKems;
    }


    public static LapLichTepDinhKemCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        LapLichTepDinhKemCls OLapLichTepDinhKem = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OLapLichTepDinhKem;
    }


    public static XmlCls GetXml(LapLichTepDinhKemCls[] LapLichTepDinhKems)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        for (int iIndex = 0; iIndex < LapLichTepDinhKems.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                LapLichTepDinhKems[iIndex].ID,
                LapLichTepDinhKems[iIndex].LICHHOICHANID,
                LapLichTepDinhKems[iIndex].TENTEP,
                LapLichTepDinhKems[iIndex].DUONGDAN,
                LapLichTepDinhKems[iIndex].TENHIENTHI
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(LapLichTepDinhKemCls OLapLichTepDinhKem)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Rows.Add(new object[]
        {
                OLapLichTepDinhKem.ID,
                OLapLichTepDinhKem.LICHHOICHANID,
                OLapLichTepDinhKem.TENTEP,
                OLapLichTepDinhKem.DUONGDAN,
                OLapLichTepDinhKem.TENHIENTHI
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
