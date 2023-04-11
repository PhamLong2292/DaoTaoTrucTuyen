using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IPhieuBaoCaoSuCoYKhoaProcess
    {
        string ServiceId { get; }
        PhieuBaoCaoSuCoYKhoaCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter);
        PhieuBaoCaoSuCoYKhoaCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoSuCoYKhoaCls OPhieuBaoCaoSuCoYKhoa);
        void Save(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoSuCoYKhoaId, PhieuBaoCaoSuCoYKhoaCls OPhieuBaoCaoSuCoYKhoa);
        void Delete(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoSuCoYKhoaId);
        PhieuBaoCaoSuCoYKhoaCls CreateModel(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoSuCoYKhoaId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoSuCoYKhoaId);
        long Count(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter);
    }

    public class PhieuBaoCaoSuCoYKhoaTemplate : IPhieuBaoCaoSuCoYKhoaProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual PhieuBaoCaoSuCoYKhoaCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter) { return null; }
        public virtual PhieuBaoCaoSuCoYKhoaCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoSuCoYKhoaCls OPhieuBaoCaoSuCoYKhoa) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoSuCoYKhoaId, PhieuBaoCaoSuCoYKhoaCls OPhieuBaoCaoSuCoYKhoa) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoSuCoYKhoaId) { }
        public virtual PhieuBaoCaoSuCoYKhoaCls CreateModel(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoSuCoYKhoaId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoSuCoYKhoaId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoSuCoYKhoaFilterCls OPhieuBaoCaoSuCoYKhoaFilter) { return 0; }
    }
}
