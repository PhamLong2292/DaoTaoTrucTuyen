using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class CaBenhFilterCls : FilterCls
    {
        public string MABN;
        public string CABENHID;
        public string DONVITHAMVANID;
        public string DONVITUVANID;
        public int? TRANGTHAI;
        public int? LAPLICHDAXEM;
        public string DataPermissionQuery;
        public int[] TrangThais = new int[0];
        public DateTime? TUNGAY;
        public DateTime? DENNGAY;
        public string NGAYLOC;//tên trường dùng để tìm kiếm theo thời gian TUNGAY, DENNGAY
        public string UserId;
        public string CHUYENKHOAMA;
        public string CHANDOANBANDAUMA;
        public string LOAIHOICHANMA;
        public string LICHHOICHANID;
        public string KHACLICHHOICHANID;
    }
}

public class CaBenhFilterParser
{
    public static CaBenhFilterCls CreateInstance()
    {
        CaBenhFilterCls OCaBenhFilter = new CaBenhFilterCls();
        return OCaBenhFilter;
    }


    public static CaBenhFilterCls ParseFromDataRow(DataRow dr)
    {
        CaBenhFilterCls OCaBenhFilter = new CaBenhFilterCls();
        return OCaBenhFilter;
    }


    public static CaBenhFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        CaBenhFilterCls OCaBenhFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OCaBenhFilter;
    }



    public static XmlCls GetXml(CaBenhFilterCls OCaBenhFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
