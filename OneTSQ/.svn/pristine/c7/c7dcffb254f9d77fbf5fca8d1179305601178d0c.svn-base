using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_KhoaHocFilterCls : FilterCls
    {
        public int? Nam;
        public int? LoaiKhoaHoc;
        public int? LoaiDaoTao;
        public string MaKhoaHoc;
        public int? TrangThai;
        public int[] TrangThais = new int[0];
        public string DataPermissionQuery;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
    }
}

public class DT_KhoaHocFilterParser
{
    public static DT_KhoaHocFilterCls CreateInstance()
    {
        DT_KhoaHocFilterCls ODT_KhoaHocFilter = new DT_KhoaHocFilterCls();
        return ODT_KhoaHocFilter;
    }


    public static DT_KhoaHocFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_KhoaHocFilterCls ODT_KhoaHocFilter = new DT_KhoaHocFilterCls();
        return ODT_KhoaHocFilter;
    }


    public static DT_KhoaHocFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_KhoaHocFilterCls ODT_KhoaHocFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_KhoaHocFilter;
    }



    public static XmlCls GetXml(DT_KhoaHocFilterCls ODT_KhoaHocFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
