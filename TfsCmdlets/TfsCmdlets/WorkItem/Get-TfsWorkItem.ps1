<#

.SYNOPSIS
    Gets the contents of one or more work items.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Get-TfsWorkItem
{
    [CmdletBinding(DefaultParameterSetName="Query by text")]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])]
    Param
    (
        [Parameter(Position=0, Mandatory=$true, ParameterSetName="Query by revision")]
        [Parameter(Position=0, Mandatory=$true, ParameterSetName="Query by date")]
        [Alias("id")]
        [ValidateNotNull()]
        [object]
        $WorkItem,

        [Parameter(ParameterSetName="Query by revision")]
        [Alias("rev")]
        [int]
        $Revision,

        [Parameter(Mandatory=$true, ParameterSetName="Query by date")]
        [datetime]
        $AsOf,

        [Parameter(Mandatory=$true, ParameterSetName="Query by WIQL")]
        [Alias('WIQL')]
        [Alias('QueryText')]
        [Alias('SavedQuery')]
        [Alias('QueryPath')]
        [string]
        $Query,

        [Parameter(Mandatory=$true, ParameterSetName="Query by filter")]
        [string]
        $Filter,

        [Parameter(Position=0, Mandatory=$true, ParameterSetName="Query by text")]
        [string]
        $Text,

        [Parameter()]
        [hashtable]
        $Macros,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        if ($Project)
        {
            $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
            $tpc = $tp.Store.TeamProjectCollection
            $store = $tp.Store
        }
        else
        {
            $tpc = Get-TfsTeamProjectCollection -Collection $Collection
            $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')
        }

        if ($WorkItem -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])
        {
            if ((-Not $Revision) -and (-Not $AsOf))
            {
                return $WorkItem
            }
        }

        switch($PSCmdlet.ParameterSetName)
        {
            "Query by revision" {
                return _GetWorkItemByRevision $WorkItem $Revision $store
            }

            "Query by date" {
                return _GetWorkItemByDate $WorkItem $AsOf $store
            }

            "Query by text" {
                $localMacros = @{TfsQueryText=$Text}
                $Wiql = "SELECT * FROM WorkItems WHERE [System.Title] CONTAINS @TfsQueryText OR [System.Description] CONTAINS @TfsQueryText"
                return _GetWorkItemByWiql $Wiql $localMacros $tp $store 
            }

            "Query by filter" {
                $Wiql = "SELECT * FROM WorkItems WHERE $Filter"
                return _GetWorkItemByWiql $Wiql $Macros $tp $store 
            }

            "Query by WIQL" {
				Write-Verbose "Get-TfsWorkItem: Running query by WIQL. Query: $Query"
                return _GetWorkItemByWiql $Query $Macros $tp $store 
            }

            "Query by saved query" {
                return _GetWorkItemBySavedQuery $StoredQueryPath $Macros $tp $store 
            }
        }
    }
}



Function _GetWorkItemByRevision($WorkItem, $Revision, $store)
{
    if ($WorkItem -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])
    {
        $ids = @($WorkItem.Id)
    }
    elseif ($WorkItem -is [int])
    {
        $ids = @($WorkItem)
    }
    elseif ($WorkItem -is [int[]])
    {
        $ids = $WorkItem
    }
    else
    {
        throw "Invalid work item ""$WorkItem"". Supply either a WorkItem object or one or more integer ID numbers"
    }

    if ($Revision -is [int] -and $Revision -gt 0)
    {
        foreach($id in $ids)
        {
            $store.GetWorkItem($id, $Revision)
        }
    }
    elseif ($Revision -is [int[]])
    {
        if ($ids.Count -ne $Revision.Count)
        {
            throw "When supplying a list of IDs and Revisions, both must have the same number of elements"
        }
        for($i = 0; $i -le $ids.Count-1; $i++)
        {
            $store.GetWorkItem($ids[$i], $Revision[$i])
        }
    }
    else
    {
        foreach($id in $ids)
        {
            $store.GetWorkItem($id)
        }
    }
}

Function _GetWorkItemByDate($WorkItem, $AsOf, $store)
{
    if ($WorkItem -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])
    {
        $ids = @($WorkItem.Id)
    }
    elseif ($WorkItem -is [int])
    {
        $ids = @($WorkItem)
    }
    elseif ($WorkItem -is [int[]])
    {
        $ids = $WorkItem
    }
    else
    {
        throw "Invalid work item ""$WorkItem"". Supply either a WorkItem object or one or more integer ID numbers"
    }

    if ($AsOf -is [datetime[]])
    {
        if ($ids.Count -ne $AsOf.Count)
        {
            throw "When supplying a list of IDs and Changed Dates (AsOf), both must have the same number of elements"
        }
        for($i = 0; $i -le $ids.Count-1; $i++)
        {
            $store.GetWorkItem($ids[$i], $AsOf[$i])
        }
    }
    else
    {
        foreach($id in $ids)
        {
            $store.GetWorkItem($id, $AsOf)
        }
    }
}

Function _GetWorkItemByWiql($QueryText, $Macros, $Project, $store)
{
	if ($QueryText -notlike 'select*')
	{
		$q = Get-TfsWorkItemQuery -Query $QueryText -Project $Project

		if (-not $q)
		{
			throw "Work item query '$QueryText' is invalid or non-existent."
		}

		if ($q.Count -gt 1)
		{
			throw "Ambiguous query name '$QueryText'. $($q.Count) queries were found matching the specified name/pattern:`n`n - " + ($q -join "`n - ")
		}

		$QueryText = $q.QueryText
	}

    if (-not $Macros -and (($QueryText -match "@project") -or ($QueryText -match "@me")))
    {
        $Macros = @{}
    }

    if ($QueryText -match "@project")
    {
		if (-not $Project)
		{
			$Project = Get-TfsTeamProject -Current
		}

        if (-not $Macros.ContainsKey("Project"))
        {
            $Macros["Project"] = $Project.Name
        }
    }

    if ($QueryText -match "@me")
    {
        $user = $null
        $store.TeamProjectCollection.GetAuthenticatedIdentity([ref] $user)
        $Macros["Me"] = $user.DisplayName
    }

	Write-Verbose "Get-TfsWorkItem: Running query $QueryText"

    $wis = $store.Query($QueryText, $Macros)

    foreach($wi in $wis)
    {
        $wi
    }
}
