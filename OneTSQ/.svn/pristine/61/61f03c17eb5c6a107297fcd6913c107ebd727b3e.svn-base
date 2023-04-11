using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class BcDanhGiaChatLuongDaoTaoCls
    {
        public string ID;
        public int QUY;
        public int NAM;
        public string BENHVIENID;
        public string GHICHULYDOBVKODANHGIA;
        public string DIENGIAICHATLUONGTTB;
        public string DIENGIAIDOHIEUQUA;
        public string DIENGIAIMUCDOPHONGPHU;
        public string DIENGIAITHOIGIANTHOILUONG;
        public string DIENGIAIMUCYNGHIA;
        public string YKIENKHAC;
        public string TAOBOI;
        public DateTime TAOVAO;
        public string SUABOI;
        public DateTime? SUAVAO;
        public int TRANGTHAI;

        public enum eTrangThai
        {
            Moi = 0
        }
        public enum ePermission
        {
            Xem = 0,
            Them = 1,
            Sua = 2,
            Xoa = 3,
            Export = 4
        }
        public enum eQuy
        {
            I = 1,
            II = 2,
            III = 3,
            IV = 4
        }
    }

    public class TongHopDanhGiaCls
    {
        public int STT;
        public string DANHMUCMA;
        public string DANHMUCTEN;
        public int DANHGIA;
        public int SOLUONG;
    }
    public class TongHopDanhGiaMucYNghiaCls
    {
        public int STT;
        public string CHUYENKHOADAOTAOTTMA;
        public string CHUYENKHOADAOTAOTTTEN;
        public string CHUYENKHOADAOTAOTTTENNGAN;
        public string BENHVIENMA;
        public string BENHVIENTEN;
        public int DANHGIA;
    }
}

public class BcDanhGiaChatLuongDaoTaoParser
{
    public readonly static Dictionary<int, string> TrangThais = new Dictionary<int, string>()
        {
            { (int)BcDanhGiaChatLuongDaoTaoCls.eTrangThai.Moi, "Mới" }
        };
    public readonly static Dictionary<int, string> sColorTrangThai = new Dictionary<int, string>()
        {
            { (int)BcDanhGiaChatLuongDaoTaoCls.eTrangThai.Moi, "<span style=\"background-color:orange;color:white;\" class=\"badge\" >Mới</span>" }
        };
    public readonly static Dictionary<int, string> Quys = new Dictionary<int, string>()
        {
            { (int)BcDanhGiaChatLuongDaoTaoCls.eQuy.I, "I" },
            { (int)BcDanhGiaChatLuongDaoTaoCls.eQuy.II, "II" },
            { (int)BcDanhGiaChatLuongDaoTaoCls.eQuy.III, "III" },
            { (int)BcDanhGiaChatLuongDaoTaoCls.eQuy.IV, "IV" }
        };
    public static BcDanhGiaChatLuongDaoTaoCls CreateInstance()
    {
        BcDanhGiaChatLuongDaoTaoCls OBcDanhGiaChatLuongDaoTao = new BcDanhGiaChatLuongDaoTaoCls();
        return OBcDanhGiaChatLuongDaoTao;
    }


    public static BcDanhGiaChatLuongDaoTaoCls ParseFromDataRow(DataRow dr)
    {
        BcDanhGiaChatLuongDaoTaoCls OBcDanhGiaChatLuongDaoTao = new BcDanhGiaChatLuongDaoTaoCls();
        OBcDanhGiaChatLuongDaoTao.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OBcDanhGiaChatLuongDaoTao.QUY = CoreXmlUtility.GetInt(dr, "QUY", true);
        OBcDanhGiaChatLuongDaoTao.NAM = CoreXmlUtility.GetInt(dr, "NAM", true);
        OBcDanhGiaChatLuongDaoTao.BENHVIENID = CoreXmlUtility.GetString(dr, "BENHVIENID", true);
        OBcDanhGiaChatLuongDaoTao.GHICHULYDOBVKODANHGIA = CoreXmlUtility.GetString(dr, "GHICHULYDOBVKODANHGIA", true);
        OBcDanhGiaChatLuongDaoTao.DIENGIAICHATLUONGTTB = CoreXmlUtility.GetString(dr, "DIENGIAICHATLUONGTTB", true);
        OBcDanhGiaChatLuongDaoTao.DIENGIAIDOHIEUQUA = CoreXmlUtility.GetString(dr, "DIENGIAIDOHIEUQUA", true);
        OBcDanhGiaChatLuongDaoTao.DIENGIAIMUCDOPHONGPHU = CoreXmlUtility.GetString(dr, "DIENGIAIMUCDOPHONGPHU", true);
        OBcDanhGiaChatLuongDaoTao.DIENGIAITHOIGIANTHOILUONG = CoreXmlUtility.GetString(dr, "DIENGIAITHOIGIANTHOILUONG", true);
        OBcDanhGiaChatLuongDaoTao.DIENGIAIMUCYNGHIA = CoreXmlUtility.GetString(dr, "DIENGIAIMUCYNGHIA", true);
        OBcDanhGiaChatLuongDaoTao.YKIENKHAC = CoreXmlUtility.GetString(dr, "YKIENKHAC", true);
        OBcDanhGiaChatLuongDaoTao.TAOBOI = CoreXmlUtility.GetString(dr, "TAOBOI", true);
        OBcDanhGiaChatLuongDaoTao.TAOVAO = CoreXmlUtility.GetDate(dr, "TAOVAO", true);
        OBcDanhGiaChatLuongDaoTao.SUABOI = CoreXmlUtility.GetString(dr, "SUABOI", true);
        OBcDanhGiaChatLuongDaoTao.SUAVAO = CoreXmlUtility.GetDateOrNull(dr, "SUAVAO", true);
        OBcDanhGiaChatLuongDaoTao.TRANGTHAI = CoreXmlUtility.GetInt(dr, "TRANGTHAI", true);
        return OBcDanhGiaChatLuongDaoTao;
    }

    public static BcDanhGiaChatLuongDaoTaoCls[] ParseFromDataTable(DataTable dtTable)
    {
        BcDanhGiaChatLuongDaoTaoCls[] BcDanhGiaChatLuongDaoTaos = new BcDanhGiaChatLuongDaoTaoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            BcDanhGiaChatLuongDaoTaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return BcDanhGiaChatLuongDaoTaos;
    }


    public static BcDanhGiaChatLuongDaoTaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        BcDanhGiaChatLuongDaoTaoCls[] BcDanhGiaChatLuongDaoTaos = ParseFromDataTable(ds.Tables[0]);
        return BcDanhGiaChatLuongDaoTaos;
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

    public static BcDanhGiaChatLuongDaoTaoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        BcDanhGiaChatLuongDaoTaoCls OBcDanhGiaChatLuongDaoTao = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBcDanhGiaChatLuongDaoTao;
    }


    public static XmlCls GetXml(BcDanhGiaChatLuongDaoTaoCls[] BcDanhGiaChatLuongDaoTaos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("QUY", typeof(int));
        ds.Tables["info"].Columns.Add("NAM", typeof(int));
        ds.Tables["info"].Columns.Add("BENHVIENID");
        ds.Tables["info"].Columns.Add("GHICHULYDOBVKODANHGIA");
        ds.Tables["info"].Columns.Add("DIENGIAICHATLUONGTTB");
        ds.Tables["info"].Columns.Add("DIENGIAIDOHIEUQUA");
        ds.Tables["info"].Columns.Add("DIENGIAIMUCDOPHONGPHU");
        ds.Tables["info"].Columns.Add("DIENGIAITHOIGIANTHOILUONG");
        ds.Tables["info"].Columns.Add("DIENGIAIMUCYNGHIA");
        ds.Tables["info"].Columns.Add("YKIENKHAC");
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("SUABOI");
        ds.Tables["info"].Columns.Add("SUAVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        for (int iIndex = 0; iIndex < BcDanhGiaChatLuongDaoTaos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                BcDanhGiaChatLuongDaoTaos[iIndex].ID,
                BcDanhGiaChatLuongDaoTaos[iIndex].QUY,
                BcDanhGiaChatLuongDaoTaos[iIndex].NAM,
                BcDanhGiaChatLuongDaoTaos[iIndex].BENHVIENID,
                BcDanhGiaChatLuongDaoTaos[iIndex].GHICHULYDOBVKODANHGIA,
                BcDanhGiaChatLuongDaoTaos[iIndex].DIENGIAICHATLUONGTTB,
                BcDanhGiaChatLuongDaoTaos[iIndex].DIENGIAIDOHIEUQUA,
                BcDanhGiaChatLuongDaoTaos[iIndex].DIENGIAIMUCDOPHONGPHU,
                BcDanhGiaChatLuongDaoTaos[iIndex].DIENGIAITHOIGIANTHOILUONG,
                BcDanhGiaChatLuongDaoTaos[iIndex].DIENGIAIMUCYNGHIA,
                BcDanhGiaChatLuongDaoTaos[iIndex].YKIENKHAC,
                BcDanhGiaChatLuongDaoTaos[iIndex].TAOBOI,
                BcDanhGiaChatLuongDaoTaos[iIndex].TAOVAO,
                BcDanhGiaChatLuongDaoTaos[iIndex].SUABOI,
                BcDanhGiaChatLuongDaoTaos[iIndex].SUAVAO,
                BcDanhGiaChatLuongDaoTaos[iIndex].TRANGTHAI
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(BcDanhGiaChatLuongDaoTaoCls OBcDanhGiaChatLuongDaoTao)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("QUY", typeof(int));
        ds.Tables["info"].Columns.Add("NAM", typeof(int));
        ds.Tables["info"].Columns.Add("BENHVIENID");
        ds.Tables["info"].Columns.Add("GHICHULYDOBVKODANHGIA");
        ds.Tables["info"].Columns.Add("DIENGIAICHATLUONGTTB");
        ds.Tables["info"].Columns.Add("DIENGIAIDOHIEUQUA");
        ds.Tables["info"].Columns.Add("DIENGIAIMUCDOPHONGPHU");
        ds.Tables["info"].Columns.Add("DIENGIAITHOIGIANTHOILUONG");
        ds.Tables["info"].Columns.Add("DIENGIAIMUCYNGHIA");
        ds.Tables["info"].Columns.Add("YKIENKHAC");
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("SUABOI");
        ds.Tables["info"].Columns.Add("SUAVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
            {
                OBcDanhGiaChatLuongDaoTao.ID,
                OBcDanhGiaChatLuongDaoTao.QUY,
                OBcDanhGiaChatLuongDaoTao.NAM,
                OBcDanhGiaChatLuongDaoTao.BENHVIENID,
                OBcDanhGiaChatLuongDaoTao.GHICHULYDOBVKODANHGIA,
                OBcDanhGiaChatLuongDaoTao.DIENGIAICHATLUONGTTB,
                OBcDanhGiaChatLuongDaoTao.DIENGIAIDOHIEUQUA,
                OBcDanhGiaChatLuongDaoTao.DIENGIAIMUCDOPHONGPHU,
                OBcDanhGiaChatLuongDaoTao.DIENGIAITHOIGIANTHOILUONG,
                OBcDanhGiaChatLuongDaoTao.DIENGIAIMUCYNGHIA,
                OBcDanhGiaChatLuongDaoTao.YKIENKHAC,
                OBcDanhGiaChatLuongDaoTao.TAOBOI,
                OBcDanhGiaChatLuongDaoTao.TAOVAO,
                OBcDanhGiaChatLuongDaoTao.SUABOI,
                OBcDanhGiaChatLuongDaoTao.SUAVAO,
                OBcDanhGiaChatLuongDaoTao.TRANGTHAI
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}


public class TongHopDanhGiaParser
{
    public static TongHopDanhGiaCls CreateInstance()
    {
        TongHopDanhGiaCls OTongHopDanhGiaDoHieuQua = new TongHopDanhGiaCls();
        return OTongHopDanhGiaDoHieuQua;
    }


    public static TongHopDanhGiaCls ParseFromDataRow(DataRow dr)
    {
        TongHopDanhGiaCls OTongHopDanhGiaDoHieuQua = new TongHopDanhGiaCls();
        OTongHopDanhGiaDoHieuQua.STT = CoreXmlUtility.GetInt(dr, "STT", true);
        OTongHopDanhGiaDoHieuQua.DANHMUCMA = CoreXmlUtility.GetString(dr, "DANHMUCMA", true);
        OTongHopDanhGiaDoHieuQua.DANHMUCTEN = CoreXmlUtility.GetString(dr, "DANHMUCTEN", true);
        OTongHopDanhGiaDoHieuQua.DANHGIA = CoreXmlUtility.GetInt(dr, "DANHGIA", true);
        OTongHopDanhGiaDoHieuQua.SOLUONG = CoreXmlUtility.GetInt(dr, "SOLUONG", true);
        return OTongHopDanhGiaDoHieuQua;
    }

    public static TongHopDanhGiaCls[] ParseFromDataTable(DataTable dtTable)
    {
        TongHopDanhGiaCls[] TongHopDanhGiaDoHieuQuas = new TongHopDanhGiaCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            TongHopDanhGiaDoHieuQuas[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return TongHopDanhGiaDoHieuQuas;
    }
}
public class TongHopDanhGiaMucYNghiaParser
{
    public static TongHopDanhGiaMucYNghiaCls CreateInstance()
    {
        TongHopDanhGiaMucYNghiaCls OTongHopDanhGiaMucYNghiaDoHieuQua = new TongHopDanhGiaMucYNghiaCls();
        return OTongHopDanhGiaMucYNghiaDoHieuQua;
    }


    public static TongHopDanhGiaMucYNghiaCls ParseFromDataRow(DataRow dr)
    {
        TongHopDanhGiaMucYNghiaCls OTongHopDanhGiaMucYNghiaDoHieuQua = new TongHopDanhGiaMucYNghiaCls();
        OTongHopDanhGiaMucYNghiaDoHieuQua.STT = CoreXmlUtility.GetInt(dr, "STT", true);
        OTongHopDanhGiaMucYNghiaDoHieuQua.CHUYENKHOADAOTAOTTMA = CoreXmlUtility.GetString(dr, "CHUYENKHOADAOTAOTTMA", true);
        OTongHopDanhGiaMucYNghiaDoHieuQua.CHUYENKHOADAOTAOTTTEN = CoreXmlUtility.GetString(dr, "CHUYENKHOADAOTAOTTTEN", true);
        OTongHopDanhGiaMucYNghiaDoHieuQua.CHUYENKHOADAOTAOTTTENNGAN = CoreXmlUtility.GetString(dr, "CHUYENKHOADAOTAOTTTENNGAN", true);
        OTongHopDanhGiaMucYNghiaDoHieuQua.BENHVIENMA = CoreXmlUtility.GetString(dr, "BENHVIENMA", true);
        OTongHopDanhGiaMucYNghiaDoHieuQua.BENHVIENTEN = CoreXmlUtility.GetString(dr, "BENHVIENTEN", true);
        OTongHopDanhGiaMucYNghiaDoHieuQua.DANHGIA = CoreXmlUtility.GetInt(dr, "DANHGIA", true);
        return OTongHopDanhGiaMucYNghiaDoHieuQua;
    }

    public static TongHopDanhGiaMucYNghiaCls[] ParseFromDataTable(DataTable dtTable)
    {
        TongHopDanhGiaMucYNghiaCls[] TongHopDanhGiaMucYNghiaDoHieuQuas = new TongHopDanhGiaMucYNghiaCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            TongHopDanhGiaMucYNghiaDoHieuQuas[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return TongHopDanhGiaMucYNghiaDoHieuQuas;
    }
}
