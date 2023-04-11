using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class TaiLieuCls 
    { 
        public string ID; 
        public string CABENHID = ""; 
        public string TEN = ""; 
    }
}

public class TaiLieuParser 
{ 
    public static TaiLieuCls CreateInstance() 
    { 
        TaiLieuCls OTaiLieu = new TaiLieuCls(); 
        return OTaiLieu; 
    } 


    public static TaiLieuCls ParseFromDataRow(DataRow dr) 
    { 
        TaiLieuCls OTaiLieu = new TaiLieuCls(); 
        OTaiLieu.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OTaiLieu.CABENHID = CoreXmlUtility.GetString(dr, "CABENHID", true);
        OTaiLieu.TEN = CoreXmlUtility.GetString(dr, "TEN", true);
        return OTaiLieu;
    }

    public static TaiLieuCls[] ParseFromDataTable(DataTable dtTable) 
    {
        TaiLieuCls[] TaiLieus = new TaiLieuCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            TaiLieus[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return TaiLieus;
    }


    public static TaiLieuCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        TaiLieuCls[] TaiLieus = ParseFromDataTable(ds.Tables[0]);
        return TaiLieus;
    }


    public static TaiLieuCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        TaiLieuCls OTaiLieu = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OTaiLieu;
    }


    public static XmlCls GetXml(TaiLieuCls[] TaiLieus)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("TEN");
        for (int iIndex = 0; iIndex < TaiLieus.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                TaiLieus[iIndex].ID,
                TaiLieus[iIndex].CABENHID,
                TaiLieus[iIndex].TEN
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(TaiLieuCls OTaiLieu)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("TEN");
            ds.Tables["info"].Rows.Add(new object[]
            {
                OTaiLieu.ID,
                OTaiLieu.CABENHID,
                OTaiLieu.TEN
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
