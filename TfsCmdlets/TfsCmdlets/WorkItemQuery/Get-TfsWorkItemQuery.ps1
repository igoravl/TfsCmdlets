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
        $Query = '*',

        [Parameter()]
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
        if($Query -is [Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition])
        {
            return $Query
        }

        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $tpc = $tp.Store.TeamProjectCollection

        if ($Folder)
        {
            if (($Folder -is [string]) -and ($Folder -notlike "$($tp.Name)/*"))
            {
                $Folder = _NormalizeQueryPath $Folder $tp.Name
            }
			
            Write-Verbose "Get-TfsWorkItemQuery: Limiting search to folder $Folder"
            
            $folders = (_FindQueryFolder $Folder $tp.QueryHierarchy)

            if (-not $folders)
            {
                throw "Query folder $Folder is invalid or missing. Be sure you provided the full path (e.g. 'Shared Queries/Current Iteration') instead of just the folder name ('Current Iteration')"
            }

            $root = $folders.Values[0]
        }
        else
        {
			Write-Verbose "Get-TfsWorkItemQuery: -Folder argument missing. Searching entire team project"
            $root = $tp.QueryHierarchy
        }

        return _FindQuery $Query $root
    }
}
