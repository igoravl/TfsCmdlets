using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TfsCmdlets
{
    public abstract class BaseAssemblyResolver
    {
        public Dictionary<string, AssemblyEntry> Assemblies { get; } = new Dictionary<string, AssemblyEntry>(StringComparer.OrdinalIgnoreCase);

        protected abstract void RegisterEventHandler();

        public void Register()
        {
            RegisterEventHandler();

            foreach (var file in Directory.GetFiles(GetAssemblyDirectory(), "*.dll"))
            {
                AddAssembly(Path.GetFileNameWithoutExtension(file), file);
            }
        }

        private string GetAssemblyDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        protected Assembly ResolveAssembly(string assemblyName)
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


        public class AssemblyEntry
        {
            private Assembly _assembly;

            public AssemblyEntry(string name, string path)
            {
                Name = name;
                Path = path;
            }

            public string Name { get; }

            public string Path { get; }

            public bool IsLoaded => (_assembly != null);

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