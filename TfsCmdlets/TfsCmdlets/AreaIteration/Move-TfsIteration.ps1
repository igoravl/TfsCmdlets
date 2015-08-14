<#
#>
Function Move-TfsIteration
{
	[CmdletBinding()]
    Param
    (
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[Alias("Path")]
		[ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
		[object]
		$Iteration,

		[Parameter(Position=1)]
		[ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
		[object]
		$Destination,

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

		$destinationNode = Get-TfsIteration -Iteration $Destination -Project $Project -Collection $Collection

		if (-not $node)
		{
			throw "Invalid or non-existent destination iteration $Destination"
		}

		$cssService = _GetCssService -Project $Project -Collection $Collection

		$cssService.MoveBranch($node.Uri, $destinationNode.Uri)

		return $cssService.GetNode($node.Uri)
    }
}
