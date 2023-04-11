using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_BcTongKetCongTacDaoTaoFilterCls : FilterCls
    {
        public DateTime? TuNgay;
        public DateTime? DenNgay;
        public string DataPermissionQuery;
    }
}

public class DT_BcTongKetCongTacDaoTaoFilterParser
{
    public static DT_BcTongKetCongTacDaoTaoFilterCls CreateInstance()
    {
        DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter = new DT_BcTongKetCongTacDaoTaoFilterCls();
        return ODT_BcTongKetCongTacDaoTaoFilter;
    }


    public static DT_BcTongKetCongTacDaoTaoFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter = new DT_BcTongKetCongTacDaoTaoFilterCls();
        return ODT_BcTongKetCongTacDaoTaoFilter;
    }


    public static DT_BcTongKetCongTacDaoTaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_BcTongKetCongTacDaoTaoFilter;
    }



    public static XmlCls GetXml(DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
