﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DangKyDeTaiFilterCls : FilterCls
    {
        public int? LOAIHINH;
        public int? CAPDETAI;
        public string CHUNGHIEM_ID;
        public string CHUCDANH_ID;
        public string NGUOIDUYET_ID;
        public int? TrangThai;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
    }
}

public class DangKyDeTaiFilterParser
{
    public static DangKyDeTaiFilterCls CreateInstance()
    {
        DangKyDeTaiFilterCls ODangKyDeTaiFilter = new DangKyDeTaiFilterCls();
        return ODangKyDeTaiFilter;
    }


    public static DangKyDeTaiFilterCls ParseFromDataRow(DataRow dr)
    {
        DangKyDeTaiFilterCls ODangKyDeTaiFilter = new DangKyDeTaiFilterCls();
        return ODangKyDeTaiFilter;
    }


    public static DangKyDeTaiFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DangKyDeTaiFilterCls ODangKyDeTaiFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODangKyDeTaiFilter;
    }



    public static XmlCls GetXml(DangKyDeTaiFilterCls ODangKyDeTaiFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
