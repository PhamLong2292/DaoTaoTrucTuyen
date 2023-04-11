using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_ChuyenKhoaDaoTaoTtCls
    {
        public string Id;
        public string Ma = "";
        public string Ten = "";
        public string TenNgan = "";
        public string MoTa = "";
        public int HieuLuc;
        public int Stt;
        public DateTime NgayTao;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
        public string GhiChu = "";
        public string ChaId;

        public static string REDISNAME = "ChuyenKhoaDaoTaoTt";

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

    public class DM_ChuyenKhoaDaoTaoTtParser
    {
        public static DM_ChuyenKhoaDaoTaoTtCls CreateInstance()
        {
            DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt = new DM_ChuyenKhoaDaoTaoTtCls();
            return OChuyenKhoaDaoTaoTt;
        }


        public static DM_ChuyenKhoaDaoTaoTtCls ParseFromDataRow(DataRow dr)
        {
            DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt = new DM_ChuyenKhoaDaoTaoTtCls();
            OChuyenKhoaDaoTaoTt.Id = CoreXmlUtility.GetString(dr, "ID", true);
            OChuyenKhoaDaoTaoTt.Ma = CoreXmlUtility.GetString(dr, "MA", true);
            OChuyenKhoaDaoTaoTt.Ten = CoreXmlUtility.GetString(dr, "TEN", true);
            OChuyenKhoaDaoTaoTt.TenNgan = CoreXmlUtility.GetString(dr, "TenNgan", true);
            OChuyenKhoaDaoTaoTt.MoTa = CoreXmlUtility.GetString(dr, "MOTA", true);
            OChuyenKhoaDaoTaoTt.HieuLuc = CoreXmlUtility.GetInt(dr, "HIEULUC", true);
            OChuyenKhoaDaoTaoTt.Stt = CoreXmlUtility.GetInt(dr, "STT", true);
            OChuyenKhoaDaoTaoTt.NgayTao = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
            OChuyenKhoaDaoTaoTt.TuNgay = dr["TUNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "TUNGAY", true);
            OChuyenKhoaDaoTaoTt.DenNgay = dr["DENNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "DENNGAY", true);
            OChuyenKhoaDaoTaoTt.GhiChu = CoreXmlUtility.GetString(dr, "GHICHU", true);
            OChuyenKhoaDaoTaoTt.ChaId = CoreXmlUtility.GetString(dr, "CHAID", true);
            return OChuyenKhoaDaoTaoTt;
        }

        public static DM_ChuyenKhoaDaoTaoTtCls[] ParseFromDataTable(DataTable dtTable)
        {
            DM_ChuyenKhoaDaoTaoTtCls[] ChuyenKhoaDaoTaoTts = new DM_ChuyenKhoaDaoTaoTtCls[dtTable.Rows.Count];
            for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
            {
                ChuyenKhoaDaoTaoTts[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
            }
            return ChuyenKhoaDaoTaoTts;
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


        public static DM_ChuyenKhoaDaoTaoTtCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            DM_ChuyenKhoaDaoTaoTtCls[] ChuyenKhoaDaoTaoTts = ParseFromDataTable(ds.Tables[0]);
            return ChuyenKhoaDaoTaoTts;
        }


        public static DM_ChuyenKhoaDaoTaoTtCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OChuyenKhoaDaoTaoTt;
        }


        public static XmlCls GetXml(DM_ChuyenKhoaDaoTaoTtCls[] ChuyenKhoaDaoTaoTts)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("info");
            ds.Tables["info"].Columns.Add("ID");
            ds.Tables["info"].Columns.Add("MA");
            ds.Tables["info"].Columns.Add("TEN");
            ds.Tables["info"].Columns.Add("TenNgan");
            ds.Tables["info"].Columns.Add("MOTA");
            ds.Tables["info"].Columns.Add("HIEULUC", typeof(int));
            ds.Tables["info"].Columns.Add("STT", typeof(int));
            ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
            ds.Tables["info"].Columns.Add("TUNGAY", typeof(DateTime?));
            ds.Tables["info"].Columns.Add("DENNGAY", typeof(DateTime?));
            ds.Tables["info"].Columns.Add("GHICHU");
            ds.Tables["info"].Columns.Add("CHAID");
            for (int iIndex = 0; iIndex < ChuyenKhoaDaoTaoTts.Length; iIndex++)
            {
                ds.Tables["info"].Rows.Add(new object[]
                {
                ChuyenKhoaDaoTaoTts[iIndex].Id,
                ChuyenKhoaDaoTaoTts[iIndex].Ma,
                ChuyenKhoaDaoTaoTts[iIndex].Ten,
                ChuyenKhoaDaoTaoTts[iIndex].TenNgan,
                ChuyenKhoaDaoTaoTts[iIndex].MoTa,
                ChuyenKhoaDaoTaoTts[iIndex].HieuLuc,
                ChuyenKhoaDaoTaoTts[iIndex].Stt,
                ChuyenKhoaDaoTaoTts[iIndex].NgayTao,
                ChuyenKhoaDaoTaoTts[iIndex].TuNgay,
                ChuyenKhoaDaoTaoTts[iIndex].DenNgay,
                ChuyenKhoaDaoTaoTts[iIndex].GhiChu,
                ChuyenKhoaDaoTaoTts[iIndex].ChaId
                });
            }
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }


        public static XmlCls GetXml(DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("info");
            ds.Tables["info"].Columns.Add("ID");
            ds.Tables["info"].Columns.Add("MA");
            ds.Tables["info"].Columns.Add("TEN");
            ds.Tables["info"].Columns.Add("TenNgan");
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
                OChuyenKhoaDaoTaoTt.Id,
                OChuyenKhoaDaoTaoTt.Ma,
                OChuyenKhoaDaoTaoTt.Ten,
                OChuyenKhoaDaoTaoTt.TenNgan,
                OChuyenKhoaDaoTaoTt.MoTa,
                OChuyenKhoaDaoTaoTt.HieuLuc,
                OChuyenKhoaDaoTaoTt.Stt,
                OChuyenKhoaDaoTaoTt.NgayTao,
                OChuyenKhoaDaoTaoTt.TuNgay,
                OChuyenKhoaDaoTaoTt.DenNgay,
                OChuyenKhoaDaoTaoTt.GhiChu,
                OChuyenKhoaDaoTaoTt.ChaId
            });
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
