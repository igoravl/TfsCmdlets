Function Get-TfsWorkItemType
{
	[CmdletBinding()]
	Param
	(
		[Parameter()]
		[string] 
		$Name = "*",

		[Parameter(ValueFromPipeline=$true, Mandatory=$true)]
		[object]
		[ValidateNotNull()]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])})] 
		$Project
	)

	Process
	{
		$tp = _GetTeamProject $Project
		return $tp.WorkItemTypes | ? Name -Like $Name
	}
}

Function Import-TfsWorkItemType
{

}

Function Export-TfsWorkItemType
{

}

Function New-TfsWorkItem
{
	[CmdletBinding()]
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
		$Project
	)

	Process
	{
		$wit = _GetWorkItemType $Type $Project

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
	Param ($Project)

	if ($Project -is [string])
	{
		return Get-TfsTeamProject -Name $Project
	}

	return $Project
}

Function _GetWorkItemType
{
	Param ($Type, $Project)

	if ($Type -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType])
	{
		return $Type
	}

	$tp = _GetTeamProject $Project

	return $tp.WorkItemTypes[$Type]
}