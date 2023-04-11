using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IPhieuBaoCaoPhanUngCoHaiADRProcess
    {
        string ServiceId { get; }
        PhieuBaoCaoPhanUngCoHaiADRCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter);
        PhieuBaoCaoPhanUngCoHaiADRCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter, ref long recordTotal);
        long Count(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter);
        void Add(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBaoCaoPhanUngCoHaiADR);
        void Save(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoPhanUngCoHaiADRId, PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBaoCaoPhanUngCoHaiADR);
        void Delete(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoPhanUngCoHaiADRId);
        PhieuBaoCaoPhanUngCoHaiADRCls CreateModel(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoPhanUngCoHaiADRId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoPhanUngCoHaiADRId);
      
    }

    public class PhieuBaoCaoPhanUngCoHaiADRTemplate : IPhieuBaoCaoPhanUngCoHaiADRProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual PhieuBaoCaoPhanUngCoHaiADRCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter) { return null; }
        public virtual PhieuBaoCaoPhanUngCoHaiADRCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter, ref long recordTotal) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter) { return -1; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBaoCaoPhanUngCoHaiADR) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoPhanUngCoHaiADRId, PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBaoCaoPhanUngCoHaiADR) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoPhanUngCoHaiADRId) { }
        public virtual PhieuBaoCaoPhanUngCoHaiADRCls CreateModel(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoPhanUngCoHaiADRId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string PhieuBaoCaoPhanUngCoHaiADRId) { return null; }
    }
}
