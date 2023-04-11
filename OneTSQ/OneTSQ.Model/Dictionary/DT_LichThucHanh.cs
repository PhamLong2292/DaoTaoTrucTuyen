using System;
using System.Data;
using OneTSQ.Model;
using OneMES3.SYS.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichThucHanhCls
    {
        public string ID;
        public string KHOAHOC_ID;
        public DateTime? BATDAU;
        public DateTime? KETTHUC;
        public string DIADIEM = "";
        public string NHOM = "";
        public string PTCHUYENMON_ID = "";
        public string LANHDAO_ID = "";
        public string NGUOITAO_ID = "";
        public DateTime NGAYTAO;
        public string NGUOISUA_ID = "";
        public DateTime? NGAYSUA;
    }
}

public class DT_LichThucHanhParser
{
    public static DT_LichThucHanhCls CreateInstance()
    {
        DT_LichThucHanhCls ODT_LichThucHanh = new DT_LichThucHanhCls();
        return ODT_LichThucHanh;
    }


    public static DT_LichThucHanhCls ParseFromDataRow(DataRow dr)
    {
        DT_LichThucHanhCls ODT_LichThucHanh = new DT_LichThucHanhCls();
        ODT_LichThucHanh.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_LichThucHanh.KHOAHOC_ID = CoreXmlUtility.GetString(dr, "KHOAHOC_ID", true);
        ODT_LichThucHanh.BATDAU = CoreXmlUtility.GetDateOrNull(dr, "BATDAU", true);
        ODT_LichThucHanh.KETTHUC = CoreXmlUtility.GetDateOrNull(dr, "KETTHUC", true);
        ODT_LichThucHanh.DIADIEM = CoreXmlUtility.GetString(dr, "DIADIEM", true);
        ODT_LichThucHanh.NHOM = CoreXmlUtility.GetString(dr, "NHOM", true);
        ODT_LichThucHanh.PTCHUYENMON_ID = CoreXmlUtility.GetString(dr, "PTCHUYENMON_ID", true);
        ODT_LichThucHanh.LANHDAO_ID = CoreXmlUtility.GetString(dr, "LANHDAO_ID", true);
        ODT_LichThucHanh.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_LichThucHanh.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        ODT_LichThucHanh.NGUOISUA_ID = CoreXmlUtility.GetString(dr, "NGUOISUA_ID", true);
        ODT_LichThucHanh.NGAYSUA = CoreXmlUtility.GetDateOrNull(dr, "NGAYSUA", true);
        return ODT_LichThucHanh;
    }

    public static DT_LichThucHanhCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_LichThucHanhCls[] DT_LichThucHanhs = new DT_LichThucHanhCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_LichThucHanhs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_LichThucHanhs;
    }


    public static DT_LichThucHanhCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_LichThucHanhCls[] DT_LichThucHanhs = ParseFromDataTable(ds.Tables[0]);
        return DT_LichThucHanhs;
    }


    public static DT_LichThucHanhCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichThucHanhCls ODT_LichThucHanh = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichThucHanh;
    }


    public static XmlCls GetXml(DT_LichThucHanhCls[] DT_LichThucHanhs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("KHOAHOC_ID");
        ds.Tables["info"].Columns.Add("BATDAU", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEM");
        ds.Tables["info"].Columns.Add("NHOM");
        ds.Tables["info"].Columns.Add("PTCHUYENMON_ID");
        ds.Tables["info"].Columns.Add("LANHDAO_ID");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        for (int iIndex = 0; iIndex < DT_LichThucHanhs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_LichThucHanhs[iIndex].ID,
                DT_LichThucHanhs[iIndex].KHOAHOC_ID,
                DT_LichThucHanhs[iIndex].BATDAU,
                DT_LichThucHanhs[iIndex].KETTHUC,
                DT_LichThucHanhs[iIndex].DIADIEM,
                DT_LichThucHanhs[iIndex].NHOM,
                DT_LichThucHanhs[iIndex].PTCHUYENMON_ID,
                DT_LichThucHanhs[iIndex].LANHDAO_ID,
                DT_LichThucHanhs[iIndex].NGUOITAO_ID,
                DT_LichThucHanhs[iIndex].NGAYTAO,
                DT_LichThucHanhs[iIndex].NGUOISUA_ID,
                DT_LichThucHanhs[iIndex].NGAYSUA
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_LichThucHanhCls ODT_LichThucHanh)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("KHOAHOC_ID");
        ds.Tables["info"].Columns.Add("BATDAU", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEM");
        ds.Tables["info"].Columns.Add("NHOM");
        ds.Tables["info"].Columns.Add("PTCHUYENMON_ID");
        ds.Tables["info"].Columns.Add("LANHDAO_ID");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_LichThucHanh.ID,
                ODT_LichThucHanh.KHOAHOC_ID,
                ODT_LichThucHanh.BATDAU,
                ODT_LichThucHanh.KETTHUC,
                ODT_LichThucHanh.DIADIEM,
                ODT_LichThucHanh.NHOM,
                ODT_LichThucHanh.PTCHUYENMON_ID,
                ODT_LichThucHanh.LANHDAO_ID,
                ODT_LichThucHanh.NGUOITAO_ID,
                ODT_LichThucHanh.NGAYTAO,
                ODT_LichThucHanh.NGUOISUA_ID,
                ODT_LichThucHanh.NGAYSUA
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
