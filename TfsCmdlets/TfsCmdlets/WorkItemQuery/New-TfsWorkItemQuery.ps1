<#
.SYNOPSIS
    Create a new work items query in the given Team Project.

.PARAMETER Query
    Specifies the path of the new work item query.
    When supplying a path, use a slash ("/") between the path segments. Leading and trailing backslashes are optional.  The last segment in the path will be the area name.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    System.String
#>
Function New-TfsWorkItemQuery
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition])]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [Alias("Name")]
        [Alias("Path")]
        [string]
        $Query,

        [Parameter()]
        [string]
        $Folder,

        [Parameter()]
        [string]
        $Definition,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $tpc = $tp.Store.TeamProjectCollection
        $store = $tp.Store

		$Query = _NormalizeQueryPath "$Folder/$Query" $tp.Name
		$folderPath = (Split-Path $Query -Parent) -replace ('\\', '/')
		$queryName = (Split-Path $Query -Leaf)

		Write-Verbose "New-TfsWorkItemQuery: Creating query '$queryName' in folder '$folderPath'"

		$folder = (_FindQueryFolder $folderPath $tp.QueryHierarchy $true)

		if (-not $folder)
		{
			throw "Invalid or non-existent work item query folder $folderPath."
		}

        if ($Definition -match "select \*")
        {
            Write-Warning "Queries containing 'SELECT *' may not work in Visual Studio. Consider replacing * with a list of fields."
        }

		$q = New-Object 'Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition' -ArgumentList $queryName, $Definition
		$folder.Values[0].Add($q)

		$tp.QueryHierarchy.Save()

		return $q
    }
}
