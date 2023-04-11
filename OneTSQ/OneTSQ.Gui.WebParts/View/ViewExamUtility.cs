using OneTSQ.Core.Model;using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.WebParts
{
    public class WorkflowViewUtility
    {
        private static IViewProfileExam[] LoadAssembly(string filename)
        {
            Collection<IViewProfileExam> ColWorkflowViews = new Collection<IViewProfileExam> { };
            Assembly a = Assembly.LoadFrom(filename);
            if (a == null) throw new Exception("Assembly " + filename + " not found or not valid");
            foreach (Module mod in a.GetLoadedModules())
            {
                foreach (Type ty in mod.GetTypes())
                {
                    foreach (Type intf in ty.GetInterfaces())
                    {
                        if (intf == typeof(IViewProfileExam))
                        {
                            IViewProfileExam w = (IViewProfileExam)a.CreateInstance(ty.FullName);
                            ColWorkflowViews.Add(w);
                        }
                    }
                }
            }

            return ColWorkflowViews.ToArray();
        }


        public static IViewProfileExam[] LoadPlugIn(SiteParam OSiteParam)
        {
            string Path = WebConfig.GetDllPath();
            string[] Files = System.IO.Directory.GetFiles(Path, "*.dll");
            Collection<IViewProfileExam> ColWorkflowViews = new Collection<IViewProfileExam> { };
            for (int iIndex = 0; iIndex < Files.Length; iIndex++)
            {
                try
                {
                    string FileDll = Files[iIndex];
                    IViewProfileExam[]
                        asms = LoadAssembly(FileDll);
                    for (int jIndex = 0; jIndex < asms.Length; jIndex++)
                    {
                        if (!string.IsNullOrEmpty(asms[jIndex].ServiceId))
                        {
                            bool Found = true;
                            if (Found)
                            {
                                ColWorkflowViews.Add(asms[jIndex]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.Source = "";
                }
            }


            return ColWorkflowViews.ToArray();
        }


        public static IViewProfileExam Find(SiteParam OSiteParam, string WorkflowStatus)
        {
            IViewProfileExam[] found = LoadPlugIn(OSiteParam);
            for (int iIndex = 0; iIndex < found.Length; iIndex++)
            {
                if (!string.IsNullOrEmpty(found[iIndex].WorkflowStatus))
                {
                    if (found[iIndex].WorkflowStatus.Trim().ToLower().Equals(WorkflowStatus.Trim().ToLower()))
                    {
                        return found[iIndex];
                    }
                }
            }
            return null;
        }

    }
}
