using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.Services.Common;
#if NETCOREAPP2_0
using System.Runtime.Loader;
#endif

namespace TfsCmdlets
{
    public class AssemblyResolver
    {
        public static bool IsVerbose { get; set; }
        public static Dictionary<string, AssemblyEntry> Assemblies { get; } = new Dictionary<string, AssemblyEntry>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<string, object> LogEntries { get; } = new Dictionary<string, object>();

        private static bool _loaded;

        public static void AddAssembly(string name, string path)
        {
            Add(name, path);
            
            RegisterEventHandler();
        }

        private static void Add(string name, string path)
        {
            if (Assemblies.ContainsKey(name)) return;

            Assemblies.Add(name, new AssemblyEntry(name, path));
        }

        private static void RegisterEventHandler()
        {
            if (_loaded) return;

#if NET462
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                var assemblyName = e.Name.Split(',')[0];
                return ResolveAssembly(assemblyName);
            };
#else
            AssemblyLoadContext.Default.Resolving += (AssemblyLoadContext ctx, AssemblyName asmName) =>
            {
                var assemblyName = asmName.FullName.Split(',')[0];
                return ResolveAssembly(assemblyName);
            };
#endif

            _loaded = true;
        }

        private static Assembly ResolveAssembly(string assemblyName)
        {
            if (!Assemblies.ContainsKey(assemblyName))
            {
                LogWarn($"Skipping unknown assembly '{assemblyName}'");
                return null;
            }

            var entry = Assemblies[assemblyName];
            LogInfo($"Resolving {assemblyName} to '{entry.Path}'");

            return entry.Assembly;
        }

        private static void Log(string message, object data)
        {
            message = "[" + (LogEntries.Count + 1).ToString("00000") + " - " + DateTime.Now.ToString("HH:mm:ss.ffff") + "] " + message;

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
            Log("[ERROR] Loading assembly " + (assemblyName ?? "(unknown)"), ex);
        }

        public class AssemblyEntry
        {
            private Assembly _assembly;

            public AssemblyEntry(string name, string path)
            {
                Name = name;
                Path = path;
            }

            public string Name { get; }

            public string Path { get;  }

            public bool IsLoaded => (_assembly != null);

            public Assembly Assembly
            {
                get
                {
                    if (IsLoaded)
                    {
                        return _assembly;
                    }

                    try
                    {
                        _assembly = Assembly.LoadFrom(Path);
                    }
                    catch (Exception ex)
                    {
                        LogError(ex, Name);
                    }

                    return _assembly;
                }
            }
        }
    }
}
