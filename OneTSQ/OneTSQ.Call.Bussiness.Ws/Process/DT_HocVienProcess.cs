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

namespace OneTSQ.Call.Bussiness.Ws
{
    public class DT_HocVienProcessBll : DT_HocVienTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_HocVienProcessBll";
            }
        }


        public override DT_HocVienCls[] Reading(RenderInfoCls ORenderInfo, DT_HocVienFilterCls ODT_HocVienFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Reading(ActionSqlParam, ODT_HocVienFilter);
        }
        public override DT_HocVienCls[] PageReading(RenderInfoCls ORenderInfo, DT_HocVienFilterCls ODT_HocVienFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().PageReading(ActionSqlParam, ODT_HocVienFilter, ref recordTotal);
        }

        public override void Add(RenderInfoCls ORenderInfo, DT_HocVienCls ODT_HocVien)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Add(ActionSqlParam, ODT_HocVien);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DT_HocVienId, DT_HocVienCls ODT_HocVien)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Save(ActionSqlParam, DT_HocVienId, ODT_HocVien);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DT_HocVienId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Delete(ActionSqlParam, DT_HocVienId);
        }


        public override DT_HocVienCls CreateModel(RenderInfoCls ORenderInfo, string DT_HocVienId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().CreateModel(ActionSqlParam, DT_HocVienId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DT_HocVienId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_HocVienProcess().Duplicate(ActionSqlParam, DT_HocVienId);
        }


    }
}
