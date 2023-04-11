using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;

namespace OneTSQ.Model
{
    public class DanhGiaDeCuong_DeTaiCls
    {
        public string ID;
        public string NGUOIDANHGIA_ID;
        public DateTime? NGAYTAO;
        public decimal? DIEM;
        public string DANHGIA;
        public string YKIENKHAC;
        public string DECUONG_ID;
        public string DETAI_ID;
    }
}

public class DanhGiaDeCuong_DeTaiParser
{
   
    public static DanhGiaDeCuong_DeTaiCls CreateInstance()
    {
        DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai = new DanhGiaDeCuong_DeTaiCls();
        return ODanhGiaDeCuong_DeTai;
    }
    public static DanhGiaDeCuong_DeTaiCls ParseFromDataRow(DataRow dr)
    {
        DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai = new DanhGiaDeCuong_DeTaiCls();
        ODanhGiaDeCuong_DeTai.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODanhGiaDeCuong_DeTai.NGUOIDANHGIA_ID = CoreXmlUtility.GetString(dr, "NGUOIDANHGIA_ID", true);
        ODanhGiaDeCuong_DeTai.NGAYTAO = CoreXmlUtility.GetDateOrNull(dr, "NGAYTAO", true);
        ODanhGiaDeCuong_DeTai.DIEM = CoreXmlUtility.GetDecimalOrNull(dr, "DIEM", true);
        ODanhGiaDeCuong_DeTai.DANHGIA = CoreXmlUtility.GetString(dr, "DANHGIA", true);
        ODanhGiaDeCuong_DeTai.YKIENKHAC = CoreXmlUtility.GetString(dr, "YKIENKHAC", true);
        ODanhGiaDeCuong_DeTai.DECUONG_ID = CoreXmlUtility.GetString(dr, "DECUONG_ID", true);
        ODanhGiaDeCuong_DeTai.DETAI_ID = CoreXmlUtility.GetString(dr, "DETAI_ID", true);
        return ODanhGiaDeCuong_DeTai;
    }

    public static DanhGiaDeCuong_DeTaiCls[] ParseFromDataTable(DataTable dtTable)
    {
        DanhGiaDeCuong_DeTaiCls[] DanhGiaDeCuong_DeTais = new DanhGiaDeCuong_DeTaiCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DanhGiaDeCuong_DeTais[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DanhGiaDeCuong_DeTais;
    }


    public static DanhGiaDeCuong_DeTaiCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DanhGiaDeCuong_DeTaiCls[] DanhGiaDeCuong_DeTais = ParseFromDataTable(ds.Tables[0]);
        return DanhGiaDeCuong_DeTais;
    }


    public static DanhGiaDeCuong_DeTaiCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODanhGiaDeCuong_DeTai;
    }


    public static XmlCls GetXml(DanhGiaDeCuong_DeTaiCls[] DanhGiaDeCuong_DeTais)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("NGUOIDANHGIA_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIEM", typeof(decimal?));
        ds.Tables["info"].Columns.Add("DANHGIA");
        ds.Tables["info"].Columns.Add("YKIENKHAC");
        ds.Tables["info"].Columns.Add("DECUONG_ID");
        ds.Tables["info"].Columns.Add("DETAI_ID");
        for (int iIndex = 0; iIndex < DanhGiaDeCuong_DeTais.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DanhGiaDeCuong_DeTais[iIndex].ID,
                DanhGiaDeCuong_DeTais[iIndex].NGUOIDANHGIA_ID,
                DanhGiaDeCuong_DeTais[iIndex].NGAYTAO,
                DanhGiaDeCuong_DeTais[iIndex].DIEM,
                DanhGiaDeCuong_DeTais[iIndex].DANHGIA,
                DanhGiaDeCuong_DeTais[iIndex].YKIENKHAC,
                DanhGiaDeCuong_DeTais[iIndex].DECUONG_ID,
                DanhGiaDeCuong_DeTais[iIndex].DETAI_ID,
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai)
    {
        DataSet ds = new DataSet();
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("NGUOIDANHGIA_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIEM", typeof(decimal?));
        ds.Tables["info"].Columns.Add("DANHGIA");
        ds.Tables["info"].Columns.Add("YKIENKHAC");
        ds.Tables["info"].Columns.Add("DECUONG_ID");
        ds.Tables["info"].Columns.Add("DETAI_ID");
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODanhGiaDeCuong_DeTai.ID,
                ODanhGiaDeCuong_DeTai.NGUOIDANHGIA_ID,
                ODanhGiaDeCuong_DeTai.NGAYTAO,
                ODanhGiaDeCuong_DeTai.DIEM,
                ODanhGiaDeCuong_DeTai.DANHGIA,
                ODanhGiaDeCuong_DeTai.YKIENKHAC,
                ODanhGiaDeCuong_DeTai.DECUONG_ID,
                ODanhGiaDeCuong_DeTai.DETAI_ID,
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
