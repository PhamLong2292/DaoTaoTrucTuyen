using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class BienBanHoiChanCls 
    { 
        public string ID; 
        public string CABENHID = ""; 
        public DateTime BATDAUHOICHANVAO;
        public DateTime? KETTHUCHOICHANVAO;
        public string DIADIEMHOICHAN = ""; 
        public string YKIENKHAMBENH = ""; 
        public string YKIENCANLAMSANG = ""; 
        public string YKIENCHANDOAN = ""; 
        public string YKIENDIEUTRI = ""; 
        public string YKIENKHAC = ""; 
        public string THUKY = ""; 
        public string CHUTRIHOICHAN = ""; 
        public string TAOBOI = ""; 
        public DateTime TAOVAO;
        public int TRANGTHAI;
    }
}

public class BienBanHoiChanParser 
{ 
    public static BienBanHoiChanCls CreateInstance() 
    { 
        BienBanHoiChanCls OBienBanHoiChan = new BienBanHoiChanCls(); 
        return OBienBanHoiChan; 
    } 


    public static BienBanHoiChanCls ParseFromDataRow(DataRow dr) 
    { 
        BienBanHoiChanCls OBienBanHoiChan = new BienBanHoiChanCls(); 
        OBienBanHoiChan.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OBienBanHoiChan.CABENHID = CoreXmlUtility.GetString(dr, "CABENHID", true);
        OBienBanHoiChan.BATDAUHOICHANVAO = CoreXmlUtility.GetDate(dr, "BATDAUHOICHANVAO", true);
        OBienBanHoiChan.KETTHUCHOICHANVAO = dr["KETTHUCHOICHANVAO"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "KETTHUCHOICHANVAO", true);
        OBienBanHoiChan.DIADIEMHOICHAN = CoreXmlUtility.GetString(dr, "DIADIEMHOICHAN", true);
        OBienBanHoiChan.YKIENKHAMBENH = CoreXmlUtility.GetString(dr, "YKIENKHAMBENH", true);
        OBienBanHoiChan.YKIENCANLAMSANG = CoreXmlUtility.GetString(dr, "YKIENCANLAMSANG", true);
        OBienBanHoiChan.YKIENCHANDOAN = CoreXmlUtility.GetString(dr, "YKIENCHANDOAN", true);
        OBienBanHoiChan.YKIENDIEUTRI = CoreXmlUtility.GetString(dr, "YKIENDIEUTRI", true);
        OBienBanHoiChan.YKIENKHAC = CoreXmlUtility.GetString(dr, "YKIENKHAC", true);
        OBienBanHoiChan.THUKY = CoreXmlUtility.GetString(dr, "THUKY", true);
        OBienBanHoiChan.CHUTRIHOICHAN = CoreXmlUtility.GetString(dr, "CHUTRIHOICHAN", true);
        OBienBanHoiChan.TAOBOI = CoreXmlUtility.GetString(dr, "TAOBOI", true);
        OBienBanHoiChan.TAOVAO = CoreXmlUtility.GetDate(dr, "TAOVAO", true);
        OBienBanHoiChan.TRANGTHAI = CoreXmlUtility.GetInt(dr, "TRANGTHAI", true);
        return OBienBanHoiChan;
    }

    public static BienBanHoiChanCls[] ParseFromDataTable(DataTable dtTable) 
    {
        BienBanHoiChanCls[] BienBanHoiChans = new BienBanHoiChanCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            BienBanHoiChans[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return BienBanHoiChans;
    }


    public static BienBanHoiChanCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        BienBanHoiChanCls[] BienBanHoiChans = ParseFromDataTable(ds.Tables[0]);
        return BienBanHoiChans;
    }


    public static BienBanHoiChanCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        BienBanHoiChanCls OBienBanHoiChan = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBienBanHoiChan;
    }


    public static XmlCls GetXml(BienBanHoiChanCls[] BienBanHoiChans)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("BATDAUHOICHANVAO",typeof(DateTime));
        ds.Tables["info"].Columns.Add("KETTHUCHOICHANVAO",typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMHOICHAN");
        ds.Tables["info"].Columns.Add("YKIENKHAMBENH");
        ds.Tables["info"].Columns.Add("YKIENCANLAMSANG");
        ds.Tables["info"].Columns.Add("YKIENCHANDOAN");
        ds.Tables["info"].Columns.Add("YKIENDIEUTRI");
        ds.Tables["info"].Columns.Add("YKIENKHAC");
        ds.Tables["info"].Columns.Add("THUKY");
        ds.Tables["info"].Columns.Add("CHUTRIHOICHAN");
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO",typeof(DateTime));
        ds.Tables["info"].Columns.Add("TRANGTHAI",typeof(int));
        for (int iIndex = 0; iIndex < BienBanHoiChans.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                BienBanHoiChans[iIndex].ID,
                BienBanHoiChans[iIndex].CABENHID,
                BienBanHoiChans[iIndex].BATDAUHOICHANVAO,
                BienBanHoiChans[iIndex].KETTHUCHOICHANVAO,
                BienBanHoiChans[iIndex].DIADIEMHOICHAN,
                BienBanHoiChans[iIndex].YKIENKHAMBENH,
                BienBanHoiChans[iIndex].YKIENCANLAMSANG,
                BienBanHoiChans[iIndex].YKIENCHANDOAN,
                BienBanHoiChans[iIndex].YKIENDIEUTRI,
                BienBanHoiChans[iIndex].YKIENKHAC,
                BienBanHoiChans[iIndex].THUKY,
                BienBanHoiChans[iIndex].CHUTRIHOICHAN,
                BienBanHoiChans[iIndex].TAOBOI,
                BienBanHoiChans[iIndex].TAOVAO,
                BienBanHoiChans[iIndex].TRANGTHAI
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(BienBanHoiChanCls OBienBanHoiChan)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("BATDAUHOICHANVAO",typeof(DateTime));
        ds.Tables["info"].Columns.Add("KETTHUCHOICHANVAO",typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMHOICHAN");
        ds.Tables["info"].Columns.Add("YKIENKHAMBENH");
        ds.Tables["info"].Columns.Add("YKIENCANLAMSANG");
        ds.Tables["info"].Columns.Add("YKIENCHANDOAN");
        ds.Tables["info"].Columns.Add("YKIENDIEUTRI");
        ds.Tables["info"].Columns.Add("YKIENKHAC");
        ds.Tables["info"].Columns.Add("THUKY");
        ds.Tables["info"].Columns.Add("CHUTRIHOICHAN");
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO",typeof(DateTime));
        ds.Tables["info"].Columns.Add("TRANGTHAI",typeof(int));
            ds.Tables["info"].Rows.Add(new object[]
            {
                OBienBanHoiChan.ID,
                OBienBanHoiChan.CABENHID,
                OBienBanHoiChan.BATDAUHOICHANVAO,
                OBienBanHoiChan.KETTHUCHOICHANVAO,
                OBienBanHoiChan.DIADIEMHOICHAN,
                OBienBanHoiChan.YKIENKHAMBENH,
                OBienBanHoiChan.YKIENCANLAMSANG,
                OBienBanHoiChan.YKIENCHANDOAN,
                OBienBanHoiChan.YKIENDIEUTRI,
                OBienBanHoiChan.YKIENKHAC,
                OBienBanHoiChan.THUKY,
                OBienBanHoiChan.CHUTRIHOICHAN,
                OBienBanHoiChan.TAOBOI,
                OBienBanHoiChan.TAOVAO,
                OBienBanHoiChan.TRANGTHAI
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
