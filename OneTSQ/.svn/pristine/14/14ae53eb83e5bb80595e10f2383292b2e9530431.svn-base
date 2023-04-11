using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_LichChuyenGiaoCls
    {
        public string ID;
        public string KYTHUAT_MA = "";
        public string KHOAHOC_ID = "";
        public string BENHVIEN_MA = "";
        public string LANHDAOBENHVIEN_ID = "";
        public string BACSY_ID = "";
        public DateTime BATDAU;
        public DateTime KETTHUC;
        public int? COGIAYDIDUONG;
        public string GIAYTO = "";
        public string NGUOITAO_ID = "";
        public DateTime NGAYTAO;
        public string NGUOISUA_ID = "";
        public DateTime? NGAYSUA;
        public int TRANGTHAI;
        public enum eTrangThai
        {
            Moi = 0,
            ChoDuyet,
            Duyet,
            TuChoi,
            HoanTat
        }
        public enum ePermission
        {
            Xem = 0,
            Them,
            Sua,
            Xoa,
            GuiDuyet,
            PheDuyet
        }
    }
}

public class DT_LichChuyenGiaoParser
{
    public readonly static Dictionary<int, string> TrangThais = new Dictionary<int, string>()
        {
            { (int)DT_LichChuyenGiaoCls.eTrangThai.Moi, "Mới" },
            { (int)DT_LichChuyenGiaoCls.eTrangThai.ChoDuyet, "Chờ duyệt" },
            { (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet, "Duyệt" },
            { (int)DT_LichChuyenGiaoCls.eTrangThai.TuChoi, "Từ chối duyệt" },
            { (int)DT_LichChuyenGiaoCls.eTrangThai.HoanTat, "Hoàn tất" }
        };
    public readonly static Dictionary<int, string> sColorTrangThai = new Dictionary<int, string>()
        {
            { (int)DT_LichChuyenGiaoCls.eTrangThai.Moi, "<span style=\"background-color:orange;color:white;\" class=\"badge\" >Mới</span>" },
            { (int)DT_LichChuyenGiaoCls.eTrangThai.ChoDuyet, "<span style=\"background-color:#71e038cf; color:black;\" class=\"badge\">Chờ duyệt</span>" },
            { (int)DT_LichChuyenGiaoCls.eTrangThai.Duyet, "<span style=\"background-color:rgb(31, 119, 180);color:white;\" class=\"badge\" >Duyệt</span>" },
            { (int)DT_LichChuyenGiaoCls.eTrangThai.TuChoi, "<span style=\"background-color:rgb(188, 189, 34);color:white;\" class=\"badge\" >Từ chối duyệt</span>" },
            { (int)DT_LichChuyenGiaoCls.eTrangThai.HoanTat, "<span style=\"background-color:#149F5B;color:white;\" class=\"badge\" >Hoàn tất</span>" }
        };
    public static DT_LichChuyenGiaoCls CreateInstance()
    {
        DT_LichChuyenGiaoCls ODT_LichChuyenGiao = new DT_LichChuyenGiaoCls();
        return ODT_LichChuyenGiao;
    }


    public static DT_LichChuyenGiaoCls ParseFromDataRow(DataRow dr)
    {
        DT_LichChuyenGiaoCls ODT_LichChuyenGiao = new DT_LichChuyenGiaoCls();
        ODT_LichChuyenGiao.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_LichChuyenGiao.KYTHUAT_MA = CoreXmlUtility.GetString(dr, "KYTHUAT_MA", true);
        ODT_LichChuyenGiao.KHOAHOC_ID = CoreXmlUtility.GetString(dr, "KHOAHOC_ID", true);
        ODT_LichChuyenGiao.BENHVIEN_MA = CoreXmlUtility.GetString(dr, "BENHVIEN_MA", true);
        ODT_LichChuyenGiao.LANHDAOBENHVIEN_ID = CoreXmlUtility.GetString(dr, "LANHDAOBENHVIEN_ID", true);
        ODT_LichChuyenGiao.BACSY_ID = CoreXmlUtility.GetString(dr, "BACSY_ID", true);
        ODT_LichChuyenGiao.BATDAU = CoreXmlUtility.GetDate(dr, "BATDAU", true);
        ODT_LichChuyenGiao.KETTHUC = CoreXmlUtility.GetDate(dr, "KETTHUC", true);
        ODT_LichChuyenGiao.COGIAYDIDUONG = CoreXmlUtility.GetIntOrNull(dr, "COGIAYDIDUONG", true);
        ODT_LichChuyenGiao.GIAYTO = CoreXmlUtility.GetString(dr, "GIAYTO", true);
        ODT_LichChuyenGiao.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_LichChuyenGiao.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        ODT_LichChuyenGiao.NGUOISUA_ID = CoreXmlUtility.GetString(dr, "NGUOISUA_ID", true);
        ODT_LichChuyenGiao.NGAYSUA = CoreXmlUtility.GetDateOrNull(dr, "NGAYSUA", true);
        ODT_LichChuyenGiao.TRANGTHAI = CoreXmlUtility.GetInt(dr, "TRANGTHAI", true);
        return ODT_LichChuyenGiao;
    }

    public static DT_LichChuyenGiaoCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_LichChuyenGiaoCls[] DT_LichChuyenGiaos = new DT_LichChuyenGiaoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_LichChuyenGiaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_LichChuyenGiaos;
    }


    public static DT_LichChuyenGiaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_LichChuyenGiaoCls[] DT_LichChuyenGiaos = ParseFromDataTable(ds.Tables[0]);
        return DT_LichChuyenGiaos;
    }


    public static DT_LichChuyenGiaoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_LichChuyenGiaoCls ODT_LichChuyenGiao = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_LichChuyenGiao;
    }


    public static XmlCls GetXml(DT_LichChuyenGiaoCls[] DT_LichChuyenGiaos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("KYTHUAT_MA");
        ds.Tables["info"].Columns.Add("KHOAHOC_ID");
        ds.Tables["info"].Columns.Add("BENHVIEN_MA");
        ds.Tables["info"].Columns.Add("LANHDAOBENHVIEN_ID");
        ds.Tables["info"].Columns.Add("BACSY_ID");
        ds.Tables["info"].Columns.Add("BATDAU", typeof(DateTime));
        ds.Tables["info"].Columns.Add("KETTHUC", typeof(DateTime));
        ds.Tables["info"].Columns.Add("COGIAYDIDUONG", typeof(int?));
        ds.Tables["info"].Columns.Add("GIAYTO");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        for (int iIndex = 0; iIndex < DT_LichChuyenGiaos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_LichChuyenGiaos[iIndex].ID,
                DT_LichChuyenGiaos[iIndex].KYTHUAT_MA,
                DT_LichChuyenGiaos[iIndex].KHOAHOC_ID,
                DT_LichChuyenGiaos[iIndex].BENHVIEN_MA,
                DT_LichChuyenGiaos[iIndex].LANHDAOBENHVIEN_ID,
                DT_LichChuyenGiaos[iIndex].BACSY_ID,
                DT_LichChuyenGiaos[iIndex].BATDAU,
                DT_LichChuyenGiaos[iIndex].KETTHUC,
                DT_LichChuyenGiaos[iIndex].COGIAYDIDUONG,
                DT_LichChuyenGiaos[iIndex].GIAYTO,
                DT_LichChuyenGiaos[iIndex].NGUOITAO_ID,
                DT_LichChuyenGiaos[iIndex].NGAYTAO,
                DT_LichChuyenGiaos[iIndex].NGUOISUA_ID,
                DT_LichChuyenGiaos[iIndex].NGAYSUA,
                DT_LichChuyenGiaos[iIndex].TRANGTHAI
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_LichChuyenGiaoCls ODT_LichChuyenGiao)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("KYTHUAT_MA");
        ds.Tables["info"].Columns.Add("KHOAHOC_ID");
        ds.Tables["info"].Columns.Add("BENHVIEN_MA");
        ds.Tables["info"].Columns.Add("LANHDAOBENHVIEN_ID");
        ds.Tables["info"].Columns.Add("BACSY_ID");
        ds.Tables["info"].Columns.Add("BATDAU", typeof(DateTime));
        ds.Tables["info"].Columns.Add("KETTHUC", typeof(DateTime));
        ds.Tables["info"].Columns.Add("COGIAYDIDUONG", typeof(int?));
        ds.Tables["info"].Columns.Add("GIAYTO");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_LichChuyenGiao.ID,
                ODT_LichChuyenGiao.KYTHUAT_MA,
                ODT_LichChuyenGiao.KHOAHOC_ID,
                ODT_LichChuyenGiao.BENHVIEN_MA,
                ODT_LichChuyenGiao.LANHDAOBENHVIEN_ID,
                ODT_LichChuyenGiao.BACSY_ID,
                ODT_LichChuyenGiao.BATDAU,
                ODT_LichChuyenGiao.KETTHUC,
                ODT_LichChuyenGiao.COGIAYDIDUONG,
                ODT_LichChuyenGiao.GIAYTO,
                ODT_LichChuyenGiao.NGUOITAO_ID,
                ODT_LichChuyenGiao.NGAYTAO,
                ODT_LichChuyenGiao.NGUOISUA_ID,
                ODT_LichChuyenGiao.NGAYSUA,
                ODT_LichChuyenGiao.TRANGTHAI
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
