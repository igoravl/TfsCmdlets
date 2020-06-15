/*
.SYNOPSIS
    Renames a Work Item Iteration.

.PARAMETER Iteration
    Specifies the name, URI or path of an Iteration. Wildcards are permitted. If omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ("\") between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of "vstfs:///Classification/Node/<GUID>" (where <GUID> is the unique identifier of the given node).

.PARAMETER NewName
    Specifies the new name of the iteration. Enter only a name, not a path and name. If you enter a path that is different from the path that is specified in the Iteration parameter, Rename-TfsIteration generates an error. To rename and move an item, use the Move-TfsIteration cmdlet.

.PARAMETER Project
    Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. If omitted, it defaults to the connection opened by Connect-TfsTeamProject (if any). 

For more details, see the Get-TfsTeamProject cmdlet.

.PARAMETER Collection
    Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
*/

using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Renames a Work Area.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsArea", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class RenameAreaNode : RenameClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Area")]
        public override object Node { get; set; }

        /// <inheritdoc/>
        protected override TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
    }

    /// <summary>
    /// Renames a Iteration.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsIteration", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class RenameIterationNode : RenameClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public override object Node { get; set; }

        /// <inheritdoc/>
        protected override TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;
    }

    /// <summary>
    /// Base implementation for Rename-Area and Rename-Iteration
    /// </summary>
    public abstract class RenameClassificationNode : BaseCmdlet
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
        /// HELP_PARAM_NEWNAME
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string NewName { get; set; }

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
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            var result = RenameItem<ClassificationNode>();

            if (Passthru)
            {
                WriteObject(result);
            }
        }
    }

    partial class ClassificationNodeDataService
    {
        protected override ClassificationNode DoRenameItem()
        {
            var (_, tp) = GetCollectionAndProject();
            var client = GetClient<WorkItemTrackingHttpClient>();
            var nodeToRename = GetItem<ClassificationNode>();
            var structureGroup = GetParameter<TreeStructureGroup>("StructureGroup");
            var structureGroupName = structureGroup.ToString().TrimEnd('s');
            var newName = GetParameter<string>(nameof(RenameClassificationNode.NewName));

            if (!ShouldProcess($"{structureGroupName} '{nodeToRename.FullPath}'", $"Rename to '{newName}'"))
            {
                return null;
            }

            var patch = new WorkItemClassificationNode()
            {
                Name = newName
            };

            return new ClassificationNode(client.UpdateClassificationNodeAsync(patch, tp.Name, structureGroup, nodeToRename.RelativePath.Substring(1))
                .GetResult($"Error renaming node '{nodeToRename.RelativePath}'"), tp.Name, client);
        }
    }
}