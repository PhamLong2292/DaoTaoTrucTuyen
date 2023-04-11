using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_ChuyenKhoaDaoTaoTtFilterCls : FilterCls
    {
        public int? HieuLuc;
    }

    public class DM_ChuyenKhoaDaoTaoTtFilterParser
    {
        public static DM_ChuyenKhoaDaoTaoTtFilterCls CreateInstance()
        {
            DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter = new DM_ChuyenKhoaDaoTaoTtFilterCls();
            return OChuyenKhoaDaoTaoTtFilter;
        }


        public static DM_ChuyenKhoaDaoTaoTtFilterCls ParseFromDataRow(DataRow dr)
        {
            DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter = new DM_ChuyenKhoaDaoTaoTtFilterCls();
            return OChuyenKhoaDaoTaoTtFilter;
        }


        public static DM_ChuyenKhoaDaoTaoTtFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OChuyenKhoaDaoTaoTtFilter;
        }



        public static XmlCls GetXml(DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter)
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
