using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_DaoTaoVienTruongCls
    {
        public string ID;
        public string TRUONG = "";
        public int? SOHOCVIEN;
        public int? NGUYENTAC;
        public int? CHITIET;
        public string BCTONGKETCONGTACDAOTAO_ID = "";
    }
}

public class DT_DaoTaoVienTruongParser
{
    public static DT_DaoTaoVienTruongCls CreateInstance()
    {
        DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong = new DT_DaoTaoVienTruongCls();
        return ODT_DaoTaoVienTruong;
    }


    public static DT_DaoTaoVienTruongCls ParseFromDataRow(DataRow dr)
    {
        DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong = new DT_DaoTaoVienTruongCls();
        ODT_DaoTaoVienTruong.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_DaoTaoVienTruong.TRUONG = CoreXmlUtility.GetString(dr, "TRUONG", true);
        ODT_DaoTaoVienTruong.SOHOCVIEN = CoreXmlUtility.GetIntOrNull(dr, "SOHOCVIEN", true);
        ODT_DaoTaoVienTruong.NGUYENTAC = CoreXmlUtility.GetIntOrNull(dr, "NGUYENTAC", true);
        ODT_DaoTaoVienTruong.CHITIET = CoreXmlUtility.GetIntOrNull(dr, "CHITIET", true);
        ODT_DaoTaoVienTruong.BCTONGKETCONGTACDAOTAO_ID = CoreXmlUtility.GetString(dr, "BCTONGKETCONGTACDAOTAO_ID", true);
        return ODT_DaoTaoVienTruong;
    }

    public static DT_DaoTaoVienTruongCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_DaoTaoVienTruongCls[] DT_DaoTaoVienTruongs = new DT_DaoTaoVienTruongCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_DaoTaoVienTruongs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_DaoTaoVienTruongs;
    }


    public static DT_DaoTaoVienTruongCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_DaoTaoVienTruongCls[] DT_DaoTaoVienTruongs = ParseFromDataTable(ds.Tables[0]);
        return DT_DaoTaoVienTruongs;
    }


    public static DT_DaoTaoVienTruongCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_DaoTaoVienTruong;
    }


    public static XmlCls GetXml(DT_DaoTaoVienTruongCls[] DT_DaoTaoVienTruongs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("TRUONG");
        ds.Tables["info"].Columns.Add("SOHOCVIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("NGUYENTAC", typeof(int?));
        ds.Tables["info"].Columns.Add("CHITIET", typeof(int?));
        ds.Tables["info"].Columns.Add("BCTONGKETCONGTACDAOTAO_ID");
        for (int iIndex = 0; iIndex < DT_DaoTaoVienTruongs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_DaoTaoVienTruongs[iIndex].ID,
                DT_DaoTaoVienTruongs[iIndex].TRUONG,
                DT_DaoTaoVienTruongs[iIndex].SOHOCVIEN,
                DT_DaoTaoVienTruongs[iIndex].NGUYENTAC,
                DT_DaoTaoVienTruongs[iIndex].CHITIET,
                DT_DaoTaoVienTruongs[iIndex].BCTONGKETCONGTACDAOTAO_ID
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("TRUONG");
        ds.Tables["info"].Columns.Add("SOHOCVIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("NGUYENTAC", typeof(int?));
        ds.Tables["info"].Columns.Add("CHITIET", typeof(int?));
        ds.Tables["info"].Columns.Add("BCTONGKETCONGTACDAOTAO_ID");
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_DaoTaoVienTruong.ID,
                ODT_DaoTaoVienTruong.TRUONG,
                ODT_DaoTaoVienTruong.SOHOCVIEN,
                ODT_DaoTaoVienTruong.NGUYENTAC,
                ODT_DaoTaoVienTruong.CHITIET,
                ODT_DaoTaoVienTruong.BCTONGKETCONGTACDAOTAO_ID
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
