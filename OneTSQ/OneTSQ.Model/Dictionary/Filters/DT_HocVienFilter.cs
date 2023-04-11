﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_HocVienFilterCls : FilterCls
    {
        public string KhoaHocDangKy_Id;
        public string KhoaHocDuyet_Id;
        public string UserName;
        public DateTime? NgaySinh;
        public int? GioiTinh;
        public string MaDiaChiHanhChinh;
    }
}

public class DT_HocVienFilterParser
{
    public static DT_HocVienFilterCls CreateInstance()
    {
        DT_HocVienFilterCls ODT_HocVienFilter = new DT_HocVienFilterCls();
        return ODT_HocVienFilter;
    }


    public static DT_HocVienFilterCls ParseFromDataRow(DataRow dr)
    {
        DT_HocVienFilterCls ODT_HocVienFilter = new DT_HocVienFilterCls();
        return ODT_HocVienFilter;
    }


    public static DT_HocVienFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_HocVienFilterCls ODT_HocVienFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_HocVienFilter;
    }



    public static XmlCls GetXml(DT_HocVienFilterCls ODT_HocVienFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
