using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class DanhGiaThoiGianBuoiBaoCaoFilterCls:FilterCls
    {
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID;
    }
}

public class DanhGiaThoiGianBuoiBaoCaoFilterParser 
{ 
    public static DanhGiaThoiGianBuoiBaoCaoFilterCls CreateInstance() 
    { 
        DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter = new DanhGiaThoiGianBuoiBaoCaoFilterCls(); 
        return ODanhGiaThoiGianBuoiBaoCaoFilter; 
    } 


    public static DanhGiaThoiGianBuoiBaoCaoFilterCls ParseFromDataRow(DataRow dr) 
    { 
        DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter = new DanhGiaThoiGianBuoiBaoCaoFilterCls(); 
        ODanhGiaThoiGianBuoiBaoCaoFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        ODanhGiaThoiGianBuoiBaoCaoFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return ODanhGiaThoiGianBuoiBaoCaoFilter;
    }


    public static DanhGiaThoiGianBuoiBaoCaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODanhGiaThoiGianBuoiBaoCaoFilter;
    }



    public static XmlCls GetXml(DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter)
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
                ODanhGiaThoiGianBuoiBaoCaoFilter.ActiveOnly,
                ODanhGiaThoiGianBuoiBaoCaoFilter.DepartmentId,
                ODanhGiaThoiGianBuoiBaoCaoFilter.ExcOwnerId,
                ODanhGiaThoiGianBuoiBaoCaoFilter.IncludeSharing,
                ODanhGiaThoiGianBuoiBaoCaoFilter.Keyword,
                ODanhGiaThoiGianBuoiBaoCaoFilter.OwnerId,
                ODanhGiaThoiGianBuoiBaoCaoFilter.OwnerUserId,
                ODanhGiaThoiGianBuoiBaoCaoFilter.PageIndex,
                ODanhGiaThoiGianBuoiBaoCaoFilter.PageSize,
                ODanhGiaThoiGianBuoiBaoCaoFilter.RoleId,
                ODanhGiaThoiGianBuoiBaoCaoFilter.SortField,
                ODanhGiaThoiGianBuoiBaoCaoFilter.SortType,
                ODanhGiaThoiGianBuoiBaoCaoFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
