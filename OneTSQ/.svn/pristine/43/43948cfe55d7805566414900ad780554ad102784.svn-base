using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class ThuocADRFilterCls : FilterCls
    {
        public string Hang_ID;
        public string Phieu_ID;
        public string KhoaPhong_ID;
        public int? LoaiThuoc;
        public int? TrangThai;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
        public DateTime? NgayVaoVien;
        public DateTime? NgayRaVien;
    }
}

public class ThuocADRFilterParser
{
    public static ThuocADRFilterCls CreateInstance()
    {
        ThuocADRFilterCls OThuocADRFilter = new ThuocADRFilterCls();
        return OThuocADRFilter;
    }


    public static ThuocADRFilterCls ParseFromDataRow(DataRow dr)
    {
        ThuocADRFilterCls OThuocADRFilter = new ThuocADRFilterCls();
        return OThuocADRFilter;
    }


    public static ThuocADRFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        ThuocADRFilterCls OThuocADRFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OThuocADRFilter;
    }



    public static XmlCls GetXml(ThuocADRFilterCls OThuocADRFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
