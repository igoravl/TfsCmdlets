<#
.SYNOPSIS
	Gets one or more Areas ("Area Paths") from a given Team Project.

.PARAMETER Area
    Specifies the name, URI or path of an Area. Wildcards are permitted. If omitted, all Areas in the given Team Project are returned.

    To supply a path, use a backslash ("\") between the path segments. Leading and trailing backslashes are optional.

    When supplying a URI, use URIs in the form of "vstfs:///Classification/Node/<GUID>" (where <GUID> is the unique identifier of the given node)

.PARAMETER Project
	${Help_Project_Parameter}

.PARAMETER Collection
	${Help_Collection_Parameter}

#>
Function Get-TfsArea
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.Server.NodeInfo])]
    Param
    (
		[Parameter(Position=0)]
		[Alias("Path")]
		[ValidateScript({($_ -is [string]) -or ($_ -is [uri]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
		[SupportsWildcards()]
		[object]
		$Area = '\**',

		[Parameter(ValueFromPipeline=$true)]
		[object]
		$Project,

		[Parameter()]
		[object]
		$Collection
    )

    Process
    {
		return _GetCssNodes -Node $Area -Scope Area -Project $Project -Collection $Collection
    }
}
