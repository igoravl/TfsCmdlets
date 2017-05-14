<#

.SYNOPSIS
    Gets the history of changes of a work item.

.PARAMETER Collection
    ${HelpParam_Collection}

.EXAMPLE
    Get-TfsWorkItem -Filter '[System.WorkItemType] = "Task"' | % { Write-Output "WI $($_.Id): $($_.Title)"; Get-TfsWorkItemHistory -WorkItem $_ } 

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem
    System.Int32
#>
Function Get-TfsWorkItemHistory
{
    [CmdletBinding()]
    [OutputType([PSCustomObject])]
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
        $wi = Get-TfsWorkItem -WorkItem $WorkItem -Collection $Collection
        $latestRev = $wi.Revisions.Count - 1

        0..$latestRev | % {
            $rev = $wi.Revisions[$_]

            [PSCustomObject] @{
                Revision = $_ + 1;
                ChangedDate = $rev.Fields['System.ChangedDate'].Value
                ChangedBy = $rev.Fields['System.ChangedBy'].Value
                Changes = _GetChangedFields $wi $_
            }
        }
    }
}

Function _GetChangedFields([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem] $wi, [int] $rev)
{
    $result = @{}

    $wi.Revisions[$rev].Fields | ? IsChangedInRevision -eq $true | % {
        $result[$_.ReferenceName] =  [PSCustomObject] @{
            NewValue = $_.Value;
            OriginalValue = $_.OriginalValue
        }
    }

    return $result
}
