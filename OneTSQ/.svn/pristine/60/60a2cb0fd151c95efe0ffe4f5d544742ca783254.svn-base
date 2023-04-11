using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class PhieuDanhGiaChatLuongDaoTaoCls
    {
        public string ID;
        public int QUY;
        public int NAM;
        public string BENHVIENTHAMVAN_ID;
        public string BENHVIENTUVAN_ID;
        public int? SOBUOIBAOCAOTHAMGIA;
        public string LYDODIEMYNGHIA;
        public string YKIENDONGGOP;
        public string HOTENNGUOILAPPHIEU;
        public string SODIENTHOAI;
        public string TAOBOI;
        public DateTime TAOVAO;
        public string SUABOI;
        public DateTime? SUAVAO;
        public int TRANGTHAI;

        public enum eTrangThai
        {
            Moi = 0,
            DaGui = 1,
        }
        public enum ePermission
        {
            Xem = 0,
            Them = 1,
            Sua = 2,
            Xoa = 3,
            Gui = 4,
            TongHop = 5
        }
        public enum eQuy
        {
            I = 1,
            II = 2,
            III = 3,
            IV = 4
        }
    }
}

public class PhieuDanhGiaChatLuongDaoTaoParser
{
    public readonly static Dictionary<int, string> TrangThais = new Dictionary<int, string>()
        {
            { (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.Moi, "Mới" },
            { (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.DaGui, "Đã gửi" }
        };
    public readonly static Dictionary<int, string> sColorTrangThai = new Dictionary<int, string>()
        {
            { (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.Moi, "<span style=\"background-color:orange;color:white;\" class=\"badge\" >Mới</span>" },
            { (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.DaGui, "<span style=\"background-color:blue;color:white;\" class=\"badge\" >Đã gửi</span>" },
        };
    public readonly static Dictionary<int, string> Quys = new Dictionary<int, string>()
        {
            { (int)BcDanhGiaChatLuongDaoTaoCls.eQuy.I, "I" },
            { (int)BcDanhGiaChatLuongDaoTaoCls.eQuy.II, "II" },
            { (int)BcDanhGiaChatLuongDaoTaoCls.eQuy.III, "III" },
            { (int)BcDanhGiaChatLuongDaoTaoCls.eQuy.IV, "IV" }
        };
    public static PhieuDanhGiaChatLuongDaoTaoCls CreateInstance()
    {
        PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao = new PhieuDanhGiaChatLuongDaoTaoCls();
        return OPhieuDanhGiaChatLuongDaoTao;
    }


    public static PhieuDanhGiaChatLuongDaoTaoCls ParseFromDataRow(DataRow dr)
    {
        PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao = new PhieuDanhGiaChatLuongDaoTaoCls();
        OPhieuDanhGiaChatLuongDaoTao.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OPhieuDanhGiaChatLuongDaoTao.QUY = CoreXmlUtility.GetInt(dr, "QUY", true);
        OPhieuDanhGiaChatLuongDaoTao.NAM = CoreXmlUtility.GetInt(dr, "NAM", true);
        OPhieuDanhGiaChatLuongDaoTao.BENHVIENTHAMVAN_ID = CoreXmlUtility.GetString(dr, "BENHVIENTHAMVAN_ID", true);
        OPhieuDanhGiaChatLuongDaoTao.BENHVIENTUVAN_ID = CoreXmlUtility.GetString(dr, "BENHVIENTUVAN_ID", true);
        OPhieuDanhGiaChatLuongDaoTao.SOBUOIBAOCAOTHAMGIA = CoreXmlUtility.GetIntOrNull(dr, "SOBUOIBAOCAOTHAMGIA", true);
        OPhieuDanhGiaChatLuongDaoTao.LYDODIEMYNGHIA = CoreXmlUtility.GetString(dr, "LYDODIEMYNGHIA", true);
        OPhieuDanhGiaChatLuongDaoTao.YKIENDONGGOP = CoreXmlUtility.GetString(dr, "YKIENDONGGOP", true);
        OPhieuDanhGiaChatLuongDaoTao.HOTENNGUOILAPPHIEU = CoreXmlUtility.GetString(dr, "HOTENNGUOILAPPHIEU", true);
        OPhieuDanhGiaChatLuongDaoTao.SODIENTHOAI = CoreXmlUtility.GetString(dr, "SODIENTHOAI", true);
        OPhieuDanhGiaChatLuongDaoTao.TAOBOI = CoreXmlUtility.GetString(dr, "TAOBOI", true);
        OPhieuDanhGiaChatLuongDaoTao.TAOVAO = CoreXmlUtility.GetDate(dr, "TAOVAO", true);
        OPhieuDanhGiaChatLuongDaoTao.SUABOI = CoreXmlUtility.GetString(dr, "SUABOI", true);
        OPhieuDanhGiaChatLuongDaoTao.SUAVAO = CoreXmlUtility.GetDateOrNull(dr, "SUAVAO", true);
        OPhieuDanhGiaChatLuongDaoTao.TRANGTHAI = CoreXmlUtility.GetInt(dr, "TRANGTHAI", true);
        return OPhieuDanhGiaChatLuongDaoTao;
    }

    public static PhieuDanhGiaChatLuongDaoTaoCls[] ParseFromDataTable(DataTable dtTable)
    {
        PhieuDanhGiaChatLuongDaoTaoCls[] PhieuDanhGiaChatLuongDaoTaos = new PhieuDanhGiaChatLuongDaoTaoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            PhieuDanhGiaChatLuongDaoTaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return PhieuDanhGiaChatLuongDaoTaos;
    }


    public static PhieuDanhGiaChatLuongDaoTaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        PhieuDanhGiaChatLuongDaoTaoCls[] PhieuDanhGiaChatLuongDaoTaos = ParseFromDataTable(ds.Tables[0]);
        return PhieuDanhGiaChatLuongDaoTaos;
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

    public static PhieuDanhGiaChatLuongDaoTaoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OPhieuDanhGiaChatLuongDaoTao;
    }


    public static XmlCls GetXml(PhieuDanhGiaChatLuongDaoTaoCls[] PhieuDanhGiaChatLuongDaoTaos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("QUY", typeof(int));
        ds.Tables["info"].Columns.Add("NAM", typeof(int));
        ds.Tables["info"].Columns.Add("BENHVIENTHAMVAN_ID");
        ds.Tables["info"].Columns.Add("BENHVIENTUVAN_ID");
        ds.Tables["info"].Columns.Add("SOBUOIBAOCAOTHAMGIA", typeof(int?));
        ds.Tables["info"].Columns.Add("LYDODIEMYNGHIA");
        ds.Tables["info"].Columns.Add("YKIENDONGGOP");
        ds.Tables["info"].Columns.Add("HOTENNGUOILAPPHIEU");
        ds.Tables["info"].Columns.Add("SODIENTHOAI");
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("SUABOI");
        ds.Tables["info"].Columns.Add("SUAVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        for (int iIndex = 0; iIndex < PhieuDanhGiaChatLuongDaoTaos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                PhieuDanhGiaChatLuongDaoTaos[iIndex].ID,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].QUY,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].NAM,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].BENHVIENTHAMVAN_ID,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].BENHVIENTUVAN_ID,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].SOBUOIBAOCAOTHAMGIA,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].LYDODIEMYNGHIA,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].YKIENDONGGOP,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].HOTENNGUOILAPPHIEU,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].SODIENTHOAI,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].TAOBOI,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].TAOVAO,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].SUABOI,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].SUAVAO,
                PhieuDanhGiaChatLuongDaoTaos[iIndex].TRANGTHAI
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("QUY", typeof(int));
        ds.Tables["info"].Columns.Add("NAM", typeof(int));
        ds.Tables["info"].Columns.Add("BENHVIENTHAMVAN_ID");
        ds.Tables["info"].Columns.Add("BENHVIENTUVAN_ID");
        ds.Tables["info"].Columns.Add("SOBUOIBAOCAOTHAMGIA", typeof(int?));
        ds.Tables["info"].Columns.Add("LYDODIEMYNGHIA");
        ds.Tables["info"].Columns.Add("YKIENDONGGOP");
        ds.Tables["info"].Columns.Add("HOTENNGUOILAPPHIEU");
        ds.Tables["info"].Columns.Add("SODIENTHOAI");
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("SUABOI");
        ds.Tables["info"].Columns.Add("SUAVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
            {
                OPhieuDanhGiaChatLuongDaoTao.ID,
                OPhieuDanhGiaChatLuongDaoTao.QUY,
                OPhieuDanhGiaChatLuongDaoTao.NAM,
                OPhieuDanhGiaChatLuongDaoTao.BENHVIENTHAMVAN_ID,
                OPhieuDanhGiaChatLuongDaoTao.BENHVIENTUVAN_ID,
                OPhieuDanhGiaChatLuongDaoTao.SOBUOIBAOCAOTHAMGIA,
                OPhieuDanhGiaChatLuongDaoTao.LYDODIEMYNGHIA,
                OPhieuDanhGiaChatLuongDaoTao.YKIENDONGGOP,
                OPhieuDanhGiaChatLuongDaoTao.HOTENNGUOILAPPHIEU,
                OPhieuDanhGiaChatLuongDaoTao.SODIENTHOAI,
                OPhieuDanhGiaChatLuongDaoTao.TAOBOI,
                OPhieuDanhGiaChatLuongDaoTao.TAOVAO,
                OPhieuDanhGiaChatLuongDaoTao.SUABOI,
                OPhieuDanhGiaChatLuongDaoTao.SUAVAO,
                OPhieuDanhGiaChatLuongDaoTao.TRANGTHAI
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
