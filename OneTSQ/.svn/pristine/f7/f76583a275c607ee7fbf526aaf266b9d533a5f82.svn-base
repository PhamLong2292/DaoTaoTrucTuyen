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
    public class DT_DiemDanhThucHanhProcessBll : DT_DiemDanhThucHanhTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_DiemDanhThucHanhProcessBll";
            }
        }


        public override DT_DiemDanhThucHanhCls[] Reading(
            RenderInfoCls ORenderInfo,
            DT_DiemDanhThucHanhFilterCls ODT_DiemDanhThucHanhFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhThucHanhProcess().Reading(ActionSqlParam, ODT_DiemDanhThucHanhFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhThucHanhProcess().Add(ActionSqlParam, ODT_DiemDanhThucHanh);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId, DT_DiemDanhThucHanhCls ODT_DiemDanhThucHanh)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhThucHanhProcess().Save(ActionSqlParam, DT_DiemDanhThucHanhId, ODT_DiemDanhThucHanh);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhThucHanhProcess().Delete(ActionSqlParam, DT_DiemDanhThucHanhId);
        }


        public override DT_DiemDanhThucHanhCls CreateModel(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhThucHanhProcess().CreateModel(ActionSqlParam, DT_DiemDanhThucHanhId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_DiemDanhThucHanhId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhThucHanhProcess().Duplicate(ActionSqlParam, DT_DiemDanhThucHanhId);
        }


    }
}
