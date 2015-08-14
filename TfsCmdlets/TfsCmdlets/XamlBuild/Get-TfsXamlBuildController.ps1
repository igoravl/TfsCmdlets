Function Get-TfsXamlBuildController
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.Build.Client.IBuildController])]
	Param
	(
		[Parameter(Position=0)]
		[ValidateScript({$_ -is [string] -or $_ -is [Microsoft.TeamFoundation.Build.Client.IBuildController]})]
		[ValidateNotNullOrEmpty()]
		[Alias("Name")]
		[object] 
		$BuildController = "*",

		[Parameter(ValueFromPipeline=$true)]
        [object]
        $Collection
	)

	Process
	{
		if ($BuildController -is [Microsoft.TeamFoundation.Build.Client.IBuildController])
		{
			return $BuildController

		}

		$tpc = Get-TfsTeamProjectCollection $Collection

		$buildServer = $tpc.GetService([type]'Microsoft.TeamFoundation.Build.Client.IBuildServer')
		$buildControllers = $buildServer.QueryBuildControllers()
		
		return $buildControllers | Where Name -Like $BuildController
 	}
}
