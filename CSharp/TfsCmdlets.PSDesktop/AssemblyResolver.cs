using System;

namespace TfsCmdlets
{
    /// <summary>
    /// Custom assembly resolver
    /// </summary>
    public partial class AssemblyResolver
    {
        partial void RegisterEventHandler()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                var assemblyName = e.Name.Split(',')[0];
                return ResolveAssembly(assemblyName);
            };
        }
    }
}
