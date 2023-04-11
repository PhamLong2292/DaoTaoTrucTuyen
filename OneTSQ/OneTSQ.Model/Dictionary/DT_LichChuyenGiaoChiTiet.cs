using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichChuyenGiaoChiTietCls
    {
        public string ID;
        public string LICHCHUYENGIAO_ID = "";
        public DateTime THOIGIAN;
        public string NOIDUNG = "";
        public int? SOCAHUONGDAN;
        public int? SOCAHOTRO;
        public string CANBOS = "";
        public string NGUOITAO_ID = "";
        public DateTime NGAYTAO;
        public string NGUOISUA_ID = "";
        public DateTime? NGAYSUA;
    }
}

public class DT_LichChuyenGiaoChiTietParser
{
    public static DT_LichChuyenGiaoChiTietCls CreateInstance()
    {
        DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet = new DT_LichChuyenGiaoChiTietCls();
        return ODT_LichChuyenGiaoChiTiet;
    }


    public static DT_LichChuyenGiaoChiTietCls ParseFromDataRow(DataRow dr)
    {
        DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet = new DT_LichChuyenGiaoChiTietCls();
        ODT_LichChuyenGiaoChiTiet.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_LichChuyenGiaoChiTiet.LICHCHUYENGIAO_ID = CoreXmlUtility.GetString(dr, "LICHCHUYENGIAO_ID", true);
        ODT_LichChuyenGiaoChiTiet.THOIGIAN = CoreXmlUtility.GetDate(dr, "THOIGIAN", true);
        ODT_LichChuyenGiaoChiTiet.NOIDUNG = CoreXmlUtility.GetString(dr, "NOIDUNG", true);
        ODT_LichChuyenGiaoChiTiet.SOCAHUONGDAN = CoreXmlUtility.GetIntOrNull(dr, "SOCAHUONGDAN", true);
        ODT_LichChuyenGiaoChiTiet.SOCAHOTRO = CoreXmlUtility.GetIntOrNull(dr, "SOCAHOTRO", true);
        ODT_LichChuyenGiaoChiTiet.CANBOS = CoreXmlUtility.GetString(dr, "CANBOS", true);
        ODT_LichChuyenGiaoChiTiet.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_LichChuyenGiaoChiTiet.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        ODT_LichChuyenGiaoChiTiet.NGUOISUA_ID = CoreXmlUtility.GetString(dr, "NGUOISUA_ID", true);
        ODT_LichChuyenGiaoChiTiet.NGAYSUA = CoreXmlUtility.GetDateOrNull(dr, "NGAYSUA", true);
        return ODT_LichChuyenGiaoChiTiet;
    }

    public static DT_LichChuyenGiaoChiTietCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_LichChuyenGiaoChiTietCls[] DT_LichChuyenGiaoChiTiets = new DT_LichChuyenGiaoChiTietCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_LichChuyenGiaoChiTiets[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_LichChuyenGiaoChiTiets;
    }


    public static DT_LichChuyenGiaoChiTietCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_LichChuyenGiaoChiTietCls[] DT_LichChuyenGiaoChiTiets = ParseFromDataTable(ds.Tables[0]);
        return DT_LichChuyenGiaoChiTiets;
    }


    public static DT_LichChuyenGiaoChiTietCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichChuyenGiaoChiTiet;
    }


    public static XmlCls GetXml(DT_LichChuyenGiaoChiTietCls[] DT_LichChuyenGiaoChiTiets)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHCHUYENGIAO_ID");
        ds.Tables["info"].Columns.Add("THOIGIAN", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NOIDUNG");
        ds.Tables["info"].Columns.Add("SOCAHUONGDAN", typeof(int?));
        ds.Tables["info"].Columns.Add("SOCAHOTRO", typeof(int?));
        ds.Tables["info"].Columns.Add("CANBOS");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        for (int iIndex = 0; iIndex < DT_LichChuyenGiaoChiTiets.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_LichChuyenGiaoChiTiets[iIndex].ID,
                DT_LichChuyenGiaoChiTiets[iIndex].LICHCHUYENGIAO_ID,
                DT_LichChuyenGiaoChiTiets[iIndex].THOIGIAN,
                DT_LichChuyenGiaoChiTiets[iIndex].NOIDUNG,
                DT_LichChuyenGiaoChiTiets[iIndex].SOCAHUONGDAN,
                DT_LichChuyenGiaoChiTiets[iIndex].SOCAHOTRO,
                DT_LichChuyenGiaoChiTiets[iIndex].CANBOS,
                DT_LichChuyenGiaoChiTiets[iIndex].NGUOITAO_ID,
                DT_LichChuyenGiaoChiTiets[iIndex].NGAYTAO,
                DT_LichChuyenGiaoChiTiets[iIndex].NGUOISUA_ID,
                DT_LichChuyenGiaoChiTiets[iIndex].NGAYSUA
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHCHUYENGIAO_ID");
        ds.Tables["info"].Columns.Add("THOIGIAN", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NOIDUNG");
        ds.Tables["info"].Columns.Add("SOCAHUONGDAN", typeof(int?));
        ds.Tables["info"].Columns.Add("SOCAHOTRO", typeof(int?));
        ds.Tables["info"].Columns.Add("CANBOS");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_LichChuyenGiaoChiTiet.ID,
                ODT_LichChuyenGiaoChiTiet.LICHCHUYENGIAO_ID,
                ODT_LichChuyenGiaoChiTiet.THOIGIAN,
                ODT_LichChuyenGiaoChiTiet.NOIDUNG,
                ODT_LichChuyenGiaoChiTiet.SOCAHUONGDAN,
                ODT_LichChuyenGiaoChiTiet.SOCAHOTRO,
                ODT_LichChuyenGiaoChiTiet.CANBOS,
                ODT_LichChuyenGiaoChiTiet.NGUOITAO_ID,
                ODT_LichChuyenGiaoChiTiet.NGAYTAO,
                ODT_LichChuyenGiaoChiTiet.NGUOISUA_ID,
                ODT_LichChuyenGiaoChiTiet.NGAYSUA
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
