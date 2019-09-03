using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;

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

            AssemblyLoadContext.Default.Resolving += delegate(AssemblyLoadContext ctx, AssemblyName asmName)
            {
                var assemblyName = asmName.FullName.Split(',')[0];

                try
                {
                    if (!PrivateAssemblies.ContainsKey(assemblyName))
                    {
                        LogWarn("Skipping unknown assembly " + asmName.Name);
                        return null;
                    }

                    var asm = LoadAssembly(assemblyName, ctx);
                    LogInfo(assemblyName + " resolved as '" + asm.FullName + "'");

                    return asm;
                }
                catch(Exception ex)
                {
                    LogError(ex, assemblyName);
                    return null;
                }
            };
        }

        private static Assembly LoadAssembly(string assemblyName, AssemblyLoadContext ctx)
        {
            if(LoadedAssemblies.ContainsKey(assemblyName))
            {
                return LoadedAssemblies[assemblyName];
            }

            var assembly = ctx.LoadFromAssemblyName(new AssemblyName(PrivateAssemblies[assemblyName]));
            LoadedAssemblies.Add(assemblyName, assembly);

            return assembly;
        }

        private static void Log(string message, object data)
        {
            message = "[" + (LogEntries.Count+1).ToString("00000") + " - " + DateTime.Now.ToString("HH:mm:ss.ffff") + "] " + message;

            LogEntries.Add(message, data);
        }

        private static void LogInfo(string message)
        {
            if (!IsVerbose) return;

            Log("[INFO ] " + message, string.Empty);
        }

        private static void LogWarn(string message)
        {
            Log("[WARN ] " + message, string.Empty);
        }

        private static void LogError(Exception ex, string assemblyName = null)
        {
            Log("[ERROR] Loading assembly " + (assemblyName?? "(unknown)"), ex);
        }
    }
}
