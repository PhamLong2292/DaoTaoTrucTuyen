using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichLyThuyetCls
    {
        public string ID;
        public DateTime? BATDAU;
        public DateTime? KETTHUC;
        public string DIADIEM = "";
        public string PTCHUYENMON_ID = "";
        public string LANHDAO_ID = "";
        public string NGUOITAO_ID = "";
        public DateTime NGAYTAO;
        public string NGUOISUA_ID = "";
        public DateTime? NGAYSUA;
    }
}

public class DT_LichLyThuyetParser
{
    public static DT_LichLyThuyetCls CreateInstance()
    {
        DT_LichLyThuyetCls ODT_LichLyThuyet = new DT_LichLyThuyetCls();
        return ODT_LichLyThuyet;
    }


    public static DT_LichLyThuyetCls ParseFromDataRow(DataRow dr)
    {
        DT_LichLyThuyetCls ODT_LichLyThuyet = new DT_LichLyThuyetCls();
        ODT_LichLyThuyet.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_LichLyThuyet.BATDAU = CoreXmlUtility.GetDateOrNull(dr, "BATDAU", true);
        ODT_LichLyThuyet.KETTHUC = CoreXmlUtility.GetDateOrNull(dr, "KETTHUC", true);
        ODT_LichLyThuyet.DIADIEM = CoreXmlUtility.GetString(dr, "DIADIEM", true);
        ODT_LichLyThuyet.PTCHUYENMON_ID = CoreXmlUtility.GetString(dr, "PTCHUYENMON_ID", true);
        ODT_LichLyThuyet.LANHDAO_ID = CoreXmlUtility.GetString(dr, "LANHDAO_ID", true);
        ODT_LichLyThuyet.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_LichLyThuyet.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        ODT_LichLyThuyet.NGUOISUA_ID = CoreXmlUtility.GetString(dr, "NGUOISUA_ID", true);
        ODT_LichLyThuyet.NGAYSUA = CoreXmlUtility.GetDateOrNull(dr, "NGAYSUA", true);
        return ODT_LichLyThuyet;
    }

    public static DT_LichLyThuyetCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_LichLyThuyetCls[] DT_LichLyThuyets = new DT_LichLyThuyetCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_LichLyThuyets[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_LichLyThuyets;
    }


    public static DT_LichLyThuyetCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_LichLyThuyetCls[] DT_LichLyThuyets = ParseFromDataTable(ds.Tables[0]);
        return DT_LichLyThuyets;
    }


    public static DT_LichLyThuyetCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichLyThuyetCls ODT_LichLyThuyet = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichLyThuyet;
    }


    public static XmlCls GetXml(DT_LichLyThuyetCls[] DT_LichLyThuyets)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BATDAU", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEM");
        ds.Tables["info"].Columns.Add("PTCHUYENMON_ID");
        ds.Tables["info"].Columns.Add("LANHDAO_ID");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        for (int iIndex = 0; iIndex < DT_LichLyThuyets.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_LichLyThuyets[iIndex].ID,
                DT_LichLyThuyets[iIndex].BATDAU,
                DT_LichLyThuyets[iIndex].KETTHUC,
                DT_LichLyThuyets[iIndex].DIADIEM,
                DT_LichLyThuyets[iIndex].PTCHUYENMON_ID,
                DT_LichLyThuyets[iIndex].LANHDAO_ID,
                DT_LichLyThuyets[iIndex].NGUOITAO_ID,
                DT_LichLyThuyets[iIndex].NGAYTAO,
                DT_LichLyThuyets[iIndex].NGUOISUA_ID,
                DT_LichLyThuyets[iIndex].NGAYSUA
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_LichLyThuyetCls ODT_LichLyThuyet)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BATDAU", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEM");
        ds.Tables["info"].Columns.Add("PTCHUYENMON_ID");
        ds.Tables["info"].Columns.Add("LANHDAO_ID");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_LichLyThuyet.ID,
                ODT_LichLyThuyet.BATDAU,
                ODT_LichLyThuyet.KETTHUC,
                ODT_LichLyThuyet.DIADIEM,
                ODT_LichLyThuyet.PTCHUYENMON_ID,
                ODT_LichLyThuyet.LANHDAO_ID,
                ODT_LichLyThuyet.NGUOITAO_ID,
                ODT_LichLyThuyet.NGAYTAO,
                ODT_LichLyThuyet.NGUOISUA_ID,
                ODT_LichLyThuyet.NGAYSUA
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
