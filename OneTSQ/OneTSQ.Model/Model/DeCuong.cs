﻿using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;

namespace OneTSQ.Model
{
    public class DeCuongCls
    {
        public string ID;
        public string DANGKYDETAI_ID;
        public string MA;
        public string TENDECUONG;
        public string NGUOIGUI_ID;
        public DateTime? THOIGIANGUI;
        public int? KETLUAN;
        public string LICHXETDUYET_ID;
        public int? TRANGTHAI;
        public string YKIENCHUNG;
        public DateTime? THOIGIANBATDAU;
        public DateTime? THOIGIANKETTHUC;
        public string DeCuongLapLichID;

        public enum eTrangThai
        {
            Moi = 0,
            ChoXetDuyet = 1,
            LapLich = 2,
            DaLapLich = 3,
            HoanTat = 4
        }
        public enum eKetLuan
        {
            Thongqua = 0,
            ThongQuanCoChinhSua = 1,
            KhongThongQua = 2,
        }
    }
}

public class DeCuongParser
{
    public readonly static Dictionary<int, string> TrangThais = new Dictionary<int, string>()
        {
            { (int)DeCuongCls.eTrangThai.Moi, "Mới" },
            { (int)DeCuongCls.eTrangThai.ChoXetDuyet, "Chờ duyệt" },
            { (int)DeCuongCls.eTrangThai.LapLich, "Chờ lập lịch" },
            { (int)DeCuongCls.eTrangThai.DaLapLich, "Đã lập lịch" },
            { (int)DeCuongCls.eTrangThai.HoanTat, "Hoàn tất" }
        };
    public readonly static Dictionary<int, string> sColorTrangThai = new Dictionary<int, string>()
        {
            { (int)DeCuongCls.eTrangThai.Moi, "<span style=\"background-color:orange;color:white;\" class=\"badge\" >Mới</span>" },
            { (int)DeCuongCls.eTrangThai.ChoXetDuyet, "<span style=\"background-color:violet; color:black;\" class=\"badge\">Chờ duyệt</span>" },
            { (int)DeCuongCls.eTrangThai.LapLich, "<span style=\"background-color:green;color:white;\" class=\"badge\" >Chờ lập lịch</span>" },
            { (int)DeCuongCls.eTrangThai.DaLapLich, "<span style=\"background-color:blue;color:white;\" class=\"badge\" >Đã lập lịch</span>" },
            { (int)DeCuongCls.eTrangThai.HoanTat, "<span style=\"background-color:red;color:white;\" class=\"badge\" >Hoàn tất</span>" },
        };
    public readonly static Dictionary<int, string> KetLuans = new Dictionary<int, string>()
        {
            { (int)DeCuongCls.eKetLuan.Thongqua, "Thông qua" },
            { (int)DeCuongCls.eKetLuan.ThongQuanCoChinhSua, "Thông qua nhưng có chỉnh sửa" },
            { (int)DeCuongCls.eKetLuan.KhongThongQua, "Không thông qua" },
        };
    public static DeCuongCls CreateInstance()
    {
        DeCuongCls ODeCuong = new DeCuongCls();
        return ODeCuong;
    }
    public static DeCuongCls ParseFromDataRow(DataRow dr)
    {
        DeCuongCls ODeCuong = new DeCuongCls();
        ODeCuong.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODeCuong.DANGKYDETAI_ID = CoreXmlUtility.GetString(dr, "DANGKYDETAI_ID", true);
        ODeCuong.MA = CoreXmlUtility.GetString(dr, "MA", true);
        ODeCuong.TENDECUONG = CoreXmlUtility.GetString(dr, "TENDECUONG", true);
        ODeCuong.NGUOIGUI_ID = CoreXmlUtility.GetString(dr, "NGUOIGUI_ID", true);
        ODeCuong.THOIGIANGUI = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANGUI", true);
        ODeCuong.KETLUAN = CoreXmlUtility.GetIntOrNull(dr, "KETLUAN", true);
        ODeCuong.LICHXETDUYET_ID = CoreXmlUtility.GetString(dr, "LICHXETDUYET_ID", true);
        ODeCuong.TRANGTHAI = CoreXmlUtility.GetIntOrNull(dr, "TRANGTHAI", true);
        ODeCuong.YKIENCHUNG = CoreXmlUtility.GetString(dr, "YKIENCHUNG", true);
        ODeCuong.THOIGIANBATDAU = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANBATDAU", true);
        ODeCuong.THOIGIANKETTHUC = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANKETTHUC", true);       
        return ODeCuong;
    }

    public static DeCuongCls[] ParseFromDataTable(DataTable dtTable)
    {
        DeCuongCls[] DeCuongs = new DeCuongCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DeCuongs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DeCuongs;
    }


    public static DeCuongCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DeCuongCls[] DeCuongs = ParseFromDataTable(ds.Tables[0]);
        return DeCuongs;
    }


    public static DeCuongCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DeCuongCls ODeCuong = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODeCuong;
    }


    public static XmlCls GetXml(DeCuongCls[] DeCuongs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("DANGKYDETAI_ID");
        ds.Tables["info"].Columns.Add("MA");
        ds.Tables["info"].Columns.Add("TENDECUONG");
        ds.Tables["info"].Columns.Add("NGUOIGUI_ID");
        ds.Tables["info"].Columns.Add("THOIGIANGUI", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETLUAN", typeof(int?));
        ds.Tables["info"].Columns.Add("LICHXETDUYET_ID");
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Columns.Add("YKIENCHUNG");
        ds.Tables["info"].Columns.Add("THOIGIANBATDAU", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("THOIGIANKETTHUC", typeof(DateTime?));        
        for (int iIndex = 0; iIndex < DeCuongs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DeCuongs[iIndex].ID,
                DeCuongs[iIndex].DANGKYDETAI_ID,
                DeCuongs[iIndex].MA,
                DeCuongs[iIndex].NGUOIGUI_ID,
                DeCuongs[iIndex].THOIGIANGUI,
                DeCuongs[iIndex].KETLUAN,
                DeCuongs[iIndex].LICHXETDUYET_ID,
                DeCuongs[iIndex].TRANGTHAI,
                DeCuongs[iIndex].YKIENCHUNG,
                DeCuongs[iIndex].THOIGIANBATDAU,
                DeCuongs[iIndex].THOIGIANKETTHUC,             
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DeCuongCls ODeCuong)
    {
        DataSet ds = new DataSet();
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("DANGKYDETAI_ID");
        ds.Tables["info"].Columns.Add("MA");
        ds.Tables["info"].Columns.Add("TENDECUONG");
        ds.Tables["info"].Columns.Add("NGUOIGUI_ID");
        ds.Tables["info"].Columns.Add("THOIGIANGUI", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETLUAN", typeof(int?));
        ds.Tables["info"].Columns.Add("LICHXETDUYET_ID");
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Columns.Add("YKIENCHUNG");
        ds.Tables["info"].Columns.Add("THOIGIANBATDAU", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("THOIGIANKETTHUC", typeof(DateTime?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODeCuong.ID,
                ODeCuong.DANGKYDETAI_ID,
                ODeCuong.MA,
                ODeCuong.TENDECUONG,
                ODeCuong.NGUOIGUI_ID,
                ODeCuong.THOIGIANGUI,
                ODeCuong.KETLUAN,
                ODeCuong.LICHXETDUYET_ID,
                ODeCuong.TRANGTHAI,
                ODeCuong.YKIENCHUNG,
                ODeCuong.THOIGIANBATDAU,
                ODeCuong.THOIGIANKETTHUC,             
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
