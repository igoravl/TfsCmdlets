<#
#>
Function Rename-TfsIteration
{
	[CmdletBinding()]
    Param
    (
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Server.NodeInfo])})] 
		[Alias("Path")]
		[object]
		$Iteration,

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
		Set-TfsIteration -Iteration $Iteration -NewName $NewName -Project $Project -Collection $Collection
    }
}
