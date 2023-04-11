using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{ 
    public class ChatLuongHoatDongTtbFilterCls:FilterCls
    {
        public string PHIEUDANHGIACHATLUONGDAOTAO_ID;
    }
}

public class ChatLuongHoatDongTtbFilterParser 
{ 
    public static ChatLuongHoatDongTtbFilterCls CreateInstance() 
    { 
        ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter = new ChatLuongHoatDongTtbFilterCls(); 
        return OChatLuongHoatDongTtbFilter; 
    } 


    public static ChatLuongHoatDongTtbFilterCls ParseFromDataRow(DataRow dr) 
    { 
        ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter = new ChatLuongHoatDongTtbFilterCls(); 
        OChatLuongHoatDongTtbFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        OChatLuongHoatDongTtbFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        OChatLuongHoatDongTtbFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        OChatLuongHoatDongTtbFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        OChatLuongHoatDongTtbFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        OChatLuongHoatDongTtbFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        OChatLuongHoatDongTtbFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        OChatLuongHoatDongTtbFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        OChatLuongHoatDongTtbFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        OChatLuongHoatDongTtbFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        OChatLuongHoatDongTtbFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        OChatLuongHoatDongTtbFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        OChatLuongHoatDongTtbFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return OChatLuongHoatDongTtbFilter;
    }


    public static ChatLuongHoatDongTtbFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if(ds.Tables[0].Rows.Count==0)return null;
        ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OChatLuongHoatDongTtbFilter;
    }



    public static XmlCls GetXml(ChatLuongHoatDongTtbFilterCls OChatLuongHoatDongTtbFilter)
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
                OChatLuongHoatDongTtbFilter.ActiveOnly,
                OChatLuongHoatDongTtbFilter.DepartmentId,
                OChatLuongHoatDongTtbFilter.ExcOwnerId,
                OChatLuongHoatDongTtbFilter.IncludeSharing,
                OChatLuongHoatDongTtbFilter.Keyword,
                OChatLuongHoatDongTtbFilter.OwnerId,
                OChatLuongHoatDongTtbFilter.OwnerUserId,
                OChatLuongHoatDongTtbFilter.PageIndex,
                OChatLuongHoatDongTtbFilter.PageSize,
                OChatLuongHoatDongTtbFilter.RoleId,
                OChatLuongHoatDongTtbFilter.SortField,
                OChatLuongHoatDongTtbFilter.SortType,
                OChatLuongHoatDongTtbFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
