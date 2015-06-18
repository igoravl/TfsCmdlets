<#
.PARAMETER Collection

	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project

	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 

	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet.

#>
Function Get-TfsWorkItemType
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType[]])]
	Param
	(
		[Parameter(Position=1)]
		[string] 
		$Name = "*",

		[Parameter(ValueFromPipeline=$true, Mandatory=$true)]
		[object]
		[ValidateNotNull()]
		$Project,

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
		$tp = Get-TfsTeamProject $Project $Collection
		return $tp.WorkItemTypes | ? Name -Like $Name
	}
}

Function Import-TfsWorkItemType
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType[]])]
	Param
	(
		[Parameter()]
		[xml] 
		$Xml,

		[Parameter(ValueFromPipeline=$true, Mandatory=$true)]
		[object]
		[ValidateNotNull()]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])})] 
		$Project,

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
		$tp = Get-TfsTeamProject $Project $Collection
		return $tp.WorkItemTypes | ? Name -Like $Name
	}
}

Function Export-TfsWorkItemType
{
	[CmdletBinding()]
	[OutputType([xml[]])]
	Param
	(
		[Parameter()]
		[string] 
		$Name = "*",

		[Parameter()]
		[switch]
		$IncludeGlobalLists,

		[Parameter(ValueFromPipeline=$true, Mandatory=$true)]
		[object]
		[ValidateNotNull()]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])})] 
		$Project,

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
		$types = Get-TfsWorkItemType -Name $Name -Project $Project -Collection $Collection

		foreach($type in $types)
		{
			$type.Export($IncludeGlobalLists)
		}
	}
}

Function New-TfsWorkItem
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])]
	Param
	(
		[Parameter(ValueFromPipeline=$true, Mandatory=$true)]
		[object] 
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType])})] 
		$Type,

		[Parameter()]
		[string]
		$Title,

		[Parameter()]
		[hashtable]
		$Fields,

		[Parameter()]
		[object]
		[ValidateScript({($_ -eq $null) -or ($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])})] 
		$Project,

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
		$wit = _GetWorkItemType $Type $Project $Collection

		$wi = $wit.NewWorkItem()
		$wi.Title = $Title

		foreach($field in $Fields)
		{
			$wi.Fields[$field.Key] = $field.Value
		}

		$wi.Save()

		return $wi
	}
}

<#
.SYNOPSIS
    Gets one or more work items
#>
Function Get-TfsWorkItem
{
	[CmdletBinding()]
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
		[string]
		$Query,

		[Parameter(Mandatory=$true, ParameterSetName="Query by saved query")]
		[string]
		$QueryName,

		[Parameter(Mandatory=$true, ParameterSetName="Query by text")]
		[string]
		$FreeText,

		[Parameter(Mandatory=$true, ParameterSetName="Query by fields")]
		[ValidateNotNull()]
		[ValidateScript({ $_.Count -gt 0 })]
		[string[]]
		$Where,

		[Parameter()]
		[object]
		[ValidateScript({($_ -eq $null) -or ($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])})] 
		$Project,

		[Parameter()]
		[hashtable]
		$Macros,

		[Parameter(ValueFromPipeline=$true)]
        [object]
        $Collection
	)

	Process
	{
		$tpc = Get-TfsTeamProjectCollection $Collection
		$store = $tpc.GetService([type] "Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")
		
		if ($Project)
		{
			$Project = Get-TfsTeamProject $Project
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

			"Query by fields" {
				$tokens = [string[]]@()

				for($i = 0; $i -lt $Where.Count; $i++)
				{
                    $tokens += "($($Where[$i]))"
				}

				$wiql = "SELECT * FROM WorkItems WHERE $([string]::Join(" AND ", $tokens))"

				return _GetWorkItemByWiql $wiql $Macros $store 
			}

			"Query by Text" {
				$EscapedText = $FreeText.Replace("'", "''")
				$Wiql = "SELECT * FROM WorkItems WHERE [System.Title] CONTAINS '$EscapedText' OR [System.Description] CONTAINS '$EscapedText'"
				return _GetWorkItemByWiql $Wiql $Macros $store 
			}

			"Query by WIQL" {
				return _GetWorkItemByWiql $Query $Macros $Project $store 
			}

			"Query by saved query" {
				return _GetWorkItemBySavedQuery $QueryName $Macros $Project $store 
			}
		}
	}
}

Function Remove-TfsWorkItem
{
	[CmdletBinding(ConfirmImpact="High", SupportsShouldProcess=$true)]
	Param
	(
		[Parameter(Position=0, Mandatory=$true, ValueFromPipeline=$true)]
		[Alias("id")]
		[ValidateNotNull()]
		[object]
		$WorkItem,

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
		$ids = @()

		foreach($wi in $WorkItem)
		{
			if ($WorkItem -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])
			{
				$id = $WorkItem.Id
			}
			elseif ($WorkItem -is [int])
			{
				$id = $WorkItem
			}
			else
			{
				throw "Invalid work item ""$WorkItem"". Supply either a WorkItem object or one or more integer ID numbers"
			}

			if ($PSCmdlet.ShouldProcess("ID: $id", "Destroy workitem"))
			{
				$ids += $id
			}
		}

		if ($ids.Count -gt 0)
		{
			$tpc = Get-TfsTeamProjectCollection $Collection
			$store = $tpc.GetService([type] "Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")

			$errors = $store.DestroyWorkItems([int[]] $ids)
		
			if ($errors -and ($errors.Count -gt 0))
			{
				$errors | Write-Error "Error $($_.Id): $($_.Exception.Message)"

				throw "Error destroying one or more work items"
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

Function _GetWorkItemByWiql($Query, $Macros, $Project, $store)
{
	if (-not $Macros)
	{
		$Macros = @{}
	}

	if (($Query -match "@project") -and $Project)
	{

		if (-not $Macros.ContainsKey("Project"))
		{
			$Macros["Project"] = $Project.Name
		}
	}

    if ($Query -match "@me")
    {
        $user = $null
        $store.TeamProjectCollection.GetAuthenticatedIdentity([ref] $user)
        $Macros["Me"] = $user.DisplayName
    }

	$wis = $store.Query($Query, $Macros)

	foreach($wi in $wis)
	{
		$wi
	}
}

Function _GetWorkItemBySavedQuery($QueryName, $Macros, $Project, $store)
{
	if ($Macros)
	{
		$wis = $store.Query($Query, $Macros)
	}
	else
	{
		$wis = $store.Query($Query)
	}

	foreach($wi in $wis)
	{
		$wi
	}
}

Function _GetWorkItemType($Type, $Project, $Collection)
{
	if ($Type -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType])
	{
		return $Type
	}

	$tp = Get-TfsTeamProject $Project $Collection

	return $tp.WorkItemTypes[$Type]
}