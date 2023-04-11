using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichLyThuyetChiTietFilterCls : FilterCls
    {
        public string LICHLYTHUYET_ID;
        public DateTime? NGAY;
        public string GIANGVIEN_ID;
        public bool? ISKHOAHOCKETTHUC;
    }
}

public class DT_LichLyThuyetChiTietFilterParser
{
    public static DT_LichLyThuyetChiTietFilterCls CreateInstance()
    {
        DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter = new DT_LichLyThuyetChiTietFilterCls();
        return ODT_LichLyThuyetChiTietFilter;
    }


    public static DT_LichLyThuyetChiTietFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter = new DT_LichLyThuyetChiTietFilterCls();
        return ODT_LichLyThuyetChiTietFilter;
    }


    public static DT_LichLyThuyetChiTietFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichLyThuyetChiTietFilter;
    }



    public static XmlCls GetXml(DT_LichLyThuyetChiTietFilterCls ODT_LichLyThuyetChiTietFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
