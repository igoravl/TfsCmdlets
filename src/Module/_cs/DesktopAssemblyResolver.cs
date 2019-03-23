using System;
using System.Collections.Generic;
using System.Reflection;

namespace TfsCmdlets
{
    public class AssemblyResolver
    {
        private static bool IsVerbose {get;set;}

        public static Dictionary<string, string> PrivateAssemblies {get;set;}

        public static readonly Dictionary<string, Assembly> LoadedAssemblies = new Dictionary<string, Assembly>();

        public static readonly Dictionary<string, object> LogEntries = new Dictionary<string, object>();

        public static void Register(Dictionary<string, string> privateAssemblies, bool verbose = false)
        {
            PrivateAssemblies = privateAssemblies;
            IsVerbose = verbose;

            AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs e)
            {
                try
                {
                    var assemblyName = e.Name.Split(',')[0];
                    var isInternal = PrivateAssemblies.ContainsKey(assemblyName);

                    if (IsVerbose) Log("[INFO ] [" + (isInternal? "Internal": "External") + "] " + assemblyName, e);

                    return PrivateAssemblies.ContainsKey(assemblyName)
                        ? LoadAssembly(assemblyName)
                        : null;
                }
                catch(Exception ex)
                {
                    LogError(ex);
                    return null;
                }
            };
        }

        private static Assembly LoadAssembly(string assemblyName)
        {
            var assembly = Assembly.LoadFrom(PrivateAssemblies[assemblyName]);

            LoadedAssemblies.Add(assemblyName, assembly);

            return assembly;
        }

        private static void Log(string message, object data)
        {
            message = "[" + (LogEntries.Count+1).ToString("00000") + "] [" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " + message;

            LogEntries.Add(message, data);
        }

        private static void LogError(Exception ex)
        {
            Log("[ERROR] " + ex.Message, ex);
        }
    }
}
