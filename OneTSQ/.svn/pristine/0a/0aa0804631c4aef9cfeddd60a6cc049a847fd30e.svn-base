using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_KetQuaDaoTaoFilterCls : FilterCls
    {
        public string KhoaHocDangKy_Id;
        public string KhoaHocDuyet_Id;
        public string HocVien_Id;
        public string DonViCongTacMa;
        public int? TrangThai;
        public int? DatTieuChuan;
        public string DataPermissionQuery;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
        public int? LoaiDaoTao;
        public int? TrangThaiKhoaHoc;
    }
}

public class DT_KetQuaDaoTaoFilterParser
{
    public static DT_KetQuaDaoTaoFilterCls CreateInstance()
    {
        DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter = new DT_KetQuaDaoTaoFilterCls();
        return ODT_KetQuaDaoTaoFilter;
    }


    public static DT_KetQuaDaoTaoFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter = new DT_KetQuaDaoTaoFilterCls();
        return ODT_KetQuaDaoTaoFilter;
    }


    public static DT_KetQuaDaoTaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_KetQuaDaoTaoFilter;
    }



    public static XmlCls GetXml(DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
