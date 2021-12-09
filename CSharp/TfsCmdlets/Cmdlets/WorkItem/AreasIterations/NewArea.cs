using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Creates a new Work Item Area in the given Team Project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsArea", SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    [TfsCmdlet(CmdletScope.Project, DataType = typeof(Models.ClassificationNode))]
    partial class NewArea
    {
        /// <summary>
        /// Specifies the path of the new Area. When supplying a path, use a backslash ("\\") 
        /// between the path segments. Leading and trailing backslashes are optional. 
        /// The last segment in the path will be the area name.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Area", "Path")]
        public string Node { get; set; }

        /// <summary>
        /// Allows the cmdlet to create parent nodes if they're missing.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Indicates the type of structure (area or iteration)
        /// </summary>
        internal TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
    }
}