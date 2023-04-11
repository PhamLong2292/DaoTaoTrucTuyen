using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DeTaiFilterCls : FilterCls
    {
        public string DANGKYDETAI_ID;
        public string NGUOIGUI_ID;
        public string LICHXETDUYET_ID;
        public string TrangThais;
        public int? LOAIHINH;
        public int? CAPDETAI;
        public int? KETLUAN;
        public int? TrangThai;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
    }
}

public class DeTaiFilterParser
{
    public static DeTaiFilterCls CreateInstance()
    {
        DeTaiFilterCls ODeTaiFilter = new DeTaiFilterCls();
        return ODeTaiFilter;
    }


    public static DeTaiFilterCls ParseFromDataRow(DataRow dr)
    {
        DeTaiFilterCls ODeTaiFilter = new DeTaiFilterCls();
        return ODeTaiFilter;
    }


    public static DeTaiFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DeTaiFilterCls ODeTaiFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODeTaiFilter;
    }



    public static XmlCls GetXml(DeTaiFilterCls ODeTaiFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
