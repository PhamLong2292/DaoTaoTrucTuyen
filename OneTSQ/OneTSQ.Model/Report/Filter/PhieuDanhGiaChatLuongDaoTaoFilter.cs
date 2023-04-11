using System;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class PhieuDanhGiaChatLuongDaoTaoFilterCls : FilterCls
    {
        public string BENHVIENTHAMVAN_ID;
        public string BENHVIENTUVAN_ID;
        public int? QUY;
        public int? NAM;
        public int? TRANGTHAI;
        public string DataPermissionQuery;
    }
}

public class PhieuDanhGiaChatLuongDaoTaoFilterParser
{
    public static PhieuDanhGiaChatLuongDaoTaoFilterCls CreateInstance()
    {
        PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter = new PhieuDanhGiaChatLuongDaoTaoFilterCls();
        return OPhieuDanhGiaChatLuongDaoTaoFilter;
    }


    public static PhieuDanhGiaChatLuongDaoTaoFilterCls ParseFromDataRow(DataRow dr)
    {
        PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter = new PhieuDanhGiaChatLuongDaoTaoFilterCls();
        OPhieuDanhGiaChatLuongDaoTaoFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        OPhieuDanhGiaChatLuongDaoTaoFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return OPhieuDanhGiaChatLuongDaoTaoFilter;
    }


    public static PhieuDanhGiaChatLuongDaoTaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OPhieuDanhGiaChatLuongDaoTaoFilter;
    }



    public static XmlCls GetXml(PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter)
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
                OPhieuDanhGiaChatLuongDaoTaoFilter.ActiveOnly,
                OPhieuDanhGiaChatLuongDaoTaoFilter.DepartmentId,
                OPhieuDanhGiaChatLuongDaoTaoFilter.ExcOwnerId,
                OPhieuDanhGiaChatLuongDaoTaoFilter.IncludeSharing,
                OPhieuDanhGiaChatLuongDaoTaoFilter.Keyword,
                OPhieuDanhGiaChatLuongDaoTaoFilter.OwnerId,
                OPhieuDanhGiaChatLuongDaoTaoFilter.OwnerUserId,
                OPhieuDanhGiaChatLuongDaoTaoFilter.PageIndex,
                OPhieuDanhGiaChatLuongDaoTaoFilter.PageSize,
                OPhieuDanhGiaChatLuongDaoTaoFilter.RoleId,
                OPhieuDanhGiaChatLuongDaoTaoFilter.SortField,
                OPhieuDanhGiaChatLuongDaoTaoFilter.SortType,
                OPhieuDanhGiaChatLuongDaoTaoFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
