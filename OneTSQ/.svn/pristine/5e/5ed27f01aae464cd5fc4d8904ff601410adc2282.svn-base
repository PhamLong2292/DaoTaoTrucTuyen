using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_TenKhoaHocCls
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
        public string NhomKhoaHoc_Ma;
        public static string REDISNAME = "TenKhoaHoc";

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

    public class DM_TenKhoaHocParser
    {
        public static DM_TenKhoaHocCls CreateInstance()
        {
            DM_TenKhoaHocCls OTenKhoaHoc = new DM_TenKhoaHocCls();
            return OTenKhoaHoc;
        }


        public static DM_TenKhoaHocCls ParseFromDataRow(DataRow dr)
        {
            DM_TenKhoaHocCls OTenKhoaHoc = new DM_TenKhoaHocCls();
            OTenKhoaHoc.Id = CoreXmlUtility.GetString(dr, "ID", true);
            OTenKhoaHoc.Ma = CoreXmlUtility.GetString(dr, "MA", true);
            OTenKhoaHoc.Ten = CoreXmlUtility.GetString(dr, "TEN", true);
            OTenKhoaHoc.MoTa = CoreXmlUtility.GetString(dr, "MOTA", true);
            OTenKhoaHoc.HieuLuc = CoreXmlUtility.GetInt(dr, "HIEULUC", true);
            OTenKhoaHoc.Stt = CoreXmlUtility.GetInt(dr, "STT", true);
            OTenKhoaHoc.NgayTao = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
            OTenKhoaHoc.TuNgay = dr["TUNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "TUNGAY", true);
            OTenKhoaHoc.DenNgay = dr["DENNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "DENNGAY", true);
            OTenKhoaHoc.GhiChu = CoreXmlUtility.GetString(dr, "GHICHU", true);
            OTenKhoaHoc.ChaId = CoreXmlUtility.GetString(dr, "CHAID", true);
            OTenKhoaHoc.NhomKhoaHoc_Ma = CoreXmlUtility.GetString(dr, "NHOMKHOAHOC_MA", true);
            return OTenKhoaHoc;
        }

        public static DM_TenKhoaHocCls[] ParseFromDataTable(DataTable dtTable)
        {
            DM_TenKhoaHocCls[] TenKhoaHocs = new DM_TenKhoaHocCls[dtTable.Rows.Count];
            for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
            {
                TenKhoaHocs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
            }
            return TenKhoaHocs;
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


        public static DM_TenKhoaHocCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            DM_TenKhoaHocCls[] TenKhoaHocs = ParseFromDataTable(ds.Tables[0]);
            return TenKhoaHocs;
        }


        public static DM_TenKhoaHocCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_TenKhoaHocCls OTenKhoaHoc = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OTenKhoaHoc;
        }


        public static XmlCls GetXml(DM_TenKhoaHocCls[] TenKhoaHocs)
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
            ds.Tables["info"].Columns.Add("NHOMKHOAHOC_MA");
            for (int iIndex = 0; iIndex < TenKhoaHocs.Length; iIndex++)
            {
                ds.Tables["info"].Rows.Add(new object[]
                {
                TenKhoaHocs[iIndex].Id,
                TenKhoaHocs[iIndex].Ma,
                TenKhoaHocs[iIndex].Ten,
                TenKhoaHocs[iIndex].MoTa,
                TenKhoaHocs[iIndex].HieuLuc,
                TenKhoaHocs[iIndex].Stt,
                TenKhoaHocs[iIndex].NgayTao,
                TenKhoaHocs[iIndex].TuNgay,
                TenKhoaHocs[iIndex].DenNgay,
                TenKhoaHocs[iIndex].GhiChu,
                TenKhoaHocs[iIndex].ChaId,
                TenKhoaHocs[iIndex].NhomKhoaHoc_Ma
                });
            }
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }


        public static XmlCls GetXml(DM_TenKhoaHocCls OTenKhoaHoc)
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
            ds.Tables["info"].Columns.Add("NHOMKHOAHOC_MA");
            ds.Tables["info"].Rows.Add(new object[]
            {
                OTenKhoaHoc.Id,
                OTenKhoaHoc.Ma,
                OTenKhoaHoc.Ten,
                OTenKhoaHoc.MoTa,
                OTenKhoaHoc.HieuLuc,
                OTenKhoaHoc.Stt,
                OTenKhoaHoc.NgayTao,
                OTenKhoaHoc.TuNgay,
                OTenKhoaHoc.DenNgay,
                OTenKhoaHoc.GhiChu,
                OTenKhoaHoc.ChaId,
                OTenKhoaHoc.NhomKhoaHoc_Ma
            });
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
