using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;

namespace OneTSQ.Model
{
    public class HoiDongXetDuyetCls
    {
        public string ID;
        public string LICHXETDUYET_ID;
        public string NGUOIDUNG_ID;
        public string CHUCVU_ID;  
  }
}

public class HoiDongXetDuyetParser
{
   
    public static HoiDongXetDuyetCls CreateInstance()
    {
        HoiDongXetDuyetCls OHoiDongXetDuyet = new HoiDongXetDuyetCls();
        return OHoiDongXetDuyet;
    }
    public static HoiDongXetDuyetCls ParseFromDataRow(DataRow dr)
    {
        HoiDongXetDuyetCls OHoiDongXetDuyet = new HoiDongXetDuyetCls();
        OHoiDongXetDuyet.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OHoiDongXetDuyet.LICHXETDUYET_ID = CoreXmlUtility.GetString(dr, "LICHXETDUYET_ID", true);
        OHoiDongXetDuyet.NGUOIDUNG_ID = CoreXmlUtility.GetString(dr, "NGUOIDUNG_ID", true);
        OHoiDongXetDuyet.CHUCVU_ID = CoreXmlUtility.GetString(dr, "CHUCVU_ID", true);      
        return OHoiDongXetDuyet;
    }

    public static HoiDongXetDuyetCls[] ParseFromDataTable(DataTable dtTable)
    {
        HoiDongXetDuyetCls[] HoiDongXetDuyets = new HoiDongXetDuyetCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            HoiDongXetDuyets[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return HoiDongXetDuyets;
    }


    public static HoiDongXetDuyetCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        HoiDongXetDuyetCls[] HoiDongXetDuyets = ParseFromDataTable(ds.Tables[0]);
        return HoiDongXetDuyets;
    }


    public static HoiDongXetDuyetCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        HoiDongXetDuyetCls OHoiDongXetDuyet = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OHoiDongXetDuyet;
    }


    public static XmlCls GetXml(HoiDongXetDuyetCls[] HoiDongXetDuyets)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHXETDUYET_ID");
        ds.Tables["info"].Columns.Add("NGUOIDUNG_ID");
        ds.Tables["info"].Columns.Add("CHUCVU_ID");     
        for (int iIndex = 0; iIndex < HoiDongXetDuyets.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                HoiDongXetDuyets[iIndex].ID,
                HoiDongXetDuyets[iIndex].LICHXETDUYET_ID,
                HoiDongXetDuyets[iIndex].NGUOIDUNG_ID,
                HoiDongXetDuyets[iIndex].CHUCVU_ID,         
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(HoiDongXetDuyetCls OHoiDongXetDuyet)
    {
        DataSet ds = new DataSet();
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHXETDUYET_ID");
        ds.Tables["info"].Columns.Add("NGUOIDUNG_ID");
        ds.Tables["info"].Columns.Add("CHUCVU_ID");
        ds.Tables["info"].Rows.Add(new object[]
        {
                OHoiDongXetDuyet.ID,
                OHoiDongXetDuyet.LICHXETDUYET_ID,
                OHoiDongXetDuyet.NGUOIDUNG_ID,
                OHoiDongXetDuyet.CHUCVU_ID,
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
