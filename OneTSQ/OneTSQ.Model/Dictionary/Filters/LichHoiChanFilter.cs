using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class LichHoiChanFilterCls : FilterCls
    {
        public string CaBenhId;
        public string ChuyenKhoaMa;
        public string ChuTriId;
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? TrangThai;
        public string[] caBenhIds = new string[0];
        public int[] TrangThais = new int[0];
        public string DataPermissionQuery;
    }
}

public class LichHoiChanFilterParser
{
    public static LichHoiChanFilterCls CreateInstance()
    {
        LichHoiChanFilterCls OLichHoiChanFilter = new LichHoiChanFilterCls();
        return OLichHoiChanFilter;
    }


    public static LichHoiChanFilterCls ParseFromDataRow(DataRow dr)
    {
        LichHoiChanFilterCls OLichHoiChanFilter = new LichHoiChanFilterCls();
        return OLichHoiChanFilter;
    }


    public static LichHoiChanFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        LichHoiChanFilterCls OLichHoiChanFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OLichHoiChanFilter;
    }



    public static XmlCls GetXml(LichHoiChanFilterCls OLichHoiChanFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
