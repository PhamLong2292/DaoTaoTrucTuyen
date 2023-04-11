
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class BienBanHoiChanToanLichBacSyFilterCls:FilterCls 
    {
        public string lichHoiChanId;
        public int? isChuyenGia;
    }
}

public class BienBanHoiChanToanLichBacSyFilterParser 
{ 
    public static BienBanHoiChanToanLichBacSyFilterCls CreateInstance() 
    { 
        BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter = new BienBanHoiChanToanLichBacSyFilterCls(); 
        return OBienBanHoiChanToanLichBacSyFilter; 
    } 


    public static BienBanHoiChanToanLichBacSyFilterCls ParseFromDataRow(DataRow dr) 
    { 
        BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter = new BienBanHoiChanToanLichBacSyFilterCls(); 
        OBienBanHoiChanToanLichBacSyFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        OBienBanHoiChanToanLichBacSyFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        OBienBanHoiChanToanLichBacSyFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        OBienBanHoiChanToanLichBacSyFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        OBienBanHoiChanToanLichBacSyFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        OBienBanHoiChanToanLichBacSyFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        OBienBanHoiChanToanLichBacSyFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        OBienBanHoiChanToanLichBacSyFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        OBienBanHoiChanToanLichBacSyFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        OBienBanHoiChanToanLichBacSyFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        OBienBanHoiChanToanLichBacSyFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        OBienBanHoiChanToanLichBacSyFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        OBienBanHoiChanToanLichBacSyFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return OBienBanHoiChanToanLichBacSyFilter;
    }


    public static BienBanHoiChanToanLichBacSyFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBienBanHoiChanToanLichBacSyFilter;
    }



    public static XmlCls GetXml(BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter)
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
                OBienBanHoiChanToanLichBacSyFilter.ActiveOnly,
                OBienBanHoiChanToanLichBacSyFilter.DepartmentId,
                OBienBanHoiChanToanLichBacSyFilter.ExcOwnerId,
                OBienBanHoiChanToanLichBacSyFilter.IncludeSharing,
                OBienBanHoiChanToanLichBacSyFilter.Keyword,
                OBienBanHoiChanToanLichBacSyFilter.OwnerId,
                OBienBanHoiChanToanLichBacSyFilter.OwnerUserId,
                OBienBanHoiChanToanLichBacSyFilter.PageIndex,
                OBienBanHoiChanToanLichBacSyFilter.PageSize,
                OBienBanHoiChanToanLichBacSyFilter.RoleId,
                OBienBanHoiChanToanLichBacSyFilter.SortField,
                OBienBanHoiChanToanLichBacSyFilter.SortType,
                OBienBanHoiChanToanLichBacSyFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
