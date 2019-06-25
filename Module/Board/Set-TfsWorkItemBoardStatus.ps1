Function Set-TfsWorkItemBoardStatus
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItem')]
    Param
    (
        [Parameter(ValueFromPipeline=$true, Position=0)]
        [Alias("id")]
        [ValidateNotNull()]
        [object]
        $WorkItem,

        [Parameter()]
        [object]
        $Board,

        [Parameter()]
        [object]
        $Column,

        [Parameter()]
        [object]
        $Lane,

        [Parameter()]
        [ValidateSet('Doing', 'Done')]
        [string]
        $ColumnStage,

        [Parameter()]
        [object]
        $Team,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.WorkItemTracking.Client)
        REQUIRES(Microsoft.TeamFoundation.WorkItemTracking.WebApi)
    }

    Process
    {
        if ((-not $Column) -and (-not $ColumnStage) -and (-not $Lane))
        {
            throw 'Supply a value to at least one of the following arguments: Column, ColumnStage, Lane'
        }

        if ($WorkItem -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])
        {
            $tp = $WorkItem.Project
            $tpc = $WorkItem.Store.TeamProjectCollection
        }
        else
        {
            $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
            $tpc = $tp.Store.TeamProjectCollection
            $WorkItem = Get-TfsWorkItem -WorkItem $WorkItem -Collection $Collection
        }

        $t = Get-TfsTeam -Team $Team -Project $tp -Collection $tpc
        $id = [int] $WorkItem.Id
        $rev = $WorkItem.Revision

        # Get the Kanban board column/lane field info

        $b = Get-TfsBoard -Board $Board -Team $t -Project $tp -Collection $tpc

		if (-not $b)
		{
			throw "Invalid or non-existent board '$Board' in team '$Team'"
        }

        $processMessages = @()
        
        $ops = @(
            @{
                Operation = 'Test';
                Path = '/rev';
                Value = $rev.ToString()
            }
        )

        if ($Column)
        {
            $ops += @{
                Operation = 'Add';
                Path = "/fields/$($b.Fields.ColumnField.ReferenceName)";
                Value = $Column
            }

            $processMessages += "Board Column='$Column'"
        }

        if ($Lane)
        {
            $ops += @{
                Operation = 'Add';
                Path = "/fields/$($b.Fields.RowField.ReferenceName)";
                Value = $Lane
            }

            $processMessages += "Board Lane='$Lane'"
        }

        if ($ColumnStage)
        {
            $ops += @{
                Operation = 'Add';
                Path = "/fields/$($b.Fields.DoneField.ReferenceName)";
                Value = ($ColumnStage -eq 'Done') 
            }

            $processMessages += "Board Stage (Doing/Done)='$ColumnStage'"
        }

        if ($PSCmdlet.ShouldProcess("$($WorkItem.WorkItemType) $id ('$($WorkItem.Title)')", "Set work item board status: $($processMessages -join ', ')"))
        {
            $patch = Get-JsonPatchDocument $ops
            $client = Get-RestClient 'Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient' -Collection $tpc
            $wi = $client.UpdateWorkItemAsync($patch, $id).Result
            return $wi
        }
    }
}