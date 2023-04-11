﻿using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using OneTSQ.Model;

namespace OneTSQ.Model
{
    public class PhieuPhanTichNguyenNhanSuCoCls
    {
        public string ID;
        public string SOBAOCAO = "";
        public string NGUOILAP_ID = "";
        public string CHUCDANH_ID = "";
        public DateTime? THOIGIANLAP;
        public string MOTASUCO;
        public int? THUCHIENQTKT;
        public int? NHIEMKHUANBENHVIEN;
        public int? THUOCDICHTRUYEN;
        public int? CHEPHAMMAU;
        public int? THIETBIYTE;
        public int? HANHVI;
        public int? TAINANNGUOIBENH;
        public int? HATANGCOSO;
        public int? QLNGUONLUCTC;
        public int? HSTHUTUCHANHCHINH;
        public int? KHAC;
        public string DTYLDUOCTHUCHIEN;
        public int? NNNHANVIEN;
        public int? NNNGUOIBENH;
        public int? NNMOITRUONGLAMVIEC;
        public int? NNTOCHUCDICHVU;
        public int? NNYEUTOBENNGOAI;
        public int? NNKHAC;
        public string KHACPHUCSUCO;
        public string DEXUATKHUYENCAO;
        public string MOTAKETQUA;
        public int? THAOLUANKHUYENCAO;
        public int? PHUHOPKHUYENCAO;
        public int? TONGTHUONGNBNC0;
        public int? TONGTHUONGNBNC1;
        public int? TONGTHUONGNBNC2;
        public int? TONGTHUONGNBNC3;
        public int? DANHGIATRENTOCHUC;
        public int? TRANGTHAI;
        public enum eTrangThai
        {
            Moi = 0,
            HoanTat = 1,
        }
        public enum TacVu
        {
            Luu = 0,
            hoantat = 1,
            Xoa = 2,
        }
        public enum eTraLoi
        {
            Khong = 1,
            Co = 2,
            KhongGhiNhan = 3,
        }

        public readonly static int[] BitArray = new int[]
        {
                    1 << (int)eLoai.KhongCoSuDongYCuaNB,
                    1 << (int)eLoai.KhongTHKhiCoCD,
                    1 << (int)eLoai.THSaiNB,
                    1 << (int)eLoai.THSaiTT,
                    1 << (int)eLoai.THSaiVTPhauThuat,
                    1 << (int)eLoai.BoSotDungCu,
                    1 << (int)eLoai.TVTrongThaiKy,
                    1 << (int)eLoai.TVKhiSinh,
                    1 << (int)eLoai.TVSoSinh,
                    1 << (int)eLoai.NKHuyet,
                    1 << (int)eLoai.ViemPhoi,
                    1 << (int)eLoai.CacLoaiNKKhac,
                    1 << (int)eLoai.NKVetMo,
                    1 << (int)eLoai.NKTietNieu,
                    1 << (int)eLoai.CPSaiThuocTD,
                    1 << (int)eLoai.ThieuThuoc,
                    1 << (int)eLoai.SaiLieuHamLuong,
                    1 << (int)eLoai.SaiThoiGian,
                    1 << (int)eLoai.SaiYLenh,
                    1 << (int)eLoai.BoSotThuoc,
                    1 << (int)eLoai.SaiThuoc,
                    1 << (int)eLoai.SaiNguoiBenh,
                    1 << (int)eLoai.SaiDuongDung,
                    1 << (int)eLoai.PhanUngPhu,
                    1 << (int)eLoai.TruyenMauNham,
                    1 << (int)eLoai.TruyenSaiLieu,
                    1 << (int)eLoai.ThieuThongTinHD,
                    1 << (int)eLoai.LoiThietBi,
                    1 << (int)eLoai.ThieuThietBi,
                    1 << (int)eLoai.TuGayHai,
                    1 << (int)eLoai.QRTDBoiNV,
                    1 << (int)eLoai.QRTDBoiNB,
                    1 << (int)eLoai.XamHaiCoTheBoiNB,
                    1 << (int)eLoai.CoHDTuTu,
                    1 << (int)eLoai.TronVien,
                    1 << (int)eLoai.TaiNan,
                    1 << (int)eLoai.BiHuHong,
                    1 << (int)eLoai.ThieuHoacKhongPH,
                    1 << (int)eLoai.TPHDDCuaDVKB,
                    1 << (int)eLoai.TPHDDCuaNL,
                    1 << (int)eLoai.TPHDDCuaCS,
                    1 << (int)eLoai.TaiLieuThieu,
                    1 << (int)eLoai.TaiLieuKhongRoRang,
                    1 << (int)eLoai.TGChoDoiKeoDai,
                    1 << (int)eLoai.CCHSTLCham,
                    1 << (int)eLoai.NhamHSTL,
                    1 << (int)eLoai.TTHCPhucTap,
                    1 << (int)eLoai.CacSCKhongDuocDeCap,
                    1 << (int)eLoai.NhanThucNV,
                    1 << (int)eLoai.ThucHanhNV,
                    1 << (int)eLoai.ThaiDoNV,
                    1 << (int)eLoai.GiaoTiepNV,
                    1 << (int)eLoai.TamSinhLyNV,
                    1 << (int)eLoai.CacYTXHNV,
                    1 << (int)eLoai.NhanThucNB,
                    1 << (int)eLoai.ThucHanhNB,
                    1 << (int)eLoai.ThaiDoNB,
                    1 << (int)eLoai.GiaoTiepNB,
                    1 << (int)eLoai.TamSinhLyNB,
                    1 << (int)eLoai.CacYTXHNB,
                    1 << (int)eLoai.CoSovatChat,
                    1 << (int)eLoai.KhoangCach,
                    1 << (int)eLoai.DGVeDoAnToan,
                    1 << (int)eLoai.NoiQuy,
                    1 << (int)eLoai.CacChinhSach,
                    1 << (int)eLoai.TuanThuQT,
                    1 << (int)eLoai.VanHoaToChuc,
                    1 << (int)eLoai.LamViecNhom,
                    1 << (int)eLoai.MoiTruongTuNhien,
                    1 << (int)eLoai.SPCNCSHT,
                    1 << (int)eLoai.QTHeThongDV,
                    1 << (int)eLoai.CacYTKhongDeCap,
                    1 << (int)eLoai.A,
                    1 << (int)eLoai.B,
                    1 << (int)eLoai.C,
                    1 << (int)eLoai.D,
                    1 << (int)eLoai.E,
                    1 << (int)eLoai.F,
                    1 << (int)eLoai.G,
                    1 << (int)eLoai.H,
                    1 << (int)eLoai.I,
                    1 << (int)eLoai.TonHaiTaiSan,
                    1 << (int)eLoai.TangNLPVChoBV,
                    1 << (int)eLoai.QTCuaTT,
                    1 << (int)eLoai.KhieuNaiCuaNB,
                    1 << (int)eLoai.TonHaiDanhTieng,
                    1 << (int)eLoai.CanThiepCuaPL,
                    1 << (int)eLoai.Khac,
        };
        //Thực hiện quy trình kỹ thuật
        public bool KhongCoSuDongYCuaNB
        {
            get { return (this.THUCHIENQTKT & BitArray[(int)eLoai.KhongCoSuDongYCuaNB]) == BitArray[(int)eLoai.KhongCoSuDongYCuaNB]; }
            set { this.THUCHIENQTKT = (value ? (this.THUCHIENQTKT | BitArray[(int)eLoai.KhongCoSuDongYCuaNB]) : (this.THUCHIENQTKT & (~BitArray[(int)eLoai.KhongCoSuDongYCuaNB]))); }
        }
        public bool KhongTHKhiCoCD
        {
            get { return (this.THUCHIENQTKT & BitArray[(int)eLoai.KhongTHKhiCoCD]) == BitArray[(int)eLoai.KhongTHKhiCoCD]; }
            set { this.THUCHIENQTKT = (value ? (this.THUCHIENQTKT | BitArray[(int)eLoai.KhongTHKhiCoCD]) : (this.THUCHIENQTKT & (~BitArray[(int)eLoai.KhongTHKhiCoCD]))); }
        }
        public bool THSaiNB
        {
            get { return (this.THUCHIENQTKT & BitArray[(int)eLoai.THSaiNB]) == BitArray[(int)eLoai.THSaiNB]; }
            set { this.THUCHIENQTKT = (value ? (this.THUCHIENQTKT | BitArray[(int)eLoai.THSaiNB]) : (this.THUCHIENQTKT & (~BitArray[(int)eLoai.THSaiNB]))); }
        }
        public bool THSaiTT
        {
            get { return (this.THUCHIENQTKT & BitArray[(int)eLoai.THSaiTT]) == BitArray[(int)eLoai.THSaiTT]; }
            set { this.THUCHIENQTKT = (value ? (this.THUCHIENQTKT | BitArray[(int)eLoai.THSaiTT]) : (this.THUCHIENQTKT & (~BitArray[(int)eLoai.THSaiTT]))); }
        }
        public bool THSaiVTPhauThuat
        {
            get { return (this.THUCHIENQTKT & BitArray[(int)eLoai.THSaiVTPhauThuat]) == BitArray[(int)eLoai.THSaiVTPhauThuat]; }
            set { this.THUCHIENQTKT = (value ? (this.THUCHIENQTKT | BitArray[(int)eLoai.THSaiVTPhauThuat]) : (this.THUCHIENQTKT & (~BitArray[(int)eLoai.THSaiVTPhauThuat]))); }
        }
        public bool BoSotDungCu
        {
            get { return (this.THUCHIENQTKT & BitArray[(int)eLoai.BoSotDungCu]) == BitArray[(int)eLoai.BoSotDungCu]; }
            set { this.THUCHIENQTKT = (value ? (this.THUCHIENQTKT | BitArray[(int)eLoai.BoSotDungCu]) : (this.THUCHIENQTKT & (~BitArray[(int)eLoai.BoSotDungCu]))); }
        }
        public bool TVTrongThaiKy
        {
            get { return (this.THUCHIENQTKT & BitArray[(int)eLoai.TVTrongThaiKy]) == BitArray[(int)eLoai.TVTrongThaiKy]; }
            set { this.THUCHIENQTKT = (value ? (this.THUCHIENQTKT | BitArray[(int)eLoai.TVTrongThaiKy]) : (this.THUCHIENQTKT & (~BitArray[(int)eLoai.TVTrongThaiKy]))); }
        }
        public bool TVKhiSinh
        {
            get { return (this.THUCHIENQTKT & BitArray[(int)eLoai.TVKhiSinh]) == BitArray[(int)eLoai.TVKhiSinh]; }
            set { this.THUCHIENQTKT = (value ? (this.THUCHIENQTKT | BitArray[(int)eLoai.TVKhiSinh]) : (this.THUCHIENQTKT & (~BitArray[(int)eLoai.TVKhiSinh]))); }
        }
        public bool TVSoSinh
        {
            get { return (this.THUCHIENQTKT & BitArray[(int)eLoai.TVSoSinh]) == BitArray[(int)eLoai.TVSoSinh]; }
            set { this.THUCHIENQTKT = (value ? (this.THUCHIENQTKT | BitArray[(int)eLoai.TVSoSinh]) : (this.THUCHIENQTKT & (~BitArray[(int)eLoai.TVSoSinh]))); }
        }
       
        //Nhiễm khuẩn bệnh viện
        public bool NKHuyet
        {
            get { return (this.NHIEMKHUANBENHVIEN & BitArray[(int)eLoai.NKHuyet]) == BitArray[(int)eLoai.NKHuyet]; }
            set { this.NHIEMKHUANBENHVIEN = (value ? (this.NHIEMKHUANBENHVIEN | BitArray[(int)eLoai.NKHuyet]) : (this.NHIEMKHUANBENHVIEN & (~BitArray[(int)eLoai.NKHuyet]))); }
        }
        public bool ViemPhoi
        {
            get { return (this.NHIEMKHUANBENHVIEN & BitArray[(int)eLoai.ViemPhoi]) == BitArray[(int)eLoai.ViemPhoi]; }
            set { this.NHIEMKHUANBENHVIEN = (value ? (this.NHIEMKHUANBENHVIEN | BitArray[(int)eLoai.ViemPhoi]) : (this.NHIEMKHUANBENHVIEN & (~BitArray[(int)eLoai.ViemPhoi]))); }
        }
        public bool CacLoaiNKKhac
        {
            get { return (this.NHIEMKHUANBENHVIEN & BitArray[(int)eLoai.CacLoaiNKKhac]) == BitArray[(int)eLoai.CacLoaiNKKhac]; }
            set { this.NHIEMKHUANBENHVIEN = (value ? (this.NHIEMKHUANBENHVIEN | BitArray[(int)eLoai.CacLoaiNKKhac]) : (this.NHIEMKHUANBENHVIEN & (~BitArray[(int)eLoai.CacLoaiNKKhac]))); }
        }
        public bool NKVetMo
        {
            get { return (this.NHIEMKHUANBENHVIEN & BitArray[(int)eLoai.NKVetMo]) == BitArray[(int)eLoai.NKVetMo]; }
            set { this.NHIEMKHUANBENHVIEN = (value ? (this.NHIEMKHUANBENHVIEN | BitArray[(int)eLoai.NKVetMo]) : (this.NHIEMKHUANBENHVIEN & (~BitArray[(int)eLoai.NKVetMo]))); }
        }
        public bool NKTietNieu
        {
            get { return (this.NHIEMKHUANBENHVIEN & BitArray[(int)eLoai.NKTietNieu]) == BitArray[(int)eLoai.NKTietNieu]; }
            set { this.NHIEMKHUANBENHVIEN = (value ? (this.NHIEMKHUANBENHVIEN | BitArray[(int)eLoai.NKTietNieu]) : (this.NHIEMKHUANBENHVIEN & (~BitArray[(int)eLoai.NKTietNieu]))); }
        }

        //Thuốc và dịc truyền
        public bool CPSaiThuocTD
        {
            get { return (this.THUOCDICHTRUYEN & BitArray[(int)eLoai.CPSaiThuocTD]) == BitArray[(int)eLoai.CPSaiThuocTD]; }
            set { this.THUOCDICHTRUYEN = (value ? (this.THUOCDICHTRUYEN | BitArray[(int)eLoai.CPSaiThuocTD]) : (this.THUOCDICHTRUYEN & (~BitArray[(int)eLoai.CPSaiThuocTD]))); }
        }
        public bool ThieuThuoc
        {
            get { return (this.THUOCDICHTRUYEN & BitArray[(int)eLoai.ThieuThuoc]) == BitArray[(int)eLoai.ThieuThuoc]; }
            set { this.THUOCDICHTRUYEN = (value ? (this.THUOCDICHTRUYEN | BitArray[(int)eLoai.ThieuThuoc]) : (this.THUOCDICHTRUYEN & (~BitArray[(int)eLoai.ThieuThuoc]))); }
        }
        public bool SaiLieuHamLuong
        {
            get { return (this.THUOCDICHTRUYEN & BitArray[(int)eLoai.SaiLieuHamLuong]) == BitArray[(int)eLoai.SaiLieuHamLuong]; }
            set { this.THUOCDICHTRUYEN = (value ? (this.THUOCDICHTRUYEN | BitArray[(int)eLoai.SaiLieuHamLuong]) : (this.THUOCDICHTRUYEN & (~BitArray[(int)eLoai.SaiLieuHamLuong]))); }
        }
        public bool SaiThoiGian
        {
            get { return (this.THUOCDICHTRUYEN & BitArray[(int)eLoai.SaiThoiGian]) == BitArray[(int)eLoai.SaiThoiGian]; }
            set { this.THUOCDICHTRUYEN = (value ? (this.THUOCDICHTRUYEN | BitArray[(int)eLoai.SaiThoiGian]) : (this.THUOCDICHTRUYEN & (~BitArray[(int)eLoai.SaiThoiGian]))); }
        }
        public bool SaiYLenh
        {
            get { return (this.THUOCDICHTRUYEN & BitArray[(int)eLoai.SaiYLenh]) == BitArray[(int)eLoai.SaiYLenh]; }
            set { this.THUOCDICHTRUYEN = (value ? (this.THUOCDICHTRUYEN | BitArray[(int)eLoai.SaiYLenh]) : (this.THUOCDICHTRUYEN & (~BitArray[(int)eLoai.SaiYLenh]))); }
        }
        public bool BoSotThuoc
        {
            get { return (this.THUOCDICHTRUYEN & BitArray[(int)eLoai.BoSotThuoc]) == BitArray[(int)eLoai.BoSotThuoc]; }
            set { this.THUOCDICHTRUYEN = (value ? (this.THUOCDICHTRUYEN | BitArray[(int)eLoai.BoSotThuoc]) : (this.THUOCDICHTRUYEN & (~BitArray[(int)eLoai.BoSotThuoc]))); }
        }
        public bool SaiThuoc
        {
            get { return (this.THUOCDICHTRUYEN & BitArray[(int)eLoai.SaiThuoc]) == BitArray[(int)eLoai.SaiThuoc]; }
            set { this.THUOCDICHTRUYEN = (value ? (this.THUOCDICHTRUYEN | BitArray[(int)eLoai.SaiThuoc]) : (this.THUOCDICHTRUYEN & (~BitArray[(int)eLoai.SaiThuoc]))); }
        }
        public bool SaiNguoiBenh
        {
            get { return (this.THUOCDICHTRUYEN & BitArray[(int)eLoai.SaiNguoiBenh]) == BitArray[(int)eLoai.SaiNguoiBenh]; }
            set { this.THUOCDICHTRUYEN = (value ? (this.THUOCDICHTRUYEN | BitArray[(int)eLoai.SaiNguoiBenh]) : (this.THUOCDICHTRUYEN & (~BitArray[(int)eLoai.SaiNguoiBenh]))); }
        }
        public bool SaiDuongDung
        {
            get { return (this.THUOCDICHTRUYEN & BitArray[(int)eLoai.SaiDuongDung]) == BitArray[(int)eLoai.SaiDuongDung]; }
            set { this.THUOCDICHTRUYEN = (value ? (this.THUOCDICHTRUYEN | BitArray[(int)eLoai.SaiDuongDung]) : (this.THUOCDICHTRUYEN & (~BitArray[(int)eLoai.SaiDuongDung]))); }
        }

        //Máu và chế phẩm máu
        public bool PhanUngPhu
        {
            get { return (this.CHEPHAMMAU & BitArray[(int)eLoai.PhanUngPhu]) == BitArray[(int)eLoai.PhanUngPhu]; }
            set { this.CHEPHAMMAU = (value ? (this.CHEPHAMMAU | BitArray[(int)eLoai.PhanUngPhu]) : (this.CHEPHAMMAU & (~BitArray[(int)eLoai.PhanUngPhu]))); }
        }
        public bool TruyenMauNham
        {
            get { return (this.CHEPHAMMAU & BitArray[(int)eLoai.TruyenMauNham]) == BitArray[(int)eLoai.TruyenMauNham]; }
            set { this.CHEPHAMMAU = (value ? (this.CHEPHAMMAU | BitArray[(int)eLoai.TruyenMauNham]) : (this.CHEPHAMMAU & (~BitArray[(int)eLoai.TruyenMauNham]))); }
        }
        public bool TruyenSaiLieu
        {
            get { return (this.CHEPHAMMAU & BitArray[(int)eLoai.TruyenSaiLieu]) == BitArray[(int)eLoai.TruyenSaiLieu]; }
            set { this.CHEPHAMMAU = (value ? (this.CHEPHAMMAU | BitArray[(int)eLoai.TruyenSaiLieu]) : (this.CHEPHAMMAU & (~BitArray[(int)eLoai.TruyenSaiLieu]))); }
        }

        //Thiết bị y tế
        public bool ThieuThongTinHD
        {
            get { return (this.THIETBIYTE & BitArray[(int)eLoai.ThieuThongTinHD]) == BitArray[(int)eLoai.ThieuThongTinHD]; }
            set { this.THIETBIYTE = (value ? (this.THIETBIYTE | BitArray[(int)eLoai.ThieuThongTinHD]) : (this.THIETBIYTE & (~BitArray[(int)eLoai.ThieuThongTinHD]))); }
        }
        public bool LoiThietBi
        {
            get { return (this.THIETBIYTE & BitArray[(int)eLoai.LoiThietBi]) == BitArray[(int)eLoai.LoiThietBi]; }
            set { this.THIETBIYTE = (value ? (this.THIETBIYTE | BitArray[(int)eLoai.LoiThietBi]) : (this.THIETBIYTE & (~BitArray[(int)eLoai.LoiThietBi]))); }
        }
        public bool ThieuThietBi
        {
            get { return (this.THIETBIYTE & BitArray[(int)eLoai.ThieuThietBi]) == BitArray[(int)eLoai.ThieuThietBi]; }
            set { this.THIETBIYTE = (value ? (this.THIETBIYTE | BitArray[(int)eLoai.ThieuThietBi]) : (this.THIETBIYTE & (~BitArray[(int)eLoai.ThieuThietBi]))); }
        }

        //Hành vi
        public bool TuGayHai
        {
            get { return (this.HANHVI & BitArray[(int)eLoai.TuGayHai]) == BitArray[(int)eLoai.TuGayHai]; }
            set { this.HANHVI = (value ? (this.HANHVI | BitArray[(int)eLoai.TuGayHai]) : (this.HANHVI & (~BitArray[(int)eLoai.TuGayHai]))); }
        }
        public bool QRTDBoiNV
        {
            get { return (this.HANHVI & BitArray[(int)eLoai.QRTDBoiNV]) == BitArray[(int)eLoai.QRTDBoiNV]; }
            set { this.HANHVI = (value ? (this.HANHVI | BitArray[(int)eLoai.QRTDBoiNV]) : (this.HANHVI & (~BitArray[(int)eLoai.QRTDBoiNV]))); }
        }
        public bool QRTDBoiNB
        {
            get { return (this.HANHVI & BitArray[(int)eLoai.QRTDBoiNB]) == BitArray[(int)eLoai.QRTDBoiNB]; }
            set { this.HANHVI = (value ? (this.HANHVI | BitArray[(int)eLoai.QRTDBoiNB]) : (this.HANHVI & (~BitArray[(int)eLoai.QRTDBoiNB]))); }
        }
        public bool XamHaiCoTheBoiNB
        {
            get { return (this.HANHVI & BitArray[(int)eLoai.XamHaiCoTheBoiNB]) == BitArray[(int)eLoai.XamHaiCoTheBoiNB]; }
            set { this.HANHVI = (value ? (this.HANHVI | BitArray[(int)eLoai.XamHaiCoTheBoiNB]) : (this.HANHVI & (~BitArray[(int)eLoai.XamHaiCoTheBoiNB]))); }
        }
        public bool CoHDTuTu
        {
            get { return (this.HANHVI & BitArray[(int)eLoai.CoHDTuTu]) == BitArray[(int)eLoai.CoHDTuTu]; }
            set { this.HANHVI = (value ? (this.HANHVI | BitArray[(int)eLoai.CoHDTuTu]) : (this.HANHVI & (~BitArray[(int)eLoai.CoHDTuTu]))); }
        }
        public bool TronVien
        {
            get { return (this.HANHVI & BitArray[(int)eLoai.TronVien]) == BitArray[(int)eLoai.TronVien]; }
            set { this.HANHVI = (value ? (this.HANHVI | BitArray[(int)eLoai.TronVien]) : (this.HANHVI & (~BitArray[(int)eLoai.TronVien]))); }
        }

        //Tai nạn
        public bool TaiNan
        {
            get { return (this.TAINANNGUOIBENH & BitArray[(int)eLoai.TaiNan]) == BitArray[(int)eLoai.TaiNan]; }
            set { this.TAINANNGUOIBENH = (value ? (this.TAINANNGUOIBENH | BitArray[(int)eLoai.TaiNan]) : (this.TAINANNGUOIBENH & (~BitArray[(int)eLoai.TaiNan]))); }
        }

        //Hạ tầng cơ sở
        public bool BiHuHong
        {
            get { return (this.HATANGCOSO & BitArray[(int)eLoai.BiHuHong]) == BitArray[(int)eLoai.BiHuHong]; }
            set { this.HATANGCOSO = (value ? (this.HATANGCOSO | BitArray[(int)eLoai.BiHuHong]) : (this.HATANGCOSO & (~BitArray[(int)eLoai.BiHuHong]))); }
        }

        public bool ThieuHoacKhongPH
        {
            get { return (this.HATANGCOSO & BitArray[(int)eLoai.ThieuHoacKhongPH]) == BitArray[(int)eLoai.ThieuHoacKhongPH]; }
            set { this.HATANGCOSO = (value ? (this.HATANGCOSO | BitArray[(int)eLoai.ThieuHoacKhongPH]) : (this.HATANGCOSO & (~BitArray[(int)eLoai.ThieuHoacKhongPH]))); }
        }

        //Quản lý nguồn lực
        public bool TPHDDCuaDVKB
        {
            get { return (this.QLNGUONLUCTC & BitArray[(int)eLoai.TPHDDCuaDVKB]) == BitArray[(int)eLoai.TPHDDCuaDVKB]; }
            set { this.QLNGUONLUCTC = (value ? (this.QLNGUONLUCTC | BitArray[(int)eLoai.TPHDDCuaDVKB]) : (this.QLNGUONLUCTC & (~BitArray[(int)eLoai.TPHDDCuaDVKB]))); }
        }
        public bool TPHDDCuaNL
        {
            get { return (this.QLNGUONLUCTC & BitArray[(int)eLoai.TPHDDCuaNL]) == BitArray[(int)eLoai.TPHDDCuaNL]; }
            set { this.QLNGUONLUCTC = (value ? (this.QLNGUONLUCTC | BitArray[(int)eLoai.TPHDDCuaNL]) : (this.QLNGUONLUCTC & (~BitArray[(int)eLoai.TPHDDCuaNL]))); }
        }
        public bool TPHDDCuaCS
        {
            get { return (this.QLNGUONLUCTC & BitArray[(int)eLoai.TPHDDCuaCS]) == BitArray[(int)eLoai.TPHDDCuaCS]; }
            set { this.QLNGUONLUCTC = (value ? (this.QLNGUONLUCTC | BitArray[(int)eLoai.TPHDDCuaCS]) : (this.QLNGUONLUCTC & (~BitArray[(int)eLoai.TPHDDCuaCS]))); }
        }

        //Hồ sơ, tài liệu
        public bool TaiLieuThieu
        {
            get { return (this.HSTHUTUCHANHCHINH & BitArray[(int)eLoai.TaiLieuThieu]) == BitArray[(int)eLoai.TaiLieuThieu]; }
            set { this.HSTHUTUCHANHCHINH = (value ? (this.HSTHUTUCHANHCHINH | BitArray[(int)eLoai.TaiLieuThieu]) : (this.HSTHUTUCHANHCHINH & (~BitArray[(int)eLoai.TaiLieuThieu]))); }
        }
        public bool TaiLieuKhongRoRang
        {
            get { return (this.HSTHUTUCHANHCHINH & BitArray[(int)eLoai.TaiLieuKhongRoRang]) == BitArray[(int)eLoai.TaiLieuKhongRoRang]; }
            set { this.HSTHUTUCHANHCHINH = (value ? (this.HSTHUTUCHANHCHINH | BitArray[(int)eLoai.TaiLieuKhongRoRang]) : (this.HSTHUTUCHANHCHINH & (~BitArray[(int)eLoai.TaiLieuKhongRoRang]))); }
        }
        public bool TGChoDoiKeoDai
        {
            get { return (this.HSTHUTUCHANHCHINH & BitArray[(int)eLoai.TGChoDoiKeoDai]) == BitArray[(int)eLoai.TGChoDoiKeoDai]; }
            set { this.HSTHUTUCHANHCHINH = (value ? (this.HSTHUTUCHANHCHINH | BitArray[(int)eLoai.TGChoDoiKeoDai]) : (this.HSTHUTUCHANHCHINH & (~BitArray[(int)eLoai.TGChoDoiKeoDai]))); }
        }
        public bool CCHSTLCham
        {
            get { return (this.HSTHUTUCHANHCHINH & BitArray[(int)eLoai.CCHSTLCham]) == BitArray[(int)eLoai.CCHSTLCham]; }
            set { this.HSTHUTUCHANHCHINH = (value ? (this.HSTHUTUCHANHCHINH | BitArray[(int)eLoai.CCHSTLCham]) : (this.HSTHUTUCHANHCHINH & (~BitArray[(int)eLoai.CCHSTLCham]))); }
        }
        public bool NhamHSTL
        {
            get { return (this.HSTHUTUCHANHCHINH & BitArray[(int)eLoai.NhamHSTL]) == BitArray[(int)eLoai.NhamHSTL]; }
            set { this.HSTHUTUCHANHCHINH = (value ? (this.HSTHUTUCHANHCHINH | BitArray[(int)eLoai.NhamHSTL]) : (this.HSTHUTUCHANHCHINH & (~BitArray[(int)eLoai.NhamHSTL]))); }
        }
        public bool TTHCPhucTap
        {
            get { return (this.HSTHUTUCHANHCHINH & BitArray[(int)eLoai.TTHCPhucTap]) == BitArray[(int)eLoai.TTHCPhucTap]; }
            set { this.HSTHUTUCHANHCHINH = (value ? (this.HSTHUTUCHANHCHINH | BitArray[(int)eLoai.TTHCPhucTap]) : (this.HSTHUTUCHANHCHINH & (~BitArray[(int)eLoai.TTHCPhucTap]))); }
        }

        //Khác
        public bool CacSCKhongDuocDeCap
        {
            get { return (this.KHAC & BitArray[(int)eLoai.CacSCKhongDuocDeCap]) == BitArray[(int)eLoai.CacSCKhongDuocDeCap]; }
            set { this.KHAC = (value ? (this.KHAC | BitArray[(int)eLoai.CacSCKhongDuocDeCap]) : (this.KHAC & (~BitArray[(int)eLoai.CacSCKhongDuocDeCap]))); }
        }

        //Nhân viên
        public bool NhanThucNV
        {
            get { return (this.NNNHANVIEN & BitArray[(int)eLoai.NhanThucNV]) == BitArray[(int)eLoai.NhanThucNV]; }
            set { this.NNNHANVIEN = (value ? (this.NNNHANVIEN | BitArray[(int)eLoai.NhanThucNV]) : (this.NNNHANVIEN & (~BitArray[(int)eLoai.NhanThucNV]))); }
        }
        public bool ThucHanhNV
        {
            get { return (this.NNNHANVIEN & BitArray[(int)eLoai.ThucHanhNV]) == BitArray[(int)eLoai.ThucHanhNV]; }
            set { this.NNNHANVIEN = (value ? (this.NNNHANVIEN | BitArray[(int)eLoai.ThucHanhNV]) : (this.NNNHANVIEN & (~BitArray[(int)eLoai.ThucHanhNV]))); }
        }
        public bool ThaiDoNV
        {
            get { return (this.NNNHANVIEN & BitArray[(int)eLoai.ThaiDoNV]) == BitArray[(int)eLoai.ThaiDoNV]; }
            set { this.NNNHANVIEN = (value ? (this.NNNHANVIEN | BitArray[(int)eLoai.ThaiDoNV]) : (this.NNNHANVIEN & (~BitArray[(int)eLoai.ThaiDoNV]))); }
        }
        public bool GiaoTiepNV
        {
            get { return (this.NNNHANVIEN & BitArray[(int)eLoai.GiaoTiepNV]) == BitArray[(int)eLoai.GiaoTiepNV]; }
            set { this.NNNHANVIEN = (value ? (this.NNNHANVIEN | BitArray[(int)eLoai.GiaoTiepNV]) : (this.NNNHANVIEN & (~BitArray[(int)eLoai.GiaoTiepNV]))); }
        }
        public bool TamSinhLyNV
        {
            get { return (this.NNNHANVIEN & BitArray[(int)eLoai.TamSinhLyNV]) == BitArray[(int)eLoai.TamSinhLyNV]; }
            set { this.NNNHANVIEN = (value ? (this.NNNHANVIEN | BitArray[(int)eLoai.TamSinhLyNV]) : (this.NNNHANVIEN & (~BitArray[(int)eLoai.TamSinhLyNV]))); }
        }
        public bool CacYTXHNV
        {
            get { return (this.NNNHANVIEN & BitArray[(int)eLoai.CacYTXHNV]) == BitArray[(int)eLoai.CacYTXHNV]; }
            set { this.NNNHANVIEN = (value ? (this.NNNHANVIEN | BitArray[(int)eLoai.CacYTXHNV]) : (this.NNNHANVIEN & (~BitArray[(int)eLoai.CacYTXHNV]))); }
        }

        //Người bệnh
        public bool NhanThucNB
        {
            get { return (this.NNNGUOIBENH & BitArray[(int)eLoai.NhanThucNB]) == BitArray[(int)eLoai.NhanThucNB]; }
            set { this.NNNGUOIBENH = (value ? (this.NNNGUOIBENH | BitArray[(int)eLoai.NhanThucNB]) : (this.NNNGUOIBENH & (~BitArray[(int)eLoai.NhanThucNB]))); }
        }
        public bool ThucHanhNB
        {
            get { return (this.NNNGUOIBENH & BitArray[(int)eLoai.ThucHanhNB]) == BitArray[(int)eLoai.ThucHanhNB]; }
            set { this.NNNGUOIBENH = (value ? (this.NNNGUOIBENH | BitArray[(int)eLoai.ThucHanhNB]) : (this.NNNGUOIBENH & (~BitArray[(int)eLoai.ThucHanhNB]))); }
        }
        public bool ThaiDoNB
        {
            get { return (this.NNNGUOIBENH & BitArray[(int)eLoai.ThaiDoNB]) == BitArray[(int)eLoai.ThaiDoNB]; }
            set { this.NNNGUOIBENH = (value ? (this.NNNGUOIBENH | BitArray[(int)eLoai.ThaiDoNB]) : (this.NNNGUOIBENH & (~BitArray[(int)eLoai.ThaiDoNB]))); }
        }
        public bool GiaoTiepNB
        {
            get { return (this.NNNGUOIBENH & BitArray[(int)eLoai.GiaoTiepNB]) == BitArray[(int)eLoai.GiaoTiepNB]; }
            set { this.NNNGUOIBENH = (value ? (this.NNNGUOIBENH | BitArray[(int)eLoai.GiaoTiepNB]) : (this.NNNGUOIBENH & (~BitArray[(int)eLoai.GiaoTiepNB]))); }
        }
        public bool TamSinhLyNB
        {
            get { return (this.NNNGUOIBENH & BitArray[(int)eLoai.TamSinhLyNB]) == BitArray[(int)eLoai.TamSinhLyNB]; }
            set { this.NNNGUOIBENH = (value ? (this.NNNGUOIBENH | BitArray[(int)eLoai.TamSinhLyNB]) : (this.NNNGUOIBENH & (~BitArray[(int)eLoai.TamSinhLyNB]))); }
        }
        public bool CacYTXHNB
        {
            get { return (this.NNNGUOIBENH & BitArray[(int)eLoai.CacYTXHNB]) == BitArray[(int)eLoai.CacYTXHNB]; }
            set { this.NNNGUOIBENH = (value ? (this.NNNGUOIBENH | BitArray[(int)eLoai.CacYTXHNB]) : (this.NNNGUOIBENH & (~BitArray[(int)eLoai.CacYTXHNB]))); }
        }


        //Môi trường làm việc
        public bool CoSovatChat
        {
            get { return (this.NNMOITRUONGLAMVIEC & BitArray[(int)eLoai.CoSovatChat]) == BitArray[(int)eLoai.CoSovatChat]; }
            set { this.NNMOITRUONGLAMVIEC = (value ? (this.NNMOITRUONGLAMVIEC | BitArray[(int)eLoai.CoSovatChat]) : (this.NNMOITRUONGLAMVIEC & (~BitArray[(int)eLoai.CoSovatChat]))); }
        }
        public bool KhoangCach
        {
            get { return (this.NNMOITRUONGLAMVIEC & BitArray[(int)eLoai.KhoangCach]) == BitArray[(int)eLoai.KhoangCach]; }
            set { this.NNMOITRUONGLAMVIEC = (value ? (this.NNMOITRUONGLAMVIEC | BitArray[(int)eLoai.KhoangCach]) : (this.NNMOITRUONGLAMVIEC & (~BitArray[(int)eLoai.KhoangCach]))); }
        }
        public bool DGVeDoAnToan
        {
            get { return (this.NNMOITRUONGLAMVIEC & BitArray[(int)eLoai.DGVeDoAnToan]) == BitArray[(int)eLoai.DGVeDoAnToan]; }
            set { this.NNMOITRUONGLAMVIEC = (value ? (this.NNMOITRUONGLAMVIEC | BitArray[(int)eLoai.DGVeDoAnToan]) : (this.NNMOITRUONGLAMVIEC & (~BitArray[(int)eLoai.DGVeDoAnToan]))); }
        }
        public bool NoiQuy
        {
            get { return (this.NNMOITRUONGLAMVIEC & BitArray[(int)eLoai.NoiQuy]) == BitArray[(int)eLoai.NoiQuy]; }
            set { this.NNMOITRUONGLAMVIEC = (value ? (this.NNMOITRUONGLAMVIEC | BitArray[(int)eLoai.NoiQuy]) : (this.NNMOITRUONGLAMVIEC & (~BitArray[(int)eLoai.NoiQuy]))); }
        }

        //Tổ chức/ Dịch vụ
        public bool CacChinhSach
        {
            get { return (this.NNTOCHUCDICHVU & BitArray[(int)eLoai.CacChinhSach]) == BitArray[(int)eLoai.CacChinhSach]; }
            set { this.NNTOCHUCDICHVU = (value ? (this.NNTOCHUCDICHVU | BitArray[(int)eLoai.CacChinhSach]) : (this.NNTOCHUCDICHVU & (~BitArray[(int)eLoai.CacChinhSach]))); }
        }
        public bool TuanThuQT
        {
            get { return (this.NNTOCHUCDICHVU & BitArray[(int)eLoai.TuanThuQT]) == BitArray[(int)eLoai.TuanThuQT]; }
            set { this.NNTOCHUCDICHVU = (value ? (this.NNTOCHUCDICHVU | BitArray[(int)eLoai.TuanThuQT]) : (this.NNTOCHUCDICHVU & (~BitArray[(int)eLoai.TuanThuQT]))); }
        }
        public bool VanHoaToChuc
        {
            get { return (this.NNTOCHUCDICHVU & BitArray[(int)eLoai.VanHoaToChuc]) == BitArray[(int)eLoai.VanHoaToChuc]; }
            set { this.NNTOCHUCDICHVU = (value ? (this.NNTOCHUCDICHVU | BitArray[(int)eLoai.VanHoaToChuc]) : (this.NNTOCHUCDICHVU & (~BitArray[(int)eLoai.VanHoaToChuc]))); }
        }
        public bool LamViecNhom
        {
            get { return (this.NNTOCHUCDICHVU & BitArray[(int)eLoai.LamViecNhom]) == BitArray[(int)eLoai.LamViecNhom]; }
            set { this.NNTOCHUCDICHVU = (value ? (this.NNTOCHUCDICHVU | BitArray[(int)eLoai.LamViecNhom]) : (this.NNTOCHUCDICHVU & (~BitArray[(int)eLoai.LamViecNhom]))); }
        }

        //Yếu tố bên ngoài
        public bool MoiTruongTuNhien
        {
            get { return (this.NNYEUTOBENNGOAI & BitArray[(int)eLoai.MoiTruongTuNhien]) == BitArray[(int)eLoai.MoiTruongTuNhien]; }
            set { this.NNYEUTOBENNGOAI = (value ? (this.NNYEUTOBENNGOAI | BitArray[(int)eLoai.MoiTruongTuNhien]) : (this.NNYEUTOBENNGOAI & (~BitArray[(int)eLoai.MoiTruongTuNhien]))); }
        }
        public bool SPCNCSHT
        {
            get { return (this.NNYEUTOBENNGOAI & BitArray[(int)eLoai.SPCNCSHT]) == BitArray[(int)eLoai.SPCNCSHT]; }
            set { this.NNYEUTOBENNGOAI = (value ? (this.NNYEUTOBENNGOAI | BitArray[(int)eLoai.SPCNCSHT]) : (this.NNYEUTOBENNGOAI & (~BitArray[(int)eLoai.SPCNCSHT]))); }
        }
        public bool QTHeThongDV
        {
            get { return (this.NNYEUTOBENNGOAI & BitArray[(int)eLoai.QTHeThongDV]) == BitArray[(int)eLoai.QTHeThongDV]; }
            set { this.NNYEUTOBENNGOAI = (value ? (this.NNYEUTOBENNGOAI | BitArray[(int)eLoai.QTHeThongDV]) : (this.NNYEUTOBENNGOAI & (~BitArray[(int)eLoai.QTHeThongDV]))); }
        }

        //các yếu tố không đề cập
        public bool CacYTKhongDeCap
        {
            get { return (this.NNKHAC & BitArray[(int)eLoai.CacYTKhongDeCap]) == BitArray[(int)eLoai.CacYTKhongDeCap]; }
            set { this.NNKHAC = (value ? (this.NNKHAC | BitArray[(int)eLoai.CacYTKhongDeCap]) : (this.NNKHAC & (~BitArray[(int)eLoai.CacYTKhongDeCap]))); }
        }

        //NC0
        public bool A
        {
            get { return (this.TONGTHUONGNBNC0 & BitArray[(int)eLoai.A]) == BitArray[(int)eLoai.A]; }
            set { this.TONGTHUONGNBNC0 = (value ? (this.TONGTHUONGNBNC0 | BitArray[(int)eLoai.A]) : (this.TONGTHUONGNBNC0 & (~BitArray[(int)eLoai.A]))); }
        }

        //NC1
        public bool B
        {
            get { return (this.TONGTHUONGNBNC1 & BitArray[(int)eLoai.B]) == BitArray[(int)eLoai.B]; }
            set { this.TONGTHUONGNBNC1 = (value ? (this.TONGTHUONGNBNC1 | BitArray[(int)eLoai.B]) : (this.TONGTHUONGNBNC1 & (~BitArray[(int)eLoai.B]))); }
        }
        public bool C
        {
            get { return (this.TONGTHUONGNBNC1 & BitArray[(int)eLoai.C]) == BitArray[(int)eLoai.C]; }
            set { this.TONGTHUONGNBNC1 = (value ? (this.TONGTHUONGNBNC1 | BitArray[(int)eLoai.C]) : (this.TONGTHUONGNBNC1 & (~BitArray[(int)eLoai.C]))); }
        }
        public bool D
        {
            get { return (this.TONGTHUONGNBNC1 & BitArray[(int)eLoai.D]) == BitArray[(int)eLoai.D]; }
            set { this.TONGTHUONGNBNC1 = (value ? (this.TONGTHUONGNBNC1 | BitArray[(int)eLoai.D]) : (this.TONGTHUONGNBNC1 & (~BitArray[(int)eLoai.D]))); }
        }

        //NC2
        public bool E
        {
            get { return (this.TONGTHUONGNBNC2 & BitArray[(int)eLoai.E]) == BitArray[(int)eLoai.E]; }
            set { this.TONGTHUONGNBNC2 = (value ? (this.TONGTHUONGNBNC2 | BitArray[(int)eLoai.E]) : (this.TONGTHUONGNBNC2 & (~BitArray[(int)eLoai.E]))); }
        }
        public bool F
        {
            get { return (this.TONGTHUONGNBNC2 & BitArray[(int)eLoai.F]) == BitArray[(int)eLoai.F]; }
            set { this.TONGTHUONGNBNC2 = (value ? (this.TONGTHUONGNBNC2 | BitArray[(int)eLoai.F]) : (this.TONGTHUONGNBNC2 & (~BitArray[(int)eLoai.F]))); }
        }

        //NC3
        public bool G
        {
            get { return (this.TONGTHUONGNBNC3 & BitArray[(int)eLoai.G]) == BitArray[(int)eLoai.G]; }
            set { this.TONGTHUONGNBNC3 = (value ? (this.TONGTHUONGNBNC3 | BitArray[(int)eLoai.G]) : (this.TONGTHUONGNBNC3 & (~BitArray[(int)eLoai.G]))); }
        }
        public bool H
        {
            get { return (this.TONGTHUONGNBNC3 & BitArray[(int)eLoai.H]) == BitArray[(int)eLoai.H]; }
            set { this.TONGTHUONGNBNC3 = (value ? (this.TONGTHUONGNBNC3 | BitArray[(int)eLoai.H]) : (this.TONGTHUONGNBNC3 & (~BitArray[(int)eLoai.H]))); }
        }
        public bool I
        {
            get { return (this.TONGTHUONGNBNC3 & BitArray[(int)eLoai.I]) == BitArray[(int)eLoai.I]; }
            set { this.TONGTHUONGNBNC3 = (value ? (this.TONGTHUONGNBNC3 | BitArray[(int)eLoai.I]) : (this.TONGTHUONGNBNC3 & (~BitArray[(int)eLoai.I]))); }
        }

        //Trên tổ chức
        public bool TonHaiTaiSan
        {
            get { return (this.DANHGIATRENTOCHUC & BitArray[(int)eLoai.TonHaiTaiSan]) == BitArray[(int)eLoai.TonHaiTaiSan]; }
            set { this.DANHGIATRENTOCHUC = (value ? (this.DANHGIATRENTOCHUC | BitArray[(int)eLoai.TonHaiTaiSan]) : (this.DANHGIATRENTOCHUC & (~BitArray[(int)eLoai.TonHaiTaiSan]))); }
        }
        public bool TangNLPVChoBV
        {
            get { return (this.DANHGIATRENTOCHUC & BitArray[(int)eLoai.TangNLPVChoBV]) == BitArray[(int)eLoai.TangNLPVChoBV]; }
            set { this.DANHGIATRENTOCHUC = (value ? (this.DANHGIATRENTOCHUC | BitArray[(int)eLoai.TangNLPVChoBV]) : (this.DANHGIATRENTOCHUC & (~BitArray[(int)eLoai.TangNLPVChoBV]))); }
        }
        public bool QTCuaTT
        {
            get { return (this.DANHGIATRENTOCHUC & BitArray[(int)eLoai.QTCuaTT]) == BitArray[(int)eLoai.QTCuaTT]; }
            set { this.DANHGIATRENTOCHUC = (value ? (this.DANHGIATRENTOCHUC | BitArray[(int)eLoai.QTCuaTT]) : (this.DANHGIATRENTOCHUC & (~BitArray[(int)eLoai.QTCuaTT]))); }
        }
        public bool KhieuNaiCuaNB
        {
            get { return (this.DANHGIATRENTOCHUC & BitArray[(int)eLoai.KhieuNaiCuaNB]) == BitArray[(int)eLoai.KhieuNaiCuaNB]; }
            set { this.DANHGIATRENTOCHUC = (value ? (this.DANHGIATRENTOCHUC | BitArray[(int)eLoai.KhieuNaiCuaNB]) : (this.DANHGIATRENTOCHUC & (~BitArray[(int)eLoai.KhieuNaiCuaNB]))); }
        }
        public bool TonHaiDanhTieng
        {
            get { return (this.DANHGIATRENTOCHUC & BitArray[(int)eLoai.TonHaiDanhTieng]) == BitArray[(int)eLoai.TonHaiDanhTieng]; }
            set { this.DANHGIATRENTOCHUC = (value ? (this.DANHGIATRENTOCHUC | BitArray[(int)eLoai.TonHaiDanhTieng]) : (this.DANHGIATRENTOCHUC & (~BitArray[(int)eLoai.TonHaiDanhTieng]))); }
        }
        public bool CanThiepCuaPL
        {
            get { return (this.DANHGIATRENTOCHUC & BitArray[(int)eLoai.CanThiepCuaPL]) == BitArray[(int)eLoai.CanThiepCuaPL]; }
            set { this.DANHGIATRENTOCHUC = (value ? (this.DANHGIATRENTOCHUC | BitArray[(int)eLoai.CanThiepCuaPL]) : (this.DANHGIATRENTOCHUC & (~BitArray[(int)eLoai.CanThiepCuaPL]))); }
        }
        public bool Khac
        {
            get { return (this.DANHGIATRENTOCHUC & BitArray[(int)eLoai.Khac]) == BitArray[(int)eLoai.Khac]; }
            set { this.DANHGIATRENTOCHUC = (value ? (this.DANHGIATRENTOCHUC | BitArray[(int)eLoai.Khac]) : (this.DANHGIATRENTOCHUC & (~BitArray[(int)eLoai.Khac]))); }
        }
        public enum eLoai
        {
            KhongCoSuDongYCuaNB = 0,
            KhongTHKhiCoCD = 1,
            THSaiNB = 2,
            THSaiTT = 3,
            BoSotDungCu = 4,
            THSaiVTPhauThuat = 5,
            TVTrongThaiKy = 6,
            TVKhiSinh = 7,
            TVSoSinh = 8,
            NKHuyet = 9,
            ViemPhoi = 10,
            CacLoaiNKKhac = 11,
            NKVetMo = 12,
            NKTietNieu = 13,
            CPSaiThuocTD = 14,
            ThieuThuoc = 15,
            SaiLieuHamLuong = 16,
            SaiThoiGian = 17,
            SaiYLenh = 18,
            BoSotThuoc = 19,
            SaiThuoc = 20,
            SaiNguoiBenh = 21,
            SaiDuongDung = 22,
            PhanUngPhu = 23,
            TruyenMauNham = 24,
            TruyenSaiLieu = 25,
            ThieuThongTinHD = 26,
            LoiThietBi = 27,
            ThieuThietBi = 28,
            TuGayHai = 29,
            QRTDBoiNV = 30,
            QRTDBoiNB = 31,
            XamHaiCoTheBoiNB = 32,
            CoHDTuTu = 33,
            TronVien = 34,
            TaiNan = 35,
            BiHuHong = 36,
            ThieuHoacKhongPH = 37,
            TPHDDCuaDVKB = 38,
            TPHDDCuaNL = 39,
            TPHDDCuaCS = 40,
            TaiLieuThieu = 41,
            TaiLieuKhongRoRang = 42,
            TGChoDoiKeoDai = 43,
            CCHSTLCham = 44,
            NhamHSTL = 45,
            TTHCPhucTap = 46,
            CacSCKhongDuocDeCap = 47,
            NhanThucNV = 48,
            ThucHanhNV = 49,
            ThaiDoNV = 50,
            GiaoTiepNV = 51,
            TamSinhLyNV = 52,
            CacYTXHNV = 53,
            NhanThucNB= 54,
            ThucHanhNB = 55,
            ThaiDoNB = 56,
            GiaoTiepNB = 57,
            TamSinhLyNB = 58,
            CacYTXHNB = 59,
            CoSovatChat = 60,
            KhoangCach = 61,
            DGVeDoAnToan = 62,
            NoiQuy = 63,            
            CacChinhSach = 64,
            TuanThuQT = 65,
            VanHoaToChuc = 66,
            LamViecNhom = 67,
            MoiTruongTuNhien = 68,
            SPCNCSHT = 69,
            QTHeThongDV = 70,
            CacYTKhongDeCap = 71,
            A = 72,
            B = 73,
            C = 74,
            D = 75,
            E = 76,
            F = 77,
            G = 78,
            H = 79,
            I = 80,
            TonHaiTaiSan = 81,
            TangNLPVChoBV = 82,
            QTCuaTT = 83,
            KhieuNaiCuaNB = 84,
            TonHaiDanhTieng = 85,
            CanThiepCuaPL = 86,
            Khac = 87,
        }
    }
}

public class PhieuPhanTichNguyenNhanSuCoParser
{
    public readonly static Dictionary<int, string> TrangThais = new Dictionary<int, string>()
        {
            { (int)PhieuPhanTichNguyenNhanSuCoCls.eTrangThai.Moi, "Mới" },
            { (int)PhieuPhanTichNguyenNhanSuCoCls.eTrangThai.HoanTat, "Hoàn tất" }
        };
    public readonly static Dictionary<int, string> sColorTrangThai = new Dictionary<int, string>()
        {
            { (int)PhieuPhanTichNguyenNhanSuCoCls.eTrangThai.Moi, "<span style=\"background-color:orange;color:white;\" class=\"badge\" >Mới</span>" },
            { (int)PhieuPhanTichNguyenNhanSuCoCls.eTrangThai.HoanTat, "<span style=\"background-color:#71e038cf; color:black;\" class=\"badge\">Hoàn Tất</span>" },
        };
    public readonly static Dictionary<int, string> TraLois = new Dictionary<int, string>()
        {
            { (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.Co, "Có" },
            { (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.Khong, "Không" },
            { (int)PhieuPhanTichNguyenNhanSuCoCls.eTraLoi.KhongGhiNhan, "Không ghi nhận" }
        };
    public static PhieuPhanTichNguyenNhanSuCoCls CreateInstance()
    {
        PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo = new PhieuPhanTichNguyenNhanSuCoCls();
        return OPhieuPhanTichNguyenNhanSuCo;
    }


    public static PhieuPhanTichNguyenNhanSuCoCls ParseFromDataRow(DataRow dr)
    {
        PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo = new PhieuPhanTichNguyenNhanSuCoCls();
        OPhieuPhanTichNguyenNhanSuCo.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OPhieuPhanTichNguyenNhanSuCo.SOBAOCAO = CoreXmlUtility.GetString(dr, "SOBAOCAO", true);
        OPhieuPhanTichNguyenNhanSuCo.NGUOILAP_ID = CoreXmlUtility.GetString(dr, "NGUOILAP_ID", true);
        OPhieuPhanTichNguyenNhanSuCo.CHUCDANH_ID = CoreXmlUtility.GetString(dr, "CHUCDANH_ID", true);
        OPhieuPhanTichNguyenNhanSuCo.THOIGIANLAP = CoreXmlUtility.GetDateOrNull(dr, "THOIGIANLAP", true);
        OPhieuPhanTichNguyenNhanSuCo.MOTASUCO = CoreXmlUtility.GetString(dr, "MOTASUCO", true);
        OPhieuPhanTichNguyenNhanSuCo.THUCHIENQTKT = CoreXmlUtility.GetIntOrNull(dr, "THUCHIENQTKT", true);
        OPhieuPhanTichNguyenNhanSuCo.NHIEMKHUANBENHVIEN = CoreXmlUtility.GetIntOrNull(dr, "NHIEMKHUANBENHVIEN", true);
        OPhieuPhanTichNguyenNhanSuCo.THUOCDICHTRUYEN = CoreXmlUtility.GetIntOrNull(dr, "THUOCDICHTRUYEN", true);
        OPhieuPhanTichNguyenNhanSuCo.CHEPHAMMAU = CoreXmlUtility.GetIntOrNull(dr, "CHEPHAMMAU", true);
        OPhieuPhanTichNguyenNhanSuCo.THIETBIYTE = CoreXmlUtility.GetIntOrNull(dr, "THIETBIYTE", true);
        OPhieuPhanTichNguyenNhanSuCo.HANHVI = CoreXmlUtility.GetIntOrNull(dr, "HANHVI", true);
        OPhieuPhanTichNguyenNhanSuCo.TAINANNGUOIBENH = CoreXmlUtility.GetIntOrNull(dr, "TAINANNGUOIBENH", true);
        OPhieuPhanTichNguyenNhanSuCo.HATANGCOSO = CoreXmlUtility.GetIntOrNull(dr, "HATANGCOSO", true);
        OPhieuPhanTichNguyenNhanSuCo.QLNGUONLUCTC = CoreXmlUtility.GetIntOrNull(dr, "QLNGUONLUCTC", true);
        OPhieuPhanTichNguyenNhanSuCo.HSTHUTUCHANHCHINH = CoreXmlUtility.GetIntOrNull(dr, "HSTHUTUCHANHCHINH", true);
        OPhieuPhanTichNguyenNhanSuCo.KHAC = CoreXmlUtility.GetIntOrNull(dr, "KHAC", true);
        OPhieuPhanTichNguyenNhanSuCo.DTYLDUOCTHUCHIEN = CoreXmlUtility.GetString(dr, "DTYLDUOCTHUCHIEN", true);
        OPhieuPhanTichNguyenNhanSuCo.NNNHANVIEN = CoreXmlUtility.GetIntOrNull(dr, "NNNHANVIEN", true);
        OPhieuPhanTichNguyenNhanSuCo.NNNGUOIBENH = CoreXmlUtility.GetIntOrNull(dr, "NNNGUOIBENH", true);
        OPhieuPhanTichNguyenNhanSuCo.NNMOITRUONGLAMVIEC = CoreXmlUtility.GetIntOrNull(dr, "NNMOITRUONGLAMVIEC", true);
        OPhieuPhanTichNguyenNhanSuCo.NNTOCHUCDICHVU = CoreXmlUtility.GetIntOrNull(dr, "NNTOCHUCDICHVU", true);
        OPhieuPhanTichNguyenNhanSuCo.NNYEUTOBENNGOAI = CoreXmlUtility.GetIntOrNull(dr, "NNYEUTOBENNGOAI", true);
        OPhieuPhanTichNguyenNhanSuCo.NNKHAC = CoreXmlUtility.GetIntOrNull(dr, "NNKHAC", true);
        OPhieuPhanTichNguyenNhanSuCo.KHACPHUCSUCO = CoreXmlUtility.GetString(dr, "KHACPHUCSUCO", true);
        OPhieuPhanTichNguyenNhanSuCo.DEXUATKHUYENCAO = CoreXmlUtility.GetString(dr, "DEXUATKHUYENCAO", true);
        OPhieuPhanTichNguyenNhanSuCo.MOTAKETQUA = CoreXmlUtility.GetString(dr, "MOTAKETQUA", true);
        OPhieuPhanTichNguyenNhanSuCo.THAOLUANKHUYENCAO = CoreXmlUtility.GetIntOrNull(dr, "THAOLUANKHUYENCAO", true);
        OPhieuPhanTichNguyenNhanSuCo.PHUHOPKHUYENCAO = CoreXmlUtility.GetIntOrNull(dr, "PHUHOPKHUYENCAO", true);
        OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC0 = CoreXmlUtility.GetIntOrNull(dr, "TONGTHUONGNBNC0", true);
        OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC1 = CoreXmlUtility.GetIntOrNull(dr, "TONGTHUONGNBNC1", true);
        OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC2 = CoreXmlUtility.GetIntOrNull(dr, "TONGTHUONGNBNC2", true);
        OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC3 = CoreXmlUtility.GetIntOrNull(dr, "TONGTHUONGNBNC3", true);
        OPhieuPhanTichNguyenNhanSuCo.DANHGIATRENTOCHUC = CoreXmlUtility.GetIntOrNull(dr, "DANHGIATRENTOCHUC", true);
        OPhieuPhanTichNguyenNhanSuCo.TRANGTHAI = CoreXmlUtility.GetIntOrNull(dr, "TRANGTHAI", true);
        return OPhieuPhanTichNguyenNhanSuCo;
    }

    public static PhieuPhanTichNguyenNhanSuCoCls[] ParseFromDataTable(DataTable dtTable)
    {
        PhieuPhanTichNguyenNhanSuCoCls[] PhieuPhanTichNguyenNhanSuCos = new PhieuPhanTichNguyenNhanSuCoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            PhieuPhanTichNguyenNhanSuCos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return PhieuPhanTichNguyenNhanSuCos;
    }


    public static PhieuPhanTichNguyenNhanSuCoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        PhieuPhanTichNguyenNhanSuCoCls[] PhieuPhanTichNguyenNhanSuCos = ParseFromDataTable(ds.Tables[0]);
        return PhieuPhanTichNguyenNhanSuCos;
    }


    public static PhieuPhanTichNguyenNhanSuCoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OPhieuPhanTichNguyenNhanSuCo;
    }


    public static XmlCls GetXml(PhieuPhanTichNguyenNhanSuCoCls[] PhieuPhanTichNguyenNhanSuCos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("SOBAOCAO");
        ds.Tables["info"].Columns.Add("NGUOILAP_ID");
        ds.Tables["info"].Columns.Add("CHUCDANH_ID");
        ds.Tables["info"].Columns.Add("THOIGIANLAP", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("MOTASUCO");
        ds.Tables["info"].Columns.Add("THUCHIENQTKT", typeof(int?));
        ds.Tables["info"].Columns.Add("NHIEMKHUANBENHVIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("THUOCDICHTRUYEN", typeof(int?));
        ds.Tables["info"].Columns.Add("CHEPHAMMAU", typeof(int?));
        ds.Tables["info"].Columns.Add("THIETBIYTE", typeof(int?));
        ds.Tables["info"].Columns.Add("HANHVI", typeof(int?));
        ds.Tables["info"].Columns.Add("TAINANNGUOIBENH", typeof(int?));
        ds.Tables["info"].Columns.Add("HATANGCOSO", typeof(int?));
        ds.Tables["info"].Columns.Add("QLNGUONLUCTC", typeof(int?));
        ds.Tables["info"].Columns.Add("HSTHUTUCHANHCHINH", typeof(int?));
        ds.Tables["info"].Columns.Add("KHAC", typeof(int?));
        ds.Tables["info"].Columns.Add("DTYLDUOCTHUCHIEN");
        ds.Tables["info"].Columns.Add("NNNHANVIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("NNNGUOIBENH", typeof(int?));
        ds.Tables["info"].Columns.Add("NNMOITRUONGLAMVIEC", typeof(int?));
        ds.Tables["info"].Columns.Add("NNTOCHUCDICHVU", typeof(int?));
        ds.Tables["info"].Columns.Add("NNYEUTOBENNGOAI", typeof(int?));
        ds.Tables["info"].Columns.Add("NNKHAC", typeof(int?));
        ds.Tables["info"].Columns.Add("KHACPHUCSUCO");
        ds.Tables["info"].Columns.Add("DEXUATKHUYENCAO");
        ds.Tables["info"].Columns.Add("MOTAKETQUA");
        ds.Tables["info"].Columns.Add("THAOLUANKHUYENCAO", typeof(int?));
        ds.Tables["info"].Columns.Add("PHUHOPKHUYENCAO", typeof(int?));
        ds.Tables["info"].Columns.Add("TONGTHUONGNBNC0", typeof(int?));
        ds.Tables["info"].Columns.Add("TONGTHUONGNBNC1", typeof(int?));
        ds.Tables["info"].Columns.Add("TONGTHUONGNBNC2", typeof(int?));
        ds.Tables["info"].Columns.Add("TONGTHUONGNBNC3", typeof(int?));
        ds.Tables["info"].Columns.Add("DANHGIATRENTOCHUC", typeof(int?));
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        for (int iIndex = 0; iIndex < PhieuPhanTichNguyenNhanSuCos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                PhieuPhanTichNguyenNhanSuCos[iIndex].ID,
                PhieuPhanTichNguyenNhanSuCos[iIndex].SOBAOCAO,
                PhieuPhanTichNguyenNhanSuCos[iIndex].NGUOILAP_ID,
                PhieuPhanTichNguyenNhanSuCos[iIndex].CHUCDANH_ID,
                PhieuPhanTichNguyenNhanSuCos[iIndex].THOIGIANLAP,
                PhieuPhanTichNguyenNhanSuCos[iIndex].MOTASUCO,
                PhieuPhanTichNguyenNhanSuCos[iIndex].THUCHIENQTKT,
                PhieuPhanTichNguyenNhanSuCos[iIndex].NHIEMKHUANBENHVIEN,
                PhieuPhanTichNguyenNhanSuCos[iIndex].THUOCDICHTRUYEN,
                PhieuPhanTichNguyenNhanSuCos[iIndex].CHEPHAMMAU,
                PhieuPhanTichNguyenNhanSuCos[iIndex].THIETBIYTE,
                PhieuPhanTichNguyenNhanSuCos[iIndex].HANHVI,
                PhieuPhanTichNguyenNhanSuCos[iIndex].TAINANNGUOIBENH,
                PhieuPhanTichNguyenNhanSuCos[iIndex].HATANGCOSO,
                PhieuPhanTichNguyenNhanSuCos[iIndex].QLNGUONLUCTC,
                PhieuPhanTichNguyenNhanSuCos[iIndex].HSTHUTUCHANHCHINH,
                PhieuPhanTichNguyenNhanSuCos[iIndex].KHAC,
                PhieuPhanTichNguyenNhanSuCos[iIndex].DTYLDUOCTHUCHIEN,
                PhieuPhanTichNguyenNhanSuCos[iIndex].NNNHANVIEN,
                PhieuPhanTichNguyenNhanSuCos[iIndex].NNNGUOIBENH,
                PhieuPhanTichNguyenNhanSuCos[iIndex].NNMOITRUONGLAMVIEC,
                PhieuPhanTichNguyenNhanSuCos[iIndex].NNTOCHUCDICHVU,
                PhieuPhanTichNguyenNhanSuCos[iIndex].NNYEUTOBENNGOAI,
                PhieuPhanTichNguyenNhanSuCos[iIndex].NNKHAC,
                PhieuPhanTichNguyenNhanSuCos[iIndex].KHACPHUCSUCO,
                PhieuPhanTichNguyenNhanSuCos[iIndex].DEXUATKHUYENCAO,
                PhieuPhanTichNguyenNhanSuCos[iIndex].MOTAKETQUA,
                PhieuPhanTichNguyenNhanSuCos[iIndex].THAOLUANKHUYENCAO,
                PhieuPhanTichNguyenNhanSuCos[iIndex].PHUHOPKHUYENCAO,
                PhieuPhanTichNguyenNhanSuCos[iIndex].TONGTHUONGNBNC0,
                PhieuPhanTichNguyenNhanSuCos[iIndex].TONGTHUONGNBNC1,
                PhieuPhanTichNguyenNhanSuCos[iIndex].TONGTHUONGNBNC2,
                PhieuPhanTichNguyenNhanSuCos[iIndex].TONGTHUONGNBNC3,
                PhieuPhanTichNguyenNhanSuCos[iIndex].DANHGIATRENTOCHUC,
                PhieuPhanTichNguyenNhanSuCos[iIndex].TRANGTHAI
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo)
    {
        DataSet ds = new DataSet();
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("SOBAOCAO");
        ds.Tables["info"].Columns.Add("NGUOILAP_ID");
        ds.Tables["info"].Columns.Add("CHUCDANH_ID");
        ds.Tables["info"].Columns.Add("THOIGIANLAP", typeof(DateTime?));
        ds.Tables["info"].Columns.Add("MOTASUCO");
        ds.Tables["info"].Columns.Add("THUCHIENQTKT", typeof(int?));
        ds.Tables["info"].Columns.Add("NHIEMKHUANBENHVIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("THUOCDICHTRUYEN", typeof(int?));
        ds.Tables["info"].Columns.Add("CHEPHAMMAU", typeof(int?));
        ds.Tables["info"].Columns.Add("THIETBIYTE", typeof(int?));
        ds.Tables["info"].Columns.Add("HANHVI", typeof(int?));
        ds.Tables["info"].Columns.Add("TAINANNGUOIBENH", typeof(int?));
        ds.Tables["info"].Columns.Add("HATANGCOSO", typeof(int?));
        ds.Tables["info"].Columns.Add("QLNGUONLUCTC", typeof(int?));
        ds.Tables["info"].Columns.Add("HSTHUTUCHANHCHINH", typeof(int?));
        ds.Tables["info"].Columns.Add("KHAC", typeof(int?));
        ds.Tables["info"].Columns.Add("DTYLDUOCTHUCHIEN");
        ds.Tables["info"].Columns.Add("NNNHANVIEN", typeof(int?));
        ds.Tables["info"].Columns.Add("NNNGUOIBENH", typeof(int?));
        ds.Tables["info"].Columns.Add("NNMOITRUONGLAMVIEC", typeof(int?));
        ds.Tables["info"].Columns.Add("NNTOCHUCDICHVU", typeof(int?));
        ds.Tables["info"].Columns.Add("NNYEUTOBENNGOAI", typeof(int?));
        ds.Tables["info"].Columns.Add("NNKHAC", typeof(int?));
        ds.Tables["info"].Columns.Add("KHACPHUCSUCO");
        ds.Tables["info"].Columns.Add("DEXUATKHUYENCAO");
        ds.Tables["info"].Columns.Add("MOTAKETQUA");
        ds.Tables["info"].Columns.Add("THAOLUANKHUYENCAO", typeof(int?));
        ds.Tables["info"].Columns.Add("PHUHOPKHUYENCAO", typeof(int?));
        ds.Tables["info"].Columns.Add("TONGTHUONGNBNC0", typeof(int?));
        ds.Tables["info"].Columns.Add("TONGTHUONGNBNC1", typeof(int?));
        ds.Tables["info"].Columns.Add("TONGTHUONGNBNC2", typeof(int?));
        ds.Tables["info"].Columns.Add("TONGTHUONGNBNC3", typeof(int?));
        ds.Tables["info"].Columns.Add("DANHGIATRENTOCHUC", typeof(int?));
        ds.Tables["info"].Columns.Add("TRANGTHAI", typeof(int));
        ds.Tables["info"].Rows.Add(new object[]
        {
                OPhieuPhanTichNguyenNhanSuCo.ID,
                OPhieuPhanTichNguyenNhanSuCo.SOBAOCAO,
                OPhieuPhanTichNguyenNhanSuCo.NGUOILAP_ID,
                OPhieuPhanTichNguyenNhanSuCo.CHUCDANH_ID,
                OPhieuPhanTichNguyenNhanSuCo.THOIGIANLAP,
                OPhieuPhanTichNguyenNhanSuCo.MOTASUCO,
                OPhieuPhanTichNguyenNhanSuCo.THUCHIENQTKT,
                OPhieuPhanTichNguyenNhanSuCo.NHIEMKHUANBENHVIEN,
                OPhieuPhanTichNguyenNhanSuCo.THUOCDICHTRUYEN,
                OPhieuPhanTichNguyenNhanSuCo.CHEPHAMMAU,
                OPhieuPhanTichNguyenNhanSuCo.THIETBIYTE,
                OPhieuPhanTichNguyenNhanSuCo.HANHVI,
                OPhieuPhanTichNguyenNhanSuCo.TAINANNGUOIBENH,
                OPhieuPhanTichNguyenNhanSuCo.HATANGCOSO,
                OPhieuPhanTichNguyenNhanSuCo.QLNGUONLUCTC,
                OPhieuPhanTichNguyenNhanSuCo.HSTHUTUCHANHCHINH,
                OPhieuPhanTichNguyenNhanSuCo.KHAC,
                OPhieuPhanTichNguyenNhanSuCo.DTYLDUOCTHUCHIEN,
                OPhieuPhanTichNguyenNhanSuCo.NNNHANVIEN,
                OPhieuPhanTichNguyenNhanSuCo.NNNGUOIBENH,
                OPhieuPhanTichNguyenNhanSuCo.NNMOITRUONGLAMVIEC,
                OPhieuPhanTichNguyenNhanSuCo.NNTOCHUCDICHVU,
                OPhieuPhanTichNguyenNhanSuCo.NNYEUTOBENNGOAI,
                OPhieuPhanTichNguyenNhanSuCo.NNKHAC,
                OPhieuPhanTichNguyenNhanSuCo.KHACPHUCSUCO,
                OPhieuPhanTichNguyenNhanSuCo.DEXUATKHUYENCAO,
                OPhieuPhanTichNguyenNhanSuCo.MOTAKETQUA,
                OPhieuPhanTichNguyenNhanSuCo.THAOLUANKHUYENCAO,
                OPhieuPhanTichNguyenNhanSuCo.PHUHOPKHUYENCAO,
                OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC0,
                OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC1,
                OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC2,
                OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC3,
                OPhieuPhanTichNguyenNhanSuCo.DANHGIATRENTOCHUC,
                OPhieuPhanTichNguyenNhanSuCo.TRANGTHAI
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
