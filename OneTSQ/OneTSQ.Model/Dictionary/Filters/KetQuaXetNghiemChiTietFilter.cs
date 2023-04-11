using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class KetQuaXetNghiemChiTietFilterCls : FilterCls
    {
        public string CABENHID;
        public string KETQUAXETNGHIEMID;
    }
}

public class KetQuaXetNghiemChiTietFilterParser
{
    public static KetQuaXetNghiemChiTietFilterCls CreateInstance()
    {
        KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter = new KetQuaXetNghiemChiTietFilterCls();
        return OKetQuaXetNghiemChiTietFilter;
    }


    public static KetQuaXetNghiemChiTietFilterCls ParseFromDataRow(DataRow dr)
    {
        KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter = new KetQuaXetNghiemChiTietFilterCls();
        OKetQuaXetNghiemChiTietFilter.ActiveOnly = CoreXmlUtility.GetInt(dr, "ActiveOnly", true);
        OKetQuaXetNghiemChiTietFilter.DepartmentId = CoreXmlUtility.GetString(dr, "DepartmentId", true);
        OKetQuaXetNghiemChiTietFilter.ExcOwnerId = CoreXmlUtility.GetString(dr, "ExcOwnerId", true);
        OKetQuaXetNghiemChiTietFilter.IncludeSharing = CoreXmlUtility.GetInt(dr, "IncludeSharing", true);
        OKetQuaXetNghiemChiTietFilter.Keyword = CoreXmlUtility.GetString(dr, "Keyword", true);
        OKetQuaXetNghiemChiTietFilter.OwnerId = CoreXmlUtility.GetString(dr, "OwnerId", true);
        OKetQuaXetNghiemChiTietFilter.OwnerUserId = CoreXmlUtility.GetString(dr, "OwnerUserId", true);
        OKetQuaXetNghiemChiTietFilter.PageIndex = CoreXmlUtility.GetInt(dr, "PageIndex", true);
        OKetQuaXetNghiemChiTietFilter.PageSize = CoreXmlUtility.GetInt(dr, "PageSize", true);
        OKetQuaXetNghiemChiTietFilter.RoleId = CoreXmlUtility.GetString(dr, "RoleId", true);
        OKetQuaXetNghiemChiTietFilter.SortField = CoreXmlUtility.GetString(dr, "SortField", true);
        OKetQuaXetNghiemChiTietFilter.SortType = CoreXmlUtility.GetString(dr, "SortType", true);
        OKetQuaXetNghiemChiTietFilter.Status = CoreXmlUtility.GetInt(dr, "Status", true);
        return OKetQuaXetNghiemChiTietFilter;
    }


    public static KetQuaXetNghiemChiTietFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OKetQuaXetNghiemChiTietFilter;
    }



    public static XmlCls GetXml(KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter)
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
                OKetQuaXetNghiemChiTietFilter.ActiveOnly,
                OKetQuaXetNghiemChiTietFilter.DepartmentId,
                OKetQuaXetNghiemChiTietFilter.ExcOwnerId,
                OKetQuaXetNghiemChiTietFilter.IncludeSharing,
                OKetQuaXetNghiemChiTietFilter.Keyword,
                OKetQuaXetNghiemChiTietFilter.OwnerId,
                OKetQuaXetNghiemChiTietFilter.OwnerUserId,
                OKetQuaXetNghiemChiTietFilter.PageIndex,
                OKetQuaXetNghiemChiTietFilter.PageSize,
                OKetQuaXetNghiemChiTietFilter.RoleId,
                OKetQuaXetNghiemChiTietFilter.SortField,
                OKetQuaXetNghiemChiTietFilter.SortType,
                OKetQuaXetNghiemChiTietFilter.Status
            });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
