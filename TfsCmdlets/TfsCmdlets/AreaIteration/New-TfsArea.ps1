<#
.SYNOPSIS
	Create a new Area on the given Team Project.

.PARAMETER Area
    Specifies the name, URI or path of an Area. Wildcards are supported. If omitted, all Areas in the given Team Project are returned.
    To supply a path, use a backslash ("\") between the path segments. Leading and trailing backslashes are optional.
    To supply a URI instead, use URIs in the form of "vstfs:///Classification/Node/<GUID>" (where <GUID> is the unique identifier of the given node)

.PARAMETER Project
	${Help_Project_Parameter}

.PARAMETER Collection
	${Help_Collection_Parameter}

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
