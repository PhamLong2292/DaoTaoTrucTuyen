using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IPhieuKhaoSatBenhVienVeTinhProcess
    {
        string ServiceId { get; }
        PhieuKhaoSatBenhVienVeTinhCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter);
        PhieuKhaoSatBenhVienVeTinhCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, PhieuKhaoSatBenhVienVeTinhCls OPhieuKhaoSatBenhVienVeTinh);
        void Save(ActionSqlParamCls ActionSqlParam, string PhieuKhaoSatBenhVienVeTinhId, PhieuKhaoSatBenhVienVeTinhCls OPhieuKhaoSatBenhVienVeTinh);
        void Delete(ActionSqlParamCls ActionSqlParam, string PhieuKhaoSatBenhVienVeTinhId);
        PhieuKhaoSatBenhVienVeTinhCls CreateModel(ActionSqlParamCls ActionSqlParam, string PhieuKhaoSatBenhVienVeTinhId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string PhieuKhaoSatBenhVienVeTinhId);
        long Count(ActionSqlParamCls ActionSqlParam, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter);
    }

    public class PhieuKhaoSatBenhVienVeTinhTemplate : IPhieuKhaoSatBenhVienVeTinhProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual PhieuKhaoSatBenhVienVeTinhCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter) { return null; }
        public virtual PhieuKhaoSatBenhVienVeTinhCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, PhieuKhaoSatBenhVienVeTinhCls OPhieuKhaoSatBenhVienVeTinh) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string PhieuKhaoSatBenhVienVeTinhId, PhieuKhaoSatBenhVienVeTinhCls OPhieuKhaoSatBenhVienVeTinh) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string PhieuKhaoSatBenhVienVeTinhId) { }
        public virtual PhieuKhaoSatBenhVienVeTinhCls CreateModel(ActionSqlParamCls ActionSqlParam, string PhieuKhaoSatBenhVienVeTinhId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string PhieuKhaoSatBenhVienVeTinhId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, PhieuKhaoSatBenhVienVeTinhFilterCls OPhieuKhaoSatBenhVienVeTinhFilter) { return 0; }
    }
}
