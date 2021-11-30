// using System.Management.Automation;
// using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
// using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
// using TfsCmdlets.Extensions;
// using TfsCmdlets.Models;
// using TfsCmdlets.Util;

// namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
// {
//     /// <summary>
//     /// Gets one or more Work Item Areas from a given Team Project.
//     /// </summary>
//     [Cmdlet(VerbsCommon.Move, "TfsArea", SupportsShouldProcess = true)]
//     [OutputType(typeof(WorkItemClassificationNode))]
//     public class MoveArea : CmdletBase
//     {
//         /// <summary>
//         /// HELP_PARAM_AREA
//         /// </summary>
//         [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
//         [SupportsWildcards()]
//         [ValidateNotNullOrEmpty]
//         [Alias("Path", "Area")]
//         public object Node { get; set; } = @"\**";

//         /// <summary>
//         /// Specifies the name and/or path of the destination parent node.
//         /// </summary>
//         [Parameter(Mandatory = true, Position = 1)]
//         [Alias("MoveTo")]
//         public object Destination { get; set; }

//         /// <summary>
//         /// Allows the cmdlet to create destination parent node(s) if they're missing.
//         /// </summary>
//         [Parameter()]
//         public SwitchParameter Force { get; set; }

//         /// <summary>
//         /// HELP_PARAM_PROJECT
//         /// </summary>
//         [Parameter()]
//         public object Project { get; set; }

//         /// <summary>
//         /// HELP_PARAM_COLLECTION
//         /// </summary>
//         [Parameter()]
//         public object Collection { get; set; }

//         /// <summary>
//         /// HELP_PARAM_PASSTHRU
//         /// </summary>
//         [Parameter()]
//         public SwitchParameter Passthru { get; set; }

//         /// <summary>
//         /// Returns the type name for the underlying IController implementing the logic of this cmdlet
//         /// </summary>
//         internal TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
//     }
// }