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
        /// <summary>
        /// Specifies the ID or the name of the extensions. Wilcards are supported. 
        /// When omitted, returns all extensions installed in the specified organization/collection.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards]
        public object Extension { get; set; } = "*";

        /// <summary>
        /// Specifies the ID or the name of the publisher. Wilcards are supported. 
        /// When omitted, returns all extensions installed in the specified organization/collection.
        /// </summary>
        [Parameter(Position = 1)]
        [SupportsWildcards]
        public string Publisher { get; set; } = "*";

        /// <summary>
        /// Includes disabled extensions in the result. When omitted, disabled extensions are not included in the result.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeDisabledExtensions { get; set; }

        [Parameter]
        public SwitchParameter IncludeErrors { get; set; }

        [Parameter]
        public SwitchParameter IncludeInstallationIssues { get; set; }
    }
}