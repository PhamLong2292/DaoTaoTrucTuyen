using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Model;

namespace OneTSQ.Common
{
    public class TuVanCaBenh
    {
        public enum ePermission
        {
            Xem = 0,
            TiepNhan = 1,
            LapLich = 2,
            DuyetHoiChan = 3
        }
        public enum eTacVu
        {
            TuChoiTiepNhan = CaBenhCls.eTrangThai.TuChoiTiepNhan,
            ChuyenLapLich = CaBenhCls.eTrangThai.ChoLapLich,
            ChuyenHoiChan = CaBenhCls.eTrangThai.ChoHoiChan,
            ThuHoiTiepNhan = CaBenhCls.eTrangThai.ChoTiepNhan
        }
        public readonly static Dictionary<int, string> TacVus = new Dictionary<int, string>()
        {
            { (int)eTacVu.TuChoiTiepNhan, "từ chối tiếp nhận" },
            { (int)eTacVu.ChuyenLapLich, "chuyển lập lịch" },
            { (int)eTacVu.ChuyenHoiChan, "chuyển hội chẩn" },
            { (int)eTacVu.ThuHoiTiepNhan, "Thu hồi tiếp nhận" }
        };
    }
}
