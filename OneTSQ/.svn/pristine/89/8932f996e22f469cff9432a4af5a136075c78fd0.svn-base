using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class TepTinCls 
    {
        public string ID;
        public string CABENHID; 
        public string TENHIENTHI = ""; 
        public string TAILIEUID = ""; 
        public string TENTEP = ""; 
        public string GHICHU = "";
        public string DUONGDAN = "";
        public byte[] NDTEP; 
    }
}

public class TepTinParser 
{ 
    public static TepTinCls CreateInstance() 
    { 
        TepTinCls OTepTin = new TepTinCls(); 
        return OTepTin; 
    } 


    public static TepTinCls ParseFromDataRow(DataRow dr) 
    { 
        TepTinCls OTepTin = new TepTinCls();
        OTepTin.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OTepTin.CABENHID = CoreXmlUtility.GetString(dr, "CABENHID", true);
        OTepTin.TENHIENTHI = CoreXmlUtility.GetString(dr, "TENHIENTHI", true);
        OTepTin.TAILIEUID = CoreXmlUtility.GetString(dr, "TAILIEUID", true);
        OTepTin.TENTEP = CoreXmlUtility.GetString(dr, "TENTEP", true);
        OTepTin.GHICHU = CoreXmlUtility.GetString(dr, "GHICHU", true);
        //OTepTin.DUONGDAN = CoreXmlUtility.GetString(dr, "DUONGDAN", true);
        try
        {
            string uploadedThamVanFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedThamVanFilePath"];
            OTepTin.DUONGDAN = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + uploadedThamVanFilePath), OTepTin.TENTEP);
        }
        catch { }
        return OTepTin;
    }

    public static TepTinCls[] ParseFromDataTable(DataTable dtTable) 
    {
        TepTinCls[] TepTins = new TepTinCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            TepTins[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return TepTins;
    }


    public static TepTinCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        TepTinCls[] TepTins = ParseFromDataTable(ds.Tables[0]);
        return TepTins;
    }


    public static TepTinCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        TepTinCls OTepTin = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OTepTin;
    }


    public static XmlCls GetXml(TepTinCls[] TepTins)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("TAILIEUID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        for (int iIndex = 0; iIndex < TepTins.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                TepTins[iIndex].ID,
                TepTins[iIndex].CABENHID,
                TepTins[iIndex].TENHIENTHI,
                TepTins[iIndex].TAILIEUID,
                TepTins[iIndex].TENTEP,
                TepTins[iIndex].GHICHU,
                TepTins[iIndex].DUONGDAN
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(TepTinCls OTepTin)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("TAILIEUID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Columns.Add("DUONGDAN");
            ds.Tables["info"].Rows.Add(new object[]
            {
                OTepTin.ID,
                OTepTin.CABENHID,
                OTepTin.TENHIENTHI,
                OTepTin.TAILIEUID,
                OTepTin.TENTEP,
                OTepTin.GHICHU,
                OTepTin.DUONGDAN
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
