using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class BienBanHoiChanFilterCls:FilterCls 
    {
        public string CABENHID;
    }
}

public class BienBanHoiChanFilterParser 
{ 
    public static BienBanHoiChanFilterCls CreateInstance() 
    { 
        BienBanHoiChanFilterCls OBienBanHoiChanFilter = new BienBanHoiChanFilterCls(); 
        return OBienBanHoiChanFilter; 
    } 


    public static BienBanHoiChanFilterCls ParseFromDataRow(DataRow dr) 
    { 
        BienBanHoiChanFilterCls OBienBanHoiChanFilter = new BienBanHoiChanFilterCls(); 
        return OBienBanHoiChanFilter;
    }


    public static BienBanHoiChanFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        BienBanHoiChanFilterCls OBienBanHoiChanFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBienBanHoiChanFilter;
    }



    public static XmlCls GetXml(BienBanHoiChanFilterCls OBienBanHoiChanFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
