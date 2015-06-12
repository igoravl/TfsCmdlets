<#
.SYNOPSIS
	Shows information about Work Item Types.

.PARAMETER Name
	Uses this parameter to filter for an specific Work Item Type.
	If suppress, cmdlet will show all Work Item Types.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	Get-TfsWorkItemType -Name "Task" -Project "My Team Project"
	Get informations about Work Item Type "Task" of a team project name "My Team Project"

.EXAMPLE
	Get-TfsWorkItemType -Project "My Team Project"
	Get all Work Item Types of a team project name "My Team Project"
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

<#
.SYNOPSIS
	This cmdlet imports a work item type XML definition file into a team project on a Team Foundation Server.
	If a work item type with the same name already exists, the new work item type definition will overwrite the existing definition.
	If the work item type does not already exist, a new work item type will be created..

.PARAMETER Xml
	Specifies the work item type XML definition file to import.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	Import-TfsWorkItemType -Xml "C:\temp\Task.xml" -Project "MyTeamProject"
	This example imports a Work Item Type Task on Team Project "MyTeamProject".
#>

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

<#
.SYNOPSIS
	Exports a work item type on XML format.

.PARAMETER Name
	Uses this parameter to filter for an specific Work Item Type.
	If suppress, cmdlet will return all Work Item Types on XML format.

.PARAMETER IncludeGlobalLists
	 Exports the definitions of referenced global lists. If not specified, global list definitions are omitted.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	$xml = Export-TfsWorkItemType -Name Task -Project "MyTeamProject" -IncludeGlobalLists
	This example export a Work Item Type Task of Team Project "MyTeamProject" including the respective GlobalLists.
#>

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

<#
.SYNOPSIS
	Specifies that a new work item of the type entered be created. The object WorkItem of the new work item is displayed.

.PARAMETER Type
	Represents the name of the work item type to create.

.PARAMETER Title
	Specifies a Title field of new work item type that will be created.

.PARAMETER Fields
	Specifies the fields that are changed and the new values to give to them.
	FieldN The name of a field to update.
	ValueN The value to set on the fieldN.
	[field1=value1[;field2=value2;...]

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	New-TfsWorkItem -Type Task -Title "Task 1" -Project "MyTeamProject"
	This example creates a new Work Item on Team Project "MyTeamProject".
#>

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