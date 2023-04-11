using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDanhGiaDeCuong_DeTaiProcess
    {
        string ServiceId { get; }
        DanhGiaDeCuong_DeTaiCls[] Reading(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter);
        DanhGiaDeCuong_DeTaiCls[] PageReading(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai);
        void Save(ActionSqlParamCls ActionSqlParam, string DanhGiaDeCuong_DeTaiId, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai);
        void Delete(ActionSqlParamCls ActionSqlParam, string DanhGiaDeCuong_DeTaiId);
        DanhGiaDeCuong_DeTaiCls CreateModel(ActionSqlParamCls ActionSqlParam, string DanhGiaDeCuong_DeTaiId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DanhGiaDeCuong_DeTaiId);
        long Count(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter);
    }

    public class DanhGiaDeCuong_DeTaiTemplate : IDanhGiaDeCuong_DeTaiProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DanhGiaDeCuong_DeTaiCls[] Reading(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter) { return null; }
        public virtual DanhGiaDeCuong_DeTaiCls[] PageReading(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DanhGiaDeCuong_DeTaiId, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DanhGiaDeCuong_DeTaiId) { }
        public virtual DanhGiaDeCuong_DeTaiCls CreateModel(ActionSqlParamCls ActionSqlParam, string DanhGiaDeCuong_DeTaiId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DanhGiaDeCuong_DeTaiId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter) { return 0; }
    }
}
