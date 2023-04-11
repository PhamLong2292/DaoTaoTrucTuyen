using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DoHieuQuaGiangDayCls
    {
        public string ID;
        public string CHUYENKHOADAOTAOTTMA;
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID;
        public int? DANHGIA;
        public int STT;

        public enum eDanhGia
        {
            HieuQua = 0,
            BinhThuong = 1,
            KhongHieuQua = 2
        }
    }
}

public class DoHieuQuaGiangDayParser
{
    public static DoHieuQuaGiangDayCls CreateInstance()
    {
        DoHieuQuaGiangDayCls ODoHieuQuaGiangDay = new DoHieuQuaGiangDayCls();
        return ODoHieuQuaGiangDay;
    }


    public static DoHieuQuaGiangDayCls ParseFromDataRow(DataRow dr)
    {
        DoHieuQuaGiangDayCls ODoHieuQuaGiangDay = new DoHieuQuaGiangDayCls();
        ODoHieuQuaGiangDay.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODoHieuQuaGiangDay.CHUYENKHOADAOTAOTTMA = CoreXmlUtility.GetString(dr, "CHUYENKHOADAOTAOTTMA", true);
        ODoHieuQuaGiangDay.PHIEUDANHGIACHATLUONGDAOTAO_ID = CoreXmlUtility.GetString(dr, "PHIEUDANHGIACHATLUONGDAOTAO_ID", true);
        ODoHieuQuaGiangDay.DANHGIA = CoreXmlUtility.GetIntOrNull(dr, "DANHGIA", true);
        ODoHieuQuaGiangDay.STT = CoreXmlUtility.GetInt(dr, "STT", true);
        return ODoHieuQuaGiangDay;
    }

    public static DoHieuQuaGiangDayCls[] ParseFromDataTable(DataTable dtTable)
    {
        DoHieuQuaGiangDayCls[] DoHieuQuaGiangDays = new DoHieuQuaGiangDayCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DoHieuQuaGiangDays[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DoHieuQuaGiangDays;
    }


    public static DoHieuQuaGiangDayCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DoHieuQuaGiangDayCls[] DoHieuQuaGiangDays = ParseFromDataTable(ds.Tables[0]);
        return DoHieuQuaGiangDays;
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

    public static DoHieuQuaGiangDayCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DoHieuQuaGiangDayCls ODoHieuQuaGiangDay = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODoHieuQuaGiangDay;
    }


    public static XmlCls GetXml(DoHieuQuaGiangDayCls[] DoHieuQuaGiangDays)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CHUYENKHOADAOTAOTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("DANHGIA", typeof(int?));
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        for (int iIndex = 0; iIndex < DoHieuQuaGiangDays.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DoHieuQuaGiangDays[iIndex].ID,
                DoHieuQuaGiangDays[iIndex].CHUYENKHOADAOTAOTTMA,
                DoHieuQuaGiangDays[iIndex].PHIEUDANHGIACHATLUONGDAOTAO_ID,
                DoHieuQuaGiangDays[iIndex].DANHGIA,
                DoHieuQuaGiangDays[iIndex].STT
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DoHieuQuaGiangDayCls ODoHieuQuaGiangDay)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CHUYENKHOADAOTAOTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("DANHGIA", typeof(int?));
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
            {
                ODoHieuQuaGiangDay.ID,
                ODoHieuQuaGiangDay.CHUYENKHOADAOTAOTTMA,
                ODoHieuQuaGiangDay.PHIEUDANHGIACHATLUONGDAOTAO_ID,
                ODoHieuQuaGiangDay.DANHGIA,
                ODoHieuQuaGiangDay.STT
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
