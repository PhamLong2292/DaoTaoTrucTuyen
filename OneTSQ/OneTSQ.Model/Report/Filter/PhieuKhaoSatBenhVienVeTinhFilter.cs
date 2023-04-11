using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class PhieuKhaoSatBenhVienVeTinhFilterCls : FilterCls
    {
        public string BenhVien_ID;
        public int? TrangThai;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
        public DateTime? ThoiGian;
        public DateTime? NgayThanhLap;
    }
}

public class PhieuKhaoSatBenhVienVeTinhFilterParser
{
    public static PhieuKhaoSatBenhVienVeTinhFilterCls CreateInstance()
    {
        PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter = new PhieuKhaoSatBenhVienVeTinhFilterCls();
        return OPhieuKhaoSatBenhVienVeTinhFilter;
    }


    public static PhieuKhaoSatBenhVienVeTinhFilterCls ParseFromDataRow(DataRow dr)
    {
        PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter = new PhieuKhaoSatBenhVienVeTinhFilterCls();
        return OPhieuKhaoSatBenhVienVeTinhFilter;
    }


    public static PhieuKhaoSatBenhVienVeTinhFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OPhieuKhaoSatBenhVienVeTinhFilter;
    }



    public static XmlCls GetXml(PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
