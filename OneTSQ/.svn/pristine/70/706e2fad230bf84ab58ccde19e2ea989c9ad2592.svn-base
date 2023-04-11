using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDM_ChuyenKhoaDaoTaoTtProcess
    {
        string ServiceId { get; }
        DM_ChuyenKhoaDaoTaoTtCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter);
        long Count(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter);
        DM_ChuyenKhoaDaoTaoTtCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt);
        void Save(ActionSqlParamCls ActionSqlParam, string ChuyenKhoaDaoTaoTtId, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt);
        void Delete(ActionSqlParamCls ActionSqlParam, string ChuyenKhoaDaoTaoTtId);
        DM_ChuyenKhoaDaoTaoTtCls CreateModel(ActionSqlParamCls ActionSqlParam, string ChuyenKhoaDaoTaoTtId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string ChuyenKhoaDaoTaoTtId);
        DM_ChuyenKhoaDaoTaoTtCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter, ref int recordTotal);
        DM_ChuyenKhoaDaoTaoTtCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaChuyenKhoaDaoTaoTt);
    }

    public class DM_ChuyenKhoaDaoTaoTtTemplate : IDM_ChuyenKhoaDaoTaoTtProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_ChuyenKhoaDaoTaoTtCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter) { return 0; }
        public virtual DM_ChuyenKhoaDaoTaoTtCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string ChuyenKhoaDaoTaoTtId, DM_ChuyenKhoaDaoTaoTtCls OChuyenKhoaDaoTaoTt) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string ChuyenKhoaDaoTaoTtId) { }
        public virtual DM_ChuyenKhoaDaoTaoTtCls CreateModel(ActionSqlParamCls ActionSqlParam, string ChuyenKhoaDaoTaoTtId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string ChuyenKhoaDaoTaoTtId) { return null; }
        public virtual DM_ChuyenKhoaDaoTaoTtCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_ChuyenKhoaDaoTaoTtFilterCls OChuyenKhoaDaoTaoTtFilter, ref int recordTotal) { return null; }
        public virtual DM_ChuyenKhoaDaoTaoTtCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaChuyenKhoaDaoTaoTt) { return null; }
    }
}
