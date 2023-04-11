using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IHinhAnhProcess
    {
        string ServiceId { get; }
        HinhAnhCls[] Reading(ActionSqlParamCls ActionSqlParam, HinhAnhFilterCls OHinhAnhFilter);
        HinhAnhCls[] PageReading(ActionSqlParamCls ActionSqlParam, HinhAnhFilterCls OHinhAnhFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, HinhAnhCls OHinhAnh);
        void Save(ActionSqlParamCls ActionSqlParam, string HinhAnhId, HinhAnhCls OHinhAnh);
        void Delete(ActionSqlParamCls ActionSqlParam, string HinhAnhId);
        HinhAnhCls CreateModel(ActionSqlParamCls ActionSqlParam, string HinhAnhId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string HinhAnhId);
    }

    public class HinhAnhTemplate : IHinhAnhProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual HinhAnhCls[] Reading(ActionSqlParamCls ActionSqlParam, HinhAnhFilterCls OHinhAnhFilter) { return null; }
        public virtual HinhAnhCls[] PageReading(ActionSqlParamCls ActionSqlParam, HinhAnhFilterCls OHinhAnhFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, HinhAnhCls OHinhAnh) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string HinhAnhId, HinhAnhCls OHinhAnh) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string HinhAnhId) { }
        public virtual HinhAnhCls CreateModel(ActionSqlParamCls ActionSqlParam, string HinhAnhId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string HinhAnhId) { return null; }
    }
}
