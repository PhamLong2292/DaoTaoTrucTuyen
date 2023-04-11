using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DM_YKienBenhVienCls
    {
        public string Id;
        public string Ma = "";
        public string Ten = "";
        public string NoiDung = "";
        public string MoTa = "";
        public int HieuLuc;
        public int Stt;
        public DateTime NgayTao;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
        public string GhiChu = "";
        public string ChaId;

        public static string REDISNAME = "YKienBenhVien";

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

    public class DM_YKienBenhVienParser
    {
        public static DM_YKienBenhVienCls CreateInstance()
        {
            DM_YKienBenhVienCls OYKienBenhVien = new DM_YKienBenhVienCls();
            return OYKienBenhVien;
        }


        public static DM_YKienBenhVienCls ParseFromDataRow(DataRow dr)
        {
            DM_YKienBenhVienCls OYKienBenhVien = new DM_YKienBenhVienCls();
            OYKienBenhVien.Id = CoreXmlUtility.GetString(dr, "ID", true);
            OYKienBenhVien.Ma = CoreXmlUtility.GetString(dr, "MA", true);
            OYKienBenhVien.Ten = CoreXmlUtility.GetString(dr, "TEN", true);
            OYKienBenhVien.NoiDung = CoreXmlUtility.GetString(dr, "NOIDUNG", true);
            OYKienBenhVien.MoTa = CoreXmlUtility.GetString(dr, "MOTA", true);
            OYKienBenhVien.HieuLuc = CoreXmlUtility.GetInt(dr, "HIEULUC", true);
            OYKienBenhVien.Stt = CoreXmlUtility.GetInt(dr, "STT", true);
            OYKienBenhVien.NgayTao = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
            OYKienBenhVien.TuNgay = dr["TUNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "TUNGAY", true);
            OYKienBenhVien.DenNgay = dr["DENNGAY"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "DENNGAY", true);
            OYKienBenhVien.GhiChu = CoreXmlUtility.GetString(dr, "GHICHU", true);
            OYKienBenhVien.ChaId = CoreXmlUtility.GetString(dr, "CHAID", true);
            return OYKienBenhVien;
        }

        public static DM_YKienBenhVienCls[] ParseFromDataTable(DataTable dtTable)
        {
            DM_YKienBenhVienCls[] YKienBenhViens = new DM_YKienBenhVienCls[dtTable.Rows.Count];
            for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
            {
                YKienBenhViens[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
            }
            return YKienBenhViens;
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


        public static DM_YKienBenhVienCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            DM_YKienBenhVienCls[] YKienBenhViens = ParseFromDataTable(ds.Tables[0]);
            return YKienBenhViens;
        }


        public static DM_YKienBenhVienCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_YKienBenhVienCls OYKienBenhVien = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OYKienBenhVien;
        }


        public static XmlCls GetXml(DM_YKienBenhVienCls[] YKienBenhViens)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("info");
            ds.Tables["info"].Columns.Add("ID");
            ds.Tables["info"].Columns.Add("MA");
            ds.Tables["info"].Columns.Add("TEN");
            ds.Tables["info"].Columns.Add("NOIDUNG");
            ds.Tables["info"].Columns.Add("MOTA");
            ds.Tables["info"].Columns.Add("HIEULUC", typeof(int));
            ds.Tables["info"].Columns.Add("STT", typeof(int));
            ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
            ds.Tables["info"].Columns.Add("TUNGAY", typeof(DateTime?));
            ds.Tables["info"].Columns.Add("DENNGAY", typeof(DateTime?));
            ds.Tables["info"].Columns.Add("GHICHU");
            ds.Tables["info"].Columns.Add("CHAID");
            for (int iIndex = 0; iIndex < YKienBenhViens.Length; iIndex++)
            {
                ds.Tables["info"].Rows.Add(new object[]
                {
                YKienBenhViens[iIndex].Id,
                YKienBenhViens[iIndex].Ma,
                YKienBenhViens[iIndex].Ten,
                YKienBenhViens[iIndex].NoiDung,
                YKienBenhViens[iIndex].MoTa,
                YKienBenhViens[iIndex].HieuLuc,
                YKienBenhViens[iIndex].Stt,
                YKienBenhViens[iIndex].NgayTao,
                YKienBenhViens[iIndex].TuNgay,
                YKienBenhViens[iIndex].DenNgay,
                YKienBenhViens[iIndex].GhiChu,
                YKienBenhViens[iIndex].ChaId
                });
            }
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }


        public static XmlCls GetXml(DM_YKienBenhVienCls OYKienBenhVien)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("info");
            ds.Tables["info"].Columns.Add("ID");
            ds.Tables["info"].Columns.Add("MA");
            ds.Tables["info"].Columns.Add("TEN");
            ds.Tables["info"].Columns.Add("NOIDUNG");
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
                OYKienBenhVien.Id,
                OYKienBenhVien.Ma,
                OYKienBenhVien.Ten,
                OYKienBenhVien.NoiDung,
                OYKienBenhVien.MoTa,
                OYKienBenhVien.HieuLuc,
                OYKienBenhVien.Stt,
                OYKienBenhVien.NgayTao,
                OYKienBenhVien.TuNgay,
                OYKienBenhVien.DenNgay,
                OYKienBenhVien.GhiChu,
                OYKienBenhVien.ChaId
            });
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
