using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_GiayToDiChuyenGiaoFilterCls : FilterCls
    {
        public int? HieuLuc;
        public string NhomKhoaHoc_Ma;
    }

    public class DM_GiayToDiChuyenGiaoFilterParser
    {
        public static DM_GiayToDiChuyenGiaoFilterCls CreateInstance()
        {
            DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter = new DM_GiayToDiChuyenGiaoFilterCls();
            return OGiayToDiChuyenGiaoFilter;
        }


        public static DM_GiayToDiChuyenGiaoFilterCls ParseFromDataRow(DataRow dr)
        {
            DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter = new DM_GiayToDiChuyenGiaoFilterCls();
            return OGiayToDiChuyenGiaoFilter;
        }


        public static DM_GiayToDiChuyenGiaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OGiayToDiChuyenGiaoFilter;
        }



        public static XmlCls GetXml(DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter)
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
