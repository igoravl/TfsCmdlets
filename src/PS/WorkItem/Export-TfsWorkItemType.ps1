<#

.SYNOPSIS
    Exports a work item type definition from a team project to XML.

.PARAMETER Name
    Uses this parameter to filter for an specific Work Item Type.
    If suppress, cmdlet will return all Work Item Types on XML format.

.PARAMETER IncludeGlobalLists
     Exports the definitions of referenced global lists. If not specified, global list definitions are omitted.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Export-TfsWorkItemType
{
    [CmdletBinding()]
    [OutputType([Xml])]
    Param
    (
        [Parameter()]
        [SupportsWildcards()]
        [string] 
        $Name = "*",

        [Parameter()]
        [switch]
        $IncludeGlobalLists,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $types = Get-TfsWorkItemType -Name $Name -Project $Project -Collection $Collection

        foreach($type in $types)
        {
            $type.Export($IncludeGlobalLists)
        }
    }
}
