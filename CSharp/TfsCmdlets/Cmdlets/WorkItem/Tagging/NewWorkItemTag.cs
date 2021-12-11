using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Creates a new work item tag.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiTagDefinition))]
    partial class NewWorkItemTag
    {
        /// <summary>
        /// Specifies the name of the new tag.
        /// </summary>
        /// <value></value>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public string Tag { get; set; }
    }
}