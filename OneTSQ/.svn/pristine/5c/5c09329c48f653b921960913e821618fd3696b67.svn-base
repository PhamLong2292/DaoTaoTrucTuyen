using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class YKienChuyenGiaCls 
    { 
        public string ID; 
        public string YKIENCHUYENGIAID = "";
        public string CABENHID = "";
        public string NOIDUNG = "";
        public string BACSYIDS = "";
        public string BACSY = "";
        public string DONVI = ""; 
        public DateTime? THOIGIAN;
    }
}

public class YKienChuyenGiaParser 
{ 
    public static YKienChuyenGiaCls CreateInstance() 
    { 
        YKienChuyenGiaCls OYKienChuyenGia = new YKienChuyenGiaCls(); 
        return OYKienChuyenGia; 
    } 


    public static YKienChuyenGiaCls ParseFromDataRow(DataRow dr) 
    { 
        YKienChuyenGiaCls OYKienChuyenGia = new YKienChuyenGiaCls(); 
        OYKienChuyenGia.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OYKienChuyenGia.YKIENCHUYENGIAID = CoreXmlUtility.GetString(dr, "YKIENCHUYENGIAID", true);
        OYKienChuyenGia.CABENHID = CoreXmlUtility.GetString(dr, "CABENHID", true);
        OYKienChuyenGia.NOIDUNG = CoreXmlUtility.GetString(dr, "NOIDUNG", true);
        OYKienChuyenGia.BACSYIDS = CoreXmlUtility.GetString(dr, "BACSYIDS", true);
        OYKienChuyenGia.BACSY = CoreXmlUtility.GetString(dr, "BACSY", true);
        OYKienChuyenGia.DONVI = CoreXmlUtility.GetString(dr, "DONVI", true);
        OYKienChuyenGia.THOIGIAN = dr["THOIGIAN"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "THOIGIAN", true);
        return OYKienChuyenGia;
    }

    public static YKienChuyenGiaCls[] ParseFromDataTable(DataTable dtTable) 
    {
        YKienChuyenGiaCls[] YKienChuyenGias = new YKienChuyenGiaCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            YKienChuyenGias[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return YKienChuyenGias;
    }


    public static YKienChuyenGiaCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        YKienChuyenGiaCls[] YKienChuyenGias = ParseFromDataTable(ds.Tables[0]);
        return YKienChuyenGias;
    }


    public static YKienChuyenGiaCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        YKienChuyenGiaCls OYKienChuyenGia = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OYKienChuyenGia;
    }


    public static XmlCls GetXml(YKienChuyenGiaCls[] YKienChuyenGias)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("YKIENCHUYENGIAID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("NOIDUNG");
        ds.Tables["info"].Columns.Add("BACSYIDS");
        ds.Tables["info"].Columns.Add("BACSY");
        ds.Tables["info"].Columns.Add("DONVI");
        ds.Tables["info"].Columns.Add("THOIGIAN",typeof(DateTime?));
        for (int iIndex = 0; iIndex < YKienChuyenGias.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                YKienChuyenGias[iIndex].ID,
                YKienChuyenGias[iIndex].YKIENCHUYENGIAID,
                YKienChuyenGias[iIndex].CABENHID,
                YKienChuyenGias[iIndex].NOIDUNG,
                YKienChuyenGias[iIndex].BACSYIDS,
                YKienChuyenGias[iIndex].BACSY,
                YKienChuyenGias[iIndex].DONVI,
                YKienChuyenGias[iIndex].THOIGIAN
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(YKienChuyenGiaCls OYKienChuyenGia)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("YKIENCHUYENGIAID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("NOIDUNG");
        ds.Tables["info"].Columns.Add("BACSYIDS");
        ds.Tables["info"].Columns.Add("BACSY");
        ds.Tables["info"].Columns.Add("DONVI");
        ds.Tables["info"].Columns.Add("THOIGIAN",typeof(DateTime?));
            ds.Tables["info"].Rows.Add(new object[]
            {
                OYKienChuyenGia.ID,
                OYKienChuyenGia.YKIENCHUYENGIAID,
                OYKienChuyenGia.CABENHID,
                OYKienChuyenGia.NOIDUNG,
                OYKienChuyenGia.BACSYIDS,
                OYKienChuyenGia.BACSY,
                OYKienChuyenGia.DONVI,
                OYKienChuyenGia.THOIGIAN
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
