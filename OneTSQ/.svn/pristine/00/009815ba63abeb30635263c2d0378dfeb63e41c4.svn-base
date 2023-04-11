using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_KeHoachLopFilterCls : FilterCls
    {
    }
}

public class DT_KeHoachLopFilterParser
{
    public static DT_KeHoachLopFilterCls CreateInstance()
    {
        DT_KeHoachLopFilterCls ODT_KeHoachLopFilter = new DT_KeHoachLopFilterCls();
        return ODT_KeHoachLopFilter;
    }


    public static DT_KeHoachLopFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_KeHoachLopFilterCls ODT_KeHoachLopFilter = new DT_KeHoachLopFilterCls();
        return ODT_KeHoachLopFilter;
    }


    public static DT_KeHoachLopFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_KeHoachLopFilterCls ODT_KeHoachLopFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_KeHoachLopFilter;
    }



    public static XmlCls GetXml(DT_KeHoachLopFilterCls ODT_KeHoachLopFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
