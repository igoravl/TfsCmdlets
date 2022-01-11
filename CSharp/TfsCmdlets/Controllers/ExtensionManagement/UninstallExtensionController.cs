using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Controllers.ExtensionManagement
{
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