using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Model
{
    public class DM_KyThuatChuyenGiaoCls
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
        public static string REDISNAME = "KyThuatChuyenGiao";

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

    public class DM_KyThuatChuyenGiaoParser
    {
        public static DM_KyThuatChuyenGiaoCls CreateInstance()
        {
            DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao = new DM_KyThuatChuyenGiaoCls();
            return OKyThuatChuyenGiao;
        }


        public static DM_KyThuatChuyenGiaoCls ParseFromDataRow(DataRow dr)
        {
            DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao = new DM_KyThuatChuyenGiaoCls();
            OKyThuatChuyenGiao.Id = CoreXmlUtility.GetString(dr, "ID", true);
            OKyThuatChuyenGiao.Ma = CoreXmlUtility.GetString(dr, "MA", true);
            OKyThuatChuyenGiao.Ten = CoreXmlUtility.GetString(dr, "TEN", true);
            OKyThuatChuyenGiao.MoTa = CoreXmlUtility.GetString(dr, "MOTA", true);
            OKyThuatChuyenGiao.HieuLuc = CoreXmlUtility.GetInt(dr, "HIEULUC", true);
            OKyThuatChuyenGiao.Stt = CoreXmlUtility.GetInt(dr, "STT", true);
            OKyThuatChuyenGiao.NgayTao = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
            OKyThuatChuyenGiao.TuNgay = dr["TUNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "TUNGAY", true);
            OKyThuatChuyenGiao.DenNgay = dr["DENNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "DENNGAY", true);
            OKyThuatChuyenGiao.GhiChu = CoreXmlUtility.GetString(dr, "GHICHU", true);
            OKyThuatChuyenGiao.ChaId = CoreXmlUtility.GetString(dr, "CHAID", true);
            return OKyThuatChuyenGiao;
        }

        public static DM_KyThuatChuyenGiaoCls[] ParseFromDataTable(DataTable dtTable)
        {
            DM_KyThuatChuyenGiaoCls[] KyThuatChuyenGiaos = new DM_KyThuatChuyenGiaoCls[dtTable.Rows.Count];
            for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
            {
                KyThuatChuyenGiaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
            }
            return KyThuatChuyenGiaos;
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


        public static DM_KyThuatChuyenGiaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            DM_KyThuatChuyenGiaoCls[] KyThuatChuyenGiaos = ParseFromDataTable(ds.Tables[0]);
            return KyThuatChuyenGiaos;
        }


        public static DM_KyThuatChuyenGiaoCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OKyThuatChuyenGiao;
        }


        public static XmlCls GetXml(DM_KyThuatChuyenGiaoCls[] KyThuatChuyenGiaos)
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
            for (int iIndex = 0; iIndex < KyThuatChuyenGiaos.Length; iIndex++)
            {
                ds.Tables["info"].Rows.Add(new object[]
                {
                KyThuatChuyenGiaos[iIndex].Id,
                KyThuatChuyenGiaos[iIndex].Ma,
                KyThuatChuyenGiaos[iIndex].Ten,
                KyThuatChuyenGiaos[iIndex].MoTa,
                KyThuatChuyenGiaos[iIndex].HieuLuc,
                KyThuatChuyenGiaos[iIndex].Stt,
                KyThuatChuyenGiaos[iIndex].NgayTao,
                KyThuatChuyenGiaos[iIndex].TuNgay,
                KyThuatChuyenGiaos[iIndex].DenNgay,
                KyThuatChuyenGiaos[iIndex].GhiChu,
                KyThuatChuyenGiaos[iIndex].ChaId
                });
            }
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }


        public static XmlCls GetXml(DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao)
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
                OKyThuatChuyenGiao.Id,
                OKyThuatChuyenGiao.Ma,
                OKyThuatChuyenGiao.Ten,
                OKyThuatChuyenGiao.MoTa,
                OKyThuatChuyenGiao.HieuLuc,
                OKyThuatChuyenGiao.Stt,
                OKyThuatChuyenGiao.NgayTao,
                OKyThuatChuyenGiao.TuNgay,
                OKyThuatChuyenGiao.DenNgay,
                OKyThuatChuyenGiao.GhiChu,
                OKyThuatChuyenGiao.ChaId
            });
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
