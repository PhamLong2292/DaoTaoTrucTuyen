using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ProcessWebService.Utility
{
    public class ProcessWebServiceUtility
    {
        private static IWebServiceProcessCommand[] LoadAssembly(string filename)
        {
            Collection<IWebServiceProcessCommand> ColWebServiceProcessCommands = new Collection<IWebServiceProcessCommand> { };
            Assembly a = Assembly.LoadFrom(filename);
            if (a == null) throw new Exception("Assembly " + filename + " not found or not valid");
            foreach (Module mod in a.GetLoadedModules())
            {
                foreach (Type ty in mod.GetTypes())
                {
                    foreach (Type intf in ty.GetInterfaces())
                    {
                        if (intf == typeof(IWebServiceProcessCommand))
                        {
                            IWebServiceProcessCommand w = (IWebServiceProcessCommand)a.CreateInstance(ty.FullName);
                            ColWebServiceProcessCommands.Add(w);
                        }
                    }
                }
            }

            return ColWebServiceProcessCommands.ToArray();
        }


        public static IWebServiceProcessCommand[] LoadPlugIns()
        {
            string Path = WebConfig.GetDllPath();
            string[] Files = System.IO.Directory.GetFiles(Path, "*.dll");
            Collection<IWebServiceProcessCommand> ColWebParts = new Collection<IWebServiceProcessCommand> { };
            for (int iIndex = 0; iIndex < Files.Length; iIndex++)
            {
                try
                {
                    string FileDll = Files[iIndex];
                    IWebServiceProcessCommand[]
                        asms = LoadAssembly(FileDll);
                    for (int jIndex = 0; jIndex < asms.Length; jIndex++)
                    {
                        if (!string.IsNullOrEmpty(asms[jIndex].ServiceId))
                        {
                            bool Found = true;
                            if (Found)
                            {
                                ColWebParts.Add(asms[jIndex]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.Source = "";
                }
            }


            return ColWebParts.ToArray();
        }


        public static AjaxOut ProcessComand(
            string SecurityCode,
            string XmlData,
            string XmlDataSchema)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                WebServiceParamCls
                    OWebServiceParam = WebServiceParamParser.ParseFromXml(XmlData, XmlDataSchema);

                
                if (string.IsNullOrEmpty(OWebServiceParam.SiteId))
                {
                    OWebServiceParam.SiteId = WebConfig.GetWebConfig("WorkflowSiteId");
                }
                if (string.IsNullOrEmpty(OWebServiceParam.SiteLang))
                {
                    OWebServiceParam.SiteLang = WebConfig.GetWebConfig("vi");
                }
                IWebServiceProcessCommand[]
                    WebServiceProcessCommands = LoadPlugIns();
                for (int iIndex = 0; iIndex < WebServiceProcessCommands.Length; iIndex++)
                {
                    RetAjaxOut = WebServiceProcessCommands[iIndex].ProcessCommand(OWebServiceParam);
                    if (RetAjaxOut.Error) throw new Exception(RetAjaxOut.InfoMessage);
                    if (RetAjaxOut.RetBoolean)
                    {
                        break;
                    }
                }

                if (RetAjaxOut.RetBoolean == false)
                {
                    throw new Exception("PROCESS COMMAND HANDLER NOT FOUND => COMMAND: " + OWebServiceParam.Command);
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
    }
}
