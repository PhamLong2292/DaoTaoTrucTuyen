using OneTSQ.Core.Model;
using System.Data;

namespace OneTSQ.Model
{
    public class DM_YKienBenhVienFilterCls : FilterCls
    {
        public int? HieuLuc;
    }

    public class DM_YKienBenhVienFilterParser
    {
        public static DM_YKienBenhVienFilterCls CreateInstance()
        {
            DM_YKienBenhVienFilterCls OYKienBenhVienFilter = new DM_YKienBenhVienFilterCls();
            return OYKienBenhVienFilter;
        }


        public static DM_YKienBenhVienFilterCls ParseFromDataRow(DataRow dr)
        {
            DM_YKienBenhVienFilterCls OYKienBenhVienFilter = new DM_YKienBenhVienFilterCls();
            return OYKienBenhVienFilter;
        }


        public static DM_YKienBenhVienFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_YKienBenhVienFilterCls OYKienBenhVienFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OYKienBenhVienFilter;
        }



        public static XmlCls GetXml(DM_YKienBenhVienFilterCls OYKienBenhVienFilter)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("info");
            XmlCls OXml = new XmlCls();
            OXml.XmlData = ds.GetXml();
            OXml.XmlDataSchema = ds.GetXmlSchema();
            return OXml;
        }
    }
}
