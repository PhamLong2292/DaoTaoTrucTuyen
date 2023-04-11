using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_TaiLieuCls
    {
        public string ID;
        public string KHOAHOC_ID = "";
        public string TENTEP = "";
        public string TENHIENTHI = "";
        public string GHICHU = "";
        public string DUONGDAN = "";
        public string NGUOITAO_ID = "";
        public DateTime NGAYTAO;
    }
}

public class DT_TaiLieuParser
{
    public static DT_TaiLieuCls CreateInstance()
    {
        DT_TaiLieuCls ODT_TaiLieu = new DT_TaiLieuCls();
        return ODT_TaiLieu;
    }


    public static DT_TaiLieuCls ParseFromDataRow(DataRow dr)
    {
        DT_TaiLieuCls ODT_TaiLieu = new DT_TaiLieuCls();
        ODT_TaiLieu.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_TaiLieu.KHOAHOC_ID = CoreXmlUtility.GetString(dr, "KHOAHOC_ID", true);
        ODT_TaiLieu.TENTEP = CoreXmlUtility.GetString(dr, "TENTEP", true);
        ODT_TaiLieu.TENHIENTHI = CoreXmlUtility.GetString(dr, "TENHIENTHI", true);
        ODT_TaiLieu.GHICHU = CoreXmlUtility.GetString(dr, "GHICHU", true);
        ODT_TaiLieu.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_TaiLieu.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        try
        {
            string uploadedDaoTaoFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedDaoTaoFilePath"];
            ODT_TaiLieu.DUONGDAN = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + uploadedDaoTaoFilePath), ODT_TaiLieu.TENTEP);
        }
        catch { }
        return ODT_TaiLieu;
    }

    public static DT_TaiLieuCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_TaiLieuCls[] DT_TaiLieus = new DT_TaiLieuCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_TaiLieus[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_TaiLieus;
    }


    public static DT_TaiLieuCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_TaiLieuCls[] DT_TaiLieus = ParseFromDataTable(ds.Tables[0]);
        return DT_TaiLieus;
    }


    public static DT_TaiLieuCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_TaiLieuCls ODT_TaiLieu = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_TaiLieu;
    }


    public static XmlCls GetXml(DT_TaiLieuCls[] DT_TaiLieus)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("KHOAHOC_ID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        for (int iIndex = 0; iIndex < DT_TaiLieus.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_TaiLieus[iIndex].ID,
                DT_TaiLieus[iIndex].KHOAHOC_ID,
                DT_TaiLieus[iIndex].TENTEP,
                DT_TaiLieus[iIndex].TENHIENTHI,
                DT_TaiLieus[iIndex].GHICHU,
                DT_TaiLieus[iIndex].DUONGDAN,
                DT_TaiLieus[iIndex].NGUOITAO_ID,
                DT_TaiLieus[iIndex].NGAYTAO
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_TaiLieuCls ODT_TaiLieu)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("KHOAHOC_ID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_TaiLieu.ID,
                ODT_TaiLieu.KHOAHOC_ID,
                ODT_TaiLieu.TENTEP,
                ODT_TaiLieu.TENHIENTHI,
                ODT_TaiLieu.GHICHU,
                ODT_TaiLieu.DUONGDAN,
                ODT_TaiLieu.NGUOITAO_ID,
                ODT_TaiLieu.NGAYTAO
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
