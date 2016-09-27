<#

.SYNOPSIS
    Changes the value of a property of a work item query.

.PARAMETER Query
	Specifies the path of a work item saved query.

.PARAMETER NewName
    Specifies the new name of the query. Enter only a name, not a path and name. If you enter a path that is different from the path that is specified in the area parameter, Rename-TfsWorkItemQuery generates an error. To rename and move an item, use the Move-TfsWorkItemQuery cmdlet instead.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition
    System.String
#>
Function Set-TfsWorkItemQuery
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition])]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [ValidateNotNull()] 
        [Alias("Path")]
        [object]
        $Query,

        [Parameter()]
        [string]
        $NewName,

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
        $q = Get-TfsWorkItemQuery -Query $Query -Project $Project -Collection $Collection

        if (-not $q)
        {
            throw "Invalid or non-existent work item query $queries"
        }

		if ($q.Count -ne 1)
		{
			throw "Ambiguous query name '$Query'. $($q.Count) queries were found matching the specified name/pattern:`n`n - " + ($q -join "`n - ")
		}

        if ($NewName)
        {
            $q.Name = $NewName
        }

        if ($Definition)
        {
            $q.QueryText = $Definition
        }

        $q.Project.QueryHierarchy.Save()

		return $q
    }
}
