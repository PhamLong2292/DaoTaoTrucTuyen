using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_TrangThietBiTruyenHinhTtCls
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

        public static string REDISNAME = "TrangThietBiTruyenHinhTt";

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

    public class DM_TrangThietBiTruyenHinhTtParser
    {
        public static DM_TrangThietBiTruyenHinhTtCls CreateInstance()
        {
            DM_TrangThietBiTruyenHinhTtCls OTrangThietBiTruyenHinhTt = new DM_TrangThietBiTruyenHinhTtCls();
            return OTrangThietBiTruyenHinhTt;
        }


        public static DM_TrangThietBiTruyenHinhTtCls ParseFromDataRow(DataRow dr)
        {
            DM_TrangThietBiTruyenHinhTtCls OTrangThietBiTruyenHinhTt = new DM_TrangThietBiTruyenHinhTtCls();
            OTrangThietBiTruyenHinhTt.Id = CoreXmlUtility.GetString(dr, "ID", true);
            OTrangThietBiTruyenHinhTt.Ma = CoreXmlUtility.GetString(dr, "MA", true);
            OTrangThietBiTruyenHinhTt.Ten = CoreXmlUtility.GetString(dr, "TEN", true);
            OTrangThietBiTruyenHinhTt.MoTa = CoreXmlUtility.GetString(dr, "MOTA", true);
            OTrangThietBiTruyenHinhTt.HieuLuc = CoreXmlUtility.GetInt(dr, "HIEULUC", true);
            OTrangThietBiTruyenHinhTt.Stt = CoreXmlUtility.GetInt(dr, "STT", true);
            OTrangThietBiTruyenHinhTt.NgayTao = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
            OTrangThietBiTruyenHinhTt.TuNgay = dr["TUNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "TUNGAY", true);
            OTrangThietBiTruyenHinhTt.DenNgay = dr["DENNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "DENNGAY", true);
            OTrangThietBiTruyenHinhTt.GhiChu = CoreXmlUtility.GetString(dr, "GHICHU", true);
            OTrangThietBiTruyenHinhTt.ChaId = CoreXmlUtility.GetString(dr, "CHAID", true);
            return OTrangThietBiTruyenHinhTt;
        }

        public static DM_TrangThietBiTruyenHinhTtCls[] ParseFromDataTable(DataTable dtTable)
        {
            DM_TrangThietBiTruyenHinhTtCls[] TrangThietBiTruyenHinhTts = new DM_TrangThietBiTruyenHinhTtCls[dtTable.Rows.Count];
            for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
            {
                TrangThietBiTruyenHinhTts[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
            }
            return TrangThietBiTruyenHinhTts;
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


        public static DM_TrangThietBiTruyenHinhTtCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            DM_TrangThietBiTruyenHinhTtCls[] TrangThietBiTruyenHinhTts = ParseFromDataTable(ds.Tables[0]);
            return TrangThietBiTruyenHinhTts;
        }


        public static DM_TrangThietBiTruyenHinhTtCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_TrangThietBiTruyenHinhTtCls OTrangThietBiTruyenHinhTt = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OTrangThietBiTruyenHinhTt;
        }


        public static XmlCls GetXml(DM_TrangThietBiTruyenHinhTtCls[] TrangThietBiTruyenHinhTts)
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
            for (int iIndex = 0; iIndex < TrangThietBiTruyenHinhTts.Length; iIndex++)
            {
                ds.Tables["info"].Rows.Add(new object[]
                {
                TrangThietBiTruyenHinhTts[iIndex].Id,
                TrangThietBiTruyenHinhTts[iIndex].Ma,
                TrangThietBiTruyenHinhTts[iIndex].Ten,
                TrangThietBiTruyenHinhTts[iIndex].MoTa,
                TrangThietBiTruyenHinhTts[iIndex].HieuLuc,
                TrangThietBiTruyenHinhTts[iIndex].Stt,
                TrangThietBiTruyenHinhTts[iIndex].NgayTao,
                TrangThietBiTruyenHinhTts[iIndex].TuNgay,
                TrangThietBiTruyenHinhTts[iIndex].DenNgay,
                TrangThietBiTruyenHinhTts[iIndex].GhiChu,
                TrangThietBiTruyenHinhTts[iIndex].ChaId
                });
            }
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }


        public static XmlCls GetXml(DM_TrangThietBiTruyenHinhTtCls OTrangThietBiTruyenHinhTt)
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
                OTrangThietBiTruyenHinhTt.Id,
                OTrangThietBiTruyenHinhTt.Ma,
                OTrangThietBiTruyenHinhTt.Ten,
                OTrangThietBiTruyenHinhTt.MoTa,
                OTrangThietBiTruyenHinhTt.HieuLuc,
                OTrangThietBiTruyenHinhTt.Stt,
                OTrangThietBiTruyenHinhTt.NgayTao,
                OTrangThietBiTruyenHinhTt.TuNgay,
                OTrangThietBiTruyenHinhTt.DenNgay,
                OTrangThietBiTruyenHinhTt.GhiChu,
                OTrangThietBiTruyenHinhTt.ChaId
            });
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
