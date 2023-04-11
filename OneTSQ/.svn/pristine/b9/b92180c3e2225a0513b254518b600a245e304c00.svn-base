using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class LapLichThanhVienHoiChanFilterCls : FilterCls
    {
        public string LICHHOICHANID;
        public string DONVICONGTACMA;
        public string KHACDONVICONGTACMA;
    }
}

public class LapLichThanhVienHoiChanFilterParser
{
    public static LapLichThanhVienHoiChanFilterCls CreateInstance()
    {
        LapLichThanhVienHoiChanFilterCls OLapLichThanhVienHoiChanFilter = new LapLichThanhVienHoiChanFilterCls();
        return OLapLichThanhVienHoiChanFilter;
    }


    public static LapLichThanhVienHoiChanFilterCls ParseFromDataRow(DataRow dr)
    {
        LapLichThanhVienHoiChanFilterCls OLapLichThanhVienHoiChanFilter = new LapLichThanhVienHoiChanFilterCls();
        return OLapLichThanhVienHoiChanFilter;
    }


    public static LapLichThanhVienHoiChanFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        LapLichThanhVienHoiChanFilterCls OLapLichThanhVienHoiChanFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OLapLichThanhVienHoiChanFilter;
    }



    public static XmlCls GetXml(LapLichThanhVienHoiChanFilterCls OLapLichThanhVienHoiChanFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
