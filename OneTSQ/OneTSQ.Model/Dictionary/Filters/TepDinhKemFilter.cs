using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class TepDinhKemFilterCls:FilterCls 
    {
        public string YKIENCHUYENGIAID;
    }
}

public class TepDinhKemFilterParser 
{ 
    public static TepDinhKemFilterCls CreateInstance() 
    { 
        TepDinhKemFilterCls OTepDinhKemFilter = new TepDinhKemFilterCls(); 
        return OTepDinhKemFilter; 
    } 


    public static TepDinhKemFilterCls ParseFromDataRow(DataRow dr) 
    { 
        TepDinhKemFilterCls OTepDinhKemFilter = new TepDinhKemFilterCls(); 
        return OTepDinhKemFilter;
    }


    public static TepDinhKemFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        TepDinhKemFilterCls OTepDinhKemFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OTepDinhKemFilter;
    }



    public static XmlCls GetXml(TepDinhKemFilterCls OTepDinhKemFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
