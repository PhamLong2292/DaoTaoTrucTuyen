using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_TieuChiThoiGianDaoTaoTtFilterCls : FilterCls
    {
        public int? HieuLuc;
    }

    public class DM_TieuChiThoiGianDaoTaoTtFilterParser
    {
        public static DM_TieuChiThoiGianDaoTaoTtFilterCls CreateInstance()
        {
            DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter = new DM_TieuChiThoiGianDaoTaoTtFilterCls();
            return OTieuChiThoiGianDaoTaoTtFilter;
        }


        public static DM_TieuChiThoiGianDaoTaoTtFilterCls ParseFromDataRow(DataRow dr)
        {
            DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter = new DM_TieuChiThoiGianDaoTaoTtFilterCls();
            return OTieuChiThoiGianDaoTaoTtFilter;
        }


        public static DM_TieuChiThoiGianDaoTaoTtFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OTieuChiThoiGianDaoTaoTtFilter;
        }



        public static XmlCls GetXml(DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter)
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
