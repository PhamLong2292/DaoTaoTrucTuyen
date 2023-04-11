using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichChuyenGiaoFilterCls : FilterCls
    {
        public string KYTHUAT_MA;
        public string KHOAHOC_ID;
        public string BENHVIEN_MA;
        public string BACSY_ID;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
        public int? Nam;
        public int? TrangThai;
        public string DataPermissionQuery;
    }
}

public class DT_LichChuyenGiaoFilterParser
{
    public static DT_LichChuyenGiaoFilterCls CreateInstance()
    {
        DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter = new DT_LichChuyenGiaoFilterCls();
        return ODT_LichChuyenGiaoFilter;
    }


    public static DT_LichChuyenGiaoFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter = new DT_LichChuyenGiaoFilterCls();
        return ODT_LichChuyenGiaoFilter;
    }


    public static DT_LichChuyenGiaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichChuyenGiaoFilter;
    }



    public static XmlCls GetXml(DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
