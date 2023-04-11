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
public class DT_KetQuaDaoTaoProcessBll : DT_KetQuaDaoTaoTemplate
{
public override string ServiceId
{
    get
    {
        return "SqlDT_KetQuaDaoTaoProcessBll";
    }
}


public override DT_KetQuaDaoTaoCls[] Reading(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Reading(ActionSqlParam, ODT_KetQuaDaoTaoFilter);
}
public override DT_KetQuaDaoTaoCls[] ReadingDiemDanh(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().ReadingDiemDanh(ActionSqlParam, ODT_KetQuaDaoTaoFilter);
}
public override DT_KetQuaDaoTaoCls[] PageReading(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter, ref long recordTotal)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().PageReading(ActionSqlParam, ODT_KetQuaDaoTaoFilter, ref recordTotal);
}


public override void Add(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Add(ActionSqlParam, ODT_KetQuaDaoTao);
}


public override void Save(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId, DT_KetQuaDaoTaoCls ODT_KetQuaDaoTao)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Save(ActionSqlParam, DT_KetQuaDaoTaoId, ODT_KetQuaDaoTao);
}


public override void Delete(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Delete(ActionSqlParam, DT_KetQuaDaoTaoId);
}


public override DT_KetQuaDaoTaoCls CreateModel(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().CreateModel(ActionSqlParam, DT_KetQuaDaoTaoId);
}


public override string Duplicate(RenderInfoCls ORenderInfo, string DT_KetQuaDaoTaoId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Duplicate(ActionSqlParam, DT_KetQuaDaoTaoId);
}
public override bool? IsTrungThoiGianHoc(RenderInfoCls ORenderInfo, string HocVienId, DateTime TuNgay, DateTime DenNgay, string KetQuaDaoTaoId)
{
    ActionSqlParamCls
        ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
    return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().IsTrungThoiGianHoc(ActionSqlParam, HocVienId, TuNgay, DenNgay, KetQuaDaoTaoId);
}

        public override long Count(RenderInfoCls ORenderInfo, DT_KetQuaDaoTaoFilterCls ODT_KetQuaDaoTaoFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDT_KetQuaDaoTaoProcess().Count(ActionSqlParam, ODT_KetQuaDaoTaoFilter);
        }

    }
}
