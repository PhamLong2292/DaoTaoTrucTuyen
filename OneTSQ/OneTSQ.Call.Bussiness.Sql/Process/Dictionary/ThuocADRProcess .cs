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
    public class ThuocADRProcessBll : ThuocADRTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlThuocADRProcessBll";
            }
        }


        public override ThuocADRCls[] Reading(RenderInfoCls ORenderInfo, ThuocADRFilterCls OThuocADRFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Reading(ActionSqlParam, OThuocADRFilter);
        }      
        public override ThuocADRCls[] PageReading(RenderInfoCls ORenderInfo, ThuocADRFilterCls OThuocADRFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().PageReading(ActionSqlParam, OThuocADRFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, ThuocADRCls OThuocADR)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Add(ActionSqlParam, OThuocADR);
        }


        public override void Save(RenderInfoCls ORenderInfo, string ThuocADRId, ThuocADRCls OThuocADR)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Save(ActionSqlParam, ThuocADRId, OThuocADR);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string ThuocADRId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Delete(ActionSqlParam, ThuocADRId);
        }


        public override ThuocADRCls CreateModel(RenderInfoCls ORenderInfo, string ThuocADRId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().CreateModel(ActionSqlParam, ThuocADRId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string ThuocADRId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Duplicate(ActionSqlParam, ThuocADRId);
        }    
        public override long Count(RenderInfoCls ORenderInfo, ThuocADRFilterCls OThuocADRFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateThuocADRProcess().Count(ActionSqlParam, OThuocADRFilter);
        }


    }
}
