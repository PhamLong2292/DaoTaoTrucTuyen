using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class HinhAnhFilterCls : FilterCls
    {
        public string CABENHID;
        public int? TYPE;
    }
}

public class HinhAnhFilterParser
{
    public static HinhAnhFilterCls CreateInstance()
    {
        HinhAnhFilterCls OHinhAnhFilter = new HinhAnhFilterCls();
        return OHinhAnhFilter;
    }


    public static HinhAnhFilterCls ParseFromDataRow(DataRow dr)
    {
        HinhAnhFilterCls OHinhAnhFilter = new HinhAnhFilterCls();
        return OHinhAnhFilter;
    }


    public static HinhAnhFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        HinhAnhFilterCls OHinhAnhFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OHinhAnhFilter;
    }



    public static XmlCls GetXml(HinhAnhFilterCls OHinhAnhFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
