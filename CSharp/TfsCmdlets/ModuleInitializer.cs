using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets
{
    /// <summary>
    /// PowerShell module initializer
    /// </summary>
    public class ModuleInitializer : IModuleAssemblyInitializer
    {
        /// <summary>
        /// Method called automatically by PowerShell upon module load/start
        /// </summary>
        void IModuleAssemblyInitializer.OnImport()
        {
            var resolver = new AssemblyResolver();
            resolver.Register();
        }
    }
}