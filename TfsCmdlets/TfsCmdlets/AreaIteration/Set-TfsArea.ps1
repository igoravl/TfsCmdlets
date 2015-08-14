<#
#>
Function Set-TfsArea
{
	[CmdletBinding()]
    Param
    (
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
		[Alias("Path")]
		[object]
		$Area,

		[Parameter()]
		[string]
		$NewName,

		[Parameter()]
		[int]
		$MoveBy,

		[Parameter()]
		[object]
		$Project,

		[Parameter()]
		[object]
		$Collection
	)

    Process
    {
		$node = Get-TfsArea -Area $Area -Project $Project -Collection $Collection

		if (-not $node)
		{
			throw "Invalid or non-existent area $Area"
		}

		$cssService = _GetCssService -Project $Project -Collection $Collection

		if ($NewName)
		{
			$cssService.RenameNode($node.Uri, $NewName)
		}

		if ($MoveBy)
		{
			$cssService.ReorderNode($node.Uri, $MoveBy)
		}

		return $cssService.GetNode($node.Uri)
    }
}
