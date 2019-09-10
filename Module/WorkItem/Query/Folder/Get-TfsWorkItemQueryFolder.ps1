#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem
<#
.SYNOPSIS
Gets the definition of one or more work item saved queries.

.PARAMETER Query
Specifies the path of a saved query. Wildcards are supported.

.PARAMETER Project
HELP_PARAM_PROJECT

.PARAMETER Collection
HELP_PARAM_COLLECTION

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String
#>
Function Get-TfsWorkItemQueryFolder
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position=0)]
        [ValidateNotNull()]
        [SupportsWildcards()]
        [Alias("Path")]
        [object]
        $Folder = '**/*',

        [Parameter()]
        [ValidateSet('Personal', 'Shared', 'Both')]
        [string]
        $Scope = 'Both',

        [Parameter()]
        [timespan]
        $Timeout = '00:00:10',

        # Include deleted folders
        [Parameter()]
        [switch]
        $IncludeDeleted,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        CHECK_ITEM($Folder)

        GET_TEAM_PROJECT($tp,$tpc)

        GET_CLIENT('Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient')

        CALL_ASYNC($client.GetQueriesAsync($tp.Name, 'None', 2), "Error fetching work item query items")

        foreach($item in $result)
        {
            _GetQueryItemRecursively -Pattern $Folder -Item $item -IsFolder $true -Scope $Scope -IncludeDeleted $IncludeDeleted.IsPresent -Project $tp.Name -Client $Client
        }
    }
}

Function _GetQueryItemRecursively($Pattern, $Item, $IsFolder, $Scope, $Project, $Client, $Depth = 2, $IncludeDeleted)
{
    _Log "Searching for pattern '$Pattern' under $($Item.Path)"

    if($Item.HasChildren -and ($Item.Children.Count -eq 0))
    {
        _Log "Fetching child nodes for node '$($Item.Path)'"

        CALL_ASYNC($client.GetQueryAsync($Project, $Item.Path, 'None', $Depth, $IncludeDeleted), "Error retrieving $StructureGroup from path '$($Item.RelativePath)'")
        $Item = $result
    }

    _Log "Item.IsPublic = $($Item.IsPublic) and Scope = $Scope"

    if(($Item.IsFolder -eq $IsFolder) -and ($Scope -eq 'Both' -or (($Scope -eq 'Shared') -and $Item.IsPublic)))
    {
    # if($Item.Path -like $Pattern)
    # {
        _Log "$($Item.Path) matches pattern $Pattern. Returning node."
        Write-Output $Item
    }

    foreach($c in $Item.Children)
    {
        $PSBoundParameters['Item'] = $c
        _GetQueryItemRecursively @PSBoundParameters
    }
}
