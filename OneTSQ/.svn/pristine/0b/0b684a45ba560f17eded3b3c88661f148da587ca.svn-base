using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class NoiDungHoiChanFilterCls:FilterCls
    {
        public string lichHoiChanId;
    }
}

public class NoiDungHoiChanFilterParser 
{ 
    public static NoiDungHoiChanFilterCls CreateInstance() 
    { 
        NoiDungHoiChanFilterCls ONoiDungHoiChanFilter = new NoiDungHoiChanFilterCls(); 
        return ONoiDungHoiChanFilter; 
    } 


    public static NoiDungHoiChanFilterCls ParseFromDataRow(DataRow dr) 
    { 
        NoiDungHoiChanFilterCls ONoiDungHoiChanFilter = new NoiDungHoiChanFilterCls(); 
        ONoiDungHoiChanFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        ONoiDungHoiChanFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        ONoiDungHoiChanFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        ONoiDungHoiChanFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        ONoiDungHoiChanFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        ONoiDungHoiChanFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        ONoiDungHoiChanFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        ONoiDungHoiChanFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        ONoiDungHoiChanFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        ONoiDungHoiChanFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        ONoiDungHoiChanFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        ONoiDungHoiChanFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        ONoiDungHoiChanFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return ONoiDungHoiChanFilter;
    }


    public static NoiDungHoiChanFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        NoiDungHoiChanFilterCls ONoiDungHoiChanFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ONoiDungHoiChanFilter;
    }



    public static XmlCls GetXml(NoiDungHoiChanFilterCls ONoiDungHoiChanFilter)
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
                ONoiDungHoiChanFilter.ActiveOnly,
                ONoiDungHoiChanFilter.DepartmentId,
                ONoiDungHoiChanFilter.ExcOwnerId,
                ONoiDungHoiChanFilter.IncludeSharing,
                ONoiDungHoiChanFilter.Keyword,
                ONoiDungHoiChanFilter.OwnerId,
                ONoiDungHoiChanFilter.OwnerUserId,
                ONoiDungHoiChanFilter.PageIndex,
                ONoiDungHoiChanFilter.PageSize,
                ONoiDungHoiChanFilter.RoleId,
                ONoiDungHoiChanFilter.SortField,
                ONoiDungHoiChanFilter.SortType,
                ONoiDungHoiChanFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
