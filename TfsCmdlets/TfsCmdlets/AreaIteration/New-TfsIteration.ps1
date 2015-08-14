<#
.SYNOPSIS
	Create a new Iteration on Team Project.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	xxxx.
#>
Function New-TfsIteration
{
	[CmdletBinding()]
    Param
    (
		[Parameter(Mandatory=$true, Position=0)]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
		[Alias("Path")]
		[object]
		$Iteration,

        [Parameter()]
		[DateTime]
        $StartDate,
    
        [Parameter()]
		[DateTime]
        $FinishDate,

		[Parameter()]
		[object]
		$Project,

		[Parameter()]
		[object]
		$Collection
	)

    Process
    {
		return _NewCssNode -Path $Iteration -Scope Iteration -Project $Project -Collection $Collection -StartDate $StartDate -FinishDate $FinishDate
    }
}
