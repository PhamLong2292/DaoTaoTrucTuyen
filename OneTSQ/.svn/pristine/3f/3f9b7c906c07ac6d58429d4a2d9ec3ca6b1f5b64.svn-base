using OneTSQ.Core.Model;
using OneTSQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDM_KyThuatChuyenGiaoProcess
    {
        string ServiceId { get; }
        DM_KyThuatChuyenGiaoCls[] Reading(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter);
        long Count(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter);
        DM_KyThuatChuyenGiaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter);
        void Add(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao);
        void Save(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId, DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao);
        void Delete(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId);
        DM_KyThuatChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId);
        string Duplicate(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId);
        DM_KyThuatChuyenGiaoCls[] PageReading(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter, ref int recordTotal);
        DM_KyThuatChuyenGiaoCls CheckCode(RenderInfoCls ORenderInfo, string MaKyThuatChuyenGiao);
    }

    public class DM_KyThuatChuyenGiaoTemplate : IDM_KyThuatChuyenGiaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DM_KyThuatChuyenGiaoCls[] Reading(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter) { return null; }
        public virtual long Count(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter) { return 0; }
        public virtual DM_KyThuatChuyenGiaoCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId, DM_KyThuatChuyenGiaoCls OKyThuatChuyenGiao) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId) { }
        public virtual DM_KyThuatChuyenGiaoCls CreateModel(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string KyThuatChuyenGiaoId) { return null; }
        public virtual DM_KyThuatChuyenGiaoCls[] PageReading(RenderInfoCls ORenderInfo, DM_KyThuatChuyenGiaoFilterCls OKyThuatChuyenGiaoFilter, ref int recordTotal) { return null; }
        public virtual DM_KyThuatChuyenGiaoCls CheckCode(RenderInfoCls ORenderInfo, string MaKyThuatChuyenGiao) { return null; }
    }
}
