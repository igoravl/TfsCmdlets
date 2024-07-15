using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Installs the Azure DevOps Shell
    /// </summary>
    [TfsCmdlet(CmdletScope.None)]
    partial class InstallShell
    {
        /// <summary>
        /// Specifies the installation target for the Azure DevOps Shell. 
        /// 
        /// When omitted, defaults to "Auto".
        /// </summary>
        [Parameter]
        public ShellInstallationTarget Target { get; set; } = ShellInstallationTarget.Auto;

        /// <summary>
        /// Specifies the installation scope for the Azure DevOps Shell.
        /// 
        /// When omitted, defaults to "CurrentUser".
        /// </summary>
        [Parameter]
        public ShellInstallationScope Scope { get; set; } = ShellInstallationScope.CurrentUser;

        /// <summary>
        /// Forces the installation of the Azure DevOps Shell, even if it is already installed.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }
}

// Controller

namespace TfsCmdlets.Controllers.Shell
{
    [CmdletController]
    partial class InstallShellController
    {
        [Import]
        private IShellInstallerFactory InstallerFactory { get; set; }

        protected override IEnumerable Run()
        {
            var installers = InstallerFactory.Create(Target);

            foreach(var installer in installers)
            {
                if(!PowerShell.ShouldProcess(installer.DisplayName, "Install Azure DevOps Shell"))
                {
                    continue;
                }

                if(!installer.Install(Scope, Force)) {
                    Logger.LogError($"Failed to install {installer.DisplayName} in scope {Scope}.");
                }
            }

            return null;
        }
    }
}