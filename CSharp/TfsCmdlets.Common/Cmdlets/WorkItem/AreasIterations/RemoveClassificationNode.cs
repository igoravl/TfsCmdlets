using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Deletes one or more Work Item Areas from a given Team Project.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsArea", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class RemoveArea : RemoveClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Area")]
        public override object Node { get; set; }

        /// <inheritdoc/>
        protected override TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
    }

    /// <summary>
    /// Deletes one or more Iterations from a given Team Project.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsIteration", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class RemoveIteration : RemoveClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public override object Node { get; set; }

        /// <inheritdoc/>
        protected override TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;
    }

    /// <summary>
    /// Base implementation for Remove-Area and Remove-Iteration
    /// </summary>
    public abstract class RemoveClassificationNode : BaseCmdlet
    {
        /// <summary>
        /// Specifies the name and/or path of the node (area or iteration).
        /// </summary>
        public virtual object Node { get; set; }

        /// <summary>
        /// Indicates the type of structure (area or iteration).
        /// </summary>
        [Parameter()]
        protected abstract TreeStructureGroup StructureGroup { get; }

        /// <summary>
        /// Specifies the new parent node for the work items currently assigned to the node 
        /// being deleted, if any. When omitted, defaults to the root node (the "\" node, at the
        /// team project level).
        /// </summary>
        [Parameter(Position = 1)]
        [Alias("NewPath")]
        public object MoveTo { get; set; } = "\\";

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            var (_, tp) = GetCollectionAndProject();
            var nodes = GetItems<ClassificationNode>();

            foreach(var n in nodes)
            {
                if (!ShouldProcess($"Team Project {tp.Name}", $"Delete {StructureGroup} '{n.RelativePath}'")) continue;

                RemoveItem<ClassificationNode>(new {Node = n});
            }

        }
        /*
                /// <summary>
                /// Performs execution of the command
                /// </summary>
                protected override void ProcessRecord()
                    {
                        if(! (PSBoundParameters.ContainsKey("StructureGroup"))){if (MyInvocation.InvocationName -like "*Area"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Areas}elseif (MyInvocation.InvocationName -like "*Iteration"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Iterations}else{throw new Exception("Invalid or missing StructureGroup argument"}};PSBoundParameters["StructureGroup"] = StructureGroup)

                        nodes = Get-TfsClassificationNode -Node Node -StructureGroup StructureGroup -Project Project -Collection Collection
                        moveToNode =  Get-TfsClassificationNode -Node MoveTo -StructureGroup StructureGroup -Project Project -Collection Collection

                        if(! moveToNode)
                        {
                            throw new Exception($"Invalid or non-existent node "{MoveTo}". To remove nodes, supply a valid node in the -MoveTo argument")
                        }

                        this.Log($"Remove nodes and move orphaned work items no node "{{moveToNode}.FullPath}"");

                        tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                        var client = GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();

                        foreach(node in nodes)
                        {
                            if(! (ShouldProcess(node.TeamProject, $"Remove node {{node}.RelativePath}")))
                            {
                                continue
                            }

                            task = client.DeleteClassificationNodeAsync(node.TeamProject,StructureGroup,node.RelativePath,moveToNode.Id); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error removing node "{{node}.FullPath}"" task.Exception.InnerExceptions })
                        }
                    }
                }

                Set-Alias -Name Remove-TfsArea -Value Remove-TfsClassificationNode
                Set-Alias -Name Remove-TfsIteration -Value Remove-TfsClassificationNode
                */
    }
}
