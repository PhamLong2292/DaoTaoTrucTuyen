using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class BinhLuanHinhAnhCls
    {
        public string ID;
        public string HINHANHID = "";
        public string CABENHANHCLSID = "";
        public string BINHLUANHINHANHID = "";
        public string BACSYIDS = "";
        public string BACSY = "";
        public DateTime? THOIGIAN;
        public string NOIDUNG = "";
    }
}

public class BinhLuanHinhAnhParser
{
    public static BinhLuanHinhAnhCls CreateInstance()
    {
        BinhLuanHinhAnhCls OBinhLuanHinhAnh = new BinhLuanHinhAnhCls();
        return OBinhLuanHinhAnh;
    }


    public static BinhLuanHinhAnhCls ParseFromDataRow(DataRow dr)
    {
        BinhLuanHinhAnhCls OBinhLuanHinhAnh = new BinhLuanHinhAnhCls();
        OBinhLuanHinhAnh.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OBinhLuanHinhAnh.HINHANHID = CoreXmlUtility.GetString(dr, "HINHANHID", true);
        OBinhLuanHinhAnh.CABENHANHCLSID = CoreXmlUtility.GetString(dr, "CABENHANHCLSID", true);
        OBinhLuanHinhAnh.BINHLUANHINHANHID = CoreXmlUtility.GetString(dr, "BINHLUANHINHANHID", true);
        OBinhLuanHinhAnh.BACSYIDS = CoreXmlUtility.GetString(dr, "BACSYIDS", true);
        OBinhLuanHinhAnh.BACSY = CoreXmlUtility.GetString(dr, "BACSY", true);
        OBinhLuanHinhAnh.THOIGIAN = dr["THOIGIAN"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "THOIGIAN", true);
        OBinhLuanHinhAnh.NOIDUNG = CoreXmlUtility.GetString(dr, "NOIDUNG", true);
        return OBinhLuanHinhAnh;
    }

    public static BinhLuanHinhAnhCls[] ParseFromDataTable(DataTable dtTable)
    {
        BinhLuanHinhAnhCls[] BinhLuanHinhAnhs = new BinhLuanHinhAnhCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            BinhLuanHinhAnhs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return BinhLuanHinhAnhs;
    }


    public static BinhLuanHinhAnhCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        BinhLuanHinhAnhCls[] BinhLuanHinhAnhs = ParseFromDataTable(ds.Tables[0]);
        return BinhLuanHinhAnhs;
    }


    public static BinhLuanHinhAnhCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        BinhLuanHinhAnhCls OBinhLuanHinhAnh = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBinhLuanHinhAnh;
    }


    public static XmlCls GetXml(BinhLuanHinhAnhCls[] BinhLuanHinhAnhs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("HINHANHID");
        ds.Tables["info"].Columns.Add("CABENHANHCLSID");
        ds.Tables["info"].Columns.Add("BINHLUANHINHANHID");
        ds.Tables["info"].Columns.Add("BACSYIDS");
        ds.Tables["info"].Columns.Add("BACSY");
        ds.Tables["info"].Columns.Add("THOIGIAN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NOIDUNG");
        for (int iIndex = 0; iIndex < BinhLuanHinhAnhs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                BinhLuanHinhAnhs[iIndex].ID,
                BinhLuanHinhAnhs[iIndex].HINHANHID,
                BinhLuanHinhAnhs[iIndex].CABENHANHCLSID,
                BinhLuanHinhAnhs[iIndex].BINHLUANHINHANHID,
                BinhLuanHinhAnhs[iIndex].BACSYIDS,
                BinhLuanHinhAnhs[iIndex].BACSY,
                BinhLuanHinhAnhs[iIndex].THOIGIAN,
                BinhLuanHinhAnhs[iIndex].NOIDUNG
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(BinhLuanHinhAnhCls OBinhLuanHinhAnh)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("HINHANHID");
        ds.Tables["info"].Columns.Add("CABENHANHCLSID");
        ds.Tables["info"].Columns.Add("BINHLUANHINHANHID");
        ds.Tables["info"].Columns.Add("BACSYIDS");
        ds.Tables["info"].Columns.Add("BACSY");
        ds.Tables["info"].Columns.Add("THOIGIAN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NOIDUNG");
        ds.Tables["info"].Rows.Add(new object[]
        {
                OBinhLuanHinhAnh.ID,
                OBinhLuanHinhAnh.HINHANHID,
                OBinhLuanHinhAnh.CABENHANHCLSID,
                OBinhLuanHinhAnh.BINHLUANHINHANHID,
                OBinhLuanHinhAnh.BACSYIDS,
                OBinhLuanHinhAnh.BACSY,
                OBinhLuanHinhAnh.THOIGIAN,
                OBinhLuanHinhAnh.NOIDUNG
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
