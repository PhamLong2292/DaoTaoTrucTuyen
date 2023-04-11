using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class TepDinhKemBlHinhAnhCls 
    { 
        public string ID; 
        public string BINHLUANHINHANHID = ""; 
        public string TENTEP = ""; 
        public string TENHIENTHI = ""; 
    }
}

public class TepDinhKemBlHinhAnhParser 
{ 
    public static TepDinhKemBlHinhAnhCls CreateInstance() 
    { 
        TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh = new TepDinhKemBlHinhAnhCls(); 
        return OTepDinhKemBlHinhAnh; 
    } 


    public static TepDinhKemBlHinhAnhCls ParseFromDataRow(DataRow dr) 
    { 
        TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh = new TepDinhKemBlHinhAnhCls(); 
        OTepDinhKemBlHinhAnh.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OTepDinhKemBlHinhAnh.BINHLUANHINHANHID = CoreXmlUtility.GetString(dr, "BINHLUANHINHANHID", true);
        OTepDinhKemBlHinhAnh.TENTEP = CoreXmlUtility.GetString(dr, "TENTEP", true);
        OTepDinhKemBlHinhAnh.TENHIENTHI = CoreXmlUtility.GetString(dr, "TENHIENTHI", true);
        return OTepDinhKemBlHinhAnh;
    }

    public static TepDinhKemBlHinhAnhCls[] ParseFromDataTable(DataTable dtTable) 
    {
        TepDinhKemBlHinhAnhCls[] TepDinhKemBlHinhAnhs = new TepDinhKemBlHinhAnhCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            TepDinhKemBlHinhAnhs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return TepDinhKemBlHinhAnhs;
    }


    public static TepDinhKemBlHinhAnhCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        TepDinhKemBlHinhAnhCls[] TepDinhKemBlHinhAnhs = ParseFromDataTable(ds.Tables[0]);
        return TepDinhKemBlHinhAnhs;
    }


    public static TepDinhKemBlHinhAnhCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OTepDinhKemBlHinhAnh;
    }


    public static XmlCls GetXml(TepDinhKemBlHinhAnhCls[] TepDinhKemBlHinhAnhs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BINHLUANHINHANHID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        for (int iIndex = 0; iIndex < TepDinhKemBlHinhAnhs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                TepDinhKemBlHinhAnhs[iIndex].ID,
                TepDinhKemBlHinhAnhs[iIndex].BINHLUANHINHANHID,
                TepDinhKemBlHinhAnhs[iIndex].TENTEP,
                TepDinhKemBlHinhAnhs[iIndex].TENHIENTHI
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BINHLUANHINHANHID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
            ds.Tables["info"].Rows.Add(new object[]
            {
                OTepDinhKemBlHinhAnh.ID,
                OTepDinhKemBlHinhAnh.BINHLUANHINHANHID,
                OTepDinhKemBlHinhAnh.TENTEP,
                OTepDinhKemBlHinhAnh.TENHIENTHI
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
