using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_DiemDanhLyThuyetFilterCls : FilterCls
    {
        public string KhoaHocId;
        public string HocVienId;
        public string LichLyThuyetChiTietId;
    }
}

public class DT_DiemDanhLyThuyetFilterParser
{
    public static DT_DiemDanhLyThuyetFilterCls CreateInstance()
    {
        DT_DiemDanhLyThuyetFilterCls ODT_DiemDanhLyThuyetFilter = new DT_DiemDanhLyThuyetFilterCls();
        return ODT_DiemDanhLyThuyetFilter;
    }


    public static DT_DiemDanhLyThuyetFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_DiemDanhLyThuyetFilterCls ODT_DiemDanhLyThuyetFilter = new DT_DiemDanhLyThuyetFilterCls();
        return ODT_DiemDanhLyThuyetFilter;
    }


    public static DT_DiemDanhLyThuyetFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_DiemDanhLyThuyetFilterCls ODT_DiemDanhLyThuyetFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_DiemDanhLyThuyetFilter;
    }



    public static XmlCls GetXml(DT_DiemDanhLyThuyetFilterCls ODT_DiemDanhLyThuyetFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
