using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class ChuyenGiaHoiChanFilterCls:FilterCls
    {
        public string BIENBANHOICHANID;
    }
}

public class ChuyenGiaHoiChanFilterParser 
{ 
    public static ChuyenGiaHoiChanFilterCls CreateInstance() 
    { 
        ChuyenGiaHoiChanFilterCls OChuyenGiaHoiChanFilter = new ChuyenGiaHoiChanFilterCls(); 
        return OChuyenGiaHoiChanFilter; 
    } 


    public static ChuyenGiaHoiChanFilterCls ParseFromDataRow(DataRow dr) 
    { 
        ChuyenGiaHoiChanFilterCls OChuyenGiaHoiChanFilter = new ChuyenGiaHoiChanFilterCls(); 
        return OChuyenGiaHoiChanFilter;
    }


    public static ChuyenGiaHoiChanFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        ChuyenGiaHoiChanFilterCls OChuyenGiaHoiChanFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OChuyenGiaHoiChanFilter;
    }



    public static XmlCls GetXml(ChuyenGiaHoiChanFilterCls OChuyenGiaHoiChanFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
