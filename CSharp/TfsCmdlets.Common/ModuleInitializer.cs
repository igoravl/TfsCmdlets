using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets
{
    public class ModuleInitializer : IModuleAssemblyInitializer
    {
        void IModuleAssemblyInitializer.OnImport()
        {
            var resolver = new AssemblyResolver();
            resolver.Register();

            ServiceManager.Register(new CmdletServiceProviderImpl());
        }
    }
}