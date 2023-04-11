using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DanhGiaDeCuong_DeTaiFilterCls : FilterCls
    {
        public string NGUOIDANHGIA_ID;
        public string DECUONG_ID;
        public string DETAI_ID;
        public int? TrangThai;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
    }
}

public class DanhGiaDeCuong_DeTaiFilterParser
{
    public static DanhGiaDeCuong_DeTaiFilterCls CreateInstance()
    {
        DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter = new DanhGiaDeCuong_DeTaiFilterCls();
        return ODanhGiaDeCuong_DeTaiFilter;
    }


    public static DanhGiaDeCuong_DeTaiFilterCls ParseFromDataRow(DataRow dr)
    {
        DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter = new DanhGiaDeCuong_DeTaiFilterCls();
        return ODanhGiaDeCuong_DeTaiFilter;
    }


    public static DanhGiaDeCuong_DeTaiFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODanhGiaDeCuong_DeTaiFilter;
    }



    public static XmlCls GetXml(DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
