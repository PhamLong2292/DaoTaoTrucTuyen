using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_GiayToDiChuyenGiaoCls
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
        public static string REDISNAME = "GiayToDiChuyenGiao";

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

    public class DM_GiayToDiChuyenGiaoParser
    {
        public static DM_GiayToDiChuyenGiaoCls CreateInstance()
        {
            DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao = new DM_GiayToDiChuyenGiaoCls();
            return OGiayToDiChuyenGiao;
        }


        public static DM_GiayToDiChuyenGiaoCls ParseFromDataRow(DataRow dr)
        {
            DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao = new DM_GiayToDiChuyenGiaoCls();
            OGiayToDiChuyenGiao.Id = CoreXmlUtility.GetString(dr, "ID", true);
            OGiayToDiChuyenGiao.Ma = CoreXmlUtility.GetString(dr, "MA", true);
            OGiayToDiChuyenGiao.Ten = CoreXmlUtility.GetString(dr, "TEN", true);
            OGiayToDiChuyenGiao.MoTa = CoreXmlUtility.GetString(dr, "MOTA", true);
            OGiayToDiChuyenGiao.HieuLuc = CoreXmlUtility.GetInt(dr, "HIEULUC", true);
            OGiayToDiChuyenGiao.Stt = CoreXmlUtility.GetInt(dr, "STT", true);
            OGiayToDiChuyenGiao.NgayTao = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
            OGiayToDiChuyenGiao.TuNgay = dr["TUNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "TUNGAY", true);
            OGiayToDiChuyenGiao.DenNgay = dr["DENNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "DENNGAY", true);
            OGiayToDiChuyenGiao.GhiChu = CoreXmlUtility.GetString(dr, "GHICHU", true);
            OGiayToDiChuyenGiao.ChaId = CoreXmlUtility.GetString(dr, "CHAID", true);
            return OGiayToDiChuyenGiao;
        }

        public static DM_GiayToDiChuyenGiaoCls[] ParseFromDataTable(DataTable dtTable)
        {
            DM_GiayToDiChuyenGiaoCls[] GiayToDiChuyenGiaos = new DM_GiayToDiChuyenGiaoCls[dtTable.Rows.Count];
            for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
            {
                GiayToDiChuyenGiaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
            }
            return GiayToDiChuyenGiaos;
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


        public static DM_GiayToDiChuyenGiaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            DM_GiayToDiChuyenGiaoCls[] GiayToDiChuyenGiaos = ParseFromDataTable(ds.Tables[0]);
            return GiayToDiChuyenGiaos;
        }


        public static DM_GiayToDiChuyenGiaoCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OGiayToDiChuyenGiao;
        }


        public static XmlCls GetXml(DM_GiayToDiChuyenGiaoCls[] GiayToDiChuyenGiaos)
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
            for (int iIndex = 0; iIndex < GiayToDiChuyenGiaos.Length; iIndex++)
            {
                ds.Tables["info"].Rows.Add(new object[]
                {
                GiayToDiChuyenGiaos[iIndex].Id,
                GiayToDiChuyenGiaos[iIndex].Ma,
                GiayToDiChuyenGiaos[iIndex].Ten,
                GiayToDiChuyenGiaos[iIndex].MoTa,
                GiayToDiChuyenGiaos[iIndex].HieuLuc,
                GiayToDiChuyenGiaos[iIndex].Stt,
                GiayToDiChuyenGiaos[iIndex].NgayTao,
                GiayToDiChuyenGiaos[iIndex].TuNgay,
                GiayToDiChuyenGiaos[iIndex].DenNgay,
                GiayToDiChuyenGiaos[iIndex].GhiChu,
                GiayToDiChuyenGiaos[iIndex].ChaId
                });
            }
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }


        public static XmlCls GetXml(DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao)
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
                OGiayToDiChuyenGiao.Id,
                OGiayToDiChuyenGiao.Ma,
                OGiayToDiChuyenGiao.Ten,
                OGiayToDiChuyenGiao.MoTa,
                OGiayToDiChuyenGiao.HieuLuc,
                OGiayToDiChuyenGiao.Stt,
                OGiayToDiChuyenGiao.NgayTao,
                OGiayToDiChuyenGiao.TuNgay,
                OGiayToDiChuyenGiao.DenNgay,
                OGiayToDiChuyenGiao.GhiChu,
                OGiayToDiChuyenGiao.ChaId
            });
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
