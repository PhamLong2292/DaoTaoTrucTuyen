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
    public class DT_KhoaHocProcessBll : DT_KhoaHocTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_KhoaHocProcessBll";
            }
        }


        public override DT_KhoaHocCls[] Reading(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Reading(ActionSqlParam, ODT_KhoaHocFilter);
        }
        public override DT_KhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().PageReading(ActionSqlParam, ODT_KhoaHocFilter, ref recordTotal);
        }
        public override DT_KhoaHocCls[] LopHocPageReading(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().LopHocPageReading(ActionSqlParam, ODT_KhoaHocFilter, ref recordTotal);
        }

        public override void Add(RenderInfoCls ORenderInfo, DT_KhoaHocCls ODT_KhoaHoc)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Add(ActionSqlParam, ODT_KhoaHoc);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_KhoaHocId, DT_KhoaHocCls ODT_KhoaHoc)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Save(ActionSqlParam, DT_KhoaHocId, ODT_KhoaHoc);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_KhoaHocId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Delete(ActionSqlParam, DT_KhoaHocId);
        }


        public override DT_KhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string DT_KhoaHocId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ActionSqlParam, DT_KhoaHocId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_KhoaHocId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Duplicate(ActionSqlParam, DT_KhoaHocId);
        }
        public override long Count(RenderInfoCls ORenderInfo, DT_KhoaHocFilterCls ODT_KhoaHocFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Count(ActionSqlParam, ODT_KhoaHocFilter);
        }

    }
}
