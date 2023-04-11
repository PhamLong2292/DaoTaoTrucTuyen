using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IPhieuPhanTichNguyenNhanSuCoProcess
    {
        string ServiceId { get; }
        PhieuPhanTichNguyenNhanSuCoCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter);
        PhieuPhanTichNguyenNhanSuCoCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo);
        void Save(ActionSqlParamCls ActionSqlParam, string PhieuPhanTichNguyenNhanSuCoId, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo);
        void Delete(ActionSqlParamCls ActionSqlParam, string PhieuPhanTichNguyenNhanSuCoId);
        PhieuPhanTichNguyenNhanSuCoCls CreateModel(ActionSqlParamCls ActionSqlParam, string PhieuPhanTichNguyenNhanSuCoId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string PhieuPhanTichNguyenNhanSuCoId);
        long Count(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter);
    }

    public class PhieuPhanTichNguyenNhanSuCoTemplate : IPhieuPhanTichNguyenNhanSuCoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual PhieuPhanTichNguyenNhanSuCoCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter) { return null; }
        public virtual PhieuPhanTichNguyenNhanSuCoCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string PhieuPhanTichNguyenNhanSuCoId, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string PhieuPhanTichNguyenNhanSuCoId) { }
        public virtual PhieuPhanTichNguyenNhanSuCoCls CreateModel(ActionSqlParamCls ActionSqlParam, string PhieuPhanTichNguyenNhanSuCoId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string PhieuPhanTichNguyenNhanSuCoId) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter) { return 0; }
    }
}
