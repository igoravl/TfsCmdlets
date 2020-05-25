﻿using System.Runtime.Loader;

namespace TfsCmdlets
{
    public partial class AssemblyResolver
    {
        private void RegisterEventHandler()
        {
            AssemblyLoadContext.Default.Resolving += (ctx, asmName) =>
            {
                var assemblyName = asmName.FullName.Split(',')[0];
                return ResolveAssembly(assemblyName);
            };
        }
    }
}