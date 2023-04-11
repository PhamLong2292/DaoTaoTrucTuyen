using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IPhieuDanhGiaChatLuongDaoTaoProcess
    {
        string ServiceId { get; }
        PhieuDanhGiaChatLuongDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter);
        void Add(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao);
        void Save(ActionSqlParamCls ActionSqlParam, string PhieuDanhGiaChatLuongDaoTaoId, PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao);
        void Delete(ActionSqlParamCls ActionSqlParam, string PhieuDanhGiaChatLuongDaoTaoId);
        PhieuDanhGiaChatLuongDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string PhieuDanhGiaChatLuongDaoTaoId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string PhieuDanhGiaChatLuongDaoTaoId);
        long Count(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter);
        PhieuDanhGiaChatLuongDaoTaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter, ref long recordTotal);
    }

    public class PhieuDanhGiaChatLuongDaoTaoTemplate : IPhieuDanhGiaChatLuongDaoTaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual PhieuDanhGiaChatLuongDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string PhieuDanhGiaChatLuongDaoTaoId, PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string PhieuDanhGiaChatLuongDaoTaoId) { }
        public virtual PhieuDanhGiaChatLuongDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string PhieuDanhGiaChatLuongDaoTaoId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string PhieuDanhGiaChatLuongDaoTaoId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter) { return 0; }
        public virtual PhieuDanhGiaChatLuongDaoTaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter, ref long recordTotal) { return null; }
    }
}
