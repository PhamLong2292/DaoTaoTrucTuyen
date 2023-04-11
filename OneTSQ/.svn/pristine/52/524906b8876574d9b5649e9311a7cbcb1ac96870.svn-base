using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class NoiDungHoiChanCls 
    { 
        public string ID; 
        public string CABENHID;
        public string LICHHOICHANID;
        public string CHANDOAN; 
        public string HUONGXUTRI;
        public int? STT;
    }
}

public class NoiDungHoiChanParser 
{ 
    public static NoiDungHoiChanCls CreateInstance() 
    { 
        NoiDungHoiChanCls ONoiDungHoiChan = new NoiDungHoiChanCls(); 
        return ONoiDungHoiChan; 
    } 


    public static NoiDungHoiChanCls ParseFromDataRow(DataRow dr) 
    { 
        NoiDungHoiChanCls ONoiDungHoiChan = new NoiDungHoiChanCls(); 
        ONoiDungHoiChan.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ONoiDungHoiChan.CABENHID = CoreXmlUtility.GetString(dr, "CABENHID", true);
        ONoiDungHoiChan.LICHHOICHANID = CoreXmlUtility.GetString(dr, "LICHHOICHANID", true);
        ONoiDungHoiChan.CHANDOAN = CoreXmlUtility.GetString(dr, "CHANDOAN", true);
        ONoiDungHoiChan.HUONGXUTRI = CoreXmlUtility.GetString(dr, "HUONGXUTRI", true);
        ONoiDungHoiChan.STT = CoreXmlUtility.GetIntOrNull(dr, "STT", true);
        return ONoiDungHoiChan;
    }

    public static NoiDungHoiChanCls[] ParseFromDataTable(DataTable dtTable) 
    {
        NoiDungHoiChanCls[] NoiDungHoiChans = new NoiDungHoiChanCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            NoiDungHoiChans[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return NoiDungHoiChans;
    }


    public static NoiDungHoiChanCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        NoiDungHoiChanCls[] NoiDungHoiChans = ParseFromDataTable(ds.Tables[0]);
        return NoiDungHoiChans;
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

    public static NoiDungHoiChanCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        NoiDungHoiChanCls ONoiDungHoiChan = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ONoiDungHoiChan;
    }


    public static XmlCls GetXml(NoiDungHoiChanCls[] NoiDungHoiChans)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("CHANDOAN");
        ds.Tables["info"].Columns.Add("HUONGXUTRI");
        ds.Tables["info"].Columns.Add("STT", typeof(int?));
        for (int iIndex = 0; iIndex < NoiDungHoiChans.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                NoiDungHoiChans[iIndex].ID,
                NoiDungHoiChans[iIndex].CABENHID,
                NoiDungHoiChans[iIndex].LICHHOICHANID,
                NoiDungHoiChans[iIndex].CHANDOAN,
                NoiDungHoiChans[iIndex].HUONGXUTRI,
                NoiDungHoiChans[iIndex].STT
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(NoiDungHoiChanCls ONoiDungHoiChan)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CABENHID");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("CHANDOAN");
        ds.Tables["info"].Columns.Add("HUONGXUTRI");
        ds.Tables["info"].Columns.Add("STT", typeof(int?));
        ds.Tables["info"].Rows.Add(new object[]
            {
                ONoiDungHoiChan.ID,
                ONoiDungHoiChan.CABENHID,
                ONoiDungHoiChan.LICHHOICHANID,
                ONoiDungHoiChan.CHANDOAN,
                ONoiDungHoiChan.HUONGXUTRI,
                ONoiDungHoiChan.STT
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
