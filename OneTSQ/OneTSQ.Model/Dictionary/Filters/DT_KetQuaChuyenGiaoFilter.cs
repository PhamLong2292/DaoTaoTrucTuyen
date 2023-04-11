using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneMES3.SYS.Core.Model;

namespace OneTSQ.Model
{
    public class DT_KetQuaChuyenGiaoFilterCls : FilterCls
    {
    }
}

public class DT_KetQuaChuyenGiaoFilterParser
{
    public static DT_KetQuaChuyenGiaoFilterCls CreateInstance()
    {
        DT_KetQuaChuyenGiaoFilterCls ODT_KetQuaChuyenGiaoFilter = new DT_KetQuaChuyenGiaoFilterCls();
        return ODT_KetQuaChuyenGiaoFilter;
    }


    public static DT_KetQuaChuyenGiaoFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_KetQuaChuyenGiaoFilterCls ODT_KetQuaChuyenGiaoFilter = new DT_KetQuaChuyenGiaoFilterCls();
        return ODT_KetQuaChuyenGiaoFilter;
    }


    public static DT_KetQuaChuyenGiaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_KetQuaChuyenGiaoFilterCls ODT_KetQuaChuyenGiaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_KetQuaChuyenGiaoFilter;
    }



    public static XmlCls GetXml(DT_KetQuaChuyenGiaoFilterCls ODT_KetQuaChuyenGiaoFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
