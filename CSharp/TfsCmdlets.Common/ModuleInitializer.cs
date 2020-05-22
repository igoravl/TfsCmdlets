using System;
using System.Diagnostics;
using System.Management.Automation;

namespace TfsCmdlets
{
    public class ModuleInitializer : IModuleAssemblyInitializer
    {
        void IModuleAssemblyInitializer.OnImport()
        {
            var resolver = new AssemblyResolver();
            resolver.Register();
        }
    }
}