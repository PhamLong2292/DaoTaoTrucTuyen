﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface ILichXetDuyetProcess
    {
        string ServiceId { get; }
        LichXetDuyetCls[] Reading(ActionSqlParamCls ActionSqlParam, LichXetDuyetFilterCls OLichXetDuyetFilter);
        LichXetDuyetCls[] PageReadingDC(ActionSqlParamCls ActionSqlParam, LichXetDuyetFilterCls OLichXetDuyetFilter, ref long recordTotal);
        LichXetDuyetCls[] PageReadingDT(ActionSqlParamCls ActionSqlParam, LichXetDuyetFilterCls OLichXetDuyetFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, LichXetDuyetCls OLichXetDuyet);
        void Save(ActionSqlParamCls ActionSqlParam, string LichXetDuyetId, LichXetDuyetCls OLichXetDuyet);
        void Delete(ActionSqlParamCls ActionSqlParam, string LichXetDuyetId);
        LichXetDuyetCls CreateModel(ActionSqlParamCls ActionSqlParam, string LichXetDuyetId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string LichXetDuyetId);
        long Count(ActionSqlParamCls ActionSqlParam, LichXetDuyetFilterCls OLichXetDuyetFilter);
    }

    public class LichXetDuyetTemplate : ILichXetDuyetProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual LichXetDuyetCls[] Reading(ActionSqlParamCls ActionSqlParam, LichXetDuyetFilterCls OLichXetDuyetFilter) { return null; }
        public virtual LichXetDuyetCls[] PageReadingDC(ActionSqlParamCls ActionSqlParam, LichXetDuyetFilterCls OLichXetDuyetFilter, ref long recordTotal) { return null; }
        public virtual LichXetDuyetCls[] PageReadingDT(ActionSqlParamCls ActionSqlParam, LichXetDuyetFilterCls OLichXetDuyetFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, LichXetDuyetCls OLichXetDuyet) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string LichXetDuyetId, LichXetDuyetCls OLichXetDuyet) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string LichXetDuyetId) { }
        public virtual LichXetDuyetCls CreateModel(ActionSqlParamCls ActionSqlParam, string LichXetDuyetId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string LichXetDuyetId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, LichXetDuyetFilterCls OLichXetDuyetFilter) { return 0; }
    }
}
