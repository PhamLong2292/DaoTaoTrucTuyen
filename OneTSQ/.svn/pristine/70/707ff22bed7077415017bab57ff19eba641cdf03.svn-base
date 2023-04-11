using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_TieuChiThoiLuongDaoTaoTtCls
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

        public static string REDISNAME = "TieuChiThoiLuongDaoTaoTt";

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

    public class DM_TieuChiThoiLuongDaoTaoTtParser
    {
        public static DM_TieuChiThoiLuongDaoTaoTtCls CreateInstance()
        {
            DM_TieuChiThoiLuongDaoTaoTtCls OTieuChiThoiLuongDaoTaoTt = new DM_TieuChiThoiLuongDaoTaoTtCls();
            return OTieuChiThoiLuongDaoTaoTt;
        }


        public static DM_TieuChiThoiLuongDaoTaoTtCls ParseFromDataRow(DataRow dr)
        {
            DM_TieuChiThoiLuongDaoTaoTtCls OTieuChiThoiLuongDaoTaoTt = new DM_TieuChiThoiLuongDaoTaoTtCls();
            OTieuChiThoiLuongDaoTaoTt.Id = CoreXmlUtility.GetString(dr, "ID", true);
            OTieuChiThoiLuongDaoTaoTt.Ma = CoreXmlUtility.GetString(dr, "MA", true);
            OTieuChiThoiLuongDaoTaoTt.Ten = CoreXmlUtility.GetString(dr, "TEN", true);
            OTieuChiThoiLuongDaoTaoTt.MoTa = CoreXmlUtility.GetString(dr, "MOTA", true);
            OTieuChiThoiLuongDaoTaoTt.HieuLuc = CoreXmlUtility.GetInt(dr, "HIEULUC", true);
            OTieuChiThoiLuongDaoTaoTt.Stt = CoreXmlUtility.GetInt(dr, "STT", true);
            OTieuChiThoiLuongDaoTaoTt.NgayTao = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
            OTieuChiThoiLuongDaoTaoTt.TuNgay = dr["TUNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "TUNGAY", true);
            OTieuChiThoiLuongDaoTaoTt.DenNgay = dr["DENNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "DENNGAY", true);
            OTieuChiThoiLuongDaoTaoTt.GhiChu = CoreXmlUtility.GetString(dr, "GHICHU", true);
            OTieuChiThoiLuongDaoTaoTt.ChaId = CoreXmlUtility.GetString(dr, "CHAID", true);
            return OTieuChiThoiLuongDaoTaoTt;
        }

        public static DM_TieuChiThoiLuongDaoTaoTtCls[] ParseFromDataTable(DataTable dtTable)
        {
            DM_TieuChiThoiLuongDaoTaoTtCls[] TieuChiThoiLuongDaoTaoTts = new DM_TieuChiThoiLuongDaoTaoTtCls[dtTable.Rows.Count];
            for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
            {
                TieuChiThoiLuongDaoTaoTts[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
            }
            return TieuChiThoiLuongDaoTaoTts;
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


        public static DM_TieuChiThoiLuongDaoTaoTtCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            DM_TieuChiThoiLuongDaoTaoTtCls[] TieuChiThoiLuongDaoTaoTts = ParseFromDataTable(ds.Tables[0]);
            return TieuChiThoiLuongDaoTaoTts;
        }


        public static DM_TieuChiThoiLuongDaoTaoTtCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_TieuChiThoiLuongDaoTaoTtCls OTieuChiThoiLuongDaoTaoTt = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OTieuChiThoiLuongDaoTaoTt;
        }


        public static XmlCls GetXml(DM_TieuChiThoiLuongDaoTaoTtCls[] TieuChiThoiLuongDaoTaoTts)
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
            for (int iIndex = 0; iIndex < TieuChiThoiLuongDaoTaoTts.Length; iIndex++)
            {
                ds.Tables["info"].Rows.Add(new object[]
                {
                TieuChiThoiLuongDaoTaoTts[iIndex].Id,
                TieuChiThoiLuongDaoTaoTts[iIndex].Ma,
                TieuChiThoiLuongDaoTaoTts[iIndex].Ten,
                TieuChiThoiLuongDaoTaoTts[iIndex].MoTa,
                TieuChiThoiLuongDaoTaoTts[iIndex].HieuLuc,
                TieuChiThoiLuongDaoTaoTts[iIndex].Stt,
                TieuChiThoiLuongDaoTaoTts[iIndex].NgayTao,
                TieuChiThoiLuongDaoTaoTts[iIndex].TuNgay,
                TieuChiThoiLuongDaoTaoTts[iIndex].DenNgay,
                TieuChiThoiLuongDaoTaoTts[iIndex].GhiChu,
                TieuChiThoiLuongDaoTaoTts[iIndex].ChaId
                });
            }
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }


        public static XmlCls GetXml(DM_TieuChiThoiLuongDaoTaoTtCls OTieuChiThoiLuongDaoTaoTt)
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
                OTieuChiThoiLuongDaoTaoTt.Id,
                OTieuChiThoiLuongDaoTaoTt.Ma,
                OTieuChiThoiLuongDaoTaoTt.Ten,
                OTieuChiThoiLuongDaoTaoTt.MoTa,
                OTieuChiThoiLuongDaoTaoTt.HieuLuc,
                OTieuChiThoiLuongDaoTaoTt.Stt,
                OTieuChiThoiLuongDaoTaoTt.NgayTao,
                OTieuChiThoiLuongDaoTaoTt.TuNgay,
                OTieuChiThoiLuongDaoTaoTt.DenNgay,
                OTieuChiThoiLuongDaoTaoTt.GhiChu,
                OTieuChiThoiLuongDaoTaoTt.ChaId
            });
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
