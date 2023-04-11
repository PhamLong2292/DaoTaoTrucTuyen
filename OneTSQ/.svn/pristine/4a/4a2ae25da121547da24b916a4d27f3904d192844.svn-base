using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class CongTacVienDeTaiFilterCls : FilterCls
    {
        public string DANGKYDETAI_ID;
        public string NGUOIDUNG_ID;
        public int? TrangThai;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
    }
}

public class CongTacVienDeTaiFilterParser
{
    public static CongTacVienDeTaiFilterCls CreateInstance()
    {
        CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter = new CongTacVienDeTaiFilterCls();
        return OCongTacVienDeTaiFilter;
    }


    public static CongTacVienDeTaiFilterCls ParseFromDataRow(DataRow dr)
    {
        CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter = new CongTacVienDeTaiFilterCls();
        return OCongTacVienDeTaiFilter;
    }


    public static CongTacVienDeTaiFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OCongTacVienDeTaiFilter;
    }



    public static XmlCls GetXml(CongTacVienDeTaiFilterCls OCongTacVienDeTaiFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
