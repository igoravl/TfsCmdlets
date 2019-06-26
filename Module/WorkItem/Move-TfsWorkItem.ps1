Function Move-TfsWorkItem
{
    [CmdletBinding(SupportsShouldProcess=$true, ConfirmImpact='High')]
    [OutputType('Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [Alias("id")]
        [ValidateNotNull()]
        [object]
        $WorkItem,

        [Parameter(Mandatory=$true, Position=1)]
        [object]
        $DestinationProject,

        [Parameter()]
        [object]
        $DestinationArea,

        [Parameter()]
        [object]
        $DestinationIteration,

        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.WorkItemTracking.Client)
    }

    Process
    {
        $wi = Get-TfsWorkItem -WorkItem $WorkItem -Collection $Collection

        $targetTp = Get-TfsTeamProject -Project $DestinationProject -Collection $Collection
        $tpc = $targetTp.Store.TeamProjectCollection
        
        if ($DestinationArea)
        {
            $targetArea = Get-TfsArea $DestinationArea -Project $targetTp

            if (-not $targetArea)
            {
                if ($PSCmdlet.ShouldProcess("Team Project '$($targetTp.Name)'", "Create area path '$DestinationArea'"))
                {
                    $targetArea = New-TfsArea $DestinationArea -Project $targetTp -Passthru
                }
            }

            _Log "Moving to area $($targetTp.Name)$($targetArea.RelativePath)"
        }
        else
        {
            _Log 'Area not informed. Moving to root iteration.'
            $targetArea = Get-TfsArea '\' -Project $targetTp
        }
        
        if ($DestinationIteration)
        {
            $targetIteration = Get-TfsIteration $DestinationIteration -Project $targetTp

            if (-not $targetIteration)
            {
                if ($PSCmdlet.ShouldProcess("Team Project '$($targetTp.Name)'", "Create iteration path '$DestinationIteration'"))
                {
                    $targetIteration = New-TfsIteration $DestinationIteration -Project $targetTp -Passthru
                }
            }

            _Log "Moving to iteration $($targetTp.Name)$($targetIteration.RelativePath)"
        }
        else
        {
            _Log 'Iteration not informed. Moving to root iteration.'
            $targetIteration = Get-TfsIteration '\' -Project $targetTp
        }

        if ($PSCmdlet.ShouldProcess("$($wi.WorkItemType) $($wi.Id) ('$($wi.Title)')", "Move work item to team project '$($targetTp.Name)' under area path '$($targetArea.RelativePath)' and iteration path '$($targetIteration.RelativePath)'"))
        {
            $patch = _GetJsonPatchDocument @(
                @{
                    Operation = 'Add';
                    Path = '/fields/System.TeamProject';
                    Value = $targetTp.Name
                },
                @{
                    Operation = 'Add';
                    Path = "/fields/System.AreaPath";
                    Value = "$($targetTp.Name)$($targetArea.RelativePath)"
                },
                @{
                    Operation = 'Add';
                    Path = "/fields/System.IterationPath";
                    Value = "$($targetTp.Name)$($targetIteration.RelativePath)"
                }
            )
    
            $client = _GetRestClient 'Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient' -Collection $tpc
            $resultWi = $client.UpdateWorkItemAsync($patch, $wi.Id).Result

            if (-not $resultWi)
            {
                throw "Error moving work item."
            }

            return Get-TfsWorkItem $resultWi.Id -Collection $tpc
        }
    }
}
