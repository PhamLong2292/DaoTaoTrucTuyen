using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_TaiLieuFilterCls : FilterCls
    {
        public string KHOAHOC_ID;
    }
}

public class DT_TaiLieuFilterParser
{
    public static DT_TaiLieuFilterCls CreateInstance()
    {
        DT_TaiLieuFilterCls ODT_TaiLieuFilter = new DT_TaiLieuFilterCls();
        return ODT_TaiLieuFilter;
    }


    public static DT_TaiLieuFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_TaiLieuFilterCls ODT_TaiLieuFilter = new DT_TaiLieuFilterCls();
        return ODT_TaiLieuFilter;
    }


    public static DT_TaiLieuFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_TaiLieuFilterCls ODT_TaiLieuFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_TaiLieuFilter;
    }



    public static XmlCls GetXml(DT_TaiLieuFilterCls ODT_TaiLieuFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
