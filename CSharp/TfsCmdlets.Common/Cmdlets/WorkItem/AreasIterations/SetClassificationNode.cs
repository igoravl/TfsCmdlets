/*
.SYNOPSIS
    Modifies the name, position and/or the dates of a Work Item Iteration.

.PARAMETER Iteration
    Specifies the name, URI or path of an Iteration. Wildcards are permitted. If omitted, all Iterations in the given Team Project are returned.nnTo supply a path, use a backslash ("\") between the path segments. Leading and trailing backslashes are optional.nnWhen supplying a URI, use URIs in the form of "vstfs:///Classification/Node/<GUID>" (where <GUID> is the unique identifier of the given node).

.PARAMETER NewName
    Specifies the new name of the iteration. Enter only a name, not a path and name. If you enter a path that is different from the path that is specified in the Iteration parameter, Rename-TfsIteration generates an error. To rename and move an item, use the Move-TfsIteration cmdlet.

.PARAMETER MoveBy
    Reorders an iteration by moving it either up or down inside its parent. A positive value moves an iteration down, whereas a negative one moves it up.

.PARAMETER StartDate
    Sets the start date of the iteration. To clear the start date, set it to null. Note that when clearing a date, both must be cleared at the same time (i.e. setting both StartDate and FinishDate to null)

.PARAMETER FinishDate
    Sets the finish date of the iteration. To clear the finish date, set it to null. Note that when clearing a date, both must be cleared at the same time (i.e. setting both StartDate and FinishDate to null)

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

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    [Cmdlet(VerbsCommon.Set, "TfsClassificationNode", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class SetClassificationNode : BaseCmdlet
    {
        /*
                [Parameter(Position=0, Mandatory=true, ValueFromPipeline=true, ValueFromPipelineByPropertyName=true)]
                [Alias("Area")]
                [Alias("Iteration")]
                [Alias("Path")]
                [ValidateScript({(_ is string]) || (_ is [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode])}) 
                [SupportsWildcards()]
                public object Node { get; set; }

                [Parameter()]
                public string StructureGroup { get; set; }

                [Parameter()]
                public int MoveBy { get; set; }

                [Parameter()]
                [Nullable[DateTime]]
                StartDate,

                [Parameter()]
                [Nullable[DateTime]]
                FinishDate,

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

                nodeToSet = Get-TfsClassificationNode -Node Node -StructureGroup StructureGroup -Project Project -Collection Collection

                if (! nodeToSet)
                {
                    throw new Exception($"Invalid or non-existent node {Node}")
                }

                tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                var client = GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();

                if (PSBoundParameters.ContainsKey("MoveBy"))
                {
                    Write-Warning "Reordering of areas/iterations is deprecated, as Azure DevOps UX keeps areas and iterations properly sorted. MoveBy argument ignored."
                }

                if (StructureGroup = = "Iterations" && (PSBoundParameters.ContainsKey($"StartDate") || {PSBoundParameters}.ContainsKey("FinishDate")))
                {
                    if (! (PSBoundParameters.ContainsKey($"StartDate") && {PSBoundParameters}.ContainsKey("FinishDate")))
                    {
                        throw new Exception("When setting iteration dates, both start and finish dates must be supplied.")
                    }

                    if(ShouldProcess(nodeToSet.RelativePath, $"Set iteration start date to "{StartDate}" and finish date to "FinishDate""))
                    {
                        patch = new Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode() -Property @{
                            attributes = _NewDictionary @([string], [object]) @{
                                startDate = StartDate
                                finishDate = FinishDate
                            }
                        }

                        task = client.UpdateClassificationNodeAsync(patch, tp.Name, structureGroup, nodeToSet.RelativePath.SubString(1)); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error setting dates on iteration "{{nodeToSet}.FullPath}"" task.Exception.InnerExceptions })
                    }
                }

                if(Passthru)
                {
                    WriteObject(Get-TfsClassificationNode -Node Node -StructureGroup StructureGroup -Project Project -Collection Collection); return;
                }
            }
        }

        Set-Alias -Name Set-TfsArea -Value Set-TfsClassificationNode
        Set-Alias -Name Set-TfsIteration -Value Set-TfsClassificationNode
        */
    }
}
