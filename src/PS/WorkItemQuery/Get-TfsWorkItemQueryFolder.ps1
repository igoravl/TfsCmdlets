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
        $Folder,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        if($Folder -is [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryFolder])
        {
            return $Folder
        }

        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $tpc = $tp.Store.TeamProjectCollection

        if ($Folder -match '\*')
        {
            # To do a pattern-based search, needs to retrieve *all* folders and then filter them
            return [TfsCmdlets.QueryHelper]::GetQueryFolderFromPath($tp.QueryHierarchy, $null) | Where-Object Path -like $Folder
        }

        return [TfsCmdlets.QueryHelper]::GetQueryFolderFromPath($tp.QueryHierarchy, $Folder)
    }
}
