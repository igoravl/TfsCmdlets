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

		[Parameter(ValueFromPipeline=$true)]
		[object]
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
