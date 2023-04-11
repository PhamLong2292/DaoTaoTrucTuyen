using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_TrangThietBiTruyenHinhTtFilterCls : FilterCls
    {
        public int? HieuLuc;
    }

    public class DM_TrangThietBiTruyenHinhTtFilterParser
    {
        public static DM_TrangThietBiTruyenHinhTtFilterCls CreateInstance()
        {
            DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter = new DM_TrangThietBiTruyenHinhTtFilterCls();
            return OTrangThietBiTruyenHinhTtFilter;
        }


        public static DM_TrangThietBiTruyenHinhTtFilterCls ParseFromDataRow(DataRow dr)
        {
            DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter = new DM_TrangThietBiTruyenHinhTtFilterCls();
            return OTrangThietBiTruyenHinhTtFilter;
        }


        public static DM_TrangThietBiTruyenHinhTtFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OTrangThietBiTruyenHinhTtFilter;
        }



        public static XmlCls GetXml(DM_TrangThietBiTruyenHinhTtFilterCls OTrangThietBiTruyenHinhTtFilter)
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
