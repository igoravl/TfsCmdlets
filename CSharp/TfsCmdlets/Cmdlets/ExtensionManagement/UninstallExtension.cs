using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    /// <summary>
    /// Uninstalls one of more extensoins from the specified organization/collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(InstalledExtension))]
    partial class UninstallExtension
    {
        /// <summary>
        /// Specifies the ID of the extension to install.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        [SupportsWildcards]
        public object Extension { get; set; }

        /// <summary>
        /// Specifies the ID of the publisher of the extension.
        /// </summary>
        [Parameter(Position = 1)]
        [SupportsWildcards]
        public string Publisher { get; set; };
   }
}