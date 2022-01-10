using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    /// <summary>
    /// Enables an extension installed in the specified collection/organization.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(InstalledExtension))]
    partial class EnableExtension
    {
        /// <summary>
        /// Specifies the ID or the name of the extensions. Wilcards are supported. 
        /// </summary>
        [Parameter(Position = 0)]
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