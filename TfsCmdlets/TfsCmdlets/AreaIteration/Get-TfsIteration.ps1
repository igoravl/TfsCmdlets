<#
.SYNOPSIS
	Gets one or more Iterations ("Iteration Paths") from a given Team Project.

.PARAMETER Iteration
    Specifies the name, URI or path of an Iteration Path. Wildcards are supported. If omitted, all Iterations in the given Team Project are returned.
    To supply a path, use a backslash ("\") between the path segments. Leading and trailing backslashes are optional.
    To supply a URI instead, use URIs in the form of "vstfs:///Classification/Node/<GUID>" (where <GUID> is the unique identifier of the given node)

.PARAMETER Project
	${Help_Project_Parameter}

.PARAMETER Collection
	${Help_Collection_Parameter}

#>
Function Get-TfsIteration
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
		$Iteration = '\**',

		[Parameter(ValueFromPipeline=$true)]
		[object]
		$Project,

		[Parameter()]
		[object]
		$Collection
    )

    Process
    {
		return _GetCssNodes -Node $Iteration -Scope Iteration -Project $Project -Collection $Collection
    }
}
