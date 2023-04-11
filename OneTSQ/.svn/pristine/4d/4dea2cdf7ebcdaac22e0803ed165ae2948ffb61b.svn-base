using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class MucDoPhongPhuBaiBaoCaoCls 
    { 
        public string ID; 
        public string CHUYENKHOADAOTAOTTMA; 
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID; 
        public int? DANHGIA;
        public int STT;

        public enum eDanhGia
        {
            PhongPhu = 0,
            BinhThuong = 1,
            KhongPhongPhu = 2
        }
    }
}

public class MucDoPhongPhuBaiBaoCaoParser 
{ 
    public static MucDoPhongPhuBaiBaoCaoCls CreateInstance() 
    { 
        MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao = new MucDoPhongPhuBaiBaoCaoCls(); 
        return OMucDoPhongPhuBaiBaoCao; 
    } 


    public static MucDoPhongPhuBaiBaoCaoCls ParseFromDataRow(DataRow dr) 
    { 
        MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao = new MucDoPhongPhuBaiBaoCaoCls(); 
        OMucDoPhongPhuBaiBaoCao.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OMucDoPhongPhuBaiBaoCao.CHUYENKHOADAOTAOTTMA = CoreXmlUtility.GetString(dr, "CHUYENKHOADAOTAOTTMA", true);
        OMucDoPhongPhuBaiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID = CoreXmlUtility.GetString(dr, "PHIEUDANHGIACHATLUONGDAOTAO_ID", true);
        OMucDoPhongPhuBaiBaoCao.DANHGIA = CoreXmlUtility.GetIntOrNull(dr, "DANHGIA", true);
        OMucDoPhongPhuBaiBaoCao.STT = CoreXmlUtility.GetInt(dr, "STT", true);
        return OMucDoPhongPhuBaiBaoCao;
    }

    public static MucDoPhongPhuBaiBaoCaoCls[] ParseFromDataTable(DataTable dtTable) 
    {
        MucDoPhongPhuBaiBaoCaoCls[] MucDoPhongPhuBaiBaoCaos = new MucDoPhongPhuBaiBaoCaoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            MucDoPhongPhuBaiBaoCaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return MucDoPhongPhuBaiBaoCaos;
    }


    public static MucDoPhongPhuBaiBaoCaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        MucDoPhongPhuBaiBaoCaoCls[] MucDoPhongPhuBaiBaoCaos = ParseFromDataTable(ds.Tables[0]);
        return MucDoPhongPhuBaiBaoCaos;
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

    public static MucDoPhongPhuBaiBaoCaoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OMucDoPhongPhuBaiBaoCao;
    }


    public static XmlCls GetXml(MucDoPhongPhuBaiBaoCaoCls[] MucDoPhongPhuBaiBaoCaos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CHUYENKHOADAOTAOTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("DANHGIA",typeof(int?));
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        for (int iIndex = 0; iIndex < MucDoPhongPhuBaiBaoCaos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                MucDoPhongPhuBaiBaoCaos[iIndex].ID,
                MucDoPhongPhuBaiBaoCaos[iIndex].CHUYENKHOADAOTAOTTMA,
                MucDoPhongPhuBaiBaoCaos[iIndex].PHIEUDANHGIACHATLUONGDAOTAO_ID,
                MucDoPhongPhuBaiBaoCaos[iIndex].DANHGIA,
                MucDoPhongPhuBaiBaoCaos[iIndex].STT
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("CHUYENKHOADAOTAOTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("DANHGIA",typeof(int?));
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
            {
                OMucDoPhongPhuBaiBaoCao.ID,
                OMucDoPhongPhuBaiBaoCao.CHUYENKHOADAOTAOTTMA,
                OMucDoPhongPhuBaiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID,
                OMucDoPhongPhuBaiBaoCao.DANHGIA,
                OMucDoPhongPhuBaiBaoCao.STT
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
