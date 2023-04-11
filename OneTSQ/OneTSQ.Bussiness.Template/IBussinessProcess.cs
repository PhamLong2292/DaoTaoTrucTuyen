﻿using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Bussiness.Template
{
    public interface IBussinessProcess
    {
        string Id { get; }
        string ServiceName { get; }

        #region Danh mục       
        ICommonProcess CreateCommonProcess();
        ICaBenhProcess CreateCaBenhProcess();
        IHinhAnhProcess CreateHinhAnhProcess();
        ITaiLieuProcess CreateTaiLieuProcess();
        ITepTinProcess CreateTepTinProcess();
        IYKienChuyenGiaProcess CreateYKienChuyenGiaProcess();
        ITepDinhKemProcess CreateTepDinhKemProcess();
        IBinhLuanHinhAnhProcess CreateBinhLuanHinhAnhProcess();
        ITepDinhKemBlHinhAnhProcess CreateTepDinhKemBlHinhAnhProcess();
        IBienBanHoiChanProcess CreateBienBanHoiChanProcess();
        IChuyenGiaHoiChanProcess CreateChuyenGiaHoiChanProcess();
        IButPheProcess CreateButPheProcess();
        ILichHoiChanProcess CreateLichHoiChanProcess();
        ILapLichTepDinhKemProcess CreateLapLichTepDinhKemProcess();
        ILapLichThanhVienHoiChanProcess CreateLapLichThanhVienHoiChanProcess();
        IBacSyProcess CreateBacSyProcess();
        IBacSyOwnerUserProcess CreateBacSyOwnerUserProcess();
        IBienBanHoiChanToanLichProcess CreateBienBanHoiChanToanLichProcess();
        IBienBanHoiChanToanLichBacSyProcess CreateBienBanHoiChanToanLichBacSyProcess();
        INoiDungHoiChanProcess CreateNoiDungHoiChanProcess();
        IKetQuaXetNghiemProcess CreateKetQuaXetNghiemProcess();
        IKetQuaXetNghiemChiTietProcess CreateKetQuaXetNghiemChiTietProcess();
        IDM_ChuyenKhoaDaoTaoTtProcess CreateChuyenKhoaDaoTaoTtProcess();
        IDM_TieuChiThoiGianDaoTaoTtProcess CreateTieuChiThoiGianDaoTaoTtProcess();
        IDM_TieuChiThoiLuongDaoTaoTtProcess CreateTieuChiThoiLuongDaoTaoTtProcess();
        IDM_TrangThietBiTruyenHinhTtProcess CreateTrangThietBiTruyenHinhTtProcess();
        IChatLuongHoatDongTtbProcess CreateChatLuongHoatDongTtbProcess();
        IDanhGiaThoiGianBuoiBaoCaoProcess CreateDanhGiaThoiGianBuoiBaoCaoProcess();
        IDanhGiaThoiLuongBuoiBaoCaoProcess CreateDanhGiaThoiLuongBuoiBaoCaoProcess();
        IDoHieuQuaGiangDayProcess CreateDoHieuQuaGiangDayProcess();
        IMucDoPhongPhuBaiBaoCaoProcess CreateMucDoPhongPhuBaiBaoCaoProcess();
        IMucDoYNghiaChuongTrinhDaoTaoProcess CreateMucDoYNghiaChuongTrinhDaoTaoProcess();
        IPhieuDanhGiaChatLuongDaoTaoProcess CreatePhieuDanhGiaChatLuongDaoTaoProcess();
        IBcDanhGiaChatLuongDaoTaoProcess CreateBcDanhGiaChatLuongDaoTaoProcess();
        IDM_YKienBenhVienProcess CreateYKienBenhVienProcess();
        IDT_DiemDanhLyThuyetProcess CreateDT_DiemDanhLyThuyetProcess();
        IDT_DiemDanhThucHanhProcess CreateDT_DiemDanhThucHanhProcess();
        IDT_HocVienProcess CreateDT_HocVienProcess();
        IDT_KeHoachLopProcess CreateDT_KeHoachLopProcess();
        IDT_KetQuaDaoTaoProcess CreateDT_KetQuaDaoTaoProcess();
        IDT_KhoaHocProcess CreateDT_KhoaHocProcess();
        IDT_LichLyThuyetProcess CreateDT_LichLyThuyetProcess();
        IDT_LichLyThuyetChiTietProcess CreateDT_LichLyThuyetChiTietProcess();
        IDT_LichThucHanhProcess CreateDT_LichThucHanhProcess();
        IDT_LichThucHanhChiTietProcess CreateDT_LichThucHanhChiTietProcess();
        IDT_TaiLieuProcess CreateDT_TaiLieuProcess();
        IDT_VanBangProcess CreateDT_VanBangProcess();
        IDT_LichChuyenGiaoProcess CreateDT_LichChuyenGiaoProcess();
        IDT_LichChuyenGiaoChiTietProcess CreateDT_LichChuyenGiaoChiTietProcess();
        IDT_KetQuaChuyenGiaoProcess CreateDT_KetQuaChuyenGiaoProcess();
        IDM_KyThuatChuyenGiaoProcess CreateKyThuatChuyenGiaoProcess();
        IDT_TaiLieuChuyenGiaoProcess CreateDT_TaiLieuChuyenGiaoProcess();
        IDM_NhomKhoaHocProcess CreateDM_NhomKhoaHocProcess();
        IDM_TenKhoaHocProcess CreateDM_TenKhoaHocProcess();
        IDM_GiayToDiChuyenGiaoProcess CreateDM_GiayToDiChuyenGiaoProcess();
        IDM_TieuChuanThamGiaKhoaHocProcess CreateDM_TieuChuanThamGiaKhoaHocProcess();
        IDT_BcTongKetCongTacDaoTaoProcess CreateDT_BcTongKetCongTacDaoTaoProcess();
        IDT_DaoTaoVienTruongProcess CreateDT_DaoTaoVienTruongProcess();
        IPhieuBaoCaoSuCoYKhoaProcess CreatePhieuBaoCaoSuCoYKhoaProcess();
        IPhieuPhanTichNguyenNhanSuCoProcess CreatePhieuPhanTichNguyenNhanSuCoProcess();
        IPhieuBaoCaoPhanUngCoHaiADRProcess CreatePhieuBaoCaoPhanUngCoHaiADRProcess();
        IThuocADRProcess CreateThuocADRProcess();
        IPhieuKhaoSatBenhVienVeTinhProcess CreatePhieuKhaoSatBenhVienVeTinhProcess();
        IDaoTaoNhanLucProcess CreateDaoTaoNhanLucProcess();
        #endregion
        #region Nghiệp vụ
        IDangKyDeTaiProcess CreateDangKyDeTaiProcess();
        IDeTaiProcess CreateDeTaiProcess();
        IDeCuongProcess CreateDeCuongProcess();
        ILichXetDuyetProcess CreateLichXetDuyetProcess();
        IDanhGiaDeCuong_DeTaiProcess CreateDanhGiaDeCuong_DeTaiProcess();
        IHoiDongXetDuyetProcess CreateHoiDongXetDuyetProcess();
        ICongTacVienDeTaiProcess CreateCongTacVienDeTaiProcess();
        ITaiLieuDinhKemProcess CreateTaiLieuDinhKemProcess();
        #endregion
    }

    public class BussinessProcessTemplate : IBussinessProcess
    {
        public virtual string Id { get { return null; } }


        public virtual string ServiceName { get { return null; } }

        #region Danh mục
        public virtual ICommonProcess CreateCommonProcess() { return null; }
        public virtual ICaBenhProcess CreateCaBenhProcess() { return null; }
        public virtual IHinhAnhProcess CreateHinhAnhProcess() { return null; }
        public virtual ITaiLieuProcess CreateTaiLieuProcess() { return null; }
        public virtual ITepTinProcess CreateTepTinProcess() { return null; }
        public virtual IYKienChuyenGiaProcess CreateYKienChuyenGiaProcess() { return null; }
        public virtual ITepDinhKemProcess CreateTepDinhKemProcess() { return null; }
        public virtual IBinhLuanHinhAnhProcess CreateBinhLuanHinhAnhProcess() { return null; }
        public virtual ITepDinhKemBlHinhAnhProcess CreateTepDinhKemBlHinhAnhProcess() { return null; }
        public virtual IBienBanHoiChanProcess CreateBienBanHoiChanProcess() { return null; }
        public virtual IChuyenGiaHoiChanProcess CreateChuyenGiaHoiChanProcess() { return null; }
        public virtual IButPheProcess CreateButPheProcess() { return null; }
        public virtual ILichHoiChanProcess CreateLichHoiChanProcess() { return null; }
        public virtual ILapLichTepDinhKemProcess CreateLapLichTepDinhKemProcess() { return null; }
        public virtual ILapLichThanhVienHoiChanProcess CreateLapLichThanhVienHoiChanProcess() { return null; }
        public virtual IBacSyProcess CreateBacSyProcess() { return null; }
        public virtual IBacSyOwnerUserProcess CreateBacSyOwnerUserProcess() { return null; }
        public virtual IBienBanHoiChanToanLichProcess CreateBienBanHoiChanToanLichProcess() { return null; }
        public virtual IBienBanHoiChanToanLichBacSyProcess CreateBienBanHoiChanToanLichBacSyProcess() { return null; }
        public virtual INoiDungHoiChanProcess CreateNoiDungHoiChanProcess() { return null; }
        public virtual IKetQuaXetNghiemProcess CreateKetQuaXetNghiemProcess() { return null; }
        public virtual IKetQuaXetNghiemChiTietProcess CreateKetQuaXetNghiemChiTietProcess() { return null; }
        public virtual IDM_ChuyenKhoaDaoTaoTtProcess CreateChuyenKhoaDaoTaoTtProcess() { return null; }
        public virtual IDM_TieuChiThoiGianDaoTaoTtProcess CreateTieuChiThoiGianDaoTaoTtProcess() { return null; }
        public virtual IDM_TieuChiThoiLuongDaoTaoTtProcess CreateTieuChiThoiLuongDaoTaoTtProcess() { return null; }
        public virtual IDM_TrangThietBiTruyenHinhTtProcess CreateTrangThietBiTruyenHinhTtProcess() { return null; }
        public virtual IChatLuongHoatDongTtbProcess CreateChatLuongHoatDongTtbProcess() { return null; }
        public virtual IDanhGiaThoiGianBuoiBaoCaoProcess CreateDanhGiaThoiGianBuoiBaoCaoProcess() { return null; }
        public virtual IDanhGiaThoiLuongBuoiBaoCaoProcess CreateDanhGiaThoiLuongBuoiBaoCaoProcess() { return null; }
        public virtual IDoHieuQuaGiangDayProcess CreateDoHieuQuaGiangDayProcess() { return null; }
        public virtual IMucDoPhongPhuBaiBaoCaoProcess CreateMucDoPhongPhuBaiBaoCaoProcess() { return null; }
        public virtual IMucDoYNghiaChuongTrinhDaoTaoProcess CreateMucDoYNghiaChuongTrinhDaoTaoProcess() { return null; }
        public virtual IPhieuDanhGiaChatLuongDaoTaoProcess CreatePhieuDanhGiaChatLuongDaoTaoProcess() { return null; }
        public virtual IBcDanhGiaChatLuongDaoTaoProcess CreateBcDanhGiaChatLuongDaoTaoProcess() { return null; }
        public virtual IDM_YKienBenhVienProcess CreateYKienBenhVienProcess() { return null; }
        public virtual IDT_DiemDanhLyThuyetProcess CreateDT_DiemDanhLyThuyetProcess() { return null; }
        public virtual IDT_DiemDanhThucHanhProcess CreateDT_DiemDanhThucHanhProcess() { return null; }
        public virtual IDT_HocVienProcess CreateDT_HocVienProcess() { return null; }
        public virtual IDT_KeHoachLopProcess CreateDT_KeHoachLopProcess() { return null; }
        public virtual IDT_KetQuaDaoTaoProcess CreateDT_KetQuaDaoTaoProcess() { return null; }
        public virtual IDT_KhoaHocProcess CreateDT_KhoaHocProcess() { return null; }
        public virtual IDT_LichLyThuyetProcess CreateDT_LichLyThuyetProcess() { return null; }
        public virtual IDT_LichLyThuyetChiTietProcess CreateDT_LichLyThuyetChiTietProcess() { return null; }
        public virtual IDT_LichThucHanhProcess CreateDT_LichThucHanhProcess() { return null; }
        public virtual IDT_LichThucHanhChiTietProcess CreateDT_LichThucHanhChiTietProcess() { return null; }
        public virtual IDT_TaiLieuProcess CreateDT_TaiLieuProcess() { return null; }
        public virtual IDT_VanBangProcess CreateDT_VanBangProcess() { return null; }
        public virtual IDT_LichChuyenGiaoProcess CreateDT_LichChuyenGiaoProcess() { return null; }
        public virtual IDT_LichChuyenGiaoChiTietProcess CreateDT_LichChuyenGiaoChiTietProcess() { return null; }
        public virtual IDT_KetQuaChuyenGiaoProcess CreateDT_KetQuaChuyenGiaoProcess() { return null; }
        public virtual IDM_KyThuatChuyenGiaoProcess CreateKyThuatChuyenGiaoProcess() { return null; }
        public virtual IDT_TaiLieuChuyenGiaoProcess CreateDT_TaiLieuChuyenGiaoProcess() { return null; }
        public virtual IDM_NhomKhoaHocProcess CreateDM_NhomKhoaHocProcess() { return null; }
        public virtual IDM_TenKhoaHocProcess CreateDM_TenKhoaHocProcess() { return null; }
        public virtual IDM_GiayToDiChuyenGiaoProcess CreateDM_GiayToDiChuyenGiaoProcess() { return null; }
        public virtual IDM_TieuChuanThamGiaKhoaHocProcess CreateDM_TieuChuanThamGiaKhoaHocProcess() { return null; }
        public virtual IDT_BcTongKetCongTacDaoTaoProcess CreateDT_BcTongKetCongTacDaoTaoProcess() { return null; }
        public virtual IDT_DaoTaoVienTruongProcess CreateDT_DaoTaoVienTruongProcess() { return null; }
        public virtual IPhieuBaoCaoSuCoYKhoaProcess CreatePhieuBaoCaoSuCoYKhoaProcess() { return null; }
        public virtual IPhieuPhanTichNguyenNhanSuCoProcess CreatePhieuPhanTichNguyenNhanSuCoProcess() { return null; }
        public virtual IPhieuBaoCaoPhanUngCoHaiADRProcess CreatePhieuBaoCaoPhanUngCoHaiADRProcess() { return null; }
        public virtual IThuocADRProcess CreateThuocADRProcess() { return null; }
        public virtual IPhieuKhaoSatBenhVienVeTinhProcess CreatePhieuKhaoSatBenhVienVeTinhProcess() { return null; }
        public virtual IDaoTaoNhanLucProcess CreateDaoTaoNhanLucProcess() { return null; }
        #endregion

        #region Nghiệp vụ
        public virtual IDangKyDeTaiProcess CreateDangKyDeTaiProcess() { return null; }
        public virtual IDeTaiProcess CreateDeTaiProcess() { return null; }
        public virtual IDeCuongProcess CreateDeCuongProcess() { return null; }
        public virtual ILichXetDuyetProcess CreateLichXetDuyetProcess() { return null; }
        public virtual IDanhGiaDeCuong_DeTaiProcess CreateDanhGiaDeCuong_DeTaiProcess() { return null; }
        public virtual IHoiDongXetDuyetProcess CreateHoiDongXetDuyetProcess() { return null; }
        public virtual ICongTacVienDeTaiProcess CreateCongTacVienDeTaiProcess() { return null; }
        public virtual ITaiLieuDinhKemProcess CreateTaiLieuDinhKemProcess() { return null; }

        #endregion

    }
}
 