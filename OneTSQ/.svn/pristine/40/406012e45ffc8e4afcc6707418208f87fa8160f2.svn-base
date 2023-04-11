using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_DiemDanhThucHanhFilterCls : FilterCls
    {
        public string LichThucHanhId;
        public string HocVienId;
        public string LichThucHanhChiTietId;
    }
}

public class DT_DiemDanhThucHanhFilterParser
{
    public static DT_DiemDanhThucHanhFilterCls CreateInstance()
    {
        DT_DiemDanhThucHanhFilterCls ODT_DiemDanhThucHanhFilter = new DT_DiemDanhThucHanhFilterCls();
        return ODT_DiemDanhThucHanhFilter;
    }


    public static DT_DiemDanhThucHanhFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_DiemDanhThucHanhFilterCls ODT_DiemDanhThucHanhFilter = new DT_DiemDanhThucHanhFilterCls();
        return ODT_DiemDanhThucHanhFilter;
    }


    public static DT_DiemDanhThucHanhFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_DiemDanhThucHanhFilterCls ODT_DiemDanhThucHanhFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_DiemDanhThucHanhFilter;
    }



    public static XmlCls GetXml(DT_DiemDanhThucHanhFilterCls ODT_DiemDanhThucHanhFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
