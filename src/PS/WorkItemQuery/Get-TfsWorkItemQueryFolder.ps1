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
Function Get-TfsWorkItemQueryFolder
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.QueryFolder])]
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

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        if($Folder -is [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryFolder2])
        {
            return $Folder
        }

        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $qh = $tp.GetQueryHierarchy2($false)
        $qh.GetChildrenAsync().Wait()

        $qh = $tp.GetQueryHierarchy2($true)
        $qh.GetChildrenAsync().Wait()

        $rootFolders = ($qh.GetChildren() | Where-Object {$Scope -eq 'Both' -or $_.IsPersonal -eq ($Scope -eq 'Personal')})
        
        $rootFolders | _GetQueryFoldersRecursively | Where-Object {($_.Path -like $Folder) -or ($_.Name -like $Folder)} | Sort-Object ParentPath, Name
    }
}
