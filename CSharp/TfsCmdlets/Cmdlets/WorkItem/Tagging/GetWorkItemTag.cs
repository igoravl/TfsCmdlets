using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Gets one or more work item tags.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(WebApiTagDefinition))]
    partial class GetWorkItemTag
    {
        /// <summary>
        /// Specifies one or more tags to returns. Wildcards are supported. 
        /// When omitted, returns all existing tags in the given project.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Tag { get; set; } = "*";

        /// <summary>
        /// Includes tags not associated to any work items.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeInactive { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        /// <value></value>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }
    }
}