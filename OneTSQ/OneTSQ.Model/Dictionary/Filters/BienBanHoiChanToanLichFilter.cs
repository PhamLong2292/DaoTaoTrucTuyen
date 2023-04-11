
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class BienBanHoiChanToanLichFilterCls:FilterCls 
    { 
    }
}

public class BienBanHoiChanToanLichFilterParser 
{ 
    public static BienBanHoiChanToanLichFilterCls CreateInstance() 
    { 
        BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter = new BienBanHoiChanToanLichFilterCls(); 
        return OBienBanHoiChanToanLichFilter; 
    } 


    public static BienBanHoiChanToanLichFilterCls ParseFromDataRow(DataRow dr) 
    { 
        BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter = new BienBanHoiChanToanLichFilterCls(); 
        OBienBanHoiChanToanLichFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        OBienBanHoiChanToanLichFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        OBienBanHoiChanToanLichFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        OBienBanHoiChanToanLichFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        OBienBanHoiChanToanLichFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        OBienBanHoiChanToanLichFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        OBienBanHoiChanToanLichFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        OBienBanHoiChanToanLichFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        OBienBanHoiChanToanLichFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        OBienBanHoiChanToanLichFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        OBienBanHoiChanToanLichFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        OBienBanHoiChanToanLichFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        OBienBanHoiChanToanLichFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return OBienBanHoiChanToanLichFilter;
    }


    public static BienBanHoiChanToanLichFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBienBanHoiChanToanLichFilter;
    }



    public static XmlCls GetXml(BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter)
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
                OBienBanHoiChanToanLichFilter.ActiveOnly,
                OBienBanHoiChanToanLichFilter.DepartmentId,
                OBienBanHoiChanToanLichFilter.ExcOwnerId,
                OBienBanHoiChanToanLichFilter.IncludeSharing,
                OBienBanHoiChanToanLichFilter.Keyword,
                OBienBanHoiChanToanLichFilter.OwnerId,
                OBienBanHoiChanToanLichFilter.OwnerUserId,
                OBienBanHoiChanToanLichFilter.PageIndex,
                OBienBanHoiChanToanLichFilter.PageSize,
                OBienBanHoiChanToanLichFilter.RoleId,
                OBienBanHoiChanToanLichFilter.SortField,
                OBienBanHoiChanToanLichFilter.SortType,
                OBienBanHoiChanToanLichFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
