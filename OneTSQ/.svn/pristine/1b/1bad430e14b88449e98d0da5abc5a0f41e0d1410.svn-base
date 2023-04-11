using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_TieuChiThoiLuongDaoTaoTtFilterCls : FilterCls
    {
        public int? HieuLuc;
    }

    public class DM_TieuChiThoiLuongDaoTaoTtFilterParser
    {
        public static DM_TieuChiThoiLuongDaoTaoTtFilterCls CreateInstance()
        {
            DM_TieuChiThoiLuongDaoTaoTtFilterCls OTieuChiThoiLuongDaoTaoTtFilter = new DM_TieuChiThoiLuongDaoTaoTtFilterCls();
            return OTieuChiThoiLuongDaoTaoTtFilter;
        }


        public static DM_TieuChiThoiLuongDaoTaoTtFilterCls ParseFromDataRow(DataRow dr)
        {
            DM_TieuChiThoiLuongDaoTaoTtFilterCls OTieuChiThoiLuongDaoTaoTtFilter = new DM_TieuChiThoiLuongDaoTaoTtFilterCls();
            return OTieuChiThoiLuongDaoTaoTtFilter;
        }


        public static DM_TieuChiThoiLuongDaoTaoTtFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_TieuChiThoiLuongDaoTaoTtFilterCls OTieuChiThoiLuongDaoTaoTtFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OTieuChiThoiLuongDaoTaoTtFilter;
        }
        public static XmlCls GetXml(DM_TieuChiThoiLuongDaoTaoTtFilterCls OTieuChiThoiLuongDaoTaoTtFilter)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("info");
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
