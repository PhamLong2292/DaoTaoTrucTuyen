using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ReportUtility
{
    public class ReportBaseUtility
    {
        private static IReport[] LoadAssembly(string filename)
        {
            Collection<IReport> ColReports = new Collection<IReport> { };
            Assembly a = Assembly.LoadFrom(filename);
            if (a == null) throw new Exception("Assembly " + filename + " not found or not valid");
            foreach (Module mod in a.GetLoadedModules())
            {
                foreach (Type ty in mod.GetTypes())
                {
                    foreach (Type intf in ty.GetInterfaces())
                    {
                        if (intf == typeof(IReport))
                        {
                            IReport r = (IReport)a.CreateInstance(ty.FullName);
                            ColReports.Add(r);
                        }
                    }
                }
            }

            return ColReports.ToArray();
        }

        public static IReport[] LoadPlugIns(Type ReportType = null)
        {
            string Path = WebConfig.GetDllPath();
            string[] Files = System.IO.Directory.GetFiles(Path, "*.dll");
            Collection<IReport> ColReports = new Collection<IReport> { };
            for (int iIndex = 0; iIndex < Files.Length; iIndex++)
            {
                try
                {
                    string FileDll = Files[iIndex];
                    IReport[]
                        rps = LoadAssembly(FileDll);
                    for (int jIndex = 0; jIndex < rps.Length; jIndex++)
                    {
                        if ((ReportType != null && rps[jIndex].ReportType == ReportType) || ReportType == null)
                        {
                            bool Found = true;
                            if (Found)
                            {
                                ColReports.Add(rps[jIndex]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.Source = "";
                }
            }


            return ColReports.ToArray();
        }

        public static IReport LoadReport(string ReportID)
        {
            string Path = WebConfig.GetDllPath();
            string[] Files = System.IO.Directory.GetFiles(Path, "*.dll");
            for (int iIndex = 0; iIndex < Files.Length; iIndex++)
            {
                try
                {
                    string FileDll = Files[iIndex];
                    IReport[]
                        rps = LoadAssembly(FileDll);
                    for (int jIndex = 0; jIndex < rps.Length; jIndex++)
                    {
                        if (!string.IsNullOrEmpty(ReportID) && rps[jIndex].Attibute.ID == ReportID)
                        {
                            return rps[jIndex];
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.Source = "";
                }
            }


            return null;
        }

        public static IReport[] ReportMapsFromObjectWebPart(Type ReportType)
        {
            IReport[]
                ReportMaps = LoadPlugIns(ReportType);

            return ReportMaps;
        }

        public static IReport GetReportById(string ReportID)
        {
            return LoadReport(ReportID);
        }
    }
}
