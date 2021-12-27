using System.Reflection;

namespace TfsCmdlets
{
    /// <summary>
    /// This class is used to resolve TfsCmdlets assemblies in runtime.
    /// </summary>
    public partial class AssemblyResolver
    {
        /// <summary>
        /// Mantains a list of all assemblies stored in the /Lib folder of this module to support 
        /// on-demand assembly resolving and loading
        /// </summary>
        public Dictionary<string, AssemblyEntry> Assemblies { get; } = new Dictionary<string, AssemblyEntry>(StringComparer.OrdinalIgnoreCase);

        partial void RegisterEventHandler();
        
        /// <summary>
        /// Registers the Assembly Resolver in the platform-specific assembly resolution mechanism and 
        /// loads the list of private assemblies to the Assemblies dictionary
        /// </summary>
        public void Register()
        {

#if NET471_OR_GREATER
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                var assemblyName = e.Name.Split(',')[0];
                return ResolveAssembly(assemblyName);
            };
#else
            System.Runtime.Loader.AssemblyLoadContext.Default.Resolving += (ctx, asmName) =>
            {
                var assemblyName = asmName.FullName.Split(',')[0];
                return ResolveAssembly(assemblyName);
            };
#endif

            foreach (var file in Directory.GetFiles(GetAssemblyDirectory(), "*.dll"))
            {
                AddAssembly(Path.GetFileNameWithoutExtension(file), file);
            }
        }

        private string GetAssemblyDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private Assembly ResolveAssembly(string assemblyName)
        {
            if (!Assemblies.ContainsKey(assemblyName))
            {
                return null;
            }

            var entry = Assemblies[assemblyName];

            return entry.Assembly;
        }

        private void AddAssembly(string name, string path)
        {
            Add(name, path);

            RegisterEventHandler();
        }

        private void Add(string name, string path)
        {
            if (Assemblies.ContainsKey(name)) return;

            Assemblies.Add(name, new AssemblyEntry(name, path));
        }

        /// <summary>
        /// Represents a private assembly
        /// </summary>
        public class AssemblyEntry
        {
            private Assembly _assembly;

            /// <summary>
            /// Creates an instance from an assembly name and its file path
            /// </summary>
            /// <param name="name">Assembly name (e.g. "Newtonsoft.json")</param>
            /// <param name="path">Full path to assembly file (e.g. "X:/path/to/module/Lib/Newtonsoft.json")</param>
            public AssemblyEntry(string name, string path)
            {
                Name = name;
                Path = path;
            }

            /// <summary>
            ///  Assembly name
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// Full path to assembly file
            /// </summary>
            public string Path { get; }

            /// <summary>
            /// Indicates whether this assembly has already been loaded by the Assembly Resolver
            /// </summary>
            public bool IsLoaded => (_assembly != null);

            /// <summary>
            /// The actual assembly represented by this instance. If the assembly wasn't previously
            /// loaded, it will be read from disk and returned to the caller
            /// </summary>
            public Assembly Assembly
            {
                get
                {
                    if (IsLoaded)
                    {
                        return _assembly;
                    }

                    _assembly = Assembly.LoadFrom(Path);

                    return _assembly;
                }
            }
        }
    }
}