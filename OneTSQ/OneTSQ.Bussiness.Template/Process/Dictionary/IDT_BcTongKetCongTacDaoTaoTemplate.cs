using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IDT_BcTongKetCongTacDaoTaoProcess
    {
        string ServiceId { get; }
        DT_BcTongKetCongTacDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter);
        DT_BcTongKetCongTacDaoTaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter, ref long recordTotal);
        void Add(ActionSqlParamCls ActionSqlParam, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao);
        void Save(ActionSqlParamCls ActionSqlParam, string DT_BcTongKetCongTacDaoTaoId, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao);
        void Delete(ActionSqlParamCls ActionSqlParam, string DT_BcTongKetCongTacDaoTaoId);
        DT_BcTongKetCongTacDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_BcTongKetCongTacDaoTaoId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_BcTongKetCongTacDaoTaoId);
    }

    public class DT_BcTongKetCongTacDaoTaoTemplate : IDT_BcTongKetCongTacDaoTaoProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DT_BcTongKetCongTacDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter) { return null; }
        public virtual DT_BcTongKetCongTacDaoTaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter, ref long recordTotal) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string DT_BcTongKetCongTacDaoTaoId, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string DT_BcTongKetCongTacDaoTaoId) { }
        public virtual DT_BcTongKetCongTacDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string DT_BcTongKetCongTacDaoTaoId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string DT_BcTongKetCongTacDaoTaoId) { return null; }
    }
}
