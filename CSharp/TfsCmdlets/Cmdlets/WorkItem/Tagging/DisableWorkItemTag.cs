using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Disables (deactivates) a work item tag.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiTagDefinition))]
    partial class DisableWorkItemTag
    {
        /// <summary>
        /// Specifies the tag to disable. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards]
        [Alias("Name")]
        public object Tag { get; set; }

        [Parameter]
        internal bool Enabled => false;
    }
}