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
    public class DT_KeHoachLopProcessBll : DT_KeHoachLopTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_KeHoachLopProcessBll";
            }
        }


        public override DT_KeHoachLopCls[] Reading(
            RenderInfoCls ORenderInfo,
            DT_KeHoachLopFilterCls ODT_KeHoachLopFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().Reading(ActionSqlParam, ODT_KeHoachLopFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_KeHoachLopCls ODT_KeHoachLop)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().Add(ActionSqlParam, ODT_KeHoachLop);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_KeHoachLopId, DT_KeHoachLopCls ODT_KeHoachLop)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().Save(ActionSqlParam, DT_KeHoachLopId, ODT_KeHoachLop);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_KeHoachLopId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().Delete(ActionSqlParam, DT_KeHoachLopId);
        }


        public override DT_KeHoachLopCls CreateModel(RenderInfoCls ORenderInfo, string DT_KeHoachLopId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().CreateModel(ActionSqlParam, DT_KeHoachLopId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_KeHoachLopId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KeHoachLopProcess().Duplicate(ActionSqlParam, DT_KeHoachLopId);
        }


    }
}
