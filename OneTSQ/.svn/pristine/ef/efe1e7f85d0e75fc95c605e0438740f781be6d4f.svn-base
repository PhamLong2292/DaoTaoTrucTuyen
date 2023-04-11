using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class TepDinhKemBlHinhAnhFilterCls:FilterCls 
    {
        public string BINHLUANHINHANHID;
    }
}

public class TepDinhKemBlHinhAnhFilterParser 
{ 
    public static TepDinhKemBlHinhAnhFilterCls CreateInstance() 
    { 
        TepDinhKemBlHinhAnhFilterCls OTepDinhKemBlHinhAnhFilter = new TepDinhKemBlHinhAnhFilterCls(); 
        return OTepDinhKemBlHinhAnhFilter; 
    } 


    public static TepDinhKemBlHinhAnhFilterCls ParseFromDataRow(DataRow dr) 
    { 
        TepDinhKemBlHinhAnhFilterCls OTepDinhKemBlHinhAnhFilter = new TepDinhKemBlHinhAnhFilterCls(); 
        return OTepDinhKemBlHinhAnhFilter;
    }


    public static TepDinhKemBlHinhAnhFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        TepDinhKemBlHinhAnhFilterCls OTepDinhKemBlHinhAnhFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OTepDinhKemBlHinhAnhFilter;
    }



    public static XmlCls GetXml(TepDinhKemBlHinhAnhFilterCls OTepDinhKemBlHinhAnhFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
