using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DaoTaoNhanLucFilterCls : FilterCls
    {
        public string PHIEUKHAOSATBVVT_ID;
        public string DM_TENKHOAHOC_ID;
        public int? SOLUONG;
        public int? TrangThai;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
    }
}

public class DaoTaoNhanLucFilterParser
{
    public static DaoTaoNhanLucFilterCls CreateInstance()
    {
        DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter = new DaoTaoNhanLucFilterCls();
        return ODaoTaoNhanLucFilter;
    }


    public static DaoTaoNhanLucFilterCls ParseFromDataRow(DataRow dr)
    {
        DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter = new DaoTaoNhanLucFilterCls();
        return ODaoTaoNhanLucFilter;
    }


    public static DaoTaoNhanLucFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODaoTaoNhanLucFilter;
    }



    public static XmlCls GetXml(DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
