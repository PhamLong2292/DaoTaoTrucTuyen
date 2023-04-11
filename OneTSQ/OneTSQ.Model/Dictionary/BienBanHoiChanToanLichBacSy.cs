using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class BienBanHoiChanToanLichBacSyCls
    {
        public string ID;
        public string LICHHOICHANID; 
        public string BACSYID;
        public int? STT;
        public int? VANGMAT;
        public int? SONGUOITHAMDU;
        public string DONVICONGTACMA;
        public int ISCHUYENGIA;
        public string GHICHU; 
    }
}

public class BienBanHoiChanToanLichBacSyParser 
{ 
    public static BienBanHoiChanToanLichBacSyCls CreateInstance() 
    { 
        BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy = new BienBanHoiChanToanLichBacSyCls(); 
        return OBienBanHoiChanToanLichBacSy; 
    } 


    public static BienBanHoiChanToanLichBacSyCls ParseFromDataRow(DataRow dr) 
    { 
        BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy = new BienBanHoiChanToanLichBacSyCls(); 
        OBienBanHoiChanToanLichBacSy.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OBienBanHoiChanToanLichBacSy.LICHHOICHANID = CoreXmlUtility.GetString(dr, "LICHHOICHANID", true);
        OBienBanHoiChanToanLichBacSy.BACSYID = CoreXmlUtility.GetString(dr, "BACSYID", true);
        OBienBanHoiChanToanLichBacSy.VANGMAT = CoreXmlUtility.GetIntOrNull(dr, "VANGMAT", true);
        OBienBanHoiChanToanLichBacSy.SONGUOITHAMDU = CoreXmlUtility.GetIntOrNull(dr, "SONGUOITHAMDU", true);
        OBienBanHoiChanToanLichBacSy.DONVICONGTACMA = CoreXmlUtility.GetString(dr, "DONVICONGTACMA", true);
        OBienBanHoiChanToanLichBacSy.ISCHUYENGIA = CoreXmlUtility.GetInt(dr, "ISCHUYENGIA", true);
        OBienBanHoiChanToanLichBacSy.GHICHU = CoreXmlUtility.GetString(dr, "GHICHU", true);
        return OBienBanHoiChanToanLichBacSy;
    }

    public static BienBanHoiChanToanLichBacSyCls[] ParseFromDataTable(DataTable dtTable) 
    {
        BienBanHoiChanToanLichBacSyCls[] BienBanHoiChanToanLichBacSys = new BienBanHoiChanToanLichBacSyCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            BienBanHoiChanToanLichBacSys[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return BienBanHoiChanToanLichBacSys;
    }


    public static BienBanHoiChanToanLichBacSyCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        BienBanHoiChanToanLichBacSyCls[] BienBanHoiChanToanLichBacSys = ParseFromDataTable(ds.Tables[0]);
        return BienBanHoiChanToanLichBacSys;
    }

public static long CountFromDataTable(DataTable dtTable)
{
    if (dtTable != null && dtTable.Rows.Count > 0)
    {
        return Int64.Parse(dtTable.Rows[0][0].ToString());
    }
    else
        return 0;
}

    public static BienBanHoiChanToanLichBacSyCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBienBanHoiChanToanLichBacSy;
    }


    public static XmlCls GetXml(BienBanHoiChanToanLichBacSyCls[] BienBanHoiChanToanLichBacSys)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("BACSYID");
        ds.Tables["info"].Columns.Add("VANGMAT", typeof(int?));
        ds.Tables["info"].Columns.Add("SONGUOITHAMDU", typeof(int?));
        ds.Tables["info"].Columns.Add("DONVICONGTACMA");
        ds.Tables["info"].Columns.Add("ISCHUYENGIA", typeof(int));
        ds.Tables["info"].Columns.Add("GHICHU");
        for (int iIndex = 0; iIndex < BienBanHoiChanToanLichBacSys.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                BienBanHoiChanToanLichBacSys[iIndex].ID,
                BienBanHoiChanToanLichBacSys[iIndex].LICHHOICHANID,
                BienBanHoiChanToanLichBacSys[iIndex].BACSYID,
                BienBanHoiChanToanLichBacSys[iIndex].VANGMAT,
                BienBanHoiChanToanLichBacSys[iIndex].SONGUOITHAMDU,
                BienBanHoiChanToanLichBacSys[iIndex].DONVICONGTACMA,
                BienBanHoiChanToanLichBacSys[iIndex].ISCHUYENGIA,
                BienBanHoiChanToanLichBacSys[iIndex].GHICHU
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("BACSYID");
        ds.Tables["info"].Columns.Add("VANGMAT", typeof(int?));
        ds.Tables["info"].Columns.Add("SONGUOITHAMDU", typeof(int?));
        ds.Tables["info"].Columns.Add("DONVICONGTACMA");
        ds.Tables["info"].Columns.Add("ISCHUYENGIA", typeof(int));
        ds.Tables["info"].Columns.Add("GHICHU");
            ds.Tables["info"].Rows.Add(new object[]
            {
                OBienBanHoiChanToanLichBacSy.ID,
                OBienBanHoiChanToanLichBacSy.LICHHOICHANID,
                OBienBanHoiChanToanLichBacSy.BACSYID,
                OBienBanHoiChanToanLichBacSy.VANGMAT,
                OBienBanHoiChanToanLichBacSy.SONGUOITHAMDU,
                OBienBanHoiChanToanLichBacSy.DONVICONGTACMA,
                OBienBanHoiChanToanLichBacSy.ISCHUYENGIA,
                OBienBanHoiChanToanLichBacSy.GHICHU
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
