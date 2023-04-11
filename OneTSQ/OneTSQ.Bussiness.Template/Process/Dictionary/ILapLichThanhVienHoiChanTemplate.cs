using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface ILapLichThanhVienHoiChanProcess
    {
        string ServiceId { get; }
        LapLichThanhVienHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam, LapLichThanhVienHoiChanFilterCls OLapLichThanhVienHoiChanFilter);
        void Add(ActionSqlParamCls ActionSqlParam, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan);
        void Save(ActionSqlParamCls ActionSqlParam, string LapLichThanhVienHoiChanId, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan);
        void Delete(ActionSqlParamCls ActionSqlParam, string LapLichThanhVienHoiChanId);
        LapLichThanhVienHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string LapLichThanhVienHoiChanId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string LapLichThanhVienHoiChanId);
    }

    public class LapLichThanhVienHoiChanTemplate : ILapLichThanhVienHoiChanProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual LapLichThanhVienHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam, LapLichThanhVienHoiChanFilterCls OLapLichThanhVienHoiChanFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string LapLichThanhVienHoiChanId, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string LapLichThanhVienHoiChanId) { }
        public virtual LapLichThanhVienHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string LapLichThanhVienHoiChanId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string LapLichThanhVienHoiChanId) { return null; }
    }
}
