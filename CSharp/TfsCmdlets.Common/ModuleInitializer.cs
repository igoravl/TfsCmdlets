using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsCmdlets.ServiceProvider;

namespace TfsCmdlets
{
    public class ModuleInitializer : IModuleAssemblyInitializer
    {
        void IModuleAssemblyInitializer.OnImport()
        {
            var resolver = new AssemblyResolver();
            resolver.Register();

            ServiceExtensions.Register(new CmdletServiceProviderImpl());
        }
    }
}