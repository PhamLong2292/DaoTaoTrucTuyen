using OneTSQ.Core.Model;
using OneTSQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Bussiness.Template
{
    public interface IDM_KyThuatChuyenGiaoProcess
    {
        string ServiceId { get; }
        DM_KyThuatChuyenGiaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter);
        long Count(ActionSqlParamCls ActionSqlParam, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter);
        DM_KyThuatChuyenGiaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter);
        void Add(ActionSqlParamCls ActionSqlParam, DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao);
        void Save(ActionSqlParamCls ActionSqlParam, string KyThuatChuyenGiaoId, DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao);
        void Delete(ActionSqlParamCls ActionSqlParam, string KyThuatChuyenGiaoId);
        DM_KyThuatChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string KyThuatChuyenGiaoId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string KyThuatChuyenGiaoId);
        DM_KyThuatChuyenGiaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter, ref int recordTotal);
        DM_KyThuatChuyenGiaoCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaKyThuatChuyenGiao);
    }

    public class DM_KyThuatChuyenGiaoTemplate : IDM_KyThuatChuyenGiaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_KyThuatChuyenGiaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter) { return null; }
        public virtual long Count(ActionSqlParamCls ActionSqlParam, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter) { return 0; }
        public virtual DM_KyThuatChuyenGiaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string KyThuatChuyenGiaoId, DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string KyThuatChuyenGiaoId) { }
        public virtual DM_KyThuatChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string KyThuatChuyenGiaoId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string KyThuatChuyenGiaoId) { return null; }
        public virtual DM_KyThuatChuyenGiaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter, ref int recordTotal) { return null; }
        public virtual DM_KyThuatChuyenGiaoCls CheckCode(ActionSqlParamCls ActionSqlParam, string MaKyThuatChuyenGiao) { return null; }
    }
}
