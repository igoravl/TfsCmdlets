Function Get-TfsWorkItemType
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType[]])]
	Param
	(
		[Parameter()]
		[string] 
		$Name = "*",

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
		$tp = _GetTeamProject $Project $Collection
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
		$tp = _GetTeamProject $Project $Collection
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

Function _GetTeamProject
{
	Param ($Project, $Collection)

	if ($Project -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])
	{
		return $Project
	}

	return Get-TfsTeamProject -Name $Project -Collection $Collection
}

Function _GetWorkItemType
{
	Param ($Type, $Project, $Collection)

	if ($Type -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType])
	{
		return $Type
	}

	$tp = _GetTeamProject $Project $Collection

	return $tp.WorkItemTypes[$Type]
}