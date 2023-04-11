using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class LichXetDuyetFilterCls : FilterCls
    {
        public string NGUOILAP_ID;
        public string NGUOIDUYET_ID;
        public string CHUTRI_ID;
        public string THUKY_ID;
        public int? LOAIHINH;
        public int? CAPDETAI;
        public int? TrangThai;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
    }
}

public class LichXetDuyetFilterParser
{
    public static LichXetDuyetFilterCls CreateInstance()
    {
        LichXetDuyetFilterCls OLichXetDuyetFilter = new LichXetDuyetFilterCls();
        return OLichXetDuyetFilter;
    }


    public static LichXetDuyetFilterCls ParseFromDataRow(DataRow dr)
    {
        LichXetDuyetFilterCls OLichXetDuyetFilter = new LichXetDuyetFilterCls();
        return OLichXetDuyetFilter;
    }


    public static LichXetDuyetFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        LichXetDuyetFilterCls OLichXetDuyetFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OLichXetDuyetFilter;
    }



    public static XmlCls GetXml(LichXetDuyetFilterCls OLichXetDuyetFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
