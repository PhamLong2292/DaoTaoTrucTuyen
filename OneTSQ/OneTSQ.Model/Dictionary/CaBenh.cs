using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using System.Web;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class CaBenhCls
    {
        public string ID;
        public string KHAMCHUABENHID;
        public string LYDOVAOVIEN = "";
        public string CHUYENKHOAMA = "";
        public string BACSY = "";
        public string LYDOYEUCAU = "";
        public string DONVITHAMVANID = "";
        public string DONVITUVANID = "";
        public string BENHNHANID = "";
        public string TIENSUKHAMBENHID = "";
        public string CHANDOANBANDAUMA = "";
        public string CHANDOANBANDAU = "";
        public string TAOBOI = "";
        public DateTime TAOVAO;
        public DateTime? DENGHIVAO;
        public DateTime? DUYETDENGHIVAO;
        public DateTime? NGAYHOICHANDENGHI;
        public DateTime? BATDAUHOICHANVAO;
        public DateTime? KETTHUCHOICHANVAO;
        public string MABN = "";
        public string HOBN = "";
        public string TENBN = "";
        public string HOTENBN = "";
        public DateTime? NGAYSINH;
        public int? HIENTHINGAYSINH;
        public int? GIOITINH;
        public string DIACHI = "";
        public string MOTA = "";
        public string TIENSUBENH = "";
        public int? TRANGTHAI;
        public int LAPLICHDAXEM;
        public int LOAIHOICHAN;

        public DateTime? NGAYNHAPVIEN;
        public DateTime? NGAYXUATVIEN;
        public string NHAPVIENTAI;
        public string BENHSU;
        public string TOANTHAN;
        public string BOPHAN;
        public string CHANDOANSOBO;
        public string CANLAMSANG;
        public string NXKETQUAXN;
        public string KETQUACDHA;
        public string CHANDOANXACDINH;
        public string PHAUTHUAT;
        public string THUTHUAT;
        public string CAUHOI;
        public string THONGTINDIEUTRI;
        public string GIUONG;
        public string PHONG;
        public string NGHENGHIEPMA;
        public string DANTOCMA;
        public string LICHHOICHANID;
        public string DUYETHOICHANBOI;
        public DateTime? DUYETHOICHANVAO;
        public string VITRITAINAN;
        public float? LNG;
        public float? LAT;
        public int? TRINHBAY;
        public int? CAPCUU;

        //Các trường lưu giá trị mã hóa để hiển thị dạng html
        public string LYDOVAOVIEN_ENCODED = "";
        public string LYDOYEUCAU_ENCODED = "";
        public string CHANDOANBANDAU_ENCODED = "";
        public string MABN_ENCODED = "";
        public string HOTENBN_ENCODED = "";
        public string DIACHI_ENCODED = "";
        public string MOTA_ENCODED = "";
        public string TIENSUBENH_ENCODED = "";
        public string NHAPVIENTAI_ENCODED;
        public string BENHSU_ENCODED;
        public string TOANTHAN_ENCODED;
        public string BOPHAN_ENCODED;
        public string CHANDOANSOBO_ENCODED;
        public string CANLAMSANG_ENCODED;
        public string NXKETQUAXN_ENCODED;
        public string KETQUACDHA_ENCODED;
        public string CHANDOANXACDINH_ENCODED;
        public string PHAUTHUAT_ENCODED;
        public string THUTHUAT_ENCODED;
        public string CAUHOI_ENCODED;
        public string THONGTINDIEUTRI_ENCODED;
        public string GIUONG_ENCODED;
        public string PHONG_ENCODED;
        /*
        string myString = "<img src=\"x\" onerror=\"alert(1)\">";
        // Encode the string.
        string myEncodedString = HttpUtility.HtmlEncode(myString);
        StringWriter myWriter = new StringWriter();
        // Decode the encoded string.
        HttpUtility.HtmlDecode(myEncodedString, myWriter);
            string myDecodedString = myWriter.ToString();
        */
        public enum eTrangThai
        {
            Moi = 0,
            ChoDuyetDeNghi = 1,//chờ duyệt đề nghị hội chẩn ở bệnh viện tham vấn.
            TuChoiDeNghi = 2,
            ChoTiepNhan = 3,
            TuChoiTiepNhan = 4,
            ChoLapLich = 5,
            DaLapLich = 6,
            ChoHoiChan = 7,
            DangHoiChan = 8,
            KetThuc = 9
        }
        public enum eTacVu
        {
            BatDau = eTrangThai.DangHoiChan,
            KetThuc = eTrangThai.KetThuc
        }
        public enum eLoaiHoichan
        {
            TuVanKCB = 0,
            CanLamSang = 1,
            PhauThuat = 2
        }
    }
}

public class CaBenhParser
{
    public static CaBenhCls CreateInstance()
    {
        CaBenhCls OCaBenh = new CaBenhCls();
        return OCaBenh;
    }

    public readonly static Dictionary<int, string> TrangThais = new Dictionary<int, string>()
        {
            { (int)CaBenhCls.eTrangThai.Moi, "Mới" },
            { (int)CaBenhCls.eTrangThai.ChoDuyetDeNghi, "Chờ duyệt đề nghị" },
            { (int)CaBenhCls.eTrangThai.TuChoiDeNghi, "Từ chối đề nghị" },
            { (int)CaBenhCls.eTrangThai.ChoTiepNhan, "Chờ tiếp nhận" },
            { (int)CaBenhCls.eTrangThai.TuChoiTiepNhan, "Từ chối tiếp nhận" },
            { (int)CaBenhCls.eTrangThai.ChoLapLich, "Chờ lập lịch" },
            { (int)CaBenhCls.eTrangThai.DaLapLich, "Đã lập lịch" },
            { (int)CaBenhCls.eTrangThai.ChoHoiChan, "Chờ hội chẩn" },
            { (int)CaBenhCls.eTrangThai.DangHoiChan, "Đang hội chẩn" },
            { (int)CaBenhCls.eTrangThai.KetThuc, "kết thúc" }
        };
    public readonly static Dictionary<int, string> sColorTrangThai = new Dictionary<int, string>()
        {
            { (int)CaBenhCls.eTrangThai.Moi, "<span style=\"background-color:orange;color:white;\" class=\"badge\" >Mới</span>" },
            { (int)CaBenhCls.eTrangThai.ChoDuyetDeNghi, "<span style=\"background-color:#71e038cf; color:black;\" class=\"badge\">Chờ duyệt đề nghị</span>" },
            { (int)CaBenhCls.eTrangThai.TuChoiDeNghi, "<span style=\"background-color:rgb(188, 189, 34);color:white;\" class=\"badge\" >Từ chối đề nghị</span>" },
            { (int)CaBenhCls.eTrangThai.ChoTiepNhan, "<span style=\"background-color:rgb(31, 119, 180);color:white;\" class=\"badge\" >Chờ tiếp nhận</span>" },
            { (int)CaBenhCls.eTrangThai.TuChoiTiepNhan, "<span style=\"background-color:rgb(148, 103, 189);color:white;\" class=\"badge\" >Từ chối tiếp nhận</span>" },
            { (int)CaBenhCls.eTrangThai.ChoLapLich, "<span style=\"background-color:rgb(140, 86, 75);color:white;\" class=\"badge\" >Chờ lập lịch</span>" },
            { (int)CaBenhCls.eTrangThai.DaLapLich, "<span style=\"background-color:rgb(14, 86, 75);color:white;\" class=\"badge\" >Đã lập lịch</span>" },
            { (int)CaBenhCls.eTrangThai.ChoHoiChan, "<span style=\"background-color:rgb(44, 160, 44);color:white;\" class=\"badge\" >Chờ hội chẩn</span>" },
            { (int)CaBenhCls.eTrangThai.DangHoiChan, "<span style=\"background-color:#2eb8ec;color:white;\" class=\"badge\">Đang hội chẩn</span>"},
            { (int)CaBenhCls.eTrangThai.KetThuc, "<span style=\"background-color:blue;color:white;\" class=\"badge\" >Kết thúc</span>" },
        };
    public readonly static Dictionary<int, string> TacVus = new Dictionary<int, string>()
        {
            { (int)CaBenhCls.eTacVu.BatDau, "bắt đầu" },
            { (int)CaBenhCls.eTacVu.KetThuc, "kết thúc" }
        };
    public readonly static Dictionary<int, string> LoaiHoichans = new Dictionary<int, string>()
        {
            { (int)CaBenhCls.eLoaiHoichan.TuVanKCB, "Hội chẩn tư vấn khám chữa bệnh" },
            { (int)CaBenhCls.eLoaiHoichan.CanLamSang, "Hội chẩn cận lâm sàng" },
            { (int)CaBenhCls.eLoaiHoichan.PhauThuat, "Hội chẩn phẫu thuật" }
        };
    public static CaBenhCls ParseFromDataRow(DataRow dr)
    {
        CaBenhCls OCaBenh = new CaBenhCls();
        OCaBenh.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OCaBenh.KHAMCHUABENHID = CoreXmlUtility.GetString(dr, "KHAMCHUABENHID", true);
        OCaBenh.LYDOVAOVIEN = CoreXmlUtility.GetString(dr, "LYDOVAOVIEN", true);
        OCaBenh.CHUYENKHOAMA = CoreXmlUtility.GetString(dr, "CHUYENKHOAMA", true);
        OCaBenh.BACSY = CoreXmlUtility.GetString(dr, "BACSY", true);
        OCaBenh.LYDOYEUCAU = CoreXmlUtility.GetString(dr, "LYDOYEUCAU", true);
        OCaBenh.DONVITHAMVANID = CoreXmlUtility.GetString(dr, "DONVITHAMVANID", true);
        OCaBenh.DONVITUVANID = CoreXmlUtility.GetString(dr, "DONVITUVANID", true);
        //OCaBenh.BENHNHANID = CoreXmlUtility.GetString(dr, "BENHNHANID", true);
        OCaBenh.TIENSUKHAMBENHID = CoreXmlUtility.GetString(dr, "TIENSUKHAMBENHID", true);
        OCaBenh.CHANDOANBANDAUMA = CoreXmlUtility.GetString(dr, "CHANDOANBANDAUMA", true);
        OCaBenh.CHANDOANBANDAU = CoreXmlUtility.GetString(dr, "CHANDOANBANDAU", true);
        OCaBenh.TAOBOI = CoreXmlUtility.GetString(dr, "TAOBOI", true);
        OCaBenh.TAOVAO = CoreXmlUtility.GetDate(dr, "TAOVAO", true);
        OCaBenh.DENGHIVAO = CoreXmlUtility.GetDateOrNull(dr, "DENGHIVAO", true);
        OCaBenh.DUYETDENGHIVAO = CoreXmlUtility.GetDateOrNull(dr, "DUYETDENGHIVAO", true);
        OCaBenh.NGAYHOICHANDENGHI = CoreXmlUtility.GetDateOrNull(dr, "NGAYHOICHANDENGHI", true);
        OCaBenh.BATDAUHOICHANVAO = CoreXmlUtility.GetDateOrNull(dr, "BATDAUHOICHANVAO", true);
        OCaBenh.KETTHUCHOICHANVAO = CoreXmlUtility.GetDateOrNull(dr, "KETTHUCHOICHANVAO", true);
        OCaBenh.MABN = CoreXmlUtility.GetString(dr, "MABN", true);
        OCaBenh.TENBN = CoreXmlUtility.GetString(dr, "TENBN", true);
        OCaBenh.HOBN = CoreXmlUtility.GetString(dr, "HOBN", true);
        OCaBenh.HOTENBN = CoreXmlUtility.GetString(dr, "HOTENBN", true);
        OCaBenh.NGAYSINH = CoreXmlUtility.GetDateOrNull(dr, "NGAYSINH", true);
        OCaBenh.HIENTHINGAYSINH = CoreXmlUtility.GetIntOrNull(dr, "HIENTHINGAYSINH", true);
        OCaBenh.GIOITINH = CoreXmlUtility.GetIntOrNull(dr, "GIOITINH", true);
        OCaBenh.DIACHI = CoreXmlUtility.GetString(dr, "DIACHI", true);
        OCaBenh.MOTA = CoreXmlUtility.GetString(dr, "MOTA", true);
        OCaBenh.TIENSUBENH = CoreXmlUtility.GetString(dr, "TIENSUBENH", true);
        OCaBenh.TRANGTHAI = CoreXmlUtility.GetIntOrNull(dr, "TRANGTHAI", true);
        OCaBenh.LAPLICHDAXEM = CoreXmlUtility.GetInt(dr, "LAPLICHDAXEM", true);
        OCaBenh.LOAIHOICHAN = CoreXmlUtility.GetInt(dr, "LOAIHOICHAN", true);

        OCaBenh.NGAYNHAPVIEN = CoreXmlUtility.GetDateOrNull(dr, "NGAYNHAPVIEN", true);
        OCaBenh.NGAYXUATVIEN = CoreXmlUtility.GetDateOrNull(dr, "NGAYXUATVIEN", true);
        OCaBenh.NHAPVIENTAI = CoreXmlUtility.GetString(dr, "NHAPVIENTAI", true);
        OCaBenh.BENHSU = CoreXmlUtility.GetString(dr, "BENHSU", true);
        OCaBenh.TOANTHAN = CoreXmlUtility.GetString(dr, "TOANTHAN", true);
        OCaBenh.BOPHAN = CoreXmlUtility.GetString(dr, "BOPHAN", true);
        OCaBenh.CHANDOANSOBO = CoreXmlUtility.GetString(dr, "CHANDOANSOBO", true);
        OCaBenh.CANLAMSANG = CoreXmlUtility.GetString(dr, "CANLAMSANG", true);
        OCaBenh.NXKETQUAXN = CoreXmlUtility.GetString(dr, "NXKETQUAXN", true);
        OCaBenh.KETQUACDHA = CoreXmlUtility.GetString(dr, "KETQUACDHA", true);
        OCaBenh.CHANDOANXACDINH = CoreXmlUtility.GetString(dr, "CHANDOANXACDINH", true);
        OCaBenh.PHAUTHUAT = CoreXmlUtility.GetString(dr, "PHAUTHUAT", true);
        OCaBenh.THUTHUAT = CoreXmlUtility.GetString(dr, "THUTHUAT", true);
        OCaBenh.CAUHOI = CoreXmlUtility.GetString(dr, "CAUHOI", true);
        OCaBenh.THONGTINDIEUTRI = CoreXmlUtility.GetString(dr, "THONGTINDIEUTRI", true);
        OCaBenh.GIUONG = CoreXmlUtility.GetString(dr, "GIUONG", true);
        OCaBenh.PHONG = CoreXmlUtility.GetString(dr, "PHONG", true);
        OCaBenh.NGHENGHIEPMA = CoreXmlUtility.GetString(dr, "NGHENGHIEPMA", true);
        OCaBenh.DANTOCMA = CoreXmlUtility.GetString(dr, "DANTOCMA", true);
        OCaBenh.LICHHOICHANID = CoreXmlUtility.GetString(dr, "LICHHOICHANID", true);
        OCaBenh.DUYETHOICHANBOI = CoreXmlUtility.GetString(dr, "DUYETHOICHANBOI", true);
        OCaBenh.DUYETHOICHANVAO = CoreXmlUtility.GetDateOrNull(dr, "DUYETHOICHANVAO", true);
        OCaBenh.VITRITAINAN = CoreXmlUtility.GetString(dr, "VITRITAINAN", true);
        //OCaBenh.LNG = dr["LNG"].ToString() == "" ? null : (float?)CoreXmlUtility.GetFloat(dr, "LNG", true);
        //OCaBenh.LAT = dr["LAT"].ToString() == "" ? null : (float?)CoreXmlUtility.GetFloat(dr, "LAT", true);
        OCaBenh.TRINHBAY = dr["TRINHBAY"].ToString() == "" ? null : (int?)CoreXmlUtility.GetInt(dr, "TRINHBAY", true);
        OCaBenh.CAPCUU = dr["CAPCUU"].ToString() == "" ? null : (int?)CoreXmlUtility.GetInt(dr, "CAPCUU", true);

        //Mã hóa dữ liệu
        OCaBenh.LYDOVAOVIEN_ENCODED = HttpUtility.HtmlEncode(OCaBenh.LYDOVAOVIEN);
        OCaBenh.LYDOYEUCAU_ENCODED = HttpUtility.HtmlEncode(OCaBenh.LYDOYEUCAU);
        OCaBenh.CHANDOANBANDAU_ENCODED = HttpUtility.HtmlEncode(OCaBenh.CHANDOANBANDAU);
        OCaBenh.MABN_ENCODED = HttpUtility.HtmlEncode(OCaBenh.MABN);
        OCaBenh.HOTENBN_ENCODED = HttpUtility.HtmlEncode(OCaBenh.HOTENBN);
        OCaBenh.MOTA_ENCODED = HttpUtility.HtmlEncode(OCaBenh.MOTA);
        OCaBenh.TIENSUBENH_ENCODED = HttpUtility.HtmlEncode(OCaBenh.TIENSUBENH);
        OCaBenh.NHAPVIENTAI_ENCODED = HttpUtility.HtmlEncode(OCaBenh.NHAPVIENTAI);
        OCaBenh.BENHSU_ENCODED = HttpUtility.HtmlEncode(OCaBenh.BENHSU);
        OCaBenh.TOANTHAN_ENCODED = HttpUtility.HtmlEncode(OCaBenh.TOANTHAN);
        OCaBenh.BOPHAN_ENCODED = HttpUtility.HtmlEncode(OCaBenh.BOPHAN);
        OCaBenh.CHANDOANSOBO_ENCODED = HttpUtility.HtmlEncode(OCaBenh.CHANDOANSOBO);
        OCaBenh.CANLAMSANG_ENCODED = HttpUtility.HtmlEncode(OCaBenh.CANLAMSANG);
        OCaBenh.NXKETQUAXN_ENCODED = HttpUtility.HtmlEncode(OCaBenh.NXKETQUAXN);
        OCaBenh.KETQUACDHA_ENCODED = HttpUtility.HtmlEncode(OCaBenh.KETQUACDHA);
        OCaBenh.CHANDOANXACDINH_ENCODED = HttpUtility.HtmlEncode(OCaBenh.CHANDOANXACDINH);
        OCaBenh.PHAUTHUAT_ENCODED = HttpUtility.HtmlEncode(OCaBenh.PHAUTHUAT);
        OCaBenh.THUTHUAT_ENCODED = HttpUtility.HtmlEncode(OCaBenh.THUTHUAT);
        OCaBenh.CAUHOI_ENCODED = HttpUtility.HtmlEncode(OCaBenh.CAUHOI);
        OCaBenh.THONGTINDIEUTRI_ENCODED = HttpUtility.HtmlEncode(OCaBenh.THONGTINDIEUTRI);
        OCaBenh.GIUONG_ENCODED = HttpUtility.HtmlEncode(OCaBenh.GIUONG);
        OCaBenh.PHONG_ENCODED = HttpUtility.HtmlEncode(OCaBenh.PHONG);
        return OCaBenh;
    }

    public static CaBenhCls[] ParseFromDataTable(DataTable dtTable)
    {
        CaBenhCls[] CaBenhs = new CaBenhCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            CaBenhs[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return CaBenhs;
    }


    public static CaBenhCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        CaBenhCls[] CaBenhs = ParseFromDataTable(ds.Tables[0]);
        return CaBenhs;
    }


    public static CaBenhCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        CaBenhCls OCaBenh = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OCaBenh;
    }


    public static XmlCls GetXml(CaBenhCls[] CaBenhs)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("KHAMCHUABENHID");
        ds.Tables["info"].Columns.Add("LYDOVAOVIEN");
        ds.Tables["info"].Columns.Add("CHUYENKHOAMA");
        ds.Tables["info"].Columns.Add("BACSY");
        ds.Tables["info"].Columns.Add("LYDOYEUCAU");
        ds.Tables["info"].Columns.Add("DONVITHAMVANID");
        ds.Tables["info"].Columns.Add("DONVITUVANID");
        ds.Tables["info"].Columns.Add("BENHNHANID");
        ds.Tables["info"].Columns.Add("TIENSUKHAMBENHID");
        ds.Tables["info"].Columns.Add("CHANDOANBANDAUMA");
        ds.Tables["info"].Columns.Add("CHANDOANBANDAU");
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("DENGHIVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DUYETDENGHIVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGAYHOICHANDENGHI", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("BATDAUHOICHANVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCHOICHANVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("MABN");
        ds.Tables["info"].Columns.Add("TENBN");
        ds.Tables["info"].Columns.Add("HOBN");
        ds.Tables["info"].Columns.Add("HOTENBN");
        ds.Tables["info"].Columns.Add("NGAYSINH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("HIENTHINGAYSINH", typeof(int?));
        ds.Tables["info"].Columns.Add("GIOITINH", typeof(int?));
        ds.Tables["info"].Columns.Add("DIACHI");
        ds.Tables["info"].Columns.Add("MOTA");
        ds.Tables["info"].Columns.Add("TIENSUBENH");
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int?));
        ds.Tables["info"].Columns.Add("LAPLICHDAXEM", typeof(int));
        ds.Tables["info"].Columns.Add("LOAIHOICHAN", typeof(int));

        ds.Tables["info"].Columns.Add("NGAYNHAPVIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGAYXUATVIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NHAPVIENTAI");
        ds.Tables["info"].Columns.Add("BENHSU");
        ds.Tables["info"].Columns.Add("TOANTHAN");
        ds.Tables["info"].Columns.Add("BOPHAN");
        ds.Tables["info"].Columns.Add("CHANDOANSOBO");
        ds.Tables["info"].Columns.Add("CANLAMSANG");
        ds.Tables["info"].Columns.Add("NXKETQUAXN");
        ds.Tables["info"].Columns.Add("KETQUACDHA");
        ds.Tables["info"].Columns.Add("CHANDOANXACDINH");
        ds.Tables["info"].Columns.Add("PHAUTHUAT");
        ds.Tables["info"].Columns.Add("THUTHUAT");
        ds.Tables["info"].Columns.Add("CAUHOI");
        ds.Tables["info"].Columns.Add("THONGTINDIEUTRI");
        ds.Tables["info"].Columns.Add("GIUONG");
        ds.Tables["info"].Columns.Add("PHONG");
        ds.Tables["info"].Columns.Add("NGHENGHIEPMA");
        ds.Tables["info"].Columns.Add("DANTOCMA");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("DUYETHOICHANBOI");
        ds.Tables["info"].Columns.Add("DUYETHOICHANVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("VITRITAINAN");
        ds.Tables["info"].Columns.Add("LNG", typeof(float?));
        ds.Tables["info"].Columns.Add("LAT", typeof(float?));
        ds.Tables["info"].Columns.Add("TRINHBAY", typeof(int?));
        ds.Tables["info"].Columns.Add("CAPCUU", typeof(int?));
        for (int iIndex = 0; iIndex < CaBenhs.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                CaBenhs[iIndex].ID,
                CaBenhs[iIndex].KHAMCHUABENHID,
                CaBenhs[iIndex].LYDOVAOVIEN,
                CaBenhs[iIndex].CHUYENKHOAMA,
                CaBenhs[iIndex].BACSY,
                CaBenhs[iIndex].LYDOYEUCAU,
                CaBenhs[iIndex].DONVITHAMVANID,
                CaBenhs[iIndex].DONVITUVANID,
                CaBenhs[iIndex].BENHNHANID,
                CaBenhs[iIndex].TIENSUKHAMBENHID,
                CaBenhs[iIndex].CHANDOANBANDAUMA,
                CaBenhs[iIndex].CHANDOANBANDAU,
                CaBenhs[iIndex].TAOBOI,
                CaBenhs[iIndex].TAOVAO,
                CaBenhs[iIndex].DENGHIVAO,
                CaBenhs[iIndex].DUYETDENGHIVAO,
                CaBenhs[iIndex].NGAYHOICHANDENGHI,
                CaBenhs[iIndex].BATDAUHOICHANVAO,
                CaBenhs[iIndex].KETTHUCHOICHANVAO,
                CaBenhs[iIndex].MABN,
                CaBenhs[iIndex].TENBN,
                CaBenhs[iIndex].HOBN,
                CaBenhs[iIndex].HOTENBN,
                CaBenhs[iIndex].NGAYSINH,
                CaBenhs[iIndex].HIENTHINGAYSINH,
                CaBenhs[iIndex].GIOITINH,
                CaBenhs[iIndex].DIACHI,
                CaBenhs[iIndex].MOTA,
                CaBenhs[iIndex].TIENSUBENH,
                CaBenhs[iIndex].TRANGTHAI,
                CaBenhs[iIndex].LAPLICHDAXEM,
                CaBenhs[iIndex].LOAIHOICHAN,

                CaBenhs[iIndex].NGAYNHAPVIEN,
                CaBenhs[iIndex].NGAYXUATVIEN,
                CaBenhs[iIndex].NHAPVIENTAI,
                CaBenhs[iIndex].BENHSU,
                CaBenhs[iIndex].TOANTHAN,
                CaBenhs[iIndex].BOPHAN,
                CaBenhs[iIndex].CHANDOANSOBO,
                CaBenhs[iIndex].CANLAMSANG,
                CaBenhs[iIndex].NXKETQUAXN,
                CaBenhs[iIndex].KETQUACDHA,
                CaBenhs[iIndex].CHANDOANXACDINH,
                CaBenhs[iIndex].PHAUTHUAT,
                CaBenhs[iIndex].THUTHUAT,
                CaBenhs[iIndex].CAUHOI,
                CaBenhs[iIndex].THONGTINDIEUTRI,
                CaBenhs[iIndex].GIUONG,
                CaBenhs[iIndex].PHONG,
                CaBenhs[iIndex].NGHENGHIEPMA,
                CaBenhs[iIndex].DANTOCMA,
                CaBenhs[iIndex].LICHHOICHANID,
                CaBenhs[iIndex].DUYETHOICHANBOI,
                CaBenhs[iIndex].DUYETHOICHANVAO,
                CaBenhs[iIndex].VITRITAINAN,
                CaBenhs[iIndex].LNG,
                CaBenhs[iIndex].LAT,
                CaBenhs[iIndex].TRINHBAY,
                CaBenhs[iIndex].CAPCUU
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(CaBenhCls OCaBenh)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("KHAMCHUABENHID");
        ds.Tables["info"].Columns.Add("LYDOVAOVIEN");
        ds.Tables["info"].Columns.Add("CHUYENKHOAMA");
        ds.Tables["info"].Columns.Add("BACSY");
        ds.Tables["info"].Columns.Add("LYDOYEUCAU");
        ds.Tables["info"].Columns.Add("DONVITHAMVANID");
        ds.Tables["info"].Columns.Add("DONVITUVANID");
        ds.Tables["info"].Columns.Add("BENHNHANID");
        ds.Tables["info"].Columns.Add("TIENSUKHAMBENHID");
        ds.Tables["info"].Columns.Add("CHANDOANBANDAUMA");
        ds.Tables["info"].Columns.Add("CHANDOANBANDAU");
        ds.Tables["info"].Columns.Add("TAOBOI");
        ds.Tables["info"].Columns.Add("TAOVAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("DENGHIVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("DUYETDENGHIVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGAYHOICHANDENGHI", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("BATDAUHOICHANVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("KETTHUCHOICHANVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("MABN");
        ds.Tables["info"].Columns.Add("TENBN");
        ds.Tables["info"].Columns.Add("HOBN");
        ds.Tables["info"].Columns.Add("HOTENBN");
        ds.Tables["info"].Columns.Add("NGAYSINH", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("HIENTHINGAYSINH", typeof(int?));
        ds.Tables["info"].Columns.Add("GIOITINH", typeof(int?));
        ds.Tables["info"].Columns.Add("DIACHI");
        ds.Tables["info"].Columns.Add("MOTA");
        ds.Tables["info"].Columns.Add("TIENSUBENH");
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int?));
        ds.Tables["info"].Columns.Add("LAPLICHDAXEM", typeof(int));
        ds.Tables["info"].Columns.Add("LOAIHOICHAN", typeof(int));

        ds.Tables["info"].Columns.Add("NGAYNHAPVIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NGAYXUATVIEN", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("NHAPVIENTAI");
        ds.Tables["info"].Columns.Add("BENHSU");
        ds.Tables["info"].Columns.Add("TOANTHAN");
        ds.Tables["info"].Columns.Add("BOPHAN");
        ds.Tables["info"].Columns.Add("CHANDOANSOBO");
        ds.Tables["info"].Columns.Add("CANLAMSANG");
        ds.Tables["info"].Columns.Add("NXKETQUAXN");
        ds.Tables["info"].Columns.Add("KETQUACDHA");
        ds.Tables["info"].Columns.Add("CHANDOANXACDINH");
        ds.Tables["info"].Columns.Add("PHAUTHUAT");
        ds.Tables["info"].Columns.Add("THUTHUAT");
        ds.Tables["info"].Columns.Add("CAUHOI");
        ds.Tables["info"].Columns.Add("THONGTINDIEUTRI");
        ds.Tables["info"].Columns.Add("GIUONG");
        ds.Tables["info"].Columns.Add("PHONG");
        ds.Tables["info"].Columns.Add("NGHENGHIEPMA");
        ds.Tables["info"].Columns.Add("DANTOCMA");
        ds.Tables["info"].Columns.Add("LICHHOICHANID");
        ds.Tables["info"].Columns.Add("DUYETHOICHANBOI");
        ds.Tables["info"].Columns.Add("DUYETHOICHANVAO", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("VITRITAINAN");
        ds.Tables["info"].Columns.Add("LNG", typeof(float?));
        ds.Tables["info"].Columns.Add("LAT", typeof(float?));
        ds.Tables["info"].Columns.Add("TRINHBAY", typeof(int));
        ds.Tables["info"].Columns.Add("CAPCUU", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
        {
                OCaBenh.ID,
                OCaBenh.KHAMCHUABENHID,
                OCaBenh.LYDOVAOVIEN,
                OCaBenh.CHUYENKHOAMA,
                OCaBenh.BACSY,
                OCaBenh.LYDOYEUCAU,
                OCaBenh.DONVITHAMVANID,
                OCaBenh.DONVITUVANID,
                OCaBenh.BENHNHANID,
                OCaBenh.TIENSUKHAMBENHID,
                OCaBenh.CHANDOANBANDAUMA,
                OCaBenh.CHANDOANBANDAU,
                OCaBenh.TAOBOI,
                OCaBenh.TAOVAO,
                OCaBenh.DENGHIVAO,
                OCaBenh.DUYETDENGHIVAO,
                OCaBenh.NGAYHOICHANDENGHI,
                OCaBenh.BATDAUHOICHANVAO,
                OCaBenh.KETTHUCHOICHANVAO,
                OCaBenh.MABN,
                OCaBenh.TENBN,
                OCaBenh.HOBN,
                OCaBenh.HOTENBN,
                OCaBenh.NGAYSINH,
                OCaBenh.HIENTHINGAYSINH,
                OCaBenh.GIOITINH,
                OCaBenh.DIACHI,
                OCaBenh.MOTA,
                OCaBenh.TIENSUBENH,
                OCaBenh.TRANGTHAI,
                OCaBenh.LAPLICHDAXEM,
                OCaBenh.LOAIHOICHAN,

                OCaBenh.NGAYNHAPVIEN,
                OCaBenh.NGAYXUATVIEN,
                OCaBenh.NHAPVIENTAI,
                OCaBenh.BENHSU,
                OCaBenh.TOANTHAN,
                OCaBenh.BOPHAN,
                OCaBenh.CHANDOANSOBO,
                OCaBenh.CANLAMSANG,
                OCaBenh.NXKETQUAXN,
                OCaBenh.KETQUACDHA,
                OCaBenh.CHANDOANXACDINH,
                OCaBenh.PHAUTHUAT,
                OCaBenh.THUTHUAT,
                OCaBenh.CAUHOI,
                OCaBenh.THONGTINDIEUTRI,
                OCaBenh.GIUONG,
                OCaBenh.PHONG,
                OCaBenh.NGHENGHIEPMA,
                OCaBenh.DANTOCMA,
                OCaBenh.LICHHOICHANID,
                OCaBenh.DUYETHOICHANBOI,
                OCaBenh.DUYETHOICHANVAO,
                OCaBenh.VITRITAINAN,
                OCaBenh.LNG,
                OCaBenh.LAT,
                OCaBenh.TRINHBAY,
                OCaBenh.CAPCUU,
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
