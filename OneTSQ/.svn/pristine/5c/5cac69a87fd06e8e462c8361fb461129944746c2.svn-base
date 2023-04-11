using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichChuyenGiaoChiTietFilterCls : FilterCls
    {
        public string LICHCHUYENGIAO_ID;
    }
}

public class DT_LichChuyenGiaoChiTietFilterParser
{
    public static DT_LichChuyenGiaoChiTietFilterCls CreateInstance()
    {
        DT_LichChuyenGiaoChiTietFilterCls ODT_LichChuyenGiaoChiTietFilter = new DT_LichChuyenGiaoChiTietFilterCls();
        return ODT_LichChuyenGiaoChiTietFilter;
    }


    public static DT_LichChuyenGiaoChiTietFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_LichChuyenGiaoChiTietFilterCls ODT_LichChuyenGiaoChiTietFilter = new DT_LichChuyenGiaoChiTietFilterCls();
        return ODT_LichChuyenGiaoChiTietFilter;
    }


    public static DT_LichChuyenGiaoChiTietFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichChuyenGiaoChiTietFilterCls ODT_LichChuyenGiaoChiTietFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichChuyenGiaoChiTietFilter;
    }



    public static XmlCls GetXml(DT_LichChuyenGiaoChiTietFilterCls ODT_LichChuyenGiaoChiTietFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
