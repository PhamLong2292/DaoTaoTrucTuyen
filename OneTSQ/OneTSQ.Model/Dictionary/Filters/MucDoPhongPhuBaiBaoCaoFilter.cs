using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class MucDoPhongPhuBaiBaoCaoFilterCls:FilterCls
    {
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID;
    }
}

public class MucDoPhongPhuBaiBaoCaoFilterParser 
{ 
    public static MucDoPhongPhuBaiBaoCaoFilterCls CreateInstance() 
    { 
        MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter = new MucDoPhongPhuBaiBaoCaoFilterCls(); 
        return OMucDoPhongPhuBaiBaoCaoFilter; 
    } 


    public static MucDoPhongPhuBaiBaoCaoFilterCls ParseFromDataRow(DataRow dr) 
    { 
        MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter = new MucDoPhongPhuBaiBaoCaoFilterCls(); 
        OMucDoPhongPhuBaiBaoCaoFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        OMucDoPhongPhuBaiBaoCaoFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        OMucDoPhongPhuBaiBaoCaoFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        OMucDoPhongPhuBaiBaoCaoFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        OMucDoPhongPhuBaiBaoCaoFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        OMucDoPhongPhuBaiBaoCaoFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        OMucDoPhongPhuBaiBaoCaoFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        OMucDoPhongPhuBaiBaoCaoFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        OMucDoPhongPhuBaiBaoCaoFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        OMucDoPhongPhuBaiBaoCaoFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        OMucDoPhongPhuBaiBaoCaoFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        OMucDoPhongPhuBaiBaoCaoFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        OMucDoPhongPhuBaiBaoCaoFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return OMucDoPhongPhuBaiBaoCaoFilter;
    }


    public static MucDoPhongPhuBaiBaoCaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OMucDoPhongPhuBaiBaoCaoFilter;
    }



    public static XmlCls GetXml(MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter)
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
                OMucDoPhongPhuBaiBaoCaoFilter.ActiveOnly,
                OMucDoPhongPhuBaiBaoCaoFilter.DepartmentId,
                OMucDoPhongPhuBaiBaoCaoFilter.ExcOwnerId,
                OMucDoPhongPhuBaiBaoCaoFilter.IncludeSharing,
                OMucDoPhongPhuBaiBaoCaoFilter.Keyword,
                OMucDoPhongPhuBaiBaoCaoFilter.OwnerId,
                OMucDoPhongPhuBaiBaoCaoFilter.OwnerUserId,
                OMucDoPhongPhuBaiBaoCaoFilter.PageIndex,
                OMucDoPhongPhuBaiBaoCaoFilter.PageSize,
                OMucDoPhongPhuBaiBaoCaoFilter.RoleId,
                OMucDoPhongPhuBaiBaoCaoFilter.SortField,
                OMucDoPhongPhuBaiBaoCaoFilter.SortType,
                OMucDoPhongPhuBaiBaoCaoFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
