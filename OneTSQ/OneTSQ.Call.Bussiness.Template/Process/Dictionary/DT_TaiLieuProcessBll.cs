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
    public class DT_TaiLieuProcessBll : DT_TaiLieuTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_TaiLieuProcessBll";
            }
        }


        public override DT_TaiLieuCls[] Reading(
            RenderInfoCls ORenderInfo,
            DT_TaiLieuFilterCls ODT_TaiLieuFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().Reading(ActionSqlParam, ODT_TaiLieuFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_TaiLieuCls ODT_TaiLieu)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().Add(ActionSqlParam, ODT_TaiLieu);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_TaiLieuId, DT_TaiLieuCls ODT_TaiLieu)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().Save(ActionSqlParam, DT_TaiLieuId, ODT_TaiLieu);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_TaiLieuId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().Delete(ActionSqlParam, DT_TaiLieuId);
        }


        public override DT_TaiLieuCls CreateModel(RenderInfoCls ORenderInfo, string DT_TaiLieuId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().CreateModel(ActionSqlParam, DT_TaiLieuId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_TaiLieuId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_TaiLieuProcess().Duplicate(ActionSqlParam, DT_TaiLieuId);
        }


    }
}
