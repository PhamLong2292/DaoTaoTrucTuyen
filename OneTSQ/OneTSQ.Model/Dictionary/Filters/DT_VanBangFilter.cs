using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_VanBangFilterCls : FilterCls
    {
        public string HOCVIEN_ID;
    }
}

public class DT_VanBangFilterParser
{
    public static DT_VanBangFilterCls CreateInstance()
    {
        DT_VanBangFilterCls ODT_VanBangFilter = new DT_VanBangFilterCls();
        return ODT_VanBangFilter;
    }


    public static DT_VanBangFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_VanBangFilterCls ODT_VanBangFilter = new DT_VanBangFilterCls();
        return ODT_VanBangFilter;
    }


    public static DT_VanBangFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_VanBangFilterCls ODT_VanBangFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_VanBangFilter;
    }



    public static XmlCls GetXml(DT_VanBangFilterCls ODT_VanBangFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
