using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class LapLichTepDinhKemFilterCls : FilterCls
    {
        public string LICHHOICHANID;
    }
}

public class LapLichTepDinhKemFilterParser
{
    public static LapLichTepDinhKemFilterCls CreateInstance()
    {
        LapLichTepDinhKemFilterCls OLapLichTepDinhKemFilter = new LapLichTepDinhKemFilterCls();
        return OLapLichTepDinhKemFilter;
    }


    public static LapLichTepDinhKemFilterCls ParseFromDataRow(DataRow dr)
    {
        LapLichTepDinhKemFilterCls OLapLichTepDinhKemFilter = new LapLichTepDinhKemFilterCls();
        return OLapLichTepDinhKemFilter;
    }


    public static LapLichTepDinhKemFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        LapLichTepDinhKemFilterCls OLapLichTepDinhKemFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OLapLichTepDinhKemFilter;
    }



    public static XmlCls GetXml(LapLichTepDinhKemFilterCls OLapLichTepDinhKemFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
