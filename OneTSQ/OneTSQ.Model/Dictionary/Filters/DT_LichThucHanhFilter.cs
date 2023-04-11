using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichThucHanhFilterCls : FilterCls
    {
        public string KhoaHocId;
    }
}

public class DT_LichThucHanhFilterParser
{
    public static DT_LichThucHanhFilterCls CreateInstance()
    {
        DT_LichThucHanhFilterCls ODT_LichThucHanhFilter = new DT_LichThucHanhFilterCls();
        return ODT_LichThucHanhFilter;
    }


    public static DT_LichThucHanhFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_LichThucHanhFilterCls ODT_LichThucHanhFilter = new DT_LichThucHanhFilterCls();
        return ODT_LichThucHanhFilter;
    }


    public static DT_LichThucHanhFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichThucHanhFilterCls ODT_LichThucHanhFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichThucHanhFilter;
    }



    public static XmlCls GetXml(DT_LichThucHanhFilterCls ODT_LichThucHanhFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
