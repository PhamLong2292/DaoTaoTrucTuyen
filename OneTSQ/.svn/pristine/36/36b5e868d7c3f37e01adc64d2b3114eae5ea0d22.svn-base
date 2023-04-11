using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_TaiLieuChuyenGiaoCls
    {
        public string ID;
        public string LICHCHUYENGIAO_ID = "";
        public string TENTEP = "";
        public string TENHIENTHI = "";
        public string GHICHU = "";
        public string DUONGDAN = "";
        public string NGUOITAO_ID = "";
        public DateTime NGAYTAO;
    }
}

public class DT_TaiLieuChuyenGiaoParser
{
    public static DT_TaiLieuChuyenGiaoCls CreateInstance()
    {
        DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao = new DT_TaiLieuChuyenGiaoCls();
        return ODT_TaiLieuChuyenGiao;
    }


    public static DT_TaiLieuChuyenGiaoCls ParseFromDataRow(DataRow dr)
    {
        DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao = new DT_TaiLieuChuyenGiaoCls();
        ODT_TaiLieuChuyenGiao.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_TaiLieuChuyenGiao.LICHCHUYENGIAO_ID = CoreXmlUtility.GetString(dr, "LICHCHUYENGIAO_ID", true);
        ODT_TaiLieuChuyenGiao.TENTEP = CoreXmlUtility.GetString(dr, "TENTEP", true);
        ODT_TaiLieuChuyenGiao.TENHIENTHI = CoreXmlUtility.GetString(dr, "TENHIENTHI", true);
        ODT_TaiLieuChuyenGiao.GHICHU = CoreXmlUtility.GetString(dr, "GHICHU", true);
        ODT_TaiLieuChuyenGiao.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_TaiLieuChuyenGiao.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        try
        {
            string uploadedDaoTaoFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedDaoTaoFilePath"];
            ODT_TaiLieuChuyenGiao.DUONGDAN = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + uploadedDaoTaoFilePath), ODT_TaiLieuChuyenGiao.TENTEP);
        }
        catch { }
        return ODT_TaiLieuChuyenGiao;
    }

    public static DT_TaiLieuChuyenGiaoCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_TaiLieuChuyenGiaoCls[] DT_TaiLieuChuyenGiaos = new DT_TaiLieuChuyenGiaoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_TaiLieuChuyenGiaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_TaiLieuChuyenGiaos;
    }


    public static DT_TaiLieuChuyenGiaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_TaiLieuChuyenGiaoCls[] DT_TaiLieuChuyenGiaos = ParseFromDataTable(ds.Tables[0]);
        return DT_TaiLieuChuyenGiaos;
    }


    public static DT_TaiLieuChuyenGiaoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_TaiLieuChuyenGiao;
    }


    public static XmlCls GetXml(DT_TaiLieuChuyenGiaoCls[] DT_TaiLieuChuyenGiaos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHCHUYENGIAO_ID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        for (int iIndex = 0; iIndex < DT_TaiLieuChuyenGiaos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_TaiLieuChuyenGiaos[iIndex].ID,
                DT_TaiLieuChuyenGiaos[iIndex].LICHCHUYENGIAO_ID,
                DT_TaiLieuChuyenGiaos[iIndex].TENTEP,
                DT_TaiLieuChuyenGiaos[iIndex].TENHIENTHI,
                DT_TaiLieuChuyenGiaos[iIndex].GHICHU,
                DT_TaiLieuChuyenGiaos[iIndex].DUONGDAN,
                DT_TaiLieuChuyenGiaos[iIndex].NGUOITAO_ID,
                DT_TaiLieuChuyenGiaos[iIndex].NGAYTAO
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("LICHCHUYENGIAO_ID");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_TaiLieuChuyenGiao.ID,
                ODT_TaiLieuChuyenGiao.LICHCHUYENGIAO_ID,
                ODT_TaiLieuChuyenGiao.TENTEP,
                ODT_TaiLieuChuyenGiao.TENHIENTHI,
                ODT_TaiLieuChuyenGiao.GHICHU,
                ODT_TaiLieuChuyenGiao.DUONGDAN,
                ODT_TaiLieuChuyenGiao.NGUOITAO_ID,
                ODT_TaiLieuChuyenGiao.NGAYTAO
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
