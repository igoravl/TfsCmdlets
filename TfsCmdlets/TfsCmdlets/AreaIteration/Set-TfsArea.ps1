<#

.PARAMETER Area
    Specifies the name, URI or path of an Area. Wildcards are supported. If omitted, all Areas in the given Team Project are returned.
    To supply a path, use a backslash ("\") between the path segments. Leading and trailing backslashes are optional.
    To supply a URI instead, use URIs in the form of "vstfs:///Classification/Node/<GUID>" (where <GUID> is the unique identifier of the given node)

.PARAMETER Project
	${Help_Project_Parameter}

.PARAMETER Collection
	${Help_Collection_Parameter}

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
