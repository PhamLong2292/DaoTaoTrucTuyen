using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_NhomKhoaHocCls
    {
        public string Id;
        public string Ma = "";
        public string Ten = "";
        public string MoTa = "";
        public int HieuLuc;
        public int Stt;
        public DateTime NgayTao;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
        public string GhiChu = "";
        public string ChaId;
        public static string REDISNAME = "NhomKhoaHoc";

        public enum ePermission
        {
            Xem = 0,
            Them = 1,
            Sua = 2,
            Xoa = 3,
            Import = 4,
            Export = 5
        }
    }

    public class DM_NhomKhoaHocParser
    {
        public static DM_NhomKhoaHocCls CreateInstance()
        {
            DM_NhomKhoaHocCls ONhomKhoaHoc = new DM_NhomKhoaHocCls();
            return ONhomKhoaHoc;
        }


        public static DM_NhomKhoaHocCls ParseFromDataRow(DataRow dr)
        {
            DM_NhomKhoaHocCls ONhomKhoaHoc = new DM_NhomKhoaHocCls();
            ONhomKhoaHoc.Id = CoreXmlUtility.GetString(dr, "ID", true);
            ONhomKhoaHoc.Ma = CoreXmlUtility.GetString(dr, "MA", true);
            ONhomKhoaHoc.Ten = CoreXmlUtility.GetString(dr, "TEN", true);
            ONhomKhoaHoc.MoTa = CoreXmlUtility.GetString(dr, "MOTA", true);
            ONhomKhoaHoc.HieuLuc = CoreXmlUtility.GetInt(dr, "HIEULUC", true);
            ONhomKhoaHoc.Stt = CoreXmlUtility.GetInt(dr, "STT", true);
            ONhomKhoaHoc.NgayTao = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
            ONhomKhoaHoc.TuNgay = dr["TUNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "TUNGAY", true);
            ONhomKhoaHoc.DenNgay = dr["DENNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "DENNGAY", true);
            ONhomKhoaHoc.GhiChu = CoreXmlUtility.GetString(dr, "GHICHU", true);
            ONhomKhoaHoc.ChaId = CoreXmlUtility.GetString(dr, "CHAID", true);
            return ONhomKhoaHoc;
        }

        public static DM_NhomKhoaHocCls[] ParseFromDataTable(DataTable dtTable)
        {
            DM_NhomKhoaHocCls[] NhomKhoaHocs = new DM_NhomKhoaHocCls[dtTable.Rows.Count];
            for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
            {
                NhomKhoaHocs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
            }
            return NhomKhoaHocs;
        }
        public static long CountFromDataTable(DataTable dtTable)
        {
            if (dtTable != null && dtTable.Rows.Count > 0)
            {
                return Int64.Parse(dtTable.Rows[0][0].ToString());
            }
            else
                return 0;
        }


        public static DM_NhomKhoaHocCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            DM_NhomKhoaHocCls[] NhomKhoaHocs = ParseFromDataTable(ds.Tables[0]);
            return NhomKhoaHocs;
        }


        public static DM_NhomKhoaHocCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_NhomKhoaHocCls ONhomKhoaHoc = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return ONhomKhoaHoc;
        }


        public static XmlCls GetXml(DM_NhomKhoaHocCls[] NhomKhoaHocs)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("info");
            ds.Tables["info"].Columns.Add("ID");
            ds.Tables["info"].Columns.Add("MA");
            ds.Tables["info"].Columns.Add("TEN");
            ds.Tables["info"].Columns.Add("MOTA");
            ds.Tables["info"].Columns.Add("HIEULUC", typeof(int));
            ds.Tables["info"].Columns.Add("STT", typeof(int));
            ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
            ds.Tables["info"].Columns.Add("TUNGAY", typeof(DateTime?));
            ds.Tables["info"].Columns.Add("DENNGAY", typeof(DateTime?));
            ds.Tables["info"].Columns.Add("GHICHU");
            ds.Tables["info"].Columns.Add("CHAID");
            for (int iIndex = 0; iIndex < NhomKhoaHocs.Length; iIndex++)
            {
                ds.Tables["info"].Rows.Add(new object[]
                {
                NhomKhoaHocs[iIndex].Id,
                NhomKhoaHocs[iIndex].Ma,
                NhomKhoaHocs[iIndex].Ten,
                NhomKhoaHocs[iIndex].MoTa,
                NhomKhoaHocs[iIndex].HieuLuc,
                NhomKhoaHocs[iIndex].Stt,
                NhomKhoaHocs[iIndex].NgayTao,
                NhomKhoaHocs[iIndex].TuNgay,
                NhomKhoaHocs[iIndex].DenNgay,
                NhomKhoaHocs[iIndex].GhiChu,
                NhomKhoaHocs[iIndex].ChaId
                });
            }
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }


        public static XmlCls GetXml(DM_NhomKhoaHocCls ONhomKhoaHoc)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("info");
            ds.Tables["info"].Columns.Add("ID");
            ds.Tables["info"].Columns.Add("MA");
            ds.Tables["info"].Columns.Add("TEN");
            ds.Tables["info"].Columns.Add("MOTA");
            ds.Tables["info"].Columns.Add("HIEULUC", typeof(int));
            ds.Tables["info"].Columns.Add("STT", typeof(int));
            ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
            ds.Tables["info"].Columns.Add("TUNGAY", typeof(DateTime?));
            ds.Tables["info"].Columns.Add("DENNGAY", typeof(DateTime?));
            ds.Tables["info"].Columns.Add("GHICHU");
            ds.Tables["info"].Columns.Add("CHAID");
            ds.Tables["info"].Rows.Add(new object[]
            {
                ONhomKhoaHoc.Id,
                ONhomKhoaHoc.Ma,
                ONhomKhoaHoc.Ten,
                ONhomKhoaHoc.MoTa,
                ONhomKhoaHoc.HieuLuc,
                ONhomKhoaHoc.Stt,
                ONhomKhoaHoc.NgayTao,
                ONhomKhoaHoc.TuNgay,
                ONhomKhoaHoc.DenNgay,
                ONhomKhoaHoc.GhiChu,
                ONhomKhoaHoc.ChaId
            });
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
