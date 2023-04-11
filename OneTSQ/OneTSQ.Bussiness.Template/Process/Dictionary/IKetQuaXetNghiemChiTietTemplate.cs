using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IKetQuaXetNghiemChiTietProcess
    {
        string ServiceId { get; }
        KetQuaXetNghiemChiTietCls[] Reading(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter);
        void Add(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet);
        void Save(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemChiTietId, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet);
        void Delete(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemChiTietId);
        KetQuaXetNghiemChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemChiTietId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemChiTietId);
        long Count(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter);
        KetQuaXetNghiemChiTietCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter, int PageIndex, int PageSize);
    }

    public class KetQuaXetNghiemChiTietTemplate : IKetQuaXetNghiemChiTietProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual KetQuaXetNghiemChiTietCls[] Reading(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemChiTietId, KetQuaXetNghiemChiTietCls OKetQuaXetNghiemChiTiet) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemChiTietId) { }
        public virtual KetQuaXetNghiemChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemChiTietId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemChiTietId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter) { return 0; }
        public virtual KetQuaXetNghiemChiTietCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemChiTietFilterCls OKetQuaXetNghiemChiTietFilter, int PageIndex, int PageSize) { return null; }
    }
}
