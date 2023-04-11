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
    public class DT_LichThucHanhProcessBll : DT_LichThucHanhTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_LichThucHanhProcessBll";
            }
        }


        public override DT_LichThucHanhCls[] Reading(
            RenderInfoCls ORenderInfo,
            DT_LichThucHanhFilterCls ODT_LichThucHanhFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().Reading(ActionSqlParam, ODT_LichThucHanhFilter);
        }


        public override void Add(RenderInfoCls ORenderInfo, DT_LichThucHanhCls ODT_LichThucHanh)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().Add(ActionSqlParam, ODT_LichThucHanh);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_LichThucHanhId, DT_LichThucHanhCls ODT_LichThucHanh)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().Save(ActionSqlParam, DT_LichThucHanhId, ODT_LichThucHanh);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_LichThucHanhId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().Delete(ActionSqlParam, DT_LichThucHanhId);
        }


        public override DT_LichThucHanhCls CreateModel(RenderInfoCls ORenderInfo, string DT_LichThucHanhId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().CreateModel(ActionSqlParam, DT_LichThucHanhId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_LichThucHanhId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().Duplicate(ActionSqlParam, DT_LichThucHanhId);
        }
        public override DT_HocVienCls[] GetHocViens(RenderInfoCls ORenderInfo, string DT_LichThucHanhId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().GetHocViens(ActionSqlParam, DT_LichThucHanhId);
        }
        public override int GetHocVienQuantity(RenderInfoCls ORenderInfo, string DT_LichThucHanhId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().GetHocVienQuantity(ActionSqlParam, DT_LichThucHanhId);
        }
        public override void AddHocViens(RenderInfoCls ORenderInfo, DT_LichThucHanhHocVienCls[] LichThucHanhHocViens)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().AddHocViens(ActionSqlParam, LichThucHanhHocViens);
        }
        public override void RemoveHocViens(RenderInfoCls ORenderInfo, string DT_LichThucHanhId, string[] HocVienIds)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().RemoveHocViens(ActionSqlParam, DT_LichThucHanhId, HocVienIds);
        }

        public override void DeleteHocVien(RenderInfoCls ORenderInfo, string DT_LichThucHanhId, string HocVienId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_LichThucHanhProcess().DeleteHocVien(ActionSqlParam, DT_LichThucHanhId, HocVienId);
        }
    }
}
