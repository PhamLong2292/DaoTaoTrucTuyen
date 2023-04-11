using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDM_GiayToDiChuyenGiaoProcess
    {
        string ServiceId { get; }
        DM_GiayToDiChuyenGiaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter);
        long Count(ActionSqlParamCls ActionSqlParam, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter);
        DM_GiayToDiChuyenGiaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao);
        void Save(ActionSqlParamCls ActionSqlParam, string GiayToDiChuyenGiaoId, DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao);
        void Delete(ActionSqlParamCls ActionSqlParam, string GiayToDiChuyenGiaoId);
        DM_GiayToDiChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string GiayToDiChuyenGiaoId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string GiayToDiChuyenGiaoId);
        DM_GiayToDiChuyenGiaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter, ref int recordTotal);
        DM_GiayToDiChuyenGiaoCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaGiayToDiChuyenGiao);
    }

    public class DM_GiayToDiChuyenGiaoTemplate : IDM_GiayToDiChuyenGiaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_GiayToDiChuyenGiaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter) { return 0; }
        public virtual DM_GiayToDiChuyenGiaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string GiayToDiChuyenGiaoId, DM_GiayToDiChuyenGiaoCls OGiayToDiChuyenGiao) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string GiayToDiChuyenGiaoId) { }
        public virtual DM_GiayToDiChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string GiayToDiChuyenGiaoId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string GiayToDiChuyenGiaoId) { return null; }
        public virtual DM_GiayToDiChuyenGiaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_GiayToDiChuyenGiaoFilterCls OGiayToDiChuyenGiaoFilter, ref int recordTotal) { return null; }
        public virtual DM_GiayToDiChuyenGiaoCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaGiayToDiChuyenGiao) { return null; }
    }
}
