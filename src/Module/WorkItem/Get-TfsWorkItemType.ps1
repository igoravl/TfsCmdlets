<#

.SYNOPSIS
    Gets one or more Work Item Type definitions from a team project.

.PARAMETER Name
    Uses this parameter to filter for an specific Work Item Type.
    If suppress, cmdlet will show all Work Item Types.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.EXAMPLE
    Get-TfsWorkItemType -Name "Task" -Project "My Team Project"
    Get informations about Work Item Type "Task" of a team project name "My Team Project"

.EXAMPLE
    Get-TfsWorkItemType -Project "My Team Project"
    Get all Work Item Types of a team project name "My Team Project"

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Get-TfsWorkItemType
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType])]
    Param
    (
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias("Name")]
        [object] 
        $Type = "*",

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        if ($Type -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType])
        {
            return $Type
        }

        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection

        return $tp.WorkItemTypes | Where-Object Name -Like $Type
    }
}
