using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Model 
{
    public class DM_KyThuatChuyenGiaoFilterCls : FilterCls
    {
        public int? HieuLuc;
    }

    public class DM_KyThuatChuyenGiaoFilterParser
    {
        public static DM_KyThuatChuyenGiaoFilterCls CreateInstance()
        {
            DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter = new DM_KyThuatChuyenGiaoFilterCls();
            return OKyThuatChuyenGiaoFilter;
        }


        public static DM_KyThuatChuyenGiaoFilterCls ParseFromDataRow(DataRow dr)
        {
            DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter = new DM_KyThuatChuyenGiaoFilterCls();
            return OKyThuatChuyenGiaoFilter;
        }


        public static DM_KyThuatChuyenGiaoFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OKyThuatChuyenGiaoFilter;
        }



        public static XmlCls GetXml(DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter)
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
