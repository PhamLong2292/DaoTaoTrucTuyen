using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
    public class DT_BcTongKetCongTacDaoTaoProcessBll : DT_BcTongKetCongTacDaoTaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_BcTongKetCongTacDaoTaoProcessBll";
            }
        }


        public override DT_BcTongKetCongTacDaoTaoCls[] Reading(RenderInfoCls ORenderInfo, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().Reading(ActionSqlParam, ODT_BcTongKetCongTacDaoTaoFilter);
        }

        public override DT_BcTongKetCongTacDaoTaoCls[] PageReading(RenderInfoCls ORenderInfo, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().PageReading(ActionSqlParam, ODT_BcTongKetCongTacDaoTaoFilter, ref recordTotal);
        }

        public override void Add(RenderInfoCls ORenderInfo, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().Add(ActionSqlParam, ODT_BcTongKetCongTacDaoTao);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().Save(ActionSqlParam, DT_BcTongKetCongTacDaoTaoId, ODT_BcTongKetCongTacDaoTao);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().Delete(ActionSqlParam, DT_BcTongKetCongTacDaoTaoId);
        }


        public override DT_BcTongKetCongTacDaoTaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().CreateModel(ActionSqlParam, DT_BcTongKetCongTacDaoTaoId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_BcTongKetCongTacDaoTaoId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_BcTongKetCongTacDaoTaoProcess().Duplicate(ActionSqlParam, DT_BcTongKetCongTacDaoTaoId);
        }


    }
}
