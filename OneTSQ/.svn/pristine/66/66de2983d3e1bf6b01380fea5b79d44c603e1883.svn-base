using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class KetQuaXetNghiemCls
    {
        public string ID;
        public string CABENHID = "";
        public string DICHVUMA = "";
        public string DICHVUTEN = "";
        public DateTime? THOIGIAN;
        public string CHANDOANMA = "";
        public string KYTHUAT = "";
        public string LOAIMAU = "";
        public string KETLUAN = "";
        public string NHANXET = "";
        public string DENGHI = "";
    }
}

public class KetQuaXetNghiemParser
{
    public static KetQuaXetNghiemCls CreateInstance()
    {
        KetQuaXetNghiemCls OKetQuaXetNghiem = new KetQuaXetNghiemCls();
        return OKetQuaXetNghiem;
    }


    public static KetQuaXetNghiemCls ParseFromDataRow(DataRow dr)
    {
        KetQuaXetNghiemCls OKetQuaXetNghiem = new KetQuaXetNghiemCls();
        OKetQuaXetNghiem.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OKetQuaXetNghiem.CABENHID = CoreXmlUtility.GetString(dr, "CABENHID", true);
        OKetQuaXetNghiem.DICHVUMA = CoreXmlUtility.GetString(dr, "DICHVUMA", true);
        OKetQuaXetNghiem.DICHVUTEN = CoreXmlUtility.GetString(dr, "DICHVUTEN", true);
        OKetQuaXetNghiem.THOIGIAN = dr["THOIGIAN"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "THOIGIAN", true);
        OKetQuaXetNghiem.CHANDOANMA = CoreXmlUtility.GetString(dr, "CHANDOANMA", true);
        OKetQuaXetNghiem.KYTHUAT = CoreXmlUtility.GetString(dr, "KYTHUAT", true);
        OKetQuaXetNghiem.LOAIMAU = CoreXmlUtility.GetString(dr, "LOAIMAU", true);
        OKetQuaXetNghiem.KETLUAN = CoreXmlUtility.GetString(dr, "KETLUAN", true);
        OKetQuaXetNghiem.NHANXET = CoreXmlUtility.GetString(dr, "NHANXET", true);
        OKetQuaXetNghiem.DENGHI = CoreXmlUtility.GetString(dr, "DENGHI", true);
        return OKetQuaXetNghiem;
    }

    public static KetQuaXetNghiemCls[] ParseFromDataTable(DataTable dtTable)
    {
        KetQuaXetNghiemCls[] KetQuaXetNghiems = new KetQuaXetNghiemCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            KetQuaXetNghiems[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return KetQuaXetNghiems;
    }


    public static KetQuaXetNghiemCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        KetQuaXetNghiemCls[] KetQuaXetNghiems = ParseFromDataTable(ds.Tables[0]);
        return KetQuaXetNghiems;
    }


    public static KetQuaXetNghiemCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        KetQuaXetNghiemCls OKetQuaXetNghiem = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OKetQuaXetNghiem;
    }


    public static XmlCls GetXml(KetQuaXetNghiemCls[] KetQuaXetNghiems)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("DICHVUMA");
        ds.Tables["info"].Columns.Add("DICHVUTEN");
        ds.Tables["info"].Columns.Add("THOIGIAN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("CHANDOANMA");
        ds.Tables["info"].Columns.Add("KYTHUAT");
        ds.Tables["info"].Columns.Add("LOAIMAU");
        ds.Tables["info"].Columns.Add("KETLUAN");
        ds.Tables["info"].Columns.Add("NHANXET");
        ds.Tables["info"].Columns.Add("DENGHI");
        for (int iIndex = 0; iIndex < KetQuaXetNghiems.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                KetQuaXetNghiems[iIndex].ID,
                KetQuaXetNghiems[iIndex].CABENHID,
                KetQuaXetNghiems[iIndex].DICHVUMA,
                KetQuaXetNghiems[iIndex].DICHVUTEN,
                KetQuaXetNghiems[iIndex].THOIGIAN,
                KetQuaXetNghiems[iIndex].CHANDOANMA,
                KetQuaXetNghiems[iIndex].KYTHUAT,
                KetQuaXetNghiems[iIndex].LOAIMAU,
                KetQuaXetNghiems[iIndex].KETLUAN,
                KetQuaXetNghiems[iIndex].NHANXET,
                KetQuaXetNghiems[iIndex].DENGHI,
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(KetQuaXetNghiemCls OKetQuaXetNghiem)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("DICHVUMA");
        ds.Tables["info"].Columns.Add("THOIGIAN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("CHANDOANMA");
        ds.Tables["info"].Columns.Add("KYTHUAT");
        ds.Tables["info"].Columns.Add("LOAIMAU");
        ds.Tables["info"].Columns.Add("KETLUAN");
        ds.Tables["info"].Columns.Add("NHANXET");
        ds.Tables["info"].Columns.Add("DENGHI");
        ds.Tables["info"].Rows.Add(new object[]
        {
                OKetQuaXetNghiem.ID,
                OKetQuaXetNghiem.CABENHID,
                OKetQuaXetNghiem.DICHVUMA,
                OKetQuaXetNghiem.THOIGIAN,
                OKetQuaXetNghiem.CHANDOANMA,
                OKetQuaXetNghiem.KYTHUAT,
                OKetQuaXetNghiem.LOAIMAU,
                OKetQuaXetNghiem.KETLUAN,
                OKetQuaXetNghiem.NHANXET,
                OKetQuaXetNghiem.DENGHI
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
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
}
