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
		[Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias("Name")]
		[string] 
		$Type = "*",

		[Parameter(ValueFromPipeline=$true)]
		[object]
		$Project,

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
        if ($Type -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType])
        {
            return $Type
        }

		$tp = Get-TfsTeamProject -Project $Project -Collection $Collection

		return $tp.WorkItemTypes | ? Name -Like $Name
	}
}
