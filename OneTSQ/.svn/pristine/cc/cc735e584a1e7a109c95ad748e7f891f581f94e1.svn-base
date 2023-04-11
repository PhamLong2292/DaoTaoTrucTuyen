using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;

namespace OneTSQ.Model
{
    public class DT_KetQuaDaoTaoCls
    {
        public string ID;
        public string HOCVIEN_ID = "";
        public string KHOAHOCDANGKY_ID = "";
        public DateTime? NGAYDANGKY;
        public int? DATTIEUCHUAN;
        public string KHOAHOCDUYET_ID = "";
        public DateTime? NGAYDUYET;
        public int? DIEMDANHLYTHUYET;
        public int? DIEMDANHTHUCHANH;
        public int? DIEMDANHLYTHUYET_TH;
        public int? DIEMDANHTHUCHANH_TH;
        public decimal? DIEMTHILYTHUYET;
        public decimal? DIEMTHITHUCHANH;
        public string XEPLOAI;
        public string TIEUCHUAN;
        public int? NOPHOCPHI;
        public int TRANGTHAI;
        public string NGUOITAO_ID = "";
        public DateTime NGAYTAO;
        public string NGUOISUA_ID = "";
        public DateTime? NGAYSUA;
        public enum eTrangThai
        {
            Moi = 0,
            DaTongHop
        }
        public enum eTrangThaiHoc
        {
            DangHoc = 0,
            KetThuc
        }
        public enum ePermission
        {
            Xem = 0,
            Them,
            Sua,
            Xoa,
            TongHopDangKy,
            NhapKetQua
        }
        public enum eDatTieuChuan
        {
            Khong = 0,
            Co
        }
    }
}

public class DT_KetQuaDaoTaoParser
{
    public readonly static Dictionary<int, string> TrangThais = new Dictionary<int, string>()
        {
            { (int)DT_KetQuaDaoTaoCls.eTrangThai.Moi, "Mới" },
            { (int)DT_KetQuaDaoTaoCls.eTrangThai.DaTongHop, "Đã tổng hợp" }
        };
    public readonly static Dictionary<int, string> TrangThaiHocs = new Dictionary<int, string>()
        {
            { (int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.DangHoc, "Đang học" },
            { (int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.KetThuc, "Kết thúc" }
        };
    public readonly static Dictionary<int, string> sColorTrangThai = new Dictionary<int, string>()
        {
            { (int)DT_KetQuaDaoTaoCls.eTrangThai.Moi, "<span style=\"background-color:orange;color:white;\" class=\"badge\" >Mới</span>" },
            { (int)DT_KetQuaDaoTaoCls.eTrangThai.DaTongHop, "<span style=\"background-color:#71e038cf; color:black;\" class=\"badge\">Đã tổng hợp</span>" },
        };
    public readonly static Dictionary<int, string> sColorTrangThaiHoc = new Dictionary<int, string>()
        {
            { (int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.DangHoc, "<span style=\"background-color:orange;color:white;\" class=\"badge\" >Đang học</span>" },
            { (int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.KetThuc, "<span style=\"background-color:#71e038cf; color:black;\" class=\"badge\">Kết thúc</span>" },
        };
    public readonly static Dictionary<int, string> DatTieuChuans = new Dictionary<int, string>()
        {
            { (int)DT_KetQuaDaoTaoCls.eDatTieuChuan.Co, "Có" },
            { (int)DT_KetQuaDaoTaoCls.eDatTieuChuan.Khong, "Không" }
        };
    public static DT_KetQuaDaoTaoCls CreateInstance()
    {
        DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao = new DT_KetQuaDaoTaoCls();
        return ODT_KetQuaDaoTao;
    }


    public static DT_KetQuaDaoTaoCls ParseFromDataRow(DataRow dr)
    {
        DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao = new DT_KetQuaDaoTaoCls();
        ODT_KetQuaDaoTao.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_KetQuaDaoTao.HOCVIEN_ID = CoreXmlUtility.GetString(dr, "HOCVIEN_ID", true);
        ODT_KetQuaDaoTao.KHOAHOCDANGKY_ID = CoreXmlUtility.GetString(dr, "KHOAHOCDANGKY_ID", true);
        ODT_KetQuaDaoTao.NGAYDANGKY = CoreXmlUtility.GetDateOrNull(dr, "NGAYDANGKY", true);
        ODT_KetQuaDaoTao.DATTIEUCHUAN = CoreXmlUtility.GetIntOrNull(dr, "DATTIEUCHUAN", true);
        ODT_KetQuaDaoTao.KHOAHOCDUYET_ID = CoreXmlUtility.GetString(dr, "KHOAHOCDUYET_ID", true);
        ODT_KetQuaDaoTao.NGAYDUYET = CoreXmlUtility.GetDateOrNull(dr, "NGAYDUYET", true);
        ODT_KetQuaDaoTao.DIEMDANHLYTHUYET = CoreXmlUtility.GetIntOrNull(dr, "DIEMDANHLYTHUYET", true);
        ODT_KetQuaDaoTao.DIEMDANHTHUCHANH = CoreXmlUtility.GetIntOrNull(dr, "DIEMDANHTHUCHANH", true);
        ODT_KetQuaDaoTao.DIEMDANHLYTHUYET_TH = CoreXmlUtility.GetIntOrNull(dr, "DIEMDANHLYTHUYET_TH", true);
        ODT_KetQuaDaoTao.DIEMDANHTHUCHANH_TH = CoreXmlUtility.GetIntOrNull(dr, "DIEMDANHTHUCHANH_TH", true);
        ODT_KetQuaDaoTao.DIEMTHILYTHUYET = CoreXmlUtility.GetDecimalOrNull(dr, "DIEMTHILYTHUYET", true);
        ODT_KetQuaDaoTao.DIEMTHITHUCHANH = CoreXmlUtility.GetDecimalOrNull(dr, "DIEMTHITHUCHANH", true);
        ODT_KetQuaDaoTao.XEPLOAI = CoreXmlUtility.GetString(dr, "XEPLOAI", true);
        ODT_KetQuaDaoTao.TIEUCHUAN = CoreXmlUtility.GetString(dr, "TIEUCHUAN", true);
        ODT_KetQuaDaoTao.NOPHOCPHI = CoreXmlUtility.GetIntOrNull(dr, "NOPHOCPHI", true);
        ODT_KetQuaDaoTao.TRANGTHAI = CoreXmlUtility.GetInt(dr, "TRANGTHAI", true);
        ODT_KetQuaDaoTao.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_KetQuaDaoTao.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        ODT_KetQuaDaoTao.NGUOISUA_ID = CoreXmlUtility.GetString(dr, "NGUOISUA_ID", true);
        ODT_KetQuaDaoTao.NGAYSUA = CoreXmlUtility.GetDateOrNull(dr, "NGAYSUA", true);
        return ODT_KetQuaDaoTao;
    }

    public static DT_KetQuaDaoTaoCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_KetQuaDaoTaoCls[] DT_KetQuaDaoTaos = new DT_KetQuaDaoTaoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_KetQuaDaoTaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_KetQuaDaoTaos;
    }


    public static DT_KetQuaDaoTaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_KetQuaDaoTaoCls[] DT_KetQuaDaoTaos = ParseFromDataTable(ds.Tables[0]);
        return DT_KetQuaDaoTaos;
    }


    public static DT_KetQuaDaoTaoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_KetQuaDaoTao;
    }


    public static XmlCls GetXml(DT_KetQuaDaoTaoCls[] DT_KetQuaDaoTaos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("HOCVIEN_ID");
        ds.Tables["info"].Columns.Add("KHOAHOCDANGKY_ID");
        ds.Tables["info"].Columns.Add("NGAYDANGKY", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DATTIEUCHUAN", typeof(int?));
        ds.Tables["info"].Columns.Add("KHOAHOCDUYET_ID");
        ds.Tables["info"].Columns.Add("NGAYDUYET", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIEMDANHLYTHUYET", typeof(int?));
        ds.Tables["info"].Columns.Add("DIEMDANHTHUCHANH", typeof(int?));
        ds.Tables["info"].Columns.Add("DIEMDANHLYTHUYET_TH", typeof(int?));
        ds.Tables["info"].Columns.Add("DIEMDANHTHUCHANH_TH", typeof(int?));
        ds.Tables["info"].Columns.Add("DIEMTHILYTHUYET", typeof(decimal?));
        ds.Tables["info"].Columns.Add("DIEMTHITHUCHANH", typeof(decimal?));
        ds.Tables["info"].Columns.Add("XEPLOAI");
        ds.Tables["info"].Columns.Add("TIEUCHUAN");
        ds.Tables["info"].Columns.Add("NOPHOCPHI", typeof(int?));
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        for (int iIndex = 0; iIndex < DT_KetQuaDaoTaos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_KetQuaDaoTaos[iIndex].ID,
                DT_KetQuaDaoTaos[iIndex].HOCVIEN_ID,
                DT_KetQuaDaoTaos[iIndex].KHOAHOCDANGKY_ID,
                DT_KetQuaDaoTaos[iIndex].DATTIEUCHUAN,
                DT_KetQuaDaoTaos[iIndex].NGAYDANGKY,
                DT_KetQuaDaoTaos[iIndex].KHOAHOCDUYET_ID,
                DT_KetQuaDaoTaos[iIndex].NGAYDUYET,
                DT_KetQuaDaoTaos[iIndex].DIEMDANHLYTHUYET,
                DT_KetQuaDaoTaos[iIndex].DIEMDANHTHUCHANH,
                DT_KetQuaDaoTaos[iIndex].DIEMDANHLYTHUYET_TH,
                DT_KetQuaDaoTaos[iIndex].DIEMDANHTHUCHANH_TH,
                DT_KetQuaDaoTaos[iIndex].DIEMTHILYTHUYET,
                DT_KetQuaDaoTaos[iIndex].DIEMTHITHUCHANH,
                DT_KetQuaDaoTaos[iIndex].XEPLOAI,
                DT_KetQuaDaoTaos[iIndex].TIEUCHUAN,
                DT_KetQuaDaoTaos[iIndex].NOPHOCPHI,
                DT_KetQuaDaoTaos[iIndex].TRANGTHAI,
                DT_KetQuaDaoTaos[iIndex].NGUOITAO_ID,
                DT_KetQuaDaoTaos[iIndex].NGAYTAO,
                DT_KetQuaDaoTaos[iIndex].NGUOISUA_ID,
                DT_KetQuaDaoTaos[iIndex].NGAYSUA
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("HOCVIEN_ID");
        ds.Tables["info"].Columns.Add("KHOAHOCDANGKY_ID");
        ds.Tables["info"].Columns.Add("NGAYDANGKY", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DATTIEUCHUAN", typeof(int?));
        ds.Tables["info"].Columns.Add("KHOAHOCDUYET_ID");
        ds.Tables["info"].Columns.Add("NGAYDUYET", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DIEMDANHLYTHUYET", typeof(int?));
        ds.Tables["info"].Columns.Add("DIEMDANHTHUCHANH", typeof(int?));
        ds.Tables["info"].Columns.Add("DIEMDANHLYTHUYET_TH", typeof(int?));
        ds.Tables["info"].Columns.Add("DIEMDANHTHUCHANH_TH", typeof(int?));
        ds.Tables["info"].Columns.Add("DIEMTHILYTHUYET", typeof(decimal?));
        ds.Tables["info"].Columns.Add("DIEMTHITHUCHANH", typeof(decimal?));
        ds.Tables["info"].Columns.Add("XEPLOAI");
        ds.Tables["info"].Columns.Add("TIEUCHUAN");
        ds.Tables["info"].Columns.Add("NOPHOCPHI", typeof(int?));
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_KetQuaDaoTao.ID,
                ODT_KetQuaDaoTao.HOCVIEN_ID,
                ODT_KetQuaDaoTao.KHOAHOCDANGKY_ID,
                ODT_KetQuaDaoTao.DATTIEUCHUAN,
                ODT_KetQuaDaoTao.NGAYDANGKY,
                ODT_KetQuaDaoTao.KHOAHOCDUYET_ID,
                ODT_KetQuaDaoTao.NGAYDUYET,
                ODT_KetQuaDaoTao.DIEMDANHLYTHUYET,
                ODT_KetQuaDaoTao.DIEMDANHTHUCHANH,
                ODT_KetQuaDaoTao.DIEMDANHLYTHUYET_TH,
                ODT_KetQuaDaoTao.DIEMDANHTHUCHANH_TH,
                ODT_KetQuaDaoTao.DIEMTHILYTHUYET,
                ODT_KetQuaDaoTao.DIEMTHITHUCHANH,
                ODT_KetQuaDaoTao.XEPLOAI,
                ODT_KetQuaDaoTao.TIEUCHUAN,
                ODT_KetQuaDaoTao.NOPHOCPHI,
                ODT_KetQuaDaoTao.TRANGTHAI,
                ODT_KetQuaDaoTao.NGUOITAO_ID,
                ODT_KetQuaDaoTao.NGAYTAO,
                ODT_KetQuaDaoTao.NGUOISUA_ID,
                ODT_KetQuaDaoTao.NGAYSUA
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
