using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class PhieuBaoCaoPhanUngCoHaiADRFilterCls : FilterCls
    {
        public string NguoiLap_ID;
        public string HinhThuc_ID;
        public string NoiBaoCao_ID;
        public int? TrangThai;
        public string DataPermissionQuery;
        public DateTime? TuNgay;
        public DateTime? DenNgay;
        public DateTime? NgaySinh;
        public int? GioiTinh;
        public int? KQXuTri;
    }

    public class PhieuBaoCaoPhanUngCoHaiADRFilterParser
    {
        public static PhieuBaoCaoPhanUngCoHaiADRFilterCls CreateInstance()
        {
            PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter = new PhieuBaoCaoPhanUngCoHaiADRFilterCls();
            return OPhieuBaoCaoPhanUngCoHaiADRFilter;
        }


        public static PhieuBaoCaoPhanUngCoHaiADRFilterCls ParseFromDataRow(DataRow dr)
        {
            PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter = new PhieuBaoCaoPhanUngCoHaiADRFilterCls();
            return OPhieuBaoCaoPhanUngCoHaiADRFilter;
        }


        public static PhieuBaoCaoPhanUngCoHaiADRFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
        {
            DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
            if (ds.Tables[0].Rows.Count == 0) return null;
            PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
            return OPhieuBaoCaoPhanUngCoHaiADRFilter;
        }



        public static XmlCls GetXml(PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter)
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
