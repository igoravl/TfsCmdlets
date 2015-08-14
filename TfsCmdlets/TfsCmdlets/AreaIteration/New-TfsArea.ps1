<#
.SYNOPSIS
	Create a new Area on Team Project.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	xxxx.
#>
Function New-TfsArea
{
	[CmdletBinding()]
    Param
    (
		[Parameter(Mandatory=$true, Position=0)]
		[Alias("Path")]
		[string]
		$Area,

		[Parameter()]
		[object]
		$Project,

		[Parameter()]
		[object]
		$Collection
	)

    Process
    {
		return _NewCssNode -Path $Area -Scope Area -Project $Project -Collection $Collection
    }
}
