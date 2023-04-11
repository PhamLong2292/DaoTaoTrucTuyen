using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface IDT_BcTongKetCongTacDaoTaoProcess
    {
        string ServiceId { get; }
        DT_BcTongKetCongTacDaoTaoCls[] Reading(RenderInfoCls ORenderInfo, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter);
        DT_BcTongKetCongTacDaoTaoCls[] PageReading(RenderInfoCls ORenderInfo, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter, ref long recordTotal);
        void Add(RenderInfoCls ORenderInfo, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao);
        void Save(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao);
        void Delete(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId);
        DT_BcTongKetCongTacDaoTaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId);
        string Duplicate(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId);
    }

    public class DT_BcTongKetCongTacDaoTaoTemplate : IDT_BcTongKetCongTacDaoTaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_BcTongKetCongTacDaoTaoCls[] Reading(RenderInfoCls ORenderInfo, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter) { return null; }
        public virtual DT_BcTongKetCongTacDaoTaoCls[] PageReading(RenderInfoCls ORenderInfo, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter, ref long recordTotal) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId) { }
        public virtual DT_BcTongKetCongTacDaoTaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId) { return null; }
    }
}
