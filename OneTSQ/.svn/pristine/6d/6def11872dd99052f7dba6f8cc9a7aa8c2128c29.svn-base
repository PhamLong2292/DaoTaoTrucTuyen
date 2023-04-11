using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class DanhGiaThoiGianBuoiBaoCaoCls 
    { 
        public string ID; 
        public string TIEUCHITHOIGIANDAOTAOTTMA; 
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID; 
        public int? DANHGIA;
        public int STT;

        public enum eDanhGia
        {
            Co = 1,
            Khong = 2
        }
    }
}

public class DanhGiaThoiGianBuoiBaoCaoParser 
{ 
    public static DanhGiaThoiGianBuoiBaoCaoCls CreateInstance() 
    { 
        DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao = new DanhGiaThoiGianBuoiBaoCaoCls(); 
        return ODanhGiaThoiGianBuoiBaoCao; 
    } 


    public static DanhGiaThoiGianBuoiBaoCaoCls ParseFromDataRow(DataRow dr) 
    { 
        DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao = new DanhGiaThoiGianBuoiBaoCaoCls(); 
        ODanhGiaThoiGianBuoiBaoCao.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODanhGiaThoiGianBuoiBaoCao.TIEUCHITHOIGIANDAOTAOTTMA = CoreXmlUtility.GetString(dr, "TIEUCHITHOIGIANDAOTAOTTMA", true);
        ODanhGiaThoiGianBuoiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID = CoreXmlUtility.GetString(dr, "PHIEUDANHGIACHATLUONGDAOTAO_ID", true);
        ODanhGiaThoiGianBuoiBaoCao.DANHGIA = CoreXmlUtility.GetIntOrNull(dr, "DANHGIA", true);
        ODanhGiaThoiGianBuoiBaoCao.STT = CoreXmlUtility.GetInt(dr, "STT", true);
        return ODanhGiaThoiGianBuoiBaoCao;
    }

    public static DanhGiaThoiGianBuoiBaoCaoCls[] ParseFromDataTable(DataTable dtTable) 
    {
        DanhGiaThoiGianBuoiBaoCaoCls[] DanhGiaThoiGianBuoiBaoCaos = new DanhGiaThoiGianBuoiBaoCaoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DanhGiaThoiGianBuoiBaoCaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DanhGiaThoiGianBuoiBaoCaos;
    }


    public static DanhGiaThoiGianBuoiBaoCaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DanhGiaThoiGianBuoiBaoCaoCls[] DanhGiaThoiGianBuoiBaoCaos = ParseFromDataTable(ds.Tables[0]);
        return DanhGiaThoiGianBuoiBaoCaos;
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

    public static DanhGiaThoiGianBuoiBaoCaoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODanhGiaThoiGianBuoiBaoCao;
    }


    public static XmlCls GetXml(DanhGiaThoiGianBuoiBaoCaoCls[] DanhGiaThoiGianBuoiBaoCaos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("TIEUCHITHOIGIANDAOTAOTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("DANHGIA",typeof(int?));
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        for (int iIndex = 0; iIndex < DanhGiaThoiGianBuoiBaoCaos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DanhGiaThoiGianBuoiBaoCaos[iIndex].ID,
                DanhGiaThoiGianBuoiBaoCaos[iIndex].TIEUCHITHOIGIANDAOTAOTTMA,
                DanhGiaThoiGianBuoiBaoCaos[iIndex].PHIEUDANHGIACHATLUONGDAOTAO_ID,
                DanhGiaThoiGianBuoiBaoCaos[iIndex].DANHGIA,
                DanhGiaThoiGianBuoiBaoCaos[iIndex].STT
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("TIEUCHITHOIGIANDAOTAOTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("DANHGIA",typeof(int?));
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
            {
                ODanhGiaThoiGianBuoiBaoCao.ID,
                ODanhGiaThoiGianBuoiBaoCao.TIEUCHITHOIGIANDAOTAOTTMA,
                ODanhGiaThoiGianBuoiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID,
                ODanhGiaThoiGianBuoiBaoCao.DANHGIA,
                ODanhGiaThoiGianBuoiBaoCao.STT
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
