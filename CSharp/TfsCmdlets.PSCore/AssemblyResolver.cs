using System;
using System.Reflection;
using System.Runtime.Loader;

namespace TfsCmdlets
{
    internal class AssemblyResolver: BaseAssemblyResolver
    {
        protected override void RegisterEventHandler()
        {
            AssemblyLoadContext.Default.Resolving += (ctx, asmName) =>
            {
                var assemblyName = asmName.FullName.Split(',')[0];
                return ResolveAssembly(assemblyName);
            };
        }
    }
}