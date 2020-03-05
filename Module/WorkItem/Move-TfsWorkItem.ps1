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
        $Destination,

        [Parameter()]
        [object]
        $Area,

        [Parameter()]
        [object]
        $Iteration,

        [Parameter()]
        [object]
        $State,

        [Parameter()]
        [object]
        $History,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Passthru
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.WorkItemTracking.Client)
        REQUIRES(Microsoft.TeamFoundation.WorkItemTracking.WebApi)
    }

    Process
    {
        $wi = Get-TfsWorkItem -WorkItem $WorkItem -Collection $Collection

        $targetTp = Get-TfsTeamProject -Project $Destination -Collection $Collection
        $tpc = $targetTp.Store.TeamProjectCollection
        
        if ($Area)
        {
            $targetArea = Get-TfsClassificationNode -StructureGroup Areas -Node $Area -Project $targetTp

            if (-not $targetArea)
            {
                if ($PSCmdlet.ShouldProcess("Team Project '$($targetTp.Name)'", "Create area path '$Area'"))
                {
                    $targetArea = New-TfsClassificationNode -StructureGroup Areas -Node $Area -Project $targetTp -Passthru
                }
            }

            _Log "Moving to area $($targetTp.Name)$($targetArea.RelativePath)"
        }
        else
        {
            _Log 'Area not informed. Moving to root iteration.'
            $targetArea = Get-TfsClassificationNode -StructureGroup Areas -Node '\' -Project $targetTp
        }

        if ($Iteration)
        {
            $targetIteration = Get-TfsClassificationNode -StructureGroup Iterations -Node $Iteration -Project $targetTp

            if (-not $targetIteration)
            {
                if ($PSCmdlet.ShouldProcess("Team Project '$($targetTp.Name)'", "Create iteration path '$Iteration'"))
                {
                    $targetIteration = New-TfsClassificationNode -StructureGroup Iterations -Node $Iteration -Project $targetTp -Passthru
                }
            }

            _Log "Moving to iteration $($targetTp.Name)$($targetIteration.RelativePath)"
        }
        else
        {
            _Log 'Iteration not informed. Moving to root iteration.'
            $targetIteration = Get-TfsClassificationNode -StructureGroup Iterations -Node '\' -Project $targetTp
        }

        $targetArea = "$($targetTp.Name)$($targetArea.RelativePath)"
        $targetIteration = "$($targetTp.Name)$($targetIteration.RelativePath)"

        $patch = _GetJsonPatchDocument @(
            @{
                Operation = 'Add';
                Path = '/fields/System.TeamProject';
                Value = $targetTp.Name
            },
            @{
                Operation = 'Add';
                Path = "/fields/System.AreaPath";
                Value = $targetArea
            },
            @{
                Operation = 'Add';
                Path = "/fields/System.IterationPath";
                Value = $targetIteration
            }
        )

        if ($State)
        {
            $patch.Add( @{
                Operation = 'Add';
                Path = '/fields/System.State';
                Value = $State
            })
        }

        if ($History)
        {
            $patch.Add( @{
                Operation = 'Add';
                Path = '/fields/System.History';
                Value = $History
            })
        }

        if ($PSCmdlet.ShouldProcess("$($wi.WorkItemType) $($wi.Id) ('$($wi.Title)')", 
            "Move work item to team project '$($targetTp.Name)' under area path " +
            "'$($targetArea)' and iteration path '$($targetIteration)'"))
        {
            $client = Get-TfsRestClient 'Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient' -Collection $tpc
            $task = $client.UpdateWorkItemAsync($patch, $wi.Id)

            CHECK_ASYNC($task,$result,'Error moving work item')

            if($Passthru.IsPresent)
            {
                return Get-TfsWorkItem $result.Id -Collection $tpc
            }
        }
    }
}
