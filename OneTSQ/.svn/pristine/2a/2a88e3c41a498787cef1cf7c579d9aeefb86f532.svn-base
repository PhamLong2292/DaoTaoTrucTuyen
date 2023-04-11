using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class MucDoYNghiaChuongTrinhDaoTaoCls 
    { 
        public string ID; 
        public string CHUYENKHOADAOTAOTTMA; 
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID; 
        public decimal? DANHGIA;
        public int STT;
    }
}

public class MucDoYNghiaChuongTrinhDaoTaoParser 
{ 
    public static MucDoYNghiaChuongTrinhDaoTaoCls CreateInstance() 
    { 
        MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao = new MucDoYNghiaChuongTrinhDaoTaoCls(); 
        return OMucDoYNghiaChuongTrinhDaoTao; 
    } 


    public static MucDoYNghiaChuongTrinhDaoTaoCls ParseFromDataRow(DataRow dr) 
    { 
        MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao = new MucDoYNghiaChuongTrinhDaoTaoCls(); 
        OMucDoYNghiaChuongTrinhDaoTao.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OMucDoYNghiaChuongTrinhDaoTao.CHUYENKHOADAOTAOTTMA = CoreXmlUtility.GetString(dr, "CHUYENKHOADAOTAOTTMA", true);
        OMucDoYNghiaChuongTrinhDaoTao.PHIEUDANHGIACHATLUONGDAOTAO_ID = CoreXmlUtility.GetString(dr, "PHIEUDANHGIACHATLUONGDAOTAO_ID", true);
        OMucDoYNghiaChuongTrinhDaoTao.DANHGIA = CoreXmlUtility.GetDecimalOrNull(dr, "DANHGIA", true);
        OMucDoYNghiaChuongTrinhDaoTao.STT = CoreXmlUtility.GetInt(dr, "STT", true);
        return OMucDoYNghiaChuongTrinhDaoTao;
    }

    public static MucDoYNghiaChuongTrinhDaoTaoCls[] ParseFromDataTable(DataTable dtTable) 
    {
        MucDoYNghiaChuongTrinhDaoTaoCls[] MucDoYNghiaChuongTrinhDaoTaos = new MucDoYNghiaChuongTrinhDaoTaoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            MucDoYNghiaChuongTrinhDaoTaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return MucDoYNghiaChuongTrinhDaoTaos;
    }


    public static MucDoYNghiaChuongTrinhDaoTaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        MucDoYNghiaChuongTrinhDaoTaoCls[] MucDoYNghiaChuongTrinhDaoTaos = ParseFromDataTable(ds.Tables[0]);
        return MucDoYNghiaChuongTrinhDaoTaos;
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

    public static MucDoYNghiaChuongTrinhDaoTaoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OMucDoYNghiaChuongTrinhDaoTao;
    }


    public static XmlCls GetXml(MucDoYNghiaChuongTrinhDaoTaoCls[] MucDoYNghiaChuongTrinhDaoTaos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CHUYENKHOADAOTAOTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("DANHGIA",typeof(decimal?));
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        for (int iIndex = 0; iIndex < MucDoYNghiaChuongTrinhDaoTaos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                MucDoYNghiaChuongTrinhDaoTaos[iIndex].ID,
                MucDoYNghiaChuongTrinhDaoTaos[iIndex].CHUYENKHOADAOTAOTTMA,
                MucDoYNghiaChuongTrinhDaoTaos[iIndex].PHIEUDANHGIACHATLUONGDAOTAO_ID,
                MucDoYNghiaChuongTrinhDaoTaos[iIndex].DANHGIA,
                MucDoYNghiaChuongTrinhDaoTaos[iIndex].STT
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CHUYENKHOADAOTAOTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("DANHGIA",typeof(decimal?));
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
            {
                OMucDoYNghiaChuongTrinhDaoTao.ID,
                OMucDoYNghiaChuongTrinhDaoTao.CHUYENKHOADAOTAOTTMA,
                OMucDoYNghiaChuongTrinhDaoTao.PHIEUDANHGIACHATLUONGDAOTAO_ID,
                OMucDoYNghiaChuongTrinhDaoTao.DANHGIA,
                OMucDoYNghiaChuongTrinhDaoTao.STT
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
