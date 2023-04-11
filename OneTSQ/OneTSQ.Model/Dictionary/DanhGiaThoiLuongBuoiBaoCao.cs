using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class DanhGiaThoiLuongBuoiBaoCaoCls 
    { 
        public string ID; 
        public string TIEUCHITHOILUONGDAOTAOTTMA; 
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID; 
        public int? DANHGIA;
        public string LYDOTHUA;
        public string LYDOTHIEU;
        public int STT;

        public enum eDanhGia
        {
            Thua = 0,
            Du = 1,
            Thieu = 2
        }
    }
}

public class DanhGiaThoiLuongBuoiBaoCaoParser 
{ 
    public static DanhGiaThoiLuongBuoiBaoCaoCls CreateInstance() 
    { 
        DanhGiaThoiLuongBuoiBaoCaoCls ODanhGiaThoiLuongBuoiBaoCao = new DanhGiaThoiLuongBuoiBaoCaoCls(); 
        return ODanhGiaThoiLuongBuoiBaoCao; 
    } 


    public static DanhGiaThoiLuongBuoiBaoCaoCls ParseFromDataRow(DataRow dr) 
    { 
        DanhGiaThoiLuongBuoiBaoCaoCls ODanhGiaThoiLuongBuoiBaoCao = new DanhGiaThoiLuongBuoiBaoCaoCls(); 
        ODanhGiaThoiLuongBuoiBaoCao.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODanhGiaThoiLuongBuoiBaoCao.TIEUCHITHOILUONGDAOTAOTTMA = CoreXmlUtility.GetString(dr, "TIEUCHITHOILUONGDAOTAOTTMA", true);
        ODanhGiaThoiLuongBuoiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID = CoreXmlUtility.GetString(dr, "PHIEUDANHGIACHATLUONGDAOTAO_ID", true);
        ODanhGiaThoiLuongBuoiBaoCao.DANHGIA = CoreXmlUtility.GetIntOrNull(dr, "DANHGIA", true);
        ODanhGiaThoiLuongBuoiBaoCao.LYDOTHUA = CoreXmlUtility.GetString(dr, "LYDOTHUA", true);
        ODanhGiaThoiLuongBuoiBaoCao.LYDOTHIEU = CoreXmlUtility.GetString(dr, "LYDOTHIEU", true);
        ODanhGiaThoiLuongBuoiBaoCao.STT = CoreXmlUtility.GetInt(dr, "STT", true);
        return ODanhGiaThoiLuongBuoiBaoCao;
    }

    public static DanhGiaThoiLuongBuoiBaoCaoCls[] ParseFromDataTable(DataTable dtTable) 
    {
        DanhGiaThoiLuongBuoiBaoCaoCls[] DanhGiaThoiLuongBuoiBaoCaos = new DanhGiaThoiLuongBuoiBaoCaoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DanhGiaThoiLuongBuoiBaoCaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DanhGiaThoiLuongBuoiBaoCaos;
    }


    public static DanhGiaThoiLuongBuoiBaoCaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DanhGiaThoiLuongBuoiBaoCaoCls[] DanhGiaThoiLuongBuoiBaoCaos = ParseFromDataTable(ds.Tables[0]);
        return DanhGiaThoiLuongBuoiBaoCaos;
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

    public static DanhGiaThoiLuongBuoiBaoCaoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        DanhGiaThoiLuongBuoiBaoCaoCls ODanhGiaThoiLuongBuoiBaoCao = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODanhGiaThoiLuongBuoiBaoCao;
    }


    public static XmlCls GetXml(DanhGiaThoiLuongBuoiBaoCaoCls[] DanhGiaThoiLuongBuoiBaoCaos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("TIEUCHITHOILUONGDAOTAOTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("DANHGIA",typeof(int?));
        ds.Tables["info"].Columns.Add("LYDOTHUA");
        ds.Tables["info"].Columns.Add("LYDOTHIEU");
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        for (int iIndex = 0; iIndex < DanhGiaThoiLuongBuoiBaoCaos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DanhGiaThoiLuongBuoiBaoCaos[iIndex].ID,
                DanhGiaThoiLuongBuoiBaoCaos[iIndex].TIEUCHITHOILUONGDAOTAOTTMA,
                DanhGiaThoiLuongBuoiBaoCaos[iIndex].PHIEUDANHGIACHATLUONGDAOTAO_ID,
                DanhGiaThoiLuongBuoiBaoCaos[iIndex].DANHGIA,
                DanhGiaThoiLuongBuoiBaoCaos[iIndex].LYDOTHUA,
                DanhGiaThoiLuongBuoiBaoCaos[iIndex].LYDOTHIEU,
                DanhGiaThoiLuongBuoiBaoCaos[iIndex].STT
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DanhGiaThoiLuongBuoiBaoCaoCls ODanhGiaThoiLuongBuoiBaoCao)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("TIEUCHITHOILUONGDAOTAOTTMA");
        ds.Tables["info"].Columns.Add("PHIEUDANHGIACHATLUONGDAOTAO_ID");
        ds.Tables["info"].Columns.Add("DANHGIA",typeof(int?));
        ds.Tables["info"].Columns.Add("LYDOTHUA");
        ds.Tables["info"].Columns.Add("LYDOTHIEU");
        ds.Tables["info"].Columns.Add("STT", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
            {
                ODanhGiaThoiLuongBuoiBaoCao.ID,
                ODanhGiaThoiLuongBuoiBaoCao.TIEUCHITHOILUONGDAOTAOTTMA,
                ODanhGiaThoiLuongBuoiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID,
                ODanhGiaThoiLuongBuoiBaoCao.DANHGIA,
                ODanhGiaThoiLuongBuoiBaoCao.LYDOTHUA,
                ODanhGiaThoiLuongBuoiBaoCao.LYDOTHIEU,
                ODanhGiaThoiLuongBuoiBaoCao.STT
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
