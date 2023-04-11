using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class KetQuaXetNghiemFilterCls : FilterCls
    {
        public string CABENHID;
    }
}

public class KetQuaXetNghiemFilterParser
{
    public static KetQuaXetNghiemFilterCls CreateInstance()
    {
        KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter = new KetQuaXetNghiemFilterCls();
        return OKetQuaXetNghiemFilter;
    }


    public static KetQuaXetNghiemFilterCls ParseFromDataRow(DataRow dr)
    {
        KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter = new KetQuaXetNghiemFilterCls();
        OKetQuaXetNghiemFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        OKetQuaXetNghiemFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        OKetQuaXetNghiemFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        OKetQuaXetNghiemFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        OKetQuaXetNghiemFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        OKetQuaXetNghiemFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        OKetQuaXetNghiemFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        OKetQuaXetNghiemFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        OKetQuaXetNghiemFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        OKetQuaXetNghiemFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        OKetQuaXetNghiemFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        OKetQuaXetNghiemFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        OKetQuaXetNghiemFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return OKetQuaXetNghiemFilter;
    }


    public static KetQuaXetNghiemFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OKetQuaXetNghiemFilter;
    }



    public static XmlCls GetXml(KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter)
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
                OKetQuaXetNghiemFilter.ActiveOnly,
                OKetQuaXetNghiemFilter.DepartmentId,
                OKetQuaXetNghiemFilter.ExcOwnerId,
                OKetQuaXetNghiemFilter.IncludeSharing,
                OKetQuaXetNghiemFilter.Keyword,
                OKetQuaXetNghiemFilter.OwnerId,
                OKetQuaXetNghiemFilter.OwnerUserId,
                OKetQuaXetNghiemFilter.PageIndex,
                OKetQuaXetNghiemFilter.PageSize,
                OKetQuaXetNghiemFilter.RoleId,
                OKetQuaXetNghiemFilter.SortField,
                OKetQuaXetNghiemFilter.SortType,
                OKetQuaXetNghiemFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
