using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_TieuChuanThamGiaKhoaHocFilterCls : FilterCls
    {
        public int? HieuLuc;
        public string NhomKhoaHoc_Ma;
    }

    public class DM_TieuChuanThamGiaKhoaHocFilterParser
    {
        public static DM_TieuChuanThamGiaKhoaHocFilterCls CreateInstance()
        {
            DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter = new DM_TieuChuanThamGiaKhoaHocFilterCls();
            return OTieuChuanThamGiaKhoaHocFilter;
        }


        public static DM_TieuChuanThamGiaKhoaHocFilterCls ParseFromDataRow(DataRow dr)
        {
            DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter = new DM_TieuChuanThamGiaKhoaHocFilterCls();
            return OTieuChuanThamGiaKhoaHocFilter;
        }


        public static DM_TieuChuanThamGiaKhoaHocFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OTieuChuanThamGiaKhoaHocFilter;
        }



        public static XmlCls GetXml(DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter)
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
