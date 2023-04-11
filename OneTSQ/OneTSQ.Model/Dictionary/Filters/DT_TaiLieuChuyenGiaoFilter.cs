using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_TaiLieuChuyenGiaoFilterCls : FilterCls
    {
        public string LICHCHUYENGIAO_ID;
    }
}

public class DT_TaiLieuChuyenGiaoFilterParser
{
    public static DT_TaiLieuChuyenGiaoFilterCls CreateInstance()
    {
        DT_TaiLieuChuyenGiaoFilterCls ODT_TaiLieuChuyenGiaoFilter = new DT_TaiLieuChuyenGiaoFilterCls();
        return ODT_TaiLieuChuyenGiaoFilter;
    }


    public static DT_TaiLieuChuyenGiaoFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_TaiLieuChuyenGiaoFilterCls ODT_TaiLieuChuyenGiaoFilter = new DT_TaiLieuChuyenGiaoFilterCls();
        return ODT_TaiLieuChuyenGiaoFilter;
    }


    public static DT_TaiLieuChuyenGiaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_TaiLieuChuyenGiaoFilterCls ODT_TaiLieuChuyenGiaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_TaiLieuChuyenGiaoFilter;
    }



    public static XmlCls GetXml(DT_TaiLieuChuyenGiaoFilterCls ODT_TaiLieuChuyenGiaoFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
