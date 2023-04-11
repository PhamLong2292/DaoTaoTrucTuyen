//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using OneTSQ.Model;
//using OneTSQ.Core.Call.Bussiness.Utility;

//namespace OneTSQ.Model.API
//{
//    public class CaBenh
//    {
//        public string ID;
//        public string TieuDe;
//        public string HoTenBN;
//        public int GioiTinh;
//        public string DiaChi;
//        public string MoTa;
//        public string ViTri;
//        public string NgaySinh;//dd/MM/yyyy
//        public DateTime TaoVao;
//        public string TaoBoi;
//        public string DonViThamVan;
//        public string DonViTuVan;
//        public List<string> HinhAnh;
//        //
//        public string MaBN;
//        public string ChuyenKhoa;
//    }
//    public class CaBenhAPI
//    {
//        public List<CaBenh> cabenhs;
//        public string errCode;
//    }
//    public class CaBenhParser
//    {
//        public static CaBenh ParserFromCaBenhCls(OneTSQ.Model.RenderInfoCls ORenderInfo, OneTSQ.Model.CaBenhCls cabenh)
//        {
//            CaBenh Ocabenh = new CaBenh();
//            Ocabenh.ID = cabenh.ID;
//            Ocabenh.HoTenBN = cabenh.HOTENBN;
//            Ocabenh.MaBN = cabenh.MABN;
//            Ocabenh.GioiTinh = (cabenh.GIOITINH != null ? (int)cabenh.GIOITINH : 0);
//            if (cabenh.NGAYSINH != null)
//            {
//                DateTime dt = (DateTime)cabenh.NGAYSINH;
//                Ocabenh.NgaySinh = dt.ToString("dd/MM/yyyy");
//            }

//            Ocabenh.ViTri = cabenh.VITRITAINAN;
//            Ocabenh.DiaChi = cabenh.DIACHI;
//            Ocabenh.TaoVao = cabenh.TAOVAO;
//            OwnerUserCls ou = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, cabenh.TAOBOI);
//            Ocabenh.TaoBoi = ou.FullName;
//            if (!string.IsNullOrEmpty(cabenh.DONVITHAMVANID))
//            {
//                OwnerCls donvi = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, cabenh.DONVITHAMVANID);
//                Ocabenh.DonViThamVan = donvi != null ? donvi.OwnerName : null;
//            }
//            if (!string.IsNullOrEmpty(cabenh.DONVITUVANID))
//            {
//                OwnerCls donvi = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().CreateModel(ORenderInfo, cabenh.DONVITUVANID);
//                Ocabenh.DonViTuVan = donvi != null ? donvi.OwnerName : null;
//            }
//            if (!string.IsNullOrEmpty(cabenh.CHUYENKHOAMA))
//            {
//                DepartmentCls donvi = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateDepartmentProcess().CreateModel(ORenderInfo, cabenh.CHUYENKHOAMA);
//                Ocabenh.DonViTuVan = donvi != null ? donvi.DepartmentCode : null;
//            }
//            Ocabenh.HinhAnh = GetHinhAnh(ORenderInfo, Ocabenh.ID);
//            return Ocabenh;
//        }
//        public static CaBenh[] ParserFromCaBenhCls(OneTSQ.Model.RenderInfoCls ORenderInfo, OneTSQ.Model.CaBenhCls[] cabenhs)
//        {
//            CaBenh[] Ocabenhs = new CaBenh[cabenhs.Length];
//            for (int i = 0; i < cabenhs.Length; i++)
//            {
//                Ocabenhs[i] = ParserFromCaBenhCls(ORenderInfo, cabenhs[i]);
//            }
//            return Ocabenhs;
//        }
//        public static List<string> GetHinhAnh(OneTSQ.Model.RenderInfoCls ORenderInfo, string CaBenh_ID)
//        {
//            List<string> HinhAnhs = new List<string>();
//            HinhAnhFilterCls OHinhAnhFilterCls = new HinhAnhFilterCls();
//            OHinhAnhFilterCls.CABENHID = CaBenh_ID;
//            //HinhAnhCls[] hinhanhs = OneTSQ.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateHinhAnhProcess().Reading(ORenderInfo, OHinhAnhFilterCls);
//            //if (hinhanhs != null && hinhanhs.Length > 0)
//            //{
//            //    for (int i = 0; i < hinhanhs.Length; i++)
//            //    {
//            //        HinhAnhs.Add(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadedImagePath"] + "/" + hinhanhs[i].TENTEP);
//            //    }
//            //}
//            //return HinhAnhs;
//            return null;
//        }
//    }

//    public class CaBenhCls
//    {
//        public string ID;
//        public int LOAIHOICHAN;//TuVanKCB = 0; CanLamSang = 1; PhauThuat = 2
//        public string MABENHVIENVETINH;
//        public string MABENHVIENHATNHAN;
//        public string MACHUYENKHOA;
//        public int? CAPCUU;//1 là cấp cứu
//        public DateTime? NGAYHOICHANDENGHI;
//        public string MABN;
//        public string HOTENBN;
//        public DateTime? NGAYSINH;
//        public int? LOAINGAYSINH;//Giờ = 1; Ngày = 2; Năm = 4
//        public int? GIOITINH;//Nam = 1; Nữ = 2
//        public string DIACHI;
//        public string MADANTOC;
//        public string MANGHENGHIEP;
//        public DateTime? NGAYVAOVIEN;
//        public string LYDOVAOVIEN;
//        public string GIUONG;
//        public string PHONG;
//        public string BENHSU;
//        public string TIENSUBENH;
//        public string TOANTHAN;
//        public string BOPHAN;
//        public string CHANDOANSOBO;
//        public string CANLAMSANG;
//        public string NXKETQUAXN;
//        public List<KetQuaXetNghiemCls> KetQuaXetNghiems;
//        public string KETQUACDHA;
//        public List<HinhAnhCls> HinhAnhs;
//        public string CHANDOANXACDINH;
//        public string PHAUTHUAT;
//        public string THUTHUAT;
//        public string THONGTINDIEUTRI;
//        public string CAUHOI;
//    }
//    public class KetQuaXetNghiemCls
//    {
//        public string NOITHUCHIEN;
//        public string MADICHVU;
//        public string TENDICHVU;
//        public DateTime? THOIGIAN;
//        public string MACHANDOAN;
//        public string KYTHUAT;
//        public string LOAIMAU;
//        public string KETLUAN;
//        public string NHANXET;
//        public string DENGHI;
//        public List<KetQuaXetNghiemChiTietCls> ketQuaXetNghiemChiTiets;
//    }
//    public class KetQuaXetNghiemChiTietCls
//    {
//        public string MACHISO;
//        public string TENCHISO;
//        public string GIATRI;
//    }
//    public class HinhAnhCls
//    {
//        public string TEN;//VD: anh.jpg
//        public byte[] NOIDUNG;
//        public long? KEY;
//        public string SCODE;
//        public string LINK;
//        public string MODALITY;
//        public DateTime? TIMEEX;
//    }
//    public class CaBenhPackageCls
//    {
//        public CaBenhCls caBenh;
//        public string guiBoi;
//    }
//}
