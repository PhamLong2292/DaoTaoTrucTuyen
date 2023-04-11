using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class ButPheCls 
    { 
        public string ID; 
        public string CABENHID = ""; 
        public DateTime THOIGIAN;
        public string NGUOIPHE = ""; 
        public int HANHDONG;
        public string NOIDUNG = ""; 
    }
}

public class ButPheParser 
{ 
    public static ButPheCls CreateInstance() 
    { 
        ButPheCls OButPhe = new ButPheCls(); 
        return OButPhe; 
    } 


    public static ButPheCls ParseFromDataRow(DataRow dr) 
    { 
        ButPheCls OButPhe = new ButPheCls(); 
        OButPhe.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OButPhe.CABENHID = CoreXmlUtility.GetString(dr, "CABENHID", true);
        OButPhe.THOIGIAN = CoreXmlUtility.GetDate(dr, "THOIGIAN", true);
        OButPhe.NGUOIPHE = CoreXmlUtility.GetString(dr, "NGUOIPHE", true);
        OButPhe.HANHDONG = CoreXmlUtility.GetInt(dr, "HANHDONG", true);
        OButPhe.NOIDUNG = CoreXmlUtility.GetString(dr, "NOIDUNG", true);
        return OButPhe;
    }

    public static ButPheCls[] ParseFromDataTable(DataTable dtTable) 
    {
        ButPheCls[] ButPhes = new ButPheCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            ButPhes[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return ButPhes;
    }


    public static ButPheCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        ButPheCls[] ButPhes = ParseFromDataTable(ds.Tables[0]);
        return ButPhes;
    }


    public static ButPheCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        ButPheCls OButPhe = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OButPhe;
    }


    public static XmlCls GetXml(ButPheCls[] ButPhes)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("THOIGIAN",typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOIPHE");
        ds.Tables["info"].Columns.Add("HANHDONG",typeof(int));
        ds.Tables["info"].Columns.Add("NOIDUNG");
        for (int iIndex = 0; iIndex < ButPhes.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                ButPhes[iIndex].ID,
                ButPhes[iIndex].CABENHID,
                ButPhes[iIndex].THOIGIAN,
                ButPhes[iIndex].NGUOIPHE,
                ButPhes[iIndex].HANHDONG,
                ButPhes[iIndex].NOIDUNG
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(ButPheCls OButPhe)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("THOIGIAN",typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOIPHE");
        ds.Tables["info"].Columns.Add("HANHDONG",typeof(int));
        ds.Tables["info"].Columns.Add("NOIDUNG");
            ds.Tables["info"].Rows.Add(new object[]
            {
                OButPhe.ID,
                OButPhe.CABENHID,
                OButPhe.THOIGIAN,
                OButPhe.NGUOIPHE,
                OButPhe.HANHDONG,
                OButPhe.NOIDUNG
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
