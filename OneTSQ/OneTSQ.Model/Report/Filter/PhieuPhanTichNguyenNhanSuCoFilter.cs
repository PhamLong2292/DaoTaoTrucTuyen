using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class PhieuPhanTichNguyenNhanSuCoFilterCls : FilterCls
    {
        public string NguoiLap_ID;
        public string ChucDanh_ID;
        public int? TrangThai;
        public string DataPermissionQuery;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
        public DateTime? NgaySinh;
        public int? GioiTinh;
    }
}

public class PhieuPhanTichNguyenNhanSuCoFilterParser
{
    public static PhieuPhanTichNguyenNhanSuCoFilterCls CreateInstance()
    {
        PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter = new PhieuPhanTichNguyenNhanSuCoFilterCls();
        return OPhieuPhanTichNguyenNhanSuCoFilter;
    }


    public static PhieuPhanTichNguyenNhanSuCoFilterCls ParseFromDataRow(DataRow dr)
    {
        PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter = new PhieuPhanTichNguyenNhanSuCoFilterCls();
        return OPhieuPhanTichNguyenNhanSuCoFilter;
    }


    public static PhieuPhanTichNguyenNhanSuCoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OPhieuPhanTichNguyenNhanSuCoFilter;
    }



    public static XmlCls GetXml(PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
