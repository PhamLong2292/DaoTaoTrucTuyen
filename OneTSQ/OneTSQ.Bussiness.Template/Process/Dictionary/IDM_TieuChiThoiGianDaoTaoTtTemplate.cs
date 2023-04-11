using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDM_TieuChiThoiGianDaoTaoTtProcess
    {
        string ServiceId { get; }
        DM_TieuChiThoiGianDaoTaoTtCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter);
        long Count(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter);
        DM_TieuChiThoiGianDaoTaoTtCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt);
        void Save(ActionSqlParamCls ActionSqlParam, string TieuChiThoiGianDaoTaoTtId, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt);
        void Delete(ActionSqlParamCls ActionSqlParam, string TieuChiThoiGianDaoTaoTtId);
        DM_TieuChiThoiGianDaoTaoTtCls CreateModel(ActionSqlParamCls ActionSqlParam, string TieuChiThoiGianDaoTaoTtId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string TieuChiThoiGianDaoTaoTtId);
        DM_TieuChiThoiGianDaoTaoTtCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter, ref int recordTotal);
        DM_TieuChiThoiGianDaoTaoTtCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaTieuChiThoiGianDaoTaoTt);
    }

    public class DM_TieuChiThoiGianDaoTaoTtTemplate : IDM_TieuChiThoiGianDaoTaoTtProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_TieuChiThoiGianDaoTaoTtCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter) { return 0; }
        public virtual DM_TieuChiThoiGianDaoTaoTtCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string TieuChiThoiGianDaoTaoTtId, DM_TieuChiThoiGianDaoTaoTtCls OTieuChiThoiGianDaoTaoTt) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string TieuChiThoiGianDaoTaoTtId) { }
        public virtual DM_TieuChiThoiGianDaoTaoTtCls CreateModel(ActionSqlParamCls ActionSqlParam, string TieuChiThoiGianDaoTaoTtId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string TieuChiThoiGianDaoTaoTtId) { return null; }
        public virtual DM_TieuChiThoiGianDaoTaoTtCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_TieuChiThoiGianDaoTaoTtFilterCls OTieuChiThoiGianDaoTaoTtFilter, ref int recordTotal) { return null; }
        public virtual DM_TieuChiThoiGianDaoTaoTtCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaTieuChiThoiGianDaoTaoTt) { return null; }
    }
}
