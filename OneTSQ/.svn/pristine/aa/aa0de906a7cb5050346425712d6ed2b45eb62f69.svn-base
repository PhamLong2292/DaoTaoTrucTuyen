using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IKetQuaXetNghiemProcess
    {
        string ServiceId { get; }
        KetQuaXetNghiemCls[] Reading(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter);
        void Add(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemCls OKetQuaXetNghiem);
        void Save(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemId, KetQuaXetNghiemCls OKetQuaXetNghiem);
        void Delete(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemId);
        KetQuaXetNghiemCls CreateModel(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemId);
        long Count(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter);
        KetQuaXetNghiemCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter, int PageIndex, int PageSize);
    }

    public class KetQuaXetNghiemTemplate : IKetQuaXetNghiemProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual KetQuaXetNghiemCls[] Reading(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemCls OKetQuaXetNghiem) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemId, KetQuaXetNghiemCls OKetQuaXetNghiem) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemId) { }
        public virtual KetQuaXetNghiemCls CreateModel(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string KetQuaXetNghiemId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter) { return 0; }
        public virtual KetQuaXetNghiemCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, KetQuaXetNghiemFilterCls OKetQuaXetNghiemFilter, int PageIndex, int PageSize) { return null; }
    }
}
