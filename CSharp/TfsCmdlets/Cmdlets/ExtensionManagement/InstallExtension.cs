using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    /// <summary>
    /// Installs an extension to the specified organization/collection.
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
}