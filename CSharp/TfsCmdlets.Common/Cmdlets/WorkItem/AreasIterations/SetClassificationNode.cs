using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    [Cmdlet(VerbsCommon.Set, "TfsClassificationNode", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class SetClassificationNode : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Position=0, Mandatory=true, ValueFromPipeline=true, ValueFromPipelineByPropertyName=true)]
        //         [Alias("Area")]
        //         [Alias("Iteration")]
        //         [Alias("Path")]
        //         [ValidateScript({(_ is string]) || (_ is [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode])}) 
        //         [SupportsWildcards()]
        //         public object Node { get; set; }

        //         [Parameter()]
        //         public string StructureGroup { get; set; }

        //         [Parameter()]
        //         public int MoveBy { get; set; }

        //         [Parameter()]
        //         [Nullable[DateTime]]
        //         StartDate,

        //         [Parameter()]
        //         [Nullable[DateTime]]
        //         FinishDate,

        //         [Parameter()]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //         [Parameter()]
        //         public SwitchParameter Passthru { get; set; }

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void ProcessRecord()
        //     {
        //         if(! (PSBoundParameters.ContainsKey("StructureGroup"))){if (MyInvocation.InvocationName -like "*Area"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Areas}elseif (MyInvocation.InvocationName -like "*Iteration"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Iterations}else{throw new Exception("Invalid or missing StructureGroup argument"}};PSBoundParameters["StructureGroup"] = StructureGroup)

        //         nodeToSet = Get-TfsClassificationNode -Node Node -StructureGroup StructureGroup -Project Project -Collection Collection

        //         if (! nodeToSet)
        //         {
        //             throw new Exception($"Invalid or non-existent node {Node}")
        //         }

        //         tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        //         var client = GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();

        //         if (PSBoundParameters.ContainsKey("MoveBy"))
        //         {
        //             Write-Warning "Reordering of areas/iterations is deprecated, as Azure DevOps UX keeps areas and iterations properly sorted. MoveBy argument ignored."
        //         }

        //         if (StructureGroup = = "Iterations" && (PSBoundParameters.ContainsKey($"StartDate") || {PSBoundParameters}.ContainsKey("FinishDate")))
        //         {
        //             if (! (PSBoundParameters.ContainsKey($"StartDate") && {PSBoundParameters}.ContainsKey("FinishDate")))
        //             {
        //                 throw new Exception("When setting iteration dates, both start and finish dates must be supplied.")
        //             }

        //             if(ShouldProcess(nodeToSet.RelativePath, $"Set iteration start date to "{StartDate}" and finish date to "FinishDate""))
        //             {
        //                 patch = new Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode() -Property @{
        //                     attributes = _NewDictionary @([string], [object]) @{
        //                         startDate = StartDate
        //                         finishDate = FinishDate
        //                     }
        //                 }

        //                 task = client.UpdateClassificationNodeAsync(patch, tp.Name, structureGroup, nodeToSet.RelativePath.SubString(1)); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error setting dates on iteration "{{nodeToSet}.FullPath}"" task.Exception.InnerExceptions })
        //             }
        //         }

        //         if(Passthru)
        //         {
        //             WriteObject(Get-TfsClassificationNode -Node Node -StructureGroup StructureGroup -Project Project -Collection Collection); return;
        //         }
        //     }
        // }

        // Set-Alias -Name Set-TfsArea -Value Set-TfsClassificationNode
        // Set-Alias -Name Set-TfsIteration -Value Set-TfsClassificationNode
    }
}
