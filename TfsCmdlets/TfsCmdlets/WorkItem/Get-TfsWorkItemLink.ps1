<#

.SYNOPSIS
    Gets the links of a work item.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Get-TfsWorkItemLink
{
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.Link])]
    Param
    (
        [Parameter(Position=0, Mandatory=$true, ValueFromPipeline=$true)]
        [Alias("id")]
        [ValidateNotNull()]
        [object]
        $WorkItem,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $wi = Get-TfsWorkItem -WorkItem $WorkItem -Collection $Collection -Project $Project

        if ($wi)
        {
            return $wi.Links
        }
    }
}
