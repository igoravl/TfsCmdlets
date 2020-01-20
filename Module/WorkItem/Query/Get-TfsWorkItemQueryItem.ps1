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
Function Get-TfsWorkItemQueryItem
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position=0)]
        [ValidateNotNull()]
        [SupportsWildcards()]
        [Alias("Path")]
        [Alias("Folder")]
        [Alias("Query")]
        [object]
        $Item = '*/**',

        [Parameter()]
        [ValidateSet('Personal', 'Shared', 'Both')]
        [string]
        $Scope = 'Both',

        [Parameter()]
        [ValidateSet('Folder', 'Query', 'Both')]
        [string]
        $ItemType,

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
        CHECK_ITEM($Item)

        GET_QUERY_ITEM_TYPE

        GET_TEAM_PROJECT($tp,$tpc)

        GET_CLIENT('Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient')

        CALL_ASYNC($client.GetQueriesAsync($tp.Name, 'All', 2), "Error fetching work item query items")

        _Log "Getting $($ItemType.ToLower()) items matching '$Item'"

        foreach($i in $result)
        {
            _GetQueryItemRecursively -Pattern $Item -Item $i -ItemType $ItemType -Scope $Scope -IncludeDeleted $IncludeDeleted.IsPresent -Project $tp.Name -Client $Client
        }
    }
}

Set-Alias -Name Get-TfsWorkItemQueryFolder -Value Get-TfsWorkItemQueryItem
Set-Alias -Name Get-TfsWorkItemQuery -Value Get-TfsWorkItemQueryItem

Function _GetQueryItemRecursively($Pattern, $Item, $ItemType, $Scope, $Project, $Client, $Depth = 2, $IncludeDeleted)
{
    _Log "Found item '$($Item.Path)' (ItemType=$($Item.ItemType),IsPublic=$($Item.IsPublic),HasChildren=$($Item.HasChildren))"

    if($Item.HasChildren -and ($Item.Children.Count -eq 0))
    {
        _Log "Fetching child nodes for node '$($Item.Path)'"

        CALL_ASYNC($client.GetQueryAsync($Project, $Item.Path, 'All', $Depth, $IncludeDeleted), "Error retrieving $StructureGroup from path '$($Item.RelativePath)'")

        $Item = $result
    }

    if(($ItemType -ne 'Both') -and ($Item.ItemType -ne $ItemType))
    {
        _Log "Skipping item. '$($Item.Path)' is '$($Item.ItemType)' but ItemType is '$ItemType'."
    }
    elseif (($Scope -eq 'Both') -or `
            (($Scope -eq 'Shared') -and $Item.IsPublic) -or `
            (($Scope -eq 'Personal') -and (-not $Item.IsPublic)))
    {
        if($Item.Path -like $Pattern)
        {
            _Log "'$($Item.Path)' matches pattern '$Pattern'. Returning node."

            $Item | Add-Member -MemberType NoteProperty -Name Project -Value $Project
            Write-Output $Item
        }
        else
        {
            _Log "Skipping item. '$($Item.Path)' does not match pattern '$Pattern'."
        }
    }
    else
    {
        _Log "Skipping item. '$($Item.Path)' does not match scope '$Scope'."
    }

    foreach($c in $Item.Children)
    {
        $PSBoundParameters['Item'] = $c
        _GetQueryItemRecursively @PSBoundParameters
    }
}
