using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_TenKhoaHocFilterCls : FilterCls
    {
        public int? HieuLuc;
        public string NhomKhoaHoc_Ma;
    }

    public class DM_TenKhoaHocFilterParser
    {
        public static DM_TenKhoaHocFilterCls CreateInstance()
        {
            DM_TenKhoaHocFilterCls OTenKhoaHocFilter = new DM_TenKhoaHocFilterCls();
            return OTenKhoaHocFilter;
        }


        public static DM_TenKhoaHocFilterCls ParseFromDataRow(DataRow dr)
        {
            DM_TenKhoaHocFilterCls OTenKhoaHocFilter = new DM_TenKhoaHocFilterCls();
            return OTenKhoaHocFilter;
        }


        public static DM_TenKhoaHocFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_TenKhoaHocFilterCls OTenKhoaHocFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OTenKhoaHocFilter;
        }



        public static XmlCls GetXml(DM_TenKhoaHocFilterCls OTenKhoaHocFilter)
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
