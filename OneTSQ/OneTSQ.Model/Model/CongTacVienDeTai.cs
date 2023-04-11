using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;

namespace OneTSQ.Model
{
    public class CongTacVienDeTaiCls
    {
        public string ID;
        public string DANGKYDETAI_ID;
        public string NGUOIDUNG_ID;  
  }
}

public class CongTacVienDeTaiParser
{
   
    public static CongTacVienDeTaiCls CreateInstance()
    {
        CongTacVienDeTaiCls OCongTacVienDeTai = new CongTacVienDeTaiCls();
        return OCongTacVienDeTai;
    }
    public static CongTacVienDeTaiCls ParseFromDataRow(DataRow dr)
    {
        CongTacVienDeTaiCls OCongTacVienDeTai = new CongTacVienDeTaiCls();
        OCongTacVienDeTai.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OCongTacVienDeTai.DANGKYDETAI_ID = CoreXmlUtility.GetString(dr, "DANGKYDETAI_ID", true);
        OCongTacVienDeTai.NGUOIDUNG_ID = CoreXmlUtility.GetString(dr, "NGUOIDUNG_ID", true);           
        return OCongTacVienDeTai;
    }

    public static CongTacVienDeTaiCls[] ParseFromDataTable(DataTable dtTable)
    {
        CongTacVienDeTaiCls[] CongTacVienDeTais = new CongTacVienDeTaiCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            CongTacVienDeTais[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return CongTacVienDeTais;
    }


    public static CongTacVienDeTaiCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        CongTacVienDeTaiCls[] CongTacVienDeTais = ParseFromDataTable(ds.Tables[0]);
        return CongTacVienDeTais;
    }


    public static CongTacVienDeTaiCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        CongTacVienDeTaiCls OCongTacVienDeTai = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OCongTacVienDeTai;
    }


    public static XmlCls GetXml(CongTacVienDeTaiCls[] CongTacVienDeTais)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("DANGKYDETAI_ID");
        ds.Tables["info"].Columns.Add("NGUOIDUNG_ID");       
        for (int iIndex = 0; iIndex < CongTacVienDeTais.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                CongTacVienDeTais[iIndex].ID,
                CongTacVienDeTais[iIndex].DANGKYDETAI_ID,
                CongTacVienDeTais[iIndex].NGUOIDUNG_ID,             
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(CongTacVienDeTaiCls OCongTacVienDeTai)
    {
        DataSet ds = new DataSet();
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("DANGKYDETAI_ID");
        ds.Tables["info"].Columns.Add("NGUOIDUNG_ID");
        ds.Tables["info"].Rows.Add(new object[]
        {
                OCongTacVienDeTai.ID,
                OCongTacVienDeTai.DANGKYDETAI_ID,
                OCongTacVienDeTai.NGUOIDUNG_ID,
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
