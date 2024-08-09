using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    /// <summary>
    /// Disables an extension installed in the specified collection/organization.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true, OutputType = typeof(InstalledExtension))]
    partial class DisableExtension
    {
        /// <summary>
        /// Specifies the ID or the name of the extensions. Wildcards are supported. 
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards]
        public object Extension { get; set; } 

        /// <summary>
        /// Specifies the ID or the name of the publisher. Wildcards are supported. 
        /// </summary>
        [Parameter(Position = 1)]
        [SupportsWildcards]
        public string Publisher { get; set; }
   }

    [CmdletController(typeof(InstalledExtension), Client=typeof(IExtensionManagementHttpClient))]
    partial class DisableExtensionController
    {
        protected override IEnumerable Run()
        {
            foreach (var item in Items)
            {
                if ((item.InstallState.Flags & ExtensionStateFlags.Disabled) != 0)
                {
                    Logger.Log($"Extension '{item.ExtensionDisplayName}' ({item.PublisherName}.{item.ExtensionName}) is already disabled. Ignoring.");
                    continue;
                }

                if (!PowerShell.ShouldProcess(Collection, $"Disable extension '{item.ExtensionDisplayName}' ({item.PublisherName}.{item.ExtensionName})"))
                    continue;

                item.InstallState.Flags = item.InstallState.Flags | ExtensionStateFlags.Disabled;

                InstalledExtension result;

                try
                {
                    result = Client.UpdateInstalledExtensionAsync(item)
                        .GetResult("Error updating extension.");
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    continue;
                }

                yield return result;
            }
        }
    }
}