using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;

namespace OneTSQ.Model
{
    public class DaoTaoNhanLucCls
    {
        public string ID;
        public string PHIEUKHAOSATBVVT_ID;
        public int? SOLUONG;
        public string DM_TENKHOAHOC_ID;
       
    } 
}

public class DaoTaoNhanLucParser
{   
    public static DaoTaoNhanLucCls ParseFromDataRow(DataRow dr)
    {
        DaoTaoNhanLucCls ODaoTaoNhanLuc = new DaoTaoNhanLucCls();
        ODaoTaoNhanLuc.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODaoTaoNhanLuc.PHIEUKHAOSATBVVT_ID = CoreXmlUtility.GetString(dr, "PHIEUKHAOSATBVVT_ID", true);
        ODaoTaoNhanLuc.SOLUONG = CoreXmlUtility.GetIntOrNull(dr, "SOLUONG", true);
        ODaoTaoNhanLuc.DM_TENKHOAHOC_ID = CoreXmlUtility.GetString(dr, "DM_TENKHOAHOC_ID", true);
        return ODaoTaoNhanLuc;
    }
  
    public static DaoTaoNhanLucCls[] ParseFromDataTable(DataTable dtTable)
    {
        DaoTaoNhanLucCls[] DaoTaoNhanLucs = new DaoTaoNhanLucCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DaoTaoNhanLucs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DaoTaoNhanLucs;
    }


    public static DaoTaoNhanLucCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DaoTaoNhanLucCls[] DaoTaoNhanLucs = ParseFromDataTable(ds.Tables[0]);
        return DaoTaoNhanLucs;
    }


    public static DaoTaoNhanLucCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DaoTaoNhanLucCls ODaoTaoNhanLuc = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODaoTaoNhanLuc;
    }


    public static XmlCls GetXml(DaoTaoNhanLucCls[] DaoTaoNhanLucs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("PHIEUKHAOSATBVVT_ID");
        ds.Tables["info"].Columns.Add("SOLUONG", typeof(int?));
        ds.Tables["info"].Columns.Add("DM_TENKHOAHOC_ID");
        for (int iIndex = 0; iIndex < DaoTaoNhanLucs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DaoTaoNhanLucs[iIndex].ID,
                DaoTaoNhanLucs[iIndex].PHIEUKHAOSATBVVT_ID,
                DaoTaoNhanLucs[iIndex].SOLUONG,
                DaoTaoNhanLucs[iIndex].DM_TENKHOAHOC_ID,          
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DaoTaoNhanLucCls ODaoTaoNhanLuc)
    {
        DataSet ds = new DataSet();
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("PHIEUKHAOSATBVVT_ID");
        ds.Tables["info"].Columns.Add("SOLUONG", typeof(int?));
        ds.Tables["info"].Columns.Add("DM_TENKHOAHOC_ID");
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODaoTaoNhanLuc.ID,
                ODaoTaoNhanLuc.PHIEUKHAOSATBVVT_ID,
                ODaoTaoNhanLuc.SOLUONG,
                ODaoTaoNhanLuc.DM_TENKHOAHOC_ID,
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
