using System;
using System.Data;
using OneTSQ.Model;
using OneMES3.SYS.Core.Model;

namespace OneTSQ.Model
{
    public class DT_KeHoachLopCls
    {
        public string ID;
        public DateTime? BATDAU;
        public DateTime? KETTHUC;
        public DateTime? THOIGIANTIEPDON;
        public string DIADIEMTIEPDON = "";
        public DateTime? BATDAULT;
        public DateTime? KETTHUCLT;
        public string DIADIEMLT = "";
        public DateTime? BATDAUTH;
        public DateTime? KETTHUCTH;
        public string DIADIEMTH = "";
        public int? SOLUONGNHOMTH;
        public int? SOHVTRONGNHOMTH;
        public DateTime? THOIGIANDANHGIATDT;
        public string DIADIEMDANHGIATDT = "";
        public DateTime? THOIGIANGIAIDAPTHACMAC;
        public string DIADIEMGIAIDAPTHACMAC = "";
        public DateTime? BATDAUTHILT;
        public DateTime? KETTHUCTHILT;
        public string DIADIEMTHILT = "";
        public DateTime? BATDAUTHIVD;
        public DateTime? KETTHUCTHIVD;
        public string DIADIEMTHIVD = "";
        public DateTime? BATDAUTHITH;
        public DateTime? KETTHUCTHITH;
        public string DIADIEMTHITH = "";
        public DateTime? THOIGIANBEGIANG;
        public string DIADIEMBEGIANG = "";
        public string LANHDAO_ID = "";
        public string NGUOILAP = "";
        public string NGUOITAO_ID = "";
        public DateTime NGAYTAO;
        public string NGUOISUA_ID = "";
        public DateTime? NGAYSUA;
    }
}

public class DT_KeHoachLopParser
{
    public static DT_KeHoachLopCls CreateInstance()
    {
        DT_KeHoachLopCls ODT_KeHoachLop = new DT_KeHoachLopCls();
        return ODT_KeHoachLop;
    }


    public static DT_KeHoachLopCls ParseFromDataRow(DataRow dr)
    {
        DT_KeHoachLopCls ODT_KeHoachLop = new DT_KeHoachLopCls();
        ODT_KeHoachLop.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_KeHoachLop.BATDAU = CoreXmlUtility.GetDateOrNull(dr, "BATDAU", true);
        ODT_KeHoachLop.KETTHUC = CoreXmlUtility.GetDateOrNull(dr, "KETTHUC", true);
        ODT_KeHoachLop.THOIGIANTIEPDON = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANTIEPDON", true);
        ODT_KeHoachLop.DIADIEMTIEPDON = CoreXmlUtility.GetString(dr, "DIADIEMTIEPDON", true);
        ODT_KeHoachLop.BATDAULT = CoreXmlUtility.GetDateOrNull(dr, "BATDAULT", true);
        ODT_KeHoachLop.KETTHUCLT = CoreXmlUtility.GetDateOrNull(dr, "KETTHUCLT", true);
        ODT_KeHoachLop.DIADIEMLT = CoreXmlUtility.GetString(dr, "DIADIEMLT", true);
        ODT_KeHoachLop.BATDAUTH = CoreXmlUtility.GetDateOrNull(dr, "BATDAUTH", true);
        ODT_KeHoachLop.KETTHUCTH = CoreXmlUtility.GetDateOrNull(dr, "KETTHUCTH", true);
        ODT_KeHoachLop.DIADIEMTH = CoreXmlUtility.GetString(dr, "DIADIEMTH", true);
        ODT_KeHoachLop.SOLUONGNHOMTH = CoreXmlUtility.GetIntOrNull(dr, "SOLUONGNHOMTH", true);
        ODT_KeHoachLop.SOHVTRONGNHOMTH = CoreXmlUtility.GetIntOrNull(dr, "SOHVTRONGNHOMTH", true);
        ODT_KeHoachLop.THOIGIANDANHGIATDT = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANDANHGIATDT", true);
        ODT_KeHoachLop.DIADIEMDANHGIATDT = CoreXmlUtility.GetString(dr, "DIADIEMDANHGIATDT", true);
        ODT_KeHoachLop.THOIGIANGIAIDAPTHACMAC = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANGIAIDAPTHACMAC", true);
        ODT_KeHoachLop.DIADIEMGIAIDAPTHACMAC = CoreXmlUtility.GetString(dr, "DIADIEMGIAIDAPTHACMAC", true);
        ODT_KeHoachLop.BATDAUTHILT = CoreXmlUtility.GetDateOrNull(dr, "BATDAUTHILT", true);
        ODT_KeHoachLop.KETTHUCTHILT = CoreXmlUtility.GetDateOrNull(dr, "KETTHUCTHILT", true);
        ODT_KeHoachLop.DIADIEMTHILT = CoreXmlUtility.GetString(dr, "DIADIEMTHILT", true);
        ODT_KeHoachLop.BATDAUTHIVD = CoreXmlUtility.GetDateOrNull(dr, "BATDAUTHIVD", true);
        ODT_KeHoachLop.KETTHUCTHIVD = CoreXmlUtility.GetDateOrNull(dr, "KETTHUCTHIVD", true);
        ODT_KeHoachLop.DIADIEMTHIVD = CoreXmlUtility.GetString(dr, "DIADIEMTHIVD", true);
        ODT_KeHoachLop.BATDAUTHITH = CoreXmlUtility.GetDateOrNull(dr, "BATDAUTHITH", true);
        ODT_KeHoachLop.KETTHUCTHITH = CoreXmlUtility.GetDateOrNull(dr, "KETTHUCTHITH", true);
        ODT_KeHoachLop.DIADIEMTHITH = CoreXmlUtility.GetString(dr, "DIADIEMTHITH", true);
        ODT_KeHoachLop.THOIGIANBEGIANG = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANBEGIANG", true);
        ODT_KeHoachLop.DIADIEMBEGIANG = CoreXmlUtility.GetString(dr, "DIADIEMBEGIANG", true);
        ODT_KeHoachLop.LANHDAO_ID = CoreXmlUtility.GetString(dr, "LANHDAO_ID", true);
        ODT_KeHoachLop.NGUOILAP = CoreXmlUtility.GetString(dr, "NGUOILAP", true);
        ODT_KeHoachLop.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_KeHoachLop.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        ODT_KeHoachLop.NGUOISUA_ID = CoreXmlUtility.GetString(dr, "NGUOISUA_ID", true);
        ODT_KeHoachLop.NGAYSUA = CoreXmlUtility.GetDateOrNull(dr, "NGAYSUA", true);
        return ODT_KeHoachLop;
    }

    public static DT_KeHoachLopCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_KeHoachLopCls[] DT_KeHoachLops = new DT_KeHoachLopCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_KeHoachLops[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_KeHoachLops;
    }


    public static DT_KeHoachLopCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_KeHoachLopCls[] DT_KeHoachLops = ParseFromDataTable(ds.Tables[0]);
        return DT_KeHoachLops;
    }


    public static DT_KeHoachLopCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_KeHoachLopCls ODT_KeHoachLop = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_KeHoachLop;
    }


    public static XmlCls GetXml(DT_KeHoachLopCls[] DT_KeHoachLops)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BATDAU", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("THOIGIANTIEPDON", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMTIEPDON");
        ds.Tables["info"].Columns.Add("BATDAULT", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCLT", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMLT");
        ds.Tables["info"].Columns.Add("BATDAUTH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCTH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMTH");
        ds.Tables["info"].Columns.Add("SOLUONGNHOMTH", typeof(int?));
        ds.Tables["info"].Columns.Add("SOHVTRONGNHOMTH", typeof(int?));
        ds.Tables["info"].Columns.Add("THOIGIANDANHGIATDT", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMDANHGIATDT");
        ds.Tables["info"].Columns.Add("THOIGIANGIAIDAPTHACMAC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMGIAIDAPTHACMAC");
        ds.Tables["info"].Columns.Add("BATDAUTHILT", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCTHILT", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMTHILT");
        ds.Tables["info"].Columns.Add("BATDAUTHIVD", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCTHIVD", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMTHIVD");
        ds.Tables["info"].Columns.Add("BATDAUTHITH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCTHITH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMTHITH");
        ds.Tables["info"].Columns.Add("THOIGIANBEGIANG", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMBEGIANG");
        ds.Tables["info"].Columns.Add("LANHDAO_ID");
        ds.Tables["info"].Columns.Add("NGUOILAP");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        for (int iIndex = 0; iIndex < DT_KeHoachLops.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_KeHoachLops[iIndex].ID,
                DT_KeHoachLops[iIndex].BATDAU,
                DT_KeHoachLops[iIndex].KETTHUC,
                DT_KeHoachLops[iIndex].THOIGIANTIEPDON,
                DT_KeHoachLops[iIndex].DIADIEMTIEPDON,
                DT_KeHoachLops[iIndex].BATDAULT,
                DT_KeHoachLops[iIndex].KETTHUCLT,
                DT_KeHoachLops[iIndex].DIADIEMLT,
                DT_KeHoachLops[iIndex].BATDAUTH,
                DT_KeHoachLops[iIndex].KETTHUCTH,
                DT_KeHoachLops[iIndex].DIADIEMTH,
                DT_KeHoachLops[iIndex].SOLUONGNHOMTH,
                DT_KeHoachLops[iIndex].SOHVTRONGNHOMTH,
                DT_KeHoachLops[iIndex].THOIGIANDANHGIATDT,
                DT_KeHoachLops[iIndex].DIADIEMDANHGIATDT,
                DT_KeHoachLops[iIndex].THOIGIANGIAIDAPTHACMAC,
                DT_KeHoachLops[iIndex].DIADIEMGIAIDAPTHACMAC,
                DT_KeHoachLops[iIndex].BATDAUTHILT,
                DT_KeHoachLops[iIndex].KETTHUCTHILT,
                DT_KeHoachLops[iIndex].DIADIEMTHILT,
                DT_KeHoachLops[iIndex].BATDAUTHIVD,
                DT_KeHoachLops[iIndex].KETTHUCTHIVD,
                DT_KeHoachLops[iIndex].DIADIEMTHIVD,
                DT_KeHoachLops[iIndex].BATDAUTHITH,
                DT_KeHoachLops[iIndex].KETTHUCTHITH,
                DT_KeHoachLops[iIndex].DIADIEMTHITH,
                DT_KeHoachLops[iIndex].THOIGIANBEGIANG,
                DT_KeHoachLops[iIndex].DIADIEMBEGIANG,
                DT_KeHoachLops[iIndex].LANHDAO_ID,
                DT_KeHoachLops[iIndex].NGUOILAP,
                DT_KeHoachLops[iIndex].NGUOITAO_ID,
                DT_KeHoachLops[iIndex].NGAYTAO,
                DT_KeHoachLops[iIndex].NGUOISUA_ID,
                DT_KeHoachLops[iIndex].NGAYSUA
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_KeHoachLopCls ODT_KeHoachLop)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BATDAU", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("THOIGIANTIEPDON", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMTIEPDON");
        ds.Tables["info"].Columns.Add("BATDAULT", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCLT", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMLT");
        ds.Tables["info"].Columns.Add("BATDAUTH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCTH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMTH");
        ds.Tables["info"].Columns.Add("SOLUONGNHOMTH", typeof(int?));
        ds.Tables["info"].Columns.Add("SOHVTRONGNHOMTH", typeof(int?));
        ds.Tables["info"].Columns.Add("THOIGIANDANHGIATDT", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMDANHGIATDT");
        ds.Tables["info"].Columns.Add("THOIGIANGIAIDAPTHACMAC", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMGIAIDAPTHACMAC");
        ds.Tables["info"].Columns.Add("BATDAUTHILT", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCTHILT", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMTHILT");
        ds.Tables["info"].Columns.Add("BATDAUTHIVD", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCTHIVD", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMTHIVD");
        ds.Tables["info"].Columns.Add("BATDAUTHITH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCTHITH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMTHITH");
        ds.Tables["info"].Columns.Add("THOIGIANBEGIANG", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIADIEMBEGIANG");
        ds.Tables["info"].Columns.Add("LANHDAO_ID");
        ds.Tables["info"].Columns.Add("NGUOILAP");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_KeHoachLop.ID,
                ODT_KeHoachLop.BATDAU,
                ODT_KeHoachLop.KETTHUC,
                ODT_KeHoachLop.THOIGIANTIEPDON,
                ODT_KeHoachLop.DIADIEMTIEPDON,
                ODT_KeHoachLop.BATDAULT,
                ODT_KeHoachLop.KETTHUCLT,
                ODT_KeHoachLop.DIADIEMLT,
                ODT_KeHoachLop.BATDAUTH,
                ODT_KeHoachLop.KETTHUCTH,
                ODT_KeHoachLop.DIADIEMTH,
                ODT_KeHoachLop.SOLUONGNHOMTH,
                ODT_KeHoachLop.SOHVTRONGNHOMTH,
                ODT_KeHoachLop.THOIGIANDANHGIATDT,
                ODT_KeHoachLop.DIADIEMDANHGIATDT,
                ODT_KeHoachLop.THOIGIANGIAIDAPTHACMAC,
                ODT_KeHoachLop.DIADIEMGIAIDAPTHACMAC,
                ODT_KeHoachLop.BATDAUTHILT,
                ODT_KeHoachLop.KETTHUCTHILT,
                ODT_KeHoachLop.DIADIEMTHILT,
                ODT_KeHoachLop.BATDAUTHIVD,
                ODT_KeHoachLop.KETTHUCTHIVD,
                ODT_KeHoachLop.DIADIEMTHIVD,
                ODT_KeHoachLop.BATDAUTHITH,
                ODT_KeHoachLop.KETTHUCTHITH,
                ODT_KeHoachLop.DIADIEMTHITH,
                ODT_KeHoachLop.THOIGIANBEGIANG,
                ODT_KeHoachLop.DIADIEMBEGIANG,
                ODT_KeHoachLop.LANHDAO_ID,
                ODT_KeHoachLop.NGUOILAP,
                ODT_KeHoachLop.NGUOITAO_ID,
                ODT_KeHoachLop.NGAYTAO,
                ODT_KeHoachLop.NGUOISUA_ID,
                ODT_KeHoachLop.NGAYSUA
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
