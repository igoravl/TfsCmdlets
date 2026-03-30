using System.Management.Automation;
using TfsCmdlets.Services.Impl;

namespace TfsCmdlets
{
    /// <summary>
    /// PowerShell module initializer
    /// </summary>
    public class ModuleInitializer : IModuleAssemblyInitializer, IModuleAssemblyCleanup
    {
        /// <summary>
        /// Method called automatically by PowerShell upon module load/start
        /// </summary>
        void IModuleAssemblyInitializer.OnImport()
        {
            ServiceLocator.SetSite(this);

            var resolver = new AssemblyResolver();
            resolver.Register();
        }

        /// <summary>
        /// Method called automatically by PowerShell upon module unload (Remove-Module)
        /// </summary>
        void IModuleAssemblyCleanup.OnRemove(PSModuleInfo module)
        {
            CurrentConnectionsImpl.ClearEnvironmentVariables();
        }
    }
}