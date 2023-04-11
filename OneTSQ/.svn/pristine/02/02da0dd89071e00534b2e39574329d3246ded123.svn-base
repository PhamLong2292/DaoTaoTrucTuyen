using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class TaiLieuDinhKemFilterCls : FilterCls
    {
        public string DOCUMENT_ID;
        public string NGUOITAO_ID;
        public int? TrangThai;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
    }
}

public class TaiLieuDinhKemFilterParser
{
    public static TaiLieuDinhKemFilterCls CreateInstance()
    {
        TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter = new TaiLieuDinhKemFilterCls();
        return OTaiLieuDinhKemFilter;
    }


    public static TaiLieuDinhKemFilterCls ParseFromDataRow(DataRow dr)
    {
        TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter = new TaiLieuDinhKemFilterCls();
        return OTaiLieuDinhKemFilter;
    }


    public static TaiLieuDinhKemFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OTaiLieuDinhKemFilter;
    }



    public static XmlCls GetXml(TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
