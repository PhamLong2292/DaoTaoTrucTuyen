using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_VanBangCls
    {
        public string ID;
        public string HOCVIEN_ID = "";
        public string TEN = "";
        public string DONVICAP = "";
        public int? NAM;
    }
}

public class DT_VanBangParser
{
    public static DT_VanBangCls CreateInstance()
    {
        DT_VanBangCls ODT_VanBang = new DT_VanBangCls();
        return ODT_VanBang;
    }


    public static DT_VanBangCls ParseFromDataRow(DataRow dr)
    {
        DT_VanBangCls ODT_VanBang = new DT_VanBangCls();
        ODT_VanBang.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_VanBang.HOCVIEN_ID = CoreXmlUtility.GetString(dr, "HOCVIEN_ID", true);
        ODT_VanBang.TEN = CoreXmlUtility.GetString(dr, "TEN", true);
        ODT_VanBang.DONVICAP = CoreXmlUtility.GetString(dr, "DONVICAP", true);
        ODT_VanBang.NAM = CoreXmlUtility.GetIntOrNull(dr, "NAM", true);
        return ODT_VanBang;
    }

    public static DT_VanBangCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_VanBangCls[] DT_VanBangs = new DT_VanBangCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_VanBangs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_VanBangs;
    }


    public static DT_VanBangCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_VanBangCls[] DT_VanBangs = ParseFromDataTable(ds.Tables[0]);
        return DT_VanBangs;
    }


    public static DT_VanBangCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_VanBangCls ODT_VanBang = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_VanBang;
    }


    public static XmlCls GetXml(DT_VanBangCls[] DT_VanBangs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("HOCVIEN_ID");
        ds.Tables["info"].Columns.Add("TEN");
        ds.Tables["info"].Columns.Add("DONVICAP");
        ds.Tables["info"].Columns.Add("NAM", typeof(int?));
        for (int iIndex = 0; iIndex < DT_VanBangs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_VanBangs[iIndex].ID,
                DT_VanBangs[iIndex].HOCVIEN_ID,
                DT_VanBangs[iIndex].TEN,
                DT_VanBangs[iIndex].DONVICAP,
                DT_VanBangs[iIndex].NAM
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_VanBangCls ODT_VanBang)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("HOCVIEN_ID");
        ds.Tables["info"].Columns.Add("TEN");
        ds.Tables["info"].Columns.Add("DONVICAP");
        ds.Tables["info"].Columns.Add("NAM", typeof(int?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_VanBang.ID,
                ODT_VanBang.HOCVIEN_ID,
                ODT_VanBang.TEN,
                ODT_VanBang.DONVICAP,
                ODT_VanBang.NAM
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
