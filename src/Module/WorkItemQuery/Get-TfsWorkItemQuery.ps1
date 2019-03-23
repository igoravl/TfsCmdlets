<#
.SYNOPSIS
Gets the definition of one or more work item saved queries.

.PARAMETER Query
Specifies the path of a saved query. Wildcards are supported.

.PARAMETER Project
${HelpParam_Project}

.PARAMETER Collection
${HelpParam_Collection}

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String
#>
Function Get-TfsWorkItemQuery
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition])]
    Param
    (
        [Parameter(Position=0)]
        [ValidateNotNull()]
        [SupportsWildcards()]
        [Alias("Path")]
        [object]
        $Query = '**/*',

        [Parameter()]
        [ValidateSet('Personal', 'Shared', 'Both')]
        [string]
        $Scope = 'Both',

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        if($Query -is [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition2])
        {
            return $Query
        }

        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $qh = $tp.GetQueryHierarchy2($true)
        $qh.GetChildrenAsync().Wait()

        $rootFolders = ($qh.GetChildren() | Where-Object {$Scope -eq 'Both' -or $_.IsPersonal -eq ($Scope -eq 'Personal')})
        
        $rootFolders | _GetQueriesRecursively | Where-Object {($_.Path -like $Query) -or ($_.Name -like $Query) -or ($_.RelativePath -like $Query)} | Sort-Object ParentPath, Name

    }
}
