using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;

namespace OneTSQ.Model
{
    public class TaiLieuDinhKemCls
    {
        public string ID;
        public string DOCUMENT_ID;
        public string TENTAILIEU;
        public string TENHIENTHI;
        public string DUONGDAN;
        public string NGUOITAO_ID;
        public DateTime? NGAYTAO;
        public string GHICHU;    
  }
}

public class TaiLieuDinhKemParser
{
   
    public static TaiLieuDinhKemCls CreateInstance()
    {
        TaiLieuDinhKemCls OTaiLieuDinhKem = new TaiLieuDinhKemCls();
        return OTaiLieuDinhKem;
    }
    public static TaiLieuDinhKemCls ParseFromDataRow(DataRow dr)
    {
        TaiLieuDinhKemCls OTaiLieuDinhKem = new TaiLieuDinhKemCls();
        OTaiLieuDinhKem.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OTaiLieuDinhKem.DOCUMENT_ID = CoreXmlUtility.GetString(dr, "DOCUMENT_ID", true);
        OTaiLieuDinhKem.TENTAILIEU = CoreXmlUtility.GetString(dr, "TENTAILIEU", true);
        OTaiLieuDinhKem.TENHIENTHI = CoreXmlUtility.GetString(dr, "TENHIENTHI", true);
        OTaiLieuDinhKem.DUONGDAN = CoreXmlUtility.GetString(dr, "DUONGDAN", true);
        OTaiLieuDinhKem.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        OTaiLieuDinhKem.NGAYTAO = CoreXmlUtility.GetDateOrNull(dr, "NGAYTAO", true);
        OTaiLieuDinhKem.GHICHU = CoreXmlUtility.GetString(dr, "GHICHU", true);       
        return OTaiLieuDinhKem;
    }

    public static TaiLieuDinhKemCls[] ParseFromDataTable(DataTable dtTable)
    {
        TaiLieuDinhKemCls[] TaiLieuDinhKems = new TaiLieuDinhKemCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            TaiLieuDinhKems[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return TaiLieuDinhKems;
    }


    public static TaiLieuDinhKemCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        TaiLieuDinhKemCls[] TaiLieuDinhKems = ParseFromDataTable(ds.Tables[0]);
        return TaiLieuDinhKems;
    }


    public static TaiLieuDinhKemCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        TaiLieuDinhKemCls OTaiLieuDinhKem = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OTaiLieuDinhKem;
    }


    public static XmlCls GetXml(TaiLieuDinhKemCls[] TaiLieuDinhKems)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("DOCUMENT_ID");
        ds.Tables["info"].Columns.Add("TENTAILIEU");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("GHICHU");        
        for (int iIndex = 0; iIndex < TaiLieuDinhKems.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                TaiLieuDinhKems[iIndex].ID,
                TaiLieuDinhKems[iIndex].DOCUMENT_ID,
                TaiLieuDinhKems[iIndex].TENTAILIEU,
                TaiLieuDinhKems[iIndex].TENHIENTHI,
                TaiLieuDinhKems[iIndex].DUONGDAN,
                TaiLieuDinhKems[iIndex].NGUOITAO_ID,
                TaiLieuDinhKems[iIndex].NGAYTAO,
                TaiLieuDinhKems[iIndex].GHICHU,             
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(TaiLieuDinhKemCls OTaiLieuDinhKem)
    {
        DataSet ds = new DataSet();
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("DOCUMENT_ID");
        ds.Tables["info"].Columns.Add("TENTAILIEU");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Rows.Add(new object[]
        {
                OTaiLieuDinhKem.ID,
                OTaiLieuDinhKem.DOCUMENT_ID,
                OTaiLieuDinhKem.TENTAILIEU,
                OTaiLieuDinhKem.TENHIENTHI,
                OTaiLieuDinhKem.DUONGDAN,
                OTaiLieuDinhKem.NGUOITAO_ID,
                OTaiLieuDinhKem.NGAYTAO,
                OTaiLieuDinhKem.GHICHU,
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
