using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface ITaiLieuDinhKemProcess
    {
        string ServiceId { get; }
        TaiLieuDinhKemCls[] Reading(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter);
        TaiLieuDinhKemCls[] PageReading(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemCls OTaiLieuDinhKem);
        void Save(ActionSqlParamCls ActionSqlParam, string TaiLieuDinhKemId, TaiLieuDinhKemCls OTaiLieuDinhKem);
        void Delete(ActionSqlParamCls ActionSqlParam, string TaiLieuDinhKemId);
        TaiLieuDinhKemCls CreateModel(ActionSqlParamCls ActionSqlParam, string TaiLieuDinhKemId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string TaiLieuDinhKemId);
        long Count(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter);
    }

    public class TaiLieuDinhKemTemplate : ITaiLieuDinhKemProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual TaiLieuDinhKemCls[] Reading(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter) { return null; }
        public virtual TaiLieuDinhKemCls[] PageReading(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemCls OTaiLieuDinhKem) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string TaiLieuDinhKemId, TaiLieuDinhKemCls OTaiLieuDinhKem) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string TaiLieuDinhKemId) { }
        public virtual TaiLieuDinhKemCls CreateModel(ActionSqlParamCls ActionSqlParam, string TaiLieuDinhKemId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string TaiLieuDinhKemId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter) { return 0; }
    }
}
