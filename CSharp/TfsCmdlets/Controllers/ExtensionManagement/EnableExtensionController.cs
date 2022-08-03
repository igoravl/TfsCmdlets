using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Controllers.ExtensionManagement
{
    [CmdletController(typeof(InstalledExtension))]
    partial class EnableExtensionController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<ExtensionManagementHttpClient>();

            foreach (var item in GetItems<InstalledExtension>(new { IncludeDisabledExtensions = true }))
            {
                if ((item.InstallState.Flags & ExtensionStateFlags.Disabled) == ExtensionStateFlags.None)
                {
                    Logger.Log($"Extension '{item.ExtensionDisplayName}' ({item.PublisherName}.{item.ExtensionName}) is already enabled. Ignoring.");
                    continue;
                }

                if (!PowerShell.ShouldProcess(Collection, $"Enable extension '{item.ExtensionDisplayName}' ({item.PublisherName}.{item.ExtensionName})"))
                    continue;

                item.InstallState.Flags = item.InstallState.Flags & (~ExtensionStateFlags.Disabled);

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