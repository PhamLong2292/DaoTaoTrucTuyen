using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class YKienChuyenGiaFilterCls:FilterCls 
    {
        public string CABENHID;
        public string YKIENCHUYENGIAID;
        public bool? isParent;
        public bool? isChildren;
    }
}

public class YKienChuyenGiaFilterParser 
{ 
    public static YKienChuyenGiaFilterCls CreateInstance() 
    { 
        YKienChuyenGiaFilterCls OYKienChuyenGiaFilter = new YKienChuyenGiaFilterCls(); 
        return OYKienChuyenGiaFilter; 
    } 


    public static YKienChuyenGiaFilterCls ParseFromDataRow(DataRow dr) 
    { 
        YKienChuyenGiaFilterCls OYKienChuyenGiaFilter = new YKienChuyenGiaFilterCls(); 
        return OYKienChuyenGiaFilter;
    }


    public static YKienChuyenGiaFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        YKienChuyenGiaFilterCls OYKienChuyenGiaFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OYKienChuyenGiaFilter;
    }



    public static XmlCls GetXml(YKienChuyenGiaFilterCls OYKienChuyenGiaFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
