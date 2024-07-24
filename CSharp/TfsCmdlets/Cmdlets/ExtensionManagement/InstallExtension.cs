using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    /// <summary>
    /// Installs an extension in the specified organization/collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true, OutputType = typeof(InstalledExtension))]
    partial class InstallExtension
    {
        /// <summary>
        /// Specifies the ID of the extension to install.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("ExtensionId")]
        public string Extension { get; set; }

        /// <summary>
        /// Specifies the ID of the publisher of the extension.
        /// </summary>
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true)]
        [Alias("PublisherId")]
        public string Publisher { get; set; }

        /// <summary>
        /// Specifies the version of the extension to install. When omitted, installs the latest version.
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Version { get; set; }
   }

    [CmdletController(typeof(InstalledExtension))]
    partial class InstallExtensionController
    {
        protected override IEnumerable Run()
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