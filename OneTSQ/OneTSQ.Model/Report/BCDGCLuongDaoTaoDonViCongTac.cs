using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class BCDGCLuongDaoTaoDonViCongTacCls
    {
        public string BCDANHGIACHATLUONGDAOTAOID;
        public string DONVICONGTACMA;
        public int? STT;
    }
}

public class BCDGCLuongDaoTaoDonViCongTacParser
{
    public static BCDGCLuongDaoTaoDonViCongTacCls CreateInstance()
    {
        BCDGCLuongDaoTaoDonViCongTacCls OBCDGCLuongDaoTaoDonViCongTac = new BCDGCLuongDaoTaoDonViCongTacCls();
        return OBCDGCLuongDaoTaoDonViCongTac;
    }


    public static BCDGCLuongDaoTaoDonViCongTacCls ParseFromDataRow(DataRow dr)
    {
        BCDGCLuongDaoTaoDonViCongTacCls OBCDGCLuongDaoTaoDonViCongTac = new BCDGCLuongDaoTaoDonViCongTacCls();
        OBCDGCLuongDaoTaoDonViCongTac.BCDANHGIACHATLUONGDAOTAOID = CoreXmlUtility.GetString(dr, "BCDANHGIACHATLUONGDAOTAOID", true);
        OBCDGCLuongDaoTaoDonViCongTac.DONVICONGTACMA = CoreXmlUtility.GetString(dr, "DONVICONGTACMA", true);
        OBCDGCLuongDaoTaoDonViCongTac.STT = CoreXmlUtility.GetIntOrNull(dr, "STT", true);
        return OBCDGCLuongDaoTaoDonViCongTac;
    }

    public static BCDGCLuongDaoTaoDonViCongTacCls[] ParseFromDataTable(DataTable dtTable)
    {
        BCDGCLuongDaoTaoDonViCongTacCls[] BCDGCLuongDaoTaoDonViCongTacs = new BCDGCLuongDaoTaoDonViCongTacCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            BCDGCLuongDaoTaoDonViCongTacs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return BCDGCLuongDaoTaoDonViCongTacs;
    }


    public static BCDGCLuongDaoTaoDonViCongTacCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        BCDGCLuongDaoTaoDonViCongTacCls[] BCDGCLuongDaoTaoDonViCongTacs = ParseFromDataTable(ds.Tables[0]);
        return BCDGCLuongDaoTaoDonViCongTacs;
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

    public static BCDGCLuongDaoTaoDonViCongTacCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        BCDGCLuongDaoTaoDonViCongTacCls OBCDGCLuongDaoTaoDonViCongTac = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBCDGCLuongDaoTaoDonViCongTac;
    }


    public static XmlCls GetXml(BCDGCLuongDaoTaoDonViCongTacCls[] BCDGCLuongDaoTaoDonViCongTacs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("BCDANHGIACHATLUONGDAOTAOID");
        ds.Tables["info"].Columns.Add("DONVICONGTACMA");
        ds.Tables["info"].Columns.Add("STT", typeof(int?));
        for (int iIndex = 0; iIndex < BCDGCLuongDaoTaoDonViCongTacs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                BCDGCLuongDaoTaoDonViCongTacs[iIndex].BCDANHGIACHATLUONGDAOTAOID,
                BCDGCLuongDaoTaoDonViCongTacs[iIndex].DONVICONGTACMA,
                BCDGCLuongDaoTaoDonViCongTacs[iIndex].STT
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(BCDGCLuongDaoTaoDonViCongTacCls OBCDGCLuongDaoTaoDonViCongTac)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("BCDANHGIACHATLUONGDAOTAOID");
        ds.Tables["info"].Columns.Add("DONVICONGTACMA");
        ds.Tables["info"].Columns.Add("STT", typeof(int?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                OBCDGCLuongDaoTaoDonViCongTac.BCDANHGIACHATLUONGDAOTAOID,
                OBCDGCLuongDaoTaoDonViCongTac.DONVICONGTACMA,
                OBCDGCLuongDaoTaoDonViCongTac.STT
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
