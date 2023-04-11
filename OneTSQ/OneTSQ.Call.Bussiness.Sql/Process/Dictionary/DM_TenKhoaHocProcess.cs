using System;
using System.Collections.ObjectModel;
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
    public class DM_TenKhoaHocProcessBll : DM_TenKhoaHocTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_TenKhoaHocProcessBll";
            }
        }


        public override DM_TenKhoaHocCls[] Reading(
            RenderInfoCls ORenderInfo,
            DM_TenKhoaHocFilterCls OTenKhoaHocFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().Reading(ActionSqlParam, OTenKhoaHocFilter);
        }

        public override long Count(
            RenderInfoCls ORenderInfo,
            DM_TenKhoaHocFilterCls OTenKhoaHocFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().Count(ActionSqlParam, OTenKhoaHocFilter);
        }

        public override DM_TenKhoaHocCls[] ReadingWithPaging(
            RenderInfoCls ORenderInfo,
            DM_TenKhoaHocFilterCls OTenKhoaHocFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().ReadingWithPaging(ActionSqlParam, OTenKhoaHocFilter);
        }

        public override void Add(RenderInfoCls ORenderInfo, DM_TenKhoaHocCls OTenKhoaHoc)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().Add(ActionSqlParam, OTenKhoaHoc);
        }


        public override void Save(RenderInfoCls ORenderInfo, string TenKhoaHocId, DM_TenKhoaHocCls OTenKhoaHoc)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().Save(ActionSqlParam, TenKhoaHocId, OTenKhoaHoc);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string TenKhoaHocId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().Delete(ActionSqlParam, TenKhoaHocId);
        }


        public override DM_TenKhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string TenKhoaHocId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().CreateModel(ActionSqlParam, TenKhoaHocId);
        }
        public override string Duplicate(RenderInfoCls ORenderInfo, string TenKhoaHocId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().Duplicate(ActionSqlParam, TenKhoaHocId);
        }
        public override DM_TenKhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DM_TenKhoaHocFilterCls OTenKhoaHocFilter, ref int recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().PageReading(ActionSqlParam, OTenKhoaHocFilter, ref recordTotal);
        }
        public override DM_TenKhoaHocCls CheckCode(RenderInfoCls ORenderInfo, string MaTenKhoaHoc)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().CheckCode(ActionSqlParam, MaTenKhoaHoc);
        }
    }
}
