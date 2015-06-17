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

Function Get-TfsWorkItem
{
	[CmdletBinding(SupportsPaging=$true)]
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

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
		$tpc = Get-TfsTeamProjectCollection $Collection
		$store = $tpc.GetService([type] "Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")

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

				Write-Output $wiql

				return _GetWorkItemByWiql $wiql $Macros $store $PSCmdlet.PagingParameters.First $PSCmdlet.PagingParameters.Skip $PSCmdlet.PagingParameters.IncludeTotalCount
			}

			"Query by Text" {
				$EscapedText = $FreeText.Replace("'", "''")
				$Wiql = "SELECT * FROM WorkItems WHERE [System.Title] CONTAINS '$EscapedText' OR [System.Description] CONTAINS '$EscapedText'"
				return _GetWorkItemByWiql $Wiql $Macros $store $PSCmdlet.PagingParameters.First $PSCmdlet.PagingParameters.Skip $PSCmdlet.PagingParameters.IncludeTotalCount
			}

			"Query by WIQL" {
				return _GetWorkItemByWiql $Query $Macros $store $PSCmdlet.PagingParameters.First $PSCmdlet.PagingParameters.Skip $PSCmdlet.PagingParameters.IncludeTotalCount
			}

			"Query by saved query" {

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

Function _GetWorkItemByWiql($Query, $Macros, $store, $First=0, $Skip=0, $IncludeTotalCount=$false)
{
	if ($Macros)
	{
		$wis = $store.Query($Query, $Macros)
	}
	else
	{
		$wis = $store.Query($Query)
	}

	if ($IncludeTotalCount)
	{
		$wis.Count
	}

	if ($First -gt 0)
	{
		$wis | Select -First $First -Skip $Skip 
	}
	else
	{
		$wis | Select -Skip $Skip
	}
}

Function _GetWorkItemType
{
	Param ($Type, $Project, $Collection)

	if ($Type -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType])
	{
		return $Type
	}

	$tp = Get-TfsTeamProject $Project $Collection

	return $tp.WorkItemTypes[$Type]
}