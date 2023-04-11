using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IKetQuaXetNghiemProcess
    {
        string ServiceId { get; }
        KetQuaXetNghiemCls[] Reading(RenderInfoCls ORenderInfo, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter);
        void Add(RenderInfoCls ORenderInfo, KetQuaXetNghiemCls OKetQuaXetNghiem);
        void Save(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId, KetQuaXetNghiemCls OKetQuaXetNghiem);
        void Delete(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId);
        KetQuaXetNghiemCls CreateModel(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId);
        string Duplicate(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId);
        long Count(RenderInfoCls ORenderInfo, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter);
        KetQuaXetNghiemCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter, int PageIndex, int PageSize);
    }

    public class KetQuaXetNghiemTemplate : IKetQuaXetNghiemProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual KetQuaXetNghiemCls[] Reading(RenderInfoCls ORenderInfo, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, KetQuaXetNghiemCls OKetQuaXetNghiem) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId, KetQuaXetNghiemCls OKetQuaXetNghiem) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId) { }
        public virtual KetQuaXetNghiemCls CreateModel(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string KetQuaXetNghiemId) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter) { return 0; }
        public virtual KetQuaXetNghiemCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter, int PageIndex, int PageSize) { return null; }
    }
}
