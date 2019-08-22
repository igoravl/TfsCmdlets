<#

.SYNOPSIS
    Exports a work item type definition from a team project to XML.

.PARAMETER Name
    Uses this parameter to filter for an specific Work Item Type.
    If suppress, cmdlet will return all Work Item Types on XML format.

.PARAMETER IncludeGlobalLists
     Exports the definitions of referenced global lists. If not specified, global list definitions are omitted.

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Export-TfsWorkItemType
{
    [CmdletBinding()]
    [OutputType('Xml')]
    Param
    (
        [Parameter()]
        [Alias('Name')]
        [SupportsWildcards()]
        [string] 
        $WorkItemType = "*",

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
        $types = Get-TfsWorkItemType -Name $WorkItemType -Project $Project -Collection $Collection

        foreach($type in $types)
        {
            $type.Export($IncludeGlobalLists)
        }
    }
}
