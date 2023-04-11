using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class DanhGiaThoiLuongBuoiBaoCaoFilterCls:FilterCls
    {
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID;
    }
}

public class DanhGiaThoiLuongBuoiBaoCaoFilterParser 
{ 
    public static DanhGiaThoiLuongBuoiBaoCaoFilterCls CreateInstance() 
    { 
        DanhGiaThoiLuongBuoiBaoCaoFilterCls ODanhGiaThoiLuongBuoiBaoCaoFilter = new DanhGiaThoiLuongBuoiBaoCaoFilterCls(); 
        return ODanhGiaThoiLuongBuoiBaoCaoFilter; 
    } 


    public static DanhGiaThoiLuongBuoiBaoCaoFilterCls ParseFromDataRow(DataRow dr) 
    { 
        DanhGiaThoiLuongBuoiBaoCaoFilterCls ODanhGiaThoiLuongBuoiBaoCaoFilter = new DanhGiaThoiLuongBuoiBaoCaoFilterCls(); 
        ODanhGiaThoiLuongBuoiBaoCaoFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        ODanhGiaThoiLuongBuoiBaoCaoFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return ODanhGiaThoiLuongBuoiBaoCaoFilter;
    }


    public static DanhGiaThoiLuongBuoiBaoCaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        DanhGiaThoiLuongBuoiBaoCaoFilterCls ODanhGiaThoiLuongBuoiBaoCaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODanhGiaThoiLuongBuoiBaoCaoFilter;
    }



    public static XmlCls GetXml(DanhGiaThoiLuongBuoiBaoCaoFilterCls ODanhGiaThoiLuongBuoiBaoCaoFilter)
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
                ODanhGiaThoiLuongBuoiBaoCaoFilter.ActiveOnly,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.DepartmentId,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.ExcOwnerId,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.IncludeSharing,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.Keyword,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.OwnerId,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.OwnerUserId,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.PageIndex,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.PageSize,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.RoleId,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.SortField,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.SortType,
                ODanhGiaThoiLuongBuoiBaoCaoFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
