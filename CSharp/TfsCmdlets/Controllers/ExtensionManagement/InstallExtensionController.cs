using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Controllers.ExtensionManagement
{
    [CmdletController(typeof(InstalledExtension))]
    partial class InstallExtensionController
    {
        public override IEnumerable<InstalledExtension> Invoke()
        {
            switch (Extension)
            {
                case string s when !string.IsNullOrEmpty(s) && s.Contains("."):
                    {
                        var client = GetClient<ExtensionManagementHttpClient>();
                        var (publisher, extension, _) = Extension.Split('.');

                        if(!PowerShell.ShouldProcess(Collection, $"Install extension '{extension}' by '{publisher}'"))
                            yield break;

                        yield return client.InstallExtensionByNameAsync(publisher, extension, Version)
                            .GetResult("Error installing extension.");
                        break;
                    }
                case string s when !string.IsNullOrEmpty(s):
                    {
                        var client = GetClient<ExtensionManagementHttpClient>();

                        if(!PowerShell.ShouldProcess(Collection, $"Install extension '{Extension}' by '{Publisher}'"))
                            yield break;

                        yield return client.InstallExtensionByNameAsync(Publisher, Extension, Version)
                            .GetResult("Error installing extension.");
                        break;
                    }
                default:
                    {
                        Logger.LogError(new ArgumentException($"Invalid or unknown extension '{Extension}', publisher '{Publisher}', version '{Version}'"));
                        break;
                    }
            }
        }
    }
}