using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    /// <summary>
    /// Gets an installed extension in the specified collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(InstalledExtension))]
    partial class GetExtension
    {
        [Parameter(Position = 0)]
        [SupportsWildcards]
        public object Extension { get; set; } = "*";

        [Parameter(Position = 1)]
        [SupportsWildcards]
        public string Publisher { get; set; } = "*";

        [Parameter]
        public SwitchParameter IncludeDisabledExtensions { get; set; }

        [Parameter]
        public SwitchParameter IncludeErrors { get; set; }

        [Parameter]
        public SwitchParameter IncludeInstallationIssues { get; set; }
    }
}