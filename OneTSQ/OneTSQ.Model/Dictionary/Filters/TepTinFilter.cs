using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class TepTinFilterCls:FilterCls 
    {
        public string CABENHID;
    }
}

public class TepTinFilterParser 
{ 
    public static TepTinFilterCls CreateInstance() 
    { 
        TepTinFilterCls OTepTinFilter = new TepTinFilterCls(); 
        return OTepTinFilter; 
    } 


    public static TepTinFilterCls ParseFromDataRow(DataRow dr) 
    { 
        TepTinFilterCls OTepTinFilter = new TepTinFilterCls(); 
        return OTepTinFilter;
    }


    public static TepTinFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        TepTinFilterCls OTepTinFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OTepTinFilter;
    }



    public static XmlCls GetXml(TepTinFilterCls OTepTinFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
