using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IKetQuaXetNghiemChiTietProcess
    {
        string ServiceId { get; }
        KetQuaXetNghiemChiTietCls[] Reading(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter);
        void Add(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet);
        void Save(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet);
        void Delete(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId);
        KetQuaXetNghiemChiTietCls CreateModel(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId);
        string Duplicate(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId);
        long Count(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter);
        KetQuaXetNghiemChiTietCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter, int PageIndex, int PageSize);
    }

    public class KetQuaXetNghiemChiTietTemplate : IKetQuaXetNghiemChiTietProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual KetQuaXetNghiemChiTietCls[] Reading(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId) { }
        public virtual KetQuaXetNghiemChiTietCls CreateModel(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string KetQuaXetNghiemChiTietId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter) { return 0; }
        public virtual KetQuaXetNghiemChiTietCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter, int PageIndex, int PageSize) { return null; }
    }
}
