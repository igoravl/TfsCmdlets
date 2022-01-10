using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    /// <summary>
    /// Disables an extension installed in the specified collection/organization.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true, OutputType = typeof(InstalledExtension))]
    partial class DisableExtension
    {
        /// <summary>
        /// Specifies the ID or the name of the extensions. Wilcards are supported. 
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards]
        public object Extension { get; set; } 

        /// <summary>
        /// Specifies the ID or the name of the publisher. Wilcards are supported. 
        /// </summary>
        [Parameter(Position = 1)]
        [SupportsWildcards]
        public string Publisher { get; set; }
   }
}