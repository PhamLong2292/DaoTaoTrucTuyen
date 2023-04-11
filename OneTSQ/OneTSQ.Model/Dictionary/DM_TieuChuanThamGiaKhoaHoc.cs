using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_TieuChuanThamGiaKhoaHocCls
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
        public static string REDISNAME = "TieuChuanThamGiaKhoaHoc";

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

    public class DM_TieuChuanThamGiaKhoaHocParser
    {
        public static DM_TieuChuanThamGiaKhoaHocCls CreateInstance()
        {
            DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc = new DM_TieuChuanThamGiaKhoaHocCls();
            return OTieuChuanThamGiaKhoaHoc;
        }


        public static DM_TieuChuanThamGiaKhoaHocCls ParseFromDataRow(DataRow dr)
        {
            DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc = new DM_TieuChuanThamGiaKhoaHocCls();
            OTieuChuanThamGiaKhoaHoc.Id = CoreXmlUtility.GetString(dr, "ID", true);
            OTieuChuanThamGiaKhoaHoc.Ma = CoreXmlUtility.GetString(dr, "MA", true);
            OTieuChuanThamGiaKhoaHoc.Ten = CoreXmlUtility.GetString(dr, "TEN", true);
            OTieuChuanThamGiaKhoaHoc.MoTa = CoreXmlUtility.GetString(dr, "MOTA", true);
            OTieuChuanThamGiaKhoaHoc.HieuLuc = CoreXmlUtility.GetInt(dr, "HIEULUC", true);
            OTieuChuanThamGiaKhoaHoc.Stt = CoreXmlUtility.GetInt(dr, "STT", true);
            OTieuChuanThamGiaKhoaHoc.NgayTao = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
            OTieuChuanThamGiaKhoaHoc.TuNgay = dr["TUNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "TUNGAY", true);
            OTieuChuanThamGiaKhoaHoc.DenNgay = dr["DENNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "DENNGAY", true);
            OTieuChuanThamGiaKhoaHoc.GhiChu = CoreXmlUtility.GetString(dr, "GHICHU", true);
            OTieuChuanThamGiaKhoaHoc.ChaId = CoreXmlUtility.GetString(dr, "CHAID", true);
            return OTieuChuanThamGiaKhoaHoc;
        }

        public static DM_TieuChuanThamGiaKhoaHocCls[] ParseFromDataTable(DataTable dtTable)
        {
            DM_TieuChuanThamGiaKhoaHocCls[] TieuChuanThamGiaKhoaHocs = new DM_TieuChuanThamGiaKhoaHocCls[dtTable.Rows.Count];
            for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
            {
                TieuChuanThamGiaKhoaHocs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
            }
            return TieuChuanThamGiaKhoaHocs;
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


        public static DM_TieuChuanThamGiaKhoaHocCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            DM_TieuChuanThamGiaKhoaHocCls[] TieuChuanThamGiaKhoaHocs = ParseFromDataTable(ds.Tables[0]);
            return TieuChuanThamGiaKhoaHocs;
        }


        public static DM_TieuChuanThamGiaKhoaHocCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OTieuChuanThamGiaKhoaHoc;
        }


        public static XmlCls GetXml(DM_TieuChuanThamGiaKhoaHocCls[] TieuChuanThamGiaKhoaHocs)
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
            for (int iIndex = 0; iIndex < TieuChuanThamGiaKhoaHocs.Length; iIndex++)
            {
                ds.Tables["info"].Rows.Add(new object[]
                {
                TieuChuanThamGiaKhoaHocs[iIndex].Id,
                TieuChuanThamGiaKhoaHocs[iIndex].Ma,
                TieuChuanThamGiaKhoaHocs[iIndex].Ten,
                TieuChuanThamGiaKhoaHocs[iIndex].MoTa,
                TieuChuanThamGiaKhoaHocs[iIndex].HieuLuc,
                TieuChuanThamGiaKhoaHocs[iIndex].Stt,
                TieuChuanThamGiaKhoaHocs[iIndex].NgayTao,
                TieuChuanThamGiaKhoaHocs[iIndex].TuNgay,
                TieuChuanThamGiaKhoaHocs[iIndex].DenNgay,
                TieuChuanThamGiaKhoaHocs[iIndex].GhiChu,
                TieuChuanThamGiaKhoaHocs[iIndex].ChaId
                });
            }
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }


        public static XmlCls GetXml(DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc)
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
                OTieuChuanThamGiaKhoaHoc.Id,
                OTieuChuanThamGiaKhoaHoc.Ma,
                OTieuChuanThamGiaKhoaHoc.Ten,
                OTieuChuanThamGiaKhoaHoc.MoTa,
                OTieuChuanThamGiaKhoaHoc.HieuLuc,
                OTieuChuanThamGiaKhoaHoc.Stt,
                OTieuChuanThamGiaKhoaHoc.NgayTao,
                OTieuChuanThamGiaKhoaHoc.TuNgay,
                OTieuChuanThamGiaKhoaHoc.DenNgay,
                OTieuChuanThamGiaKhoaHoc.GhiChu,
                OTieuChuanThamGiaKhoaHoc.ChaId
            });
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
