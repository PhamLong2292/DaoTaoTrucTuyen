using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Common
{
    public class CaBenh
    {
        public readonly static Dictionary<int, string> HanhDongs = new Dictionary<int, string>()
        {
            { (int)ThamVanCaBenh.eTacVu.DeNghi, "đề nghị" },
            { (int)ThamVanCaBenh.eTacVu.DuyetDeNghi, "duyệt đề nghị" },
            { (int)ThamVanCaBenh.eTacVu.TuChoiDeNghi, "từ chối đề nghị" },

            { (int)TuVanCaBenh.eTacVu.TuChoiTiepNhan, "từ chối tiếp nhận" },
            { (int)TuVanCaBenh.eTacVu.ChuyenLapLich, "chuyển lập lịch" },
            { (int)TuVanCaBenh.eTacVu.ChuyenHoiChan, "duyệt hội chẩn" },
            { (int)TuVanCaBenh.eTacVu.ThuHoiTiepNhan, "thu hồi tiếp nhận" }
        };
    }
}
