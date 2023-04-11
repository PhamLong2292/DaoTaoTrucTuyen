using System;
using System.Data;
using System.Web;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class KetQuaXetNghiemChiTietCls
    {
        public string ID;
        public string KETQUAXETNGHIEM_ID;
        public string CHISOMA;
        public string CHISOTEN;
        public string GIATRI;
        //Các trường lưu giá trị mã hóa để hiển thị dạng html
        public string GIATRI_ENCODED = "";
    }
}

public class KetQuaXetNghiemChiTietParser
{
    public static KetQuaXetNghiemChiTietCls CreateInstance()
    {
        KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet = new KetQuaXetNghiemChiTietCls();
        return OKetQuaXetNghiemChiTiet;
    }


    public static KetQuaXetNghiemChiTietCls ParseFromDataRow(DataRow dr)
    {
        KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet = new KetQuaXetNghiemChiTietCls();
        OKetQuaXetNghiemChiTiet.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OKetQuaXetNghiemChiTiet.KETQUAXETNGHIEM_ID = CoreXmlUtility.GetString(dr, "KETQUAXETNGHIEM_ID", true);
        OKetQuaXetNghiemChiTiet.CHISOMA = CoreXmlUtility.GetString(dr, "CHISOMA", true);
        OKetQuaXetNghiemChiTiet.CHISOTEN = CoreXmlUtility.GetString(dr, "CHISOTEN", true);
        OKetQuaXetNghiemChiTiet.GIATRI = CoreXmlUtility.GetString(dr, "GIATRI", true);
        //Mã hóa dữ liệu
        OKetQuaXetNghiemChiTiet.GIATRI_ENCODED = HttpUtility.HtmlEncode(OKetQuaXetNghiemChiTiet.GIATRI);
        return OKetQuaXetNghiemChiTiet;
    }

    public static KetQuaXetNghiemChiTietCls[] ParseFromDataTable(DataTable dtTable)
    {
        KetQuaXetNghiemChiTietCls[] KetQuaXetNghiemChiTiets = new KetQuaXetNghiemChiTietCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            KetQuaXetNghiemChiTiets[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return KetQuaXetNghiemChiTiets;
    }


    public static KetQuaXetNghiemChiTietCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        KetQuaXetNghiemChiTietCls[] KetQuaXetNghiemChiTiets = ParseFromDataTable(ds.Tables[0]);
        return KetQuaXetNghiemChiTiets;
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

    public static KetQuaXetNghiemChiTietCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OKetQuaXetNghiemChiTiet;
    }


    public static XmlCls GetXml(KetQuaXetNghiemChiTietCls[] KetQuaXetNghiemChiTiets)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("KETQUAXETNGHIEM_ID");
        ds.Tables["info"].Columns.Add("CHISOMA");
        ds.Tables["info"].Columns.Add("CHISOTEN");
        ds.Tables["info"].Columns.Add("GIATRI");
        for (int iIndex = 0; iIndex < KetQuaXetNghiemChiTiets.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                KetQuaXetNghiemChiTiets[iIndex].ID,
                KetQuaXetNghiemChiTiets[iIndex].KETQUAXETNGHIEM_ID,
                KetQuaXetNghiemChiTiets[iIndex].CHISOMA,
                KetQuaXetNghiemChiTiets[iIndex].CHISOTEN,
                KetQuaXetNghiemChiTiets[iIndex].GIATRI
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("KETQUAXETNGHIEM_ID");
        ds.Tables["info"].Columns.Add("CHISOMA");
        ds.Tables["info"].Columns.Add("CHISOTEN");
        ds.Tables["info"].Columns.Add("GIATRI");
        ds.Tables["info"].Rows.Add(new object[]
            {
                OKetQuaXetNghiemChiTiet.ID,
                OKetQuaXetNghiemChiTiet.KETQUAXETNGHIEM_ID,
                OKetQuaXetNghiemChiTiet.CHISOMA,
                OKetQuaXetNghiemChiTiet.CHISOTEN,
                OKetQuaXetNghiemChiTiet.GIATRI
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
