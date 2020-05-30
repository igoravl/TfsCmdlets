using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    [Cmdlet(VerbsCommon.Move, "WorkItem", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    //[OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem))]
    public class MoveWorkItem : BaseCmdlet
    {
        /*
                [Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
                [Alias("id")]
                [ValidateNotNull()]
                public object WorkItem { get; set; }

                [Parameter(Mandatory=true, Position=1)]
                public object Destination { get; set; }

                [Parameter()]
                public object Area { get; set; }

                [Parameter()]
                public object Iteration { get; set; }

                [Parameter()]
                public object State { get; set; }

                [Parameter()]
                public object History { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

            protected override void BeginProcessing()
            {
                #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.Client"
                #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.WebApi"
            }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                wi = Get-TfsWorkItem -WorkItem WorkItem -Collection Collection

                targetTp = Get-TfsTeamProject -Project Destination -Collection Collection
                tpc = targetTp.Store.TeamProjectCollection

                if (Area)
                {
                    targetArea = Get-TfsClassificationNode -StructureGroup Areas -Node Area -Project targetTp

                    if (! targetArea)
                    {
                        if (ShouldProcess($"Team Project "{{targetTp}.Name}"", "Create area path "Area""))
                        {
                            targetArea = New-TfsClassificationNode -StructureGroup Areas -Node Area -Project targetTp -Passthru
                        }
                    }

                    this.Log($"Moving to area {{targetTp}.Name}$(targetArea.RelativePath)");
                }
                else
                {
                    this.Log("Area not informed. Moving to root iteration.");
                    targetArea = Get-TfsClassificationNode -StructureGroup Areas -Node "" -Project targetTp
                }

                if (Iteration)
                {
                    targetIteration = Get-TfsClassificationNode -StructureGroup Iterations -Node Iteration -Project targetTp

                    if (! targetIteration)
                    {
                        if (ShouldProcess($"Team Project "{{targetTp}.Name}"", "Create iteration path "Iteration""))
                        {
                            targetIteration = New-TfsClassificationNode -StructureGroup Iterations -Node Iteration -Project targetTp -Passthru
                        }
                    }

                    this.Log($"Moving to iteration {{targetTp}.Name}$(targetIteration.RelativePath)");
                }
                else
                {
                    this.Log("Iteration not informed. Moving to root iteration.");
                    targetIteration = Get-TfsClassificationNode -StructureGroup Iterations -Node "" -Project targetTp
                }

                targetArea = $"{{targetTp}.Name}$(targetArea.RelativePath)"
                targetIteration = $"{{targetTp}.Name}$(targetIteration.RelativePath)"

                patch = _GetJsonPatchDocument @(
                    @{
                        Operation = "Add";
                        Path = "/fields/System.TeamProject";
                        Value = targetTp.Name
                    },
                    @{
                        Operation = "Add";
                        Path = "/fields/System.AreaPath";
                        Value = targetArea
                    },
                    @{
                        Operation = "Add";
                        Path = "/fields/System.IterationPath";
                        Value = targetIteration
                    }

                if (State)
                {
                    patch.Add( @{
                        Operation = "Add";
                        Path = "/fields/System.State";
                        Value = State
                    })
                }

                if (History)
                {
                    patch.Add( @{
                        Operation = "Add";
                        Path = "/fields/System.History";
                        Value = History
                    })
                }

                if (ShouldProcess($"{{wi}.WorkItemType} $(wi.Id) ("$(wi.Title)")", 
                    $"Move work item to team project "{{targetTp}.Name}" under area path " +
                    $""{{targetArea}}" and iteration path "$(targetIteration)""))
                {
                    var client = tpc.GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();
                    task = client.UpdateWorkItemAsync(patch, wi.Id)

                    result = task.Result; if(task.IsFaulted) { _throw new Exception("Error moving work item" task.Exception.InnerExceptions })

                    if(Passthru.IsPresent)
                    {
                        WriteObject(Get-TfsWorkItem result.Id -Collection tpc); return;
                    }
                }
            }
        }
        */
    }
}
