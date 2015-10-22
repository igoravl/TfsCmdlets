<#

.SYNOPSIS
    Imports a work item type definition to a team project from XML.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Import-TfsWorkItemType
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType[]])]
    Param
    (
        [Parameter(Position=0)]
        [xml] 
        $Xml,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $tp = Get-TfsTeamProject $Project $Collection
        return $tp.WorkItemTypes | ? Name -Like $Name
    }
}
