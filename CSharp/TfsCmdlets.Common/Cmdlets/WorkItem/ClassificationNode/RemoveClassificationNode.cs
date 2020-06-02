/*
.SYNOPSIS
    Deletes one or more Work Item Areas.

.PARAMETER Area
    Specifies the name, URI or path of an Area. Wildcards are permitted. If omitted, all Areas in the given Team Project are returned. 
To supply a path, use a backslash ("\") between the path segments. Leading and trailing backslashes are optional. 
When supplying a URI, use URIs in the form of "vstfs:///Classification/Node/<GUID>" (where <GUID> is the unique identifier of the given node)

.PARAMETER MoveTo
    Specifies the new area path for the work items currently assigned to the area being deleted, if any. When omitted, defaults to the root area

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

namespace TfsCmdlets.Cmdlets.WorkItem.ClassificationNode
{
    [Cmdlet(VerbsCommon.Remove, "ClassificationNode", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public class RemoveClassificationNode : BaseCmdlet
    {
        /*
                [Parameter(Mandatory=true, Position=0, ValueFromPipeline=true, ValueFromPipelineByPropertyName=true)]
                [Alias("Area")]
                [Alias("Iteration")]
                [Alias("Path")]
                [ValidateScript({(_ is string]) || (_ is [type]"Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode")}) 
                [SupportsWildcards()]
                public object Node { get; set; }

                [Parameter(Position=1)]
                [Alias("NewPath")]
                [ValidateScript({ (_ is string]) || (_ is [type]"Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode") }) 
                public object MoveTo { get; set; } = "\",

                [Parameter()]
                [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
                StructureGroup,

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

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
