using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.UnitTests
{
    public static class RoslynReferenceHelper
    {
        public static List<PortableExecutableReference> LoadReferences()
        {
            // 1) Starts with assemblies already loaded
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var loaded = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
                .Select(a => a.Location);

            foreach (var loc in loaded)
                set.Add(loc);

            // 2) Adds assemblies found in the output folder
            var baseDir = AppContext.BaseDirectory;
            if (!string.IsNullOrEmpty(baseDir) && Directory.Exists(baseDir))
            {
                foreach (var dll in Directory.GetFiles(baseDir, "*.dll"))
                    set.Add(Path.GetFullPath(dll));
            }

            // Creates metadata references for all assemblies found
            var references = set
                .Where(File.Exists)
                .Select(p => MetadataReference.CreateFromFile(p))
                .ToList();

            return references;
        }
    }
}
