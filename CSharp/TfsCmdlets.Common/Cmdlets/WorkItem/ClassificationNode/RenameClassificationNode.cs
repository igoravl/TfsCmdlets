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
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.ClassificationNode
{
    [Cmdlet(VerbsCommon.Rename, "ClassificationNode", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class RenameClassificationNode : BaseCmdlet
    {
        /*
                [Parameter(Mandatory=true, Position=0, ValueFromPipeline=true, ValueFromPipelineByPropertyName=true)]
                [ValidateScript({(_ is string]) || (_ is [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode])}) 
                [Alias("Area")]
                [Alias("Iteration")]
                [Alias("Path")]
                public object Node { get; set; }

                [Parameter(Mandatory=true, Position=1)]
                public string NewName { get; set; }

                [Parameter()]
                [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
                StructureGroup,

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                if(! (PSBoundParameters.ContainsKey("StructureGroup"))){if (MyInvocation.InvocationName -like "*Area"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Areas}elseif (MyInvocation.InvocationName -like "*Iteration"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Iterations}else{throw new Exception("Invalid or missing StructureGroup argument"}};PSBoundParameters["StructureGroup"] = StructureGroup)

                tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                var client = GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();

                nodeToRename = Get-TfsClassificationNode -Node Node -StructureGroup StructureGroup -Project Project -Collection Collection

                if(! ShouldProcess(nodeToRename.FullPath, $"Rename node to "{NewName}""))
                {
                    return
                }

                patch = new Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode() -Property @{
                    Name = NewName
                }

                task = client.UpdateClassificationNodeAsync(patch, tp.Name, structureGroup, nodeToRename.RelativePath.SubString(1)); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error renaming node {node}" task.Exception.InnerExceptions })

                if (Passthru)
                {
                    WriteObject(result); return;
                }
            }
        }

        Set-Alias -Name Rename-TfsArea -Value Rename-TfsClassificationNode
        Set-Alias -Name Rename-TfsIteration -Value Rename-TfsClassificationNode
        */
    }
}
