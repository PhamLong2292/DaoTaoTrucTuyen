using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class BcDanhGiaChatLuongDaoTaoFilterCls : FilterCls
    {
        public string BENHVIENID;
        public DateTime? TUNGAY;
        public DateTime? DENNGAY;
        public int? TRANGTHAI;
        public string DataPermissionQuery;
    }
}

public class BcDanhGiaChatLuongDaoTaoFilterParser
{
    public static BcDanhGiaChatLuongDaoTaoFilterCls CreateInstance()
    {
        BcDanhGiaChatLuongDaoTaoFilterCls OBcDanhGiaChatLuongDaoTaoFilter = new BcDanhGiaChatLuongDaoTaoFilterCls();
        return OBcDanhGiaChatLuongDaoTaoFilter;
    }


    public static BcDanhGiaChatLuongDaoTaoFilterCls ParseFromDataRow(DataRow dr)
    {
        BcDanhGiaChatLuongDaoTaoFilterCls OBcDanhGiaChatLuongDaoTaoFilter = new BcDanhGiaChatLuongDaoTaoFilterCls();
        OBcDanhGiaChatLuongDaoTaoFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        OBcDanhGiaChatLuongDaoTaoFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        OBcDanhGiaChatLuongDaoTaoFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        OBcDanhGiaChatLuongDaoTaoFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        OBcDanhGiaChatLuongDaoTaoFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        OBcDanhGiaChatLuongDaoTaoFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        OBcDanhGiaChatLuongDaoTaoFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        OBcDanhGiaChatLuongDaoTaoFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        OBcDanhGiaChatLuongDaoTaoFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        OBcDanhGiaChatLuongDaoTaoFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        OBcDanhGiaChatLuongDaoTaoFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        OBcDanhGiaChatLuongDaoTaoFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        OBcDanhGiaChatLuongDaoTaoFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return OBcDanhGiaChatLuongDaoTaoFilter;
    }


    public static BcDanhGiaChatLuongDaoTaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        BcDanhGiaChatLuongDaoTaoFilterCls OBcDanhGiaChatLuongDaoTaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBcDanhGiaChatLuongDaoTaoFilter;
    }



    public static XmlCls GetXml(BcDanhGiaChatLuongDaoTaoFilterCls OBcDanhGiaChatLuongDaoTaoFilter)
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
                OBcDanhGiaChatLuongDaoTaoFilter.ActiveOnly,
                OBcDanhGiaChatLuongDaoTaoFilter.DepartmentId,
                OBcDanhGiaChatLuongDaoTaoFilter.ExcOwnerId,
                OBcDanhGiaChatLuongDaoTaoFilter.IncludeSharing,
                OBcDanhGiaChatLuongDaoTaoFilter.Keyword,
                OBcDanhGiaChatLuongDaoTaoFilter.OwnerId,
                OBcDanhGiaChatLuongDaoTaoFilter.OwnerUserId,
                OBcDanhGiaChatLuongDaoTaoFilter.PageIndex,
                OBcDanhGiaChatLuongDaoTaoFilter.PageSize,
                OBcDanhGiaChatLuongDaoTaoFilter.RoleId,
                OBcDanhGiaChatLuongDaoTaoFilter.SortField,
                OBcDanhGiaChatLuongDaoTaoFilter.SortType,
                OBcDanhGiaChatLuongDaoTaoFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
