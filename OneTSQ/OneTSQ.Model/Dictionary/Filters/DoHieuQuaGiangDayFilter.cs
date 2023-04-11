using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DoHieuQuaGiangDayFilterCls : FilterCls
    {
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID;
    }
}

public class DoHieuQuaGiangDayFilterParser
{
    public static DoHieuQuaGiangDayFilterCls CreateInstance()
    {
        DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter = new DoHieuQuaGiangDayFilterCls();
        return ODoHieuQuaGiangDayFilter;
    }


    public static DoHieuQuaGiangDayFilterCls ParseFromDataRow(DataRow dr)
    {
        DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter = new DoHieuQuaGiangDayFilterCls();
        ODoHieuQuaGiangDayFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        ODoHieuQuaGiangDayFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        ODoHieuQuaGiangDayFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        ODoHieuQuaGiangDayFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        ODoHieuQuaGiangDayFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        ODoHieuQuaGiangDayFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        ODoHieuQuaGiangDayFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        ODoHieuQuaGiangDayFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        ODoHieuQuaGiangDayFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        ODoHieuQuaGiangDayFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        ODoHieuQuaGiangDayFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        ODoHieuQuaGiangDayFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        ODoHieuQuaGiangDayFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return ODoHieuQuaGiangDayFilter;
    }


    public static DoHieuQuaGiangDayFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODoHieuQuaGiangDayFilter;
    }



    public static XmlCls GetXml(DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ActiveOnly");
        ds.Tables["info"].Columns.Add("DepartmentId");
        ds.Tables["info"].Columns.Add("ExcOwnerId");
        ds.Tables["info"].Columns.Add("IncludeSharing");
        ds.Tables["info"].Columns.Add("Keyword");
        ds.Tables["info"].Columns.Add("OwnerId");
        ds.Tables["info"].Columns.Add("OwnerUserId");
        ds.Tables["info"].Columns.Add("PageIndex");
        ds.Tables["info"].Columns.Add("PageSize");
        ds.Tables["info"].Columns.Add("RoleId");
        ds.Tables["info"].Columns.Add("SortField");
        ds.Tables["info"].Columns.Add("SortType");
        ds.Tables["info"].Columns.Add("Status");
        ds.Tables["info"].Rows.Add(new object[]
            {
                ODoHieuQuaGiangDayFilter.ActiveOnly,
                ODoHieuQuaGiangDayFilter.DepartmentId,
                ODoHieuQuaGiangDayFilter.ExcOwnerId,
                ODoHieuQuaGiangDayFilter.IncludeSharing,
                ODoHieuQuaGiangDayFilter.Keyword,
                ODoHieuQuaGiangDayFilter.OwnerId,
                ODoHieuQuaGiangDayFilter.OwnerUserId,
                ODoHieuQuaGiangDayFilter.PageIndex,
                ODoHieuQuaGiangDayFilter.PageSize,
                ODoHieuQuaGiangDayFilter.RoleId,
                ODoHieuQuaGiangDayFilter.SortField,
                ODoHieuQuaGiangDayFilter.SortType,
                ODoHieuQuaGiangDayFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
