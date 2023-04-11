using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class HoiDongXetDuyetFilterCls : FilterCls
    {
        public string LICHXETDUYET_ID;
        public string NGUOIDUNG_ID;
        public string CHUCVU_ID;
        public int? TrangThai;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
    }
}

public class HoiDongXetDuyetFilterParser
{
    public static HoiDongXetDuyetFilterCls CreateInstance()
    {
        HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter = new HoiDongXetDuyetFilterCls();
        return OHoiDongXetDuyetFilter;
    }


    public static HoiDongXetDuyetFilterCls ParseFromDataRow(DataRow dr)
    {
        HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter = new HoiDongXetDuyetFilterCls();
        return OHoiDongXetDuyetFilter;
    }


    public static HoiDongXetDuyetFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OHoiDongXetDuyetFilter;
    }



    public static XmlCls GetXml(HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
