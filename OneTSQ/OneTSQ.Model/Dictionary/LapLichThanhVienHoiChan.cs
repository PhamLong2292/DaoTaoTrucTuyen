using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class LapLichThanhVienHoiChanCls
    {
        public string ID;
        public string LICHHOICHANID = "";
        public string BACSYID = "";
        public int? STT;
        public string DONVICONGTACMA = "";
        public string DONVICONGTAC = "";
    }
}

public class LapLichThanhVienHoiChanParser
{
    public static LapLichThanhVienHoiChanCls CreateInstance()
    {
        LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan = new LapLichThanhVienHoiChanCls();
        return OLapLichThanhVienHoiChan;
    }


    public static LapLichThanhVienHoiChanCls ParseFromDataRow(DataRow dr)
    {
        LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan = new LapLichThanhVienHoiChanCls();
        OLapLichThanhVienHoiChan.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OLapLichThanhVienHoiChan.LICHHOICHANID = CoreXmlUtility.GetString(dr, "LICHHOICHANID", true);
        OLapLichThanhVienHoiChan.BACSYID = CoreXmlUtility.GetString(dr, "BACSYID", true);
        OLapLichThanhVienHoiChan.DONVICONGTACMA = CoreXmlUtility.GetString(dr, "DONVICONGTACMA", true);
        OLapLichThanhVienHoiChan.DONVICONGTAC = CoreXmlUtility.GetString(dr, "DONVICONGTAC", true);
        return OLapLichThanhVienHoiChan;
    }

    public static LapLichThanhVienHoiChanCls[] ParseFromDataTable(DataTable dtTable)
    {
        LapLichThanhVienHoiChanCls[] LapLichThanhVienHoiChans = new LapLichThanhVienHoiChanCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            LapLichThanhVienHoiChans[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return LapLichThanhVienHoiChans;
    }


    public static LapLichThanhVienHoiChanCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        LapLichThanhVienHoiChanCls[] LapLichThanhVienHoiChans = ParseFromDataTable(ds.Tables[0]);
        return LapLichThanhVienHoiChans;
    }


    public static LapLichThanhVienHoiChanCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OLapLichThanhVienHoiChan;
    }


    public static XmlCls GetXml(LapLichThanhVienHoiChanCls[] LapLichThanhVienHoiChans)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("BACSYID");
        ds.Tables["info"].Columns.Add("DONVICONGTACMA");
        ds.Tables["info"].Columns.Add("DONVICONGTAC");
        for (int iIndex = 0; iIndex < LapLichThanhVienHoiChans.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                LapLichThanhVienHoiChans[iIndex].ID,
                LapLichThanhVienHoiChans[iIndex].LICHHOICHANID,
                LapLichThanhVienHoiChans[iIndex].BACSYID,
                LapLichThanhVienHoiChans[iIndex].DONVICONGTACMA,
                LapLichThanhVienHoiChans[iIndex].DONVICONGTAC
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("BACSYID");
        ds.Tables["info"].Columns.Add("DONVICONGTACMA");
        ds.Tables["info"].Columns.Add("DONVICONGTAC");
        ds.Tables["info"].Rows.Add(new object[]
            {
                OLapLichThanhVienHoiChan.ID,
                OLapLichThanhVienHoiChan.LICHHOICHANID,
                OLapLichThanhVienHoiChan.BACSYID,
                OLapLichThanhVienHoiChan.DONVICONGTACMA,
                OLapLichThanhVienHoiChan.DONVICONGTAC
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
