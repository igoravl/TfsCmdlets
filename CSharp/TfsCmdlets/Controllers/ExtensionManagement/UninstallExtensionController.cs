using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Controllers.ExtensionManagement
{
    [CmdletController(typeof(InstalledExtension))]
    partial class UninstallExtensionController
    {
        public override IEnumerable<InstalledExtension> Invoke()
        {
            var client = GetClient<ExtensionManagementHttpClient>();

            foreach (var item in Items)
            {
                Logger.Log(item.ToString());

                if (!PowerShell.ShouldProcess(Collection, $"Uninstall extension '{item.ExtensionDisplayName}' by '{item.PublisherDisplayName}' ({item.ExtensionName}.{item.PublisherName})"))
                    continue;

                try
                {
                    // client.UninstallExtensionByNameAsync(item.PublisherName, item.ExtensionName)
                    //     .Wait("Error uninstalling extension.");
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