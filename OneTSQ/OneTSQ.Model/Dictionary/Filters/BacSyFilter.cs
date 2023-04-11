using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class BacSyFilterCls : FilterCls
    {
        public string DONVIMA;
        public string KHACDONVIMA;
        public string CHUYENKHOAMA;
        public string LICHHOICHANID;
        public string KHACLICHHOICHANID;
    }
}

public class BacSyFilterParser
{
    public static BacSyFilterCls CreateInstance()
    {
        BacSyFilterCls OBacSyFilter = new BacSyFilterCls();
        return OBacSyFilter;
    }


    public static BacSyFilterCls ParseFromDataRow(DataRow dr)
    {
        BacSyFilterCls OBacSyFilter = new BacSyFilterCls();
        return OBacSyFilter;
    }


    public static BacSyFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        BacSyFilterCls OBacSyFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBacSyFilter;
    }



    public static XmlCls GetXml(BacSyFilterCls OBacSyFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
