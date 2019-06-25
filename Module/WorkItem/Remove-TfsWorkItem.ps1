<#

.SYNOPSIS
    Deletes a work item from a team project collection.

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem
    System.Int32
#>
Function Remove-TfsWorkItem
{
    [CmdletBinding(ConfirmImpact="High", SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(Position=0, Mandatory=$true, ValueFromPipeline=$true)]
        [Alias("id")]
        [ValidateNotNull()]
        [object]
        $WorkItem,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $ids = @()

        foreach($wi in $WorkItem)
        {
            if ($WorkItem -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])
            {
                $id = $WorkItem.Id
            }
            elseif ($WorkItem -is [int])
            {
                $id = $WorkItem
            }
            else
            {
                throw "Invalid work item ""$WorkItem"". Supply either a WorkItem object or one or more integer ID numbers"
            }

            if ($PSCmdlet.ShouldProcess("$($wi.WorkItemType) $id ('$($wi.Title)')", "Remove work item"))
            {
                $ids += $id
            }
        }

        if ($ids.Count -gt 0)
        {
            $tpc = Get-TfsTeamProjectCollection $Collection
            $store = $tpc.GetService([type] "Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")

            $errors = $store.DestroyWorkItems([int[]] $ids)
        
            if ($errors -and ($errors.Count -gt 0))
            {
                $errors | Write-Error "Error $($_.Id): $($_.Exception.Message)"

                throw "Error destroying one or more work items"
            }
        }
    }
}
