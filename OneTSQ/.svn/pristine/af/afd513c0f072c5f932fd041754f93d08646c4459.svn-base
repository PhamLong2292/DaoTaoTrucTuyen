using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IHoiDongXetDuyetProcess
    {
        string ServiceId { get; }
        HoiDongXetDuyetCls[] Reading(ActionSqlParamCls ActionSqlParam, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter);
        HoiDongXetDuyetCls[] PageReading(ActionSqlParamCls ActionSqlParam, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, HoiDongXetDuyetCls OHoiDongXetDuyet);
        void Save(ActionSqlParamCls ActionSqlParam, string HoiDongXetDuyetId, HoiDongXetDuyetCls OHoiDongXetDuyet);
        void Delete(ActionSqlParamCls ActionSqlParam, string HoiDongXetDuyetId);
        HoiDongXetDuyetCls CreateModel(ActionSqlParamCls ActionSqlParam, string HoiDongXetDuyetId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string HoiDongXetDuyetId);
        long Count(ActionSqlParamCls ActionSqlParam, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter);
    }

    public class HoiDongXetDuyetTemplate : IHoiDongXetDuyetProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual HoiDongXetDuyetCls[] Reading(ActionSqlParamCls ActionSqlParam, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter) { return null; }
        public virtual HoiDongXetDuyetCls[] PageReading(ActionSqlParamCls ActionSqlParam, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, HoiDongXetDuyetCls OHoiDongXetDuyet) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string HoiDongXetDuyetId, HoiDongXetDuyetCls OHoiDongXetDuyet) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string HoiDongXetDuyetId) { }
        public virtual HoiDongXetDuyetCls CreateModel(ActionSqlParamCls ActionSqlParam, string HoiDongXetDuyetId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string HoiDongXetDuyetId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter) { return 0; }
    }
}
