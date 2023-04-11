using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class LichHoiChanCls
    {
        public string ID;
        public DateTime? BATDAU;
        public DateTime? KETTHUC;
        public string DIADIEM = "";
        public string THUKY = "";
        public string CHUTRI = "";
        public int TRANGTHAI;
        public string TAOBOI = "";
        public DateTime TAOVAO;
        public string GHICHU = "";
        public string DUYETBOI = "";
        public DateTime? DUYETVAO;
        public string CHUYENKHOAMA = "";
        public enum eTrangThai
        {
            Moi = 0,
            ChoDuyet,
            DaDuyet,
            TuChoiDuyet,
            HoanTat,
            Huy = 1000
        };
        public enum ePermission
        {
            Xem = 0,
            Them,
            Sua,
            Xoa,
            ChuyenDuyet,
            Huy = 1000
        };
    }
}

public class LichHoiChanParser
{
    public static LichHoiChanCls CreateInstance()
    {
        LichHoiChanCls OLichHoiChan = new LichHoiChanCls();
        return OLichHoiChan;
    }
    public static Dictionary<int, string> sTrangThai = new Dictionary<int, string>()
        {
            {(int)LichHoiChanCls.eTrangThai.Moi, "Mới" },
            {(int)LichHoiChanCls.eTrangThai.ChoDuyet, "Chờ duyệt" },
            {(int)LichHoiChanCls.eTrangThai.DaDuyet, "Đã duyệt" },
            {(int)LichHoiChanCls.eTrangThai.TuChoiDuyet, "Từ chối duyệt" },
            {(int)LichHoiChanCls.eTrangThai.HoanTat, "Hoàn tất" },
            {(int)LichHoiChanCls.eTrangThai.Huy, "Hủy" },
        };
    public static Dictionary<int, string> sColorTrangThai = new Dictionary<int, string>()
        {
            {(int)LichHoiChanCls.eTrangThai.Moi, "<span style=\"background-color:#FFFFFF; color:black;\" class=\"badge\">Mới</span>" },
            {(int)LichHoiChanCls.eTrangThai.ChoDuyet, "<span style=\"background-color:#f7ea4a; color:black;\" class=\"badge\">Chờ duyệt</span>" },
            {(int)LichHoiChanCls.eTrangThai.DaDuyet, "<span style=\"background-color:green; color:black;\" class=\"badge\">Đã duyệt</span>" },
            {(int)LichHoiChanCls.eTrangThai.TuChoiDuyet, "<span style=\"background-color:red; color:black;\" class=\"badge\">Từ chối duyệt</span>" },
            {(int)LichHoiChanCls.eTrangThai.HoanTat, "<span style=\"background-color:blue; color:black;\" class=\"badge\">Hoàn tất</span>" },
            {(int)LichHoiChanCls.eTrangThai.Huy, "<span style=\"background-color:gray; color:black;\" class=\"badge\">Hủy</span>" },
        };
    public static LichHoiChanCls ParseFromDataRow(DataRow dr)
    {
        LichHoiChanCls OLichHoiChan = new LichHoiChanCls();
        OLichHoiChan.ID = CoreXmlUtility.GetString(dr, "ID", true);
        //OLichHoiChan.BATDAU = CoreXmlUtility.GetDate(dr, "BATDAU", true);
        //OLichHoiChan.KETTHUC = CoreXmlUtility.GetDate(dr, "KETTHUC", true);
        OLichHoiChan.BATDAU = dr["BATDAU"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "BATDAU", true);
        OLichHoiChan.KETTHUC = dr["KETTHUC"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "KETTHUC", true);
        OLichHoiChan.DIADIEM = CoreXmlUtility.GetString(dr, "DIADIEM", true);
        OLichHoiChan.THUKY = CoreXmlUtility.GetString(dr, "THUKY", true);
        OLichHoiChan.CHUTRI = CoreXmlUtility.GetString(dr, "CHUTRI", true);
        OLichHoiChan.TRANGTHAI = CoreXmlUtility.GetInt(dr, "TRANGTHAI", true);
        OLichHoiChan.TAOBOI = CoreXmlUtility.GetString(dr, "TAOBOI", true);
        OLichHoiChan.TAOVAO = CoreXmlUtility.GetDate(dr, "TAOVAO", true);
        OLichHoiChan.GHICHU = CoreXmlUtility.GetString(dr, "GHICHU", true);
        OLichHoiChan.DUYETBOI = CoreXmlUtility.GetString(dr, "DUYETBOI", true);
        //OLichHoiChan.DUYETVAO = CoreXmlUtility.GetDate(dr, "DUYETVAO", true);
        OLichHoiChan.DUYETVAO = dr["DUYETVAO"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "DUYETVAO", true);
        OLichHoiChan.CHUYENKHOAMA = CoreXmlUtility.GetString(dr, "CHUYENKHOAMA", true);
        return OLichHoiChan;
    }
    public static LichHoiChanCls[] ParseFromDataTable(DataTable dtTable)
    {
        LichHoiChanCls[] LichHoiChans = new LichHoiChanCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            LichHoiChans[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return LichHoiChans;
    }
    public static LichHoiChanCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        LichHoiChanCls[] LichHoiChans = ParseFromDataTable(ds.Tables[0]);
        return LichHoiChans;
    }
    public static LichHoiChanCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        LichHoiChanCls OLichHoiChan = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OLichHoiChan;
    }
    public static XmlCls GetXml(LichHoiChanCls[] LichHoiChans)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BATDAU", typeof(DateTime));
        ds.Tables["info"].Columns.Add("KETTHUC", typeof(DateTime));
        ds.Tables["info"].Columns.Add("DIADIEM");
        ds.Tables["info"].Columns.Add("THUKY");
        ds.Tables["info"].Columns.Add("CHUTRI");
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Columns.Add("DUYETBOI");
        ds.Tables["info"].Columns.Add("DUYETVAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("CHUYENKHOAMA");
        for (int iIndex = 0; iIndex < LichHoiChans.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                LichHoiChans[iIndex].ID,
                LichHoiChans[iIndex].BATDAU,
                LichHoiChans[iIndex].KETTHUC,
                LichHoiChans[iIndex].DIADIEM,
                LichHoiChans[iIndex].THUKY,
                LichHoiChans[iIndex].CHUTRI,
                LichHoiChans[iIndex].TRANGTHAI,
                LichHoiChans[iIndex].TAOBOI,
                LichHoiChans[iIndex].TAOVAO,
                LichHoiChans[iIndex].GHICHU,
                LichHoiChans[iIndex].DUYETBOI,
                LichHoiChans[iIndex].DUYETVAO,
                LichHoiChans[iIndex].CHUYENKHOAMA

            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
    public static XmlCls GetXml(LichHoiChanCls OLichHoiChan)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BATDAU", typeof(DateTime));
        ds.Tables["info"].Columns.Add("KETTHUC", typeof(DateTime));
        ds.Tables["info"].Columns.Add("DIADIEM");
        ds.Tables["info"].Columns.Add("THUKY");
        ds.Tables["info"].Columns.Add("CHUTRI");
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("GHICHU");
        ds.Tables["info"].Columns.Add("DUYETBOI");
        ds.Tables["info"].Columns.Add("DUYETVAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("CHUYENKHOAMA");
        ds.Tables["info"].Rows.Add(new object[]
            {
                OLichHoiChan.ID,
                OLichHoiChan.BATDAU,
                OLichHoiChan.KETTHUC,
                OLichHoiChan.DIADIEM,
                OLichHoiChan.THUKY,
                OLichHoiChan.CHUTRI,
                OLichHoiChan.TRANGTHAI,
                OLichHoiChan.TAOBOI,
                OLichHoiChan.TAOVAO,
                OLichHoiChan.GHICHU,
                OLichHoiChan.DUYETBOI,
                OLichHoiChan.DUYETVAO,
                OLichHoiChan.CHUYENKHOAMA
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
