/*
.SYNOPSIS

.DESCRIPTION

.EXAMPLE

.INPUTS

.OUTPUTS

.NOTES

*/

using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    [Cmdlet(VerbsCommon.New, "TfsClassificationNode", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class NewClassificationNode : BaseCmdlet
    {
        /*
                [Parameter(Mandatory=true, Position=0, ValueFromPipeline=true, ValueFromPipelineByPropertyName=true)]
                [Alias("Area")]
                [Alias("Iteration")]
                [Alias("Path")]
                public string Node { get; set; }

                [Parameter()]
                [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
                StructureGroup,

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

                [Parameter()]
                public SwitchParameter Force { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                if(! (PSBoundParameters.ContainsKey("StructureGroup"))){if (MyInvocation.InvocationName -like "*Area"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Areas}elseif (MyInvocation.InvocationName -like "*Iteration"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Iterations}else{throw new Exception("Invalid or missing StructureGroup argument"}};PSBoundParameters["StructureGroup"] = StructureGroup)

                tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                Node = _NormalizeNodePath Node -Project tp.Name -Scope StructureGroup

                if(! ShouldProcess(tp.Name, $"Create node "{Node}""))
                {
                    return
                }

                var client = GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();

                parentPath = (Split-Path Node -Parent)
                nodeName = (Split-Path Node -Leaf)

                if(! (Test-TfsClassificationNode -Node parentPath -StructureGroup StructureGroup -Project Project))
                {
                    this.Log($"Parent node "{parentPath}" does not exist");

                    if(! Force.IsPresent)
                    {
                        _throw new Exception($"Parent node "{parentPath}" does not exist. Check the path or use -Force the create any missing parent nodes.")
                    }

                    this.Log($"Creating missing parent path "{parentPath}"");

                    PSBoundParameters["Node"] = parentPath
                    PSBoundParameters["StructureGroup"] = StructureGroup

                    New-TfsClassificationNode @PSBoundParameters
                }

                patch = new Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode() -Property @{
                    Name = nodeName
                }

                task = client.CreateOrUpdateClassificationNodeAsync(patch, tp.Name, structureGroup, parentPath, $"Error creating node {node}"); result = task.Result; if(task.IsFaulted) { _throw new Exception( task.Exception.InnerExceptions })

                if (Passthru)
                {
                    WriteObject(node); return;
                }
            }
        }

        Set-Alias -Name New-TfsArea -Value New-TfsClassificationNode
        Set-Alias -Name New-TfsIteration -Value New-TfsClassificationNode
        */
    }
}
