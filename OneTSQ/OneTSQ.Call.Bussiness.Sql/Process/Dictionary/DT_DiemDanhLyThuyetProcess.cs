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
    public class DT_DiemDanhLyThuyetProcessBll : DT_DiemDanhLyThuyetTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_DiemDanhLyThuyetProcessBll";
            }
        }


        public override DT_DiemDanhLyThuyetCls[] Reading(
            RenderInfoCls ORenderInfo,
            DT_DiemDanhLyThuyetFilterCls ODT_DiemDanhLyThuyetFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhLyThuyetProcess().Reading(ActionSqlParam, ODT_DiemDanhLyThuyetFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhLyThuyetProcess().Add(ActionSqlParam, ODT_DiemDanhLyThuyet);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId, DT_DiemDanhLyThuyetCls ODT_DiemDanhLyThuyet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhLyThuyetProcess().Save(ActionSqlParam, DT_DiemDanhLyThuyetId, ODT_DiemDanhLyThuyet);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhLyThuyetProcess().Delete(ActionSqlParam, DT_DiemDanhLyThuyetId);
        }


        public override DT_DiemDanhLyThuyetCls CreateModel(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhLyThuyetProcess().CreateModel(ActionSqlParam, DT_DiemDanhLyThuyetId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_DiemDanhLyThuyetId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DiemDanhLyThuyetProcess().Duplicate(ActionSqlParam, DT_DiemDanhLyThuyetId);
        }


    }
}
