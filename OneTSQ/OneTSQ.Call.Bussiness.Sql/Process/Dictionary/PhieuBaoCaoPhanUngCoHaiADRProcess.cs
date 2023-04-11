﻿using System;
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
    public class PhieuBaoCaoPhanUngCoHaiADRProcessBll : PhieuBaoCaoPhanUngCoHaiADRTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlPhieuBaoCaoPhanUngCoHaiADRProcessBll";
            }
        }


        public override PhieuBaoCaoPhanUngCoHaiADRCls[] Reading(
            RenderInfoCls ORenderInfo,
            PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().Reading(ActionSqlParam, OPhieuBaoCaoPhanUngCoHaiADRFilter);
        }
        public override PhieuBaoCaoPhanUngCoHaiADRCls[] PageReading(
          RenderInfoCls ORenderInfo,
          PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuBaoCaoPhanUngCoHaiADRFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().PageReading(ActionSqlParam, OPhieuBaoCaoPhanUngCoHaiADRFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBaoCaoPhanUngCoHaiADR)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().Add(ActionSqlParam, OPhieuBaoCaoPhanUngCoHaiADR);
        }


        public override void Save(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId, PhieuBaoCaoPhanUngCoHaiADRCls OPhieuBaoCaoPhanUngCoHaiADR)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().Save(ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRId, OPhieuBaoCaoPhanUngCoHaiADR);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().Delete(ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRId);
        }


        public override PhieuBaoCaoPhanUngCoHaiADRCls CreateModel(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().CreateModel(ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string PhieuBaoCaoPhanUngCoHaiADRId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().Duplicate(ActionSqlParam, PhieuBaoCaoPhanUngCoHaiADRId);
        }


    }
}
