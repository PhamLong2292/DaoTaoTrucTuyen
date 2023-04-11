using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichThucHanhChiTietFilterCls : FilterCls
    {
        public string KHOAHOC_ID;
        public string LICHTHUCHANH_ID;
        public DateTime? NGAY;
        public string GIANGVIEN_ID;
        public string HOCVIEN_ID;
        public bool? ISKHOAHOCKETTHUC;
    }
}

public class DT_LichThucHanhChiTietFilterParser
{
    public static DT_LichThucHanhChiTietFilterCls CreateInstance()
    {
        DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter = new DT_LichThucHanhChiTietFilterCls();
        return ODT_LichThucHanhChiTietFilter;
    }


    public static DT_LichThucHanhChiTietFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter = new DT_LichThucHanhChiTietFilterCls();
        return ODT_LichThucHanhChiTietFilter;
    }


    public static DT_LichThucHanhChiTietFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichThucHanhChiTietFilter;
    }



    public static XmlCls GetXml(DT_LichThucHanhChiTietFilterCls ODT_LichThucHanhChiTietFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
