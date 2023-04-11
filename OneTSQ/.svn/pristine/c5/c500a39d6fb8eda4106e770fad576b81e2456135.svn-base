using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IThuocADRProcess
    {
        string ServiceId { get; }
        ThuocADRCls[] Reading(ActionSqlParamCls ActionSqlParam, ThuocADRFilterCls OThuocADRFilter);
        ThuocADRCls[] PageReading(ActionSqlParamCls ActionSqlParam, ThuocADRFilterCls OThuocADRFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, ThuocADRCls OThuocADR);
        void Save(ActionSqlParamCls ActionSqlParam, string ThuocADRId, ThuocADRCls OThuocADR);
        void Delete(ActionSqlParamCls ActionSqlParam, string ThuocADRId);
        ThuocADRCls CreateModel(ActionSqlParamCls ActionSqlParam, string ThuocADRId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string ThuocADRId);
        long Count(ActionSqlParamCls ActionSqlParam, ThuocADRFilterCls OThuocADRFilter);
    }

    public class ThuocADRTemplate : IThuocADRProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual ThuocADRCls[] Reading(ActionSqlParamCls ActionSqlParam, ThuocADRFilterCls OThuocADRFilter) { return null; }
        public virtual ThuocADRCls[] PageReading(ActionSqlParamCls ActionSqlParam, ThuocADRFilterCls OThuocADRFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, ThuocADRCls OThuocADR) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string ThuocADRId, ThuocADRCls OThuocADR) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string ThuocADRId) { }
        public virtual ThuocADRCls CreateModel(ActionSqlParamCls ActionSqlParam, string ThuocADRId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string ThuocADRId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, ThuocADRFilterCls OThuocADRFilter) { return 0; }
    }
}
