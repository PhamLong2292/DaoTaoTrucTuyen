using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class ButPheFilterCls:FilterCls
    {
        public string CABENHID;
        public int[] HanhDongs = new int[0];
        public DateTime? TuNgay;
        public DateTime? DenNgay;
    }
}

public class ButPheFilterParser 
{ 
    public static ButPheFilterCls CreateInstance() 
    { 
        ButPheFilterCls OButPheFilter = new ButPheFilterCls(); 
        return OButPheFilter; 
    } 


    public static ButPheFilterCls ParseFromDataRow(DataRow dr) 
    { 
        ButPheFilterCls OButPheFilter = new ButPheFilterCls(); 
        return OButPheFilter;
    }


    public static ButPheFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        ButPheFilterCls OButPheFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OButPheFilter;
    }



    public static XmlCls GetXml(ButPheFilterCls OButPheFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
