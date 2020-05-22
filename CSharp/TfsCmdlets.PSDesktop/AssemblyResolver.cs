using System;
using System.Reflection;

namespace TfsCmdlets
{
    internal class AssemblyResolver: BaseAssemblyResolver
    {
        protected override void RegisterEventHandler()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                var assemblyName = e.Name.Split(',')[0];
                return ResolveAssembly(assemblyName);
            };
        }
    }
}
