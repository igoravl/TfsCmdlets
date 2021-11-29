// using System.Management.Automation;
// using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
// using TfsCmdlets.Models;

// namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
// {
//     /// <summary>
//     /// Determines whether the specified Work Area exist.
//     /// </summary>
//     [Cmdlet(VerbsDiagnostic.Test, "TfsArea")]
//     public class TestArea : CmdletBase
//     {
//         /// <summary>
//         /// HELP_PARAM_AREA
//         /// </summary>
//         [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
//         [Alias("Area", "Path")]
//         [SupportsWildcards()]
//         public string Node { get; set; }

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

//         /// <inheritdoc/>
//         internal TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
//     }
// }