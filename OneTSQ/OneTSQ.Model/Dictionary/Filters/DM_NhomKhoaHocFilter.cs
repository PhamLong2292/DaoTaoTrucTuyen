using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_NhomKhoaHocFilterCls : FilterCls
    {
        public int? HieuLuc;
    }

    public class DM_NhomKhoaHocFilterParser
    {
        public static DM_NhomKhoaHocFilterCls CreateInstance()
        {
            DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter = new DM_NhomKhoaHocFilterCls();
            return ONhomKhoaHocFilter;
        }


        public static DM_NhomKhoaHocFilterCls ParseFromDataRow(DataRow dr)
        {
            DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter = new DM_NhomKhoaHocFilterCls();
            return ONhomKhoaHocFilter;
        }


        public static DM_NhomKhoaHocFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return ONhomKhoaHocFilter;
        }



        public static XmlCls GetXml(DM_NhomKhoaHocFilterCls ONhomKhoaHocFilter)
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
