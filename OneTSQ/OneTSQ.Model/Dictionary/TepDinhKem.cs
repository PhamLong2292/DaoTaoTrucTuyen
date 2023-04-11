using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class TepDinhKemCls 
    { 
        public string ID; 
        public string YKIENCHUYENGIAID = ""; 
        public string TENTEP = "";
        public string TENHIENTHI = "";
        public string DUONGDAN = ""; 
    }
}

public class TepDinhKemParser 
{ 
    public static TepDinhKemCls CreateInstance() 
    { 
        TepDinhKemCls OTepDinhKem = new TepDinhKemCls(); 
        return OTepDinhKem; 
    } 


    public static TepDinhKemCls ParseFromDataRow(DataRow dr) 
    { 
        TepDinhKemCls OTepDinhKem = new TepDinhKemCls(); 
        OTepDinhKem.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OTepDinhKem.YKIENCHUYENGIAID = CoreXmlUtility.GetString(dr, "YKIENCHUYENGIAID", true);
        OTepDinhKem.TENTEP = CoreXmlUtility.GetString(dr, "TENTEP", true);
        OTepDinhKem.TENHIENTHI = CoreXmlUtility.GetString(dr, "TENHIENTHI", true);
        //OTepDinhKem.DUONGDAN = CoreXmlUtility.GetString(dr, "DUONGDAN", true);
        try
        {
            string uploadedThamVanFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedTuVanFilePath"];
            System.Net.WebClient myWebClient = new System.Net.WebClient();
            OTepDinhKem.DUONGDAN = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + uploadedThamVanFilePath), OTepDinhKem.TENTEP);
            //OTepTin.NDTEP = System.IO.File.ReadAllBytes(OTepTin.DUONGDAN);
        }
        catch { }
        return OTepDinhKem;
    }

    public static TepDinhKemCls[] ParseFromDataTable(DataTable dtTable) 
    {
        TepDinhKemCls[] TepDinhKems = new TepDinhKemCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            TepDinhKems[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return TepDinhKems;
    }


    public static TepDinhKemCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        TepDinhKemCls[] TepDinhKems = ParseFromDataTable(ds.Tables[0]);
        return TepDinhKems;
    }


    public static TepDinhKemCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        TepDinhKemCls OTepDinhKem = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OTepDinhKem;
    }


    public static XmlCls GetXml(TepDinhKemCls[] TepDinhKems)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("YKIENCHUYENGIAID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        for (int iIndex = 0; iIndex < TepDinhKems.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                TepDinhKems[iIndex].ID,
                TepDinhKems[iIndex].YKIENCHUYENGIAID,
                TepDinhKems[iIndex].TENTEP,
                TepDinhKems[iIndex].TENHIENTHI,
                TepDinhKems[iIndex].DUONGDAN
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(TepDinhKemCls OTepDinhKem)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("YKIENCHUYENGIAID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("DUONGDAN");
            ds.Tables["info"].Rows.Add(new object[]
            {
                OTepDinhKem.ID,
                OTepDinhKem.YKIENCHUYENGIAID,
                OTepDinhKem.TENTEP,
                OTepDinhKem.TENHIENTHI,
                OTepDinhKem.DUONGDAN
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
