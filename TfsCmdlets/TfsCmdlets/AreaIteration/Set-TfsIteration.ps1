<#
.SYNOPSIS
	Set Iteration Dates of an specific Iteration of one Team Project.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	xxxx.
#>
Function Set-TfsIteration
{
	[CmdletBinding()]
    Param
    (
		[Parameter(Position=0, Mandatory=$true, ValueFromPipeline=$true)]
		[Alias("Path")]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
		[object]
		$Iteration = '\**',

		[Parameter()]
		[string]
		$NewName,

		[Parameter()]
		[int]
		$MoveBy,

        [Parameter()]
		[Nullable[DateTime]]
        $StartDate,
    
        [Parameter()]
		[Nullable[DateTime]]
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
		$node = Get-TfsIteration -Iteration $Iteration -Project $Project -Collection $Collection

		if (-not $node)
		{
			throw "Invalid or non-existent iteration $Iteration"
		}

		$cssService = _GetCssService -Project $Project -Collection $Collection
		$cssService4 = _GetCssService -Project $Project -Collection $Collection -Version 4

		if ($NewName)
		{
			$cssService.RenameNode($node.Uri, $NewName)
		}

		if ($MoveBy)
		{
			$cssService.ReorderNode($node.Uri, $MoveBy)
		}

		if ($StartDate -or $FinishDate)
		{
			if (-not $PSBoundParameters.ContainsKey("StartDate"))
			{
				$StartDate = $node.StartDate
			}

			if (-not $PSBoundParameters.ContainsKey("FinishDate"))
			{
				$FinishDate = $node.FinishDate
			}

			[void]$cssService4.SetIterationDates($node.Uri, $StartDate, $FinishDate)
		}

        return $cssService.GetNode($node.Uri)
    }
}
