using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    /// <summary>
    /// Uninstalls one of more extensions from the specified organization/collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true, OutputType = typeof(InstalledExtension))]
    partial class UninstallExtension
    {
        /// <summary>
        /// Specifies the ID of the extension to uninstall.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Extension { get; set; }

        /// <summary>
        /// Specifies the ID of the publisher of the extension.
        /// </summary>
        [Parameter(Position = 1)]
        public string Publisher { get; set; }
   }

    [CmdletController(typeof(InstalledExtension))]
    partial class UninstallExtensionController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<ExtensionManagementHttpClient>();

            foreach (var item in Items)
            {
                if (!PowerShell.ShouldProcess(Collection, $"Uninstall extension '{item.ExtensionDisplayName}' by '{item.PublisherDisplayName}' ({item.ExtensionName}.{item.PublisherName})"))
                    continue;

                try
                {
                    client.UninstallExtensionByNameAsync(item.PublisherName, item.ExtensionName)
                        .Wait("Error uninstalling extension.");
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }

            return null;
        }
    }
}