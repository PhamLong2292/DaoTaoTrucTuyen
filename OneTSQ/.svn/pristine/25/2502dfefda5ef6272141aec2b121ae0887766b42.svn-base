using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface ILapLichTepDinhKemProcess
    {
        string ServiceId { get; }
        LapLichTepDinhKemCls[] Reading(ActionSqlParamCls ActionSqlParam, LapLichTepDinhKemFilterCls OLapLichTepDinhKemFilter);
        void Add(ActionSqlParamCls ActionSqlParam, LapLichTepDinhKemCls OLapLichTepDinhKem);
        void Save(ActionSqlParamCls ActionSqlParam, string LapLichTepDinhKemId, LapLichTepDinhKemCls OLapLichTepDinhKem);
        void Delete(ActionSqlParamCls ActionSqlParam, string LapLichTepDinhKemId);
        LapLichTepDinhKemCls CreateModel(ActionSqlParamCls ActionSqlParam, string LapLichTepDinhKemId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string LapLichTepDinhKemId);
    }

    public class LapLichTepDinhKemTemplate : ILapLichTepDinhKemProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual LapLichTepDinhKemCls[] Reading(ActionSqlParamCls ActionSqlParam, LapLichTepDinhKemFilterCls OLapLichTepDinhKemFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, LapLichTepDinhKemCls OLapLichTepDinhKem) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string LapLichTepDinhKemId, LapLichTepDinhKemCls OLapLichTepDinhKem) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string LapLichTepDinhKemId) { }
        public virtual LapLichTepDinhKemCls CreateModel(ActionSqlParamCls ActionSqlParam, string LapLichTepDinhKemId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string LapLichTepDinhKemId) { return null; }
    }
}
