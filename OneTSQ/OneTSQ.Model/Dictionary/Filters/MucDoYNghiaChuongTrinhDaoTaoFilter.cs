using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class MucDoYNghiaChuongTrinhDaoTaoFilterCls:FilterCls
    {
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID;
    }
}

public class MucDoYNghiaChuongTrinhDaoTaoFilterParser 
{ 
    public static MucDoYNghiaChuongTrinhDaoTaoFilterCls CreateInstance() 
    { 
        MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter = new MucDoYNghiaChuongTrinhDaoTaoFilterCls(); 
        return OMucDoYNghiaChuongTrinhDaoTaoFilter; 
    } 


    public static MucDoYNghiaChuongTrinhDaoTaoFilterCls ParseFromDataRow(DataRow dr) 
    { 
        MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter = new MucDoYNghiaChuongTrinhDaoTaoFilterCls(); 
        OMucDoYNghiaChuongTrinhDaoTaoFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        OMucDoYNghiaChuongTrinhDaoTaoFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return OMucDoYNghiaChuongTrinhDaoTaoFilter;
    }


    public static MucDoYNghiaChuongTrinhDaoTaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OMucDoYNghiaChuongTrinhDaoTaoFilter;
    }



    public static XmlCls GetXml(MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter)
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
                OMucDoYNghiaChuongTrinhDaoTaoFilter.ActiveOnly,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.DepartmentId,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.ExcOwnerId,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.IncludeSharing,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.Keyword,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.OwnerId,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.OwnerUserId,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.PageIndex,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.PageSize,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.RoleId,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.SortField,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.SortType,
                OMucDoYNghiaChuongTrinhDaoTaoFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
