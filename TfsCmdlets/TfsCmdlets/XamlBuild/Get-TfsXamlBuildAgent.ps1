Function Get-TfsXamlBuildAgent
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.Build.Client.IBuildAgent])]
	Param
	(
		[Parameter(Position=0)]
		[ValidateScript({$_ -is [string] -or $_ -is [Microsoft.TeamFoundation.Build.Client.IBuildAgent]})]
		[ValidateNotNullOrEmpty()]
		[Alias("Name")]
		[object] 
		$BuildAgent = "*",

		[Parameter(Position=0, ValueFromPipeline=$true)]
		[ValidateScript({$_ -is [string] -or $_ -is [Microsoft.TeamFoundation.Build.Client.IBuildController]})]
		[ValidateNotNullOrEmpty()]
		[Alias("Controller")]
		[object] 
		$BuildController = "*",

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
		if ($BuildAgent -is [Microsoft.TeamFoundation.Build.Client.IBuildAgent])
		{
			return $BuildAgent
		}

		$controllers = Get-TfsXamlBuildController -BuildController $BuildController -Collection $Collection

		foreach($controller in $controllers)
		{
			$controller.Agents | Where Name -Like $BuildAgent
		}		
 	}
}
