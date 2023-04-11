using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_DaoTaoVienTruongFilterCls : FilterCls
    {
        public string BCTONGKETCONGTACDAOTAO_ID;
    }
}

public class DT_DaoTaoVienTruongFilterParser
{
    public static DT_DaoTaoVienTruongFilterCls CreateInstance()
    {
        DT_DaoTaoVienTruongFilterCls ODT_DaoTaoVienTruongFilter = new DT_DaoTaoVienTruongFilterCls();
        return ODT_DaoTaoVienTruongFilter;
    }


    public static DT_DaoTaoVienTruongFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_DaoTaoVienTruongFilterCls ODT_DaoTaoVienTruongFilter = new DT_DaoTaoVienTruongFilterCls();
        return ODT_DaoTaoVienTruongFilter;
    }


    public static DT_DaoTaoVienTruongFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_DaoTaoVienTruongFilterCls ODT_DaoTaoVienTruongFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_DaoTaoVienTruongFilter;
    }



    public static XmlCls GetXml(DT_DaoTaoVienTruongFilterCls ODT_DaoTaoVienTruongFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
