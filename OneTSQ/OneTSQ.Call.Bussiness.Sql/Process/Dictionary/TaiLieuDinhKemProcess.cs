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
    public class TaiLieuDinhKemProcessBll : TaiLieuDinhKemTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlTaiLieuDinhKemProcessBll";
            }
        }


        public override TaiLieuDinhKemCls[] Reading(RenderInfoCls ORenderInfo, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Reading(ActionSqlParam, OTaiLieuDinhKemFilter);
        }      
        public override TaiLieuDinhKemCls[] PageReading(RenderInfoCls ORenderInfo, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().PageReading(ActionSqlParam, OTaiLieuDinhKemFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, TaiLieuDinhKemCls OTaiLieuDinhKem)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Add(ActionSqlParam, OTaiLieuDinhKem);
        }


        public override void Save(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId, TaiLieuDinhKemCls OTaiLieuDinhKem)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Save(ActionSqlParam, TaiLieuDinhKemId, OTaiLieuDinhKem);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Delete(ActionSqlParam, TaiLieuDinhKemId);
        }


        public override TaiLieuDinhKemCls CreateModel(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().CreateModel(ActionSqlParam, TaiLieuDinhKemId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string TaiLieuDinhKemId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Duplicate(ActionSqlParam, TaiLieuDinhKemId);
        }    
        public override long Count(RenderInfoCls ORenderInfo, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateTaiLieuDinhKemProcess().Count(ActionSqlParam, OTaiLieuDinhKemFilter);
        }


    }
}
