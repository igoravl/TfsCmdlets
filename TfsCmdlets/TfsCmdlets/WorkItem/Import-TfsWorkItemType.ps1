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
		[Parameter(Position=0)]
		[xml] 
		$Xml,

		[Parameter(ValueFromPipeline=$true)]
		[object]
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
