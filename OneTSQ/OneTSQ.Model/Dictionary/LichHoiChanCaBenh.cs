using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class LichHoiChanCaBenhCls
    {
        public string LICHHOICHANID;
        public string CABENHID;
        public int? STT;
    }
}

public class LichHoiChanCaBenhParser
{
    public static LichHoiChanCaBenhCls CreateInstance()
    {
        LichHoiChanCaBenhCls OLichHoiChanCaBenh = new LichHoiChanCaBenhCls();
        return OLichHoiChanCaBenh;
    }


    public static LichHoiChanCaBenhCls ParseFromDataRow(DataRow dr)
    {
        LichHoiChanCaBenhCls OLichHoiChanCaBenh = new LichHoiChanCaBenhCls();
        OLichHoiChanCaBenh.LICHHOICHANID = CoreXmlUtility.GetString(dr, "LICHHOICHANID", true);
        OLichHoiChanCaBenh.CABENHID = CoreXmlUtility.GetString(dr, "CABENHID", true);
        OLichHoiChanCaBenh.STT = CoreXmlUtility.GetIntOrNull(dr, "STT", true);
        return OLichHoiChanCaBenh;
    }

    public static LichHoiChanCaBenhCls[] ParseFromDataTable(DataTable dtTable)
    {
        LichHoiChanCaBenhCls[] LichHoiChanCaBenhs = new LichHoiChanCaBenhCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            LichHoiChanCaBenhs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return LichHoiChanCaBenhs;
    }


    public static LichHoiChanCaBenhCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        LichHoiChanCaBenhCls[] LichHoiChanCaBenhs = ParseFromDataTable(ds.Tables[0]);
        return LichHoiChanCaBenhs;
    }


    public static LichHoiChanCaBenhCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        LichHoiChanCaBenhCls OLichHoiChanCaBenh = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OLichHoiChanCaBenh;
    }


    public static XmlCls GetXml(LichHoiChanCaBenhCls[] LichHoiChanCaBenhs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("STT", typeof(int?));
        for (int iIndex = 0; iIndex < LichHoiChanCaBenhs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                LichHoiChanCaBenhs[iIndex].LICHHOICHANID,
                LichHoiChanCaBenhs[iIndex].CABENHID,
                LichHoiChanCaBenhs[iIndex].STT
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(LichHoiChanCaBenhCls OLichHoiChanCaBenh)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("STT", typeof(int?));
        ds.Tables["info"].Rows.Add(new object[]
            {
                OLichHoiChanCaBenh.LICHHOICHANID,
                OLichHoiChanCaBenh.CABENHID,
                OLichHoiChanCaBenh.STT
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
