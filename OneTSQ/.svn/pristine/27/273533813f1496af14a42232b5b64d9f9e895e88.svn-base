using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichLyThuyetFilterCls : FilterCls
    {
    }
}

public class DT_LichLyThuyetFilterParser
{
    public static DT_LichLyThuyetFilterCls CreateInstance()
    {
        DT_LichLyThuyetFilterCls ODT_LichLyThuyetFilter = new DT_LichLyThuyetFilterCls();
        return ODT_LichLyThuyetFilter;
    }


    public static DT_LichLyThuyetFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_LichLyThuyetFilterCls ODT_LichLyThuyetFilter = new DT_LichLyThuyetFilterCls();
        return ODT_LichLyThuyetFilter;
    }


    public static DT_LichLyThuyetFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichLyThuyetFilterCls ODT_LichLyThuyetFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichLyThuyetFilter;
    }



    public static XmlCls GetXml(DT_LichLyThuyetFilterCls ODT_LichLyThuyetFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
