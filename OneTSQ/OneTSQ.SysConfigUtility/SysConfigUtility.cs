using OneTSQ.Utility;
using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Core.Model;
using OneTSQ.ParameterConfig;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OneTSQ.SysConfigUtility
{
    public class SysConfigUtility
    {
        private static Type[] LoadAssembly(string filename)
        {
            Collection<Type> ColSysConfigs = new Collection<Type> { };
            Assembly a = Assembly.LoadFrom(filename);
            if (a == null) throw new Exception("Assembly " + filename + " not found or not valid");
            foreach (Module mod in a.GetLoadedModules())
            {
                var _parameterAttributes = mod.GetTypes().Where(f => f.IsDefined(typeof(ParameterAttribute), true));
                if (_parameterAttributes.Count() > 0)
                {
                    foreach (var param in _parameterAttributes)
                    {
                        ColSysConfigs.Add(param);
                    }
                }
                //foreach (Type ty in mod.GetTypes())
                //{
                //    foreach (Type intf in ty.GetInterfaces())
                //    {
                //        if (intf == typeof(ISysConfig))
                //        {
                //            ISysConfig w = (ISysConfig)a.CreateInstance(ty.FullName);

                //        }
                //    }
                //}
            }

            return ColSysConfigs.ToArray();
        }


        public static Type[] LoadPlugIns()
        {
            string Path = WebConfig.GetDllPath();
            string[] Files = System.IO.Directory.GetFiles(Path, "*.dll");
            Collection<Type> ColWebParts = new Collection<Type> { };
            for (int iIndex = 0; iIndex < Files.Length; iIndex++)
            {
                try
                {
                    string FileDll = Files[iIndex];
                    Type[] asms = LoadAssembly(FileDll);
                    for (int jIndex = 0; jIndex < asms.Length; jIndex++)
                    {
                        ColWebParts.Add(asms[jIndex]);
                    }
                }
                catch (Exception ex)
                {
                    ex.Source = "";
                }
            }


            return ColWebParts.ToArray();
        }


        public static void InitSysConfig()
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                Type[] asms = LoadPlugIns();
                for (int iIndex = 0; iIndex < asms.Length; iIndex++)
                {
                    ISysConfig isysConfig = (ISysConfig)asms[iIndex].Module.Assembly.CreateInstance(asms[iIndex].FullName);
                    var attribute = (ParameterAttribute)asms[iIndex].GetCustomAttributes(typeof(ParameterAttribute), true).Single();
                    if (attribute is SystemParameterAttribute)
                    {
                        if (!string.IsNullOrEmpty(attribute.SchemaCode))
                        {
                            RenderInfoCls ORenderInfo = WebEnvironments.ServerSideCreateRenderInfo();
                            CoreCallBussinessUtility.CreateBussinessProcess().CreateConfigProcess().InitOrUpdateSysConfig(ORenderInfo, attribute.SchemaCode, isysConfig.ConfigCode, isysConfig.ConfigName, isysConfig.Description, isysConfig.ConfigType);
                        }
                    }
                }

                if (RetAjaxOut.RetBoolean == false)
                {
                    throw new Exception("INIT SYSCONFIG FAILED => CODE: " + RetAjaxOut.InfoMessage);
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = string.IsNullOrEmpty(ex.Message) ? ex.ToString() : ex.Message;
            }
        }
    }
}
