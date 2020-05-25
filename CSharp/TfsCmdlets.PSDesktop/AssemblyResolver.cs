using System;

namespace TfsCmdlets
{
    public partial class AssemblyResolver
    {
        private void RegisterEventHandler()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                var assemblyName = e.Name.Split(',')[0];
                return ResolveAssembly(assemblyName);
            };
        }
    }
}
