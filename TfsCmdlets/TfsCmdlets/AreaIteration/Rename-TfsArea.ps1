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
Function Rename-TfsArea
{
	[CmdletBinding()]
    Param
    (
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
		[Alias("Path")]
		[object]
		$Area,

		[Parameter(Position=1)]
		[string]
		$NewName,

		[Parameter()]
		[object]
		$Project,

		[Parameter()]
		[object]
		$Collection
	)

    Process
    {
		Set-TfsArea -Area $Area -NewName $NewName -Project $Project -Collection $Collection
    }
}
