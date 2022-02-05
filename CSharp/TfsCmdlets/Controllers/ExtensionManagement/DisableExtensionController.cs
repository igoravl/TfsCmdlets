using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Controllers.ExtensionManagement
{
    [CmdletController(typeof(InstalledExtension))]
    partial class DisableExtensionController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<ExtensionManagementHttpClient>();

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
                    result = client.UpdateInstalledExtensionAsync(item)
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