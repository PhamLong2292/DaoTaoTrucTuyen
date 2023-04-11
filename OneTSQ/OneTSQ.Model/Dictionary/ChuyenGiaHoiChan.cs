using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class ChuyenGiaHoiChanCls 
    { 
        public string ID; 
        public string BIENBANHOICHANID = "";
        public string BACSYID = "";
        public string DONVICONGTAC = ""; 
    }
}

public class ChuyenGiaHoiChanParser 
{ 
    public static ChuyenGiaHoiChanCls CreateInstance() 
    { 
        ChuyenGiaHoiChanCls OChuyenGiaHoiChan = new ChuyenGiaHoiChanCls(); 
        return OChuyenGiaHoiChan; 
    } 


    public static ChuyenGiaHoiChanCls ParseFromDataRow(DataRow dr) 
    { 
        ChuyenGiaHoiChanCls OChuyenGiaHoiChan = new ChuyenGiaHoiChanCls(); 
        OChuyenGiaHoiChan.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OChuyenGiaHoiChan.BIENBANHOICHANID = CoreXmlUtility.GetString(dr, "BIENBANHOICHANID", true);
        OChuyenGiaHoiChan.BACSYID = CoreXmlUtility.GetString(dr, "BACSYID", true);
        OChuyenGiaHoiChan.DONVICONGTAC = CoreXmlUtility.GetString(dr, "DONVICONGTAC", true);
        return OChuyenGiaHoiChan;
    }

    public static ChuyenGiaHoiChanCls[] ParseFromDataTable(DataTable dtTable) 
    {
        ChuyenGiaHoiChanCls[] ChuyenGiaHoiChans = new ChuyenGiaHoiChanCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            ChuyenGiaHoiChans[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return ChuyenGiaHoiChans;
    }


    public static ChuyenGiaHoiChanCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        ChuyenGiaHoiChanCls[] ChuyenGiaHoiChans = ParseFromDataTable(ds.Tables[0]);
        return ChuyenGiaHoiChans;
    }


    public static ChuyenGiaHoiChanCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        ChuyenGiaHoiChanCls OChuyenGiaHoiChan = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OChuyenGiaHoiChan;
    }


    public static XmlCls GetXml(ChuyenGiaHoiChanCls[] ChuyenGiaHoiChans)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BIENBANHOICHANID");
        ds.Tables["info"].Columns.Add("BACSYID");
        ds.Tables["info"].Columns.Add("DONVICONGTAC");
        for (int iIndex = 0; iIndex < ChuyenGiaHoiChans.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                ChuyenGiaHoiChans[iIndex].ID,
                ChuyenGiaHoiChans[iIndex].BIENBANHOICHANID,
                ChuyenGiaHoiChans[iIndex].BACSYID,
                ChuyenGiaHoiChans[iIndex].DONVICONGTAC
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(ChuyenGiaHoiChanCls OChuyenGiaHoiChan)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BIENBANHOICHANID");
        ds.Tables["info"].Columns.Add("BACSYID");
        ds.Tables["info"].Columns.Add("DONVICONGTAC");
            ds.Tables["info"].Rows.Add(new object[]
            {
                OChuyenGiaHoiChan.ID,
                OChuyenGiaHoiChan.BIENBANHOICHANID,
                OChuyenGiaHoiChan.BACSYID,
                OChuyenGiaHoiChan.DONVICONGTAC
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
