using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class TaiLieuFilterCls:FilterCls 
    { 
    }
}

public class TaiLieuFilterParser 
{ 
    public static TaiLieuFilterCls CreateInstance() 
    { 
        TaiLieuFilterCls OTaiLieuFilter = new TaiLieuFilterCls(); 
        return OTaiLieuFilter; 
    } 


    public static TaiLieuFilterCls ParseFromDataRow(DataRow dr) 
    { 
        TaiLieuFilterCls OTaiLieuFilter = new TaiLieuFilterCls(); 
        return OTaiLieuFilter;
    }


    public static TaiLieuFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        TaiLieuFilterCls OTaiLieuFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OTaiLieuFilter;
    }



    public static XmlCls GetXml(TaiLieuFilterCls OTaiLieuFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
