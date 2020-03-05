#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem
<#
.SYNOPSIS
Create a new work items query in the given Team Project.

.PARAMETER Query
Specifies the path of the new work item query.
When supplying a path, use a slash ("/") between the path segments. Leading and trailing slashes are optional.  The last segment in the path will be the query name.

.PARAMETER Project
HELP_PARAM_PROJECT

.PARAMETER Collection
HELP_PARAM_COLLECTION

.INPUTS
System.String
#>
Function New-TfsWorkItemQueryItem
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position=0)]
        [ValidateNotNull()]
        [Alias("Path")]
        [Alias("Folder")]
        [Alias("Query")]
        [object]
        $Item,

        [Parameter()]
        [ValidateSet('Personal', 'Shared')]
        [string]
        $Scope = 'Personal',

        [Parameter()]
        [ValidateSet('Folder', 'Query')]
        [string]
        $ItemType,

        [Parameter()]
        [Alias("Definition")]
        [string]
        $Wiql,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Force,

        [Parameter()]
        [switch]
        $Passthru
    )

    Process
    {
        GET_QUERY_ITEM_TYPE

        if (-not $PSCmdlet.ShouldProcess($queryName, "Create work item $ItemType '$Item'"))
        {
            return
        }

        GET_TEAM_PROJECT($tp,$tpc)

        $client = Get-TfsRestClient 'Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient' -Collection $tpc

        
        $newItem = New-Object 'ITEM_TYPE' -Property @{
            Path = $Item
            IsFolder = ($ItemType -eq 'Folder')
        }

        _Log "New-TfsWorkItemQuery: Creating query '$queryName' in folder '$queryPath'"

        CALL_ASYNC($client.CreateQueryAsync($newItem, $tp.Name, $Item), "Error creating new $ItemType")

        if ($Passthru -or $SkipSave)
        {
            return $result
        }
    }
}

Set-Alias -Name New-TfsWorkItemQueryFolder -Value New-TfsWorkItemQueryItem
Set-Alias -Name New-TfsWorkItemQuery -Value New-TfsWorkItemQueryItem
