<#

.SYNOPSIS
    Gets the work item link end types of a team project collection.

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
    System.String
#>
Function Get-TfsWorkItemLinkEndType
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLinkTypeEnd])]
    Param
    (
        [Parameter(Position=0, ValueFromPipeline=$true)]
        [object]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection -Collection $Collection

        return $tpc.WorkItemStore.WorkItemLinkTypes.LinkTypeEnds
    }
}
