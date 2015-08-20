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
Function Remove-TfsArea
{
	[CmdletBinding(ConfirmImpact='High', SupportsShouldProcess=$true)]
	Param
	(
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[Alias("Path")]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
		[object]
		$Area,

		[Parameter(Position=1)]
		[Alias("NewPath")]
		[ValidateScript({ ($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo]) })] 
		[object]
		$MoveTo = '\',

		[Parameter()]
		[object]
		$Project,

		[Parameter()]
		[object]
		$Collection
	)

	Process
	{
		$Areas = Get-TfsArea -Area $Area -Project $Project -Collection $Collection | Sort -Property Path -Descending

		foreach($i in $Areas)
		{
			if ($PSCmdlet.ShouldProcess($i.RelativePath, "Delete Area"))
			{
				$projectName = $i.Path.Split("\")[1]
				_DeleteCssNode -Node $i -MoveToNode $MoveTo -Scope Area -Project $projectName -Collection $Collection
			}
		}
	}
}
