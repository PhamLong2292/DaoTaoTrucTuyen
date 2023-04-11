using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;

namespace OneTSQ.Model
{
    public class ThuocADRCls
    {
        public string ID;
        public string HANG_ID;
        public string DANGBAOCHE;
        public string NHASANXUAT;
        public string SOLOSX;
        public string LIEUDUNG1LAN;
        public string SOLANDUNG;
        public string DUONGDUNG;
        public DateTime? NGAYVAOVIEN;
        public DateTime? NGAYRAVIEN;
        public string LYDODUNGTHUOC;
        public int? PHANUNGCAITHIEN;
        public int? PHANUNGXUATHIEN;
        public int? LOAITHUOC;
        public string PHIEU_ID;
        public enum eTraLoi
        {
            Khong = 1,
            Co = 2,
            KhongGhiNhan = 3,
            KhongNgungGiamLieu = 4,
            KhongTaiSuDung = 5,
        }
    } 
}

public class ThuocADRParser
{   
    public static ThuocADRCls ParseFromDataRow(DataRow dr)
    {
        ThuocADRCls OThuocADR = new ThuocADRCls();
        OThuocADR.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OThuocADR.HANG_ID = CoreXmlUtility.GetString(dr, "HANG_ID", true);
        OThuocADR.DANGBAOCHE = CoreXmlUtility.GetString(dr, "DANGBAOCHE", true);
        OThuocADR.NHASANXUAT = CoreXmlUtility.GetString(dr, "NHASANXUAT", true);
        OThuocADR.SOLOSX = CoreXmlUtility.GetString(dr, "SOLOSX", true);
        OThuocADR.LIEUDUNG1LAN = CoreXmlUtility.GetString(dr, "LIEUDUNG1LAN", true);
        OThuocADR.SOLANDUNG = CoreXmlUtility.GetString(dr, "SOLANDUNG", true);
        OThuocADR.DUONGDUNG = CoreXmlUtility.GetString(dr, "DUONGDUNG", true);
        OThuocADR.NGAYVAOVIEN = CoreXmlUtility.GetDateOrNull(dr, "NGAYVAOVIEN", true);
        OThuocADR.NGAYRAVIEN = CoreXmlUtility.GetDateOrNull(dr, "NGAYRAVIEN", true);
        OThuocADR.LYDODUNGTHUOC = CoreXmlUtility.GetString(dr, "LYDODUNGTHUOC", true);
        OThuocADR.PHANUNGCAITHIEN = CoreXmlUtility.GetIntOrNull(dr, "PHANUNGCAITHIEN", true);
        OThuocADR.PHANUNGXUATHIEN = CoreXmlUtility.GetIntOrNull(dr, "PHANUNGXUATHIEN", true);
        OThuocADR.LOAITHUOC = CoreXmlUtility.GetIntOrNull(dr, "LOAITHUOC", true);
        OThuocADR.PHIEU_ID = CoreXmlUtility.GetString(dr, "PHIEU_ID", true);
        return OThuocADR;
    }
    public readonly static Dictionary<int, string> TraLois = new Dictionary<int, string>()
        {
            { (int)ThuocADRCls.eTraLoi.Co, "Có" },
            { (int)ThuocADRCls.eTraLoi.Khong, "Không" },
            { (int)ThuocADRCls.eTraLoi.KhongGhiNhan, "Không ghi nhận" },
            { (int)ThuocADRCls.eTraLoi.KhongNgungGiamLieu, "Không ngừng/giảm liều" },
            { (int)ThuocADRCls.eTraLoi.KhongTaiSuDung, "Không tái sử dụng" }
        };

    public static ThuocADRCls[] ParseFromDataTable(DataTable dtTable)
    {
        ThuocADRCls[] ThuocADRs = new ThuocADRCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            ThuocADRs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return ThuocADRs;
    }


    public static ThuocADRCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        ThuocADRCls[] ThuocADRs = ParseFromDataTable(ds.Tables[0]);
        return ThuocADRs;
    }


    public static ThuocADRCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        ThuocADRCls OThuocADR = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OThuocADR;
    }


    public static XmlCls GetXml(ThuocADRCls[] ThuocADRs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("HANG_ID");
        ds.Tables["info"].Columns.Add("DANGBAOCHE");
        ds.Tables["info"].Columns.Add("NHASANXUAT");
        ds.Tables["info"].Columns.Add("SOLOSX");
        ds.Tables["info"].Columns.Add("LIEUDUNG1LAN");
        ds.Tables["info"].Columns.Add("SOLANDUNG");
        ds.Tables["info"].Columns.Add("DUONGDUNG");
        ds.Tables["info"].Columns.Add("NGAYVAOVIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGAYRAVIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("LYDODUNGTHUOC");
        ds.Tables["info"].Columns.Add("PHANUNGCAITHIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("PHANUNGXUATHIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("LOAITHUOC", typeof(int?));
        ds.Tables["info"].Columns.Add("PHIEU_ID");
        for (int iIndex = 0; iIndex < ThuocADRs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                ThuocADRs[iIndex].ID,
                ThuocADRs[iIndex].HANG_ID,
                ThuocADRs[iIndex].DANGBAOCHE,
                ThuocADRs[iIndex].NHASANXUAT,
                ThuocADRs[iIndex].SOLOSX,
                ThuocADRs[iIndex].LIEUDUNG1LAN,
                ThuocADRs[iIndex].SOLANDUNG,
                ThuocADRs[iIndex].DUONGDUNG,
                ThuocADRs[iIndex].NGAYVAOVIEN,
                ThuocADRs[iIndex].NGAYRAVIEN,
                ThuocADRs[iIndex].LYDODUNGTHUOC,
                ThuocADRs[iIndex].PHANUNGCAITHIEN,
                ThuocADRs[iIndex].PHANUNGXUATHIEN,
                ThuocADRs[iIndex].LOAITHUOC,
                ThuocADRs[iIndex].PHIEU_ID,              
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(ThuocADRCls OThuocADR)
    {
        DataSet ds = new DataSet();
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("HANG_ID");
        ds.Tables["info"].Columns.Add("DANGBAOCHE");
        ds.Tables["info"].Columns.Add("NHASANXUAT");
        ds.Tables["info"].Columns.Add("SOLOSX");
        ds.Tables["info"].Columns.Add("LIEUDUNG1LAN");
        ds.Tables["info"].Columns.Add("SOLANDUNG");
        ds.Tables["info"].Columns.Add("DUONGDUNG");
        ds.Tables["info"].Columns.Add("NGAYVAOVIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGAYRAVIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("LYDODUNGTHUOC");
        ds.Tables["info"].Columns.Add("PHANUNGCAITHIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("PHANUNGXUATHIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("LOAITHUOC", typeof(int?));
        ds.Tables["info"].Columns.Add("PHIEU_ID");
        ds.Tables["info"].Rows.Add(new object[]
        {
                OThuocADR.ID,
                OThuocADR.HANG_ID,
                OThuocADR.DANGBAOCHE,
                OThuocADR.NHASANXUAT,
                OThuocADR.SOLOSX,
                OThuocADR.LIEUDUNG1LAN,
                OThuocADR.SOLANDUNG,
                OThuocADR.DUONGDUNG,
                OThuocADR.NGAYVAOVIEN,
                OThuocADR.NGAYRAVIEN,
                OThuocADR.LYDODUNGTHUOC,
                OThuocADR.PHANUNGCAITHIEN,
                OThuocADR.PHANUNGXUATHIEN,
                OThuocADR.LOAITHUOC,
                OThuocADR.PHIEU_ID,
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
