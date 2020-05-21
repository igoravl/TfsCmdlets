using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.ClassificationNode
{
    [Cmdlet(VerbsCommon.Move, "ClassificationNode", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode))]
    public class MoveClassificationNode : PSCmdlet
    {
        /*
                [Parameter(Mandatory=true, Position=0, ValueFromPipeline=true, ValueFromPipelineByPropertyName=true)]
                [Alias("Area")]
                [Alias("Iteration")]
                [Alias("Path")]
                public object Node { get; set; }

                [Parameter(Mandatory=true, Position=1)]
                [Alias("MoveTo")]
                public object Destination { get; set; }

                [Parameter()]
                [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
                StructureGroup,

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

            protected override void ProcessRecord()
            {
                if(! (PSBoundParameters.ContainsKey("StructureGroup"))){if (MyInvocation.InvocationName -like "*Area"){StructureGroup = [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]::Areas}elseif (MyInvocation.InvocationName -like "*Iteration"){StructureGroup = [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]::Iterations}else{throw new Exception("Invalid or missing StructureGroup argument"}};PSBoundParameters["StructureGroup"] = StructureGroup)

                tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                sourceNode = _GetNode -Node Node -Project Project -Collection Collection

                if(! sourceNode)
                {
                    throw new Exception($"Invalid or non-existent {StructureGroup} path "Node"")
                }

                _Log $"Source node: "{{sourceNode}.FullPath}""

                destinationNode = _GetNode -Node Destination -Project Project -Collection Collection

                if(! destinationNode)
                {
                    throw new Exception($"Invalid or non-existent {StructureGroup} path "Node"")
                }

                _Log $"Destination node: "{{destinationNode}.FullPath}""

                moveTo = $"{{destinationNode}.Path}\$(sourceNode.Name)"

                if (! ShouldProcess(sourceNode.FullPath, $"Move node to "{moveTo}""))
                {
                    return
                }

                patch = new Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode() -Property @{
                    Id = sourceNode.Id
                }

                client = Get-TfsRestClient "Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient" -Collection tpc

                task = client.CreateOrUpdateClassificationNodeAsync(patch, tp.Name, structureGroup, destinationNode.RelativePath.SubString(1)); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error moving node {{sourceNode}.RelativePath} to $(destinationNode.RelativePath)" task.Exception.InnerExceptions })

                if(Passthru.IsPresent)
                {
                    WriteObject(result); return;
                }
            }
        }

        Set-Alias -Name Move-TfsArea -Value Move-TfsClassificationNode
        Set-Alias -Name Move-TfsIteration -Value Move-TfsClassificationNode
        */
    }
}
