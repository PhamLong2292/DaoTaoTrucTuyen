using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneMES3.SYS.Core.Model;

namespace OneTSQ.Model
{
    public class DT_KetQuaChuyenGiaoCls
    {
        public string ID;
        public string SOQD = "";
        public DateTime? NGAYQD;
        public DateTime? TUNGAY;
        public DateTime? DENNGAY;
        public int? SOLUOTBENHNHAN;
        public int? SOCAHUONGDAN;
        public int? SOCAHOTRO;
        public int? SOGIOTHAMGIA;
        public string CHAPHANHTHOIGIAN = "";
        public string CHAPHANHQUYCHE = "";
        public string PHOIHOP = "";
        public int? HTNHIEMVU;
        public string DEXUATTHOIGIAN = "";
        public string DEXUATCHEDO = "";
        public string DEXUATDIEUKIEN = "";
        public DateTime? THOIGIANBAOCAO;
        public string NXTINHTHANTHAIDOYTHUC = "";
        public int? NXKHANANGTHUCHIENDOCLAP;
        public int? NXDUNGYCDEXUAT;
        public int? NXMUCDOHTNHIEMVU;
        public string DEXUATGIAIPHAP = "";
        public string NOINHANXET = "";
        public DateTime? NGAYNHANXET;
        public string NGUOINHANXET = "";
        public DateTime NGAYTAO;
        public string NGUOITAO_ID = "";
        public DateTime? NGAYSUA;
        public string NGUOISUA_ID = "";
        public enum eHtNhiemVu
        {
            XuatSac = 0,
            Tot = 1,
            BinhThuong = 2,
            KhongHoanThanh = 3
        }
    }
}

public class DT_KetQuaChuyenGiaoParser
{
    public static DT_KetQuaChuyenGiaoCls CreateInstance()
    {
        DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao = new DT_KetQuaChuyenGiaoCls();
        return ODT_KetQuaChuyenGiao;
    }


    public static DT_KetQuaChuyenGiaoCls ParseFromDataRow(DataRow dr)
    {
        DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao = new DT_KetQuaChuyenGiaoCls();
        ODT_KetQuaChuyenGiao.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_KetQuaChuyenGiao.SOQD = CoreXmlUtility.GetString(dr, "SOQD", true);
        ODT_KetQuaChuyenGiao.NGAYQD = CoreXmlUtility.GetDateOrNull(dr, "NGAYQD", true);
        ODT_KetQuaChuyenGiao.TUNGAY = CoreXmlUtility.GetDateOrNull(dr, "TUNGAY", true);
        ODT_KetQuaChuyenGiao.DENNGAY = CoreXmlUtility.GetDateOrNull(dr, "DENNGAY", true);
        ODT_KetQuaChuyenGiao.SOLUOTBENHNHAN = CoreXmlUtility.GetIntOrNull(dr, "SOLUOTBENHNHAN", true);
        ODT_KetQuaChuyenGiao.SOCAHUONGDAN = CoreXmlUtility.GetIntOrNull(dr, "SOCAHUONGDAN", true);
        ODT_KetQuaChuyenGiao.SOCAHOTRO = CoreXmlUtility.GetIntOrNull(dr, "SOCAHOTRO", true);
        ODT_KetQuaChuyenGiao.SOGIOTHAMGIA = CoreXmlUtility.GetIntOrNull(dr, "SOGIOTHAMGIA", true);
        ODT_KetQuaChuyenGiao.CHAPHANHTHOIGIAN = CoreXmlUtility.GetString(dr, "CHAPHANHTHOIGIAN", true);
        ODT_KetQuaChuyenGiao.CHAPHANHQUYCHE = CoreXmlUtility.GetString(dr, "CHAPHANHQUYCHE", true);
        ODT_KetQuaChuyenGiao.PHOIHOP = CoreXmlUtility.GetString(dr, "PHOIHOP", true);
        ODT_KetQuaChuyenGiao.HTNHIEMVU = CoreXmlUtility.GetIntOrNull(dr, "HTNHIEMVU", true);
        ODT_KetQuaChuyenGiao.DEXUATTHOIGIAN = CoreXmlUtility.GetString(dr, "DEXUATTHOIGIAN", true);
        ODT_KetQuaChuyenGiao.DEXUATCHEDO = CoreXmlUtility.GetString(dr, "DEXUATCHEDO", true);
        ODT_KetQuaChuyenGiao.DEXUATDIEUKIEN = CoreXmlUtility.GetString(dr, "DEXUATDIEUKIEN", true);
        ODT_KetQuaChuyenGiao.THOIGIANBAOCAO = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANBAOCAO", true);
        ODT_KetQuaChuyenGiao.NXTINHTHANTHAIDOYTHUC = CoreXmlUtility.GetString(dr, "NXTINHTHANTHAIDOYTHUC", true);
        ODT_KetQuaChuyenGiao.NXKHANANGTHUCHIENDOCLAP = CoreXmlUtility.GetIntOrNull(dr, "NXKHANANGTHUCHIENDOCLAP", true);
        ODT_KetQuaChuyenGiao.NXDUNGYCDEXUAT = CoreXmlUtility.GetIntOrNull(dr, "NXDUNGYCDEXUAT", true);
        ODT_KetQuaChuyenGiao.NXMUCDOHTNHIEMVU = CoreXmlUtility.GetIntOrNull(dr, "NXMUCDOHTNHIEMVU", true);
        ODT_KetQuaChuyenGiao.DEXUATGIAIPHAP = CoreXmlUtility.GetString(dr, "DEXUATGIAIPHAP", true);
        ODT_KetQuaChuyenGiao.NOINHANXET = CoreXmlUtility.GetString(dr, "NOINHANXET", true);
        ODT_KetQuaChuyenGiao.NGAYNHANXET = CoreXmlUtility.GetDateOrNull(dr, "NGAYNHANXET", true);
        ODT_KetQuaChuyenGiao.NGUOINHANXET = CoreXmlUtility.GetString(dr, "NGUOINHANXET", true);
        ODT_KetQuaChuyenGiao.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        ODT_KetQuaChuyenGiao.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_KetQuaChuyenGiao.NGAYSUA = CoreXmlUtility.GetDateOrNull(dr, "NGAYSUA", true);
        ODT_KetQuaChuyenGiao.NGUOISUA_ID = CoreXmlUtility.GetString(dr, "NGUOISUA_ID", true);
        return ODT_KetQuaChuyenGiao;
    }

    public static DT_KetQuaChuyenGiaoCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_KetQuaChuyenGiaoCls[] DT_KetQuaChuyenGiaos = new DT_KetQuaChuyenGiaoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_KetQuaChuyenGiaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_KetQuaChuyenGiaos;
    }


    public static DT_KetQuaChuyenGiaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_KetQuaChuyenGiaoCls[] DT_KetQuaChuyenGiaos = ParseFromDataTable(ds.Tables[0]);
        return DT_KetQuaChuyenGiaos;
    }


    public static DT_KetQuaChuyenGiaoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_KetQuaChuyenGiao;
    }


    public static XmlCls GetXml(DT_KetQuaChuyenGiaoCls[] DT_KetQuaChuyenGiaos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("SOQD");
        ds.Tables["info"].Columns.Add("NGAYQD", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("TUNGAY", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DENNGAY", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("SOLUOTBENHNHAN", typeof(int?));
        ds.Tables["info"].Columns.Add("SOCAHUONGDAN", typeof(int?));
        ds.Tables["info"].Columns.Add("SOCAHOTRO", typeof(int?));
        ds.Tables["info"].Columns.Add("SOGIOTHAMGIA", typeof(int?));
        ds.Tables["info"].Columns.Add("CHAPHANHTHOIGIAN");
        ds.Tables["info"].Columns.Add("CHAPHANHQUYCHE");
        ds.Tables["info"].Columns.Add("PHOIHOP");
        ds.Tables["info"].Columns.Add("HTNHIEMVU", typeof(int?));
        ds.Tables["info"].Columns.Add("DEXUATTHOIGIAN");
        ds.Tables["info"].Columns.Add("DEXUATCHEDO");
        ds.Tables["info"].Columns.Add("DEXUATDIEUKIEN");
        ds.Tables["info"].Columns.Add("THOIGIANBAOCAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NXTINHTHANTHAIDOYTHUC");
        ds.Tables["info"].Columns.Add("NXKHANANGTHUCHIENDOCLAP", typeof(int?));
        ds.Tables["info"].Columns.Add("NXDUNGYCDEXUAT", typeof(int?));
        ds.Tables["info"].Columns.Add("NXMUCDOHTNHIEMVU", typeof(int?));
        ds.Tables["info"].Columns.Add("DEXUATGIAIPHAP");
        ds.Tables["info"].Columns.Add("NOINHANXET");
        ds.Tables["info"].Columns.Add("NGAYNHANXET", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGUOINHANXET");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        for (int iIndex = 0; iIndex < DT_KetQuaChuyenGiaos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_KetQuaChuyenGiaos[iIndex].ID,
                DT_KetQuaChuyenGiaos[iIndex].SOQD,
                DT_KetQuaChuyenGiaos[iIndex].NGAYQD,
                DT_KetQuaChuyenGiaos[iIndex].TUNGAY,
                DT_KetQuaChuyenGiaos[iIndex].DENNGAY,
                DT_KetQuaChuyenGiaos[iIndex].SOLUOTBENHNHAN,
                DT_KetQuaChuyenGiaos[iIndex].SOCAHUONGDAN,
                DT_KetQuaChuyenGiaos[iIndex].SOCAHOTRO,
                DT_KetQuaChuyenGiaos[iIndex].SOGIOTHAMGIA,
                DT_KetQuaChuyenGiaos[iIndex].CHAPHANHTHOIGIAN,
                DT_KetQuaChuyenGiaos[iIndex].CHAPHANHQUYCHE,
                DT_KetQuaChuyenGiaos[iIndex].PHOIHOP,
                DT_KetQuaChuyenGiaos[iIndex].HTNHIEMVU,
                DT_KetQuaChuyenGiaos[iIndex].DEXUATTHOIGIAN,
                DT_KetQuaChuyenGiaos[iIndex].DEXUATCHEDO,
                DT_KetQuaChuyenGiaos[iIndex].DEXUATDIEUKIEN,
                DT_KetQuaChuyenGiaos[iIndex].THOIGIANBAOCAO,
                DT_KetQuaChuyenGiaos[iIndex].NXTINHTHANTHAIDOYTHUC,
                DT_KetQuaChuyenGiaos[iIndex].NXKHANANGTHUCHIENDOCLAP,
                DT_KetQuaChuyenGiaos[iIndex].NXDUNGYCDEXUAT,
                DT_KetQuaChuyenGiaos[iIndex].NXMUCDOHTNHIEMVU,
                DT_KetQuaChuyenGiaos[iIndex].DEXUATGIAIPHAP,
                DT_KetQuaChuyenGiaos[iIndex].NOINHANXET,
                DT_KetQuaChuyenGiaos[iIndex].NGAYNHANXET,
                DT_KetQuaChuyenGiaos[iIndex].NGUOINHANXET,
                DT_KetQuaChuyenGiaos[iIndex].NGAYTAO,
                DT_KetQuaChuyenGiaos[iIndex].NGUOITAO_ID,
                DT_KetQuaChuyenGiaos[iIndex].NGAYSUA,
                DT_KetQuaChuyenGiaos[iIndex].NGUOISUA_ID
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("SOQD");
        ds.Tables["info"].Columns.Add("NGAYQD", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("TUNGAY", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DENNGAY", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("SOLUOTBENHNHAN", typeof(int?));
        ds.Tables["info"].Columns.Add("SOCAHUONGDAN", typeof(int?));
        ds.Tables["info"].Columns.Add("SOCAHOTRO", typeof(int?));
        ds.Tables["info"].Columns.Add("SOGIOTHAMGIA", typeof(int?));
        ds.Tables["info"].Columns.Add("CHAPHANHTHOIGIAN");
        ds.Tables["info"].Columns.Add("CHAPHANHQUYCHE");
        ds.Tables["info"].Columns.Add("PHOIHOP");
        ds.Tables["info"].Columns.Add("HTNHIEMVU", typeof(int?));
        ds.Tables["info"].Columns.Add("DEXUATTHOIGIAN");
        ds.Tables["info"].Columns.Add("DEXUATCHEDO");
        ds.Tables["info"].Columns.Add("DEXUATDIEUKIEN");
        ds.Tables["info"].Columns.Add("THOIGIANBAOCAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NXTINHTHANTHAIDOYTHUC");
        ds.Tables["info"].Columns.Add("NXKHANANGTHUCHIENDOCLAP", typeof(int?));
        ds.Tables["info"].Columns.Add("NXDUNGYCDEXUAT", typeof(int?));
        ds.Tables["info"].Columns.Add("NXMUCDOHTNHIEMVU", typeof(int?));
        ds.Tables["info"].Columns.Add("DEXUATGIAIPHAP");
        ds.Tables["info"].Columns.Add("NOINHANXET");
        ds.Tables["info"].Columns.Add("NGAYNHANXET", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGUOINHANXET");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_KetQuaChuyenGiao.ID,
                ODT_KetQuaChuyenGiao.SOQD,
                ODT_KetQuaChuyenGiao.NGAYQD,
                ODT_KetQuaChuyenGiao.TUNGAY,
                ODT_KetQuaChuyenGiao.DENNGAY,
                ODT_KetQuaChuyenGiao.SOLUOTBENHNHAN,
                ODT_KetQuaChuyenGiao.SOCAHUONGDAN,
                ODT_KetQuaChuyenGiao.SOCAHOTRO,
                ODT_KetQuaChuyenGiao.SOGIOTHAMGIA,
                ODT_KetQuaChuyenGiao.CHAPHANHTHOIGIAN,
                ODT_KetQuaChuyenGiao.CHAPHANHQUYCHE,
                ODT_KetQuaChuyenGiao.PHOIHOP,
                ODT_KetQuaChuyenGiao.HTNHIEMVU,
                ODT_KetQuaChuyenGiao.DEXUATTHOIGIAN,
                ODT_KetQuaChuyenGiao.DEXUATCHEDO,
                ODT_KetQuaChuyenGiao.DEXUATDIEUKIEN,
                ODT_KetQuaChuyenGiao.THOIGIANBAOCAO,
                ODT_KetQuaChuyenGiao.NXTINHTHANTHAIDOYTHUC,
                ODT_KetQuaChuyenGiao.NXKHANANGTHUCHIENDOCLAP,
                ODT_KetQuaChuyenGiao.NXDUNGYCDEXUAT,
                ODT_KetQuaChuyenGiao.NXMUCDOHTNHIEMVU,
                ODT_KetQuaChuyenGiao.DEXUATGIAIPHAP,
                ODT_KetQuaChuyenGiao.NOINHANXET,
                ODT_KetQuaChuyenGiao.NGAYNHANXET,
                ODT_KetQuaChuyenGiao.NGUOINHANXET,
                ODT_KetQuaChuyenGiao.NGAYTAO,
                ODT_KetQuaChuyenGiao.NGUOITAO_ID,
                ODT_KetQuaChuyenGiao.NGAYSUA,
                ODT_KetQuaChuyenGiao.NGUOISUA_ID
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
