<#
.SYNOPSIS
	Create a new Iteration on Team Project.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

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
