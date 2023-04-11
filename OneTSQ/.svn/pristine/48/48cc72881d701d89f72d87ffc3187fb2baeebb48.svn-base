using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Model;

namespace OneTSQ.Common
{
    public class ThamVanCaBenh
    {
        public enum eTacVu
        {
            DeNghi = CaBenhCls.eTrangThai.ChoDuyetDeNghi,
            DuyetDeNghi = CaBenhCls.eTrangThai.ChoTiepNhan,
            TuChoiDeNghi = CaBenhCls.eTrangThai.TuChoiDeNghi
        }
        public enum ePermission
        {
            Xem = 0,
            Them = 1,
            Sua = 2,
            Xoa = 3,
            DeNghi = 4,
            DuyetDeNghi = 5
        }
        public readonly static Dictionary<int, string> TacVus = new Dictionary<int, string>()
        {
            { (int)eTacVu.DeNghi, "đề nghị" },
            { (int)eTacVu.DuyetDeNghi, "duyệt đề nghị" },
            { (int)eTacVu.TuChoiDeNghi, "từ chối đề nghị" }
        };
    }
}
