using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class ChatLuongHoatDongTtbCls 
    { 
        public string ID; 
        public string TRANGTHIETBITRUYENHINHTTMA; 
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID;
        public string TRANGTHIETBITRUYENHINHTTCHAMA;
        public int? DANHGIA;
        public int STT;
        public enum eDanhGia
        {
            RatTot = 0,
            Tot = 1,
            BinhThuong = 2,
            Kem = 3,
            RatKem = 4
        }
    }
}

public class ChatLuongHoatDongTtbParser 
{ 
    public static ChatLuongHoatDongTtbCls CreateInstance() 
    { 
        ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb = new ChatLuongHoatDongTtbCls(); 
        return OChatLuongHoatDongTtb; 
    } 


    public static ChatLuongHoatDongTtbCls ParseFromDataRow(DataRow dr) 
    { 
        ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb = new ChatLuongHoatDongTtbCls(); 
        OChatLuongHoatDongTtb.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OChatLuongHoatDongTtb.TRANGTHIETBITRUYENHINHTTMA = CoreXmlUtility.GetString(dr, "TRANGTHIETBITRUYENHINHTTMA", true);
        OChatLuongHoatDongTtb.PHIEUDANHGIACHATLUONGDAOTAO_ID = CoreXmlUtility.GetString(dr, "PHIEUDANHGIACHATLUONGDAOTAO_ID", true);
        OChatLuongHoatDongTtb.TRANGTHIETBITRUYENHINHTTCHAMA = CoreXmlUtility.GetString(dr, "TRANGTHIETBITRUYENHINHTTCHAMA", true);
        OChatLuongHoatDongTtb.DANHGIA = CoreXmlUtility.GetIntOrNull(dr, "DANHGIA", true);
        OChatLuongHoatDongTtb.STT = CoreXmlUtility.GetInt(dr, "STT", true);
        return OChatLuongHoatDongTtb;
    }

    public static ChatLuongHoatDongTtbCls[] ParseFromDataTable(DataTable dtTable) 
    {
        ChatLuongHoatDongTtbCls[] ChatLuongHoatDongTtbs = new ChatLuongHoatDongTtbCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            ChatLuongHoatDongTtbs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return ChatLuongHoatDongTtbs;
    }


    public static ChatLuongHoatDongTtbCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        ChatLuongHoatDongTtbCls[] ChatLuongHoatDongTtbs = ParseFromDataTable(ds.Tables[0]);
        return ChatLuongHoatDongTtbs;
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

    public static ChatLuongHoatDongTtbCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OChatLuongHoatDongTtb;
    }


    public static XmlCls GetXml(ChatLuongHoatDongTtbCls[] ChatLuongHoatDongTtbs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("TRANGTHIETBITRUYENHINHTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("TRANGTHIETBITRUYENHINHTTCHAMA");
        ds.Tables["info"].Columns.Add("DANHGIA",typeof(int?));
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        for (int iIndex = 0; iIndex < ChatLuongHoatDongTtbs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                ChatLuongHoatDongTtbs[iIndex].ID,
                ChatLuongHoatDongTtbs[iIndex].TRANGTHIETBITRUYENHINHTTMA,
                ChatLuongHoatDongTtbs[iIndex].PHIEUDANHGIACHATLUONGDAOTAO_ID,
                ChatLuongHoatDongTtbs[iIndex].TRANGTHIETBITRUYENHINHTTCHAMA,
                ChatLuongHoatDongTtbs[iIndex].DANHGIA,
                ChatLuongHoatDongTtbs[iIndex].STT
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(ChatLuongHoatDongTtbCls OChatLuongHoatDongTtb)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("TRANGTHIETBITRUYENHINHTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("TRANGTHIETBITRUYENHINHTTCHAMA");
        ds.Tables["info"].Columns.Add("DANHGIA",typeof(int?));
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
            {
                OChatLuongHoatDongTtb.ID,
                OChatLuongHoatDongTtb.TRANGTHIETBITRUYENHINHTTMA,
                OChatLuongHoatDongTtb.PHIEUDANHGIACHATLUONGDAOTAO_ID,
                OChatLuongHoatDongTtb.TRANGTHIETBITRUYENHINHTTCHAMA,
                OChatLuongHoatDongTtb.DANHGIA,
                OChatLuongHoatDongTtb.STT
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
