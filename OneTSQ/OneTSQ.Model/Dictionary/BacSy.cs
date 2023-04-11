using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class BacSyCls
    {
        public string ID;
        public string MA = "";
        public string HO = "";
        public string TEN = "";
        public string HOTEN = "";
        public string TENTHUONGGOI = "";
        public string BIDANH = "";
        public int? GIOITINH;
        public DateTime? NGAYSINH;
        public string DIENTHOAI = "";
        public string EMAIL = "";
        public string DIACHISONHA = "";
        public string DIACHIHANHCHINHMA = "";
        public string CMND = "";
        public DateTime? NGAYCAPCMND;
        public string NOICAPCMND = "";
        public string DANTOCMA = "";
        public string QUOCTICHMA = "";
        public string CHUYENMONMA = "";
        public string CAPBACMA = "";
        public string CHUYENNGANHMA = "";
        public string TRINHDOMA = "";
        public string CHUCDANHMA = "";
        public string CHUYENKHOAMA = "";
        public string DONVIMA = "";
        public string QUATRINHCONGTAC = "";
        public string NDANH = "";
        public string EXTANH = "";
    }
}

public class BacSyParser
{
    public static BacSyCls CreateInstance()
    {
        BacSyCls OBacSy = new BacSyCls();
        return OBacSy;
    }


    public static BacSyCls ParseFromDataRow(DataRow dr)
    {
        BacSyCls OBacSy = new BacSyCls();
        OBacSy.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OBacSy.MA = CoreXmlUtility.GetString(dr, "MA", true);
        OBacSy.HO = CoreXmlUtility.GetString(dr, "HO", true);
        OBacSy.TEN = CoreXmlUtility.GetString(dr, "TEN", true);
        OBacSy.HOTEN = CoreXmlUtility.GetString(dr, "HOTEN", true);
        OBacSy.TENTHUONGGOI = CoreXmlUtility.GetString(dr, "TENTHUONGGOI", true);
        OBacSy.BIDANH = CoreXmlUtility.GetString(dr, "BIDANH", true);
        OBacSy.GIOITINH = dr["GIOITINH"].ToString() == "" ? null : (int?)CoreXmlUtility.GetInt(dr, "GIOITINH", true);
        OBacSy.NGAYSINH = dr["NGAYSINH"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "NGAYSINH", true);
        OBacSy.DIENTHOAI = CoreXmlUtility.GetString(dr, "DIENTHOAI", true);
        OBacSy.EMAIL = CoreXmlUtility.GetString(dr, "EMAIL", true);
        OBacSy.DIACHISONHA = CoreXmlUtility.GetString(dr, "DIACHISONHA", true);
        OBacSy.DIACHIHANHCHINHMA = CoreXmlUtility.GetString(dr, "DIACHIHANHCHINHMA", true);
        OBacSy.CMND = CoreXmlUtility.GetString(dr, "CMND", true);
        OBacSy.NGAYCAPCMND = dr["NGAYCAPCMND"].ToString() == "" ? null : (DateTime?)CoreXmlUtility.GetDate(dr, "NGAYCAPCMND", true);
        OBacSy.NOICAPCMND = CoreXmlUtility.GetString(dr, "NOICAPCMND", true);
        OBacSy.DANTOCMA = CoreXmlUtility.GetString(dr, "DANTOCMA", true);
        OBacSy.QUOCTICHMA = CoreXmlUtility.GetString(dr, "QUOCTICHMA", true);
        OBacSy.CHUYENMONMA = CoreXmlUtility.GetString(dr, "CHUYENMONMA", true);
        OBacSy.CAPBACMA = CoreXmlUtility.GetString(dr, "CAPBACMA", true);
        OBacSy.CHUYENNGANHMA = CoreXmlUtility.GetString(dr, "CHUYENNGANHMA", true);
        OBacSy.TRINHDOMA = CoreXmlUtility.GetString(dr, "TRINHDOMA", true);
        OBacSy.CHUCDANHMA = CoreXmlUtility.GetString(dr, "CHUCDANHMA", true);
        OBacSy.CHUYENKHOAMA = CoreXmlUtility.GetString(dr, "CHUYENKHOAMA", true);
        OBacSy.DONVIMA = CoreXmlUtility.GetString(dr, "DONVIMA", true);
        OBacSy.QUATRINHCONGTAC = CoreXmlUtility.GetString(dr, "QUATRINHCONGTAC", true);
        OBacSy.NDANH = CoreXmlUtility.GetString(dr, "NDANH", true);
        OBacSy.EXTANH = CoreXmlUtility.GetString(dr, "EXTANH", true);
        return OBacSy;
    }

    public static BacSyCls[] ParseFromDataTable(DataTable dtTable)
    {
        BacSyCls[] BacSys = new BacSyCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            BacSys[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return BacSys;
    }


    public static BacSyCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        BacSyCls[] BacSys = ParseFromDataTable(ds.Tables[0]);
        return BacSys;
    }


    public static BacSyCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        BacSyCls OBacSy = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBacSy;
    }


    public static XmlCls GetXml(BacSyCls[] BacSys)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("MA");
        ds.Tables["info"].Columns.Add("HO");
        ds.Tables["info"].Columns.Add("TEN");
        ds.Tables["info"].Columns.Add("HOTEN");
        ds.Tables["info"].Columns.Add("TENTHUONGGOI");
        ds.Tables["info"].Columns.Add("BIDANH");
        ds.Tables["info"].Columns.Add("GIOITINH", typeof(int?));
        ds.Tables["info"].Columns.Add("NGAYSINH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIENTHOAI");
        ds.Tables["info"].Columns.Add("EMAIL");
        ds.Tables["info"].Columns.Add("DIACHISONHA");
        ds.Tables["info"].Columns.Add("DIACHIHANHCHINHMA");
        ds.Tables["info"].Columns.Add("CMND");
        ds.Tables["info"].Columns.Add("NGAYCAPCMND", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NOICAPCMND");
        ds.Tables["info"].Columns.Add("DANTOCMA");
        ds.Tables["info"].Columns.Add("QUOCTICHMA");
        ds.Tables["info"].Columns.Add("CHUYENMONMA");
        ds.Tables["info"].Columns.Add("CAPBACMA");
        ds.Tables["info"].Columns.Add("CHUYENNGANHMA");
        ds.Tables["info"].Columns.Add("TRINHDOMA");
        ds.Tables["info"].Columns.Add("CHUCDANHMA");
        ds.Tables["info"].Columns.Add("CHUYENKHOAMA");
        ds.Tables["info"].Columns.Add("DONVIMA");
        ds.Tables["info"].Columns.Add("QUATRINHCONGTAC");
        ds.Tables["info"].Columns.Add("NDANH");
        ds.Tables["info"].Columns.Add("EXTANH");
        for (int iIndex = 0; iIndex < BacSys.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                BacSys[iIndex].ID,
                BacSys[iIndex].MA,
                BacSys[iIndex].HO,
                BacSys[iIndex].TEN,
                BacSys[iIndex].HOTEN,
                BacSys[iIndex].TENTHUONGGOI,
                BacSys[iIndex].BIDANH,
                BacSys[iIndex].GIOITINH,
                BacSys[iIndex].NGAYSINH,
                BacSys[iIndex].DIENTHOAI,
                BacSys[iIndex].EMAIL,
                BacSys[iIndex].DIACHISONHA,
                BacSys[iIndex].DIACHIHANHCHINHMA,
                BacSys[iIndex].CMND,
                BacSys[iIndex].NGAYCAPCMND,
                BacSys[iIndex].NOICAPCMND,
                BacSys[iIndex].DANTOCMA,
                BacSys[iIndex].QUOCTICHMA,
                BacSys[iIndex].CHUYENMONMA,
                BacSys[iIndex].CAPBACMA,
                BacSys[iIndex].CHUYENNGANHMA,
                BacSys[iIndex].TRINHDOMA,
                BacSys[iIndex].CHUCDANHMA,
                BacSys[iIndex].CHUYENKHOAMA,
                BacSys[iIndex].DONVIMA,
                BacSys[iIndex].QUATRINHCONGTAC,
                BacSys[iIndex].NDANH,
                BacSys[iIndex].EXTANH
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(BacSyCls OBacSy)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("MA");
        ds.Tables["info"].Columns.Add("HO");
        ds.Tables["info"].Columns.Add("TEN");
        ds.Tables["info"].Columns.Add("HOTEN");
        ds.Tables["info"].Columns.Add("TENTHUONGGOI");
        ds.Tables["info"].Columns.Add("BIDANH");
        ds.Tables["info"].Columns.Add("GIOITINH", typeof(int?));
        ds.Tables["info"].Columns.Add("NGAYSINH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIENTHOAI");
        ds.Tables["info"].Columns.Add("EMAIL");
        ds.Tables["info"].Columns.Add("DIACHISONHA");
        ds.Tables["info"].Columns.Add("DIACHIHANHCHINHMA");
        ds.Tables["info"].Columns.Add("CMND");
        ds.Tables["info"].Columns.Add("NGAYCAPCMND", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NOICAPCMND");
        ds.Tables["info"].Columns.Add("DANTOCMA");
        ds.Tables["info"].Columns.Add("QUOCTICHMA");
        ds.Tables["info"].Columns.Add("CHUYENMONMA");
        ds.Tables["info"].Columns.Add("CAPBACMA");
        ds.Tables["info"].Columns.Add("CHUYENNGANHMA");
        ds.Tables["info"].Columns.Add("TRINHDOMA");
        ds.Tables["info"].Columns.Add("CHUCDANHMA");
        ds.Tables["info"].Columns.Add("CHUYENKHOAMA");
        ds.Tables["info"].Columns.Add("DONVIMA");
        ds.Tables["info"].Columns.Add("QUATRINHCONGTAC");
        ds.Tables["info"].Columns.Add("NDANH");
        ds.Tables["info"].Columns.Add("EXTANH");
        ds.Tables["info"].Rows.Add(new object[]
        {
                OBacSy.ID,
                OBacSy.MA,
                OBacSy.HO,
                OBacSy.TEN,
                OBacSy.HOTEN,
                OBacSy.TENTHUONGGOI,
                OBacSy.BIDANH,
                OBacSy.GIOITINH,
                OBacSy.NGAYSINH,
                OBacSy.DIENTHOAI,
                OBacSy.EMAIL,
                OBacSy.DIACHISONHA,
                OBacSy.DIACHIHANHCHINHMA,
                OBacSy.CMND,
                OBacSy.NGAYCAPCMND,
                OBacSy.NOICAPCMND,
                OBacSy.DANTOCMA,
                OBacSy.QUOCTICHMA,
                OBacSy.CHUYENMONMA,
                OBacSy.CAPBACMA,
                OBacSy.CHUYENNGANHMA,
                OBacSy.TRINHDOMA,
                OBacSy.CHUCDANHMA,
                OBacSy.CHUYENKHOAMA,
                OBacSy.DONVIMA,
                OBacSy.QUATRINHCONGTAC,
                OBacSy.NDANH,
                OBacSy.EXTANH
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
