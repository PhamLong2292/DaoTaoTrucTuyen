using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_TieuChiThoiGianDaoTaoTtCls
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

        public static string REDISNAME = "TieuChiThoiGianDaoTaoTt";

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

    public class DM_TieuChiThoiGianDaoTaoTtParser
    {
        public static DM_TieuChiThoiGianDaoTaoTtCls CreateInstance()
        {
            DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt = new DM_TieuChiThoiGianDaoTaoTtCls();
            return OTieuChiThoiGianDaoTaoTt;
        }


        public static DM_TieuChiThoiGianDaoTaoTtCls ParseFromDataRow(DataRow dr)
        {
            DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt = new DM_TieuChiThoiGianDaoTaoTtCls();
            OTieuChiThoiGianDaoTaoTt.Id = CoreXmlUtility.GetString(dr, "ID", true);
            OTieuChiThoiGianDaoTaoTt.Ma = CoreXmlUtility.GetString(dr, "MA", true);
            OTieuChiThoiGianDaoTaoTt.Ten = CoreXmlUtility.GetString(dr, "TEN", true);
            OTieuChiThoiGianDaoTaoTt.MoTa = CoreXmlUtility.GetString(dr, "MOTA", true);
            OTieuChiThoiGianDaoTaoTt.HieuLuc = CoreXmlUtility.GetInt(dr, "HIEULUC", true);
            OTieuChiThoiGianDaoTaoTt.Stt = CoreXmlUtility.GetInt(dr, "STT", true);
            OTieuChiThoiGianDaoTaoTt.NgayTao = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
            OTieuChiThoiGianDaoTaoTt.TuNgay = dr["TUNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "TUNGAY", true);
            OTieuChiThoiGianDaoTaoTt.DenNgay = dr["DENNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "DENNGAY", true);
            OTieuChiThoiGianDaoTaoTt.GhiChu = CoreXmlUtility.GetString(dr, "GHICHU", true);
            OTieuChiThoiGianDaoTaoTt.ChaId = CoreXmlUtility.GetString(dr, "CHAID", true);
            return OTieuChiThoiGianDaoTaoTt;
        }

        public static DM_TieuChiThoiGianDaoTaoTtCls[] ParseFromDataTable(DataTable dtTable)
        {
            DM_TieuChiThoiGianDaoTaoTtCls[] TieuChiThoiGianDaoTaoTts = new DM_TieuChiThoiGianDaoTaoTtCls[dtTable.Rows.Count];
            for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
            {
                TieuChiThoiGianDaoTaoTts[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
            }
            return TieuChiThoiGianDaoTaoTts;
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


        public static DM_TieuChiThoiGianDaoTaoTtCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            DM_TieuChiThoiGianDaoTaoTtCls[] TieuChiThoiGianDaoTaoTts = ParseFromDataTable(ds.Tables[0]);
            return TieuChiThoiGianDaoTaoTts;
        }


        public static DM_TieuChiThoiGianDaoTaoTtCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OTieuChiThoiGianDaoTaoTt;
        }


        public static XmlCls GetXml(DM_TieuChiThoiGianDaoTaoTtCls[] TieuChiThoiGianDaoTaoTts)
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
            for (int iIndex = 0; iIndex < TieuChiThoiGianDaoTaoTts.Length; iIndex++)
            {
                ds.Tables["info"].Rows.Add(new object[]
                {
                TieuChiThoiGianDaoTaoTts[iIndex].Id,
                TieuChiThoiGianDaoTaoTts[iIndex].Ma,
                TieuChiThoiGianDaoTaoTts[iIndex].Ten,
                TieuChiThoiGianDaoTaoTts[iIndex].MoTa,
                TieuChiThoiGianDaoTaoTts[iIndex].HieuLuc,
                TieuChiThoiGianDaoTaoTts[iIndex].Stt,
                TieuChiThoiGianDaoTaoTts[iIndex].NgayTao,
                TieuChiThoiGianDaoTaoTts[iIndex].TuNgay,
                TieuChiThoiGianDaoTaoTts[iIndex].DenNgay,
                TieuChiThoiGianDaoTaoTts[iIndex].GhiChu,
                TieuChiThoiGianDaoTaoTts[iIndex].ChaId
                });
            }
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }


        public static XmlCls GetXml(DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt)
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
                OTieuChiThoiGianDaoTaoTt.Id,
                OTieuChiThoiGianDaoTaoTt.Ma,
                OTieuChiThoiGianDaoTaoTt.Ten,
                OTieuChiThoiGianDaoTaoTt.MoTa,
                OTieuChiThoiGianDaoTaoTt.HieuLuc,
                OTieuChiThoiGianDaoTaoTt.Stt,
                OTieuChiThoiGianDaoTaoTt.NgayTao,
                OTieuChiThoiGianDaoTaoTt.TuNgay,
                OTieuChiThoiGianDaoTaoTt.DenNgay,
                OTieuChiThoiGianDaoTaoTt.GhiChu,
                OTieuChiThoiGianDaoTaoTt.ChaId
            });
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
