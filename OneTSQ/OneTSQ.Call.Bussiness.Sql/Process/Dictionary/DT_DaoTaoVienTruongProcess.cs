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
    public class DT_DaoTaoVienTruongProcessBll : DT_DaoTaoVienTruongTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_DaoTaoVienTruongProcessBll";
            }
        }


        public override DT_DaoTaoVienTruongCls[] Reading(
            RenderInfoCls ORenderInfo,
            DT_DaoTaoVienTruongFilterCls ODT_DaoTaoVienTruongFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DaoTaoVienTruongProcess().Reading(ActionSqlParam, ODT_DaoTaoVienTruongFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DaoTaoVienTruongProcess().Add(ActionSqlParam, ODT_DaoTaoVienTruong);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DaoTaoVienTruongProcess().Save(ActionSqlParam, DT_DaoTaoVienTruongId, ODT_DaoTaoVienTruong);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DaoTaoVienTruongProcess().Delete(ActionSqlParam, DT_DaoTaoVienTruongId);
        }


        public override DT_DaoTaoVienTruongCls CreateModel(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DaoTaoVienTruongProcess().CreateModel(ActionSqlParam, DT_DaoTaoVienTruongId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_DaoTaoVienTruongId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_DaoTaoVienTruongProcess().Duplicate(ActionSqlParam, DT_DaoTaoVienTruongId);
        }


    }
}
