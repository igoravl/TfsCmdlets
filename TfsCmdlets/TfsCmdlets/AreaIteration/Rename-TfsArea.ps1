<#
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
