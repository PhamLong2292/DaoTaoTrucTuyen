using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class BinhLuanHinhAnhFilterCls : FilterCls
    {
        public string HINHANHID;
        public string CABENHANHCLSID;
        public string BINHLUANHINHANHID;
        public bool? isParent;
        public bool? isChildren;
    }
}

public class BinhLuanHinhAnhFilterParser
{
    public static BinhLuanHinhAnhFilterCls CreateInstance()
    {
        BinhLuanHinhAnhFilterCls OBinhLuanHinhAnhFilter = new BinhLuanHinhAnhFilterCls();
        return OBinhLuanHinhAnhFilter;
    }


    public static BinhLuanHinhAnhFilterCls ParseFromDataRow(DataRow dr)
    {
        BinhLuanHinhAnhFilterCls OBinhLuanHinhAnhFilter = new BinhLuanHinhAnhFilterCls();
        return OBinhLuanHinhAnhFilter;
    }


    public static BinhLuanHinhAnhFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        BinhLuanHinhAnhFilterCls OBinhLuanHinhAnhFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBinhLuanHinhAnhFilter;
    }



    public static XmlCls GetXml(BinhLuanHinhAnhFilterCls OBinhLuanHinhAnhFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
