using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Enables (activates) a work item tag.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiTagDefinition))]
    partial class EnableWorkItemTag
    {
        /// <summary>
        /// Specifies the name of the work item tag to rename.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards]
        [Alias("Name")]
        public object Tag { get; set; }

        [Parameter]
        internal bool Enabled => true;

        protected override string CommandName => "ToggleWorkItemTag";
    }
}