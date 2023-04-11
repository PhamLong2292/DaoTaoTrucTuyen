using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using System.Web;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class HinhAnhCls
    {
        public string ID;
        public string CABENHID = "";
        public string DUONGDAN = "";
        public byte[] NDTEP;
        public string TENTEP = "";
        public string TENHIENTHI = "";
        public long? KEY;
        public string SCODE = "";
        public string LINK = "";
        public string MODALITY = "";
        public DateTime? TIMEEX;
        public string MOTA = "";
        public int TYPE;
        //Các trường lưu giá trị mã hóa để hiển thị dạng html
        public string MOTA_ENCODED = "";
    }
}

public class HinhAnhParser
{
    public static HinhAnhCls CreateInstance()
    {
        HinhAnhCls OHinhAnh = new HinhAnhCls();
        return OHinhAnh;
    }


    public static HinhAnhCls ParseFromDataRow(DataRow dr)
    {
        HinhAnhCls OHinhAnh = new HinhAnhCls();
        OHinhAnh.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OHinhAnh.CABENHID = CoreXmlUtility.GetString(dr, "CABENHID", true);
        OHinhAnh.TENTEP = CoreXmlUtility.GetString(dr, "TENTEP", true);
        OHinhAnh.TENHIENTHI = CoreXmlUtility.GetString(dr, "TENHIENTHI", true);
        OHinhAnh.KEY = dr["KEY"].ToString() == "" ? null : (long?)CoreXmlUtility.GetLong(dr, "KEY", true);
        OHinhAnh.SCODE = CoreXmlUtility.GetString(dr, "SCODE", true);
        OHinhAnh.LINK = CoreXmlUtility.GetString(dr, "LINK", true);
        OHinhAnh.MODALITY = CoreXmlUtility.GetString(dr, "MODALITY", true);
        OHinhAnh.TIMEEX = CoreXmlUtility.GetDateOrNull(dr, "TIMEEX", true);
        OHinhAnh.MOTA = CoreXmlUtility.GetString(dr, "MOTA", true);
        OHinhAnh.TYPE = CoreXmlUtility.GetInt(dr, "TYPE", true);
        if (OHinhAnh.TYPE != (int)HinhAnh.eType.DICOM && !string.IsNullOrEmpty(OHinhAnh.TENTEP))//Không phải ảnh DICOM
        {
            string UploadedImagePath = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedImagePath"];
            System.Net.WebClient myWebClient = new System.Net.WebClient();
            try
            {
                OHinhAnh.DUONGDAN = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + UploadedImagePath), OHinhAnh.TENTEP);
            }
            catch { }
        }
        //Mã hóa dữ liệu
        OHinhAnh.MOTA_ENCODED = HttpUtility.HtmlEncode(OHinhAnh.MOTA);
        return OHinhAnh;
    }

    public static HinhAnhCls[] ParseFromDataTable(DataTable dtTable)
    {
        HinhAnhCls[] HinhAnhs = new HinhAnhCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            HinhAnhs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return HinhAnhs;
    }


    public static HinhAnhCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        HinhAnhCls[] HinhAnhs = ParseFromDataTable(ds.Tables[0]);
        return HinhAnhs;
    }


    public static HinhAnhCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        HinhAnhCls OHinhAnh = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OHinhAnh;
    }


    public static XmlCls GetXml(HinhAnhCls[] HinhAnhs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("KEY", typeof(long?));
        ds.Tables["info"].Columns.Add("SCODE");
        ds.Tables["info"].Columns.Add("LINK");
        ds.Tables["info"].Columns.Add("MODALITY");
        ds.Tables["info"].Columns.Add("TIMEEX", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("MOTA");
        ds.Tables["info"].Columns.Add("TYPE", typeof(int));
        for (int iIndex = 0; iIndex < HinhAnhs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                HinhAnhs[iIndex].ID,
                HinhAnhs[iIndex].CABENHID,
                HinhAnhs[iIndex].DUONGDAN,
                HinhAnhs[iIndex].TENTEP,
                HinhAnhs[iIndex].TENHIENTHI,
                HinhAnhs[iIndex].KEY,
                HinhAnhs[iIndex].SCODE,
                HinhAnhs[iIndex].LINK,
                HinhAnhs[iIndex].MODALITY,
                HinhAnhs[iIndex].TIMEEX,
                HinhAnhs[iIndex].MOTA,
                HinhAnhs[iIndex].TYPE
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(HinhAnhCls OHinhAnh)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("DUONGDAN");
        ds.Tables["info"].Columns.Add("TENTEP");
        ds.Tables["info"].Columns.Add("TENHIENTHI");
        ds.Tables["info"].Columns.Add("KEY", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("SCODE");
        ds.Tables["info"].Columns.Add("LINK");
        ds.Tables["info"].Columns.Add("MODALITY");
        ds.Tables["info"].Columns.Add("TIMEEX", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("MOTA");
        ds.Tables["info"].Columns.Add("TYPE", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
        {
                OHinhAnh.ID,
                OHinhAnh.CABENHID,
                OHinhAnh.DUONGDAN,
                OHinhAnh.TENTEP,
                OHinhAnh.TENHIENTHI,
                OHinhAnh.KEY,
                OHinhAnh.SCODE,
                OHinhAnh.LINK,
                OHinhAnh.MODALITY,
                OHinhAnh.TIMEEX,
                OHinhAnh.MOTA,
                OHinhAnh.TYPE
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
