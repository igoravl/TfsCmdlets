// using System.Collections.Generic;
// using System.Management.Automation;
// using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
// using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
// using TfsCmdlets.Extensions;
// using TfsCmdlets.Models;

// namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
// {
//     /// <summary>
//     /// Renames a Work Area.
//     /// </summary>
//     [Cmdlet(VerbsCommon.Rename, "TfsArea", SupportsShouldProcess = true)]
//     [OutputType(typeof(WorkItemClassificationNode))]
//     public class RenameAreaNode : RenameClassificationNode
//     {
//         /// <summary>
//         /// HELP_PARAM_AREA
//         /// </summary>
//         [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
//         [ValidateNotNullOrEmpty]
//         [Alias("Path", "Area")]
//         public override object Node { get; set; }

//         /// <inheritdoc/>
//         internal override TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
//     }

//     /// <summary>
//     /// Renames a Iteration.
//     /// </summary>
//     [Cmdlet(VerbsCommon.Rename, "TfsIteration", SupportsShouldProcess = true)]
//     [OutputType(typeof(WorkItemClassificationNode))]
//     public class RenameIterationNode : RenameClassificationNode
//     {
//         /// <summary>
//         /// HELP_PARAM_ITERATION
//         /// </summary>
//         [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
//         [ValidateNotNullOrEmpty]
//         [Alias("Path", "Iteration")]
//         public override object Node { get; set; }

//         /// <inheritdoc/>
//         internal override TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;
//     }

//     /// <summary>
//     /// Base implementation for Rename-Area and Rename-Iteration
//     /// </summary>
//     public abstract class RenameClassificationNode : CmdletBase
//     {
//         /// <summary>
//         /// Specifies the name and/or path of the node (area or iteration).
//         /// </summary>
//         public virtual object Node { get; set; }

//         /// <summary>
//         /// Indicates the type of structure (area or iteration).
//         /// </summary>
//         [Parameter()]
//         internal abstract TreeStructureGroup StructureGroup { get; }

//         /// <summary>
//         /// HELP_PARAM_NEWNAME
//         /// </summary>
//         [Parameter(Position = 1, Mandatory = true)]
//         [ValidateNotNullOrEmpty]
//         public string NewName { get; set; }

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

//     }

//     // TODO

//     //partial class ClassificationNodeDataService
//     //{
//     //    protected override ClassificationNode DoRenameItem()
//     //    {
//     //        var tp = Data.GetProject(parameters);
//     //        var client = Data.GetClient<WorkItemTrackingHttpClient>(parameters);
//     //        var nodeToRename = GetItem<ClassificationNode>();
//     //        var structureGroup = parameters.Get<TreeStructureGroup>("StructureGroup");
//     //        var structureGroupName = structureGroup.ToString().TrimEnd('s');
//     //        var newName = parameters.Get<string>(nameof(RenameClassificationNode.NewName));

//     //        if (!PowerShell.ShouldProcess($"{structureGroupName} '{nodeToRename.FullPath}'", $"Rename to '{newName}'"))
//     //        {
//     //            return null;
//     //        }

//     //        var patch = new WorkItemClassificationNode()
//     //        {
//     //            Name = newName,
//     //            Attributes = nodeToRename.Attributes
//     //        };

//     //        return new ClassificationNode(client.UpdateClassificationNodeAsync(patch, tp.Name, structureGroup, nodeToRename.RelativePath.Substring(1))
//     //            .GetResult($"Error renaming node '{nodeToRename.RelativePath}'"), tp.Name, client);
//     //    }
//     //}
// }